using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        int[,] board = new int [3,3];

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void fillArray()
        {
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        bool check(int playedValue, int loc)
        {
            string strP = playedValue == 1 ? "X" : "O";
            switch (loc)
            {
                case 0:
                    if(board[0, 1] == playedValue && board[0, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 0] == playedValue && board[2, 0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 1] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 1:
                    if (board[0, 0] == playedValue && board[0, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 1] == playedValue && board[2, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 2:
                    if (board[0, 0] == playedValue && board[0, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 2] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 1] == playedValue && board[2, 0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 3:
                    if (board[1, 1] == playedValue && board[1, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0, 0] == playedValue && board[2, 0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 4:
                    if (board[0, 1] == playedValue && board[2, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 0] == playedValue && board[1, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0, 2] == playedValue && board[2, 0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0, 0] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 5:
                    if (board[0, 2] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 0] == playedValue && board[1, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 6:
                    if (board[0, 0] == playedValue && board[1, 0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2, 1] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1, 1] == playedValue && board[0, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 7:
                    if (board[0, 1] == playedValue && board[1, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2, 0] == playedValue && board[2, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 8:
                    if (board[0, 0] == playedValue && board[1, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0, 2] == playedValue && board[1, 2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2, 0] == playedValue && board[2, 1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
            } // end of switch statement
            return false;
        } // end of function check

        void play(int val, int row, int col)
        {
            board[row, col] = val;
            if(row == 0 && col == 0)
                label1.Text = (val == 1) ? "X" : "O";
            else if(row == 0 && col == 1)
                label2.Text = (val == 1) ? "X" : "O";
            else if (row == 0 && col == 2)
                label3.Text = (val == 1) ? "X" : "O";
            else if (row == 1 && col == 0)
                label4.Text = (val == 1) ? "X" : "O";
            else if (row == 1 && col == 1)
                label5.Text = (val == 1) ? "X" : "O";
            else if (row == 1 && col == 2)
                label6.Text = (val == 1) ? "X" : "O";
            else if (row == 2 && col == 0)
                label7.Text = (val == 1) ? "X" : "O";
            else if (row == 2 && col == 1)
                label8.Text = (val == 1) ? "X" : "O";
            else
                label9.Text = (val == 1) ? "X" : "O";
        }

        private void simulate()
        {
            Random rnd = new Random();
            int value_to_play = rnd.Next(1, 3);
            foreach(var control in this.Controls)
            {
                if(control is Label)
                {
                    Label l = (Label)control;
                    l.Text = "";
                }
            }

            List<int> locations = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            fillArray();
            while (locations.Count > 0)
            {
                int loc_position = rnd.Next(0, locations.Count);
                int loc = locations[loc_position];
                int row = loc / 3;
                int col = loc % 3;
                play(value_to_play, row, col);
                locations.RemoveAt(loc_position);
                if (locations.Count <= 4)
                {
                    if (check(value_to_play, loc))
                        return;
                }
                
                value_to_play = (value_to_play == 1) ? 2 : 1;
            }

            label10.Text = "It is a draw!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            simulate();
        }
    }
}
