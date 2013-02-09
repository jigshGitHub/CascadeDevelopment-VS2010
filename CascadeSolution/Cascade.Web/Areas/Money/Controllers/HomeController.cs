using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.Money.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //
        // GET: /Money/Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            //This will return blank view for ADD feature
            return View();
        }

        public JsonResult GetMoneyData(int id)
        {
            MoneyDataRepository moneyRepo = new MoneyDataRepository();
            var _moneyData = from _money in moneyRepo.GetAll().Distinct()
                             where _money.MID == id
                             select _money;
            //return 
            return Json(_moneyData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Add(Tbl_Money _moneyform)
        {
            MoneyDataRepository repository;
            _moneyform.CreatedAt = DateTime.Now;
            try
            {
                repository = new MoneyDataRepository();
                if (_moneyform.MID > 0)
                {
                    repository.Update(_moneyform);
                }
                else
                {
                    repository.Add(_moneyform);
                }
            }
            catch (Exception ex)
            {

            }
            //return 
            return Json(_moneyform, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            //return the view
            return View();
        }

        public ActionResult GetAllMoneyRecords(Decimal? Amount, int Source)
        {
            var dataQueries = new DataQueries();
            
            IEnumerable<MoneyViewEditResult> results = dataQueries.GetMoneyViewEditRecords(Amount, Source);
            #region Store selected Criteria in the VieBag for Export to Excel use
            if (Amount != null)
            {
                ViewBag.Amount = Amount;
            }
            else
            {
                ViewBag.Amount = null;
            }
            if (Source != null)
            {
                ViewBag.Source = Source;
            }
            else
            {
                ViewBag.Source = null;
            }
            #endregion
            //show the results
            return PartialView("_moneyRecords", results.ToList());

        }

    }
}
