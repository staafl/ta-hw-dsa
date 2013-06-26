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
        
        // debugging input
        
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
        else { 
            count = int.Parse(Console.ReadLine()); 
        }


        var list = new List<string>();

        for (int ii = 0; ii < count; ++ii)
        {
            var l = Console.ReadLine();
            if (l == null)
                break;
            list.Add(l);

        }
        
        // create a hashset of preceding characters for every character

        var beforeHashes = new List<HashSet<char>>();
        var seen = new SortedSet<char>();
        for (int ii = 0; ii < 52; ++ii)
        {
            chars.Add(new HashSet<char>());
        }

        // fill the hashsets
        
        foreach (var elem in list)
        {
            for (int ii = 0; ii < elem.Length; ++ii)
            {
                seen.Add(elem[ii]);
                for (int jj = ii + 1; jj < elem.Length; ++jj)
                {
                    chars[CharToInt(elem[jj])].Add(elem[ii]);
                }
            }
        }

        var ret = "";
        
        // topological sort
        // inefficient version of kahn's algorithm

        while (seen.Count > 0)
        {
            foreach (var ch in seen)
            {
                if (ret.Contains(ch))
                    continue;
                var before = chars[CharToInt(ch)];
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
        
        // 30/100 attempt using dynamic programming
        // http://en.wikipedia.org/wiki/Shortest_common_supersequence

        var agg = new StringBuilder();

        foreach (var now in list)
            SCS(now, agg);

        Console.WriteLine(agg.ToString());

    }
    
    // letter to number 0..51
    static int CharToInt(char ch)
    {
        if (ch >= 'a')
            return 26 + ch - 'a';
        return ch - 'A';
    }

    // number 0..51 to 'A'..'Z', 'a'..'z'
    static char IntToChar(int num)
    {
        if (num > 25)
            return (char)((num - 26) + 'a');
        return (char)(num + 'A');
    }
    
    // DO solution attempt
    
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