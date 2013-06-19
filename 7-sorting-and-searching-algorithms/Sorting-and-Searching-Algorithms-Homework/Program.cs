namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /*  Open Sorting-and-Searching-Algorithms-Homework.zip and:
        1 Implement SelectionSorter.Sort() method using selection sort algorithm
        2 Implement Quicksorter.Sort() method using quicksort algorithm
        3 Implement MergeSorter.Sort() method using merge sort algorithm
        4 Implement SortableCollection.LinearSearch() method using linear search
        Don't use built-in search methods. Write your own.
        5 Implement SortableCollection.BinarySearch() method using binary search algorithm
        6 Implement SortableCollection.Shuffle() method using shuffle algorithm of your choice
        Document what is the complexity of the algorithm
        7 * Unit test sorting algorithms
        SelectionSorter.Sort()
        Quicksorter.Sort()
        MergeSorter.Sort()
        8 * Unit test searching algorithms
        SortableCollection.LinearSearch()
        SortableCollection.BinarySearch()
     * */
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var collection = new SortableCollection<int>(new[] { 22, 11, 101, 33, 0, 101 });
            Console.WriteLine("All items before sorting:");
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("SelectionSorter result:");
            collection.Sort(new SelectionSorter<int>());
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("Quicksorter result:");
            collection.Sort(new Quicksorter<int>());
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("MergeSorter result:");
            collection.Sort(new MergeSorter<int>());
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("Linear search 101:");
            Console.WriteLine(collection.LinearSearch(101));
            Console.WriteLine();

            Console.WriteLine("Binary search 101:");
            Console.WriteLine(collection.LinearSearch(101));
            Console.WriteLine();

            Console.WriteLine("Shuffle:");
            collection.Shuffle();
            collection.PrintAllItemsOnConsole();
            Console.WriteLine();

            Console.WriteLine("Shuffle again:");
            collection.Shuffle();
            collection.PrintAllItemsOnConsole();
        }
    }
}
