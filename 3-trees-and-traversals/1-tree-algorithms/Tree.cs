using System;
using System.Collections.Generic;
using System.Linq;

class Tree
{
    readonly Dictionary<int, Node> nodes = new Dictionary<int, Node>();

    public void AddEdge(int parentIndex, int childIndex)
    {
        var child = GetOrMakeNode(childIndex);

        if (child.Parent != null)
        {
            throw new InvalidOperationException("Child already has parent!");
        }

        var parent = GetOrMakeNode(parentIndex);

        child.Parent = parent;
        parent.Children.Add(child);
    }

    // O(n)
    public Node Root
    {
        get
        {
            return nodes.Values.Where(n => n.Parent == null).Single();
        }
    }

    // O(n)
    public IEnumerable<Node> Leaves
    {
        get
        {
            // we could do this with recursive traversal
            // but it would be O(n) as well, so why bother?
            return nodes.Values.Where(n => n.Children.Count == 0);
        }
    }

    // O(n)
    public IEnumerable<Node> Nodes
    {
        get
        {
            return nodes.Values;
        }
    }

    // O(n)
    public IEnumerable<Node> InternalNodes
    {
        get
        {
            return nodes.Values.Where(n => n.Parent != null && n.Children.Count != 0);
        }
    }

    // O(n)
    public IEnumerable<Node> LongestPath
    {
        get
        {
            // I'm assuming the assignment asks for longest path
            // between any two nodes.
            // This is usually the longest path between two leaves,
            // but may occasionally be the longest path between a leaf
            // and the root, if the root has many single-child descendants:
            // leaf-leaf: 5-4-1-0-2
            //       0
            //      / \
            //     1   2
            //    / \
            //   3   4
            //      /
            //     5
            // leaf-root: 5-3-2-1-0
            //       0
            //        \
            //         1
            //          \
            //           2
            //          / \
            //         3   4
            //        /
            //       5
            // solution:
            // * 
            int temp1, temp2;
            List<Node> temp3 = new List<Node>();
            return DiameterThroughNode(this.Root, out temp1, out temp2, out temp3);
        }
    }

    IEnumerable<Node> DiameterThroughNode(Node node, out int diameter, out int height, out List<Node> pathFromLeaf)
    {
        var justNode = new[] { node };

        if (node.Children.Count == 0)
        {
            diameter = 0;
            height = 1;
            pathFromLeaf = new List<Node> { node };
            return justNode;
        }

        if (node.Children.Count == 1)
        {
            int childDiameter;
            int childHeight;
            var childPath = DiameterThroughNode(node.Children.First(), 
                                    out childDiameter, 
                                    out childHeight, 
                                    out pathFromLeaf);
                                    
            height = 1 + childHeight;
            pathFromLeaf.Add(node);
            if (childHeight > childDiameter)
            {
                diameter = childHeight;
                return childPath.Concat(justNode);
            }
            else
            {
                diameter = childDiameter;
                return childPath;
            }
        }

        var childDiameters = new Dictionary<Node, int>();
        var childHeights = new Dictionary<Node, int>();
        var childPathsLeafToLeaf = new Dictionary<Node, IEnumerable<Node>>();
        var childPathsFromLeaf = new Dictionary<Node, List<Node>>();

        foreach (var child in node.Children)
        {
            int childDiameter;
            int childHeight;
            List<Node> childPathFromLeaf;

            childPathsLeafToLeaf[child] = DiameterThroughNode(child, 
                                      out childDiameter, 
                                      out childHeight, 
                                      out childPathFromLeaf);
                                      
            childDiameters[child] = childDiameter;
            childHeights[child] = childHeight;
            childPathsFromLeaf[child] = childPathFromLeaf;
        }

        var maxHeights = childHeights.OrderByDescending(kvp => kvp.Value).Take(2).ToArray();

        var maxByHeight0 = maxHeights[0].Key;
        var maxByHeight1 = maxHeights[1].Key;

        var maxByDiameter = childDiameters.OrderByDescending(kvp => kvp.Value).First().Key;

        height = 1 + childHeights[maxByHeight0];

        var maxHeightPath = childHeights[maxByHeight0] +
                            childHeights[maxByHeight1];

        pathFromLeaf = childPathsFromLeaf[maxByHeight0];
        pathFromLeaf.Add(node);

        if (maxHeightPath > childDiameters[maxByDiameter])
        {
            diameter = maxHeightPath;
            return childPathsFromLeaf[maxByHeight0].Concat(
                   childPathsFromLeaf[maxByHeight1].Reverse<Node>());
        }
        else
        {
            diameter = childDiameters[maxByDiameter];
            return childPathsLeafToLeaf[maxByDiameter];
        }
    }

