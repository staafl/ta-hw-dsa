// our own Tuples - structs, instead of classes, for efficiency

public struct Tuple<P, V>
{
    public Tuple(P p, V v) : this()
    {
        this.Item1 = p;
        this.Item2 = v;
    }

    public P Item1 { get; private set; }

    public V Item2 { get; private set; }

    public override string ToString()
    {
        return "(" + Item1 + "," + Item2 + ")";
    }
}

public static class Tuple
{
    public static Tuple<P, V> Create<P, V>(P p, V v)
    {
        return new Tuple<P, V>(p, v);
    }
}