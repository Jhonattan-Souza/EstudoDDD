using System;
using EstudoDDD.Domain.Tests.AutoData;
using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EstudoDDD.Domain.Tests
{
    public class MoneyTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardClauseTests(GuardClauseAssertion guardClause)
        {
            guardClause.Verify(typeof(Money).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Sum_ShouldReturn_CorrectResult(Money sut, Money money)
        {
            var sum = sut + money;

            sum.OneCentCount.Should().Be(sut.OneCentCount + money.OneCentCount);
            sum.TenCentCount.Should().Be(sut.TenCentCount + money.TenCentCount);
            sum.QuarterCount.Should().Be(sut.QuarterCount + money.QuarterCount);
            sum.OneDollarCount.Should().Be(sut.OneDollarCount + money.OneDollarCount);
            sum.FiveDollarCount.Should().Be(sut.FiveDollarCount + money.FiveDollarCount);
            sum.TwentyDollarCount.Should().Be(sut.TwentyDollarCount + money.TwentyDollarCount);
        }

        [Theory, AutoNSubstituteData]
        public void TwoMoneyInstances_ShouldBeEqual_IfContainsDifferentAmount(Money sut)
        {
            var money = new Money(sut.OneCentCount,
                sut.TenCentCount,
                sut.QuarterCount,
                sut.OneDollarCount,
                sut.FiveDollarCount,
                sut.TwentyDollarCount);

            sut.Should().Be(money);
            sut.GetHashCode().Should().Be(money.GetHashCode());
        }

        [Theory, AutoNSubstituteData]
        public void TwoMoneyInstances_ShouldNotBeEqual_IfContainsDifferentAmount(Money sut)
        {
            var money = new Money(sut.OneCentCount + 1,
                sut.TenCentCount,
                sut.QuarterCount,
                sut.OneDollarCount,
                sut.FiveDollarCount,
                sut.TwentyDollarCount);

            sut.Should().NotBe(money);
            sut.GetHashCode().Should().NotBe(money.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_CreateMoney_WithNegativeValues(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            Action createMoney = () =>
                new Money(oneCentCount,
                          tenCentCount,
                          quarterCount,
                          oneDollarCount,
                          fiveDollarCount,
                          twentyDollarCount);

            createMoney.ShouldThrow<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_IsCalculated_Correctly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            double expectedAmount)
        {
            var money = new Money(oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            money.Amount.ShouldBeEquivalentTo(expectedAmount);
        }

        [Fact]
        public void Subtraction_ShouldReturn_CorrectResult()
        {
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var subtraction = money1 - money2;

            subtraction.OneCentCount.Should().Be(9);
            subtraction.TenCentCount.Should().Be(8);
            subtraction.QuarterCount.Should().Be(7);
            subtraction.OneDollarCount.Should().Be(6);
            subtraction.FiveDollarCount.Should().Be(5);
            subtraction.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_Subtract_More_ThanExists()
        {
            var money1 = new Money(0, 1, 0, 0, 0, 0);
            var money2 = new Money(1, 0, 0, 0, 0, 0);

            Action action = () =>
            {
                var money = money1 - money2;
            };

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}