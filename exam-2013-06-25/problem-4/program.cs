using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
static class Program
{
    static void Main(string[] args)
    {
        int count;
        if (args.Length != 0 || Debugger.IsAttached)
        {
            Console.SetIn(new StringReader(
@"
abyc
xab
bc
"));
            if (false) Console.SetIn(new StringReader(
  @"
abd
abc
bc
bcd
adsf
adsfxf
abyc
xabz
"
  ));
            if (false) Console.SetIn(new StringReader(
 @"
ab
bc
cd
de
"
 ));

            Console.ReadLine();
            count = 10000;
        }
        else { count = int.Parse(Console.ReadLine()); }


        var list = new List<string>();

        for (int ii = 0; ii < count; ++ii)
        {
            var l = Console.ReadLine();
            if (l == null)
                break;
            list.Add(l);

        }


        var chars = new List<Tuple<char, HashSet<char>>>();
        var seen = new SortedSet<char>();
        for (int ii = 0; ii < 52; ++ii)
        {
            chars.Add(Tuple.Create(IntToChar(ii), new HashSet<char>()));
        }

        foreach (var elem in list)
        {
            for (int ii = 0; ii < elem.Length; ++ii)
            {
                seen.Add(elem[ii]);
                for (int jj = ii + 1; jj < elem.Length; ++jj)
                {
                    chars[CharToInt(elem[jj])].Item2.Add(elem[ii]);
                }
            }
        }

        var ret = "";


        while (seen.Count > 0)
        {
            foreach (var ch in seen)
            {
                if (ret.Contains(ch))
                    continue;
                var t = chars[CharToInt(ch)];
                var before = t.Item2;
                // Debug.Assert(t.Item1 == ch);
                foreach (var elem in ret)
                    before.Remove(elem);
                if (before.Count == 0)
                {
                    ret += ch;
                    seen.Remove(ch);
                    break;
                }
            }
        }

        Console.WriteLine(ret);

        return;

        ret = "";

        foreach (var t in chars.OrderBy(t => t.Item2.Count).ThenBy(t => t.Item1))
        {
            if (seen.Contains(t.Item1))
                ret += t.Item1;
        }

        Console.WriteLine(ret);


        list.Sort((e1, e2) => e1.Length - e2.Length);
        // list.Reverse();


        var agg = new StringBuilder();

        foreach (var now in list)
            SCS(now, agg);

        Console.WriteLine(agg.ToString());

        Console.ReadKey();
    }
    static int CharToInt(char ch)
    {
        if (ch >= 'a')
            return 26 + ch - 'a';
        return ch - 'A';
    }

    static char IntToChar(int num)
    {
        if (num > 25)
            return (char)((num - 26) + 'a');
        return (char)(num + 'A');
    }
    class Link
    {
        public int len;
        public char letter;
        public Link next;
        public override string ToString()
        {
            return len + " " + letter;
        }
    }

    public static string SCS(string x, string y)
    {
        var sb = new StringBuilder(y);
        SCS(x, sb);
        return sb.ToString();
    }
    public static void SCS(string x, StringBuilder y)
    {
        int lx = x.Length;
        int ly = y.Length;

        var links = new Link[ly + 1, lx + 1];


        for (int ii = 0; ii <= lx; ++ii)
        {
            for (int jj = 0; jj <= ly; ++jj)
            {
                links[jj, ii] = new Link();
            }
        }
        Action printTable = () => { };
        if (false && Debugger.IsAttached)

            printTable = () =>
            {
                Console.WriteLine();

                for (int ii = 0; ii <= lx; ++ii)
                {
                    for (int jj = 0; jj <= ly; ++jj)
                    {
                        Console.Write(links[jj, ii] + " | ");
                    }
                    Console.WriteLine();
                }
            };

        links[ly, lx] = new Link();

        for (int i = ly - 1; i >= 0; i--)
            links[i, lx] = new Link { len = ly - i, letter = y[i], next = links[i + 1, lx] };

        for (int j = lx - 1; j >= 0; j--)
            links[ly, j] = new Link { len = lx - j, letter = x[j], next = links[ly, j + 1] };

        printTable();

        for (int i = ly - 1; i >= 0; i--)
        {
            for (int j = lx - 1; j >= 0; j--)
            {
                var lp = links[i, j];
                if (y[i] == x[j])
                {
                    lp.next = links[i + 1, j + 1];
                    lp.letter = x[j];
                }
                else if (links[i, j + 1].len < links[i + 1, j].len)
                {
                    lp.next = links[i, j + 1];
                    lp.letter = x[j];
                }
                else if (links[i, j + 1].len > links[i + 1, j].len)
                {
                    lp.next = links[i + 1, j];
                    lp.letter = y[i];
                }
                else if (x[j] < y[i])
                {
                    lp.next = links[i, j + 1];
                    lp.letter = x[j];
                }
                else
                {
                    lp.next = links[i + 1, j];
                    lp.letter = y[i];
                }


                lp.len = lp.next.len + 1;
                printTable();
            }
        }
        {
            y.Length = 0;
            var lp = links[0, 0];
            for (; lp != null && lp.letter != '\0'; lp = lp.next)
                y.Append(lp.letter);
        }

    }

    public static void Reverse(this StringBuilder text)
    {
        if (text.Length > 1)
        {
            int pivotPos = text.Length / 2;
            for (int i = 0; i < pivotPos; i++)
            {
                int iRight = text.Length - (i + 1);
                char rightChar = text[i];
                char leftChar = text[iRight];
                text[i] = leftChar;
                text[iRight] = rightChar;
            }
        }
    }

}