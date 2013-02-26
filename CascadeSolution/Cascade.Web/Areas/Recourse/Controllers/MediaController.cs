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
            return View();
        }

        public ActionResult GetMediaRequestNotFulfilled()
        {
            DataQueries query = new DataQueries();
            IEnumerable<MediaRequestTypes> data = from mainRecord in query.GetMediaRequestResponses(this.UserAgency)
                                                  from mediaRequestType in query.GetMediaRequestTypes(UserAgency).Where(record => record.RequestedId == mainRecord.Id)
                                                  from mediaType in new MSI_MediaTypesRepository().GetAll().Where(record => record.IsActive.Value == true && record.ID == mediaRequestType.TypeId)
                                                  from mediaStatus in new MSI_MediaRequestStatusRepository().GetAll().Where(record => record.Id == mediaRequestType.RequestStatusId)
                                                  select new MediaRequestTypes(mediaRequestType, mainRecord) { MediaType = mediaType.Name, MediaStatus = mediaStatus.Name };
            return PartialView("_mediaRequestNotFulfilled", data);
        }   

    }
}
