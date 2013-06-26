using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Numerics;
class Program
{
    static readonly List<BigInteger> factorials = new List<BigInteger> { 1 };

    static void Main(string[] args) 
    {
        if (args.Length != 0 || Debugger.IsAttached)
        {
            Console.SetIn(new StringReader(
@"2"));
        }

        var a = int.Parse(Console.ReadLine());

        Console.WriteLine(Factorial(a) / (Factorial(a / 2) * Factorial(a / 2 + 1)));

        //    2  4  6
        // 1, 1, 2, 5, 14, 42, 132, 429, 1430, 4862, 16796, 58786, 208012, 742900, 2674440, 9694845, 35357670, 129644790, 477638700, 1767263190, 6564120420, 24466267020, 91482563640, 343059613650, 1289904147324, 4861946401452, … (sequence A000108 in OEIS)
        // 
    }

    static BigInteger Factorial(int n) {
        BigInteger last = factorials[factorials.Count - 1];
        for (int ii = factorials.Count; ii <= n; ++ii)
        {
            last *= ii;
            factorials.Add(last);
        }
        return factorials[n];
    }
}