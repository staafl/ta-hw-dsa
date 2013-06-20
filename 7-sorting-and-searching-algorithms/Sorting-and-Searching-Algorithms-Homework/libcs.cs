using System;
using System.Collections.Generic;
using System.Linq;

public static class libcs
{
    public static bool IsSorted<T>(this IEnumerable<T> seq)
    {
        return seq.OrderBy(_ => _, Comparer<T>.Default)
                  .SequenceEqual(seq);
    }

    public static int CompareTo<T>(this T value1, T value2)
    {
        return Comparer<T>.Default.Compare(value1, value2);
    }

    public static void Swap<T>(this IList<T> collection, int index1, int index2)
    {
        var temp = collection[index1];
        collection[index1] = collection[index2];
        collection[index2] = temp;
    }

    public static bool SafeEquals<T>(this T what, T whom)
    {
        if (what == null)
            return whom == null;
        return what.Equals(whom);
    }

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> seq)
    {
        foreach (var elem in seq)
            collection.Add(elem);
    }

    public static void Swap<T>(ref T v1, ref T v2)
    {
        var temp = v1;
        v1 = v2;
        v2 = temp;
    }

    public static bool SameContents<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
    {
        return seq1.OrderBy(t => t, Comparer<T>.Default).SequenceEqual(
               seq2.OrderBy(t => t, Comparer<T>.Default));
    }

    public static bool SameContents<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, Func<T, int> f)
    {
        return seq1.OrderBy(f).SequenceEqual(
               seq2.OrderBy(f));
    }

    public static void ThrowIfNegative(this int num, string msg = null)
    {
        if (num < 0)
        {
            throw GetException<ArgumentOutOfRangeException>(msg ?? "Argument must be nonnegative, got: " + num);
        }
    }

    public static void ThrowIfNull<T>(this T obj, string msg = null)
    {
        if (obj == null)
        {
            throw GetException<ArgumentNullException>(msg ?? "Argument cannot be null.");
        }
    }

    public static void ThrowIfNull<T, TE>(this T obj, string msg = null) where TE : Exception
    {
        if (obj == null)
        {
            throw GetException<TE>(msg ?? "Value cannot be null.");
        }
    }

    public static void ThrowIfNotDefined<TEnum>(this TEnum enumValue, string msg = null)
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            throw GetException<ArgumentException>(msg ?? "Invalid enum value: " + enumValue);
        }
    }

    public static void ThrowIfNotDefined<TEnum, TE>(this TEnum enumValue, string msg = null)
        where TE : Exception
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            throw GetException<TE>(msg ?? "Invalid enum value: " + enumValue);
        }
    }

    public static TE GetException<TE>(string msg, Exception innerException = null) where TE : Exception
    {

        var ex = Activator.CreateInstance(typeof(TE), new object[] { msg, innerException });

        return (TE)ex;

    }

    public static V GetOrDefault<K, V>(this IDictionary<K, V> dict, K key, V defValue)
    {
        V ret;
        if (dict.TryGetValue(key, out ret))
            return ret;
        return defValue;
    }
}

















// 2013-06-13
