using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    /* 7 We are given a matrix of passable and non-passable cells. 
       Write a recursive program for finding all paths between two 
       cells in the matrix.
    * */
    static void Main(string[] args)
    {
        int rowA, rowB, columnA, columnB;
        
        bool[,] matrix = GetMatrix(out rowA, out columnA, out rowB, out columnB);

        Recurse(rowA, columnA, 
                rowB, columnB, 
                new HashSet<Tuple<int, int>>(), 
                matrix, 
                new Stack<string>());
    }
    
    static bool[,] GetMatrix(out int rowA, out int columnA, out int rowB, out int columnB)
    {
        rowA = columnA = rowB = columnB = 0;
        
        string[] asStrings = new[]{ "##########",
                                    "##A....###",
                                    "##.###.###",
                                    "##.##.B###",
                                    "##.##.####",
                                    "##....####",
                                    "##########",
                                    "##########",
                                    "##########",
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
                if (cell == 'A')
                {
                    rowA = row;
                    columnA = column;
                }
                else if (cell == 'B')
                {
                    rowB = row;
                    columnB = column;
                }
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
                        int rowB, int columnB, 
                        HashSet<Tuple<int, int>> visited,
                        bool[,] matrix, 
                        Stack<string> stack)
    {
        visited.Add(Tuple.Create(row, column));
        stack.Push(string.Format("[{0}:{1}]", row, column));

        if (row == rowB &&
            column == columnB)
        {
            Console.Write("(");
            Console.Write(string.Join(" ", stack.Reverse()));
            Console.WriteLine(")");
        }
        else
        {
            foreach (var cell in GetPassableNeighbours(row, column, matrix))
            {
                if (visited.Contains(cell))
                    continue;
                    
                Recurse(cell.Item1, cell.Item2, rowB, columnB, visited, matrix, stack);
            }
        }

        visited.Remove(Tuple.Create(row, column));
        stack.Pop();

    }
}



















