using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 3 Modify the previous program to skip duplicates:
       n=4, k=2  (1 2), (1 3), (1 4), (2 3), (2 4), (3 4)
    * */
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        
        Recurse(n, k, 0, new Stack<int>());
    }
    
    static void Recurse(int n, int k, int depth, Stack<int> stack)
    {
        if (depth == k) 
        {
            Console.Write("(");
            Console.Write(string.Join(" ", stack.Reverse()));
            Console.WriteLine(")");
        }
        else
        {
            int startFrom = stack.Count == 0 ? 1 : stack.Peek() + 1;
            for (int ii = startFrom; ii <= n; ++ii) 
            {
                stack.Push(ii);
                Recurse(n, k, depth + 1, stack);
                stack.Pop();
            }
        }
        
    }
}