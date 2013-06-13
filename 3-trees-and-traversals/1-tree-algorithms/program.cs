using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    /* 1 You are given a tree of N nodes represented as a set of N-1 pairs of nodes (parent node, child node), each in the range (0..N-1).
    Write a program to read the tree and find:
    a) the root node
    b) all leaf nodes
    c) all middle nodes
    d) the longest path in the tree
    e) * all paths in the tree with given sum S of their nodes
    f) * all subtrees with given sum S of their nodes
    */

    static void Main(string[] args)
    {
        //
        //      3
        //     / \
        //    5   2
        //   /|\   \
        //  0 1 6   4
        
        Console.SetIn(new StringReader(
@"2 4
3 2
5 0
3 5
5 6
5 1
"));


        var tree = new Tree();
        string str;
        while ((str = Console.ReadLine()) != null)
        {
            int[] nums = str.Split(null)
                            .Select(int.Parse)
                            .ToArray();
            tree.AddEdge(nums[0], nums[1]);
        }

        Debug.Assert(tree.Root.Index == 3);

        Debug.Assert(tree.Leaves.SameContents(node => node.Index, new[] { 0, 1, 6, 4 }));

        Debug.Assert(tree.InternalNodes.SameContents(node => node.Index, new[] { 5, 2 }));
        
        var longest = tree.LongestPath.Select(n => n.Index);
        
        Debug.Assert(longest.Count() == 5);
        
        Debug.Assert(longest.Contains(4));
        Debug.Assert(longest.Contains(2));
        Debug.Assert(longest.Contains(3));
        Debug.Assert(longest.Contains(5));
        
        Debug.Assert(longest.Contains(0) ||
                     longest.Contains(1) ||
                     longest.Contains(6));

        Debug.Assert(tree.AllPathsOfSum(6).Count() == 4);
        Debug.Assert(tree.AllPathsOfSum(20).First().SameContents(node => node.Index, new[] { 6, 5, 3, 2, 4 }));

        Debug.Assert(tree.AllSubtreesOfSum(6).SameContents(node => node.Index, new[] { 6, 2 }));



    }
}

