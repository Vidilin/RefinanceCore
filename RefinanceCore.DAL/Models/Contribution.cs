using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RefinanceCore.DAL.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public int CityId { get; set; }

        [Display(Name = "ContributionName")]
        public string Name { get; set; }

        [Display(Name = "BaseAmount")]
        public int BaseAmount { get; set; }

        /// <summary>
        /// Дополнительный взнос
        /// </summary>
        /// <returns></returns>
        //public decimal GetAdditionalPayment(Quota quota)
        //{
        //    if (quota == null) return 0;
        //    if (quota.City == null) return 0;
        //    if (quota.City.Id != this.CityId) return 0;
        //    //quota.City == null ?
        //    //contribution.Id == quota.City.Id ?

        //    decimal result = quota.Amount * quota.City.SignificanceLevel * this.BaseAmount * 0.0001M;

        //    return Math.Round(result, 2);
        //}
    }
}
