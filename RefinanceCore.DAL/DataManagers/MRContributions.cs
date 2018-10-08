using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RefinanceCore.DAL.Models.DbModels;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.DAL.DataManagers
{
    public class MRContributions : Abstract.DataManagerBase, Interfaces.IContributionsManager
    {
        private readonly string _connectionString;

        public MRContributions(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Contribution GetContribution(int id)
        {
            using (var db = GetConnect(_connectionString))
            {
                return db.Contributions.Where(o => o.Id == id).Select(o => new Contribution
                {
                    Id = o.Id,
                    BaseAmount = o.BaseAmount,
                    CityId = o.CityId,
                    Name = o.Name,
                }).SingleOrDefault();
            }
        }

        public IList<Contribution> GetContributionByCity(int cityId)
        {
            using (var db = GetConnect(_connectionString))
            {
                return db.Contributions.Where(o => o.CityId == cityId).Select(o => new Contribution
                {
                    Id = o.Id,
                    BaseAmount = o.BaseAmount,
                    CityId = o.CityId,
                    Name = o.Name,
                }).ToList();
            }
        }
    }
}
