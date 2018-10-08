using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.DataManagers;
using Microsoft.EntityFrameworkCore;
using RefinanceCore.Web.Models;

namespace RefinanceCore.Web.Controllers
{
    public class RefinanceController : Controller
    {
        private readonly IDataBaseManager _db;

        public RefinanceController(IDataBaseManager db)
        {
            _db = db;
        }

        public IActionResult Quotas(int page = 1)
        {
            int pageSize = 5;

            var user = _db.GetUser(@"iogin");
            ViewBag.Title = user.Login;
            var source = _db.GetAllQuotas(user.Id);

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Quotas = items
            };

            return View(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}