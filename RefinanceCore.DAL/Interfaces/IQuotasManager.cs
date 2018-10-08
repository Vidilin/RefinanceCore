using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;
using System.Linq;

namespace RefinanceCore.DAL.Interfaces
{
    public interface IQuotasManager
    {
        Quota GetQuota(int id);
        IList<Models.ViewModels.QuotaViewModel> GetAllQuotas(int userId);
        void AddQuota(Quota quota);
        void DeleteQuota(int id);
        void EditQuota(Quota quota);
    }
}
