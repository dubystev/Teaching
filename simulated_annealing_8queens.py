
from timeit import default_timer as timer
import math
import random

'''
Simulated Annealing implementation for solving the 8-queens problem
Many variants of the algorithm exist, but the implementation is done to make sure it is close to the AI Textbook 
(Russell & Norvig) version
'''

global n_impr_SA
global start_time
global timeElapsed
global execTime, elapsedTime
global avg_impr_SA
global cur_sol, next_sol
SA_temp = 0.2  # the temperature parameter (t); it is the schedule input and it determines the rate of acceptance of (worse) solutions
'''
Try to vary the value of SA_temp, don't try to go beyond the value 1.0 and also avoid setting a value of 0.0 and below
What do you observe if the value is very low, for example 0.01? Try to run the SA algorithm for at least 20 times with SA_temp = 0.01.
What do you observe from these runs? Compare your observation with another observation if you run the SA algorithm with
SA_temp value of 1.0. 
'''


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
    global cur_sol, next_sol, start_time, elapsedTime, timeElapsed
    iter = 0  # tracks the number of iterations covered by the algorithm so far...
    cur_sol = []
    timeElapsed = False
    print("Simulated Annealing implementation for solving the 8-queens problem")

    # Setting up the chess board, the variable cur_sol is used to store the current chess board configuration
    for i in range(0, 8):
        position = random.randint(0, 7)  # generate a real number in the range [0, 7]
        cur_sol.append(position)

    # print the initial configuration
    print('Initial configuration printed below...')
    print_board(cur_sol)
    print()

    start_time = timer()

    while not timeElapsed:
        next_sol = cur_sol.copy()  # make a copy of the current configuration
        # make a random move in the next solution
        position = random.randint(0, 7)  # randomly choose which column to make a move
        val = random.randint(0, 7)  # randomly choose the new row to move the queen in column (:= position) to
        while val == next_sol[position]:
            val = random.randint(0, 7)  # try not to select the same row where the queen is currently placed
        next_sol[position] = val  # make the move
        eval_cur = compute_value(cur_sol)
        eval_next = compute_value(next_sol)
        if sa_accept(eval_next, eval_cur):
            cur_sol = next_sol.copy()
            eval_cur = eval_next
        iter += 1
        if eval_cur == 0:
            print('The Simulated Annealing algorithm has found a solution to the 8-queens problem')
            break

        cur_time = timer()
        elapsedTime = cur_time - start_time
        if elapsedTime >= execTime:
            timeElapsed = True

    print('Done after {0} iterations. Solution printed below'.format(iter))
    if eval_cur > 0:
        print('The current solution is not optimal, algorithm had to be aborted')
    print_board(cur_sol)


def same_diagonal(chess_board, i, j):
    abs_diff_a = i - chess_board[i]
    abs_diff_b = j - chess_board[j]
    sum_a = i + chess_board[i]
    sum_b = j + chess_board[j]
    if abs_diff_a == abs_diff_b or sum_a == sum_b:
        return True
    else:
        return False


def compute_value(chess_board):  # compute the heuristic function value of the current solution, # attacking pairs
    h_value = 0  # if h_value = 0 is eventually returned, then the configuration is a solution to the 8-queens problem
    for i in range(0, 7):
        for j in range(i + 1, 8):
            if chess_board[i] == chess_board[j]:  # same row attack
                h_value += 1
            elif same_diagonal(chess_board, i, j):  # same diagonal attack
                h_value += 1
    return h_value


def sa_accept(eval, eval_0):
    global n_impr_SA, avg_impr_SA
    global execTime, elapsedTime
    impr = eval_0 - eval
    if impr > 0:
        n_impr_SA += 1
        avg_impr_SA += (impr - avg_impr_SA) / n_impr_SA

    arg1 = impr / (SA_temp * avg_impr_SA)

    cur_time = timer()
    elapsedTime = cur_time - start_time

    arg2 = execTime / (execTime - elapsedTime)
    arg = arg1 * arg2
    rnd = random.random()  # generate a real number in the range (0, 1)
    try:
        return rnd < math.exp(arg)  # if the random number is less than math.exp(arg), accept the next solution
    except:
        return True


if __name__ == "__main__":
    global n_impr_SA, avg_impr_SA, execTime, elapsedTime
    execTime = 120  # 120 secs = 2 mins, the algorithm will have to terminate after two minutes if no solution is found
    n_impr_SA = 1
    avg_impr_SA = 1
    main()
