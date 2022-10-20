using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public enum EnumTradeCategory
    {
        [Description("HIGHRISK")]
        HIGHRISK,
        [Description("EXPIRED")]
        EXPIRED,
        [Description("MEDIUMRISK")]
        MEDIUMRISK,
        [Description("")]
        UNKNOWN
    }
}
