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
using System.Net.Http;
using System.Net.Http.Headers;
using Cascade.Data.Models;
using CascadeBusiness = Cascade.Business;
using Cascade.ViewModels.Recourse.Media;
namespace Cascade.Web.Areas.Recourse.Controllers
{
    public class MediaController : BaseController
    {
        private IFileProcessor fileProcessor = new FileProcessor();
        
        //
        // GET: /Recourse/DPS/

        [HttpGet]
        public ActionResult Index()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpGet]
        public ActionResult ViewMedia()
        {
            //This will display all DPS Records
            return View();
        }

        [HttpPost]
        public JsonResult Add(MSI_MediaForm _mediaform)
        {
            MSIMediaFormRepository repository;
            try
            {
                repository = new MSIMediaFormRepository();
                if (_mediaform.ID > 0)
                {
                    repository.Update(_mediaform);
                }
                else
                {
                    repository.Add(_mediaform);
                }
            }
            catch (Exception ex)
            {

            }
            //Keep the Affected RecordID in session
            Session["RecordID"] = _mediaform.ID;
            //return _dpsform;
            return Json(_mediaform, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(MSI_MediaForm _mediaform)
        {
            vwAccount mediaFormData = null;
            vwAccountRepository viewRepository = null;
            try
            {
                viewRepository = new vwAccountRepository();
                if (_mediaform.PIMSAcct != null)
                {
                    mediaFormData = viewRepository.Get(x => x.ACCOUNT == _mediaform.PIMSAcct);

                }
                else
                {
                    mediaFormData = viewRepository.Get(x => x.OriginalAccount == _mediaform.OrigAcct);
                }

                //Filled data obtained from the Database
                _mediaform.AcctName = mediaFormData.NAME;
                _mediaform.CompanyRequesting = mediaFormData.RESPONSIBILITY;
                _mediaform.Portfolio = mediaFormData.Portfolio;
                _mediaform.PIMSAcct = mediaFormData.ACCOUNT;
                _mediaform.OrigAcct = mediaFormData.OriginalAccount;
                _mediaform.Seller = mediaFormData.Seller;
                _mediaform.OriginalLender = mediaFormData.Originator;
                _mediaform.SSN = mediaFormData.SSN;
                _mediaform.OpenDate = mediaFormData.OpenDate;
                _mediaform.CODate = mediaFormData.ChargeOffDate;
                _mediaform.OrderNumber = RandomGen2.Next().ToString();
                _mediaform.GUID = System.Guid.NewGuid().ToString();

            }
            catch (Exception ex)
            {
            }
            //return the Json Object
            return Json(_mediaform, JsonRequestBehavior.AllowGet);
        }
       
      
        public ActionResult GetAllMediaRecords(DateTime? StartDate, DateTime? EndDate, int? Portfolio, string Responsibility, string Account, string GUID)
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
            IEnumerable<MediaViewEditResult> results = dataQueries.GetMediaViewEditRecords(StartDate, EndDate, _portfolioowner, Responsibility, Account, GUID);
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
            return PartialView("_mediaRecords", results.ToList());

        }


        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            //return the view
            return View();
        }

