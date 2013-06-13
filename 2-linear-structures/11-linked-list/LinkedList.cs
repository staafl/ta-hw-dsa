using System;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    // The assignment doesn't explicitly state it, but I assume
    // we are talking about a simple singly-linked list, not a doubly-linked,
    // skiplist, or any other variety.
    
    // A small optimization here is keeping the last node in a field ('tail'), 
    // which improves appending from O(n) to O(1).
    
    public ListItem<T> First { get { return head.Next; } }
    
    // sentinel value for head of list
    // simplifies code in Add() and Remove()
    readonly ListItem<T> head = new ListItem<T>(default(T));
    ListItem<T> tail;
    
    public LinkedList()
    {
        this.tail = head;

    }

    public int Count { get; private set; }

    public bool RemoveAt(int index)
    {
        return Remove((_,i) => i == index, false) > 0;
    }
    
    public bool Remove(T elem)
    {
        return Remove((e,_) => e == null ? elem == null : e.Equals(elem), false) > 0;
    }
    
    public int RemoveAll(T elem)
    {
        return Remove((e,_) => e == null ? elem == null : e.Equals(elem), true);
    }
    
    public int Remove(Func<T, int, bool> predicate, bool removeAll)
    {
        int index = -1;
        int removed = 0;
        var previous = this.head;
        var current = this.head.Next;
        
        while (current != null) 
        {
            index += 1;
            if (predicate(current.Value, index))
            {
                previous.Next = current.Next;
                removed += 1;
                this.Count -= 1;

                if (current == this.tail)
                {
                    this.tail = previous;
                }

                if (!removeAll)
                {
                    break;
                }
                current = current.Next;
                
            }
            else
            {
                previous = current;
                current = current.Next;
            }

        }
        
        return removed;
    }

    public void Add(params T[] values)
    {
        Add(values as IEnumerable<T>);
    }
    public void Add(IEnumerable<T> values)
    {
        foreach(T value in values)
        {
            this.Count += 1;
            
            var newItem = new ListItem<T>(value);
            
            //* with optimization
            this.tail.Next = newItem;
            this.tail = newItem;
            
            /*/ without optimization:
            var previous = this.head;
            var current = this.head.Next;
            
            while (current != null) 
            {
                previous = current;
                current = current.Next;
            }
            
            previous.Next = newItem;
            //*/
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.First;
        while (current != null) 
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    


    

}
