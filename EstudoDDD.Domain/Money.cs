using System;

namespace EstudoDDD.Domain
{
    public class Money : ValueObject<Money>
    {
        public Money(int oneCentCount, 
                     int tenCentCount, 
                     int quarterCount, 
                     int oneDollarCount, 
                     int fiveDollarCount, 
                     int twentyDollarCount)
        {
            if (oneCentCount < 0) throw new InvalidOperationException(nameof(oneCentCount));
            if (tenCentCount < 0) throw new InvalidOperationException(nameof(tenCentCount));
            if (quarterCount < 0) throw new InvalidOperationException(nameof(quarterCount));
            if (oneDollarCount < 0) throw new InvalidOperationException(nameof(oneDollarCount));
            if (fiveDollarCount < 0) throw new InvalidOperationException(nameof(fiveDollarCount));
            if (twentyDollarCount < 0) throw new InvalidOperationException(nameof(twentyDollarCount));

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
            OneCentCount * 0.01m +
            TenCentCount * 0.10m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5 +
            TwentyDollarCount * 20;

        public static Money operator -(Money money1, Money money2)
        {
            return new Money
            (
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount
            );
        }
        public static Money operator +(Money money1, Money money2)
        {
            return new Money
            (
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount
            );
        }

        protected override bool EqualsCore(Money otherObject)
        {
            return OneCentCount == otherObject.OneCentCount
                   && TenCentCount == otherObject.TenCentCount
                   && QuarterCount == otherObject.QuarterCount
                   && OneDollarCount == otherObject.OneDollarCount
                   && FiveDollarCount == otherObject.FiveDollarCount
                   && TwentyDollarCount == otherObject.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }
        }

        public static Money Empty = new Money(0, 0, 0, 0, 0, 0);
        public static Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);
    }
}