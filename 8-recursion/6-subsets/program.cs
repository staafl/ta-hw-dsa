using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 6 Write a program for generating and printing all subsets of k strings 
       from given set of strings.
       Example: s = {test, rock, fun}, k=2
       (test rock),  (test fun),  (rock fun)
    * */
    static void Main(string[] args)
    {
        int k = int.Parse(Console.ReadLine());
        string[] elements = Console.ReadLine().Split(' ');

        Recurse(k, 0, elements, new Stack<string>());
    }

    static void Recurse(int k, int depth, string[] elements, Stack<string> stack)
    {
        if (stack.Count == k)
        {
            Console.Write("(");
            Console.Write(string.Join(" ", stack.Reverse()));
            Console.WriteLine(")");
        }
        else
        {
            int remaining = elements.Length - depth;

            if (stack.Count + remaining < k)
                return;

            // take element

            stack.Push(elements[depth]);
            Recurse(k, depth + 1, elements, stack);
            stack.Pop();

            // drop element

            if (stack.Count + remaining - 1 < k)
                return;

            Recurse(k, depth + 1, elements, stack);

        }

    }
}


