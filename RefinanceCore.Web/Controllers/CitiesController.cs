using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;
using RefinanceCore.DAL.Models.ViewModels;

namespace RefinanceCore.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private readonly IDataBaseManager db;

        public CitiesController(IDataBaseManager db)
        {
            this.db = db;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var result = db.GetAllCities();
            return Json(result);
        }
    }
}