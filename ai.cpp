#include"board.h"
using namespace std;
// test game
// possible unneeded board states being generated in else
// rewrite algo using mini-max assumption?
//write better ui?
//LOW fix score returned by lookahead == 3, optimize for lookahead == 3

void printBoard(vector<vector<int>>v) {
	for (unsigned int i = 0; i < v.size(); i++) {
		for (unsigned int j = 0; j < v[i].size(); j++) {
			cout << v[i][j] << " ";
		}
		cout << endl;
	}
}

double randrange(double a = 0, double b = 1) {
	double r = ((double)rand() / (RAND_MAX));
	double scaled = a + r * (b - a);
	return scaled;
}
unsigned int getLowestRow(vector<vector<int>>v, int col) {
	for (unsigned int i = 0; i < v.size(); i++) {
		if (v[i][col] == 0) {
			return i;
		}
	}

	return v.size();
}

void sumVector(vector<double>& a, vector<double>b) {
	//sum vector of same size
	if (a.size() == b.size()) {

		for (unsigned int i = 0; i < b.size(); i++) {
			a[i] += b[i];
		}
	}
	else {
		cout << "vectors are of different sizes\n";
	}
}

double scoreMove(vector<vector<int>>v, int aiMove, int lookahead, double a = 1, bool first = true) {
	double alpha = 0.1; //set to  > 1/7
	//base case - eval board state
	if (lookahead == 0) {
		//simulate game
		for (unsigned int i = 0; i < v[0].size(); i++) {
			//double score = 0;
			Board game;
			game.loadGame(v);

			//make winning move if able
			if (game.isWin(2)) {
				return 150;
			}
			//avoid making moves that allows player to win on next turn
			else if (game.isWin(1)) {
				return -100;
			}
			else {
				return 0;
			}
		}
		return 0;
	}
	else if (first == true) {
		//recursive case - update board state with ai & player moves
		double score = 0;
		//ai move
		unsigned int aiLowestRow = getLowestRow(v, aiMove);
		if (aiLowestRow < v.size()) {
			v[aiLowestRow][aiMove] = 2;
			//player move
			for (unsigned int playerMove = 0; playerMove < v[0].size(); playerMove++) {
				if (getLowestRow(v, playerMove) < v.size()) {
					vector<vector<int>>b = v;
					b[getLowestRow(b, playerMove)][playerMove] = 1;
					score += a * scoreMove(b, aiMove, lookahead - 1, a * alpha, false);
				}
			}
		}

		return score;
	}
	else {
		double score = 0;
		//ai move
		for (unsigned int amove = 0; amove < v[0].size(); amove++) {
			unsigned int aLowestRow = getLowestRow(v, amove);
			if (aLowestRow < v.size()) {
				//player move
				for (unsigned int playerMove = 0; playerMove < v[0].size(); playerMove++) {
					vector<vector<int>>b = v;
					b[aLowestRow][amove] = 2;
					unsigned int pLowestRow = getLowestRow(b, playerMove);
					if (pLowestRow < v.size()) {
						b[pLowestRow][playerMove] = 1;
						score += (a * scoreMove(b, amove, lookahead - 1, a * alpha, false)) / (v[0].size() - 1);
					}
				}
			}
		}
		return score;
	}
}

int getBestMove(vector<vector<int>>v, int pID) {
	int lookahead = 2;
	int bestMove = 0;
	double highestScore = -100000;
	vector<double>scores(v[0].size(), -100000);

	for (unsigned int col = 0; col < v[0].size(); col++) {//todo change to v[0].size()
		//only run score function on rows that are not full
		if (getLowestRow(v, col) < v.size()) {
			double s = scoreMove(v, col, lookahead);
			//add some randomness <1 to score
			s += randrange();
			if (s > highestScore) {
				highestScore = s;
				bestMove = col;
			}
			scores[col] = s;
		}
	}
	//debug
	for (unsigned int i = 0; i < scores.size(); i++) {
		cout << scores[i] << " ";
	}
	cout << endl;

	return bestMove;
}

int main() {
	Board game1;
	int playerStart = 0; //set either to 0 or 1
	int playerID;

	cout << "Type a number between 0 - 6 to play to that column" << endl;

	//play game
	while (true) {
		game1.printBoard();
		if (game1.getTurn() % 2 == playerStart) {
			//player turn
			playerID = 1;
			int col;

			do {
				cin >> col;
			} while (!game1.playPiece(col, playerID));

		}
		else {
			//ai turn
			playerID = 2;

			vector<vector<int>>gam = game1.getBoard();
			int move = getBestMove(gam, playerID);
			cout << endl << "ai move " << move << endl;
			game1.playPiece(move, playerID);
		}

		if (game1.isWin(playerID)) {
			game1.printBoard();
			cout << playerID << " has won the game." << endl;
			return playerID;
		}
		game1.incrementTurn();
	}

	return 0;
}