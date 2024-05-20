﻿namespace Multicurrency;

public interface IExpression
{
    Money Reduce(Bank bank, string to);
    IExpression Plus(IExpression addend);
    IExpression Times(int multiplier);
}