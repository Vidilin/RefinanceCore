using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using RefinanceCore.DAL;
using RefinanceCore.DAL.DataManagers;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;

namespace RefinanceCore.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string connection = @"Data Source=T3\SQLEXPRESSLOCAL;Initial Catalog=DBRefinanceCore;Integrated Security=True";
            //string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBRefinanceCore;Integrated Security=True";

            //IDataBaseManager db = new DataBase(connection);

            //var user = db.GetUser("iogin");

            //IList<Quota> quotas = new List<Quota>();

            //quotas.Add(new Quota
            //{
            //    Amount = 50000M,
            //    CityId = 1,
            //    Purpose = DAL.Enums.Purpose.Mortgage,
            //    UserId = user.Id,
            //    Comment = "Коммент"
            //});

            //quotas.Add(new Quota
            //{
            //    Amount = 100000M,
            //    CityId = 2,
            //    Purpose = DAL.Enums.Purpose.ConsumerLoan,
            //    UserId = user.Id,
            //    Comment = "Комментарий"
            //});

            //quotas.Add(new Quota
            //{
            //    Amount = 110000M,
            //    CityId = 3,
            //    Purpose = DAL.Enums.Purpose.CarLoan,
            //    UserId = user.Id,
            //    Comment = "Кшм"
            //});

            //quotas.Add(new Quota
            //{
            //    Amount = 150000M,
            //    CityId = 4,
            //    Purpose = DAL.Enums.Purpose.Mortgage,
            //    UserId = user.Id,
            //    Comment = "Лом"
            //});

            //foreach (var q in quotas)
            //{
            //    db.MRQuotas.AddQuota(q);
            //}           

            var quotasList = GetQuotas();//db.GetAllQuotas(user.Id);

            //string writePath = @"F:\1vidilin\Projects\netCore\RefinanceCore\test.txt";

            //using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
            {
                //sw.WriteLine(text);


                foreach (var q in quotasList)
                {
                    //var qd = db.GetQuota(q.Id);
                    Console.WriteLine("Number {0}", q.Id);
                    Console.WriteLine("City {0}", q.City.Name);
                    Console.WriteLine("Purpose {0}", q.Purpose);
                    Console.WriteLine("Amount {0}", q.Amount);
                    Console.WriteLine("CreateDate {0}", q.CreateDate.ToShortDateString());
                    Console.WriteLine("Comment {0}", q.Comment);
                    Console.WriteLine("InterestRate {0}", q.InterestRate);
                    Console.WriteLine("====================================");

                    foreach (var contr in q.QuotaContributions)
                    {
                        Console.WriteLine("Contribution {0}, AdditionalPayment {1}", contr.Name, contr.AdditionalPayment);
                    }

                    Console.WriteLine("Contributions total {0}", q.QuotaContributions.Sum(o => o.AdditionalPayment));
                }
            }
            //db.MRCities.Get

            //var login = "login";

            Console.ReadLine();
        }

        private static List<Quota> GetQuotas()
        {
            DateTime createDate = new DateTime(2018, 9, 1);

            var cities = GetTestCities(); var conrs = GetContributions();

            var quotas = new List<Quota>
            {
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=1, CityId=1, Amount = 10000M, Comment="comment1", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=2, CityId=1, Amount = 400000M, Comment="comment2", Purpose=DAL.Enums.Purpose.CarLoan, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=3, CityId=1, Amount = 1000000M, Comment="comment3", Purpose=DAL.Enums.Purpose.Mortgage, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==2), conrs.Where(o=>o.CityId==2).ToList()) { Id=4, CityId=2, Amount = 47000M, Comment="comment4", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==3), conrs.Where(o=>o.CityId==3).ToList()) { Id=5, CityId=3, Amount = 800000M, Comment="comment5", Purpose=DAL.Enums.Purpose.CarLoan, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==4), conrs.Where(o=>o.CityId==4).ToList()) { Id=6, CityId=4, Amount = 60000M, Comment="comment6", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=1 },
                new Quota (cities.Single(o => o.Id==4), conrs.Where(o=>o.CityId==4).ToList()) { Id=7, CityId=4, Amount = 2000000M, Comment="comment7", Purpose=DAL.Enums.Purpose.Mortgage, CreateDate=createDate, UserId=1 },
            };

            return quotas;
        }

        private static List<Contribution> GetContributions()
        {
            var cotrs = new List<Contribution>
            {
                new Contribution { Id=1, CityId=1, Name="Удаленность", BaseAmount=10},
                new Contribution { Id=2, CityId=1, Name="Плохие дороги", BaseAmount=5},
                new Contribution { Id=3, CityId=2, Name="Плохая экология", BaseAmount=8},
                new Contribution { Id=4, CityId=3, Name="Омск", BaseAmount=3},
                new Contribution { Id=5, CityId=3, Name="ОМСК", BaseAmount=6},
                new Contribution { Id=6, CityId=4, Name="Административный центр", BaseAmount=7},
            };

            return cotrs;
        }

        private static List<City> GetTestCities()
        {
            var cities = new List<City>
            {
                new City { Id=1, Name="Хабаровск", SignificanceLevel = 5},
                new City { Id=2, Name="Челябинск", SignificanceLevel = 10},
                new City { Id=3, Name="Омск", SignificanceLevel = 1},
                new City { Id=4, Name="Воронеж", SignificanceLevel = 3},
            };
            return cities;
        }
    }
}
