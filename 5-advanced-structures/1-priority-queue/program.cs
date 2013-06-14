using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
class Program
{
    /* 1 Implement a class PriorityQueue<T> based 
     * on the data structure "binary heap".
     * */
    static void Main(string[] args)
    {
        var heap = new Heap<int>();
        heap.Add(1);
        heap.Add(2);
        heap.Add(3);

        Debug.Assert(heap.SameContents(new[] { 1, 2, 3 }));
        Console.WriteLine(string.Join(",", heap));

        Debug.Assert(heap.ChopHead() == 3);
        Debug.Assert(heap.ChopHead() == 2);
        Debug.Assert(heap.ChopHead() == 1);
        Debug.Assert(heap.IsEmpty);

        // higher string means lower priority
        var pqueue = new PriorityQueue<string, string>((s1, s2) => -s1.CompareTo(s2));

        pqueue.Enqueue("18:00", "Buy food");
        pqueue.Enqueue("06:00", "Walk dog");
        pqueue.Enqueue("21:00", "Do homework");
        pqueue.Enqueue("09:00", "Go to work");
        pqueue.Enqueue("21:00", "Drink beer");

        Debug.Assert(pqueue.Count == 5);

        Debug.Assert(pqueue.Dequeue() == "Walk dog");
        Debug.Assert(pqueue.Dequeue() == "Go to work");
        Debug.Assert(pqueue.Dequeue() == "Buy food");
        Debug.Assert(new[] { "Do homework", "Drink beer" }.Contains(pqueue.Dequeue()));
        Debug.Assert(new[] { "Do homework", "Drink beer" }.Contains(pqueue.Dequeue()));

    }
}