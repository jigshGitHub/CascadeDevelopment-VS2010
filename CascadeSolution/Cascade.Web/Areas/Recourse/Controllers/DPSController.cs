using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using Cascade.Web.Presentation.ViewModels.DPS;
using Cascade.Web.Areas.Recourse.Models;

namespace Cascade.Web.Areas.Recourse.Controllers
{
    public class DPSController : BaseController
    {
        //
        // GET: /Recourse/DPS/

        [HttpGet]
        public ActionResult Index()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpPost]
        public JsonResult Add(MSI_DPSForm _dpsform)
        {
            MSIDPSFormDataRepository repository;
            try
            {
                repository = new MSIDPSFormDataRepository();
                if (_dpsform.ID > 0)
                {
                    repository.Update(_dpsform);
                }
                else
                {
                    repository.Add(_dpsform);
                }
            }
            catch (Exception ex)
            {

            }
            //return _dpsform;
            return Json(_dpsform, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(MSI_DPSForm _dpsform)
        {
            vwAccount dpsFormData = null;
            vwAccountRepository viewRepository = null;
            try
            {
                viewRepository = new vwAccountRepository();
                if (_dpsform.PIMSAcct != null)
                {
                    dpsFormData = viewRepository.Get(x => x.ACCOUNT == _dpsform.PIMSAcct);

                }
                else
                {
                    dpsFormData = viewRepository.Get(x => x.OriginalAccount == _dpsform.OriginalAcct);
                }
                //Filled data obtained from the Database
                _dpsform.AcctName = dpsFormData.NAME;
                _dpsform.CurrentResp = dpsFormData.RESPONSIBILITY;
                _dpsform.Portfolio = dpsFormData.Portfolio;
                _dpsform.PIMSAcct = dpsFormData.ACCOUNT;
                _dpsform.OriginalAcct = dpsFormData.OriginalAccount;
                _dpsform.GUID = System.Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                //We have some issue
            }
            //return the Json Object
            return Json(_dpsform, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult ViewDPS()
        {
            //This will display all DPS Records
            return View();
        }
                
        public ActionResult GetAllDPSRecords(DateTime? StartDate, DateTime? EndDate, int? Portfolio, string Responsibility, string Account, string GUID)
        {
            var dataQueries = new DataQueries();
            string _portfolioowner = null;
            RProductCodeRepository rProdCodeRepo = new RProductCodeRepository();
            if (Portfolio != null)
            {
                _portfolioowner = rProdCodeRepo.Get(x => x.ProductID == Portfolio).PRODUCT_CODE;
            }
            if (Responsibility == "undefined")
            {
                Responsibility = null;
            }
            if (GUID == "")
            {
                GUID = null;
            }
            if (Account == "")
            {
                Account = null;
            }
            IEnumerable<DPSViewEditResult> results = dataQueries.GetDPSViewEditRecords(StartDate, EndDate, _portfolioowner, Responsibility, Account, GUID);
            #region Store selected Criteria in the VieBag for Export to Excel use
            if (StartDate != null && EndDate != null)
            {
                ViewBag.StartDate = StartDate.ToString();
                ViewBag.EndDate = EndDate.ToString();
            }
            else
            {
                ViewBag.StartDate = null;
                ViewBag.EndDate = null;
            }
            if (_portfolioowner != null)
            {
                ViewBag.PortfolioOwner = _portfolioowner;
            }
            else
            {
                ViewBag.PortfolioOwner = null;
            }
            if (Responsibility != null)
            {
                ViewBag.Responsibility = Responsibility;
            }
            else
            {
                ViewBag.Responsibility = null;
            }
            if (Account != null)
            {
                ViewBag.Account = Account;
            }
            else
            {
                ViewBag.Account = null;
            }
            if (GUID != null)
            {
                ViewBag.GUID = GUID;
            }
            else
            {
                ViewBag.GUID = null;
            }
            #endregion
            //show the results
            return PartialView("_dpsRecords", results.ToList());

        }
                
        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            //return the view
            return View();
        }

        public JsonResult GetDPSData(int id)
        {
            MSIDPSFormDataRepository portDPSRepo = new MSIDPSFormDataRepository();
            var _portDPSData = from _portDPS in portDPSRepo.GetAll().Distinct()
                               where _portDPS.ID == id
                               select _portDPS;
            //return _dpsform;
            return Json(_portDPSData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Export(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<DPSViewEditResult> results = dataQueries.GetDPSViewEditRecordsExport(StartDate, EndDate, PortfolioOwner, Responsibility, Account, GUID);
            //return View();
            return PartialView("Export", results.ToList());
        }

    }
}
