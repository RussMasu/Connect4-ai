using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//todo add controls to play first, second, or random
namespace Connect4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            turnOrder();
            button1.MouseDoubleClick += button1_Click;
            button2.MouseDoubleClick += button2_Click;
            button3.MouseDoubleClick += button3_Click;
            button4.MouseDoubleClick += button4_Click;
            button5.MouseDoubleClick += button5_Click;
            button6.MouseDoubleClick += button6_Click;
            button7.MouseDoubleClick += button7_Click;
        }
        private string getImagePath(int x,int y)
        {
            /*
            reads index x from array and returns correct image path
            */
            string imagepath;

            if (Program.b[x,y] == 1)
            {
                imagepath = "images/red.png";
            }
            else if (Program.b[x, y] == 2)
            {
                imagepath = "images/yellow.png";
            }
            else
            {
                imagepath = "images/white.png";
            }
            return imagepath;
        }
        private void turnOrder()
        {
            Random random = new Random();
            if (random.Next(2) == 0)
            {
                label1.Text = "Player 2 goes first";
                //ai move
                int aiMove = Ai.getBestMove(2);
                Board.playPiece(aiMove, 2);
                //update board
                updateBoard();
            }
            else
            {
                label1.Text = "Player 1 goes first";
            }
        }
        private void updateBoard()
        {
            PictureBox[,] boxes = new PictureBox[,] {
                { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 },
                { pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14 },
                { pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21 },
                { pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28 },
                { pictureBox29, pictureBox30, pictureBox31, pictureBox32, pictureBox33, pictureBox34, pictureBox35 },
                { pictureBox36, pictureBox37, pictureBox38, pictureBox39, pictureBox40, pictureBox41, pictureBox42 }
            };
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                for (int j = 0; j < boxes.GetLength(1); j++)
                {
                    boxes[i, j].Load(getImagePath(i, j));
                }
            }
            //update label1 if wincondition is met
            if (Board.isWin(Program.b,1))
            {
                label1.Text = "Player 1 has won";
            }
            else if (Board.isWin(Program.b,2))
            {
                label1.Text = "Player 2 has won";
            }
        }
        private void updateGame(int col)
        {
            label1.Text = "Play to a column!";
            //to do have game continue after col is full
            if (Program.b[Program.height - 1, 0] != 0)
            {
                label1.Text = "Column is full";
            }
            else
            {
                //player move
                Board.playPiece(col, 1);
                //ai move
                int aiMove = Ai.getBestMove(2);
                Board.playPiece(aiMove, 2);
                //update board
                updateBoard();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            updateGame(0);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            updateGame(1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            updateGame(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateGame(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateGame(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            updateGame(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            updateGame(6);
        }
    }
}
