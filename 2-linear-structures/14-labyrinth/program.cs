using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
class Program
{
    // 14 * We are given a labyrinth of size N x N. 
    // Some of its cells are empty (0) and some are full (x). 
    // We can move from an empty cell to another empty cell if they 
    // share common wall. Given a starting position (*) calculate and fill in 
    // the array the minimal distance from this position to any other cell in 
    // the array. Use "u" for all unreachable cells. 
    //
    // Example:
    // 0    0    0    x    0    x    
    // 0    x    0    x    0    x    
    // 0    *    x    0    x    0    
    // 0    x    0    0    0    0    
    // 0    0    0    x    x    0    
    // 0    0    0    x    0    x    
    // =========================
    // 
    // 3    4    5    x    u    x    
    // 2    x    6    x    u    x    
    // 1    *    x    8    x    10    
    // 2    x    6    7    8    9    
    // 3    4    5    x    x    10    
    // 4    5    6    x    u    x    

    static void Main(string[] args) 
    {
        // simple one-time BFS traversal
        
        var labyrinth = new List<string[]>();
        Console.WriteLine("Reading labyrinth from 'labyrinth.txt':");
        
        string input;
        using (var sr = new StreamReader("labyrinth.txt"))
        while ((input = sr.ReadLine()) != null)
        {
            labyrinth.Add(input.Split(null));
        }
        
        Print(labyrinth);
        
        Console.WriteLine("Enter start row:");
        int startRow = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Enter start column:");
        int startColumn = int.Parse(Console.ReadLine());
        
        labyrinth[startRow][startColumn] = "*";
        
        Print(labyrinth);
        
        var directions = new []{Tuple.Create(-1,0),Tuple.Create(1,0),
                                Tuple.Create(0,-1),Tuple.Create(0,1)};
        
        // row, column, distance
        var queue = new Queue<Tuple<int,int,int>>();
        
        queue.Enqueue(Tuple.Create(startRow, startColumn, 0));
        
        while (queue.Count > 0)
        {
            var triple = queue.Dequeue();
            var distance = triple.Item3;
            
            foreach (var dir in directions)
            {
                var newRow = triple.Item1 + dir.Item1;
                var newColumn = triple.Item2 + dir.Item2;
                
                if (newRow < 0 || newRow >= labyrinth.Count)
                    continue;
                
                if (newColumn < 0 || newColumn >= labyrinth[0].Length)
                    continue;
                    
                string cellContents = labyrinth[newRow][newColumn];
                
                int cellContentsInt;
                
                if (!int.TryParse(cellContents, out cellContentsInt))
                    continue;
                
                if (cellContentsInt != 0 && cellContentsInt < distance + 1)
                    continue;
                    
                labyrinth[newRow][newColumn] = (distance + 1).ToString();
                
                queue.Enqueue(Tuple.Create(newRow, newColumn, distance + 1));

            }
        }
        
        for(int row = 0; row < labyrinth.Count; ++row) 
        {
            for(int column = 0; column < labyrinth[0].Length; ++column) 
            {
                if (labyrinth[row][column] == "0")
                {
                    labyrinth[row][column] = "u";
                }
            }
        }
        
        Print(labyrinth);
        
    }
    
    static void Print(List<string[]> labyrinth) 
    {
        Console.WriteLine();
        for(int row = 0; row < labyrinth.Count; ++row) 
        {
            for(int column = 0; column < labyrinth[0].Length; ++column) 
            {
                Console.Write(labyrinth[row][column].ToString().PadRight(6, ' '));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}























