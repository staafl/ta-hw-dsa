using System;
using System.Collections.Generic;
using System.Linq;

static class Program
{
    // 11 Implement the data structure linked list. 
    // Define a class ListItem<T> that has two fields: 
    //  value (of type T) and NextItem (of type ListItem<T>). 
    // Define additionally a class LinkedList<T> with a single field FirstElement (of type ListItem<T>).
    
    static void Main()
    {
        var linkedList = new LinkedList<int>();

        linkedList.Add(Enumerable.Range(1, 20));
        Console.WriteLine(linkedList.PrettyPrint());

        Console.WriteLine("Removing odd elements:");
        Console.WriteLine(linkedList.Remove((n,_) => n % 2 == 1, true));
        Console.WriteLine(linkedList.PrettyPrint());
        
        Console.WriteLine("Removing elements at odd index:");
        Console.WriteLine(linkedList.Remove((_,i) => i % 2 == 1, true));
        Console.WriteLine(linkedList.PrettyPrint());
        
        Console.WriteLine("Doubling ourselves:");
        Console.WriteLine(linkedList.Count);
        linkedList.Add(linkedList.ToArray());
        Console.WriteLine(linkedList.Count);
        Console.WriteLine(linkedList.PrettyPrint());
        

    }
    
    public static string PrettyPrint<T>(this IEnumerable<T> seq)
    {
        return string.Join(", ", seq);
    }
}