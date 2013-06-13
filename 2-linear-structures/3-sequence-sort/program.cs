using System;
using System.Collections.Generic;
 
public class Program
{
    // 3 Write a program that reads a sequence of integers (List<int>) ending 
    // with an empty line and sorts them in an increasing order.
    public static void Main()
    {
        Console.WriteLine("Enter integers, one per line, terminated by an empty line:");
        
        var numbers = new List<int>();
        while(true)
        {
            var input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
                break;
        
            numbers.Add(int.Parse(input));
        }

        numbers.Sort();
        foreach(var elem in numbers)
        {
            Console.WriteLine(elem);
        }
    }
}