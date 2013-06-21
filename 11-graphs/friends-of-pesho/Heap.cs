using System;
using System.Collections.Generic;
using System.Linq;

class Heap<T> : IEnumerable<T>
{
    T[] array = new T[15];

    readonly Comparison<T> comparison;

    public Heap()
        : this(Comparer<T>.Default.Compare)
    {
    }

    public Heap(Comparison<T> comparison)
    {
        comparison.ThrowIfNull();
        this.comparison = comparison;
    }

    public void Add(T elem)
    {
        MaybeExpand(1);

        this.array[this.Count] = elem;
        this.Count += 1;

        this.BubbleUp(this.Count - 1);
    }

    public bool Delete(T elem)
    {
        for (int ii = 0; ii < this.Count; ++ii)
        {
            if (array[ii].SafeEquals(elem))
            {
                if (ii == this.Count - 1)
                {
                    this.Count -= 1;
                    return true;
                }

                this.array[ii] = this.array[this.Count - 1];

                this.Count -= 1;

                BubbleDown(ii);

                return true;
            }
        }

        return false;
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

    public T ChopHead()
    {

        if (this.Count == 0)
            throw new InvalidOperationException("Heap is empty.");

        var ret = this.array[0];

        this.array[0] = this.array[this.Count - 1];

        this.BubbleDown(0);

        this.Count -= 1;

        return ret;
    }

    public bool IsEmpty
    {
        get { return this.Count == 0; }
    }


    bool Dominates(int who, int whom)
    {
        if (whom >= this.Count)
            return true;
        return this.comparison(this.array[who],
                               this.array[whom]) >= 0;
    }

    bool DominatedByParent(int index)
    {
        if (index <= 0)
            return true;

        return Dominates(Parent(index), index);
    }

    int SwapWithParent(int index)
    {
        Swap(index, index / 2);
        return index / 2;
    }

    bool DominatesChildren(int index)
    {
        if (LeftChild(index) >= this.Count)
            return true;

        if (DominatedByParent(LeftChild(index)))
            return false;

        if (RightChild(index) >= this.Count)
            return true;

        if (DominatedByParent(RightChild(index)))
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
        var temp = this.array[index1];
        this.array[index1] = this.array[index2];
        this.array[index2] = temp;
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