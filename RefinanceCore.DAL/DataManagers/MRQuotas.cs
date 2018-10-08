using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;
using RefinanceCore.DAL.Models.ViewModels;
using System.Data.SqlClient;
using System.Linq;
using RefinanceCore.DAL.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace RefinanceCore.DAL.DataManagers
{
    public class MRQuotas : Abstract.DataManagerBase, Interfaces.IQuotasManager
    {
        private readonly string _connectionString;

        public MRQuotas(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Quota GetQuota(int id)
        {
            using (var db = GetConnect(_connectionString))
            {
                var dbQuota = db.Quotas.Where(o => o.Id == id/* && o.UserId == userId*/).SingleOrDefault();

                if (dbQuota == null)
                {
                    return null;
                }

                var result = new Quota
                {
                    Id = dbQuota.Id,
                    Purpose = (Enums.Purpose)dbQuota.Purpose,
                    Amount = dbQuota.Amount,
                    CreateDate = dbQuota.CreateDate,
                    ModifyDate = dbQuota.ModifyDate,
                    Comment = dbQuota.Comment,
                    UserId = dbQuota.UserId,
                    CityId = dbQuota.CityId
                };

                result.City = db.Cities.Where(o => o.Id == dbQuota.CityId).Select(o => new City
                {
                    Id = o.Id,
                    Name = o.Name,
                    SignificanceLevel = o.SignificanceLevel
                }).SingleOrDefault();

                result.SetContributions(db.Contributions.Where(o => o.CityId == result.City.Id).Select(o => new Contribution
                {
                    Id = o.Id,
                    CityId = o.CityId,
                    Name = o.Name,
                    BaseAmount = o.BaseAmount,
                }).ToList());

                return result;
            }
        }

        public IList<QuotaViewModel> GetAllQuotas(int userId)
        {
            using (var db = GetConnect(_connectionString))
            {
                return db.Quotas.Where(o => o.UserId == userId).Select(o => new QuotaViewModel
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    CityName = o.City.Name,
                    QuotaPurpose = (Enums.Purpose)o.Purpose,
                }).ToList();
            }
        }

        public void AddQuota(Quota quota)
        {
            using (var db = GetConnect(_connectionString))
            {
                quota.CreateDate = DateTime.Now;
                db.Database.ExecuteSqlCommand
                    ("INSERT INTO Quotas (CityId, Purpose, Amount, CreateDate, Comment, UserId) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                        quota.CityId, (int)quota.Purpose, quota.Amount, quota.CreateDate, quota.Comment, quota.UserId);
            }
        }

        public void DeleteQuota(int id)
        {
            using (var db = GetConnect(_connectionString))
            {
                var deletedRow = db.Quotas.Where(o => o.Id == id).SingleOrDefault();
                if (deletedRow != null)
                {
                    db.Quotas.Remove(deletedRow);
                    db.SaveChanges();
                }              
            }
        }

        public void EditQuota(Quota quota)
        {
            using (var db = GetConnect(_connectionString))
            {
                var editedRow = db.Quotas.Where(o => o.Id == quota.Id).SingleOrDefault();

                if (editedRow != null)
                {
                    editedRow.ModifyDate = DateTime.Now;
                    editedRow.Amount = quota.Amount;
                    editedRow.CityId = quota.CityId;
                    editedRow.Comment = quota.Comment;
                    editedRow.Purpose = (int)quota.Purpose;

                    db.SaveChanges();
                }
            }
        }
    }
}
