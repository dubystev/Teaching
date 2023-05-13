// Shift_Reduce_Parser.cpp : This file contains the 'main' function. Program execution begins and ends there.
// This program implements a Shift-Reduce Parser, a kind of bottom-up parser
// Input: an LR parsing table (a csv file) of a particular grammar
// Output: A message indicating a successful parse or failure
// Reference: Chapter 4 of the textbook: "Concepts of Programming Languages by Robert W. Sebesta"

#include <vector>
#include <string>
#include <sstream>
// Exclusive to the main program
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <map>
#include <utility>
using namespace std;

vector<string> STACK;
vector<vector<string>> rules;
string input;
map<pair<int, string>, string> action;
map<pair<int, string>, int> Goto;

/**
* splits a string into word/tokens based on a delimiter
* input := input string
* delim := the delimiter
*/
vector<string> split(string input, char delim) {
    stringstream s(input);
    vector<string> temp;
    string word;
    while (getline(s, word, delim))
        temp.push_back(word);
    return temp;
}

string extract() {
    string val = "";
    int i = 0;
    while (true) {
        char ch = input[i];
        if (ch == '(' || ch == ')' || ch == '+' || ch == '*') {
            if (val != "")
                break;
            val += ch;
            break;
        }
        if (isalpha(ch) || isdigit(ch))
            val += ch;
        else
            break;
        i++;
    }
    return val;
}

void erase(string substr) {
    int len = substr.length();
    if (input[len] == ' ')
        input.erase(0, len + 1);
    else
        input.erase(0, len);
}

string getAction() { // confirmed
    string top = STACK[0];
    int pair1 = stoi(top);
    string pair2 = extract();
    if (pair2 == "")
        pair2 = "$";
    pair<int, string> temp;
    temp.first = pair1;
    temp.second = pair2;
    if (action.find(temp) == action.end()) { // if map key does not exist
        return "reject";
    }
    else {
        string the_action = action[temp];
        return the_action;
    }
}

void printSTACK() {
    for (int i = STACK.size() - 1; i >= 0; i--)
        cout << STACK[i];
}

void shift(string state_to_push) {
    string val = extract();
    STACK.insert(STACK.begin(), val);
    STACK.insert(STACK.begin(), state_to_push);
    erase(val);
}

bool isnum(string val) {
    for (char c : val) {
        if (isdigit(c))
            continue;
        else
            return false;
    }

    return true;
}

void reduce(int rule_no) {
    string sequence = rules[rule_no - 1][2]; // get the right hand side of the rule
    string lhs = rules[rule_no - 1][0]; // left-hand side of the rule
    string str_builder = "";
    while (str_builder != sequence) {
        string item = STACK[0];
        STACK.erase(STACK.begin());
        if (isnum(item))
            continue;
        str_builder = item + str_builder;
    }
    cout << "Intermediate stack entry after reduction of rule " << rule_no << ": ";
    printSTACK();
    cout<<", GOTO([" << STACK[0] << ", " << lhs << "])";
    cout << endl;

    int pair1 = stoi(STACK[0]);
    pair<int, string> temp;
    temp.first = pair1; temp.second = lhs;
    if (Goto.find(temp) != Goto.end()) { // if map key exists
        int entry = Goto[temp];
        string str_entry = "";
        // convert integer to string
        while (entry != 0) {
            int rem = entry % 10;
            str_entry = (char)(rem + '0') + str_entry;
            entry /= 10;
        }
        STACK.insert(STACK.begin(), lhs);
        STACK.insert(STACK.begin(), str_entry);
    }
}

int main()
{
    string expr = "id + id * id"; // change the value of the string to test for another expression
    cout << "***A Shift Reduce Parser in C++***\n";

    //read in the grammar rules
    fstream fin;
    string line;
    vector<string> temp;
    fin.open("Rules.txt", ios::in);
    while (getline(fin, line)) {
        temp = split(line, ' ');
        rules.push_back(temp);
    }
    fin.close();

    //read information from the parsing table
    fin.open("Parsing Table.csv", ios::in);
    int i = 0; // an iterator that keeps track of the number of lines in the parsing table file
    int index_of_end_tag = -1; // stores the index of the '$' symbol in the parsing table
    vector<string> row; // to store a row in the csv file
    vector<string> tns; // terminals and non-terminals of the grammar
    while (fin >> line) {
        row = split(line, ',');
        if (i == 0) // register the terminals and non-terminals
        { 
            int iter = 0;
            for (string item : row) {
                if(iter != 0)
                    tns.push_back(item);
                if (item == "$")
                    index_of_end_tag = iter;
                iter++;
            }
        }
        else 
        {
            int iter = 0;
            int state;
            bool _action = true;
            for (string item : row) {
                if (iter == 0) {
                    state = stoi(item); // first component of the pair structure
                    iter++;
                    continue;
                }

                string comp2 = tns[iter - 1]; // second component of the pair structure
                pair<int, string> temp;
                temp.first = state;
                temp.second = comp2;

                if (_action && item != "<blank>") {
                    action[temp] = item;
                }
                else if(item != "<blank>") {
                    int goto_state = stoi(item);
                    Goto[temp] = goto_state;
                }


                if (iter == index_of_end_tag) // signifies the moment where subsequent items are stored in the Goto map.
                    _action = false;
                iter++;
            }
        }
        i++;
    }

    fin.close();
    cout << "There are " << (i - 1) << " states in the parsing table\n";

    STACK.push_back("0");
    input = expr + '$';
    cout << "STACK: ";
    printSTACK();
    cout << "\tinput: " << input << endl;
    string str_action = getAction();
    string msg;
    msg = (str_action[0] == 'S') ? "Shift " : "Reduce via rule ";
    msg += str_action[1];
    if (isdigit(str_action[1]))
        cout << "action: " << msg << endl;
    while (str_action != "accept" && str_action != "reject") {
        if (str_action[0] == 'S') { // to shift
            string arg = "";
            arg += str_action[1];
            shift(arg);
            cout << "STACK: ";
            printSTACK();
            cout << "\tinput: " << input << endl;
        }
        else { // to reduce
            string arg = "";
            arg += str_action[1];
            reduce(stoi(arg));
            cout << "STACK: ";
            printSTACK();
            cout << "\tinput: " << input << endl;
        }

        str_action = getAction();
        msg = (str_action[0] == 'S') ? "Shift " : "Reduce via rule ";
        msg += str_action[1];
        if(isdigit(str_action[1]))
            cout << "action: " << msg<<endl;
    }

    cout << "Decision on string: " << str_action;
}
