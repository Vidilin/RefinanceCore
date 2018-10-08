using System;
using System.Collections.Generic;

namespace RefinanceCore.DAL.Models.DbModels
{
    internal partial class DbQuota
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int Purpose { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }

        public DbCitiy City { get; set; }
        public DbUser User { get; set; }
    }
}
