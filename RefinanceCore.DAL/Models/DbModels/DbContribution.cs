using System;
using System.Collections.Generic;

namespace RefinanceCore.DAL.Models.DbModels
{
    internal partial class DbContribution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseAmount { get; set; }
        public int CityId { get; set; }

        public DbCitiy City { get; set; }
    }
}
