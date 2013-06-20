using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 4 Write a recursive program for generating and printing all permutations 
       of the numbers 1, 2, ..., n for given integer number n. 
       Example:
       n=3  {1, 2, 3}, {1, 3, 2}, {2, 1, 3}, {2, 3, 1},{3, 1, 2},{3, 2, 1}
    * */
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        
        // see also http://en.wikipedia.org/wiki/Permutation#Algorithms_to_generate_permutations 
        // for non-recursive approaches
        Recurse(n, 0, new HashSet<int>());
    }
    
    static void Recurse(int n, int depth, HashSet<int> set)
    {
        if (depth == n) 
        {
            Console.Write("{");
            Console.Write(string.Join(" ", set.Reverse()));
            Console.WriteLine("}");
        }
        else
        {
            for (int ii = 1; ii <= n; ++ii) 
            {
                if (set.Contains(ii))
                    continue;
                set.Add(ii);
                Recurse(n, depth + 1, set);
                set.Remove(ii);
            }
        }
        
    }
}