using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

class Program
{
    /* 3 Define classes File { string name, int size } and Folder { string name, File[] files, Folder[] childFolders } 
     * and using them build a tree keeping all files and folders on the hard drive starting from C:\WINDOWS. 
     * Implement a method that calculates the sum of the file sizes in given subtree of the tree and test it accordingly. 
     * Use recursive DFS traversal.
     * */
    static void Main(string[] args)
    {
        var testFolder = Folder.Create("test");
        testFolder.Print(Console.Out);

        Debug.Assert(testFolder.GetSize() == 104);
        Debug.Assert(testFolder.Folders[0].GetSize() == 52);

        // this takes 3-4 minutes on my machine
        // testFolder = Folder.Create("c:\\windows");

        // testFolder.Print(Console.Out);

    }
}