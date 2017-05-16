using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC480_HW1_EightPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {

            ///////////    Uniform Cost //////////////////////

            int[] PuzzleArray = new int[] { 0, 2, 3, 1, 8, 4, 7, 6, 5 };
            int[] Easy = new int[] { 1, 3, 4, 8, 6, 2, 7, 0, 5 };
            int[] Medium = new int[] { 2, 8, 1, 0, 4, 3, 7, 6, 5 };
            int[] Hard = new int[] { 5, 6, 7, 4, 0, 8, 3, 2, 1 };
            int[] GoalState = new int[] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };
            StateNode GoalStateNode = new StateNode();
            int Time = 0;
            int TotalCostOfAllMoves;
            //int CostFromStart;
            
            int SizeOfSortedList = 0;
            int MaxOfSortedList = 0;

            int SizeOfPQ = 0;
            int MaxOfPQ = 0;

            PuzzleArray = Hard ;
            
            foreach (var item in PuzzleArray)
            {
                Console.Write(item.ToString() + " ");

            }
            
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in PuzzleArray)
            {
                Console.Write(item.ToString() + " ");

            }
            Console.WriteLine();
            Console.WriteLine("State array");
            Console.WriteLine();

            StateNode node = new StateNode();
            StateNode root = new StateNode(PuzzleArray);

            //foreach (var item in node.StateArray)
            //{
                
            //    Console.Write(item.ToString() + " ");

            //}

            Console.WriteLine();
            Console.WriteLine("State array Passing PuzzleArray");
            Console.WriteLine();

            foreach (var item in root.StateArray2)
            {

                Console.Write(item.ToString() + " ");

            }

            //// Create Queue of Nodes
            //Queue<StateNode> q = new Queue<StateNode>();

            // Create Queue that will accept queue returned from Successor function
            Queue<StateNode> qFromSuccessorFunction = new Queue<StateNode>();

            //// Put original puzzle state in queue
            //q.Enqueue(root);
            //SizeOfQueue = 1;
            //MaxOfQueue = MaxOfQueue + 1;


            // Depth First Search - Create a stack
           // Stack<StateNode> stack = new Stack<StateNode>();

            // Uniform Cost Search - Need to make a Sorted List
            // Sorted based on the smallest cost from the start node of tree
            // Each move made the cost is the value of tile moved
            SortedList<int, StateNode> sortedListBasedOnCostOfMovesFromStart = new SortedList<int, StateNode>();

            
            PriorityQueue<StateNode> pq = new PriorityQueue<StateNode>();

            //// Put original puzzle state in Sorted List
            //sortedListBasedOnCostOfMovesFromStart.Add(root.CostOfMovesFromStart, root);
            //SizeOfSortedList = SizeOfSortedList + 1;
            //MaxOfSortedList = MaxOfSortedList + 1;

            pq.Add(root.CostOfMovesFromStart, root);
            SizeOfPQ = SizeOfPQ + 1;
            MaxOfPQ = MaxOfPQ + 1;

            //// Put original puzzle state on stack
            //stack.Push(root);
            //SizeOfStack = SizeOfStack + 1;
            //MaxOfStack = MaxOfStack + 1;

            // Create two Dictionaries to look up previous states for state checking. Need to check if on the stack or if it was 
            // previously looked at.
            Dictionary<int, int> dictOfNodesInPQ = new Dictionary<int, int>();

            Dictionary<int, int> dictOfStateArraysSeenBefore = new Dictionary<int, int>();

            dictOfNodesInPQ.Add(root.ArrayStateInt, root.ArrayStateInt);




            // Beginning of General Search Loop
            bool keepRunning = true;
            while (keepRunning)
            {
                if (pq.Count == 0)
                {
                    Console.WriteLine("sortedList empty");
                    keepRunning = false;
                }
                if (keepRunning == false)
                {
                    break;
                }

                
                //Remove Nodes from from of the sortedList
                StateNode removeFromPQ = new StateNode();
                //Assigns Node to first element of list
                removeFromPQ = pq.RemoveMin();
                //Then need to remove the first element(<key, value> pair) of sorted list
                

                Time = Time + 1;

                //// Remove nodes from top of stack for Depth-First Search
                //StateNode removeFromTopOfStack = new StateNode();
                //removeFromTopOfStack = stack.Pop();
                //Time = Time + 1;

                // Remove node from Dictionary with list of nodes currently on stack
                dictOfNodesInPQ.Remove(removeFromPQ.ArrayStateInt);
                Console.WriteLine();
                Console.WriteLine("removing this node StateArrayInt: " + removeFromPQ.ArrayStateInt);
                Console.WriteLine();

                // add this node to dictionary of nodes we have seen before if we haven't already seen it
                if (dictOfStateArraysSeenBefore.ContainsKey(removeFromPQ.ArrayStateInt))
                {
                    //do nothing
                }
                else
                {
                    dictOfStateArraysSeenBefore.Add(removeFromPQ.ArrayStateInt, removeFromPQ.ArrayStateInt);
                }



                
                // check if node ArrayState is Equal to the GoalState to Solve PuzzleEight
                bool isEqual = Enumerable.SequenceEqual(removeFromPQ.StateArray2, GoalState);
                if (isEqual)
                {
                    Console.WriteLine();
                    Console.WriteLine("Puzzle is Solved!");
                    GoalStateNode = removeFromPQ;
                    Console.WriteLine();
                    break;
                }
                

                // create a queue to accept the queue returned from successor function

                
                qFromSuccessorFunction = StateNode.Successor(removeFromPQ);
                
                

                

                //// add nodes from Successor Function into the q for the loop

                while (qFromSuccessorFunction.Count != 0)
                {
                    StateNode removeFromFrontOfSuccessorQueue = new StateNode();
                    removeFromFrontOfSuccessorQueue = qFromSuccessorFunction.Dequeue();
                    
                    

                   

                    //If we have already seen this state before or this state is on the stack currently, do nothing. Otherwise put state in both dictionaries
                    if(dictOfNodesInPQ.ContainsKey(removeFromFrontOfSuccessorQueue.ArrayStateInt) || (dictOfStateArraysSeenBefore.ContainsKey(removeFromFrontOfSuccessorQueue.ArrayStateInt)))
                    {
                        // do nothing
                    }
                    else
                    {
                        pq.Add(removeFromFrontOfSuccessorQueue.CostOfMovesFromStart, removeFromFrontOfSuccessorQueue);
                       // sortedListBasedOnCostOfMovesFromStart.Add(removeFromFrontOfSuccessorQueue.CostOfMovesFromStart, removeFromFrontOfSuccessorQueue);
                        dictOfNodesInPQ.Add(removeFromFrontOfSuccessorQueue.ArrayStateInt, removeFromFrontOfSuccessorQueue.ArrayStateInt);
                        dictOfStateArraysSeenBefore.Add(removeFromFrontOfSuccessorQueue.ArrayStateInt, removeFromFrontOfSuccessorQueue.ArrayStateInt);
                    }

                    SizeOfPQ = pq.Count;
                    //SizeOfSortedList = sortedListBasedOnCostOfMovesFromStart.Count();
                    //if (MaxOfSortedList < SizeOfSortedList)
                    //{
                    //    MaxOfSortedList = SizeOfSortedList;
                    //}
                    if (MaxOfPQ < SizeOfPQ)
                    {
                        MaxOfPQ = SizeOfPQ;
                    }
                    
                }


            }
            Console.WriteLine();
            Console.WriteLine("out of loop");
            Console.WriteLine();


            Console.WriteLine();
            Console.WriteLine("Here were the successful moves to solve the puzzle:");
            Console.WriteLine();

            StateNode.PrettyPrintPathToSolvePuzzle(GoalStateNode);

            Console.WriteLine();
            Console.WriteLine("Time = " + Time);
            Console.WriteLine();
            Console.WriteLine("MaxOfPQ " + MaxOfPQ);
            Console.WriteLine();


            Console.ReadLine();
        }

        public static void SwapNums(int[] Array, int position1, int position2)
        {
            int temp = Array[position1];
            Array[position1] = Array[position2];
            Array[position2] = temp;
        }

        
    }
}
