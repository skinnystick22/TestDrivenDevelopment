namespace Multicurrency;

public sealed class Sum(IExpression augend, IExpression addend) : IExpression
{
    public IExpression Augend { get; } = augend;
    public IExpression Addend { get; } = addend;

    public Money Reduce(Bank bank, string to)
    {
        var amount = Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount;
        return new Money(amount, to);
    }

    public IExpression Plus(IExpression addend)
    {
        return new Sum(this, addend);
    }

    public IExpression Times(int multiplier)
    {
        return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
    }
}