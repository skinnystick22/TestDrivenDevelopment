namespace Multicurrency;

public sealed class Pair(string from, string to)
{
    private readonly string _from = from;
    private readonly string _to = to;

    public override bool Equals(object? obj)
    {
        var pair = obj as Pair;
        return _from == pair._from && _to == pair._to;
    }

    public override int GetHashCode()
    {
        return _from.GetHashCode() ^ _to.GetHashCode();
    }
}