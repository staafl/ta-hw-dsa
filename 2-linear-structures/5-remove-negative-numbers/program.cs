using System;
using System.Collections.Generic;

public class Program
{
    // 5 Write a program that removes from given sequence all negative numbers.

    public static void Main()
    {
        var numbers = new List<int>();
        Console.WriteLine("Program input: enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        for(var ii = 0; ii < n; ++ii)
        {
            numbers.Add(int.Parse(Console.ReadLine()));
        }
        for(var ii = numbers.Count - 1; ii >= 0; --ii)
        {
            if(numbers[ii] < 0)
                numbers.RemoveAt(ii);
        }
        foreach(var elem in numbers)
        {
            Console.WriteLine(elem);
        }
    }
}