using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RefinanceCore.DAL.Models.DbModels;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.DAL.DataManagers
{
    public class MRCities : Abstract.DataManagerBase, Interfaces.ICitiesManager
    {
        private readonly string _connectionString;

        public MRCities(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public City GetCity(int id)
        {
            using (var db = GetConnect(_connectionString))
            {
                return db.Cities.Where(o => o.Id == id).Select(o => new City
                {
                    Id = o.Id,
                    Name = o.Name,
                    SignificanceLevel = o.SignificanceLevel,
                }).SingleOrDefault();
            }
        }

        public IList<City> GetAllCities()
        {
            using (var db = GetConnect(_connectionString))
            {
                return db.Cities.Select(o => new City
                {
                    Id = o.Id,
                    Name = o.Name,
                    SignificanceLevel = o.SignificanceLevel,
                }).ToList();
            }
        }
    }
}
