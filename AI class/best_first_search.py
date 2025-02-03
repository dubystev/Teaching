"""
Best First Search (Uninformed Search) implementation on the Romania graph in the AI textbook.
This is based on the AI Textbook (Russell & Norvig) 4th Edition, Chapter 3.
SOLVING PROBLEMS BY SEARCHING.
Author: Stephen A. Adubi
08/12/2022 @ 18:05
"""

from priority_queue import PRQNode
from priority_queue import PriorityQueue

global graph, goal_state, initial_state
global frontier


def main():
    global graph, initial_state, goal_state
    initial_state = "Arad"  # to try the algorithm on a new scenario, change the initial state
    goal_state = "Bucharest"  # to try the algorithm on a new scenario, change the goal state
    graph = {"Oradea" : [("Zerind",71), ("Sibiu",151)], "Zerind" : [("Oradea",71), ("Arad",75)], "Arad" : [("Zerind",75),
            ("Sibiu",140), ("Timisoara",118)], "Sibiu" : [("Oradea",151), ("Arad",140), ("Fagaras",99),
            ("Rimnicu Vilcea",80)], "Timisoara" : [("Arad",118), ("Lugoj",111)], "Lugoj" : [("Timisoara",111),
            ("Mehadia",70)], "Fagaras" : [("Sibiu",99), ("Bucharest",211)], "Rimnicu Vilcea" : [("Sibiu",80),
            ("Craiova",146), ("Pitesti",97)], "Mehadia" : [("Lugoj",70), ("Drobeta",75)], "Drobeta" : [("Mehadia",75),
            ("Craiova",120)], "Craiova" : [("Drobeta",120), ("Rimnicu Vilcea",146), ("Pitesti",138)], "Pitesti" :
            [("Rimnicu Vilcea",97), ("Bucharest",101), ("Craiova",138)], "Bucharest" : [("Pitesti",101), ("Fagaras",211),
            ("Giurgiu",90), ("Urziceni",85)], "Giurgiu" : [("Bucharest",90)], "Urziceni" : [("Bucharest",85),
            ("Vaslui",142), ("Hirsova",98)], "Hirsova" : [("Eforie",86), ("Urziceni",98)], "Vaslui" : [("Iasi",92),
            ("Urziceni",142)], "Eforie" : [("Hirsova",86)], "Iasi" : [("Neamt",87), ("Vaslui",92)], "Neamt" : [("Iasi",87)]}
    return best_first_search()


def best_first_search():
    global goal_state, initial_state, frontier
    node = PRQNode(initial_state, initial_state)
    frontier = PriorityQueue()
    frontier.push(node)
    reached = {initial_state: node}  # create a dictionary using the initial state of the problem

    while not frontier.isEmpty():
        printF(frontier)
        node = frontier.pop()
        if node.state == goal_state:
            return node
        for child in expand(node):
            s = child.state
            if s not in reached or child.priority < reached[s].priority:
                reached[s] = child
                frontier.push(child)
        print()
    return -1


def printF(frontier):
    item = frontier.head
    while(item is not None):
        print(f'{item.state}: {item.path} ({item.priority})')
        item = item.next
    

def expand(node):
    s = node.state
    children = list(graph[s])  # get the list of children (neighbours) of the current city where the agent is
    for i in range(0, len(children)):
        s_prime = children[i]
        cost = node.priority + s_prime[1]
        node_temp = PRQNode(s_prime[0], node.path + " -> " + s_prime[0], cost)
        yield node_temp


if __name__ == "__main__":
    value = main()
    if value == -1:
        print('Oops, search terminated with a failure')
    else:
        print('Path returned by best-first-search can be seen below')
        print('{0} with a cost of {1} units'.format(value.path, value.priority))
