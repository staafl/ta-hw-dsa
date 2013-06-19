using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
static class Program
{
    // 1. A text file students.txt holds information about students and their 
    // courses in the following format:
    // 
    // Kiril  | Ivanov   | C#
    // Stefka | Nikolova | SQL
    // Stela  | Mineva   | Java
    // Milena | Petrova  | C#
    // Ivan   | Grigorov | C#
    // Ivan   | Kolev    | SQL
    // 
    // Using SortedDictionary<K,T> print the courses in alphabetical order 
    // and for each of them prints the students ordered by family and then 
    // by name:
    // 
    // C#: Ivan Grigorov, Kiril Ivanov, Milena Petrova
    // Java: Stela Mineva
    // SQL: Ivan Kolev, Stefka Nikolova

    static void Main(string[] args) 
    {
        // there is another solution with nested dictionaries, but
        // it's messier and less efficient
        
        // I personally would use a SortedSet
        
        var people = new SortedDictionary<Triple, bool>();
        
        foreach (var triple in ReadInput())
            people.Add(triple, true);
            
        Print(people.Keys);
    }
    
    static void Print(IEnumerable<Triple> triples) 
    {
        foreach (var byCourse in triples.Chunk(t => t.Course)) 
        {
            Console.Write("{0}: ", byCourse.First().Course);
            
            bool first = true;
            
            foreach (var triple in byCourse)
            {
                Console.Write("{0}{1} {2}", first ? "" : ", ", triple.FirstName, triple.LastName);
                first = false;
            }   
            
            Console.WriteLine();
        }
            
    }
    
    // Groups a sequence by a given key while preserving its ordering
    // O(n)
    
    static IEnumerable<IEnumerable<T>> Chunk<T,K>(this IEnumerable<T> seq, Func<T,K> keySelector) 
        where K : class
    {
        using (var enm = seq.GetEnumerator())
        {
            var list = new List<T>();
            
            K key = null;
            
            while (enm.MoveNext())
            {
                var key2 = keySelector(enm.Current);
                
                if (key != null && !key.Equals(key2))
                {
                    if (list.Count > 0)
                        yield return list;

                    list = new List<T>();
                }
                key = key2;
                list.Add(enm.Current);
            }
            
            if (list.Count > 0)
                yield return list;
        }
    }
        
    static IEnumerable<Triple> ReadInput() 
    {
        foreach (var line in File.ReadLines("people.txt")) 
        {
            var split = line.Split('|').Select(s => s.Trim()).ToArray();
            
            yield return new Triple(split[2], split[0], split[1]);
        }
    }

}




