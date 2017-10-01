using System;
using System.Linq;

namespace EstudoDDD.Domain
{
    public class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; } = Money.Empty;
        public Money MoneyInTransaction { get; private set; } = Money.Empty;

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = {
                Money.Cent,
                Money.TenCent,
                Money.Quarter,
                Money.Dollar,
                Money.FiveDollar,
                Money.TwentyDollar,
            };

            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = Money.Empty;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.Empty;
        }
    }
}