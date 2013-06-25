using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int start = int.Parse(Console.ReadLine());
        int target = int.Parse(Console.ReadLine());
        
        int forbiddenCount = int.Parse(Console.ReadLine());
        
        bool[] forbidden = new bool[MAX];
        
        for (int ii = 0; ii < forbiddenCount; ++ii)
        {
            forbidden[int.Parse(Console.ReadLine())] = true;
        }
        
        int value = Solve(start, target, forbidden);

        Console.WriteLine(value);
    }
    
    static readonly int[] powers = new[]{1,10,100,1000,10000,100000};
    
    static int Rotate(bool left, int wheel, int combo) 
    {
        // example:
        // wheel: 2
        //          V
        // combo: 12345
        
        int leading = combo / powers[wheel]; // 123
        int trailing = combo % powers[wheel];
        
        int digit = leading % 10; // 3
        digit += (left ? 1 : 9);
        digit %= 10;
        
        int newLeading = (leading/10)*10 + digit;
        int newCombo = newLeading * powers[wheel] + trailing;
        
        return newCombo;
    }
    
    const int MAX = 100*1000;

    static int Solve(int start, int target, bool[] forbidden)
    {
        var queue = new Queue<int>();
        queue.Enqueue(start);
        
        int firstAtLevel = start;
        bool hangingFirst = false;
        int level = -1;
        
        bool[] seen = new bool[MAX];
        seen[start] = true;
        
        while(queue.Count > 0)
        {
            var now = queue.Dequeue();
            
            seen[now] = true;
            bool isFirst = (firstAtLevel == now);
            if (isFirst)
            {
                hangingFirst = true;
                level += 1;
            }
            
            if (now == target)
                return level;

            for (bool left = true; ; left = !left)
            {
                for (int ii = 0; ii < 5; ++ii)
                {
                    var next = Rotate(left, ii, now);
                    if (!forbidden[next] && !seen[next])
                    {
                        seen[next] = true;
                        queue.Enqueue(next);
                        if (hangingFirst)
                        {
                            hangingFirst = false;
                            firstAtLevel = next;
                        }
                    }
                }
                if (!left)
                    break;
            }
            
        }
        return -1;
        
    }

    
}