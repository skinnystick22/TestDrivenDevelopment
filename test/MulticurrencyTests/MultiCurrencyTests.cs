using Multicurrency;

namespace MulticurrencyTests;

public class MultiCurrencyTests
{
    [Fact]
    public void TestMultiplication()
    {
        var five = Money.Dollar(5);
        Assert.Equal(Money.Dollar(10), five.Times(2));
        Assert.Equal(Money.Dollar(15), five.Times(3));
    }


    [Fact]
    public void TestEquality()
    {
        Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)));
        Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));
        Assert.False(Money.Franc(5).Equals(Money.Dollar(5)));
    }

    [Fact]
    public void TestCurrency()
    {
        Assert.Equal("USD", Money.Dollar(1).Currency);
        Assert.Equal("CHF", Money.Franc(1).Currency);
    }

    [Fact]
    public void TestPlusReturnsSum()
    {
        var five = Money.Dollar(5);
        var sum = (Sum)five.Plus(five);
        Assert.Equal(five, sum.Augend);
        Assert.Equal(five, sum.Addend);
    }

    [Fact]
    public void TestReduceSum()
    {
        var sum = new Sum(Money.Dollar(3), Money.Dollar(4));
        var bank = new Bank();
        var result = bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(7), result);
    }

    [Fact]
    public void TestSimpleAddition()
    {
        var five = Money.Dollar(5);
        var sum = five.Plus(five);
        var bank = new Bank();
        var reduced = bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(10), reduced);
    }

    [Fact]
    public void TestReduceMoney()
    {
        var bank = new Bank();
        var result = bank.Reduce(Money.Dollar(1), "USD");
        Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void TestReduceMoneyDifferentCurrency()
    {
        var bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        var result = bank.Reduce(Money.Franc(2), "USD");
        Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void TestIdentityRate()
    {
        var bank = new Bank();
        var rate = bank.Rate("USD", "USD");
        Assert.Equal(1, rate);
    }

    [Fact]
    public void TestMixedAddition()
    {
        IExpression fiveDollars = Money.Dollar(5);
        IExpression tenFrancs = Money.Franc(10);
        var bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        var result = bank.Reduce(fiveDollars.Plus(tenFrancs), "USD");
        Assert.Equal(Money.Dollar(10), result);
    }

    [Fact]
    public void TestSumPlusMoney()
    {
        IExpression fiveDollars = Money.Dollar(5);
        IExpression tenFrancs = Money.Franc(10);
        var bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        var sum = new Sum(fiveDollars, tenFrancs).Plus(fiveDollars);
        var result = bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(15), result);
    }

    [Fact]
    public void TestSumTimes()
    {
        IExpression fiveDollars = Money.Dollar(5);
        IExpression tenFrancs = Money.Franc(10);
        var bank = new Bank();
        bank.AddRate("CHF", "USD", 2);
        var sum = new Sum(fiveDollars, tenFrancs).Times(2);
        var result = bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(20), result);
    }
}