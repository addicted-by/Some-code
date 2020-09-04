using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    interface ICalculator
    {
        decimal YTM(List<PayAction> coupons);
    }
}
