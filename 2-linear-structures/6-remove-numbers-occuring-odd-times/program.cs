using System;
using System.Collections.Generic;

public class Program
{
    // 6 Write a program that removes from given sequence all numbers 
    // that occur odd number of times. 
    // Example:
    // {4, 2, 2, 5, 2, 3, 2, 3, 1, 5, 2}  {5, 3, 3, 5}
    
    public static void Main()
    {
        Console.WriteLine("Program input: enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        
        var numbers = new List<int>();
        var dict = new Dictionary<int, int>();
        
        for(var ii = 0; ii < n; ++ii)
        {
            int number = int.Parse(Console.ReadLine());
            numbers.Add(number);
            
            int count;
            
            if(dict.TryGetValue(number, out count))
                dict[number] = count + 1;
            else
                dict[number] = 1;
        }
        
        foreach(var kvp in dict)
        {
            if(kvp.Value % 2 == 1)
            {
                numbers.RemoveAll(e => e == kvp.Key);
            }
        }
        foreach(var elem in numbers)
        {
            Console.WriteLine(elem);
        }
    }
}