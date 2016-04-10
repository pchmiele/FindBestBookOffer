using System.Text.RegularExpressions;

namespace FindBestBookOffer.Model
{
    public class Price
    {
        private readonly decimal amount;
        private readonly Currency currency;

        public Price(decimal amount, Currency currency)
        {
            this.amount = amount;
            this.currency = currency;
        }

        protected bool Equals(Price other)
        {
            return amount == other.amount && currency == other.currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Price) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (amount.GetHashCode()*397) ^ (int) currency;
            }
        }

        public static Regex IsPreviousPriceRegex()
        {
            return new Regex(@"<del>(.*?)<\/del>");
        }

        public static Regex IsPriceInUSDRegex()
        {
            return new Regex(@"\$\d+\.\d{2}");
        }

        public override string ToString()
        {
            return string.Format("Price: {0}{1}", amount, currency);
        }

        public static bool operator >(Price price1, Price price2)
        {
            return price1.amount > price2.amount;
        }

        public static bool operator <(Price price1, Price price2)
        {
            return price1.amount < price2.amount;
        }
    }
}