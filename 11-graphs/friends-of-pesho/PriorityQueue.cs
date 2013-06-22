using System;
using System.Collections.Generic;
using System.Linq;

class PriorityQueue<P, V> : IEnumerable<Tuple<P,V>>
{
    readonly SortedSet<Tuple<P, V>> set;
    readonly Dictionary<V, Tuple<P, V>> entries;


    
    public PriorityQueue(Comparison<P> comparison)
    {
        comparison.ThrowIfNull();

        set = new SortedSet<Tuple<P, V>>(Comparer<Tuple<P, V>>.Create((e1, e2) => comparison(e1.Item1, e2.Item1)));
        entries = new Dictionary<V,Tuple<P, V>>();
    }

    public PriorityQueue()
        : this(Comparer<P>.Default.Compare)
    {
    }

    public void Enqueue(P priority, V item)
    {
        var e = new Tuple<P, V>(priority, item);
        this.entries.Add(item, e);
        this.set.Add(e);
    }

    public void Rekey(V item, P newPriority)
    {
        Tuple<P, V> e;
        if (this.entries.TryGetValue(item, out e))
        {
            this.set.Remove(e);
            this.entries.Remove(item);
        }
        this.Enqueue(newPriority, item);
    }
    public P Priority(V item)
    {
        return this.entries[item].Item1;
    }
    public P PriorityOrDefault(V item, P def)
    {
        Tuple<P, V> e;
        if (this.entries.TryGetValue(item, out e))
        {
            return e.Item1;
        }
        return def;
    }
    public V Dequeue()
    {
        return this.DequeueWithPriority().Item2;
    }

    public Tuple<P, V> DequeueWithPriority()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty.");

        var value = this.set.Min;
        this.set.Remove(value);
        entries.Remove(value.Item2);
        return value;
    }

    public int Count
    {
        get { return this.entries.Count; }
    }

    public bool IsEmpty
    {
        get { return this.Count == 0; }
    }

    public IEnumerator<Tuple<P, V>> GetEnumerator()
    {
        return this.set.GetEnumerator();
    }

    public IEnumerable<V> Values
    {
        get
        {
            return this.set.Select(e => e.Item2);
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }





}