using System;
using EstudoDDD.Domain.Tests.AutoData;
using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Xunit;
using static EstudoDDD.Domain.Money;

namespace EstudoDDD.Domain.Tests
{
    public class SnackMachineTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardClauseTest(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(SnackMachine).GetConstructors());
        }

        [Fact]
        public void ReturnMoney_EmptiesMoney_InTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void InsertedMoney_GoesTo_MoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void CannotInsert_MoreThanOneCoin_AtATime()
        {
            var snackMachine = new SnackMachine();
            var twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void MoneyInTransaction_GoesToMoneyInside_AfterPurchase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Should().Be(Empty);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }
    }
}