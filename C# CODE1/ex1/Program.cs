using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            int step = 0, n = -1;
            List<Bond> bonds;
            using (StreamReader r = new StreamReader("bonds.json"))
            
                bonds = JsonConvert.DeserializeObject<List<Bond>>(r.ReadToEnd());
            

            while (n != 0)
            {
                Console.WriteLine("Choose bond:");
                foreach (Bond bond in bonds)
                    Console.WriteLine($"{++step}. ISIN: {{{bond.ISIN}}}. Name: {{{bond.Name}}}. Nominal: {{{bond.Nominal}}}");
                Console.Write("№: ");
                n = Convert.ToInt32(Console.ReadLine());
                if (n == 0) break;
                Bond b = new Bond();
                try { b = bonds[n - 1]; }
                catch { Console.WriteLine("Incorrect bond's number"); Environment.Exit(0); }
                               
                flag = true;
                while (flag == true)
                {
                    BondDescriptor bd = new BondDescriptor(b, new BondCalculatorNewton());
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine($"Your choice: <{b.Name}>");
                    Console.WriteLine("1. General information");
                    Console.WriteLine("2. Profit");
                    Console.WriteLine("3. Current cost");
                    Console.WriteLine("4. Next pays");
                    Console.WriteLine("5. Choose other bond");
                    Console.Write("> ");
                    var a = Console.ReadLine();
                    switch (a)
                    {
                        case "1":
                            bd.General();
                            break;
                        case "2":
                            bd.Profit();
                            break;
                        case "3":
                            bd.CurCost();
                            break;
                        case "4":
                            bd.NextPay();
                            break;
                        case "5":
                            flag = false;
                            step = 0;
                            break;
                    }
                }
            }
            }
    }
}
