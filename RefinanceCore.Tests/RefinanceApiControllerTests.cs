using Microsoft.AspNetCore.Mvc;
using RefinanceCore.Web.Controllers;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;
using RefinanceCore.DAL.Models.ViewModels;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RefinanceCore.Tests
{
    public class RefinanceApiControllerTests
    {
        [Fact]
        public void GetQuotaReturnsNotFoundResultWhenQuotaNotFound()
        {
            //Arrange
            int testId = 1;
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(repo => repo.GetQuota(testId))
                .Returns(null as Quota);
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.GetQuota(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetList()
        {
            //Arrange
            int userId = 1;
            string login = "iogin";
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllQuotas(userId)).Returns(GetViewQuotas(userId));
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            mock.Setup(o => o.GetUser(login)).Returns(GetTestUser(login));
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            //var l = Jso
        }

        [Fact]
        public void AddQuota()
        {
            //Arrange
            int userId = 1;
            string login = "iogin";
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllQuotas(userId)).Returns(GetViewQuotas(userId));
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            mock.Setup(o => o.GetUser(login)).Returns(GetTestUser(login));
            var controller = new RefinanceApiController(mock.Object);
            var newQuota = new Quota() { CityId = 1, Purpose = DAL.Enums.Purpose.Mortgage, Amount = 2500000M, CreateDate = DateTime.Now, Comment = "Ипотека", UserId = 1 };

            //Act
            var result = controller.Post(newQuota);

            //Assert
            mock.Verify(r => r.AddQuota(newQuota));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteQuota()
        {
            //Arrange
            int userId = 1;
            int qId = 2;
            string login = "iogin";
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllQuotas(userId)).Returns(GetViewQuotas(userId));
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            mock.Setup(o => o.GetUser(login)).Returns(GetTestUser(login));
            mock.Setup(o => o.GetQuota(qId)).Returns(GetTestQuota(qId));
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.Delete(qId);

            //Assert
            mock.Verify(r => r.DeleteQuota(qId));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteNotFoundQuota()
        {
            //Arrange
            int userId = 1;
            string login = "iogin";
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllQuotas(userId)).Returns(GetViewQuotas(userId));
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            mock.Setup(o => o.GetUser(login)).Returns(GetTestUser(login));
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.Delete(100);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditQuota()
        {
            //Arrange
            int userId = 1;
            int qId = 2;
            string login = "iogin";
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllQuotas(userId)).Returns(GetViewQuotas(userId));
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            mock.Setup(o => o.GetUser(login)).Returns(GetTestUser(login));
            mock.Setup(o => o.GetQuota(qId)).Returns(GetTestQuota(qId));
            var controller = new RefinanceApiController(mock.Object);
            var editedQuota = GetQuotas(userId).Single(o => o.Id == qId);
            editedQuota.Amount = 450000M;
            editedQuota.Comment = "editedcomment";

            //Act
            var result = controller.Put(editedQuota);

            //Assert
            mock.Verify(r => r.EditQuota(editedQuota));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetReportReturnsNotFoundResultWhenQuotaNotFound()
        {
            //Arrange
            int testId = 1;
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(repo => repo.GetQuota(testId))
                .Returns(null as Quota);
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.GetReport(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetReportReturnsBadRequestResultWhenIdNull()
        {
            //Arrange
            var mock = new Mock<IDataBaseManager>();

            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.GetReport(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetReportReturnsFileResult()
        {
            //Arrange
            int testId = 1;
            var testQ = GetQuotas(1).Single(o => o.Id == testId);
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(repo => repo.GetQuota(testId))
                .Returns(testQ);
            var controller = new RefinanceApiController(mock.Object);

            //Act
            var result = controller.GetReport(testId);

            //Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("text/html", fileResult?.ContentType);
            Assert.Equal("report.html", fileResult?.FileDownloadName);
        }

        private Quota GetTestQuota (int id)
        {
            var quotas = GetQuotas(1);

            return quotas.SingleOrDefault(o => o.Id == id);
        }

        private List<Quota> GetQuotas(int userId)
        {
            DateTime createDate = new DateTime(2018, 9, 1);

            var cities = GetTestCities(); var conrs = GetContributions();

            var quotas = new List<Quota>
            {
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=1, CityId=1, Amount = 10000M, Comment="comment1", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=2, CityId=1, Amount = 400000M, Comment="comment2", Purpose=DAL.Enums.Purpose.CarLoan, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==1), conrs.Where(o=>o.CityId==1).ToList()) { Id=3, CityId=1, Amount = 1000000M, Comment="comment3", Purpose=DAL.Enums.Purpose.Mortgage, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==2), conrs.Where(o=>o.CityId==2).ToList()) { Id=4, CityId=2, Amount = 47000M, Comment="comment4", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==3), conrs.Where(o=>o.CityId==3).ToList()) { Id=5, CityId=3, Amount = 800000M, Comment="comment5", Purpose=DAL.Enums.Purpose.CarLoan, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==4), conrs.Where(o=>o.CityId==4).ToList()) { Id=6, CityId=4, Amount = 60000M, Comment="comment6", Purpose=DAL.Enums.Purpose.ConsumerLoan, CreateDate=createDate, UserId=userId },
                new Quota (cities.Single(o => o.Id==4), conrs.Where(o=>o.CityId==4).ToList()) { Id=7, CityId=4, Amount = 2000000M, Comment="comment7", Purpose=DAL.Enums.Purpose.Mortgage, CreateDate=createDate, UserId=userId },
            };

            return quotas;
        }

        private List<QuotaViewModel> GetViewQuotas(int userId)
        {
            var quotas = GetQuotas(userId);
            return quotas.Select(o => new QuotaViewModel
            {
                Id = o.Id,
                Amount = o.Amount,
                CityName = o.City.Name,
                QuotaPurpose = o.Purpose,
            }).ToList();
        }

        private List<Contribution> GetContributions()
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

        private List<City> GetTestCities()
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

        private User GetTestUser(string login)
        {
            return new User { Login = login, Id = 1 };
        }
    }
}
