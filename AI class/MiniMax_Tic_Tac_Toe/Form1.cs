using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MiniMax_Tic_Tac_Toe
{
    /*
     * Human = "X"
     * Machine = "O"
     */
    struct mTREE // stands for move tree, keeps the possible states (after some legit moves) in memory, needed for the machine player
    {
        public int[][] state;
        public int utility; // the potential quality of this move (considering its children nodes)
        public int loc; // the location in state where a player makes a move to, i.e. the move made at this point 
        public mTREE[] children;
    };

    public partial class Form1 : Form
    {
        int[][] board = new int[3][];
        Random rnd = new Random();
        int move_max;
        List<int> locations = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 3; i++)
                board[i] = new int[3];
        }

        private void button3_Click(object sender, EventArgs e) // 'make move' button - for the human player
        {
            string choice_loc = comboBox1.Text;
            int loc = int.Parse(choice_loc) - 1;
            int row = loc / 3;
            int col = loc % 3;
            play(1, row, col);
            locations.Remove(loc);
            comboBox1.Items.Remove(loc + 1);
            if (locations.Count <= 4)
            {
                if (check(1, loc))
                {
                    button3.Enabled = false;
                    return;
                }
            }

            if (locations.Count == 0)
            {
                label10.Text = "It is a draw";
                button3.Enabled = false;
                return;
            }

            make_move(); // machine makes move
        }

        private void make_move()
        {
            minimax();
            int loc = move_max;
            int row = loc / 3;
            int col = loc % 3;
            play(2, row, col); // make the move of maximum reward according to the MiniMax algorithm
            locations.Remove(loc);
            comboBox1.Items.Remove(loc + 1);
            if (locations.Count <= 4)
            {
                if (check(2, loc))
                {
                    button3.Enabled = false;
                    return;
                }
            }

            if (locations.Count == 0)
            {
                label10.Text = "It is a draw";
                button3.Enabled = false;
                return;
            }

            MessageBox.Show("It is your turn to play");
        }

        void fillArray()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i][j] = 0;
                }
            }
        }

        void play(int val, int row, int col)
        {
            board[row][col] = val;
            if (row == 0 && col == 0)
                label1.Text = (val == 1) ? "X" : "O";
            else if (row == 0 && col == 1)
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

        bool check(int playedValue, int loc)
        {
            string strP = playedValue == 1 ? "X" : "O";
            switch (loc)
            {
                case 0:
                    if (board[0][1] == playedValue && board[0][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][0] == playedValue && board[2][0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][1] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 1:
                    if (board[0][0] == playedValue && board[0][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][1] == playedValue && board[2][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 2:
                    if (board[0][0] == playedValue && board[0][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][2] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][1] == playedValue && board[2][0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 3:
                    if (board[1][1] == playedValue && board[1][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0][0] == playedValue && board[2][0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 4:
                    if (board[0][1] == playedValue && board[2][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][0] == playedValue && board[1][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0][2] == playedValue && board[2][0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0][0] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 5:
                    if (board[0][2] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][0] == playedValue && board[1][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 6:
                    if (board[0][0] == playedValue && board[1][0] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2][1] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[1][1] == playedValue && board[0][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 7:
                    if (board[0][1] == playedValue && board[1][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2][0] == playedValue && board[2][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
                case 8:
                    if (board[0][0] == playedValue && board[1][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[0][2] == playedValue && board[1][2] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    else if (board[2][0] == playedValue && board[2][1] == playedValue)
                    {
                        label10.Text = strP + " Wins!";
                        return true;
                    }
                    break;
            } // end of switch statement
            return false;
        } // end of function check

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        void getLegitActions(List<int> list, int[][] arr)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i][j] == 0)
                    {
                        //reverse map (row, col) to a single integer value
                        int loc = i * 3 + j;
                        list.Add(loc);
                    }
                }
            }
        }

        void map_copy_state(int[][] arr, int[][] copy_arr)
        {
            for (int i = 0; i < 3; i++)
            {
                copy_arr[i] = new int[3];
                Array.Copy(arr[i], copy_arr[i], arr[i].Length);
            }
        }

        void minimax()
        {
            int[][] state = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                state[i] = new int[3];
                Array.Copy(board[i], state[i], board[i].Length);
            }

            mTREE tree = new mTREE();
            tree.state = state;
            max_value(ref tree);
            move_max = tree.loc; // at this point, a move has been recommended by one of tree's children
        }

        int max_value(ref mTREE t)
        {
            int utility;
            if ((utility = compute_utility(t.state)) != 2)
            {
                t.utility = utility;
                return utility;
            }

            List<int> actions = new List<int>(); // list of legitimate places left for "X" or "O" to be played
            getLegitActions(actions, t.state); // retrieve legitimate moves yet to be made
            int max_v = int.MinValue;
            int i = 0;
            t.children = new mTREE[actions.Count];
            foreach(int a in actions)
            {
                int row = a / 3;
                int col = a % 3;
                t.children[i] = new mTREE();
                t.children[i].state = new int[3][];
                map_copy_state(t.state, t.children[i].state); // create a copy of the current state
                t.children[i].state[row][col] = 2; // make the simulated move
                int v = min_value(ref t.children[i]); // the utility
                if ((v > max_v) || (v == max_v && rnd.Next(0, 2) == 0))
                {
                    max_v = v;
                    t.utility = max_v;
                    t.loc = a; // the move itself, the position to play "O"
                }

                i++;
            }

            return max_v;
        }

        int min_value(ref mTREE t)
        {
            int utility;
            if ((utility = compute_utility(t.state)) != 2)
            {
                t.utility = utility;
                return utility;
            }

            List<int> actions = new List<int>(); // list of legitimate places left for "X" or "O" to be played
            getLegitActions(actions, t.state); // retrieve legitimate moves yet to be made
            int min_v = int.MaxValue;
            int i = 0;
            t.children = new mTREE[actions.Count];
            foreach (int a in actions)
            {
                int row = a / 3;
                int col = a % 3;
                t.children[i] = new mTREE();
                t.children[i].state = new int[3][];
                map_copy_state(t.state, t.children[i].state); // create a copy of the current state
                t.children[i].state[row][col] = 1; // make the simulated move
                int v = max_value(ref t.children[i]); // the utility
                if (v < min_v)
                {
                    min_v = v;
                    t.utility = min_v;
                    t.loc = a; // the move itself, the position to play "X"
                }
            }

            i++;

            return min_v;
        }

        int key;
        bool diagonal(int[][] _board)
        {
            if (_board[0][2] == _board[1][1] && _board[1][1] == _board[2][0] && _board[0][2] != 0)
            {
                key = _board[0][2];
                return true;
            }
            if (_board[0][0] == _board[1][1] && _board[1][1] == _board[2][2] && _board[0][0] != 0)
            {
                key = _board[0][0];
                return true;
            }
            return false;
        }

        bool horizontal(int[][] _board)
        {
            if (_board[0][0] == _board[0][1] && _board[0][1] == _board[0][2] && _board[0][0] != 0)
            {
                key = _board[0][0];
                return true;
            }
            if (_board[1][0] == _board[1][1] && _board[1][1] == _board[1][2] && _board[1][0] != 0)
            {
                key = _board[1][0];
                return true;
            }
            if (_board[2][0] == _board[2][1] && _board[2][1] == _board[2][2] && _board[2][0] != 0)
            {
                key = _board[2][0];
                return true;
            }
            return false;
        }

        bool vertical(int[][] _board)
        {
            if (_board[0][0] == _board[1][0] && _board[1][0] == _board[2][0] && _board[0][0] != 0)
            {
                key = _board[0][0];
                return true;
            }
            if (_board[0][1] == _board[1][1] && _board[1][1] == _board[2][1] && _board[0][1] != 0)
            {
                key = _board[0][1];
                return true;
            }
            if (_board[0][2] == _board[1][2] && _board[1][2] == _board[2][2] && _board[0][2] != 0)
            {
                key = _board[0][2];
                return true;
            }
            return false;
        }

        int compute_utility(int[][] state)
        {
            if (vertical(state))
            {
                if (key == 1)
                    return -1;
                else
                    return 1;
            }

            if (horizontal(state))
            {
                if (key == 1)
                    return -1;
                else
                    return 1;
            }

            if (diagonal(state))
            {
                if (key == 1)
                    return -1;
                else
                    return 1;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state[i][j] == 0)
                        return 2;
                }
            }

            return 0; // simulation ends in a draw
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!button3.Enabled)
                button3.Enabled = true;

            Random rnd = new Random();
            int value_to_play = rnd.Next(1, 3);
            //int value_to_play = 2; test
            foreach (var control in this.Controls)
            {
                if (control is Label)
                {
                    Label l = (Label)control;
                    l.Text = "";
                }
            }

            locations = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            //locations = new List<int>() { 0, 6, 7 }; test
            comboBox1.Items.Clear();
            foreach (int i in locations)
                comboBox1.Items.Add(i + 1);
            fillArray();

            // testing a pre-determined state
            //label2.Text = "X"; board[0][1] = 1;
            //label3.Text = "O"; board[0][2] = 2;
            //label4.Text = "X"; board[1][0] = 1;
            //label5.Text = "O"; board[1][1] = 2;
            //label6.Text = "O"; board[1][2] = 2;
            //label9.Text = "X"; board[2][2] = 1;
            // end of test

            if (value_to_play == 1)
                MessageBox.Show("It's your turn to play");
            else
                make_move(); // machine makes move
        }
    }
}
