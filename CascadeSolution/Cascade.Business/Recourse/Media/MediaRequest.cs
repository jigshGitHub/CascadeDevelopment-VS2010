using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using Cascade.ViewModels.Recourse.Media;
namespace Cascade.Business
{
    public class MediaRequest
    {
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

        public IEnumerable<MediaRequestTypes> GetNotFulfilled(string agency,Guid userId)
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses(agency,userId)
                       from mediaRequestType in query.GetMediaRequestTypes(agency).Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value != (int)MediaRequestStatus.RequestFulfillment && record.RequestStatusId.Value != (int)MediaRequestStatus.RequestComplete)
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

        public IEnumerable<MediaRequestTypes> GetDownloadable(string agency,Guid userId)
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses(agency,userId)
                       from mediaRequestType in query.GetMediaRequestTypes(agency).Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value == (int)MediaRequestStatus.RequestFulfillment && record.MediaUploaded.Value == true && record.MediaDownloaded != true )
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
                        requestedType = (from requestResponse in query.GetMediaRequestResponses().Where(record => record.ACCOUNT == accountNumber && record.RequestedByUserId != (userId.HasValue ? userId : Guid.Parse(media.CreatedBy)))
                                        from requestMediaType in query.GetMediaRequestTypes("").Where(record => record.RequestedId == requestResponse.Id && record.TypeId == media.MediaTypeId && record.RequestStatusId == (int)MediaRequestStatus.RequestReceived)
                                         select requestMediaType).SingleOrDefault();
                        if (requestedType != null)
                        {
                            requestedType.RequestStatusId = (int)MediaRequestStatus.RequestInProgress;
                            requestedType.RespondedDocuments = media.MediaDocuments;
                            Query.UpdateMediaRequestdType(requestedType);
                        }
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

                requestedType.RequestStatusId = (int)MediaRequestStatus.RequestFulfillment;
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

    }
}
