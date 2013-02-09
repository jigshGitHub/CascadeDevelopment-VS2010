using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using JQueryDataTables.Models;
using JQueryDataTables.Models.Repository;
using Cascade.Web.Presentation.ExtensionMethods;

namespace Cascade.Web.Areas.Collections.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Collections/Home/

        public ActionResult Index()
        {
            return Redirect("https://hilcolegal.csofinancial.com:8443/hilcolegal/");
        }

        public ActionResult GetAllDPSRecords()
        {
            //Get the View Repository
            PortDPSRepository portDPSRepo = new PortDPSRepository();
            //Get the Report Data
            var _portDPSData = from portDPS in portDPSRepo.GetAll().Distinct()
                               select portDPS;
            //return the data
            return PartialView("_dpsRecords", _portDPSData.ToList());
            
        }

        //public ActionResult AjaxHandler(JQueryDataTableParamModelColl param)
        //{
        //    var allDPS = DataRepositoryColl.GetAllDPS();
        //    IEnumerable<Port_DPS> filteredDPSs;

        //    //Check whether the companies should be filtered by keyword
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        //Used if particulare columns are filtered 
        //        var portfolioFilter = Convert.ToString(Request["sSearch_1"]);
        //        var nameFilter = Convert.ToString(Request["sSearch_2"]);

        //        //Optionally check whether the columns are searchable at all 
        //        var isPortfolioSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
        //        var isNameSearchable = Convert.ToBoolean(Request["bSearchable_2"]);

        //        filteredDPSs = DataRepositoryColl.GetAllDPS()
        //           .Where(c => isPortfolioSearchable && c.Portfolio_.ToLower().Contains(param.sSearch.ToLower())
        //            ||
        //            isNameSearchable && c.AcctName.ToString().ToLower().Contains(param.sSearch.ToLower())
        //           );
        //    }
        //    else
        //    {
        //        filteredDPSs = allDPS;
        //    }

        //    var isPortfolioSortable = Convert.ToBoolean(Request["bSortable_1"]);
        //    var isAmountSortable = Convert.ToBoolean(Request["bSortable_2"]);
        //    var isDateRecSortable = Convert.ToBoolean(Request["bSortable_3"]);
        //    var isTranDateSortable = Convert.ToBoolean(Request["bSortable_4"]);
        //    var isNameSortable = Convert.ToBoolean(Request["bSortable_5"]);
        //    var isOrigAccountSortable = Convert.ToBoolean(Request["bSortable_6"]);
        //    var isPIMSActSortable = Convert.ToBoolean(Request["bSortable_7"]);

        //    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        //    Func<Port_DPS, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Portfolio_ :
        //                                                   sortColumnIndex == 2 && isOrigAccountSortable ? Convert.ToString(c.Amount) :
        //                                                   sortColumnIndex == 3 && isPIMSActSortable ? Convert.ToString(c.DateRec) :
        //                                                   sortColumnIndex == 4 && isAmountSortable ? Convert.ToString(c.TransDate) :
        //                                                   sortColumnIndex == 5 && isPortfolioSortable ? c.AcctName :
        //                                                   sortColumnIndex == 6 && isDateRecSortable ? c.OrigAcct_ :
        //                                                   sortColumnIndex == 7 && isTranDateSortable ? c.PIMSAcct_ :
        //                                                   "");

        //    var sortDirection = Request["sSortDir_0"]; // asc or desc
        //    if (sortDirection == "asc")
        //        filteredDPSs = filteredDPSs.OrderBy(orderingFunction);
        //    else
        //        filteredDPSs = filteredDPSs.OrderByDescending(orderingFunction);

        //    var displayedDPSs = filteredDPSs.Skip(param.iDisplayStart).Take(param.iDisplayLength);
        //    //var result = from c in displayedCompanies select new[] { Convert.ToString(c.ID), c.Name, c.Address, c.Town };
        //    var result = from c in displayedDPSs
        //                 select new[] { Convert.ToString(c.ID),
        //                                c.Portfolio_,
        //                                c.Amount.ToIntegerCurrency(),
        //                                c.DateRec.ConverToDate(),
        //                                c.TransDate.ConverToDate(),
        //                                c.AcctName, 
        //                                c.OrigAcct_, 
        //                                c.PIMSAcct_
        //    };

        //    return Json(new
        //    {
        //        sEcho = param.sEcho,
        //        iTotalRecords = allDPS.Count(),
        //        iTotalDisplayRecords = filteredDPSs.Count(),
        //        aaData = result
        //    },
        //                JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult AjaxHandler(JQueryDataTableParamModelColl param)
        //{
        //    var allportfolios = DataRepositoryColl.GetPortfolios();
        //    IEnumerable<TBL_Portfolio> filteredPortfolios;
        //    //Check whether the companies should be filtered by keyword
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        //Used if particulare columns are filtered 
        //        var portfolioFilter = Convert.ToString(Request["sSearch_1"]);
        //        var accountFilter = Convert.ToString(Request["sSearch_2"]);
                
        //        //Optionally check whether the columns are searchable at all 
        //        var isPortfolioSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
        //        var isAccountSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                
        //        filteredPortfolios = DataRepositoryColl.GetPortfolios()
        //           .Where(c => isPortfolioSearchable && c.PortfolioName.ToLower().Contains(param.sSearch.ToLower())
        //            ||
        //            isAccountSearchable && c.ACCOUNT.ToString().ToLower().Contains(param.sSearch.ToLower())
        //           );
        //    }
        //    else
        //    {
        //        filteredPortfolios = allportfolios;
        //    }

        //    var isportfolioSortable = Convert.ToBoolean(Request["bSortable_1"]);
        //    var isAccountSortable = Convert.ToBoolean(Request["bSortable_2"]);
        //    var isPortfolioOwnerSortable = Convert.ToBoolean(Request["bSortable_3"]);
        //    var isNameSortable = Convert.ToBoolean(Request["bSortable_4"]);
        //    var isPayDateSortable = Convert.ToBoolean(Request["bSortable_5"]);
        //    var isPayAmtSortable = Convert.ToBoolean(Request["bSortable_6"]);
        //    var isDaysSinceLastPaySortable = Convert.ToBoolean(Request["bSortable_7"]);
        //    var isBalanceSortable = Convert.ToBoolean(Request["bSortable_8"]);
        //    var isAccuredInterestSortable = Convert.ToBoolean(Request["bSortable_9"]);
        //    var isTotalnterestSortable = Convert.ToBoolean(Request["bSortable_10"]);
        //    var isAddress1Sortable = Convert.ToBoolean(Request["bSortable_11"]);
        //    var isCitySortable = Convert.ToBoolean(Request["bSortable_12"]);
        //    var isStateSortable = Convert.ToBoolean(Request["bSortable_13"]);
        //    var isZipCodeSortable = Convert.ToBoolean(Request["bSortable_14"]);
        //    var isWorkStatusSortable = Convert.ToBoolean(Request["bSortable_15"]);
        //    var isWorkGroupSortable = Convert.ToBoolean(Request["bSortable_16"]);
                        
        //    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        //    Func<TBL_Portfolio, string> orderingFunction = (c => sortColumnIndex == 1 && isportfolioSortable ? c.PortfolioName  :
        //                                                   sortColumnIndex == 2 && isAccountSortable ? Convert.ToString(c.ACCOUNT) :
        //                                                   sortColumnIndex == 3 && isPortfolioOwnerSortable ? c.PortfolioOwner :
        //                                                   sortColumnIndex == 4 && isNameSortable ? c.NAME :
        //                                                   sortColumnIndex == 5 && isPayDateSortable ? Convert.ToString(c.LAST_PAY_DATE) :
        //                                                   sortColumnIndex == 6 && isPayAmtSortable ? Convert.ToString(c.LAST_PAY_AMT) :
        //                                                   sortColumnIndex == 7 && isDaysSinceLastPaySortable ? Convert.ToString(c.DaysSinceLastPay) :
        //                                                   sortColumnIndex == 8 && isBalanceSortable ? Convert.ToString(c.Balance) :
        //                                                   sortColumnIndex == 9 && isAccuredInterestSortable ? Convert.ToString(c.AccruedInterest) :
        //                                                   sortColumnIndex == 10 && isTotalnterestSortable ? Convert.ToString(c.TotalInterest) :
        //                                                   sortColumnIndex == 11 && isAddress1Sortable ? c.ADDRESS1 :
        //                                                   sortColumnIndex == 12 && isCitySortable ? c.CITY :
        //                                                   sortColumnIndex == 13 && isStateSortable ? c.STATE :
        //                                                   sortColumnIndex == 14 && isZipCodeSortable ? Convert.ToString(c.ZIP_CODE) :
        //                                                   sortColumnIndex == 15 && isZipCodeSortable ? c.WorkStatusDescription :
        //                                                   sortColumnIndex == 16 && isZipCodeSortable ? c.WorkGroup :
        //                                                   "");

        //    var sortDirection = Request["sSortDir_0"]; // asc or desc
        //    if (sortDirection == "asc")
        //        filteredPortfolios = filteredPortfolios.OrderBy(orderingFunction);
        //    else
        //        filteredPortfolios = filteredPortfolios.OrderByDescending(orderingFunction);

        //    var displayedPortfolios = filteredPortfolios.Skip(param.iDisplayStart).Take(param.iDisplayLength);
        //    //var result = from c in displayedCompanies select new[] { Convert.ToString(c.ID), c.Name, c.Address, c.Town };
        //    var result = from c in displayedPortfolios select new[] { Convert.ToString(c.ACCOUNT),Convert.ToString(c.ACCOUNT), c.PortfolioName, 
        //        c.PortfolioOwner, c.NAME 
        //        , Convert.ToString(c.LAST_PAY_DATE).Replace("12:00:00 AM","") 
        //        , string.Format("{0:C}", Convert.ToDecimal(Convert.ToString(c.LAST_PAY_AMT)))
        //        , Convert.ToString(c.DaysSinceLastPay)
        //        , string.Format("{0:C}", Convert.ToDecimal(Convert.ToString(c.Balance))) 
        //        , string.Format("{0:C}", Convert.ToDecimal(Convert.ToString(c.AccruedInterest))) 
        //        , string.Format("{0:C}", Convert.ToDecimal(Convert.ToString(c.TotalInterest)))  
        //        , c.ADDRESS1, c.CITY , c.STATE
        //        ,Convert.ToString(c.ZIP_CODE) , c.WorkStatusDescription , c.WorkGroup 
        //    };
           
        //    return Json(new
        //    {
        //        sEcho = param.sEcho,
        //        iTotalRecords = allportfolios.Count(),
        //        iTotalDisplayRecords = filteredPortfolios.Count(),
        //        aaData = result
        //    },
        //                JsonRequestBehavior.AllowGet);
        //}


        ////
        //// GET: /Collections/Home/Details/5

        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            //return the view
            return View();
        }

        public JsonResult GetDPSData(int id)
        {
            PortDPSRepository portDPSRepo = new PortDPSRepository();
            var _portDPSData = from _portDPS in portDPSRepo.GetAll().Distinct()
                               where _portDPS.ID == id
                               select _portDPS;
            //return _dpsform;
            return Json(_portDPSData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        ////
        //// GET: /Collections/Home/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Collections/Home/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Collections/Home/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Collections/Home/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Collections/Home/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Collections/Home/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
