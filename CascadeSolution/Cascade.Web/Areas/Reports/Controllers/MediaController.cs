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
    public class ReportMediaController : BaseController
    {
        //
        // GET: /Reports/ReportMedia/
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
            IEnumerable<MediaViewEditResult> results;
            //For Report based on Report Type selection
            switch (ReportType)
            {
                case "MediaNotSubmitted":
                    results = dataQueries.GetMediaNotSubmittedReportRecords(null);
                    return PartialView("_mediaNotSubmitted", results);

                case "MediaNotConfirmed":
                    results = dataQueries.GetMediaNotSubmittedReportRecords(null);
                    return PartialView("_mediaNotConfirmed", results);

                case "MediaNotReceived":
                    results = dataQueries.GetMediaNotSubmittedReportRecords(null);
                    return PartialView("_mediaNotReceived", results);

                case "MediaNotForwarded":
                    results = dataQueries.GetMediaNotSubmittedReportRecords(null);
                    return PartialView("_mediaNotForwarded", results);

                default:
                    return PartialView();
            }
        }

        public ActionResult Export(string ReportType)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<MediaViewEditResult> results;
            ViewBag.ReportType = ReportType;

            switch (ReportType)
            {

                case "MediaNotSubmitted":
                    results = dataQueries.GetMediaNotSubmittedReportRecords("Yes");
                    return PartialView("_export", results);

                case "MediaNotConfirmed":
                    results = dataQueries.GetMediaNotSubmittedReportRecords("Yes");
                    return PartialView("_export", results);

                case "MediaNotReceived":
                    results = dataQueries.GetMediaNotSubmittedReportRecords("Yes");
                    return PartialView("_export", results);

                case "MediaNotForwarded":
                    results = dataQueries.GetMediaNotSubmittedReportRecords("Yes");
                    return PartialView("_export", results);

                default:
                    return PartialView();
            }

        }

    }
}
