using System;
using System.Collections.Generic;
using System.Text;
using RefinanceCore.DAL.Models;
using System.Linq;
using RefinanceCore.DAL.Models.DbModels;

namespace RefinanceCore.DAL.DataManagers
{
    public class MRUsers : Abstract.DataManagerBase, Interfaces.IUsersManager
    {
        private readonly string _connectionString;

        public MRUsers(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public User GetUser(string login)
        {
            using (var db = GetConnect(_connectionString))
            {
                //var user = db.Users.FromSql("SELECT TOP 1 * FROM Users WHERE Login = {login}");
                var user = db.Users.SingleOrDefault(o => o.Login == login);
                if (user != null)
                {
                    return new User { Id = user.Id, Login = user.Login };
                }
                else
                {
                    var newUser = new DbUser { Login = login };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return new User { Id = newUser.Id, Login = login };
                }
            }
        }
    }
}
