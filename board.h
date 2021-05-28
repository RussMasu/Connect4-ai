#pragma once
#include<iostream>
#include<vector>
using namespace std;

class Board {
public:
    bool playPiece(int player, int col);
    bool isWin(int player);
    void printBoard();
    vector<vector<int>> getBoard();
    int getTurn();
    void incrementTurn();
    void loadGame(vector<vector<int>>v);
    int playGame();
    Board(); //default constructor
private:
    vector<vector<int>>b;
    int height;
    int width;
    int winSize;
    int turn;
};
//split function definitions into seperate cpp file