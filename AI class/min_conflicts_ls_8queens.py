"""
Minimum Conflicts (Local Search) implementation for solving the 8-queens problem.

It is a hill climbing-like implementation of the heuristic, since it picks the
next best action (out of several actions) @ each step.

This is based on the AI Textbook (Russell & Norvig) 4th Edition, Chapter 6.
CONSTRAINTS SATISFACTION PROBLEMS.

Author: Stephen A. Adubi.
03/12/2022 @ 15:30
"""

from timeit import default_timer as timer
import random
import copy

'''
function MIN-CONFLICTS(csp,max steps) returns a solution or failure                 Line 1
  inputs: csp, a constraint satisfaction problem                                    Line 2
          max steps, the number of steps allowed before giving up                   Line 3
  current ← an initial complete assignment for csp                                  Line 4
  for i = 1 to max steps do                                                         Line 5
    if current is a solution for csp then return current                            Line 6
    var ← a randomly chosen conflicted variable from csp.VARIABLES                  Line 7
    value ← the value v for var that minimizes CONFLICTS(csp, var, v, current)      Line 8
    set var=value in current                                                        Line 9
return failure                                                                      Line 10
'''

global start_time
global timeElapsed  # using time-based stopping criterion rather than maximum steps stated in the Text
global execTime, elapsedTime
global csp  # definition of the csp
global current  # holds the partial (complete) solution to the csp
global conflicted_vars  # stores the variables whose values violate some constraints


def print_board(chess_board):
    for i in range(7, -1, -1):
        for j in range(8):
            val = chess_board[j]
            if i == val:
                print('Q', end=" ")
            else:
                print('_', end=" ")
        print()


def main():
    global current, start_time, elapsedTime, timeElapsed, csp, conflicted_vars
    sol_found = False
    csp = ([0, 1, 2, 3, 4, 5, 6, 7], [0, 1, 2, 3, 4, 5, 6, 7])  # item 1: variables, item 2: domain
    iter = 0  # tracks the number of iterations covered by the algorithm so far...
    '''
    current: a dictionary where key = column (a variable) and value = the row (csp value) where 
    queen in column is placed
    '''
    current = {}
    conflicted_vars = []  # a list of variables in a conflict in the current solution
    timeElapsed = False
    print("Minimum Conflict Local Search implementation for solving the 8-queens problem")

    # Setting up the chess board, the variable cur_sol is used to store the current chess board configuration
    # The following block implements Line 4, the solution will most likely not be consistent
    for i in range(0, 8):
        position = random.randint(0, 7)  # generate a real number in the range [0, 7]
        current[i] = position

    # print the initial configuration
    print('Initial configuration printed below...')
    print_board(current)
    print()

    start_time = timer()

    while not timeElapsed:  # implements Line 5
        if compute_value(current) == 0:  # implements Line 6
            print('The Minimum conflicts algorithm has found a solution to the 8-queens problem')
            sol_found = True  # declare success
            break
        iter += 1

        var = random.choice(conflicted_vars)  # implements line 7, get a random variable that is in a conflict
        domains = list(csp[1])
        cur_value = current[var]  # get the current value of the chosen variable

        min_conflict = 1000
        for i in domains:  # trying to re-assign var (chosen variable) to a value with minimum conflict, Line 8
            if i != cur_value:
                temp = copy.deepcopy(current)
                temp[var] = i
                value_of_move = compute_conflicts(temp, i)
                if value_of_move < min_conflict:
                    min_conflict = value_of_move
                    selected_value = i
                elif value_of_move == min_conflict and random.choice([0, 1]):
                    min_conflict = value_of_move
                    selected_value = i

        current[var] = selected_value  # line 9, actually make a move based on the best option at that time

        cur_time = timer()
        elapsedTime = cur_time - start_time
        if elapsedTime >= execTime:
            timeElapsed = True

    print('Done after {0} iterations. Solution printed below'.format(iter))
    if not sol_found: # implements line 10. If this condition is true, then no solution was found within the time limit
        print('The current solution is not optimal, algorithm had to be aborted')
    print_board(current)


def same_diagonal(chess_board, i, j):
    abs_diff_a = chess_board[i] - i
    abs_diff_b = chess_board[j] - j
    sum_a = i + chess_board[i]
    sum_b = j + chess_board[j]
    if abs_diff_a == abs_diff_b or sum_a == sum_b:
        return True
    else:
        return False


def compute_value(solution):  # compute the heuristic function value of the current solution, # attacking pairs
    global conflicted_vars
    h_value = 0  # if h_value = 0 is eventually returned, then the configuration is a solution to the 8-queens problem
    list(conflicted_vars).clear()
    for i in solution.keys():
        for j in range(i + 1, 8):
            if solution[i] == solution[j]:  # same row attack
                h_value += 1
                if i not in conflicted_vars: conflicted_vars.append(i)  # record the variable in conflict
                if j not in conflicted_vars: conflicted_vars.append(j)
            elif same_diagonal(solution, i, j):  # same diagonal attack
                h_value += 1
                if i not in conflicted_vars: conflicted_vars.append(i)
                if j not in conflicted_vars: conflicted_vars.append(j)
    return h_value


def compute_conflicts(solution, new):  # compute the heuristic function value of the current solution, # attacking pairs
    h_value = 0  # if h_value = 0 is eventually returned, then the configuration is a solution to the 8-queens problem
    for i in solution.keys():
        if solution[i] == solution[new]:  # same row attack
            h_value += 1
        elif same_diagonal(solution, i, new):  # same diagonal attack
            h_value += 1
    return h_value


if __name__ == "__main__":
    global execTime
    execTime = 120  # 120 secs = 2 mins, the algorithm will have to terminate after two minutes if no solution is found
    main()