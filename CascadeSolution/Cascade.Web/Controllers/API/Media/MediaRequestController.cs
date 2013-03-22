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
using System.Web.Security;
using Cascade.Web.ApplicationIntegration;
namespace Cascade.Web.Controllers.API.Media
{
    public class MediaRequestController : ApiController
    {
        private static readonly string thisClass = "Cascade.Web.Controllers.API.Media.MediaRequestController";
        
        public IEnumerable<MSI_MediaRequestResponse> Get(string agency, Guid userId)
        {
            IEnumerable<MSI_MediaRequestResponse> data = null;
            DataQueries query = new DataQueries();
            try
            {
                data = from requestResponse in query.GetMediaRequestResponses(agency, userId).Where(record => record.AgencyId == agency)
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
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters RequestId={1}", thisMethod, submittedRequest.Id);
            LogHelper.Info(logMessage);
            
            DataQueries query = new DataQueries();
            bool isMediaRequestTypeUpdateMode = false;
            CascadeBusiness.MediaRequest business = new CascadeBusiness.MediaRequest();
            string userRole="";
            List<string> emailRecipients = new List<string>();
            List<string> userIds = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(submittedRequest.Id))
                {
                    submittedRequest.Id = Guid.NewGuid().ToString();

                    foreach (MSI_MediaRequestTypes mediaReqType in submittedRequest.MSI_MediaRequestTypes)
                    {
                        if (mediaReqType.RequestStatusId.HasValue)
                        {
                            if (mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.RequestFulfillment)
                            {
                                isMediaRequestTypeUpdateMode = true;
                                business.PerfomPostFulfillmentProcess(mediaReqType.Id, mediaReqType.RespondedUserID.Value);
                            }
                            else if (mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.RequestComplete)
                            {
                                isMediaRequestTypeUpdateMode = true;
                                business.PerfomCompleteRequest(mediaReqType.Id, mediaReqType.RespondedUserID.Value);
                            }
                            else if (mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.OriginatorUpdateRequested ||
                                mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.OriginatorInProcess ||
                                mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.NoOriginatorMediaAvailable ||
                                mediaReqType.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.OriginatorFulfilled
                                )
                            {
                                isMediaRequestTypeUpdateMode = true;
                                business.PerfomUpdateRequest(mediaReqType.Id, mediaReqType.RespondedUserID.Value, mediaReqType.RequestStatusId.Value);
                            }
                        }
                        else
                        {
                            userRole = Roles.GetRolesForUser(Membership.GetUser(submittedRequest.RequestedByUserId).UserName).Single();
                            mediaReqType.Id = Guid.NewGuid().ToString();
                            mediaReqType.RequestedId = submittedRequest.Id;
                            mediaReqType.RequestStatusId = business.EvaluateMediaRequestStatus(CascadeBusiness.MediaRequestLevel.Initiate, (CascadeBusiness.UserRoles)Enum.Parse(typeof(CascadeBusiness.UserRoles), userRole, true));
                            mediaReqType.LastUpdatedDate = DateTime.Now;
                            mediaReqType.LastUpdatedBy = submittedRequest.RequestedByUserId;
                        }
                    }
                    submittedRequest.RequestedDate = DateHelper.GetDateWithTimings(submittedRequest.RequestedDate);
                    if (!isMediaRequestTypeUpdateMode)
                    {
                        business.SaveMediaRequestResponse(submittedRequest);

                        if (userRole == CascadeBusiness.UserRoles.mediaAdmin.ToString())
                        {
                            emailRecipients.Add(System.Configuration.ConfigurationManager.AppSettings["emailOriginatorAccount"]);
                            userIds.Add(Membership.GetUser(Membership.GetUserNameByEmail(System.Configuration.ConfigurationManager.AppSettings["emailOriginatorAccount"])).ProviderUserKey.ToString());
                        }
                        else if (userRole == CascadeBusiness.UserRoles.agency.ToString())
                        {
                            emailRecipients = MemberShipHelper.GetEmailAddressUserInRole(CascadeBusiness.UserRoles.mediaAdmin.ToString());
                            userIds = MemberShipHelper.GetUserIdsUserInRole(CascadeBusiness.UserRoles.mediaAdmin.ToString());
                        }
                        if (emailRecipients.Count > 0)
                        {
                            CascadeBusiness.NotificationMessage message = business.GetInitialMediaRequestMessage(submittedRequest);
                            Emailer.SendMessage(Emailer.CreateMessage(message.Subject, emailRecipients, message.BodyText, ""));
                            business.SaveMessageNotification(message, userIds);
                        }
                        business.PerfomPreFulfillmentProcess(submittedRequest.ACCOUNT, null);
                    }
                    else
                    {
                        #region OriginatorUpdateRequested Notifications
                        MSI_MediaRequestTypes requestType = submittedRequest.MSI_MediaRequestTypes.Where(record => record.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.OriginatorUpdateRequested).FirstOrDefault();
                        if (requestType != null)
                        {
                            userRole = Roles.GetRolesForUser(Membership.GetUser(requestType.RespondedUserID.Value).UserName).Single();

                            if (userRole == CascadeBusiness.UserRoles.mediaAdmin.ToString())
                            {
                                emailRecipients.Add(System.Configuration.ConfigurationManager.AppSettings["emailOriginatorAccount"]);
                                userIds.Add(Membership.GetUser(Membership.GetUserNameByEmail(System.Configuration.ConfigurationManager.AppSettings["emailOriginatorAccount"])).ProviderUserKey.ToString());
                            }
                            else if (userRole == CascadeBusiness.UserRoles.agency.ToString())
                            {
                                emailRecipients = MemberShipHelper.GetEmailAddressUserInRole(CascadeBusiness.UserRoles.mediaAdmin.ToString());
                                userIds = MemberShipHelper.GetUserIdsUserInRole(CascadeBusiness.UserRoles.mediaAdmin.ToString());
                            }
                            if (emailRecipients.Count > 0)
                            {
                                CascadeBusiness.NotificationMessage message = business.GetUpdateRequestedToOriginatorMessage(submittedRequest.MSI_MediaRequestTypes.Where(record => record.RequestStatusId== (int)CascadeBusiness.MediaRequestStatus.OriginatorUpdateRequested));
                                Emailer.SendMessage(Emailer.CreateMessage(message.Subject, emailRecipients, message.BodyText, ""));
                                business.SaveMessageNotification(message, userIds);
                            }

                        }
                        #endregion

                        #region RequestFulfillment Notifications
                        requestType = null;
                        requestType = submittedRequest.MSI_MediaRequestTypes.Where(record => record.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.RequestFulfillment).FirstOrDefault();
                        if (requestType != null)
                        {
                            string userEmail = MemberShipHelper.GetEmailAddress(requestType.RequestedUserID.Value);
                            if (!string.IsNullOrEmpty(userEmail))
                            {
                                emailRecipients.Add(userEmail);
                                userIds.Add(requestType.RequestedUserID.ToString());
                            }
                            if (emailRecipients.Count > 0)
                            {
                                CascadeBusiness.NotificationMessage message = business.GetRequestFulfillmentMessage(submittedRequest.MSI_MediaRequestTypes.Where(record => record.RequestStatusId == (int)CascadeBusiness.MediaRequestStatus.RequestFulfillment));
                                Emailer.SendMessage(Emailer.CreateMessage(message.Subject, emailRecipients, message.BodyText, ""));
                                business.SaveMessageNotification(message, userIds);
                            }

                        }

                        #endregion
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
                            mediaReqType.RequestStatusId = (int)CascadeBusiness.MediaRequestStatus.SentToOwner;
                            mediaReqType.LastUpdatedDate = DateTime.Now;
                            mediaReqType.LastUpdatedBy = submittedRequest.RequestedByUserId;
                            business.SaveMediaRequestdType(mediaReqType);

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
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in POST MediaRequest : {0}", ex.Message))
                });

            }

            return submittedRequest;
        }

    }
}
