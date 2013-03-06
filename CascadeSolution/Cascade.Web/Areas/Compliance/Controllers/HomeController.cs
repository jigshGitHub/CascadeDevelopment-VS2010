using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
using Cascade.Web.Areas.Recourse.Models;
using Cascade.Web.ApplicationIntegration;
namespace Cascade.Web.Areas.Compliance.Controllers
{
    public class HomeController : BaseController
    {
        private IFileProcessor fileProcessor = new FileProcessor();
        //
        // GET: /Compliance/Home/

        public ActionResult Index()
        {
            
            return View();
        }
        
        public ActionResult ReportNCRA()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "NCRA";
            return View();
        }

        public ActionResult ReportRC()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "RC";
            return View();
        }
        public ActionResult ReportORP()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "ORP";
            return View();
        }
        public ActionResult ReportSOA()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "SOA";
            return View();
        }
        public ActionResult ReportNCP()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "NCP";
            return View();
        }
        public ActionResult ReportAAI()
        {
            //Save the Report Type in ViewBag. It will be stored in the Hidden field on the Page
            ViewBag.ReportType = "AAI";
            return View();
        }

        public ActionResult GetReportData(string ReportType)
        {
            var dataQueries = new DataQueries();
            IEnumerable<ComplianceViewResult> results;
            //For Report based on Report Type selection
            results = dataQueries.GetComplianceReportRecords(UserAgency, ReportType);
            ViewBag.ReportType = ReportType;
            return PartialView("_compliance"+ReportType, results);
          
        }


        public ActionResult Export(string ReportType)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<ComplianceViewResult> results;
            ViewBag.ReportType = ReportType;

            results = dataQueries.GetComplianceReportRecordsExport(UserAgency, ReportType);

            return PartialView("Export", results);            

        }


        //
        // GET: /Compliance/Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Compliance/Home/Create

        public ActionResult Create()
        {
            ViewBag.UserID = UserId.ToString();
            ViewBag.AgencyID = UserAgency;
            ViewBag.UserRole = UserRoles.First().ToLower();
            return View();
        }

        public ActionResult ViewEdit(string id, string agency)
        {
            ViewBag.UserID = UserId.ToString();
            ViewBag.Account = (string.IsNullOrEmpty(id)) ? "" : id; ;
            ViewBag.AgencyID = (string.IsNullOrEmpty("agency")) ? UserAgency : agency;
            ViewBag.UserRole = UserRoles.First().ToLower();
            return View();
        }

        //
        // POST: /Compliance/Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Compliance/Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Compliance/Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Compliance/Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Compliance/Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadDocument()
        {            
            string account = Request.Form["hdnAccount"];
            string agency = Request.Form["hdnAgency"];
            string complaintDocument, debtOwnerProcessDocument;
            complaintDocument = Request.Files["complaintDocument"].FileName;
            debtOwnerProcessDocument = Request.Files["debtOwnerProcessDocument"].FileName;
            Cascade.Data.Repositories.MSI_ComplaintMainRepository repository = new MSI_ComplaintMainRepository();
            Cascade.Data.Models.MSI_ComplaintMain complaint = (from existingComplaint in repository.GetAll().Where(record => record.AgencyId == agency && record.Account == account)
                                                       select existingComplaint).First();
            if (!string.IsNullOrEmpty(complaintDocument))
            {
                complaint.ComplaintDocument = fileProcessor.SaveUploadedFile(Request.Files["complaintDocument"]) + "_" + complaintDocument;
                repository.Update(complaint);
            }
            if (!string.IsNullOrEmpty(debtOwnerProcessDocument))
            {
                complaint.DebtOwnerProcessDocument = fileProcessor.SaveUploadedFile(Request.Files["debtOwnerProcessDocument"]) + "_" + debtOwnerProcessDocument;
                repository.Update(complaint);
            }
            return RedirectToAction("ViewEdit", "Home", new { id = account, agency = agency });

        }

        public FilePathResult DownloadDoc(string fileName)
        {
            return File(fileProcessor.GetFilePath(fileName), "text/plain", fileName.Split(new char[]{'_'})[1]);
        }
    }
}
