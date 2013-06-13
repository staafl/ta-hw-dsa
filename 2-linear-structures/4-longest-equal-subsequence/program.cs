using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    // 4 Write a method that finds the longest subsequence of equal numbers 
    // in given List<int> and returns the result as new List<int>. 
    // Write a program to test whether the method works correctly.

    public static void Main()
    {
        Console.WriteLine("Program input: enter N, followed by N integers, one per line");
        int n = int.Parse(Console.ReadLine());
        
        var numbers = new List<int>();
        for(var ii = 0; ii < n; ++ii)
        {
            numbers.Add(int.Parse(Console.ReadLine()));
        }

        Console.WriteLine("Expected output: enter expected element");
        int expectedElem = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Expected output: enter expected count");
        int expectedCount = int.Parse(Console.ReadLine());
        
        var list = LongestEqualSequence(numbers);
        if(list.Count == expectedCount &&
           list.Count > 0 && 
           list[0] == expectedElem)
        {
          Console.WriteLine("Sequence matches.");
        }
        else 
        {
          Console.WriteLine("Sequence doesn't match.");
        }
    }
    
    public static List<int> LongestEqualSequence(List<int> list) 
    {
        int? currElem = null;
        int currCnt = 0;
        
        int? maxElem = null;
        int maxCnt = 0;
        
        foreach(var elem in list)
        {
          if(elem != currElem)
          {
            currCnt = 0;
            currElem = elem;
          }
          currCnt += 1;
          if(currCnt > maxCnt) 
          {
            maxCnt = currCnt;
            maxElem = elem;
          }
        }
        
        if(maxElem == null)
        {
          return new List<int>();
        }
        return Enumerable.Repeat(maxElem.Value, maxCnt).ToList();
    }
}