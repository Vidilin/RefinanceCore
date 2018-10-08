using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.DAL.Interfaces
{
    public interface ICitiesManager
    {
        City GetCity(int id);
        IList<City> GetAllCities();
    }
}
