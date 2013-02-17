using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
using Cascade.Helpers;

namespace Cascade.Web.Controllers.API.Media
{
    public class MediaRequestController : ApiController
    {
        public IEnumerable<MSI_MediaRequestResponse> Get(string agency)
        {
            IEnumerable<MSI_MediaRequestResponse> data = null;
            DataQueries query = new DataQueries();
            try{
                data = from requestResponse in query.GetMediaRequestResponses(agency).Where(record => record.AgencyId == agency)
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

        public MSI_MediaRequestResponse Details(string id)
        {
            return new MSI_MediaRequestResponse();
        }

        public MSI_MediaRequestResponse Post(MSI_MediaRequestResponse submittedRequest)
        {
            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                if (submittedRequest.Id == Guid.Empty)
                {
                    submittedRequest.Id =  Guid.NewGuid();
                    
                    foreach (MSI_MediaRequestedTypes mediaReqType in submittedRequest.MSI_MediaRequestedTypes)
                    {
                        mediaReqType.Id = Guid.NewGuid();
                        mediaReqType.RequestedId = submittedRequest.Id;
                    }
                    submittedRequest.RequestedDate = DateHelper.GetDateWithTimings(submittedRequest.RequestedDate);

                    repository.Add(submittedRequest); 

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
