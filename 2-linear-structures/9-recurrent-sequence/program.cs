using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{

    // 9 We are given the following sequence:
    // S1 = N;
    // S2 = S1 + 1;
    // S3 = 2*S1 + 1;
    // S4 = S1 + 2;
    // S5 = S2 + 1;
    // S6 = 2*S2 + 1;
    // S7 = S2 + 2;
    // ...
    // Using the Queue<T> class write a program to print its first 50 members for given N.
    // Example: N=2  2, 3, 5, 4, 4, 7, 5, 6, 11, 7, 5, 9, 6, ...

    public static void Main()
    {
        const int COUNT_TO_SHOW = 50;
        
        Console.WriteLine("Program input: enter N");
        int n = int.Parse(Console.ReadLine());
        
        var queue = new Queue<int>();
        queue.Enqueue(n);
        
        for (int ii = 0; ii < COUNT_TO_SHOW; ++ii)
        {
            int elem = queue.Dequeue();
            Console.WriteLine("{0}: {1}", ii+1, elem);
            queue.Enqueue(elem + 1);
            queue.Enqueue(2*elem + 1);
            queue.Enqueue(elem + 2);
        }
        
    }
}

















