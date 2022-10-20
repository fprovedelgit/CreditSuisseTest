using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "CreditSuisseTest";

            Console.WriteLine("                                                                                                ");
            Console.WriteLine("     ██████╗██████╗ ███████╗██████╗ ██╗████████╗    ███████╗██╗   ██╗██╗███████╗███████╗███████╗");
            Console.WriteLine("    ██╔════╝██╔══██╗██╔════╝██╔══██╗██║╚══██╔══╝    ██╔════╝██║   ██║██║██╔════╝██╔════╝██╔════╝");
            Console.WriteLine("    ██║     ██████╔╝█████╗  ██║  ██║██║   ██║       ███████╗██║   ██║██║███████╗███████╗█████╗  ");
            Console.WriteLine("    ██║     ██╔══██╗██╔══╝  ██║  ██║██║   ██║       ╚════██║██║   ██║██║╚════██║╚════██║██╔══╝  ");
            Console.WriteLine("    ╚██████╗██║  ██║███████╗██████╔╝██║   ██║       ███████║╚██████╔╝██║███████║███████║███████╗");
            Console.WriteLine("     ╚═════╝╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝   ╚═╝       ╚══════╝ ╚═════╝ ╚═╝╚══════╝╚══════╝╚══════╝");
            Console.WriteLine("                                                                                                ");
            Console.WriteLine("                                     Trades categorization");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            
            if (args.Count() == 0)
            {
                Console.WriteLine("Syntax: CreditSuisseTest.exe {input file}");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("Error: Please, enter trades input file!");
                Console.ReadKey();
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Error: Informed file does not exist!");
                Console.ReadKey();
                return;
            }
            else
            {
                try
                {
                    ITradeService service = new TradeService();
                    ICategorizationTradesReturn categorizationTradesReturn = service.CategorizeTradeFile(args[0]);
                    if (categorizationTradesReturn.Success)
                    {
                        foreach (var item in categorizationTradesReturn.CategorizedTrades)
                        {
                            Console.WriteLine(Utility.GetDescriptionFromEnumValue(item.Category));
                        }
                        Console.WriteLine();
                        Console.WriteLine("Process finished success!");
                    }
                    else
                    {
                        Console.WriteLine("Process finished with erros!");
                        foreach (var item in categorizationTradesReturn.Messages)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    Console.WriteLine($"Log file [{categorizationTradesReturn.LogFileName}] is created.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    
                }
                
                Console.ReadKey();
                return;
            }

            
        }
    }
}