        public JsonResult GetMediaData(int id)
        {
            MSIMediaFormRepository portMediaRepo = new MSIMediaFormRepository();
            var _portMediaData = from _portMedia in portMediaRepo.GetAll().Distinct()
                               where _portMedia.ID == id
                             select _portMedia;
            return Json(_portMediaData.SingleOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Export(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            var dataQueries = new DataQueries();
            IEnumerable<MediaViewEditResult> results = dataQueries.GetMediaViewEditRecordsExport(StartDate, EndDate, PortfolioOwner, Responsibility, Account, GUID);
            //return View();
            return PartialView("Export", results.ToList());
        }
                
        public ActionResult UploadDocument()
        {
            
            if (Session["RecordID"] != null)
            {
                int _recordID = 0;
                string mediaDocument;
                //Gather the Info
                _recordID = Convert.ToInt32(Session["RecordID"].ToString());
                mediaDocument = Request.Files["mediaDocument"].FileName;
                //Get the Info from the Repository
                Cascade.Data.Repositories.MSIMediaFormRepository repository = new MSIMediaFormRepository();
                Cascade.Data.Models.MSI_MediaForm _mediaForm = (from existingForm in repository.GetAll().Where(record => record.ID == _recordID)
                                                                select existingForm).First();
                if (!string.IsNullOrEmpty(mediaDocument))
                {
                    string additionalIdentifier = Guid.NewGuid().ToString();
                    //User the additional Identifier to uniquely Identify filename
                    fileProcessor.SaveUploadedFileWithIdentifier(Request.Files["mediaDocument"], additionalIdentifier);
                    _mediaForm.FileName = additionalIdentifier + "_" + mediaDocument;
                    repository.Update(_mediaForm);
                }
                //Redirect to View Edit Form
                return RedirectToAction("Details", "Media", new { id = _mediaForm.ID });
            }
            else
            {
                //Something is wrong so go to main page
                return RedirectToAction("Index", "Media");
            }
            
        }

        public FilePathResult DownloadDoc(string fileName)
        {
            return File(fileProcessor.GetFilePath(fileName), "text/plain", fileName);
        }

        public ActionResult MediaRequest()
        {
            //ViewBag.UserID = UserId.ToString();
            //ViewBag.AgencyID = UserAgency;
            //ViewBag.UserRole = UserRoles.First().ToLower();
            return View();
        }

        public ActionResult GetMediaRequests()
        {
            IEnumerable<MSI_MediaRequestResponse> data = null;
            DataQueries query = new DataQueries();
            try
            {
                data = query.GetMediaRequestResponses(UserAgency);
            }
            catch (Exception ex)
            {
            }
            return PartialView("_mediaRequests", data);

        }

        [ActionName("Create")]
        public ActionResult MediaRequestCreate(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                Guid result;
                if (Guid.TryParse(id, out result))
                {
                    ViewBag.Id = result.ToString();                      
                }
                else
                    ViewBag.Account = id;
                    
            }
            return View();
        }

        public ActionResult MediaUpload()
        {
            //ViewBag.UserID = UserId.ToString();
            //ViewBag.AgencyID = UserAgency;
            //ViewBag.UserRole = UserRoles.First().ToLower();
            return View();
        }

        public bool PerformFileUploadOperation(MSI_MediaTracker _mediaTracker, int MediaTypeId,  IEnumerable<HttpPostedFileBase> FilesCollection)
        {
            Cascade.Data.Repositories.MSIMediaTrackerRepository repository = new MSIMediaTrackerRepository();
            string _allMediaDocuments = "";
            string _additionalIdentifier = Guid.NewGuid().ToString() + "_" + _mediaTracker.Account + "_" + MediaTypeId.ToString();
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
                _mediaTracker.MediaTypeId = MediaTypeId;
                _mediaTracker.MediaDocuments = _allMediaDocuments;
                repository.Add(_mediaTracker);
            }
            //response
            return true;

        }

