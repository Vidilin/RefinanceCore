using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RefinanceCore.DAL.Models.DataManagers.DbDataManagers;

namespace RefinanceCore.DAL.Abstract
{
    public abstract class DataManagerBase
    {
        internal DBRefinanceCoreContext GetConnect(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBRefinanceCoreContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)             
                .Options;
            return new DBRefinanceCoreContext(options);
        }
    }
}
