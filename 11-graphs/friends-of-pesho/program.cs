using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

// no need to define whole classes

// using Adjacency = System.Tuple<int, int>;
// using AdjacencyList = System.Collections.Generic.List<System.Tuple<int, int>>;
// using Graph = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Tuple<int, int>>>;

using Adjacency = Tuple<int, int>;
using AdjacencyList = System.Collections.Generic.List<Tuple<int, int>>;
using Graph = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<Tuple<int, int>>>;



class Program
{
    static void Main(string[] args)
    {
        //if (args.Length != 0)
        //{
        //    Console.SetIn(new StreamReader("..\\..\\tests\\test.010.in.txt"));
        //}

        var split = GetInts();

        int verticeCount = split[0];
        int edgeCount = split[1];
        int hospitalCount = split[2];

        var hospitals = GetInts();

        var graph = new Graph();

        for (int ii = 0; ii < verticeCount; ++ii)
        {
            graph[ii + 1] = new AdjacencyList();
        }

        for (int ii = 0; ii < edgeCount; ++ii)
        {
            split = GetInts();

            graph[split[0]].Add(Tuple.Create(split[1], split[2]));
            graph[split[1]].Add(Tuple.Create(split[0], split[2]));
        }

        int? minTree = null;
        int? minHospital = null;

        // we're looking for the hospital which generates
        // the minimum path sum for each house
        // we modify Dijkstra's algorithm and run it once for each hospital

        foreach (var hospital in hospitals)
        {
            var treeWeight = GetTreeWeight(graph, hospital, verticeCount, hospitals);

            if (treeWeight < minTree || minTree == null)
            {
                minTree = treeWeight;
                minHospital = hospital;
            }
        }

        Console.Write(minTree);
    }

    // a *standard* implementation of Dijkstra's algorithm using a priority queue
    // with a minor modification for the problem

    static int GetTreeWeight(Graph graph, int hospital, int verticeCount, IEnumerable<int> hospitals)
    {
        var distances = new MinHeap<int>();

        foreach (var adj in graph[hospital])
        {
            distances.TrySetPriority(adj.Item2, adj.Item1);
        }   

        // return value
        int ret = 0;

        var tree = new HashSet<int>();
        int housesAdded = 0;
        int totalHouses = verticeCount - hospitals.Count();

        while (housesAdded < totalHouses && distances.Count > 0)
        {
            // edge nearest to 'hospital'

            var min = distances.ChopHeadWithPriority();
            var weight = min.Item1;

            // the new vertex in the tree

            var v1 = min.Item2;

            // modification of algorithm:
            // sum the distance to the root of all nodes that aren't
            // hospitals

            if (!hospitals.Contains(v1))
            {
                housesAdded += 1;
                ret += weight;
            }

            tree.Add(v1);

            // update the priorities of all external neighbours of the vertex
            // we've just added

            foreach (var adj in graph[v1])
            {
                var v2 = adj.Item1;
                if (v2 == hospital)
                    continue;
                if (tree.Contains(v2))
                {
                    continue;
                }

                // perhaps improve priority
                distances.TrySetPriority(weight + adj.Item2, v2, false);

            }
        }

        return ret;
    }

    static int[] GetInts()
    {
        return Console.ReadLine()
                      .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(int.Parse)
                      .ToArray();
    }
}


