        [HttpPost]
        public ActionResult UploadAllDocuments(IEnumerable<HttpPostedFileBase> AfdvtIssuerfiles, IEnumerable<HttpPostedFileBase> AfdvtSellerfiles,
            IEnumerable<HttpPostedFileBase> Applicationfiles, IEnumerable<HttpPostedFileBase> Contactfiles,
            IEnumerable<HttpPostedFileBase> Statementfiles, IEnumerable<HttpPostedFileBase> ChargeOffStmtfiles, IEnumerable<HttpPostedFileBase> RightOfCurefiles,
            IEnumerable<HttpPostedFileBase> BalanceLtrfiles, IEnumerable<HttpPostedFileBase> NoticeOfIntentfiles,
            IEnumerable<HttpPostedFileBase> CardHolderAgrmtfiles, IEnumerable<HttpPostedFileBase> TermAndCndfiles)
        {
            //Collect information from the hiddern fields
            string PIMSAcctNumber = Request.Form["hdnpimsAccountNumber"];
            string OrigAcctNumber = Request.Form["hdnoriginalAccountNumber"];
            string hdnstatementStDt = Request.Form["hdnstatementStDt"];
            string hdnstatementEndDt = Request.Form["hdnstatementEndDt"];
            //Get the Object
            MSI_MediaTracker _mediaTracker = new MSI_MediaTracker();
            //Get the necessary Information 
            _mediaTracker.Account = PIMSAcctNumber;
            _mediaTracker.OriginalAccount = OrigAcctNumber;
            _mediaTracker.CreatedDate = DateTime.Now;
            _mediaTracker.CreatedBy = UserId.ToString();
            _mediaTracker.IsActive = true;
            //For Files
            #region [[ Affedavit(Issuer) ]]
            if (AfdvtIssuerfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 1, AfdvtIssuerfiles);
            }
            #endregion

            #region [[ Affedavit(Seller) ]]
            if (AfdvtSellerfiles.Count() > 1)
            {
                PerformFileUploadOperation(_mediaTracker, 2,  AfdvtSellerfiles);
            }
            #endregion

            #region [[ Application ]]
            if (Applicationfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 3,  Applicationfiles);
            }
            #endregion

            #region [[ Contact ]]
            if (Contactfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 4, Contactfiles);
            }
            #endregion

            #region [[ Charge of Statement ]]
            if (ChargeOffStmtfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 6,  ChargeOffStmtfiles);
            }
            #endregion

            #region [[ Right of Cure ]]
            if (RightOfCurefiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 7,  RightOfCurefiles);
            }
            #endregion

            #region [[ Balance Letter ]]
            if (BalanceLtrfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 8,  BalanceLtrfiles);
            }
            #endregion

            #region [[ Notice of Intent ]]
            if (NoticeOfIntentfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 9,  NoticeOfIntentfiles);
            }
            #endregion

            #region [[ Card Holder Agreement ]]
            if (CardHolderAgrmtfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 10,  CardHolderAgrmtfiles);
            }
            #endregion

            #region [[ Terms and Conditions ]]
            if (TermAndCndfiles.Count() >= 1)
            {
                PerformFileUploadOperation(_mediaTracker, 11,  TermAndCndfiles);
            }
            #endregion

            //IMPORTANT: Do it at the end so TypeConstraints are added for only Statement type media
            #region [[ Statement ]]
            if (Statementfiles.Count() >= 1)
            {
                if (hdnstatementStDt != "" && hdnstatementEndDt != "")
                {
                    _mediaTracker.TypeConstraints = hdnstatementStDt + ":" + hdnstatementEndDt;
                }
                PerformFileUploadOperation(_mediaTracker, 5, Statementfiles);
            }
            #endregion

            //Exit 
            return RedirectToAction("Index", "Media");
        }
        
        public ActionResult PIMSDataSearch(string nameSearch)
        {
            ViewBag.NameSearch = nameSearch;
            return View();
        }

        public ActionResult GetPIMSDataForMedia(string nameSearch)
        {
            IEnumerable<vwAccount> data = new DataQueries().GetAccounts(nameSearch);
            return PartialView("_pimsMediaRecords", data);
        }

        public ActionResult MediaRequestNotFulfilled()
        {
            ViewBag.RoleBasedHeaderTitle = (UserRoles.Contains("agency")) ? "Media Request" : "Media Fulfillment";
            return View();
        }

        public ActionResult GetMediaRequestNotFulfilled()
        {
            CascadeBusiness.MediaRequest business = new CascadeBusiness.MediaRequest();
            IEnumerable<MediaRequestTypes> data = business.GetNotFulfilled(UserAgency);
            return PartialView("_mediaRequestNotFulfilled", data);
        }

        public ActionResult MediaFulfillment(string id)
        {
            ViewBag.Account = id;
            return View();
        }

    }
}
