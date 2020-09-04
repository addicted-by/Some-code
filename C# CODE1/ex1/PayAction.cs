using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    public class PayAction
    {
        public PayAction(DateTime Date, decimal Sum, string s)
        {
            this.Date = Date;
            this.Sum = Sum;
            this.S = s;
        }
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
        public string S { get; set; }

    }
}
