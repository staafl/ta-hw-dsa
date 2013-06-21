using System;
using System.Collections.Generic;
using System.Linq;

class PriorityQueue<P, V> : IEnumerable<V>
{
    readonly Heap<Tuple<P, V>> heap;
    readonly Dictionary<V,P> priorities;

    public PriorityQueue(Comparison<P> comparison)
    {
        comparison.ThrowIfNull();

        heap = new Heap<Tuple<P, V>>((t1, t2) => comparison(t1.Item1, t2.Item1));
        priorities = new Dictionary<V,P>();
    }

    public PriorityQueue()
        : this(Comparer<P>.Default.Compare)
    {
    }

    public void Enqueue(P priority, V item)
    {
        this.priorities.Add(item, priority);
        this.heap.Add(Tuple.Create(priority, item));
    }

    public void Rekey(V item, P newPriority)
    {
        if (this.priorities.ContainsKey(item))
        {
            var tuple = new Tuple<P, V>(this.priorities[item], item);
            this.heap.Delete(tuple);
            this.priorities.Remove(item);
        }
        this.Enqueue(newPriority, item);
    }
    public P Priority(V item)
    {
        return this.priorities[item];
    }
    public P PriorityOrDefault(V item, P def)
    {
        return this.priorities.GetOrDefault(item, def);
    }
    public V Dequeue()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        var ret = this.heap.ChopHead().Item2;
        priorities.Remove(ret);
        return ret;
    }

    public Tuple<P, V> DequeueWithPriority()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        var ret = this.heap.ChopHead();
                priorities.Remove(ret.Item2);
        return ret;
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

    public IEnumerable<Tuple<P, V>> NotInOrder()
    {
        return heap;
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