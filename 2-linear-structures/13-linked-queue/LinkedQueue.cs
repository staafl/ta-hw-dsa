using System;
using System.Collections.Generic;

class LinkedQueue<T> : IEnumerable<T>
{
    // using linked list from problem 11

    readonly LinkedList<T> list = new LinkedList<T>();

    public int Count { get { return list.Count; } }

    public T Dequeue()
    {
        ThrowIfEmpty();

        T ret = list.First.Value;
        list.RemoveAt(0);

        return ret;

    }

    public void Enqueue(T elem)
    {
        list.Add(elem);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return (list as System.Collections.IEnumerable).GetEnumerator();
    }

    void ThrowIfEmpty()
    {
        if (this.Count == 0)
            throw new InvalidOperationException("Queue is empty!");
    }
}