namespace SortingHomework
{
    using System;
    using System.Collections.Generic;

    public class SortableCollection<T> where T : IComparable<T>
    {
        private readonly IList<T> items;

        public SortableCollection()
        {
            this.items = new List<T>();
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.items = new List<T>(items);
        }

        public IList<T> Items
        {
            get
            {
                return this.items;
            }
        }

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.items);
        }

        // time: O(n)
        // space: O(1)
        public bool LinearSearch(T item)
        {
            foreach (var elem in this.items)
                if (elem.SafeEquals(item))
                    return true;
            return false;
        }

        // time: O(log n)
        // space: O(1)
        public bool BinarySearch(T item)
        {
            var len = items.Count;
            if (len == 0)
                return false;
            var start = 0;

            var comparer = Comparer<T>.Default;

            while (true)
            {
                var split = len / 2 + start;
                var atSplit = this.items[split];
                if (atSplit.SafeEquals(item))
                {
                    return true;
                }

                if (comparer.Compare(atSplit, item) < 0)
                {
                    len = len - (split - start + 1);
                    start = split + 1;
                }
                else
                {
                    len = split - start;
                }

                if (len < 1)
                {
                    return false;
                }

            }

        }

        // take to class scope to avoid duplicate-seed issues when used 
        // in a loop etc
        static readonly Random rand = new Random();

        // time: O(n)
        // space: O(1)
        public void Shuffle()
        {
            // http://en.wikipedia.org/wiki/Fisher–Yates_shuffle

            // for element X, choose position [X..length) and swap

            for (int ii = 0; ii < this.items.Count; ++ii)
            {
                var position = rand.Next(ii, this.items.Count);
                this.items.Swap(ii, position);

                // note that this is vulnerable to the entropy of the random generator
                // only a maximum of 12 items can be shuffled uniformly using a 32 bit seed
            }
        }

        public void PrintAllItemsOnConsole()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                if (i == 0)
                {
                    Console.Write(this.items[i]);
                }
                else
                {
                    Console.Write(" " + this.items[i]);
                }
            }

            Console.WriteLine();
        }
    }
}
