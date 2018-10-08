using RefinanceCore.DAL.Enums;
using RefinanceCore.DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RefinanceCore.DAL.Models
{
    public class Quota
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        /// <summary>
        /// Цель рефинансирования 
        /// </summary>
        [Display(Name = "Цель")]
        public Purpose Purpose { get; set; }

        /// <summary>
        /// Сумма рефинансирования
        /// </summary>
        [Display(Name = "Сумма")]
        [Range(0, 5000000, ErrorMessage = "Сумма должна быть от 0 до 5000000")]
        [Required(ErrorMessage = "Укажите сумму")]
        public decimal Amount { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата редактирования")]
        public DateTime? ModifyDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Комментарий")]
        [StringLength(1024, ErrorMessage = "Длина не более 1024 символов")]
        public string Comment { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// Список дополнительных взносов 
        /// </summary>
        private IList<Contribution> Contributions { get; set; }

        public Quota()
        {
            Contributions = new List<Contribution>();
            City = new City();
        }

        public Quota(City city, IList<Contribution> contributions)
        {
            Contributions = contributions;
            City = city;
        }

        internal void SetContributions(IList<Contribution> list)
        {
            this.Contributions = list;
        }

        /// <summary>
        /// Дополнительный взнос
        /// </summary>
        /// <returns></returns>
        private decimal GetAdditionalPayment(Contribution contribution)
        {
            //quota.City == null ?
            if (contribution.CityId != this.City.Id) return 0;

            decimal result = this.Amount * this.City.SignificanceLevel * contribution.BaseAmount * 0.0001M;

            return Math.Round(result, 2);
        }

        public IList<ContributionViewModel> QuotaContributions
        {
            get
            {
                var result = this.Contributions.Select(o => new ContributionViewModel
                {
                    Name = o.Name,
                    AdditionalPayment = this.GetAdditionalPayment(o),
                }).ToList();

                return result;
            }           
        }
        /// <summary>
        /// Процентная ставка
        /// </summary>
        [Display(Name = "InterestRate")]
        public decimal InterestRate           
        {
            get
            {
                if (this.City == null) return 0;

                decimal purposeRate;

                switch (this.Purpose)
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

                decimal result = (this.City.SignificanceLevel + 10) * purposeRate;

                return Math.Round(result, 2);
            }           
        }

        private readonly Dictionary<int, string> purposes = new Dictionary<int, string>
            {
                {1, "Ипотека"},
                {2, "Потребительский кредит"},
                {3, "Автокредит"}
            };

        public string StringPurpose
        {
            get
            {
                string result = string.Empty;
                if (this.purposes.ContainsKey((int)this.Purpose)) result = purposes[(int)this.Purpose];
                return result;
            }
        }
    }
}
