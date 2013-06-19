using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;
using System.Diagnostics;

class Program
{
    /* 3 Implement a class BiDictionary<K1,K2,T> that allows adding 
       triples {key1, key2, value} and fast search by key1, key2 or by 
       both key1 and key2. 
       
       Note: multiple values can be stored for given key.
     * */

    static void Main(string[] args) 
    {
        // the assignment isn't very clear on what it wants
        // I'm just implementing the basic functionality
        
        var biDict = new BiDictionary<string, int, string>();
        
        biDict.Add("german", 7, "sieben");
        biDict.Add("german", 8, "acht");
        biDict.Add("german", 9, "neun");
        biDict.Add("german", 10, "zehn");
        
        biDict.Add("greek", 7, "efta");
        biDict.Add("greek", 8, "ohto");
        biDict.Add("greek", 9, "enia");
        biDict.Add("greek", 10, "deka");
        
        Debug.Assert(biDict.ByKey1("german").SameContents(new[]{"sieben","acht","neun","zehn"}));
        
        Debug.Assert(biDict.ByKey2(7).SameContents(new[]{"sieben","efta"}));
        
        Debug.Assert(biDict.ByPair("greek", 10).SameContents(new[]{"deka"}));
        
    }
    
    class BiDictionary<K1,K2,T>
    {
        readonly MultiDictionary<K1,T> dict1 = new MultiDictionary<K1,T>(true);
        readonly MultiDictionary<K2,T> dict2 = new MultiDictionary<K2,T>(true);
        readonly MultiDictionary<Tuple<K1,K2>,T> dictBoth = new MultiDictionary<Tuple<K1,K2>,T>(true);
        
        public void Add(K1 key1, K2 key2, T value) 
        {
            this.dict1.Add(key1, value);
            this.dict2.Add(key2, value);
            this.dictBoth.Add(Tuple.Create(key1, key2), value);
        }
        
        public IEnumerable<T> ByKey1(K1 key1) 
        {
            return this.dict1[key1];
        }
        
        public IEnumerable<T> ByKey2(K2 key2) 
        {
            return this.dict2[key2];
        }
        
        public IEnumerable<T> ByPair(K1 key1, K2 key2) 
        {
            return this.dictBoth[Tuple.Create(key1, key2)];
        }
        
    }
}