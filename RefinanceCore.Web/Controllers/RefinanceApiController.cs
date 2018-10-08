using Microsoft.AspNetCore.Mvc;
using RefinanceCore.DAL.Interfaces;
using RefinanceCore.DAL.Models;
using RefinanceCore.Web.Models;
using System;
using System.Linq;

namespace RefinanceCore.Web.Controllers
{
    [Produces("application/json")]
    [Route("/api/RefinanceApi")]
    public class RefinanceApiController : Controller
    {
        private readonly IDataBaseManager db;

        public RefinanceApiController(IDataBaseManager db)
        {
            this.db = db;
        }

        [HttpGet, Route("list")]
        public JsonResult Get([FromQuery]int page = 1)
        {
            const int pageSize = 5;
            var user = db.GetUser(@"iogin");
            var source = db.GetAllQuotas(user.Id);
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel result = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Quotas = items
            };
            return Json(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuota(int id)
        {
            //var user = db.MRUsers.GetUser(@"iogin");
            Quota quota = db.GetQuota(id);
            if (quota == null)
                return NotFound();
            return Json(quota);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Quota newRow)
        {
            if (newRow == null)
            {
                ModelState.AddModelError("", "Не указаны данные");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = db.GetUser(@"iogin");
            newRow.UserId = user.Id;

            db.AddQuota(newRow);
            return Ok(newRow);
        }

        [HttpPut]
        public IActionResult Put([FromBody]Quota editedRow)
        {
            if (editedRow == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldRow = db.GetQuota(editedRow.Id);

            if (oldRow == null)
            {
                return NotFound();
            }

            db.EditQuota(editedRow);
            return Ok(editedRow);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedRow = db.GetQuota(id);

            if (deletedRow == null)
            {
                return NotFound();
            }

            db.DeleteQuota(deletedRow.Id);
            return Ok(deletedRow);
        }

        [HttpGet, Route("calc")]
        public IActionResult GetRate([FromQuery]int? cityId, [FromQuery]int? purpose, [FromQuery]decimal? amount)
        {
            if (cityId == null || purpose == null || amount == null)
            {
                return BadRequest();
            }
            var city = db.GetCity((int)cityId);
            var contrs = db.GetContributionByCity((int)cityId);
            var quota = new Quota(city, contrs) { Amount = (decimal)amount, Purpose = (DAL.Enums.Purpose)purpose };
            return Json(quota);
        }

        [HttpGet, Route("report")]
        public IActionResult GetReport([FromQuery]int? quotaId)
        {
            if (quotaId == null)
                return BadRequest();

            Quota quota = db.GetQuota((int)quotaId);
            if (quota == null)
                return NotFound();

            var result = GetStringReport(quota);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(result);

            return File(bytes, "text/html", "report.html");
        }

        private string GetStringReport(Quota quota)
        {
            var result = $"<!DOCTYPE html>\n<html>\n<head>\n<meta charset=\"utf-8\">\n<title>Квота номер {quota.Id}</title>\n</head>\n<body>\n" +
             $"<p> Номер : {quota.Id}</p>" +
             $"<p> Город : {quota.City.Name}</p>" +
             $"<p> Цель : {quota.StringPurpose}</p>" +
             $"<p> Сумма : {Math.Round(quota.Amount, 2)}</p>" +             
             $"<p> Дата создания: {quota.CreateDate.ToShortDateString()}</p>" +
             $"<p> Комментарий : {quota.Comment}</p>" +
             $"<p> Дополнительные взносы : </p><ul>";
            
            foreach(var row in quota.QuotaContributions)
            {
                result += $"<li><p>Дополнительный взнос - {row.Name} : {row.AdditionalPayment} </p></li>";
            }

            var total = quota.QuotaContributions.Sum(o => o.AdditionalPayment);
            result += $"</ul><p>Итоговый дополнительный взнос : {total} </p>";
            result += $"<p> Итоговая процентная ставка : {quota.InterestRate}%</p>";
            result += $"<p> Дата генерации отчета: {DateTime.Now.ToShortDateString()}</p>\n</body>\n</html>";
            return result;
        }
    }
}