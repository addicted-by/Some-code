using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    public class Coupon
    {
        // Дата купона
        public DateTime Date { get; set; }

        /// Сумма купона в валюте облигации
        public decimal AmountInCurrency { get; set; }
    }
}
