using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Board
    {
        static public bool playPiece(int col,int player)
        {
            if (col >= Program.width)
            {
                return false;
            }
            else if(Program.b[Program.height-1,col] != 0)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < Program.height; i++)
                {
                    if(Program.b[i,col]==0)
                    {
                        Program.b[i, col] = player;
                        break;
                    }
                }
                return true;
            }

        }
        static public bool isWin(int[,]v, int player)
        {
            //check horizontal win condition --b.size -height
            for(int i = 0; i < Program.height; i++)
            {
                int stack = 0;
                bool prev = false;
                for(int j = 0; j < Program.width; j++)
                {
                    if(prev == false && v[i,j] == player)
                    {
                        prev = true;
                        stack++;
                    }
                    else if (prev==true && v[i,j] == player)
                    {
                        stack++;
                        if(stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                    }
                }
            }
            //check vertical win condition
            for(int j = 0; j < Program.width; j++)
            {
                int stack = 0;
                bool prev = false;
                for(int i = 0; i < Program.height; i++)
                {
                    if(prev==false && v[i,j] == player)
                    {
                        prev = true;
                        stack++;
                    }
                    else if(prev==true && v[i,j] == player)
                    {
                        stack++;
                        if(stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                    }
                }
            }
            //check main diagonal(\) win condition
            for(int i = 1; i < Program.height; i++)
            {
                int stack = 0;
                bool prev = false;
                int ri = i;
                for(int j = 0; j <= i; j++)
                {
                    if (v[ri, j] == player)
                    {
                        if (prev == false)
                        {
                            prev = true;
                        }
                        stack++;
                        if (stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                    }
                    ri--;
                }
            }
            for(int j = 1; j < Program.width - 1; j++)
            {
                int stack = 0;
                bool prev = false;
                int rj = j;
                for(int i = Program.height - 1; i >= j - 1; i--)
                {
                    if (v[i, rj] == player)
                    {
                        if (prev == false)
                        {
                            prev = true;
                        }
                        stack++;
                        if (stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                    }
                    rj++;
                }
            }
            //check anti diagonal(/) win condition
            for(int i = 0; i < Program.height - 1; i++)
            {
                int stack = 0;
                bool prev = false;
                int ri = i;
                for(int j = 0; j < Program.width - i - 1; j++)
                {
                    if(v[ri,j] == player)
                    {
                        if (prev == false)
                        {
                            prev = true;
                        }
                        stack++;
                        if (stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                        ri++;
                    }
                }

            }
            for(int j = 1; j < Program.width - 1; j++)
            {
                int stack = 0;
                bool prev = false;
                int rj = j;
                for (int i = 0; i < Program.height - j + 1; i++)
                {
                    if (v[i, rj] == player)
                    {
                        if (prev == false)
                        {
                            prev = true;
                        }
                        stack++;
                        if (stack == Program.winsize)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        prev = false;
                        stack = 0;
                    }
                    rj++;
                }
            }
            //no win condition is met
            return false;
        }
    }
}
