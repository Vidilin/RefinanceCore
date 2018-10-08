using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.DAL.DataManagers
{
    public class DataBase : IDataBaseManager
    {
        public readonly string _connectionString;

        private ICitiesManager MRCities { get; set; }
        private IContributionsManager MRContributions { get; set; }
        private IQuotasManager MRQuotas { get; set; }
        private IUsersManager MRUsers { get; set; }

        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
            MRCities = new MRCities(connectionString);
            MRContributions = new MRContributions(connectionString);
            MRQuotas = new MRQuotas(connectionString);
            MRUsers = new MRUsers(connectionString);
        }

        public User GetUser(string login)
        {
            return MRUsers.GetUser(login);
        }

        public Quota GetQuota(int id)
        {
            return MRQuotas.GetQuota(id);
        }

        public IList<Models.ViewModels.QuotaViewModel> GetAllQuotas(int userId)
        {
            return MRQuotas.GetAllQuotas(userId);
        }

        public void AddQuota(Quota quota)
        {
            MRQuotas.AddQuota(quota);
        }

        public void DeleteQuota(int id)
        {
            MRQuotas.DeleteQuota(id);
        }

        public void EditQuota(Quota quota)
        {
            MRQuotas.EditQuota(quota);
        }

        public City GetCity(int id)
        {
            return MRCities.GetCity(id);
        }

        public IList<City> GetAllCities()
        {
            return MRCities.GetAllCities();
        }

        public Contribution GetContribution(int id)
        {
            return MRContributions.GetContribution(id);
        }

        public IList<Contribution> GetContributionByCity(int cityId)
        {
            return MRContributions.GetContributionByCity(cityId);
        }
    }
}
