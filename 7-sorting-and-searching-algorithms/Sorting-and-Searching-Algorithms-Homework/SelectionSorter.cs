namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SelectionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        // time: O(n^2)
        // space: O(1)
        public void Sort(IList<T> collection)
        {
            for (int ii = 0; ii < collection.Count - 1; ++ii)
            {
                int minIndex = ii;
                for (int jj = ii + 1; jj < collection.Count; ++jj)
                {
                    if (collection[jj].CompareTo(collection[minIndex]) < 0)
                    {
                        minIndex = jj;
                    }
                }

                collection.Swap(ii, minIndex);

            }
        }
    }
}
