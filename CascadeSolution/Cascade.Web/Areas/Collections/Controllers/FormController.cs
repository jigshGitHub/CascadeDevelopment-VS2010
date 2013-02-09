using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using JQueryDataTables.Models.Repository;
using Cascade.Web.Presentation.ViewModels.DPS;
using Cascade.Web.Areas.Collections.Models;

namespace Cascade.Web.Areas.Collections.Controllers
{
    public class FormController : BaseController
    {
        //
        // GET: /Collections/Form/
        [HttpGet]
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult GetAllDPS()
        {

            var allDPS = from dps in DataRepositoryColl.GetAllDPS()
                         //select new { ID = dps.ID, DateRec = dps.DateRec };
                         select new Form { ID = dps.ID, DateRec = dps.DateRec, Amount = dps.Amount,  CheckNumber = dps.CheckNumber, 
                                                  Name = dps.AcctName,  NetPayment = dps.Net_Payment, OriginalAccount = dps.OrigAcct_, 
                                                  OurCheck = dps.OurCheckNumber, PIMSAccount = dps.PIMSAcct_, PmtTypeId = dps.PmtType,
                                                  Portfolio = dps.Portfolio_,  Responsibility = dps.CurrentResp, TranDate = dps.TransDate, 
                                                  TransCode = dps.TransCode, TransSourceId = dps.TransSource, Uploaded = dps.Uploaded__y_n_
                                                };  
            return Json(allDPS.ToList(), JsonRequestBehavior.AllowGet);
        }


        public string GetDescriptionDetails(string _descriptionText)
        {
            if (!string.IsNullOrEmpty(_descriptionText))
            {
                //We have value
                return " | " + _descriptionText;
            }
            else
            {
                return "";
            }
        }
        //Get Trans Codes List
        public JsonResult GetTransCodeList()
        {
            var alltransCodes = from transCode in DataRepositoryColl.GetTransCodes()
                                select new { Text = transCode.TransCode + GetDescriptionDetails(transCode.Description), Value = transCode.TransCode };
            //return Json
            return Json(alltransCodes.ToList(), JsonRequestBehavior.AllowGet);

        }
        //Get Trans Sources List
        public JsonResult GetTransSourceList()
        {
            var alltransSources = from transSource in DataRepositoryColl.GetTransSources()
                                  select new { Text = transSource.TransSource + GetDescriptionDetails(transSource.Description), Value = transSource.TransSource };
            //return Json
            return Json(alltransSources.ToList(), JsonRequestBehavior.AllowGet);

        }
        //Get Pmt Types List
        public JsonResult GetPmtTypesList()
        {
            var allpmtTypes = from pmtType in DataRepositoryColl.GetPmtTypes()
                              select new { Text = pmtType.Payment_Type_ID_code + GetDescriptionDetails(pmtType.Payment_Type_ID), Value = pmtType.Payment_Type_ID_code };
            //return Json
            return Json(allpmtTypes.ToList(), JsonRequestBehavior.AllowGet);

        }
        //Get Portfolios
        public JsonResult GetPortfoliosList()
        {
            var portfolios = from portfolio in DataRepositoryColl.GetAllPortfolios()
                             select new { Text = portfolio.Portfolio_, Value = portfolio.Lender_FileDescription };
            //return Json
            return Json(portfolios.ToList(), JsonRequestBehavior.AllowGet);

        }
        //Get Responsibilities
        public JsonResult GetResponsibilitesList()
        {
            var responsibilities = from respo in DataRepositoryColl.GetResponsibilities()
                                   select new { Text = respo.Agency + GetDescriptionDetails(respo.Name), Value = respo.Name };
            //return Json
            return Json(responsibilities.ToList(), JsonRequestBehavior.AllowGet);

        }
        
    }
}
