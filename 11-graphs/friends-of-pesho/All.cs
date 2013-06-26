// 2013-06-24 23:45:07

using Adjacency = Tuple<int, int>;
using AdjacencyList = System.Collections.Generic.List<Tuple<int, int>>;
using Graph = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Tuple<int, int>>>;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System;


// .\Heap.cs


class MinHeap<T> : IEnumerable<T>
{
    readonly Dictionary<T, int> reverse = new Dictionary<T, int>();

    Tuple<int, T>[] array = new Tuple<int, T>[15];

    public bool TrySetPriority(int priority, T elem, bool allowDownwardMotion = false)
    {
        int oldIndex;

        if (!this.reverse.TryGetValue(elem, out oldIndex))
        {
            MaybeExpand(1);

            this.array[this.Count] = Tuple.Create(priority, elem);
            this.reverse[elem] = this.Count;
            this.Count += 1;
            this.BubbleUp(this.Count - 1);

            return true;
        }

        // Debug.Assert(array[ii].Item2.SafeEquals(elem));


        if (priority > array[oldIndex].Item1)
        {
            if (!allowDownwardMotion)
                return false;

            this.array[oldIndex] = Tuple.Create(priority, elem);

            this.BubbleDown(oldIndex);
        }
        else
        {
            this.array[oldIndex] = Tuple.Create(priority, elem);

            this.BubbleUp(oldIndex);
        }

        return false;
    }

    public int GetPriority(T item)
    {
        return this.array[reverse[item]].Item1;
    }

    public int GetPriorityOrDefault(T item, int def)
    {
        int ii;
        if (!this.reverse.TryGetValue(item, out ii))
            return def;

        return this.array[ii].Item1;
    }

    public int Count
    {
        get;
        private set;
    }

    public int Capacity
    {
        get { return array.Length; }
    }

    public Tuple<int, T> ChopHeadWithPriority()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("Heap is empty.");

        var ret = this.array[0];

        Check();

        this.reverse.Remove(ret.Item2);

        if (this.Count > 1)
        {
            var last = this.array[this.Count - 1];
            this.array[0] = last;
            this.reverse[last.Item2] = 0;

            this.BubbleDown(0);
        }

        this.Count -= 1;
        Check();

        return ret;
    }

    public T ChopHead()
    {
        return this.ChopHeadWithPriority().Item2;
    }

    public bool IsEmpty
    {
        get { return this.Count == 0; }
    }

    [Conditional("NOTHING")]
    void Check()
    {

        Debug.Assert(this.reverse.Count == this.Count);

        foreach (var kvp in reverse)
        {
            Debug.Assert(array[kvp.Value].Item2.SafeEquals(kvp.Key));
        }

        CheckHeap();

    }

    void CheckHeap(int ii = 0)
    {
        if (ii >= this.Count)
            return;
        Debug.Assert(DominatesChildren(ii));
        CheckHeap(LeftChild(ii));
        CheckHeap(RightChild(ii));

    }

    bool Dominates(int who, int whom)
    {
        if (whom >= this.Count)
            return true;
        return this.array[who].Item1 <= this.array[whom].Item1;
    }

    bool DominatedByParent(int index)
    {
        if (index <= 0)
            return true;

        return Dominates(Parent(index), index);
    }

    int SwapWithParent(int index)
    {
        var parent = Parent(index);
        Swap(index, parent);
        return parent;
    }

    bool DominatesChildren(int index)
    {
        if (LeftChild(index) >= this.Count)
            return true;

        if (!Dominates(index, LeftChild(index)))
            return false;

        if (RightChild(index) >= this.Count)
            return true;

        if (!Dominates(index, RightChild(index)))
            return false;

        return true;
    }

    int LeftChild(int index)
    {
        return index * 2 + 1;
    }

    int RightChild(int index)
    {
        return index * 2 + 2;
    }

    int Parent(int index)
    {
        if (index % 2 == 0)
            return index / 2 - 1;
        else
            return index / 2;

    }

    void Swap(int index1, int index2)
    {
        var temp1 = this.array[index1];
        var temp2 = this.array[index2];

        this.array[index1] = temp2;
        this.array[index2] = temp1;

        this.reverse[temp1.Item2] = index2;
        this.reverse[temp2.Item2] = index1;
    }

    int SwapWithDominantChild(int index)
    {
        if (Dominates(LeftChild(index), RightChild(index)))
        {
            Swap(index, LeftChild(index));
            return LeftChild(index);
        }
        else
        {
            Swap(index, RightChild(index));
            return RightChild(index);
        }
    }

    void BubbleUp(int index)
    {
        if (DominatedByParent(index))
            return;

        var parentIndex = SwapWithParent(index);

        BubbleUp(parentIndex);
    }

    void BubbleDown(int index)
    {
        if (DominatesChildren(index))
            return;

        var largerChildIndex = SwapWithDominantChild(index);

        BubbleDown(largerChildIndex);
    }

    bool MaybeExpand(int d)
    {
        if (this.Count + d < this.Capacity)
            return false;

        // jump over a power of 2
        // 1 + 2 + 4 ... + 2^n => 
        // 1 + 2 + 4 ... + 2^n + 2^(n+1) + 2^(n+2)

        Array.Resize(ref array, (this.Capacity + 1) * 4 - 1);

        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return (array as IEnumerable<T>).Take(this.Count).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }



}
// !!! DON'T FORGET TO SET THE PROBLEM NUMBER !!!
// .\libcs.cs


static class Helpers
{
    public static bool SafeEquals<T>(this T what, T whom)
    {
        if (what == null)
            return whom == null;
        return what.Equals(whom);
    }
    public static bool SameContents<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
    {
        return seq1.OrderBy(t=>t,Comparer<T>.Default).SequenceEqual(
               seq2.OrderBy(t=>t,Comparer<T>.Default));
    }
    
