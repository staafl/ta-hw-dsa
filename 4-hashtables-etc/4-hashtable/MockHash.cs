struct MockHash
{
    public MockHash(int hash, int equality) : this()
    {
        this.Hash = hash;
        this.Equality = equality;
    }

    public int Hash { get; set; }

    public int Equality { get; set; }

    public override int GetHashCode()
    {
        return this.Hash;
    }

    public override bool Equals(object obj2)
    {
        if (obj2 == null)
            return false;
        if (!(obj2 is MockHash))
            return false;
        return ((MockHash)obj2).Equality == this.Equality;
    }
}