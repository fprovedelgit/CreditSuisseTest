using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    public class TradeService : ITradeService
    {
        
        public ICategorizationTradesReturn CategorizeTradeFile(string filename)
        {
            CategorizationTradesReturn @return = new CategorizationTradesReturn();
            try
            {
                if (!File.Exists(filename))
                    throw new Exception($"Input file [{filename}] not exists!");
                @return.LogFileName = filename + $".{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";
                @return.InputFileName = filename;
                if ((new FileInfo(filename)).Length == 0)
                    throw new Exception($"Input file [{filename}] is empty!");
                string tradeLine = string.Empty;
                DateTime referenceDate = DateTime.Now;
                long numberOfTrades = 0;
                
                using (StreamReader file = new StreamReader(filename))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            if (!DateTime.TryParseExact(ln, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
                                @return.AddError($"Input file is not correct format. Reference Date in first line is invalid [{ln}]");
                            else
                                @return.ReferenceDate = referenceDate;
                        }
                        else if (counter == 1)
                        {
                            if (!long.TryParse(ln.Trim(), out numberOfTrades))
                            {
                                @return.AddError($"Input file is not correct format. Number of Trades in second line is invalid [{ln}]");
                            }
                            else
                                @return.NumberOfTrades = numberOfTrades;
                        }
                        else
                        {
                            tradeLine = ln;
                            string[] fields = ln.Split(' ');
                            if (fields.Count() != 3)
                            {
                                @return.AddError($"Input file is not correct format. Trades don't have expeted number of fields [\"{ln}\",{fields.Count()}]");
                            }
                            double amount = 0;
                            if(!double.TryParse(fields[0].Trim(),out amount))
                            {
                                @return.AddError($"Input file is not correct format. Trades Amount is not a valid value [{fields[0]}]");
                            }
                            string clientSector = fields[1];
                            if (!fields[1].ToLower().Equals("public") || !fields[1].ToLower().Equals("private"))
                            {
                                @return.AddError($"Input file is not correct format. Trades Client Sector is not a valid value [{clientSector}]");
                            }
                            DateTime nextPendingPayment;
                            if (!DateTime.TryParseExact(fields[2].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out nextPendingPayment))
                                @return.AddError($"Input file is not correct format. Trades Next Pending Payment is not a valid value [{fields[2]}]");
                            ITrade trade = new Trade(amount,clientSector,nextPendingPayment);
                            @return.CategorizedTrades.Add(trade.Categorize(referenceDate));
                        }
                        counter++;
                    }
                    file.Close();
                }

                

                @return.Success = (@return.Messages.Count()==0);
            }
            catch (Exception ex)
            {
                @return.Messages.Add(ex.Message);
                @return.Success = false;
            } 
            finally
            {
                using (StreamWriter logfile = new StreamWriter(@return.LogFileName))
                {
                    logfile.WriteLine($"Input filename: {@return.InputFileName}");

                    if (@return.Success)
                    {
                        logfile.WriteLine($"Reference Date: {@return.ReferenceDate.ToString("MM/dd/yyyy")}");
                        logfile.WriteLine($"Number of lines: {@return.NumberOfTrades}");
                        foreach (var item in @return.CategorizedTrades)
                        {
                            logfile.WriteLine(item.ToString());
                        }
                    }
                    else
                    {
                        foreach (var message in @return.Messages)
                        {
                            logfile.WriteLine(message);
                        }
                    }
                    logfile.Flush();
                    logfile.Close();
                }
            }
            return @return;
        }
    }
}
