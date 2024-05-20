﻿namespace Multicurrency;

public sealed class Money(int amount, string currency) : IExpression
{
    public readonly int Amount = amount;
    public readonly string Currency = currency;

    public Money Reduce (Bank bank, string to)
    {
        var rate = bank.Rate(Currency, to);
        return new Money(Amount / rate, to);
    }

    public IExpression Plus(IExpression addend)
    {
        return new Sum(this, addend);
    }

    public IExpression Times(int multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    public static Money Dollar(int amount)
    {
        return new Money(amount, "USD");
    }

    public static Money Franc(int amount)
    {
        return new Money(amount, "CHF");
    }

    public override bool Equals(object? obj)
    {
        var money = obj as Money;
        return Amount == money.Amount && Currency == money.Currency;
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}