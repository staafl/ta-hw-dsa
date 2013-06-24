using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class MinHeap<T> : IEnumerable<T>
{
    Dictionary<T,int> reverse = new Dictionary<T,int>();
    
    Tuple<int,T>[] array = new Tuple<int,T>[15];

    public bool TrySetPriority(int priority, T elem)
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

        // fixme: unhardcode this

        // Debug.Assert(array[ii].Item2.SafeEquals(elem));

        if (priority > array[oldIndex].Item1)
        {
            // fixme: delete and reinsert
            return false;
        }
        
        this.array[oldIndex] = Tuple.Create(priority, elem);
        this.BubbleUp(oldIndex);
        this.BubbleDown(oldIndex);

        return true;
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

    public int Priority(T item)
    {
        return this.array[reverse[item]].Item1;
    }

    public int PriorityOrDefault(T item, int def)
    {
        int ii;
        if (!this.reverse.TryGetValue(item, out ii))
            return def;
            
        return this.array[ii].Item1;
    }

    public bool Delete(T elem)
    {
        int ii;
        if (!this.reverse.TryGetValue(elem, out ii))
            return false;
        
        // Debug.Assert(array[ii].Item2.SafeEquals(elem));

        this.reverse.Remove(elem);


        if (ii == this.Count - 1)
        {
            Check();
            this.Count -= 1;
            return true;
        }

        var last = this.array[this.Count - 1];
        this.array[ii] = last;
        this.reverse[last.Item2] = ii;

        this.Count -= 1;

        this.BubbleDown(ii);
        this.BubbleUp(ii);
        
        // reverse.Remove(elem);

        Check();
        return true;

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

    public Tuple<int,T> ChopHeadWithPriority()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("Heap is empty.");

        var ret = this.array[0];

        Check();

        this.reverse.Remove(ret.Item2);

        // fixme - duplication with delete

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