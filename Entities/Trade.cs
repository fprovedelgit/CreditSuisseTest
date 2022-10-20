using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public class Trade : ITrade
    {
        public double Value { get; set; }
        public string ClientSector { get; set; }
        public DateTime NextPendingPayment { get; set; }
        public Trade(double amount, string clientSector, DateTime nextPendingPayment)
        {
            Value = amount;
            ClientSector = clientSector;
            NextPendingPayment = nextPendingPayment;
        }

        public ITradeCategorized Categorize(DateTime referencedDate)
        {
            ITradeCategorized categorized = new TradeCategorized(Value,ClientSector,NextPendingPayment);
            if (referencedDate.Subtract(NextPendingPayment).TotalDays > 30)
                categorized.Category = EnumTradeCategory.EXPIRED;
            else if ( Value>1000000 && ClientSector.ToLower().Equals("private"))
                categorized.Category = EnumTradeCategory.HIGHRISK;
            else if (Value > 1000000 && ClientSector.ToLower().Equals("public"))
                categorized.Category = EnumTradeCategory.MEDIUMRISK;
            else
                categorized.Category = EnumTradeCategory.UNKNOWN;
            return categorized;
        }
    }
}
