using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 5 Write a recursive program for generating and printing all ordered 
       k-element substacks from n-element set (variations Vkn).
       Example: n=3, k=2, stack = {hi, a, b} =>
       (hi hi), (hi a), (hi b), (a hi), (a a), (a b), (b hi), (b a), (b b)
    * */
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        string[] elements = Console.ReadLine().Split(' ');
        
        Recurse(n, k, 0, elements, new Stack<string>());
    }
    
    static void Recurse(int n, int k, int depth, string[] elements, Stack<string> stack)
    {
        if (depth == k) 
        {
            Console.Write("{");
            Console.Write(string.Join(" ", stack.Reverse()));
            Console.WriteLine("}");
        }
        else
        {
            foreach (var elem in elements)
            {
                stack.Push(elem);
                Recurse(n, k, depth + 1, elements, stack);
                stack.Pop();
            }
        }
        
    }
}