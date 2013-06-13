using System;
using System.Collections.Generic;
using System.Linq;
 
public class Program
{
    // 1 Write a program that reads from the console a sequence of positive 
    // integer numbers. The sequence ends when empty line is entered. 
    // Calculate and print the sum and average of the elements of the sequence. 
    // Keep the sequence in List<int>.

    public static void Main()
    {
        string input;
        var numbers = new List<int>();

        Console.WriteLine("Enter integers, one per line, terminated by an empty line");
        while(true)
        {
            input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
                break;
            numbers.Add(int.Parse(input));
        }
        
        if(numbers.Count > 0)
          Console.WriteLine("Average: {0}", numbers.Average());
        Console.WriteLine("Sum: {0}", numbers.Sum());
        Console.WriteLine("Count: {0}", numbers.Count());
    }
}