using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args) 
    {
        var dict = new SortedList<string,string>();
        bool letter = false;
        string letters = "";
        string numbers = "";
        foreach (var elem in Console.ReadLine() + "|")
        {
            if(!Char.IsNumber(elem))
            {
                dict[numbers] = letters;
                numbers = "";
                letters += elem;
            }
            else 
            {
                numbers += elem;
            }
        }
    }
    
    static void Recurse()
}