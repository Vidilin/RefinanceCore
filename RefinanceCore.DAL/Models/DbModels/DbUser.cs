using System;
using System.Collections.Generic;

namespace RefinanceCore.DAL.Models.DbModels
{
    internal partial class DbUser
    {
        public DbUser()
        {
            Quotas = new HashSet<DbQuota>();
        }

        public int Id { get; set; }
        public string Login { get; set; }

        public ICollection<DbQuota> Quotas { get; set; }
    }
}
