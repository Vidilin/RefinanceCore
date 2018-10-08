using System;
using System.Collections.Generic;

namespace RefinanceCore.DAL.Models.DbModels
{
    internal partial class DbCitiy
    {
        public DbCitiy()
        {
            Contributions = new HashSet<DbContribution>();
            Quotas = new HashSet<DbQuota>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SignificanceLevel { get; set; }

        public ICollection<DbContribution> Contributions { get; set; }
        public ICollection<DbQuota> Quotas { get; set; }
    }
}
