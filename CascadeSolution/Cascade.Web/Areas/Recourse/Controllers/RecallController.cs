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
using Cascade.Web.ApplicationIntegration;

namespace Cascade.Web.Areas.Recourse.Controllers
{
    public class RecallController : BaseController
    {
        //
        // GET: /Recourse/DPS/

        private IFileProcessor fileProcessor = new FileProcessor();

        #region [[Recall Methods]]
        public ActionResult GetAllRecallRecords(DateTime? StartDate, DateTime? EndDate, int? Portfolio, string Responsibility, string Account, string GUID)
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
            IEnumerable<RecallViewEditResult> results = dataQueries.GetRecallViewEditRecords(StartDate, EndDate, _portfolioowner, Responsibility, Account, GUID);
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
            return PartialView("_recallRecords", results.ToList());

        }

        public ActionResult Details(string id)
        {
            ViewBag.hdnId = id;
            //return the view
            return View();
        }

        public JsonResult GetRecallData(int id)
        {
            MSIRecallFormRepository portRecallRepo = new MSIRecallFormRepository();
            var _portRecallData = from _portRecall in portRecallRepo.GetAll().Distinct()
                                  where _portRecall.ID == id
                                  select _portRecall;
            return Json(_portRecallData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Export(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<RecallViewEditResult> results = dataQueries.GetRecallViewEditRecordsExport(StartDate, EndDate, PortfolioOwner, Responsibility, Account, GUID);
            //return View();
            return PartialView("Export", results.ToList());
        }

        [HttpPost]
        public JsonResult Add(MSI_RecallForm _recallform)
        {
            MSIRecallFormRepository repository;
            _recallform.IsActive = true;
            try
            {
                repository = new MSIRecallFormRepository();
                if (_recallform.ID > 0)
                {
                    repository.Update(_recallform);
                }
                else
                {
                    repository.Add(_recallform);
                }
            }
            catch (Exception ex)
            {

            }
            //return _dpsform;
            return Json(_recallform, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpGet]
        public ActionResult ViewRecall()
        {
            //show the view
            return View();
        }

        [HttpPost]
        public ActionResult UploadAllDocuments(IEnumerable<HttpPostedFileBase> checkDocuments)
        {
            //Let us get Id of newly created reocrd
            string _addedRecallRecordID = Request.Form["hdnRecallRecordID"];
            //If ID is not null then go ahaead
            if (_addedRecallRecordID != null)
            {
                int _recordID = 0;
                //Gather the Info
                _recordID = Convert.ToInt32(_addedRecallRecordID);
                //Get the Info from the Repository
                Cascade.Data.Repositories.MSIRecallFormRepository repository = new MSIRecallFormRepository();
                Cascade.Data.Models.MSI_RecallForm _recallForm = (from existingForm in repository.GetAll().Where(record => record.ID == _recordID)
                                                                  select existingForm).First();
                _recallForm.UploadedOn = DateTime.Now;
                _recallForm.IsActive = true;
                _recallForm.UploadedBy = UserId.ToString();
                #region [[ Check Images ]]
                if (checkDocuments != null)
                {
                    if (checkDocuments.Count() >= 1)
                    {
                        PerformFileUploadOperation(_recallForm, checkDocuments);
                    }
                }
                #endregion
                //Redirect to View Edit Form
                return RedirectToAction("Details", "Recall", new { id = _recallForm.ID });
            }
            else
            {
                //Something is wrong so go to main page
                return RedirectToAction("Index", "Recall");
            }

        }

        public bool PerformFileUploadOperation(MSI_RecallForm _recallForm, IEnumerable<HttpPostedFileBase> FilesCollection)
        {
            Cascade.Data.Repositories.MSIRecallFormRepository repository = new MSIRecallFormRepository();
            string _allMediaDocuments = "";
            //set document name if we already have one and already uploaded in the past
            _allMediaDocuments = (_recallForm.CheckDocuments == null) ? "" : _recallForm.CheckDocuments;
            //get GUID
            string _additionalIdentifier = Guid.NewGuid().ToString();
            //Get all fileNames together so store in the Database
            foreach (var file in FilesCollection)
            {
                if (file != null)
                {
                    if (_allMediaDocuments.Length > 0)
                    {
                        _allMediaDocuments = _allMediaDocuments + "|" + _additionalIdentifier + "_" + file.FileName;
                    }
                    else
                    {
                        _allMediaDocuments = _additionalIdentifier + "_" + file.FileName;
                    }
                }

            }
            //Now Upload and set the properties
            foreach (var file in FilesCollection)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        //Upload the file
                        fileProcessor.SaveUploadedFileWithIdentifier(file, _additionalIdentifier);
                    }
                }

            }
            if (_allMediaDocuments != "")
            {
                //Perform Save Operation
                _recallForm.CheckDocuments = _allMediaDocuments;
                repository.Update(_recallForm);
            }
            //response
            return true;

        }

        #endregion

