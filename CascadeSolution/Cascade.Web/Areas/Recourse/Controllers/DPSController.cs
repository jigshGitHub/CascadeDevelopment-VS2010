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
    public class DPSController : BaseController
    {
        private IFileProcessor fileProcessor = new FileProcessor();
        //
        // GET: /Recourse/DPS/

        [HttpGet]
        public ActionResult Index(int? prevRecord)
        {
            //This will return blank view for ADD feature
            ViewBag.prevRecord = "";
            if (prevRecord != null) ViewBag.prevRecord = prevRecord.ToString();
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
                    //Set IsActive as True for every new Record
                    _dpsform.IsActive = true;
                    repository.Add(_dpsform);
                }
            }
            catch (Exception ex)
            {

            }
            //return _dpsform;
            return Json(_dpsform, JsonRequestBehavior.AllowGet);
        }

        public FilePathResult DownloadDoc(string fileName)
        {

            return File(fileProcessor.GetFilePath(fileName), "text/plain", fileName);
        }

        [HttpPost]
        public ActionResult UploadAllDocuments(IEnumerable<HttpPostedFileBase> checkDocuments)
        {
            //Let us get Id of newly created reocrd
            string _addedDPSRecordID = Request.Form["hdnDPSRecordID"];
            //If ID is not null then go ahaead
            if (_addedDPSRecordID != null)
            {
                int _recordID = 0;
                //Gather the Info
                _recordID = Convert.ToInt32(_addedDPSRecordID);
                //Get the Info from the Repository
                Cascade.Data.Repositories.MSIDPSFormDataRepository repository = new MSIDPSFormDataRepository();
                Cascade.Data.Models.MSI_DPSForm _dpsForm = (from existingForm in repository.GetAll().Where(record => record.ID == _recordID)
                                                            select existingForm).First();
                _dpsForm.UploadedOn = DateTime.Now;
                _dpsForm.IsActive = true;
                _dpsForm.Uploaded = true;
                _dpsForm.UploadedBy = UserId.ToString(); 
                #region [[ Check Images ]]
                if (checkDocuments != null)
                {
                    if (checkDocuments.Count() >= 1)
                    {
                        PerformFileUploadOperation(_dpsForm, checkDocuments);
                    }
                }
                #endregion
                //Redirect to View Edit Form
                return RedirectToAction("Details", "DPS", new { id = _dpsForm.ID });
            }
            else
            {
                //Something is wrong so go to main page
                return RedirectToAction("Index", "DPS");
            }

        }

        public bool PerformFileUploadOperation(MSI_DPSForm  _dpsForm, IEnumerable<HttpPostedFileBase> FilesCollection)
        {
            Cascade.Data.Repositories.MSIDPSFormDataRepository repository = new MSIDPSFormDataRepository();
            string _allMediaDocuments = "";
            //set document name if we already have one and already uploaded in the past
            _allMediaDocuments = (_dpsForm.CheckDocuments == null) ? "" : _dpsForm.CheckDocuments;
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
                _dpsForm.CheckDocuments = _allMediaDocuments;
                repository.Update(_dpsForm);
            }
            //response
            return true;

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
