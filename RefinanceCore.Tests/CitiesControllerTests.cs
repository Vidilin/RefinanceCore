using Microsoft.AspNetCore.Mvc;
using RefinanceCore.Web.Controllers;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace RefinanceCore.Tests
{
    public class CitiesControllerTests
    {
        [Fact]
        public void ReturnsAllCities()
        {
            //Arrange
            var mock = new Mock<IDataBaseManager>();
            mock.Setup(o => o.GetAllCities()).Returns(GetTestCities());
            var controller = new CitiesController(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
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
    }
}
