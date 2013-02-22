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
            IEnumerable<DPSViewEditResult> resultsDPS;
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

                case "RecallsReceivable":
                    results = dataQueries.GetRecallsReceivableReportRecords(null, "RecallsReceivable");
                    return PartialView("_recallsReceivable", results);
                case "RecallsInvoiceLookup":
                    results = dataQueries.GetRecallsReceivableReportRecords(null, "RecallsInvoiceLookup");
                    return PartialView("_recallsInvoiceLookup", results);
                case "RecallsSellerCheckLookup":
                    results = dataQueries.GetRecallsReceivableReportRecords(null, "RecallsSellerCheckLookup");
                    return PartialView("_recallsSellerCheckLookup", results);
                case "RecallsPayable":
                    results = dataQueries.GetRecallsReceivableReportRecords(null, "RecallsPayable");
                    return PartialView("_recallsPayable", results);
                case "RecallsPaidByOurCheck":
                    results = dataQueries.GetRecallsReceivableReportRecords(null, "RecallsPaidByOurCheck");
                    return PartialView("_recallsPaidByOurCheck", results);
                case "DPSCheckDetail":
                    resultsDPS = dataQueries.GetDPSReportRecords(null, "DPSCheckDetail");
                    return PartialView("_dpsCheckDetails", resultsDPS);
                case "DPSPayable":
                    resultsDPS = dataQueries.GetDPSReportRecords(null, "DPSPayable");
                    return PartialView("_dpsPayable", resultsDPS);
                case "DPSPaidByOurCheck":
                    resultsDPS = dataQueries.GetDPSReportRecords(null, "DPSPaidByOurCheck");
                    return PartialView("_dpsPaidByOurCheck", resultsDPS);
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
            IEnumerable<DPSViewEditResult> resultsDPS;
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

                case "RecallsReceivable":
                    results = dataQueries.GetRecallsReceivableReportRecords("Yes", "RecallsReceivable");
                    return PartialView("_export", results);
                case "RecallsInvoiceLookup":
                    results = dataQueries.GetRecallsReceivableReportRecords("Yes", "RecallsInvoiceLookup");
                    return PartialView("_export", results);
                case "RecallsSellerCheckLookup":
                    results = dataQueries.GetRecallsReceivableReportRecords("Yes", "RecallsSellerCheckLookup");
                    return PartialView("_export", results);
                case "RecallsPayable":
                    results = dataQueries.GetRecallsReceivableReportRecords("Yes", "RecallsPayable");
                    return PartialView("_export", results);
                case "RecallsPaidByOurCheck":
                    results = dataQueries.GetRecallsReceivableReportRecords("Yes", "RecallsPaidByOurCheck");
                    return PartialView("_export", results);
                case "DPSCheckDetail":
                    resultsDPS = dataQueries.GetDPSReportRecords("Yes", "DPSCheckDetail");
                    return PartialView("_exportDPS", resultsDPS);
                case "DPSPayable":
                    resultsDPS = dataQueries.GetDPSReportRecords("Yes", "DPSPayable");
                    return PartialView("_exportDPS", resultsDPS);
                case "DPSPaidByOurCheck":
                    resultsDPS = dataQueries.GetDPSReportRecords("Yes", "DPSPaidByOurCheck");
                    return PartialView("_exportDPS", resultsDPS);

                default:
                    return PartialView();
            }

        }

    }
}
