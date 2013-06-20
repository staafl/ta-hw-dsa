using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    const int BOARD_SIDE = 4;
    
    /* 12 * Write a recursive program to solve the "8 Queens Puzzle" with backtracking. 
       Learn more at: http://en.wikipedia.org/wiki/Eight_queens_puzzle
    * */
    static void Main(string[] args)
    {
        Recurse(0, new HashSet<int>(), new HashSet<Tuple<int,int>>());
    }

    static void Recurse(int row,
                        HashSet<int> columns, 
                        HashSet<Tuple<int,int>> cells)
    {
        if (row == BOARD_SIDE)
        {
            PrintSolution(cells);
            Console.WriteLine("Hit enter to continue...");
            Console.ReadLine();
            return;
        }
        
        // every row has exactly one queen
        // we're basically generating a permutation of columns
        
        for (int column = 0; column < BOARD_SIDE; ++column) 
        {
            if (columns.Contains(column))
                continue;
                
            columns.Add(column);
            
            cells.Add(Tuple.Create(row, column));
            
            Recurse(row + 1, columns, cells);
            
            cells.Remove(Tuple.Create(row, column));
            
            columns.Remove(column);
        }
        
    }
    
    static void PrintSolution(IEnumerable<Tuple<int,int>> cells)
    {
        Action header = () => 
        {
            Console.Write(new string(' ', 4));
            
            for (int column = 0; column < BOARD_SIDE; ++column) 
            {
                Console.Write((char)('A' + column));
            }
            Console.WriteLine();
        };
        
        header();

        for (int row = 0; row < BOARD_SIDE; ++row) 
        {
            Console.Write("{0} | ", row + 1);
            for (int column = 0; column < BOARD_SIDE; ++column) 
            {
                if (cells.Contains(Tuple.Create(row, column)))
                    Console.Write("Q");
                else
                    Console.Write(".");
            }
            Console.Write(" | {0}", row + 1);
            Console.WriteLine();
        }
        
        header();
    }
}


