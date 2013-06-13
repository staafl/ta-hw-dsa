using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
/* 2 Write a program that extracts from a given sequence of strings all elements that present in it odd number of times. Example:
{C#, SQL, PHP, PHP, SQL, SQL }  {C#, SQL}
* */
    static void Main(string[] args) {
        var sequence = new[] { "C#", "SQL", "PHP", "PHP", "SQL", "SQL" };
        
        var set = new HashSet<string>();
        
        foreach (string str in sequence)
            if(!set.Remove(str))
                set.Add(str);
            
        foreach (var str in set)
            Console.Write("{0} ", str);
    }
}