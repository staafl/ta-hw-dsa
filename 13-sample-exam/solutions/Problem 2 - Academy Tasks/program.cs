using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        if (Debugger.IsAttached || args.Length != 0)
        {
            const string input =
@"1, 2, 3
2";
            Console.SetIn(new StringReader(input));
            Console.SetIn(new StreamReader(@"D:\_lib\telerik academy\algorithms\homework\13-sample-exam\solutions\Problem 2 - Academy Tasks\Tests\test.015.in.txt"));
        }

        var pleas = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var variety = int.Parse(Console.ReadLine());

        Console.WriteLine(Solve(pleas, variety));
    }

    static int Solve(int[] pleas, int variety)
    {
        var count = pleas.Length;

        var min = 0;
        var max = 0;


        int ii = 1;
        for (; ii < count; ++ii)
        {
            if (pleas[ii] <= pleas[min])
                min = ii;
            if (pleas[ii] >= pleas[max])
                max = ii;

            if (pleas[max] - pleas[min] >= variety)
                break;
        }

        if (ii == count)
        {
            return ii; // / 2 + ii % 2;
        }

        if ((ii % 2) == 0)
        {
            for (int jj = 0; jj < ii; jj += 2)
            {
                if (Math.Abs(pleas[jj] - pleas[min]) >= variety)
                    return min / 2 + 1;
                if (Math.Abs(pleas[jj] - pleas[max]) >= variety)
                    return max / 2 + 1;
            }
        }

        return Math.Max(min, max) / 2 + 2;


        //        var mins = new int[count];
        //        mins[0,0] = pleas[0];
        //        
        //        var maxs = new int[count];
        //        maxs[0,0] = pleas[0];
        //        
        //        var counts = new int[count];
        //        
        //        for (int ii = 1; ii <= length; ++ii)
        //        {
        //            var twoBack = Math.Max(ii - 2, 0);
        //            mins[ii] = Math.Min(pleas[ii], Math.Min(mins[ii - 1], mins[twoBack]));
        //            maxs[ii] = Math.Max(pleas[ii], Math.Min(mins[ii - 1], mins[twoBack]));
        //            
        //            if (maxs[ii] - mins[ii] >= variety)
        //                return
        //        }

    }
}