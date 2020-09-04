using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    // метод Ньютона
    class BondCalculatorNewton : ICalculator
    {
        delegate decimal FCalc(List<PayAction> _actions, decimal _x);
        delegate decimal DerCalc(List<PayAction> __actions, decimal __x);

        public decimal YTM(List<PayAction> actions)
        {
            decimal x = 0, eps = (decimal)0.01;
            FCalc fCalc = (_actions, _x) =>
            {
                decimal sum = 0;
                foreach (PayAction i in actions)
                    
                    sum += i.Sum / (decimal)Math.Pow(1 + (double)x,
                        (double)(((decimal)(i.Date - actions[0].Date).Days) / 365));
                
                return sum;
            };
             DerCalc derCalc = (__actions, __x) =>
             {
                 decimal sum = 0;
                 foreach (PayAction i in actions)
                 
                        sum -= (decimal)((i.Date - actions[0].Date).Days) / 365 *  i.Sum / (decimal)(Math.Pow((double)(1 + x), (double)((decimal)((i.Date - actions[0].Date).Days) + 365) / 365));

                 return sum;
             };
            decimal f = fCalc(actions, x);
            while (Math.Abs(f) >= eps)
            {
                x -= f / derCalc(actions, x);
                f = fCalc(actions, x);
            }
            return x;
        }
    }
}
