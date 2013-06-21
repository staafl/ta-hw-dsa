using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main(string[] args) 
    {
        int n = int.Parse(Console.ReadLine());
        
        ulong value = 0;
        
        // http://www.youtube.com/watch?feature=player_detailpage&v=s936-UUKwtM#t=320s
        // 1 + (n-1) + (n-1)*(n-2)/2! + (n-1)*(n-2)*(n-3)*3!... = 2^(n-1)
        // the sum of row n in pascal's triangle: http://www.mathsisfun.com/images/pascals-triangle-4.gif
        var multiplier = (ulong)1 << n-1;
        
        for(int ii = 0; ii < n; ++ii)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                ii -= 1;
                continue;
            }
            var split = line.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

            // manhattan distance to origin
            // http://en.wikipedia.org/wiki/Taxicab_geometry
            var distance = (ulong)(Math.Abs(split[0]) + Math.Abs(split[1]));
            
            // ignore integer overflow
            unchecked
            {
                value += multiplier * distance;
            }
        }

        
        Console.WriteLine(value);
    }
}