        #region [[Putback Methods]]

        [HttpPost]
        public JsonResult AddPutback(MSI_PutBackForm _putbackform)
        {
            MSIPutBackFormRepository repository;
            _putbackform.IsActive = true;
            try
            {
                repository = new MSIPutBackFormRepository();
                if (_putbackform.ID > 0)
                {
                    repository.Update(_putbackform);
                }
                else
                {
                    repository.Add(_putbackform);
                }
            }
            catch (Exception ex)
            {

            }
            //return _dpsform;
            return Json(_putbackform, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllPutbackRecords(DateTime? StartDate, DateTime? EndDate, int? Portfolio, string Responsibility, string Account, string GUID)
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
            IEnumerable<PutBackViewEditResult> results = dataQueries.GetPutBackViewEditRecords(StartDate, EndDate, _portfolioowner, Responsibility, Account, GUID);
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
            return PartialView("_putbackRecords", results.ToList());

        }

        public ActionResult DetailsPutback(string id)
        {
            ViewBag.hdnId = id;
            //return the view
            return View();
        }

        public JsonResult GetPutbackData(int id)
        {
            MSIPutBackFormRepository portRecallRepo = new MSIPutBackFormRepository();
            var _portRecallData = from _portRecall in portRecallRepo.GetAll().Distinct()
                                  where _portRecall.ID == id
                                  select _portRecall;
            return Json(_portRecallData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult ExportPutBack(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<PutBackViewEditResult> results = dataQueries.GetPutBackViewEditRecordsExport(StartDate, EndDate, PortfolioOwner, Responsibility, Account, GUID);
            //return View();
            return PartialView("ExportPutBack", results.ToList());
        }

        [HttpGet]
        public ActionResult PutBackIndex()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpGet]
        public ActionResult ViewPutback()
        {
            //show the view
            return View();
        }

        [HttpPost]
        public ActionResult UploadAllDocumentsPutBack(IEnumerable<HttpPostedFileBase> checkDocuments)
        {
            //Let us get Id of newly created reocrd
            string _addedPutBackRecordID = Request.Form["hdnPutbackRecordID"];
            //If ID is not null then go ahaead
            if (_addedPutBackRecordID != null)
            {
                int _recordID = 0;
                //Gather the Info
                _recordID = Convert.ToInt32(_addedPutBackRecordID);
                //Get the Info from the Repository
                Cascade.Data.Repositories.MSIPutBackFormRepository repository = new MSIPutBackFormRepository();
                Cascade.Data.Models.MSI_PutBackForm _putbackForm = (from existingForm in repository.GetAll().Where(record => record.ID == _recordID)
                                                                    select existingForm).First();
                _putbackForm.UploadedOn = DateTime.Now;
                _putbackForm.IsActive = true;
                _putbackForm.UploadedBy = UserId.ToString();
                #region [[ Check Images ]]
                if (checkDocuments != null)
                {
                    if (checkDocuments.Count() >= 1)
                    {
                        PerformFileUploadOperationPutBack(_putbackForm, checkDocuments);
                    }
                }
                #endregion
                //Redirect to View Edit Form
                return RedirectToAction("DetailsPutback", "Recall", new { id = _putbackForm.ID });
            }
            else
            {
                //Something is wrong so go to main page
                return RedirectToAction("PutBackIndex", "Recall");
            }

        }

        public bool PerformFileUploadOperationPutBack(MSI_PutBackForm _putbackForm, IEnumerable<HttpPostedFileBase> FilesCollection)
        {
            Cascade.Data.Repositories.MSIPutBackFormRepository repository = new MSIPutBackFormRepository();
            string _allMediaDocuments = "";
            //set document name if we already have one and already uploaded in the past
            _allMediaDocuments = (_putbackForm.CheckDocuments == null) ? "" : _putbackForm.CheckDocuments;
            //get GUID
            string _additionalIdentifier = Guid.NewGuid().ToString();
            //Get all fileNames together so store in the Database
            foreach (var file in FilesCollection)
            {
                if (file != null)
                {
                    if (_allMediaDocuments.Length > 0)
                    {
                        _allMediaDocuments = _allMediaDocuments + "|" + _additionalIdentifier + "_" + file.FileName;
                    }
                    else
                    {
                        _allMediaDocuments = _additionalIdentifier + "_" + file.FileName;
                    }
                }

            }
            //Now Upload and set the properties
            foreach (var file in FilesCollection)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        //Upload the file
                        fileProcessor.SaveUploadedFileWithIdentifier(file, _additionalIdentifier);
                    }
                }

            }
            if (_allMediaDocuments != "")
            {
                //Perform Save Operation
                _putbackForm.CheckDocuments = _allMediaDocuments;
                repository.Update(_putbackForm);
            }
            //response
            return true;

        }
        #endregion

        public FilePathResult DownloadDoc(string fileName)
        {

            return File(fileProcessor.GetFilePath(fileName), "text/plain", fileName);
        }










    }
}
