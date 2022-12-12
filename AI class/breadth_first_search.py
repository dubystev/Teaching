"""
Breadth First Search (Uninformed Search) implementation on the Romania graph in the AI textbook.
This is based on the AI Textbook (Russell & Norvig) 4th Edition, Chapter 3.
SOLVING PROBLEMS BY SEARCHING.
Author: Stephen A. Adubi
08/12/2022 @ 19:00
"""

from queue import Node
from queue import FIFOQueue
global graph, goal_state, initial_state
global frontier


def main():
    global graph, initial_state, goal_state
    initial_state = "Arad"  # to try the algorithm on a new scenario, change the initial state
    goal_state = "Bucharest"  # to try the algorithm on a new scenario, change the goal state
    graph = {"Oradea" : ["Zerind", "Sibiu"], "Zerind" : ["Oradea", "Arad"], "Arad" : ["Zerind", "Sibiu", "Timisoara"],
            "Sibiu" : ["Oradea", "Arad", "Fagaras", "Rimnicu Vilcea"], "Timisoara" : ["Arad", "Lugoj"], "Lugoj" :
            ["Timisoara", "Mehadia"], "Fagaras" : ["Sibiu", "Bucharest"], "Rimnicu Vilcea" : ["Sibiu", "Craiova",
            "Pitesti"], "Mehadia" : ["Lugoj", "Drobeta"], "Drobeta" : ["Mehadia", "Craiova"], "Craiova" : ["Drobeta",
            "Rimnicu Vilcea", "Pitesti"], "Pitesti" : ["Rimnicu Vilcea", "Bucharest", "Craiova"], "Bucharest" :
            ["Pitesti", "Fagaras", "Giurgiu", "Urziceni"], "Giurgiu" : ["Bucharest"], "Urziceni" : ["Bucharest",
            "Vaslui", "Hirsova"], "Hirsova" : ["Eforie", "Urziceni"], "Vaslui" : ["Iasi", "Urziceni"], "Eforie" :
            ["Hirsova"], "Iasi" : ["Neamt", "Vaslui"], "Neamt" : ["Iasi"]}
    return bfs()


def bfs():
    global goal_state, initial_state, frontier
    node = Node(initial_state, initial_state)
    if node.state == goal_state:
        return node
    frontier = FIFOQueue()
    frontier.push(node)
    reached = [initial_state]  # create a list of reached states

    while not frontier.isEmpty():
        node = frontier.pop()
        for child in expand(node):
            s = child.state
            if s == goal_state:
                return child
            if s not in reached:
                reached.append(s)
                frontier.push(child)
    return -1


def expand(node):
    s = node.state
    children = list(graph[s])  # get the list of children (neighbours) of the current city where the agent is
    for i in range(0, len(children)):
        s_prime = children[i]
        node_temp = Node(s_prime, node.path + " -> " + s_prime)
        yield node_temp


if __name__ == "__main__":
    value = main()
    if value == -1:
        print('Oops, search terminated with a failure')
    else:
        print('Path returned by Breadth First Search can be seen below')
        print('{0}'.format(value.path))
