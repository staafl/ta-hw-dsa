using System;
using System.Collections.Generic;
 
public class Program
{
    // 2 Write a program that reads N integers from the console and reverses 
    // them using a stack. Use the Stack<int> class.

    public static void Main()
    {
        Console.WriteLine("Enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        
        var numbersStack = new Stack<int>();
        for(var ii = 0; ii < n; ++ii)
        {
            numbersStack.Push(int.Parse(Console.ReadLine()));
        }
        foreach(var elem in numbersStack)
        {
            Console.WriteLine(elem);
        }
    }
}