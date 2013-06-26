//#define BLAH
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using Wintellect.PowerCollections;
class Program
{
#if !BLAH
    static string input = @"Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
End";
#endif
    static void Main(string[] args)
    {
        //if (args.Length != 0 || Debugger.IsAttached)
        //{
        //    Console.SetIn(new StringReader(input));
        //}

        var output = new StringBuilder(10000);
        var line = "";
        // var ll = new LinkedList<string>();
        var bl = new BigList<string>();
        Dictionary<string, int> count = new Dictionary<string, int>();

        var sb = new StringBuilder();
        // var reader = new StringReader(Console.In.ReadToEnd());
        var reader = Console.In;
        while ((line = Console.ReadLine()) != null)
        {
            if (line == "End")
            {
                Console.WriteLine(output.ToString());
                break;
            }
            sb.Length = 0;
            sb.Append(line);
            if (Eat(sb, "Append "))
            {
                var value = sb.ToString();
                bl.Add(value);
                // ll.AddLast(value);
                output.AppendLine("OK");
                int cnt;
                if (!count.TryGetValue(value, out cnt))
                    cnt = 0;
                cnt += 1;
                count[value] = cnt;
                continue;
            }
            if (Eat(sb, "Serve "))
            {
                int cnt = GetNumber(sb);

                if (cnt > bl.Count)
                // if (cnt > ll.Count)
                {
                    output.AppendLine("Error");
                    continue;
                }
                bool first = true;
                var range = bl.GetRange(0, cnt);
                foreach (var elem in range)
                // while (cnt > 0)
                {
                    if (!first)
                        output.Append(" ");
                    first = false;
                    //output.Append(ll.First.Value);
                    output.Append(elem);
                    count[elem] -= 1;
                    // count[ll.First.Value] -= 1;
                    // ll.RemoveFirst();
                    // cnt -= 1;
                }
                bl.RemoveRange(0, cnt);
                output.AppendLine();
                continue;
            }
            if (Eat(sb, "Insert "))
            {
                var pos = GetNumber(sb);

                if (bl.Count < pos)
                {
                    output.AppendLine("Error");
                    continue;
                }
                sb.Remove(0, 1);
                var value = sb.ToString();
                /*
                if (ll.Count >= 1)
                {
                    var node = ll.First;
                    for (int ii = 0; ii < pos; ++ii)
                    {
                        node = node.Next;
                    }
                    if (node == null)
                        ll.AddLast(sb.ToString());
                    else
                        ll.AddBefore(node, sb.ToString());
                }
                else
                {
                    ll.AddFirst(value);
                }*/

                bl.Insert(pos, value);

                int cnt;
                if (!count.TryGetValue(value, out cnt))
                    cnt = 0;
                cnt += 1;
                count[value] = cnt;

                output.AppendLine("OK");
                continue;
            }
            if (Eat(sb, "Find "))
            {
                int cnt;
                if (!count.TryGetValue(sb.ToString(), out cnt))
                    cnt = 0;
                output.AppendLine(cnt + "");
                continue;
            }
        }

        if (Debugger.IsAttached)
        {
            Console.ReadKey();
        }
    }
#if BLAH
    static readonly string input = @"Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
Append Nakov
Serve 1
Find Ina
Append Mike
Insert 0 Peter
Append Penka
Insert 3 Doncho
Serve 5
Append Asya
Insert 4 Nakov
Append Nakov
Find Asya
Find Nakov
Serve 3
Find Peter
Serve 4
Find Nakov
Insert 1 Ina
End";
#endif
    static int GetNumber(StringBuilder sb1)
    {
        int ret = 0;
        for (int ii = 0; ii < sb1.Length; ++ii)
        {
            if (sb1[ii] == ' ')
            {
                sb1.Remove(0, ii);
                return ret;
            }
            ret *= 10;
            ret += (sb1[ii] - '0');
        }
        return ret;
    }
    static bool Eat(StringBuilder sb, string what)
    {
        for (int ii = 0; ii < what.Length; ++ii)
        {
            if (sb[ii] != what[ii])
                return false;
        }
        sb.Remove(0, what.Length);
        return true;
    }
}