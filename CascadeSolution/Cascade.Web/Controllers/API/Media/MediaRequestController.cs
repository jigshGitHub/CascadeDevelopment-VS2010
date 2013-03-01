using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
using Cascade.Helpers;
using CascadeBusiness = Cascade.Business;

namespace Cascade.Web.Controllers.API.Media
{
    public class MediaRequestController : ApiController
    {
        public IEnumerable<MSI_MediaRequestResponse> Get(string agency,Guid userId)
        {
            IEnumerable<MSI_MediaRequestResponse> data = null;
            DataQueries query = new DataQueries();
            try{
                data = from requestResponse in query.GetMediaRequestResponses(agency,userId).Where(record => record.AgencyId == agency)
                       select requestResponse;
            }

            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Gets MediaRequest : {0}", ex.Message))
                });

            }
            return data;

        }

        [HttpGet]
        public MSI_MediaRequestResponse Details(string id)
        {
            MSI_MediaRequestResponse data = null;
            DataQueries query = new DataQueries();
            try
            {
                data = query.GetMediaRequestResponse(id);
            }

            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Details MediaRequest : {0}", ex.Message))
                });

            }
            if (data == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No data available id = {0}", id)),
                    ReasonPhrase = "No request found"
                };
                throw new HttpResponseException(resp);
            }
            return data;
        }

        [HttpGet]
        public MSI_MediaRequestResponse Details(string accountNumber, string agency, Guid userId)
        {
            MSI_MediaRequestResponse data = null;
            DataQueries query = new DataQueries();
            try
            {
                data = query.GetMediaRequestResponse(accountNumber, agency, userId);
            }

            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Details MediaRequest : {0}", ex.Message))
                });

            }
            return data;
        }

        public MSI_MediaRequestResponse Post(MSI_MediaRequestResponse submittedRequest)
        {
            DataQueries query = new DataQueries();
            bool isMediaRequestTypeUpdateMode = false;
            CascadeBusiness.MediaRequest business = new CascadeBusiness.MediaRequest();
            try
            {
                if (string.IsNullOrEmpty(submittedRequest.Id))
                {
                    submittedRequest.Id = Guid.NewGuid().ToString();

                    foreach (MSI_MediaRequestTypes mediaReqType in submittedRequest.MSI_MediaRequestTypes)
                    {
                        if (mediaReqType.RequestStatusId.HasValue)
                        {
                            if (mediaReqType.RequestStatusId == (int)MediaRequestStatus.RequestFulfillment)
                            {
                                isMediaRequestTypeUpdateMode = true;
                                business.PerfomPostFulfillmentProcess(mediaReqType.Id, mediaReqType.RespondedUserID.Value);
                            }
                        }
                        else
                        {
                            mediaReqType.Id = Guid.NewGuid().ToString();
                            mediaReqType.RequestedId = submittedRequest.Id;
                            mediaReqType.RequestStatusId = (int)MediaRequestStatus.RequestReceived;
                            mediaReqType.LastUpdatedDate = DateTime.Now;
                            mediaReqType.LastUpdatedBy = submittedRequest.RequestedByUserId;
                        }
                    }
                    submittedRequest.RequestedDate = DateHelper.GetDateWithTimings(submittedRequest.RequestedDate);
                    if (!isMediaRequestTypeUpdateMode)
                    {
                        query.AddMediaRequestResponse(submittedRequest);
                        business.PerfomPreFulfillmentProcess(submittedRequest.ACCOUNT, null);
                    }

                }
                else
                {
                    MSI_MediaRequestTypes mediaType = null;
                    foreach (MSI_MediaRequestTypes mediaReqType in submittedRequest.MSI_MediaRequestTypes)
                    {
                        mediaType = query.GetMediaRequestdType(submittedRequest.Id, mediaReqType.TypeId);
                        if (mediaType == null)
                        {
                            mediaReqType.Id = Guid.NewGuid().ToString();
                            mediaReqType.RequestedId = submittedRequest.Id;
                            mediaReqType.RequestStatusId = (int)MediaRequestStatus.RequestReceived;
                            mediaReqType.LastUpdatedDate = DateTime.Now;
                            mediaReqType.LastUpdatedBy = submittedRequest.RequestedByUserId;
                            query.AddMediaRequestdType(mediaReqType);

                            business.PerfomPreFulfillmentProcess(submittedRequest.ACCOUNT, null);
                        }
                        else
                        {
                        }
                        mediaType = null;

                    }
                }
            }

            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in POST MediaRequest : {0}", ex.Message))
                });

            }
            //if (account == null)
            //{
            //    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            //    {
            //        Content = new StringContent(string.Format("No account information found for account number = {0}", accountNumber)),
            //        ReasonPhrase = "Product ID Not Found"
            //    };
            //    throw new HttpResponseException(resp);
            //}

            return submittedRequest;
        }

    }
}
