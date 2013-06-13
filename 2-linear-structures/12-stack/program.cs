using System;
using System.Collections.Generic;
using System.Linq;

static class Program
{
    // 12 Implement the ADT stack as auto-resizable array. 
    // Resize the capacity on demand (when no space is available to add / 
    // insert a new element).
    static void Main(string[] args) 
    {
        var stack = new Stack<int>();
        
        Action show = () =>
        {
            Console.WriteLine("Capacity: {0}, Count: {1}, Elements: {2}", stack.Capacity, stack.Count, stack.PrettyPrint());
        };
        show();
        stack.Push(1);
        show();
        stack.Push(2);
        show();
        stack.Push(3);
        show();
        stack.Push(4);
        show();
        stack.Push(4);
        show();
        stack.Push(5);
        show();
        try
        {
            while (true)
            {
                Console.WriteLine(stack.Pop());
                show();
            }
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Stack is empty!");
            show();
        }
    }
    
    public static string PrettyPrint<T>(this IEnumerable<T> seq)
    {
        return string.Join(", ", seq);
    }
}
