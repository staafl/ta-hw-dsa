using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    /* 
       9 Write a recursive program to find the largest connected area of adjacent empty cells in a matrix.
       10 * We are given a matrix of passable and non-passable cells. 
       Write a recursive program for finding all areas of passable cells in the matrix.
    * */
    static void Main(string[] args)
    {
        // problem 9
        
        Console.WriteLine("Maximum area(s):");
        FindAreas(true);
        
        // problem 10
        
        Console.WriteLine("All areas:");
        FindAreas(false);
    }
    
    static void FindAreas(bool largestOnly) 
    {
        bool[,] matrix = GetMatrix();
        
        var allVisited = new HashSet<Tuple<int, int>>();
        
        var areas = new List<IEnumerable<Tuple<int,int>>>();
        
        for (int row = 0; row < matrix.GetLength(0); ++row) 
        {
            for (int column = 0; column < matrix.GetLength(1); ++column) 
            {
                if (allVisited.Contains(Tuple.Create(row, column)))
                    continue;
                    
                if (!matrix[row, column])
                    continue;
                    
                var visited = new HashSet<Tuple<int, int>>();
                
                Recurse(row, column, 
                        visited, 
                        matrix);
                        
                areas.Add(visited);
                
                foreach (var elem in visited)
                    allVisited.Add(elem);
            }
        }
        
        if (largestOnly) 
        {   
            foreach (var area in areas.Where(a => a.Count() == areas.Max(b => b.Count())))
                PrintArea(matrix, area);
                
        }
        else 
        {
            foreach (var area in areas)
                PrintArea(matrix, area);
        }

    }
    static bool[,] GetMatrix()
    {
        string[] asStrings = new[]{ "##########",
                                    "##.....###",
                                    "##.###.###",
                                    "##.##..###",
                                    "##.##.####",
                                    "##....####",
                                    "########.#",
                                    "#.##..####",
                                    "####..####",
                                    "##########" };

        int rows = asStrings.Length;
        int columns = asStrings[0].Length;
        
        var ret = new bool[rows, columns];
        
        for (int row = 0; row < rows; ++row) 
        {
            for (int column = 0; column < columns; ++column) 
            {
                char cell = asStrings[row][column];
                ret[row, column] = (cell != '#');
            }
        }
        
        return ret;
    }
    
    static IEnumerable<Tuple<int, int>> GetPassableNeighbours(
        int row, int column, bool[,] matrix)
    {
        for (int ii = -1; ii <= 1; ++ii) 
        {
            for (int jj = -1; jj <= 1; ++jj)
            {
                if ((ii == 0) == (jj == 0))
                    continue;
                    
                int otherRow = row + ii;
                int otherColumn = column + jj;
                
                if (otherRow < 0 || otherRow >= matrix.GetLength(0))
                    continue;
                    
                if (otherColumn < 0 || otherColumn >= matrix.GetLength(1))
                    continue;
                    
                if (matrix[otherRow, otherColumn])
                    yield return Tuple.Create(otherRow, otherColumn);
            }
        }
    }
    
    static void Recurse(int row, int column, 
                        HashSet<Tuple<int, int>> visited,
                        bool[,] matrix)
    {
        visited.Add(Tuple.Create(row, column));

        var neighbours = GetPassableNeighbours(row, column, matrix);
        
        foreach (var cell in neighbours)
        {
            if (visited.Contains(cell))
                continue;
                
            Recurse(cell.Item1, cell.Item2, visited, matrix);
        }

    }
    
    static void PrintArea(bool[,] matrix, IEnumerable<Tuple<int,int>> area)
    {
        for (int row = 0; row < matrix.GetLength(0); ++row) 
        {
            for (int column = 0; column < matrix.GetLength(1); ++column) 
            {
                Console.Write(area.Contains(Tuple.Create(row, column)) ? "." : "#");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}



















