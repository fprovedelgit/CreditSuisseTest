using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public interface ICategorizationTradesReturn
    {
        bool Success { get; }
        long NumberOfTrades { get; set; }
        DateTime ReferenceDate { get; set; }
        string InputFileName { get; set; }
        string LogFileName { get; set; }
        IList<string> Messages { get; }
        IList<ITradeCategorized> CategorizedTrades { get; }
        void AddError(string message);
    }
}
