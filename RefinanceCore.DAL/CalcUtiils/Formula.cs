using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;
using RefinanceCore.DAL.Enums;

namespace RefinanceCore.DAL.CalcUtiils
{
    public static class Formula
    {
        /// <summary>
        /// Процентная ставка
        /// </summary>
        public static decimal GetInterestRate (Quota quota)
        {
            //quota.City == null ?

            decimal purposeRate;

            switch (quota.Purpose)
            {
                case Purpose.Mortgage:
                    purposeRate = 1M; break;

                case Purpose.CarLoan:
                    purposeRate = 1.2M; break;

                case Purpose.ConsumerLoan:
                    purposeRate = 1.3M; break;

                default:
                    purposeRate = 0M; break;
            }
            //if (quota.QuotaPurpose == Purpose.Mortgage) purposeRate = 1;
            //else if (quota.QuotaPurpose == Purpose.ConsumerLoan) purposeRate = 1.2;
            //else if ()

            decimal result = (quota.City.SignificanceLevel + 10) * purposeRate;

            return Math.Round(result, 2);
        }

        /// <summary>
        /// Дополнительный взнос
        /// </summary>
        /// <returns></returns>
        public static decimal GetAdditionalPayment (Quota quota, Contribution contribution)
        {
            //quota.City == null ?
            //contribution.Id == quota.City.Id ?

            decimal result = quota.Amount * quota.City.SignificanceLevel * contribution.BaseAmount * 0.0001M;

            return Math.Round(result, 2);
        }
    }
}
