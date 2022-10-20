using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public interface ITradeCategorized : ITrade
    {
        EnumTradeCategory Category { get; set; }
    }
}
