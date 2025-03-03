namespace GeKtvi.Toolkit.Tests;

public class ComparableList<T> : List<T>
{
    public ComparableList()
    {
    }

    public ComparableList(IEnumerable<T> collection) : base(collection)
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        if (obj is IEnumerable<T> enumerable)
            return Enumerable.SequenceEqual(this, enumerable);

        return false;
    }

    public override int GetHashCode() => 0;
}

