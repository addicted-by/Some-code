using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    class BondDescriptor
    {
        Bond bond;
        ICalculator calculator;
        private List<PayAction> actions { set; get; }
        public BondDescriptor(Bond bond, ICalculator calculator)
        {
            this.bond = bond;
            this.calculator = calculator;
            List<PayAction> tmp = new List<PayAction>();
            tmp.Add(new PayAction(bond.SettlementDate, -bond.Price, "Buying"));
            DateTime end = bond.MaturityDate;
            if (bond.BuyBackDate != null && bond.BuyBackDate >= bond.SettlementDate)
            {

                end = (DateTime)bond.BuyBackDate;
                tmp.Add(new PayAction(end, bond.Nominal, "Buyback"));
            }
            else
            {
                tmp.Add(new PayAction(end, bond.Nominal, "Maturity"));
            }
            
            foreach (Coupon C in bond.Coupons)
            {
                if (C.Date <= end)
                    tmp.Add(new PayAction(C.Date, C.AmountInCurrency, "Coupon"));
                else
                    break;

            }
            actions = tmp;
        }
        public void General()
       {
            Console.WriteLine(
                           $"Name:                        {{{bond.Name}}}" +
                           $"\nISIN:                        {{{bond.ISIN}}}" +
                           $"\nCurrency:                    {{{bond.Currency}}}" +
                           $"\nPrice:                       {{{bond.Price}}}" +
                           $"\nCoupon Rate:                 {{{bond.CouponRate}}}" +
                           $"\nAccumulate Coupon Income:    {{{bond.AccumulateCouponIncome}}}" +
                           $"\nSettlement Date:             {{{bond.SettlementDate}}}" +
                           $"\nNominal:                     {{{bond.Nominal}}}" +
                           $"\nMaturity Date:               {{{bond.MaturityDate}}}" +
                           $"\nBuyBack Date:                {{{bond.BuyBackDate}}}");
        }
        public void Profit()
        {
            Console.WriteLine("Profit: {0:f2}%", calculator.YTM(actions)*100);
        }
        public void CurCost()
        {
            Console.WriteLine("Current Price: {0:f2}", bond.Price);
        }
        public void NextPay()
        {
            if (actions.Count <= 5)
            {
                foreach (PayAction i in actions)
                {
                    if (i.S != "Buying")
                        Console.WriteLine("{0} {1} {2:f2}", i.Date.ToString("dd.MM.yyyy"), i.S, i.Sum);
                    else
                        continue;
                }
            }
            else
            {
                Console.WriteLine("{0} {1} {2:f2}", actions[1].Date.ToString("dd.MM.yyyy"), actions[1].S,
                    actions[1].Sum);
                Console.WriteLine("{0} {1} {2:f2}", actions[2].Date.ToString("dd.MM.yyyy"), actions[2].S,
                    actions[2].Sum);

                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("{0} {1} {2:f2}",actions[actions.Count - 2].Date.ToString("dd.MM.yyyy"),
                    actions[actions.Count - 2].S, actions[actions.Count - 2].Sum);
                Console.WriteLine("{0} {1} {2:f2}", actions[actions.Count - 2].Date.ToString("dd.MM.yyyy"),
                    actions[actions.Count - 1].S, actions[actions.Count - 1].Sum);
            }
        }

    }
}
