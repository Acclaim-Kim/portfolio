# search.py
# ---------
# Licensing Information: Please do not distribute or publish solutions to this
# project. You are free to use and extend these projects for educational
# purposes. The Pacman AI projects were developed at UC Berkeley, primarily by
# John DeNero (denero@cs.berkeley.edu) and Dan Klein (klein@cs.berkeley.edu).
# For more info, see http://inst.eecs.berkeley.edu/~cs188/sp09/pacman.html

"""
In search.py, you will implement generic search algorithms which are called
by Pacman agents (in searchAgents.py).
"""
import util
from util import heappush, heappop
class SearchProblem:
    """
    This class outlines the structure of a search problem, but doesn't implement
    any of the methods (in object-oriented terminology: an abstract class).

    You do not need to change anything in this class, ever.
    """

    def getStartState(self):
      """
      Returns the start state for the search problem
      """
      util.raiseNotDefined()

    def isGoalState(self, state):
      """
      state: Search state

      Returns True if and only if the state is a valid goal state
      """
      util.raiseNotDefined()

    def getSuccessors(self, state):
      """
      state: Search state

      For a given state, this should return a list of triples,
      (successor, action, stepCost), where 'successor' is a
      successor to the current state, 'action' is the action
      required to get there, and 'stepCost' is the incremental
      cost of expanding to that successor
      """
      util.raiseNotDefined()

    def getCostOfActions(self, actions):
      """
      actions: A list of actions to take

      This method returns the total cost of a particular sequence of actions.  The sequence must
      be composed of legal moves
      """
      util.raiseNotDefined()


def tinyMazeSearch(problem):
    """
    Returns a sequence of moves that solves tinyMaze.  For any other
    maze, the sequence of moves will be incorrect, so only use this for tinyMaze
    """
    from game import Directions
    s = Directions.SOUTH
    w = Directions.WEST
    return  [s,s,w,s,w,w,s,w]

def depthFirstSearch(problem):
    """
    Search the deepest nodes in the search tree first.
    Your search algorithm needs to return a list of actions that reaches
    the goal. Make sure that you implement the graph search version of DFS,
    which avoids expanding any already visited states. 
    Otherwise your implementation may run infinitely!
    To get started, you might want to try some of these simple commands to
    understand the search problem that is being passed in:
    print("Start:", problem.getStartState())
    print("Is the start a goal?", problem.isGoalState(problem.getStartState()))
    print("Start's successors:", problem.getSuccessors(problem.getStartState()))
    """
    """
    YOUR CODE HERE
    """
    #print("Start:", problem.getStartState())
    #print("Is the start a goal?", problem.isGoalState(problem.getStartState()))
    #print("Start's successors:", problem.getSuccessors(problem.getStartState()))

    from util import Stack

    stack = Stack() #I choose to use Stack to implement depthFirstSearch
    visited = set()
    start_state = problem.getStartState()

    stack.push((start_state, []))  # (current_state, path_taken)

    while not stack.isEmpty():
        state, path = stack.pop() #pops the most recently added node (deepest in the stack)

        if problem.isGoalState(state):
            return path  # Return solution when goal is reached

        if state not in visited:
            visited.add(state) #add to the visited to prevent overlapped state

            for successor, action, _ in problem.getSuccessors(state): #we do not need cost for depthFirstSerach
                if successor not in visited:
                    stack.push((successor, path + [action]))

    return []

    

def breadthFirstSearch(problem):
    """
    YOUR CODE HERE
    """
    from util import Queue

    queue = Queue() #I choose to use Queue to implement breathFirstSearch
    visited = set()
    start_state = problem.getStartState()

    queue.push((start_state, []))  # (current_state, path_taken)

    while not queue.isEmpty():
        state, path = queue.pop()

        if problem.isGoalState(state):
            return path  # Return solution when goal is reached

        if state not in visited:
            visited.add(state)

            for successor, action, _ in problem.getSuccessors(state):
                if successor not in visited:
                    queue.push((successor, path + [action]))

    return []


def uniformCostSearch(problem):
    """
    YOUR CODE HERE
    """
    from util import PriorityQueue

    pq = PriorityQueue() #I choose to use prioriotyQueue to implement uniformCostSearch
    visited = set()
    start_state = problem.getStartState()

    pq.push((start_state, [], 0), 0)  # (current_state, path_taken, cost), priority = cost

    while not pq.isEmpty():
        state, path, cost = pq.pop()

        if problem.isGoalState(state):
            return path  # Return solution when goal is reached

        if state not in visited:
            visited.add(state)

            for successor, action, step_cost in problem.getSuccessors(state): #cost matters for uniformCostSearch because elements are arranged depensd on the priority
                if successor not in visited:
                    new_cost = cost + step_cost #update the cost
                    pq.push((successor, path + [action], new_cost), new_cost)

    return []


def nullHeuristic(state, problem=None):
    """
    A heuristic function estimates the cost from the current state to the nearest
    goal in the provided SearchProblem.  This heuristic is trivial.
    """
    return 0

def aStarSearch(problem, heuristic=nullHeuristic):
    """
    YOUR CODE HERE
    """
    from util import PriorityQueue

    pq = PriorityQueue() #I choose to use PriorityQueue for starSerach because elements are arranged depends on the priority.
    visited = set()
    start_state = problem.getStartState()

    pq.push((start_state, [], 0), heuristic(start_state, problem))  # f = g + h

    while not pq.isEmpty():
        state, path, cost = pq.pop()

        if problem.isGoalState(state):
            return path 

        if state not in visited:
            visited.add(state)

            for successor, action, step_cost in problem.getSuccessors(state):
                if successor not in visited:
                    g = cost + step_cost
                    h = heuristic(successor, problem)
                    pq.push((successor, path + [action], g), g + h) #Now consider forward and heuristic cost as well.

    return []



# Abbreviations
bfs = breadthFirstSearch
dfs = depthFirstSearch
astar = aStarSearch
ucs = uniformCostSearch
