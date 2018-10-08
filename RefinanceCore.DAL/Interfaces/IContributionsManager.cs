using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.DAL.Interfaces
{
    public interface IContributionsManager
    {
        Contribution GetContribution(int id);
        IList<Contribution> GetContributionByCity(int cityId);
    }
}
