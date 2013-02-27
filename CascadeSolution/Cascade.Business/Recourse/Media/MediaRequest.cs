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

        public IEnumerable<MediaRequestTypes> GetNotFulfilled(string agency)
        {
            IEnumerable<MediaRequestTypes> data = null;
            try
            {
                data = from mainRecord in query.GetMediaRequestResponses(agency)
                       from mediaRequestType in query.GetMediaRequestTypes(agency).Where(record => record.RequestedId == mainRecord.Id && record.RequestStatusId.Value != (int)MediaRequestStatus.RequestFulfillment)
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

        public MSI_MediaRequestResponse PerfomPreFulfillmentProcess(string accountNumber)
        {
            IEnumerable<MSI_MediaTracker> mediaAvailable;
            MSI_MediaRequestTypes requestedType;
            MSI_MediaRequestResponse data = null;
            try
            {
                mediaAvailable = Query.GetMediaTrackers(accountNumber);
                if (mediaAvailable != null)
                {
                    foreach (MSI_MediaTracker media in mediaAvailable)
                    {
                        requestedType = (from requestResponse in query.GetMediaRequestResponses("").Where(record => record.ACCOUNT == accountNumber)
                                        from requestMediaType in query.GetMediaRequestTypes("").Where(record => record.RequestedId == requestResponse.Id && record.TypeId == media.MediaTypeId && record.RequestStatusId != (int)MediaRequestStatus.RequestFulfillment)
                                         select requestMediaType).SingleOrDefault();
                        if (requestedType != null)
                        {
                            requestedType.RequestStatusId = (int)MediaRequestStatus.RequestInProgress;
                            requestedType.RespondedDocuments = media.MediaDocuments;
                            Query.UpdateMediaRequestdType(requestedType);
                        }
                    }
                    data = Query.GetMediaRequestResponse(accountNumber, "");
                    data.MSI_MediaRequestTypes = data.MSI_MediaRequestTypes.Where(record => record.RequestStatusId != (int)MediaRequestStatus.RequestFulfillment).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomPreFulfillmentProcess:" + ex.Message);
            }
            return data;
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
                throw new Exception("Exception in Cascade.Business.MediaRequest.PerfomPreFulfillmentProcess:" + ex.Message);
            }
        }

    }
}
