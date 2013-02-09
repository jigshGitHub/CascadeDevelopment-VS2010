using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
using Cascade.Web.Areas.Recourse.Models;

namespace Cascade.Web.Areas.Reports.Controllers
{
    public class RecallsController : BaseController
    {
        //
        // GET: /Reports/Recalls/
        public ActionResult Index(string id)
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = id;
            return View();
        }

        public ActionResult GetReportData(string ReportType)
        {
             var dataQueries = new DataQueries();
             ViewBag.ReportType = ReportType;
             IEnumerable<RecallViewEditResult> results;
             //For Report based on Report Type selection
             switch (ReportType)
             {
                 case "RecallsUpload":
                     results = dataQueries.GetRecallsUploadReportRecords(null);
                     return PartialView("_recallsUpload", results);

                 case "RecallsNotClosed":
                     results = dataQueries.GetRecallsNotClosedReportRecords(null);
                     return PartialView("_recallsNotClosed", results);

                 case "RecallsNoNoteSent":
                     results = dataQueries.GetRecallsNoNoteSentReportRecords(null);
                     return PartialView("_recallsNoNoteSent", results);

                 case "RecallsNotUploaded":
                     results = dataQueries.GetRecallsNotUploadedReportRecords(null);
                     return PartialView("_recallsNotUploaded", results);

                 case "AddSellerCheck":
                     results = dataQueries.GetAddSellerCheckReportRecords(null);
                     return PartialView("_addSellerCheck", results);

                 //
                 
                 
                 default:
                     return PartialView();
             }
        }
        
        public ActionResult Export(string ReportType)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<RecallViewEditResult> results;
            ViewBag.ReportType = ReportType;

            switch (ReportType)
            {
                case "RecallsUpload":
                    results = dataQueries.GetRecallsUploadReportRecords("Yes");
                    return PartialView("_export", results);

                case "RecallsNotClosed":
                    results = dataQueries.GetRecallsNotClosedReportRecords("Yes");
                    return PartialView("_export", results);

                case "RecallsNoNoteSent":
                    results = dataQueries.GetRecallsNoNoteSentReportRecords("Yes");
                    return PartialView("_export", results);

                case "RecallsNotUploaded":
                    results = dataQueries.GetRecallsNotUploadedReportRecords("Yes");
                    return PartialView("_export", results);

                case "AddSellerCheck":
                    results = dataQueries.GetAddSellerCheckReportRecords("Yes");
                    return PartialView("_export", results);

                default:
                    return PartialView();
            }
            
        }

    }
}
