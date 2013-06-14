using System;
using System.Collections.Generic;
using System.Linq;

class PriorityQueue<P, V> : IEnumerable<V>
{

    readonly Heap<Tuple<P,V>> heap;

    public PriorityQueue(Comparison<P> comparison)
    {
        comparison.ThrowIfNull();

        heap = new Heap<Tuple<P, V>>((t1, t2) => comparison(t1.Item1, t2.Item1));
    }

    public PriorityQueue()
        : this(Comparer<P>.Default.Compare)
    {
    }

    public void Enqueue(P priority, V item)
    {
        this.heap.Add(Tuple.Create(priority, item));
    }

    public V Dequeue()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        return this.heap.ChopHead().Item2;
    }

    public int Count
    {
        get { return this.heap.Count; }
    }

    public int Capacity
    {
        get { return this.heap.Capacity; }
    }

    public bool IsEmpty
    {
        get { return this.Count == 0; }
    }

    // slow
    public IEnumerator<V> GetEnumerator()
    {
        return heap.OrderBy(t => t.Item1).Select(t => t.Item2).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }





}