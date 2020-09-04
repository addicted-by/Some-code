using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    // Метод дихотомии
    class BondCalculatorDihotomy : ICalculator
    {
        delegate decimal FCalc(List<PayAction> _actions, decimal _x);
        public decimal YTM(List<PayAction> actions)
        {
            decimal x = 0, eps = (decimal)0.01, a = x, b = x;
            FCalc fCalc = (_actions, _x) =>
            {
                decimal sum = 0;
                foreach (PayAction i in actions) 

                    sum += i.Sum / (decimal)Math.Pow(1 + (double)x, 
                        (double)((decimal)((i.Date - actions[0].Date).Days) / 365));

                return sum;
            };
            decimal f = fCalc(actions, x);
            if (f > 0)
            {
                while (f > 0 && Math.Abs(f) >= eps)
                {
                    a = b++;
                    x = b;
                    f = fCalc(actions, x);
                }
            }
            else
            {
                while (f < 0 && Math.Abs(f) >= eps)
                {
                    b = a--;
                    x = a;
                    f = fCalc(actions, x);
                }
            }
            while (Math.Abs(f) >= eps)
            {
                x = (a + b) / 2;
                f = fCalc(actions, x);
                if (f > 0)
                    a = x;
                else
                    b = x;
            }
            return x;
        }
    }
}
