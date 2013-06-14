using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
class Program
{
    /* 3 Write a program that finds a set of words (e.g. 1000 words) 
     * in a large text (e.g. 100 MB text file). Print how many times 
     * each word occurs in the text.
     * Hint: you may find a C# trie in Internet.
     * */
    static void Main(string[] args)
    {
        var dict = new Dictionary<string, int>();
        var knownCount = new Dictionary<string, int>
        {
            {"foo", 10*1000},
            {"bar", 20*1000},
            {"quux",30*1000},
            {"frob",40*1000},
            {"asdf",50*1000}
        };
        var trie = new Trie<int>();

        var sw = new Stopwatch();

        sw.Start();

        // obviously, I couldn't zip the 100 MB file
        // use "bin\debug\generator.cs" to generate it if you want

        using (var reader = new StreamReader("text.txt"))
            foreach (var word in Words(reader))
                dict[word] = 1 + dict.GetOrDefault(word, 0);

        sw.Stop();
        /*
        foreach (var kvp in knownCount)
            Debug.Assert(dict[kvp.Key] == kvp.Value);
        */

        Console.WriteLine("Using hashtable: " + sw.Elapsed.TotalMilliseconds);

        sw.Reset();
        sw.Start();

        using (var reader = new StreamReader("text.txt"))
            foreach (var word in Words(reader))
                trie.Add(word, 1 + trie.GetOrDefault(word, 0));

        sw.Stop();

        foreach (var kvp in dict)
            Debug.Assert(trie.Find(kvp.Key) == kvp.Value);

        // the trie would probably do much better compared to a hashtable when used on
        // natural text with large amount of repetition and low average word length
        // it is however extremely space inefficient

        // at any rate, I'd be surprised if this implementation could beat .NET's build-in
        // hashtable

        Console.WriteLine("Using trie: " + sw.Elapsed.TotalMilliseconds);
    }

    static IEnumerable<string> Words(TextReader reader)
    {
        bool inWord = false;
        var sb = new StringBuilder();

        int read;

        while ((read = reader.Read()) != -1)
        {
            char ch = (char)read;
            if (Char.IsLetter(ch) != inWord)
            {
                inWord = !inWord;
                if (!inWord)
                {
                    yield return sb.ToString();
                    sb.Length = 0;
                }
            }

            if (inWord)
            {
                sb.Append(ch);
            }
        }

        if (inWord)
        {
            yield return sb.ToString();
        }
    }
}