//should only include h file
#include "board.h"

Board::Board() {
    //stuff that happens on ini
    height = 6;
    width = 7;
    winSize = 4;
    turn = 0;

    for (int i = 0; i < height; i++) {
        vector<int>col;
        for (int j = 0; j < width; j++) {
            col.push_back(0);
        }
        b.push_back(col);
    } 
}
bool Board::playPiece(int col, int player) {
    if (col >= width) {
        cout << "Cannot play to col outside of board" << endl;
        return false;
    }
    if (b[height-1][col] != 0) {
        //cout << "Cannot play to col that is full" << endl;
        return false;
    }
    else {
        for (int i = 0; i < b.size(); i++) {
            if (b[i][col] == 0) {
                b[i][col] = player;
                return true;
            }
        }
    }
    return true;

}
void Board::printBoard() {
    for (int i = b.size()-1; i >= 0; i--) {
        for (int j = 0; j < b[i].size(); j++) {
            cout << b[i][j]<<" ";
        }
        cout << endl;
    }
    cout << endl;

}
bool Board::isWin(int player) {
    /*check to see if player won the game of connect four*/
    //check horizontal win condition
    for (int i = 0; i < b.size(); i++) {
        int stack = 0;
        bool prev = false;
        for (int j = 0; j < b[i].size(); j++) {
            if (prev == false && b[i][j] == player) {
                prev = true;
                stack++;
            }
            else if (prev == true && b[i][j] == player) {
                stack++;
                if (stack == winSize) {
                    //player has won with a horizontal stack
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }
        }
    }
    //check vertical win condition
    for (int j = 0; j < b[0].size(); j++) {
        int stack = 0;
        bool prev = false;
        for (int i = 0; i < b.size(); i++) {
            if (prev == false && b[i][j] == player) {
                prev = true;
                stack++;
            }
            else if (prev == true && b[i][j] == player) {
                stack++;
                if (stack == winSize) {
                    //player has won with a vertical stack
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }

        }
    }
    //check main diagonal (\) win condition
    for (int i = 1; i < b.size(); i++) {
        int stack = 0;
        bool prev = false;
        int ri = i;
        for (int j = 0; j <= i; j++) {
            if (b[ri][j] == player) {
                if (prev == false) {
                    prev = true;
                }
                stack++;
                if (stack == winSize) {
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }
            ri--;
        }
    }
    for (int j = 1; j < b[0].size() - 1; j++) {
        int stack = 0;
        bool prev = false;
        int rj = j;
        for (int i = b.size() - 1; i >= j-1; i--) {
            if (b[i][rj] == player) {
                if (prev == false) {
                    prev = true;
                }
                stack++;
                if (stack == winSize) {
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }
            rj++;
        }
    }
    //check anti diagonal (/) win condition
    for(int i = 0; i < b.size()-1; i++){
        int stack = 0;
        bool prev = false;
        int ri = i;
        for (int j = 0; j < b[0].size() - i -1; j++) {
            if (b[ri][j] == player) {
                if (prev == false) {
                    prev = true;
                }
                stack++;
                if (stack == winSize) {
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }
            ri++;
        }
    }
    for (int j = 1; j < b[0].size() - 1; j++) {
        int stack = 0;
        bool prev = false;
        int rj = j;
        for (int i = 0; i <b.size()-j+1; i++) {
            if (b[i][rj] == player) {
                if (prev == false) {
                    prev = true;
                }
                stack++;
                if (stack == winSize) {
                    return true;
                }
            }
            else {
                prev = false;
                stack = 0;
            }
            rj++;
        }
    }
  
    //all win conditions false
    return false;

}
void Board::loadGame(vector<vector<int>>v) {
    for (int i = 0; i < v.size(); i++) {
        for (int j = 0; j < v[i].size(); j++) {
            b[i][j] = v[i][j];
        }
    }
}
int Board::playGame() {
    /* runs the game after board object has been created*/
    cout << "Type a number between 0 - 6 to play to that column" << endl;
    while (turn < height * width) {
        printBoard();
        int playerID = turn % 2 + 1;
        int col;
        
        do {
            cin >> col;
        }
        while (!playPiece(col,playerID));

        if (isWin(playerID)) {
            printBoard();
            cout << playerID << " has won the game." << endl;
            return playerID;
        }
        turn++;
    }
}

vector<vector<int>> Board::getBoard() {
    return b;
}

int Board::getTurn() {
    return turn;
}
void Board::incrementTurn() {
    turn++;
}