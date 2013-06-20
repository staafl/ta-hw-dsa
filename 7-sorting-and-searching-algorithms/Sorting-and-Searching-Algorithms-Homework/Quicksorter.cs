namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QuickSorter<T> : ISorter<T> where T : IComparable<T>
    {
        // A simple Quicksort implementation using a 'median of sorted three' pivot strategy to avoid pathological cases
        // (see http://stackoverflow.com/questions/7559608/median-of-three-values-strategy) and a simple optimization for
        // minimizing the recursive depth.
        // I have chosen to use 'meet-in-the-middle' in-place value-swapping approach for clarity, since the time complexity
        // remains O(n*log n) and all we save is O(n*log n) auxiliary space. Once you start optimizing Quicksort, there really is
        // no end to it :-)
        // time: O(n*log n)
        // space: O(n*log n)
        public void Sort(IList<T> collection)
        {
            collection.ThrowIfNull();

            if (collection.Count < 2)
                return;

            // find median pivot and bubble-sort first/middle/last elements

            var copy = collection.ToArray();

            var first = collection[0];
            var middle = collection[collection.Count / 2];
            var last = collection[collection.Count - 1];

            if (first.CompareTo(middle) > 0)
            {
                libcs.Swap(ref first, ref middle);
            }
            if (first.CompareTo(last) > 0)
            {
                libcs.Swap(ref first, ref last);
            }
            if (middle.CompareTo(last) > 0)
            {
                libcs.Swap(ref middle, ref last);
            }

            collection[0] = first;
            collection[collection.Count / 2] = middle;
            collection[collection.Count - 1] = last;

            if (collection.Count <= 3)
            {
                return;
            }

            // split list around pivot

            var pivot = middle;

            var left = new List<T>();
            var right = new List<T>();

            foreach (var elem in collection)
            {
                if (elem.CompareTo(pivot) < 0)
                {
                    left.Add(elem);
                }
                else
                {
                    right.Add(elem);
                }
            }

            // deal with shorter list first to minimize call depth

            if (left.Count > right.Count)
            {
                Sort(right);
                Sort(left);
            }
            else
            {
                Sort(left);
                Sort(right);
            }

            collection.Clear();
            collection.AddRange(left);
            collection.AddRange(right);
        }
    }
}
