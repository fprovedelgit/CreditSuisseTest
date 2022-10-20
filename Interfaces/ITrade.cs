using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPendingPayment { get; }
        ITradeCategorized Categorize(DateTime referencedDate);
    }
}
