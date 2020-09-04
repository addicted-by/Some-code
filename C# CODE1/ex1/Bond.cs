using System;
using System.Collections.Generic;
using System.Text;

namespace ex1
{
    /// <summary>
    /// Облигация
    /// </summary>
    public class Bond
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ISIN
        /// </summary>
        public string ISIN { get; set; }
        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Цена покупки
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Величина купона в процентах
        /// </summary>
        public decimal CouponRate { get; set; }
        /// <summary>
        /// НКД
        /// </summary>
        public decimal AccumulateCouponIncome { get; set; }
        /// <summary>
        /// Дата покупки
        /// </summary>
        public DateTime SettlementDate { get; set; }
        /// <summary>
        /// Номинал
        /// </summary>
        public decimal Nominal { get; set; }
        /// <summary>
        /// Дата погашения
        /// </summary>
        public DateTime MaturityDate { get; set; }
        /// <summary>
        /// Дата обратного выкупа
        /// </summary>
        public DateTime? BuyBackDate { get; set; }
        /// <summary>
        /// Купоны
        /// </summary>
        public List<Coupon> Coupons { get; set; }
    }
}
