using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Ai
    {
        static public void copyArray(int [,]v, int[,]b)
        {
            for (int i = 0; i < Program.height; i++)
            {
                for (int j = 0; j < Program.width; j++)
                {
                    b[i, j] = v[i, j];
                }
            }
        }
        static public int getBestMove(int player)
        {
            //returns move with highest score using scoreMove function

            int lookahead = 2;
            int bestMove = 0;
            double highestScore = -100000;
            /*
            debug save scores in array
            double[] scores = new double[Program.width];
            for(int i = 0; i < Program.width; i++)
            {
                scores[i] = highestScore;
            }
            */
            Random random = new Random();
            for (int col = 0; col < Program.width; col++)
            {
                //only score avalible columns
                if(Program.b[Program.height-1,col] == 0)
                {
                    double s = scoreMove(Program.b, col, lookahead, player);//todo add func
                    //add some randomness <1 to score
                    s += random.NextDouble();
                    //update bestMove if higher score is found
                    if (s > highestScore)
                    {
                        highestScore = s;
                        bestMove = col;
                    }
                    //scores[col] = s;
                }
            }
            return bestMove;
        }
        static int getLowestRow(int[,]v, int col)
        {
            //helper function - return lowest avalible position in column given 2d array
            for(int i = 0; i < Program.height; i++)
            {
                if (v[i, col] == 0)
                {
                    return i;
                }
            }
            return Program.height;
        }
        static double scoreMove(int[,] v, int aiMove, int lookahead, double a = 1)
        {
            double alpha = 0.1;  //alpha add to priortize earlier  moves
            //base case - eval board state
            if(lookahead == 0)
            {
                for (int i = 0; i < Program.width; i++)
                {
                    //make winning move if able
                    if (Board.isWin(v, 2))
                    {
                        return 150;
                    }
                    //else avoid making move that allows player to win
                    else if (Board.isWin(v, 1))
                    {
                        return -100;
                    }
                    else
                    {
                        return 0;
                    }
                }
                return 0;
            }
            //recursive case
            else
            {
                double score = 0;
                //ai move
                int aiLowestRow = Ai.getLowestRow(v, aiMove);
                if(aiLowestRow < Program.height)
                {
                    //todo may be pass by ref
                    int[,] b2 = new int[Program.height, Program.width];
                    copyArray(v, b2);
                    b2[aiLowestRow, aiMove] = 2;
                    //player move
                    for(int playerMove = 0; playerMove < Program.width; playerMove++)
                    {
                        if (Ai.getLowestRow(b2, playerMove) < Program.height)
                        {
                            int[,] b = new int[Program.height, Program.width];
                            copyArray(b2,b);
                            b[Ai.getLowestRow(b, playerMove), playerMove] = 1;
                            score += a * scoreMove(b, aiMove, lookahead - 1, a * alpha);
                        }
                    }
                }
                return score;
            }
        }
    }
}
