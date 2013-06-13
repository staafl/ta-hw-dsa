using System;
using System.Collections.Generic;
using System.Linq;

public class HashedSet<T> : IEnumerable<T>
{
    readonly HashTable<T, bool> ht = new HashTable<T, bool>();

    public bool Add(T elem)
    {
        return ht.Add(elem, true);
    }

    public bool Remove(T elem)
    {
        return ht.Remove(elem);
    }

    public bool Find(T elem)
    {
        return ht.ContainsKey(elem);
    }

    public int Count
    {
        get
        {
            return ht.Count;
        }
    }

    public void Clear()
    {
        ht.Clear();
    }

    public HashedSet<T> Intersect(HashedSet<T> other)
    {

        var ret = new HashedSet<T>();

        foreach (var elem in this)
            if (other.Find(elem))
                ret.Add(elem);

        return ret;

    }

    public HashedSet<T> Union(HashedSet<T> other)
    {

        var ret = new HashedSet<T>();

        foreach (var elem in this)
            ret.Add(elem);

        foreach (var elem in other)
            ret.Add(elem);

        return ret;

    }


    public IEnumerator<T> GetEnumerator()
    {
        return ht.Keys.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}