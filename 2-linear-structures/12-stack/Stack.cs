using System;
using System.Collections.Generic;

class Stack<T> : IEnumerable<T>
{
    T[] elements;

    int head = -1;

    public Stack(int capacity = 0)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity must be non-negative: " + capacity);
        }
        elements = new T[capacity];
    }

    public int Count
    {
        get { return this.head + 1; }
    }
    
    public int Capacity
    {
        get { return this.elements.Length; }
    }

    public void Push(T value)
    {
        this.head += 1;

        if (this.elements.Length < this.head + 1)
            this.EnsureCapacity(this.head + 1);

        this.elements[this.head] = value;
    }

    private void EnsureCapacity(int size)
    {
        if (size <= this.elements.Length)
            return;

        // next power of two after 'size'
        int newSize = (int)Math.Ceiling(Math.Pow(2, Math.Floor(Math.Log(size)/Math.Log(2)) + 1));

        Array.Resize(ref this.elements, newSize);
    }

    public T Peek()
    {
        ThrowIfEmpty();

        return this.elements[this.head];
    }

    void ThrowIfEmpty()
    {
        if (this.head == -1)
            throw new InvalidOperationException("Stack is empty.");
    }
    public T Pop()
    {
        ThrowIfEmpty();

        T value = this.elements[this.head];

        // release reference to the value to allow GC
        this.elements[this.head] = default(T);

        this.head -= 1;

        return value;
    }

    public void Clear()
    {
        this.head = -1;
        Array.Clear(this.elements, 0, this.elements.Length);
    }

    public void TrimExcess()
    {
        Array.Resize(ref this.elements, this.Count);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
            yield return this.elements[i];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public override string ToString()
    {
        return string.Join(" ", this);
    }
}