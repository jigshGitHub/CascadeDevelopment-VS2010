using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.People.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //
        // GET: /People/Home/
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            //return the view
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpPost]
        public JsonResult Add(Tbl_People _peopleform)
        {
            PeopleDataRepository repository;
            _peopleform.CreatedAt = DateTime.Now;
            try
            {
                repository = new PeopleDataRepository();
                if (_peopleform.PID > 0)
                {
                    repository.Update(_peopleform);
                }
                else
                {
                    repository.Add(_peopleform);
                }
            }
            catch (Exception ex)
            {

            }
            //return 
            return Json(_peopleform, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeopleData(int id)
        {
            PeopleDataRepository peopleRepo = new PeopleDataRepository();
            var _peopleData = from _people in peopleRepo.GetAll().Distinct()
                              where _people.PID == id
                              select _people;
            //return 
            return Json(_peopleData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllPeopleRecords(string FirstName, string LastName)
        {
            var dataQueries = new DataQueries();
            if (FirstName == "")
            {
                FirstName = null;
            }
            if (LastName == "")
            {
                LastName = null;
            }
            IEnumerable<PeopleViewEditResult> results = dataQueries.GetPeopleViewEditRecords(FirstName, LastName);
            #region Store selected Criteria in the VieBag for Export to Excel use
            if (FirstName != null)
            {
                ViewBag.FirstName = FirstName;
            }
            else
            {
                ViewBag.FirstName = null;
            }
            if (LastName != null)
            {
                ViewBag.LastName = LastName;
            }
            else
            {
                ViewBag.LastName = null;
            }
            #endregion
            //show the results
            return PartialView("_peopleRecords", results.ToList());

        }
        
        public ActionResult Export(string FirstName, string LastName)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<PeopleViewEditResult> results = dataQueries.GetPeopleViewEditRecords(FirstName, LastName);
            //return View();
            return PartialView("Export", results.ToList());
        }

    }
}
