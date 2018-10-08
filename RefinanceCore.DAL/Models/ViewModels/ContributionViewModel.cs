using System.ComponentModel.DataAnnotations;

namespace RefinanceCore.DAL.Models.ViewModels
{
    public class ContributionViewModel
    {
        [Display(Name = "ContributionName")]
        public string Name { get; set; }

        [Display(Name = "AdditionalPayment")]
        public decimal AdditionalPayment { get; set; }
    }
}
