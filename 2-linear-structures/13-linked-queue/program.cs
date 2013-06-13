using System;
using System.Collections.Generic;
using System.Linq;

static class Program
{
    // 13 Implement the ADT queue as dynamic linked list. 
    // Use generics (LinkedQueue<T>) to allow storing different 
    // data types in the queue.
    static void Main(string[] args)
    {
        
        var linkedQueue = new LinkedQueue<int>();
        Action show = () => {
            Console.WriteLine("Count: {0}, Elements: {1}", linkedQueue.Count, linkedQueue.PrettyPrint());
        };

        show();
        linkedQueue.Enqueue(1);
        show();
        linkedQueue.Enqueue(2);
        show();
        linkedQueue.Enqueue(3);
        show();
        linkedQueue.Enqueue(4);
        show();
        linkedQueue.Enqueue(5);
        show();
        Console.WriteLine(linkedQueue.Dequeue());
        linkedQueue.Enqueue(6);
        show();
        Console.WriteLine(linkedQueue.Dequeue());
        linkedQueue.Enqueue(7);
        show();
        try
        {
            while (true)
            {
                Console.WriteLine(linkedQueue.Dequeue());
                show();
            }
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Queue is empty!");
            show();
        }
    }

    public static string PrettyPrint<T>(this IEnumerable<T> seq)
    {
        return string.Join(", ", seq);
    }
}
