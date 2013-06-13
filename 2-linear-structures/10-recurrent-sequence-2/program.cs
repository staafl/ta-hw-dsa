using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{

    // Exercises (5)
    // 10 * We are given numbers N and M and the following operations:
    // 1 N = N+1
    // 2 N = N+2
    // 3 N = N*2
    // Write a program that finds the shortest sequence of operations from 
    // the list above that starts from N and finishes in M. 
    // Hint: use a queue.
    // Example: N = 5, M = 16
    // Sequence: 5  7  8  16

    public static void Main()
    {
        // using BFS to find shortest distance between nodes in
        // the unweighted graph of values
        
        Console.WriteLine("Program input: enter N");
        int n = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Program input: enter M > N");
        int m = int.Parse(Console.ReadLine());
        
        if (m <= n)
        {
            Console.WriteLine("M must be > N!");
            return;
        }
        
        var queue = new Queue<Tuple<int, string>>();
        queue.Enqueue(Tuple.Create(n, n + ""));
        
        Action<int, string> enqueue = (next, str) => 
        {
            queue.Enqueue(Tuple.Create(next, str + next));
        };
        while(true)
        {
            var pair = queue.Dequeue();
            if (pair.Item1 == m)
            {
                Console.WriteLine(pair.Item2);
                break;
            }
            
            enqueue(pair.Item1 + 1, pair.Item2 + " + 1 = ");
            enqueue(pair.Item1 + 2, pair.Item2 + " + 2 = ");
            enqueue(pair.Item1 * 2, pair.Item2 + " * 2 = ");
        }
        
        
        
    }
}

















