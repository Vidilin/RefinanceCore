using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RefinanceCore.DAL.Enums
{
    public enum Purpose
    {
        /// <summary>
        /// Ипотека
        /// </summary>
        [Display(Name = "Ипотека")]
        Mortgage = 1,
        /// <summary>
        /// Потребительский кредит
        /// </summary>
        [Display(Name = "Потребительский кредит")]
        ConsumerLoan = 2,
        /// <summary>
        /// Автокредит
        /// </summary>
        [Display(Name = "Автокредит")]
        CarLoan = 3
    }
}
