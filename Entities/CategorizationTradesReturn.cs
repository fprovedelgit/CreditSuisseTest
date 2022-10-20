using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public class CategorizationTradesReturn : ICategorizationTradesReturn
    {
        const long MAXERROS = 10;
        public bool Success { get; set; }
        public long NumberOfTrades { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string InputFileName { get; set; }
        public string LogFileName { get; set; }
        public IList<string> Messages { get; set; }

        public IList<ITradeCategorized> CategorizedTrades { get; set; }
        public CategorizationTradesReturn()
        {
            CategorizedTrades = new List<ITradeCategorized>();
            Messages = new List<string>();
            Success = false;
        }

        public void AddError(string message)
        {
            Messages.Add(message);
            Success = false;
            if (Messages.Count >= MAXERROS)
                throw new Exception($"Maximum number of errors reached [{MAXERROS}].");
        }
    }
}
