using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 11 * Write a program to generate all permutations with repetitions 
       of given multi-set. For example the multi-set {1, 3, 5, 5} has the 
       following 12 unique permutations:
        { 1, 3, 5, 5 }	{ 1, 5, 3, 5 }
        { 1, 5, 5, 3 }	{ 3, 1, 5, 5 }
        { 3, 5, 1, 5 }	{ 3, 5, 5, 1 }
        { 5, 1, 3, 5 }	{ 5, 1, 5, 3 }
        { 5, 3, 1, 5 }	{ 5, 3, 5, 1 }
        { 5, 5, 1, 3 }	{ 5, 5, 3, 1 }
        Ensure your program efficiently avoids duplicated permutations. 
        Test it with { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }.
        Hint: http://hardprogrammer.blogspot.com/2006/11/permutaciones-con-repeticin.html
    * */
    
    const bool HARD_TEST = true;
    static void Main(string[] args)
    {
        string[] elements;
        if (HARD_TEST)
            elements = new []{ "1", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", 
                               "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", "5", 
                               "5", "5", "5", "5", "5", "5", "5", "5", "5" };
                               
        else
            elements = Console.ReadLine().Split(' ');
        
        var indexedElements = from ix in Enumerable.Range(0, elements.Length)
                              select Tuple.Create(ix, elements[ix]);
                              
        Recurse(indexedElements, new HashSet<int>(), new Stack<string>());
    }

    static void Recurse(IEnumerable<Tuple<int, string>> indexedElements, 
                        HashSet<int> seenEver, Stack<string> stack)
    {
        var seenNow = new HashSet<string>();
        
        if (stack.Count == indexedElements.Count())
        {
            Console.WriteLine("(" + string.Join(" ", stack.Reverse()) + ")");
            return;
        }
        
        foreach (var tuple in indexedElements)
        {
            if (seenEver.Contains(tuple.Item1))
                continue;
                
            if (seenNow.Contains(tuple.Item2))
                continue;
                
            seenEver.Add(tuple.Item1);
            seenNow.Add(tuple.Item2);
            stack.Push(tuple.Item2);
            
            Recurse(indexedElements, seenEver, stack);
            
            stack.Pop();
            seenEver.Remove(tuple.Item1);
            
        }
    }
}