    public static bool SameContents<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, Func<T, int> f)
    {
        return seq1.OrderBy(f).SequenceEqual(
               seq2.OrderBy(f));
    }
    
    public static void ThrowIfNegative(this int num, string msg = null)
    {
        if (num < 0)
        {
            throw GetException<ArgumentOutOfRangeException>(msg ?? "Argument must be nonnegative, got: " + num);
        }
    }

    public static void ThrowIfNull<T>(this T obj, string msg = null)
    {
        if (obj == null)
        {
            throw GetException<ArgumentNullException>(msg ?? "Argument cannot be null.");
        }
    }

    public static void ThrowIfNull<T, TE>(this T obj, string msg = null) where TE : Exception
    {
        if (obj == null)
        {
            throw GetException<TE>(msg ?? "Value cannot be null.");
        }
    }

    public static void ThrowIfNotDefined<TEnum>(this TEnum enumValue, string msg = null)
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            throw GetException<ArgumentException>(msg ?? "Invalid enum value: " + enumValue);
        }
    }

    public static void ThrowIfNotDefined<TEnum, TE>(this TEnum enumValue, string msg = null)
        where TE : Exception
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            throw GetException<TE>(msg ?? "Invalid enum value: " + enumValue);
        }
    }

    public static TE GetException<TE>(string msg, Exception innerException = null) where TE : Exception
    {

        var ex = Activator.CreateInstance(typeof(TE), new object[] { msg, innerException });

        return (TE)ex;

    }
    
    public static V GetOrDefault<K,V>(this IDictionary<K,V> dict, K key, V defValue) {
        V ret;
        if (dict.TryGetValue(key, out ret))
            return ret;
        return defValue;
    }
}

















// 2013-06-13
// 2013-06-13
// 2013-06-13
// !!! DON'T FORGET TO SET THE PROBLEM NUMBER !!!
// .\program.cs


// no need to define whole classes

// using Adjacency = System.Tuple<int, int>;
// using AdjacencyList = System.Collections.Generic.List<System.Tuple<int, int>>;
// using Graph = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Tuple<int, int>>>;




class Program
{
    static void Main(string[] args)
    {
        //if (args.Length != 0)
        //{
        //    Console.SetIn(new StreamReader("..\\..\\tests\\test.010.in.txt"));
        //}

        var split = GetInts();

        int verticeCount = split[0];
        int edgeCount = split[1];
        int hospitalCount = split[2];

        var hospitals = GetInts();

        var graph = new Graph();

        for (int ii = 0; ii < verticeCount; ++ii)
        {
            graph[ii + 1] = new AdjacencyList();
        }

        for (int ii = 0; ii < edgeCount; ++ii)
        {
            split = GetInts();

            graph[split[0]].Add(Tuple.Create(split[1], split[2]));
            graph[split[1]].Add(Tuple.Create(split[0], split[2]));
        }

        int? minTree = null;
        int? minHospital = null;

        // we're looking for the hospital which generates
        // the minimum path sum for each house
        // we modify Dijkstra's algorithm and run it once for each hospital

        foreach (var hospital in hospitals)
        {
            var treeWeight = GetTreeWeight(graph, hospital, verticeCount, hospitals);

            if (treeWeight < minTree || minTree == null)
            {
                minTree = treeWeight;
                minHospital = hospital;
            }
        }

        Console.Write(minTree);
    }

    // a *standard* implementation of Dijkstra's algorithm using a priority queue
    // with a minor modification for the problem

    static int GetTreeWeight(Graph graph, int hospital, int verticeCount, IEnumerable<int> hospitals)
    {
        var distances = new MinHeap<int>();

        foreach (var adj in graph[hospital])
        {
            distances.TrySetPriority(adj.Item2, adj.Item1);
        }   

        // return value
        int ret = 0;

        var tree = new HashSet<int>();
        int housesAdded = 0;
        int totalHouses = verticeCount - hospitals.Count();

        while (housesAdded < totalHouses && distances.Count > 0)
        {
            // edge nearest to 'hospital'

            var min = distances.ChopHeadWithPriority();
            var weight = min.Item1;

            // the new vertex in the tree

            var v1 = min.Item2;

            // modification of algorithm:
            // sum the distance to the root of all nodes that aren't
            // hospitals

            if (!hospitals.Contains(v1))
            {
                housesAdded += 1;
                ret += weight;
            }

            tree.Add(v1);

            // update the priorities of all external neighbours of the vertex
            // we've just added

            foreach (var adj in graph[v1])
            {
                var v2 = adj.Item1;
                if (v2 == hospital)
                    continue;
                if (tree.Contains(v2))
                {
                    continue;
                }

                // perhaps improve priority
                distances.TrySetPriority(weight + adj.Item2, v2, false);

            }
        }

        return ret;
    }

    static int[] GetInts()
    {
        return Console.ReadLine()
                      .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(int.Parse)
                      .ToArray();
    }
}


















// !!! DON'T FORGET TO SET THE PROBLEM NUMBER !!!
// .\Tuple.cs

// our own Tuples - structs, instead of classes, for efficiency

public struct Tuple<P, V>
{
    public Tuple(P p, V v) : this()
    {
        this.Item1 = p;
        this.Item2 = v;
    }

    public P Item1 { get; private set; }

    public V Item2 { get; private set; }

    public override string ToString()
    {
        return "(" + Item1 + "," + Item2 + ")";
    }
}

public static class Tuple
{
    public static Tuple<P, V> Create<P, V>(P p, V v)
    {
        return new Tuple<P, V>(p, v);
    }
}
// !!! DON'T FORGET TO SET THE PROBLEM NUMBER !!!