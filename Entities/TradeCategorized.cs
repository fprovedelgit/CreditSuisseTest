using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public class TradeCategorized : ITradeCategorized
    {
        public TradeCategorized(double amount, string clientSector, DateTime nextPendingPayment)  
        {
            Value = amount;
            ClientSector = clientSector;
            NextPendingPayment = nextPendingPayment;
            Category = EnumTradeCategory.UNKNOWN;
        }
        public TradeCategorized(double amount, string clientSector, DateTime nextPendingPayment, EnumTradeCategory category)
        {
            Value = amount;
            ClientSector = clientSector;
            NextPendingPayment = nextPendingPayment;
            Category = category;
        }
        public EnumTradeCategory Category { get; set; }

        public double Value { get; }

        public string ClientSector { get; }

        public DateTime NextPendingPayment { get; }

        public ITradeCategorized Categorize(DateTime referencedDate)
        {
            return Categorize(referencedDate);
        }
        public override string ToString()
        {
            return $"{Value} {ClientSector} {NextPendingPayment.ToString("MM/dd/yyyy")} {Utility.GetDescriptionFromEnumValue(Category)}";
        }
    }
}