    IEnumerable<Node> PathFromTo(Node node1, Node node2, bool includeNode2)
    {
        // fixme: check for null args
        var node = node1;
        while (node != node2 && node != null)
        {
            yield return node;
            node = node.Parent;
        }
        if (includeNode2 && node == node2)
        {
            yield return node2;
        }
    }

    // Again, I assume the problem asks for all paths between any two nodes,
    // not just downward paths or root-leaf paths.

    // This is difficult to analyze. I spent several hours on it and
    // it seems it is exponential in the length and quadratic in
    // the number of nodes: O(n^2*e^sum).

    // I think caching intermediate paths would bring it to O(n^2 * sum),
    // which I think is optimal (you need to read all n^2 paths and iterate 
    // up to length times each to check its length). I decided to leave 
    // it as it is for readability

    // Of course, this is still inefficient, but I've been unable to
    // think of (or find anywhere) a substantially better solution which
    // handles all paths through the tree, instead of just paths going down,
    // as well as negative numbers and variable number of descendants.
    // I suspect there isn't any. 

    public IEnumerable<IEnumerable<Node>> AllPathsOfSum(int sum)
    {
        return this.Nodes.SelectMany(root => AllPathsOfSum(root, sum, true));
    }

    IEnumerable<IEnumerable<Node>> AllPathsOfSum(Node node, int sum, bool allowFork)
    {
        var justNode = new[] { node };

        if (node.Index == sum)
            yield return justNode;

        for (int ii = 0; ii <= Math.Abs(sum - node.Index); ++ii)
        {
            int left = Math.Sign(sum) * ii;
            int right = Math.Sign(sum) * Math.Abs((sum - node.Index) - ii);

            foreach (var node1 in node.Children)
            {
                if (right == 0)
                {
                    foreach (var path1 in AllPathsOfSum(node1, left, false))
                        yield return path1.Concat(justNode);
                }
                foreach (var node2 in node.Children)
                {
                    if (allowFork)
                    {
                        if (left != 0 &&
                            right != 0 &&
                            node1 == node2)
                            continue;

                        if (node1.Index > node2.Index)
                            continue;

                        foreach (var path1 in AllPathsOfSum(node1, left, false))
                            foreach (var path2 in AllPathsOfSum(node2, right, false))
                            {
                                yield return path1.Concat(justNode).Concat(path2.Reverse());
                            }
                    }
                }
            }
        }
    }

    // just so you won't say I couldn't solve the other variant.
    // O(n), only handles downward paths. Handles negative numbers.

    public IEnumerable<IEnumerable<Node>> AllDownwardPathsOfSum(int sum)
    {
        return this.Nodes.SelectMany(node => AllDownwardPathsOfSum(node, sum, sum, new List<Node>()));
    }

    IEnumerable<IEnumerable<Node>> AllDownwardPathsOfSum(Node node, int initialSum, int sum, List<Node> soFar)
    {
        var justNode = new[] { node };

        if (sum == node.Index)
            yield return soFar.Concat(justNode);

        soFar.Add(node);
        foreach (var child in node.Children)
        {
            foreach (var path in AllDownwardPathsOfSum(child, initialSum, sum - node.Index, soFar))
                yield return path;
        }
        soFar.RemoveAt(soFar.Count - 1);
    }

    // O(n) - asymptotically optimal
    // this could also be done bottom to top
    // but I'm not sure if it would be faster
    // the naive-and-efficient solution doesn't handle negative numbers

    public IEnumerable<Node> AllSubtreesOfSum(int sum)
    {
        int temp;
        var nodes = new List<Node>();
        AllSubtreesOfSum(this.Root, sum, out temp, nodes);
        return nodes;
    }

    public void AllSubtreesOfSum(Node node, int sum, out int thisSum, List<Node> nodes)
    {
        thisSum = node.Index;

        foreach (var child in node.Children)
        {
            int subSum;
            AllSubtreesOfSum(child, sum, out subSum, nodes);
            thisSum += subSum;
        }

        if (thisSum == sum)
        {
            nodes.Add(node);
        }
    }

    Node GetOrMakeNode(int index)
    {
        Node node;

        if (!nodes.TryGetValue(index, out node))
        {
            node = new Node(index);
            nodes[index] = node;
        }

        return node;
    }
}