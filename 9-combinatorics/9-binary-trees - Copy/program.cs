using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var balls = Console.ReadLine();

        var indexed = from ix in Enumerable.Range(0, balls.Length)
                      select Tuple.Create(ix, (char?)balls[ix]);

        indexed = indexed.Concat(new[] { Tuple.Create(-1, (char?)null) }).ToArray();
        int value = 0;
        var rootColors = new HashSet<char>();
        foreach (var root in indexed)
        {
            if (root.Item1 == -1)
                continue;

            if (rootColors.Contains(root.Item2.Value))
                continue;

            rootColors.Add(root.Item2.Value);

            var used = new HashSet<int>();

            used.Add(root.Item1);

            value += Recurse(indexed, used);
        }
        Console.WriteLine(value);
    }

    static int Recurse(IEnumerable<Tuple<int, char?>> indexedBalls,
                       HashSet<int> usedBalls)
    {
        int ret = 0;

        int remaining = indexedBalls.Count() - usedBalls.Count - 1;

        if (remaining == 0)
            return 1;

        var usedPairColorsHere = new HashSet<Tuple<char?, char?>>();

        foreach (var ball1 in indexedBalls)
        {
            if (usedBalls.Contains(ball1.Item1))
                continue;

            if (ball1.Item1 == -1)
                continue;

            usedBalls.Add(ball1.Item1);

            foreach (var ball2 in indexedBalls)
            {
                if (usedBalls.Contains(ball2.Item1))
                    continue;

                // examine each pair just once

                if (ball2.Item1 < ball1.Item1 && ball2.Item1 != -1)
                    continue;

                var pairColors1 = Tuple.Create(ball1.Item2, ball2.Item2);

                var pairColors2 = Tuple.Create(ball2.Item2, ball1.Item2);

                int multiplier = 2;

                if (usedPairColorsHere.Contains(pairColors1))
                {
                    multiplier -= 1;
                }

                usedPairColorsHere.Add(pairColors1);

                if (usedPairColorsHere.Contains(pairColors2))
                {
                    multiplier -= 1;
                }

                usedPairColorsHere.Add(pairColors2);

                if (multiplier != 0)
                {
                    if (ball2.Item1 != -1)
                        usedBalls.Add(ball2.Item1);

                    ret += multiplier * Recurse(indexedBalls, usedBalls);

                    usedBalls.Remove(ball2.Item1);
                }
            }

            usedBalls.Remove(ball1.Item1);
        }

        return ret;

    }
}

















