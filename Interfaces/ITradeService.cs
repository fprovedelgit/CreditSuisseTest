using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public interface ITradeService
    {
        ICategorizationTradesReturn CategorizeTradeFile(string filename);
    }
}
