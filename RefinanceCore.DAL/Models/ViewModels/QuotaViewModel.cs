using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RefinanceCore.DAL.Enums;

namespace RefinanceCore.DAL.Models.ViewModels
{
    public class QuotaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CityName")]
        public string CityName { get; set; }

        /// <summary>
        /// Цель рефинансирования 
        /// </summary>
        [Display(Name = "QuotaPurpose")]
        public Purpose QuotaPurpose { get; set; }

        /// <summary>
        /// Сумма рефинансирования
        /// </summary>
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        private readonly Dictionary<int, string> purposes = new Dictionary<int, string>
            {
                {1, "Ипотека"},
                {2, "Потребительский кредит"},
                {3, "Автокредит"}
            };

        public string Purpose
        {
            get
            {
                string result = string.Empty;
                if (this.purposes.ContainsKey((int)this.QuotaPurpose)) result = purposes[(int)this.QuotaPurpose];
                return result;
            }          
        }
    }
}
