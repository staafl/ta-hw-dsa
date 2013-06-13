using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
class Program
{
    /* 6 * A text file phones.txt holds information about people, their town and phone number:
     * Duplicates can occur in people names, towns and phone numbers. 
     * Write a program to read the phones file and execute a sequence of commands 
     * given in the file commands.txt:
     * find(name) - display all matching records by given name (first, middle, last or nickname)
     * find(name, town) - display all matching records by given name and town
     * Mimi Shmatkata          | Plovdiv  | 0888 12 34 56
     * Kireto                  | Varna    | 052 23 45 67
     * Daniela Ivanova Petrova | Karnobat | 0899 999 888
     * Bat Gancho              | Sofia    | 02 946 946 946
     * */

    static void Main(string[] args)
    {

        var byName = new Dictionary<string, List<string>>();
        var byNameAndTown = new Dictionary<Tuple<string, string>, List<string>>();

        // this problem would normally call for a multidictionary
        // however, since it's simple and short I have decided not
        // to burden it with an external library reference.

        foreach (string line in File.ReadLines("phones.txt"))
        {
            var fields = line.Split('|').Select(s => s.Trim());
            var names = fields.First().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var town = fields.Skip(1).First();

            foreach (var name in names)
            {
                // I don't believe this is improper variable reuse
                List<string> tempList;

                if (!byName.TryGetValue(name, out tempList))
                    byName[name] = tempList = new List<string>();

                tempList.Add(line);

                if (!byNameAndTown.TryGetValue(Tuple.Create(name, town), out tempList))
                    byNameAndTown[Tuple.Create(name, town)] = tempList = new List<string>();

                tempList.Add(line);

            }
        }

        foreach (string cmd in File.ReadLines("commands.txt"))
        {
            Console.WriteLine();
            Console.WriteLine(">> " + cmd);
            Match match;

            match = Regex.Match(cmd, @"find\( ([^),]+), ([^),]+) \)", RegexOptions.IgnorePatternWhitespace);

            if (match.Success)
            {
                Show(Tuple.Create(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim()), byNameAndTown);
                continue;
            }

            match = Regex.Match(cmd, @"find\( ([^),]+) \)", RegexOptions.IgnorePatternWhitespace);

            if (match.Success)
            {
                Show(match.Groups[1].Value.Trim(), byName);
                continue;
            }

            Console.WriteLine("Unrecognized command");
        }

    }

    static void Show<T>(T key, IDictionary<T, List<string>> dict)
    {
        List<string> list;
        if (!dict.TryGetValue(key, out list))
            return;
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}