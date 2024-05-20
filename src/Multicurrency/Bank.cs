namespace Multicurrency;

public sealed class Bank
{
    private readonly Dictionary<Pair, int> _rates = new();

    public void AddRate(string from, string to, int rate)
    {
        _rates.Add(new Pair(from, to), rate);
    }

    public Money Reduce(IExpression source, string to)
    {
        return source.Reduce(this, to);
    }

    public int Rate(string from, string to)
    {
        return from == to ? 1 : _rates[new Pair(from, to)];
    }
}