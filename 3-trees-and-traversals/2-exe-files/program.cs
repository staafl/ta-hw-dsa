using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
class Program
{
    /*2 Write a program to traverse the directory C:\WINDOWS and all its subdirectories recursively and 
     * to display all files matching the mask *.exe. Use the class System.IO.Directory.
     * */

    static void Main(string[] args)
    {
        ListExes("test");
        ListExes("c:\\windows");
    }

    static void ListExes(string directory)
    {
        if (directory.ToLower().Contains("windows\\assembly"))
            // ignore the GAC pseudo-directory
            return;
        if (directory.ToLower().Contains("windows\\winsxs"))
            // ignore the side-by-side executable cache
            return;

        try
        {
            foreach (var file in Directory.GetFiles(directory, "*.exe"))
                Console.WriteLine(file);

            foreach (var subDir in Directory.GetDirectories(directory))
                ListExes(subDir);
        }
        catch (UnauthorizedAccessException)
        { }
    }
}