using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using Cascade.ViewModels.Recourse.Media;
using Cascade.Helpers;
namespace Cascade.Business
{
    public class MediaRequest
    {
        private static readonly string thisClass = "Cascade.Business.MediaRequest";
        private DataQueries query;

        public MediaRequest()
        {
            query = new DataQueries();
        }

        public DataQueries Query
        {
            get
            {
                return query;
            }
        }

        public IEnumerable<MediaRequestTypes> GetNotFulfilled(string agency, Guid userId)
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses(agency, userId)
                       from mediaRequestType in query.GetMediaRequestTypes(agency).Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value != (int)MediaRequestStatus.Fulfilled && record.RequestStatusId.Value != (int)MediaRequestStatus.RequestComplete)
                       from mediaType in new MSI_MediaTypesRepository().GetAll().Where(record => record.IsActive.Value == true && record.ID == mediaRequestType.TypeId)
                       from mediaStatus in new MSI_MediaRequestStatusRepository().GetAll().Where(record => record.Id == mediaRequestType.RequestStatusId)
                       select new MediaRequestTypes(mediaRequestType, mainRecord) { MediaType = mediaType.Name, MediaStatus = mediaStatus.Name };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.GetNotFulfilled:" + ex.Message);
            }
            return data;
        }

        public IEnumerable<MediaRequestTypes> GetNotCimpleted()
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses()
                       from mediaRequestType in query.GetMediaRequestTypes("").Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value != (int)MediaRequestStatus.RequestComplete)
                       from mediaType in new MSI_MediaTypesRepository().GetAll().Where(record => record.IsActive.Value == true && record.ID == mediaRequestType.TypeId)
                       from mediaStatus in new MSI_MediaRequestStatusRepository().GetAll().Where(record => record.Id == mediaRequestType.RequestStatusId)
                       select new MediaRequestTypes(mediaRequestType, mainRecord) { MediaType = mediaType.Name, MediaStatus = mediaStatus.Name };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.GetNotCimpleted:" + ex.Message);
            }
            return data;
        }

        public IEnumerable<MediaRequestTypes> GetDownloadable(string agency, Guid userId)
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses(agency, userId)
                       from mediaRequestType in query.GetMediaRequestTypes(agency).Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value == (int)MediaRequestStatus.Fulfilled && record.MediaUploaded.Value == true && record.MediaDownloaded != true)
                       from mediaType in new MSI_MediaTypesRepository().GetAll().Where(record => record.IsActive.Value == true && record.ID == mediaRequestType.TypeId)
                       from mediaStatus in new MSI_MediaRequestStatusRepository().GetAll().Where(record => record.Id == mediaRequestType.RequestStatusId)
                       select new MediaRequestTypes(mediaRequestType, mainRecord) { MediaType = mediaType.Name, MediaStatus = mediaStatus.Name };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.GetNotFulfilled:" + ex.Message);
            }
            return data;
        }

        public void PerfomPreFulfillmentProcess(string accountNumber, Guid? userId)
        {
            IEnumerable<MSI_MediaTracker> mediaAvailable;
            MSI_MediaRequestTypes requestedType;
            try
            {
                mediaAvailable = Query.GetMediaTrackers(accountNumber);
                if (mediaAvailable != null)
                {
                    foreach (MSI_MediaTracker media in mediaAvailable)
                    {
                        requestedType = (from requestResponse in query.GetMediaRequestResponses().Where(record => record.ACCOUNT == accountNumber )
                                         from requestMediaType in query.GetMediaRequestTypes("").Where(record => record.RequestedId == requestResponse.Id && record.TypeId == media.MediaTypeId && record.RequestStatusId != (int)MediaRequestStatus.Fulfilled)
                                         select requestMediaType).SingleOrDefault();
                        if (requestedType != null)
                        {
                            if (requestedType.RequestStatusId != (int)MediaRequestStatus.Uploaded)
                            {
                                requestedType.RequestStatusId = (int)MediaRequestStatus.Uploaded;
                                requestedType.RespondedDocuments = media.MediaDocuments;
                                Query.UpdateMediaRequestdType(requestedType);
                            }
                        }
                        requestedType = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomPreFulfillmentProcess:" + ex.Message);
            }
        }

        public void PerfomPostFulfillmentProcess(string id, Guid userId)
        {
            MSI_MediaRequestTypes requestedType;
            try
            {
                requestedType = Query.GetMediaRequestdType(id);

                requestedType.RequestStatusId = (int)MediaRequestStatus.Fulfilled;
                requestedType.RespondedUserID = userId;
                requestedType.RespondedDate = DateTime.Now;
                requestedType.MediaUploaded = true;
                requestedType.LastUpdatedBy = userId;
                requestedType.LastUpdatedDate = DateTime.Now;
                Query.UpdateMediaRequestdType(requestedType);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomPostFulfillmentProcess:" + ex.Message);
            }
        }

        public void PerfomCompleteRequest(string id, Guid userId)
        {
            MSI_MediaRequestTypes requestedType;
            try
            {
                requestedType = Query.GetMediaRequestdType(id);

                requestedType.RequestStatusId = (int)MediaRequestStatus.RequestComplete;
                requestedType.RespondedUserID = userId;
                requestedType.RespondedDate = DateTime.Now;
                requestedType.MediaUploaded = true;
                requestedType.LastUpdatedBy = userId;
                requestedType.LastUpdatedDate = DateTime.Now;
                Query.UpdateMediaRequestdType(requestedType);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomUpdateRequest:" + ex.Message);
            }
        }

        public void PerfomUpdateRequest(string id, Guid userId, int updatedStatusId)
        {
            MSI_MediaRequestTypes requestedType;
            try
            {
                requestedType = Query.GetMediaRequestdType(id);

                requestedType.RequestStatusId = updatedStatusId;
                requestedType.LastUpdatedBy = userId;
                requestedType.LastUpdatedDate = DateTime.Now;
                Query.UpdateMediaRequestdType(requestedType);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomUpdateRequest:" + ex.Message);
            }
        }

        public void PerformRequestStatusUpdate(string id, Guid userId)
        {
            MSI_MediaRequestTypes requestedType;
            try
            {
                requestedType = Query.GetMediaRequestdType(id);

                requestedType.ReSubmittedDate = DateTime.Now;
                requestedType.ReSubmittedBy = userId;
                requestedType.LastUpdatedBy = userId;
                requestedType.LastUpdatedDate = DateTime.Now;
                Query.UpdateMediaRequestdType(requestedType);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerformRequestStatusUpdate:" + ex.Message);
            }
        }

        public void PerformRequestDownloaded(string id, Guid userId)
        {
            MSI_MediaRequestTypes requestedType;
            try
            {
                requestedType = Query.GetMediaRequestdType(id);
                requestedType.RequestStatusId = (int)MediaRequestStatus.Downloaded;
                requestedType.MediaDownloaded = true;
                requestedType.LastUpdatedBy = userId;
                requestedType.LastUpdatedDate = DateTime.Now;
                Query.UpdateMediaRequestdType(requestedType);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerformRequestDownloaded:" + ex.Message);
            }
        }

        public void SaveMediaRequestResponse(MSI_MediaRequestResponse submittedRequest)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters RequestId={1}", thisMethod, submittedRequest.Id);
            LogHelper.Info(logMessage);

            try
            {
                Query.AddMediaRequestResponse(submittedRequest);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
        }

        public void SaveMediaRequestdType(MSI_MediaRequestTypes submittedMediatype)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters RequestId={1}", thisMethod, submittedMediatype.Id);
            LogHelper.Info(logMessage);

            try
            {
                Query.AddMediaRequestdType(submittedMediatype);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
        }

        public void SaveMessageNotification(NotificationMessage message, List<string> recipientsUserIds)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters Notification Subject={1}", thisMethod, message.Subject);
            LogHelper.Info(logMessage);

            MSI_MessageNotification notification = null;
            try
            {
                foreach (string recipentUserId in recipientsUserIds)
                {
                    notification = new MSI_MessageNotification();
                    notification.Id = Guid.NewGuid();
                    notification.RecipientUserId = Guid.Parse(recipentUserId);
                    notification.Subject = message.Subject;
                    notification.Body = message.BodyText;
                    notification.SentOn = DateTime.Now;
                    notification.IsActive = true;
                    Query.AddMessageNotification(notification);
                    notification = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
        }

        public NotificationMessage GetInitialMediaRequestMessage(MSI_MediaRequestResponse submittedRequest)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);

            NotificationMessage message = null;
            StringBuilder bodyBuilder;
            try
            {
                message = new NotificationMessage();
                message.Subject = "New media request-" + DateTime.Now.ToShortDateString();

                bodyBuilder = new StringBuilder();
                bodyBuilder.Append("<table>");
                bodyBuilder.Append("<tr><td>Portfolio Number</td><td>:</td><td>" + submittedRequest.Portfolio + "</td></tr>");
                bodyBuilder.Append("<tr><td>Account Holder's Name</td><td>:</td><td>" + submittedRequest.NAME + "</td></tr>");
                bodyBuilder.Append("<tr><td>Requested Date</td><td>:</td><td>" + submittedRequest.RequestedDate + "</td></tr>");

                string mediaTypesRequested = "";
                IEnumerable<MSI_MediaTypes> availableMediaTypes = Query.GetMediaTypes();

                foreach (MSI_MediaRequestTypes mediaReqType in submittedRequest.MSI_MediaRequestTypes)
                {
                    mediaTypesRequested = mediaTypesRequested + availableMediaTypes.Where(record => record.ID == mediaReqType.TypeId).SingleOrDefault().Name + ", ";
                }
                bodyBuilder.Append("<tr><td>Requested Media Types</td><td>:</td><td>" + mediaTypesRequested.Substring(0,mediaTypesRequested.Length-2) + "</td></tr>");

                message.BodyText = bodyBuilder.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while constructing InitialMediaRequestMessage", ex);
            }
            return message;
        }

        public NotificationMessage GetUpdateRequestedToOriginatorMessage(IEnumerable<MSI_MediaRequestTypes> mediaRequestTypes)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);

            NotificationMessage message = null;
            StringBuilder bodyBuilder;
            try
            {
                message = new NotificationMessage();
                message.Subject = "Update requested-" + DateTime.Now.ToShortDateString();

                bodyBuilder = new StringBuilder();
                bodyBuilder.Append("Update requested for following media:");

                string mediaTypesRequested = "";
                IEnumerable<MSI_MediaTypes> availableMediaTypes = Query.GetMediaTypes();

                foreach (MSI_MediaRequestTypes mediaReqType in mediaRequestTypes)
                {
                    mediaTypesRequested = mediaTypesRequested + availableMediaTypes.Where(record => record.ID == mediaReqType.TypeId).SingleOrDefault().Name + ", ";
                }
                bodyBuilder.Append(mediaTypesRequested.Substring(0, mediaTypesRequested.Length - 2));

                message.BodyText = bodyBuilder.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while constructing GetUpdateRequestedToOriginatorMessage", ex);
            }
            return message;
        }

        public NotificationMessage GetRequestFulfillmentMessage(IEnumerable<MSI_MediaRequestTypes> mediaRequestTypes)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);

            NotificationMessage message = null;
            StringBuilder bodyBuilder;
            try
            {
                message = new NotificationMessage();
                message.Subject = "Request Fulfillment-" + DateTime.Now.ToShortDateString();

                bodyBuilder = new StringBuilder();
                bodyBuilder.Append("Following media available to download:");

                string mediaTypesRequested = "";
                IEnumerable<MSI_MediaTypes> availableMediaTypes = Query.GetMediaTypes();

                foreach (MSI_MediaRequestTypes mediaReqType in mediaRequestTypes)
                {
                    mediaTypesRequested = mediaTypesRequested + availableMediaTypes.Where(record => record.ID == mediaReqType.TypeId).SingleOrDefault().Name + ", ";
                }
                bodyBuilder.Append(mediaTypesRequested.Substring(0, mediaTypesRequested.Length - 2));

                message.BodyText = bodyBuilder.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error("Error while constructing GetUpdateRequestedToOriginatorMessage", ex);
            }
            return message;
        }

        public int EvaluateMediaRequestStatus(MediaRequestLevel levelOfRequest, UserRoles userRole)
        {
            if (levelOfRequest == MediaRequestLevel.Initiate && userRole == UserRoles.agency)
                return (int)MediaRequestStatus.SentToOwner;
            else if (levelOfRequest == MediaRequestLevel.Initiate && (userRole == UserRoles.user || userRole == UserRoles.mediaAdmin))
                return (int)MediaRequestStatus.SentToOriginator;
            else
                return 0;
        }
    }
}
