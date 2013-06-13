using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{

    // 8 * The majorant of an array of size N is a value that occurs in it at least 
    // N/2 + 1 times. Write a program to find the majorant of given array (if exists). 
    // Example:
    // {2, 2, 3, 3, 2, 3, 4, 3, 3}  3

    public static void Main()
    {
        // the obvious solution would be using a hashtable
        // O(n) time, O(n) space 
        // here's a cute approach from the ++algorithms book
        // O(n) time, O(1) space, but since you need to traverse the
        // array twice, you need to keep it in memory => it could be
        // regarded as O(n) when reading the sequence from a generator
        
        Console.WriteLine("Program input: enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        
        int[] array = new int[n];
        for(int ii = 0; ii < n; ++ii) 
        {
            array[ii] = int.Parse(Console.ReadLine());
        }
        
        int count = 1;
        // assuming n != 0 for brevity
        int current = array[0];
        
        for (var ii = 1; ii < n; ++ii)
        {
            if (array[ii] == current)
            {
                count += 1;
            }
            else
            {
                count -= 1;
                if (count == 0)
                {
                    current = array[ii];
                }
            }
        }
        
        if (count > 0)
        {
            // 'current' is the only possible answer
            // check if it really is the majorant
            int countCurrent = 0;
            for (int ii = 0; ii < n; ++ii)
            {
                if (array[ii] == current)
                {
                    countCurrent += 1;
                    if (countCurrent >= (n/2)+1)
                    {
                        Console.WriteLine("Majorant element: {0}", current);
                        return;
                    }
                }
            }
            
        }
        
        Console.WriteLine("No majorant element.");
        
        
        
    }
}

















