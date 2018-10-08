using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefinanceCore.DAL.Models.ViewModels;

namespace RefinanceCore.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<QuotaViewModel> Quotas { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
