using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
/* 1 Write a recursive program that simulates the execution of n nested loops from 1 to n. Examples:
             
        1 1         
n=2 ->  1 2 
        2 1         
        2 2         
             
      1 1 1
      1 1 2
      1 1 3
      1 2 1
n=3 ->  ...
      3 2 3
      3 3 1
      3 3 2
      3 3 3
* */
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        Recurse(0, n, new Stack<int>());
    }
    
    static void Recurse(int depth, int maxDepth, Stack<int> counters) 
    {
        for (int ii = 1; ii <= maxDepth; ++ii) 
        {
            if (depth == maxDepth) 
            {
                // the reversed stack is a very useful technique
                foreach (var cnt in counters.Reverse())
                    Console.Write("{0} ", cnt);
                Console.WriteLine();
            }
            else
            {
                counters.Push(ii);
                Recurse(depth + 1, maxDepth, counters);
                counters.Pop();
            }
        }
    }
}


















