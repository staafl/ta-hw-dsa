using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class HashTable<K, T> : IEnumerable<KeyValuePair<K, T>>
{
    public enum DuplicateHandling
    {
        Overwrite,
        LeaveOld,
        ThrowException
    }

    const int DEF_CAPACITY = 16;

    LinkedList<KeyValuePair<K, T>>[] array;

    public HashTable(int capacity = DEF_CAPACITY)
    {
        capacity.ThrowIfNegative();

        this.array = new LinkedList<KeyValuePair<K, T>>[capacity];
    }

    // iteration

    public IEnumerable<K> Keys
    {
        get
        {
            foreach (var kvp in this)
                yield return kvp.Key;
        }
    }

    static IEnumerable<T1> Enumerate<T1>(IEnumerable<T1>[] array)
    {
        foreach (var seq in array)
        {
            if (seq != null)
            {
                foreach (var elem in seq)
                    yield return elem;
            }
        }
    }

    public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
    {
        return Enumerate(this.array).GetEnumerator();

        /*foreach (var list in array) 
        {
            if (list != null) 
            {
                foreach (var kvp in list)
                    yield return kvp;
            }
        }*/
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }


    // storage management

    public int Capacity
    {
        get
        {
            return array.Length;
        }
    }

    public int Count
    {
        get;
        private set;
    }

    public void Clear()
    {
        Array.Clear(array, 0, this.Capacity);
        this.Count = 0;
    }


    public bool MaybeResize()
    {
        if ((this.Count * 4) / 3 < this.Capacity)
            return false;

        var oldArray = this.array;
        this.array = new LinkedList<KeyValuePair<K, T>>[this.Capacity * 2];
        this.Count = 0;

        foreach (var kvp in Enumerate(oldArray))
        {
            var temp = this.Add(kvp.Key, kvp.Value, DuplicateHandling.Overwrite);
            Debug.Assert(temp);
        }
        return true;
    }

    public int OccupiedBuckets
    {
        get
        {
            return Enumerable.Range(0, this.Capacity).Where(_ix => array[_ix] != null).Count();
        }
    }
    public int FreeBuckets
    {
        get
        {
            return Enumerable.Range(0, this.Capacity).Where(_ix => array[_ix] == null).Count();
        }
    }

    // element access

    public bool ContainsKey(K key)
    {
        key.ThrowIfNull();
        T value;
        return this.TryGetValue(key, out value);
    }

    public bool Add(K key, T value, DuplicateHandling dh = DuplicateHandling.Overwrite)
    {

        key.ThrowIfNull();
        dh.ThrowIfNotDefined();

        bool ret = true;

        var list = this.array[this.GetIndex(key)];

        if (list == null)
        {
            list = new LinkedList<KeyValuePair<K, T>>();

            array[this.GetIndex(key)] = list;

            this.Count += 1;
        }
        else
        {
            var existing = list.Cast<KeyValuePair<K, T>?>().FirstOrDefault(kvp => kvp.Value.Key.Equals(key));

            if (existing == null)
            {
                this.Count += 1;
            }
            else
            {
                if (dh == DuplicateHandling.Overwrite)
                {
                    ret = false;
                    list.Remove(existing.Value);
                }
                else if (dh == DuplicateHandling.LeaveOld)
                {
                    return false;
                }
                else if (dh == DuplicateHandling.ThrowException)
                {
                    throw new InvalidOperationException("Key already present in dictionary: " + key);
                }
            }
        }

        list.AddLast(new KeyValuePair<K, T>(key, value));

        if (ret)
            this.MaybeResize();

        return ret;

    }

    public T GetOrDefault(K key, T defValue)
    {
        T value;
        if (!TryGetValue(key, out value))
            value = defValue;
        return value;
    }

    public bool TryGetValue(K key, out T value)
    {

        key.ThrowIfNull();

        value = default(T);

        var list = this.array[this.GetIndex(key)];

        if (list == null)
            return false;

        foreach (var kvp in list)
        {
            if (kvp.Key.Equals(key))
            {
                value = kvp.Value;
                return true;
            }
        }

        return false;

    }

    public T this[K key]
    {
        get
        {
            key.ThrowIfNull();
            T value;
            if (!this.TryGetValue(key, out value))
                throw new KeyNotFoundException("Key not found in the dictionary: " + key);
            return value;
        }
        set
        {
            key.ThrowIfNull();
            Add(key, value, DuplicateHandling.Overwrite);
        }
    }

    public bool Remove(K key)
    {

        var list = this.array[this.GetIndex(key)];

        if (list == null)
            return false;

        var kvp = list.Cast<KeyValuePair<K, T>?>().FirstOrDefault(_kvp => _kvp.Value.Key.Equals(key));

        if (kvp == null)
            return false;

        this.Count -= 1;
        list.Remove(kvp.Value);

        if (list.Count == 0)
        {
            this.array[this.GetIndex(key)] = null;
        }

        return true;

    }

    int GetIndex(K key)
    {
        return Math.Abs(key.GetHashCode() % this.Capacity);
    }

}

















