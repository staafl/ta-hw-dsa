using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 0 || Debugger.IsAttached)
        {
            Console.SetIn(new StringReader(
@"2 0 2
3 4 3
...
.#.
.#.
.#D
...
...
##.
D..
..D
...
##.
..."));
            Console.SetIn(new StringReader(
@"0 0 0
2 3 4
.#U.
..#.
U...
..D.
....
..U."));
        }

        var split = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int x = split[1];
        int y = split[2];
        int z = split[0];

        split = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int l = split[0];
        int r = split[1];
        int c = split[2];

        var list = new List<char[,]>();

        for (int ii = 0; ii < l; ++ii)
        {
            var arr = new char[r, c];
            for (int row = 0; row < r; ++row)
            {
                var str = Console.ReadLine();
                for (int col = 0; col < c; ++col)
                {
                    char ch;
                    try
                    {
                        ch = str[col];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new SystemException();
                    }
                    arr[row, col] = ch;

                }
            }

            list.Add(arr);
        }


        var value = Solve(list, Tuple.Create(x, y, z), new HashSet<Tuple<int, int, int>>());

        Console.WriteLine(value);
        if (Debugger.IsAttached)
            Console.ReadKey();
    }

    static int Solve(List<char[,]> list, Tuple<int, int, int> pos, HashSet<Tuple<int, int, int>> seen)
    {
        var queue = new Queue<Tuple<Tuple<int, int, int>, int>>();
        queue.Enqueue(Tuple.Create(pos, 0));
        while (queue.Count > 0)
        {
            var t = queue.Dequeue();
            foreach (var next in GetNeighbours(list, t.Item1))
            {
                if (next.Item3 == -1 ||
                    next.Item3 == list.Count)
                    return t.Item2 + 1;
                if (!Passable(list, next))
                    continue;
                if (seen.Add(next))
                {
                    queue.Enqueue(Tuple.Create(next, t.Item2 + 1));
                }
            }
        }

        return -1;
    }

    static char GetCh(List<char[,]> list, Tuple<int, int, int> pos)
    {
        return list[pos.Item3][pos.Item1, pos.Item2];
    }
    static bool Passable(List<char[,]> list, Tuple<int, int, int> pos)
    {
        var ch = GetCh(list, pos);
        return  ch != '#';
    }
    static IEnumerable<Tuple<int, int, int>> GetNeighbours(List<char[,]> list, Tuple<int, int, int> pos)
    {
        var ch = GetCh(list, pos);

        if (ch == 'D')
            yield return Tuple.Create(pos.Item1, pos.Item2, pos.Item3 - 1);
        else if (ch == 'U')
            yield return Tuple.Create(pos.Item1, pos.Item2, pos.Item3 + 1);

        for (int row = -1; row <= 1; ++row)
        {
            for (int col = -1; col <= 1; ++col)
            {
                if ((row == 0) == (col == 0))
                    continue;
                var t = Tuple.Create(pos.Item1 + row, pos.Item2 + col, pos.Item3);
                if (t.Item1 < 0 || t.Item2 < 0)
                    continue;
                if (t.Item1 >= list[0].GetLength(0) || t.Item2 >= list[0].GetLength(1))
                    continue;

                yield return t;
            }
        }
    }
}