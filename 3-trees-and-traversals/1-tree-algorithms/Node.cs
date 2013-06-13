using System.Collections.Generic;

class Node
{
    public int Index { get; private set; }

    public Node Parent { get; set; }

    public List<Node> Children { get; private set; }

    public Node(int index)
    {
        this.Index = index;
        this.Children = new List<Node>();
    }

    public override string ToString()
    {
        return "(" + this.Index + ")";
    }

    public override int GetHashCode()
    {
        return this.Index.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        var asNode = obj as Node;
        if (asNode == null)
            return false;

        return this.Index == asNode.Index;
    }
}