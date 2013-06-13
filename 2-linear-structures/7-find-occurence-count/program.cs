using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{

    // 7 Write a program that finds in given array of integers (all belonging to the range [0..1000]) 
    // how many times each of them occurs.
    // Example: array = {3, 4, 4, 2, 3, 3, 4, 3, 2}
    // 2  2 times
    // 3  4 times
    // 4  3 times

    public static void Main()
    {
        // since the assignment specifies that we're working with integers <= 1000,
        // I guess they want us to solve it using an array instead of a dictionary

        const int MIN_VALUE = 0;
        const int MAX_VALUE = 1000;
        
        Console.WriteLine("Program input: enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        int[] counts = new int[MAX_VALUE-MIN_VALUE+1];
        
        for (var ii = 0; ii < n; ++ii)
        {
            int num = int.Parse(Console.ReadLine());
            counts[num - MIN_VALUE] += 1;
        }
        
        for (int ii = MIN_VALUE; ii <= MAX_VALUE; ++ii) 
        {
            if (counts[ii - MIN_VALUE] != 0)
            {
                Console.WriteLine("{0}: {1}", ii, counts[ii - MIN_VALUE]);
            }
        }
    }
}