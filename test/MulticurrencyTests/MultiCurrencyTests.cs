using Multicurrency;

namespace MulticurrencyTests;

public class MultiCurrencyTests
{
    private readonly Bank _bank;
    private readonly Money _fiveDollars;
    private readonly Money _tenFrancs;
    private readonly Money _fiveFrancs;
    private readonly Money _tenDollars;

    public MultiCurrencyTests()
    {
        _bank = new Bank();
        _bank.AddRate("CHF", "USD", 2);
        _fiveDollars = Money.Dollar(5);
        _tenDollars = Money.Dollar(10);
        _tenFrancs = Money.Franc(10);
        _fiveFrancs = Money.Franc(5);
    }

    [Fact]
    public void TestMultiplication()
    {
        Assert.Equal(Money.Dollar(10), _fiveDollars.Times(2));
        Assert.Equal(Money.Dollar(15), _fiveDollars.Times(3));
    }


    [Fact]
    public void TestEquality()
    {
        Assert.True(_fiveDollars.Equals(_fiveDollars));
        Assert.False(_fiveDollars.Equals(Money.Dollar(6)));
        Assert.False(_fiveFrancs.Equals(_fiveDollars));
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
        var sum = (Sum)_fiveDollars.Plus(_fiveDollars);
        Assert.Equal(_fiveDollars, sum.Augend);
        Assert.Equal(_fiveDollars, sum.Addend);
    }

    [Fact]
    public void TestReduceSum()
    {
        var sum = new Sum(Money.Dollar(3), Money.Dollar(4));
        var result = _bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(7), result);
    }

    [Fact]
    public void TestSimpleAddition()
    {
        var sum = _fiveDollars.Plus(_fiveDollars);
        var reduced = _bank.Reduce(sum, "USD");
        Assert.Equal(_tenDollars, reduced);
    }

    [Fact]
    public void TestReduceMoney()
    {
        var result = _bank.Reduce(Money.Dollar(1), "USD");
        Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void TestReduceMoneyDifferentCurrency()
    {
        var result = _bank.Reduce(Money.Franc(2), "USD");
        Assert.Equal(Money.Dollar(1), result);
    }

    [Fact]
    public void TestIdentityRate()
    {
        var rate = _bank.Rate("USD", "USD");
        Assert.Equal(1, rate);
    }

    [Fact]
    public void TestMixedAddition()
    {
        var result = _bank.Reduce(_fiveDollars.Plus(_tenFrancs), "USD");
        Assert.Equal(_tenDollars, result);
    }

    [Fact]
    public void TestSumPlusMoney()
    {
        var sum = new Sum(_fiveDollars, _tenFrancs).Plus(_fiveDollars);
        var result = _bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(15), result);
    }

    [Fact]
    public void TestSumTimes()
    {
        var sum = new Sum(_fiveDollars, _tenFrancs).Times(2);
        var result = _bank.Reduce(sum, "USD");
        Assert.Equal(Money.Dollar(20), result);
    }
}