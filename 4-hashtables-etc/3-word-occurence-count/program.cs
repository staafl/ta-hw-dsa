using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
class Program
{
/* 1 Write a program that counts how many times each word from given text file words.txt appears in it. The character casing differences should be ignored. The result words should be ordered by their number of occurrences in the text. Example:
	is  2
	the  2
	this  3
	text  6
This is the TEXT. Text, text, text - THIS TEXT! Is this the text?
 * */ 

    static void Main(string[] args) {

        string text = File.ReadAllText("words.txt");
        string stripped = Regex.Replace(text, @"[^\w ]+", "");
        string[] words = stripped.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        
        // GroupBy uses a dictionary internally
        var groups = words.GroupBy(w => w.ToLower());
        
        foreach (var group in groups.OrderBy(g => g.Count()))
            Console.WriteLine("{0} -> {1}", group.Key, group.Count());
        
    }
}