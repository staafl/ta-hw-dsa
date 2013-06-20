namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {

        public void Sort(IList<T> collection)
        {
            MergeSorter<T>.Sort(collection, null);
        }

        // My own somewhat optimized version of MergeSort.
        // FastMerge uses only half the auxiliary space the naive solution would, and
        // since we're using the same buffer in all calls, we reduce the required space
        // from O(n*log n) to O(n).
        // time: O(n*log n)
        // space: O(n)
        public static void Sort(IList<T> collection, IComparer<T> cmp = null)
        {
            cmp = cmp ?? Comparer<T>.Default;

            int cnt = collection.Count;

            if (cnt == 0 || cnt == 1)
                return;

            T[] buffer = new T[(cnt + 1) / 2];

            T[] array = collection.ToArray();

            MergeSortInner(array, 0, cnt, buffer, cmp);

            collection.Clear();
            collection.AddRange(array);

        }

        // Merges the sorted array sections array[left..middle) and array[middle..right)
        // into array[left..right)
        // using left_buf as temporary storage
        static void FastMerge(T[] array,
                                  int left,
                                  int middle,
                                  int right,
                                  T[] left_buf,
                                  IComparer<T> cmp)
        {

            int left_cnt = middle - left;

            Array.Copy(array, left, left_buf, 0, left_cnt);

            // We are using the (middle, right] slice of the original array as 
            // an additional buffer

            // how far we are in the sorted section
            int pos = left;

            // how many elements we have chopped from [left..middle)
            int left_head = 0;

            // how many elements we have chopped from [middle..right)
            int right_head = middle;

            while ((left_head < left_cnt) && (right_head < right))
            {
                var left_item = left_buf[left_head];
                var right_item = array[right_head];

                if (cmp.Compare(left_item, right_item) != 1)
                {
                    array[pos] = left_item;
                    ++left_head;
                }
                else
                {
                    array[pos] = right_item;
                    ++right_head;
                }

                ++pos;
            }

            while (left_head < left_cnt)
                array[pos++] = left_buf[left_head++];

        }

        static void MergeSortInner(T[] array,
                                        int left,
                                        int right,
                                        T[] buffer,
                                        IComparer<T> cmp)
        {

            int cnt = right - left;

            if (cnt < 2)
                return;

            var middle = (right + left) / 2;

            MergeSortInner(array, left, middle, buffer, cmp);
            MergeSortInner(array, middle, right, buffer, cmp);

            FastMerge(array, left, middle, right, buffer, cmp);
        }



    }
}
