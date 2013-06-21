using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        var balls = Console.ReadLine();

        Console.WriteLine(CountBinaryTrees(balls.Length) * GetColorings(balls));
        
    }

    static readonly List<BigInteger> factorials = new List<BigInteger>{1};

    static BigInteger GetFactorial(int n)
    {
        if (factorials.Count <= n)
        {
            for (int ii = factorials.Count; ii <= n; ++ii)
            {
                factorials.Add(factorials[ii - 1] * ii);
            }
        }
        return factorials[n];
    }

    static BigInteger CountBinaryTrees(int n)
    {
        // n-th Catalan number: (2n)!/[(n+1)!n!]
        // http://en.wikipedia.org/wiki/Catalan_number
        return GetFactorial(2 * n) / (GetFactorial(n + 1) * GetFactorial(n));
    }

    static BigInteger GetColorings(string balls)
    {
        // permutations with repetitions: n!/[c_1!*c_2!*...*c_k!]
        // http://en.wikipedia.org/wiki/Multinomial_theorem#Multinomial_coefficients

        BigInteger permutations = GetFactorial(balls.Length);

        BigInteger result = permutations;

        foreach (var group in balls.GroupBy(c => c))
            result /= GetFactorial(group.Count());

        return result;
    }
}

















