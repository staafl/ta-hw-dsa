using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 2 Write a recursive program for generating and printing all 
       the combinations with duplicates of k elements from n-element set. 
       Example:
       n=3, k=2  (1 1), (1 2), (1 3), (2 2), (2 3), (3 3)
    * */
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        
        Recurse(n, k, 0, new Stack<int>());
    }
    
    static void Recurse(int n, int k, int depth, Stack<int> stack)
    {
        // we can use stack.Count instead of passing depth as an argument, 
        // but I prefer to keep it explicit
        
        if (depth == k) 
        {
            Console.Write("(");
            Console.Write(string.Join(" ", stack.Reverse()));
            Console.WriteLine(")");
        }
        else
        {
            int startFrom = stack.Count == 0 ? 1 : stack.Peek();
            for (int ii = startFrom; ii <= n; ++ii) 
            {
                stack.Push(ii);
                Recurse(n, k, depth + 1, stack);
                stack.Pop();
            }
        }
        
    }
}