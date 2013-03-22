using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Helpers;
using System.Web.Security;
namespace Cascade.Web.ApplicationIntegration
{
    public static class MenuHelper
    {
        public static MvcHtmlString CreateLink(this HtmlHelper helper, string linktext, string action, string controller, string area)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, new { @area = area });

            var anchor = new TagBuilder("a");
            anchor.InnerHtml = HttpUtility.HtmlEncode(linktext);
            anchor.MergeAttribute("href", url);

            if (IsActiveLink(helper, controller, action, area))
                anchor.MergeAttribute("class", "activeLink");
            //else if (linktext == "Recourse")
            //    anchor.MergeAttribute("class", "activeLink");

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }

        private static bool IsActiveLink(HtmlHelper helper, string controller, string action, string area)
        {
            //if (!IsTokenMatches(helper, area, "area"))
            //    return false;
            //if (!IsValueMatches(helper, controller, "controller"))
            //    return false;
            if (IsTokenMatches(helper, area, "area"))
                return true;
            if (IsValueMatches(helper, controller, "controller"))
                return true;

            return IsValueMatches(helper, action, "action");
        }

        public static bool IsTokenMatches(HtmlHelper helper, string item, string dataToken)
        {
            var routeData = (string)helper.ViewContext.RouteData.DataTokens[dataToken];

            if (dataToken == "action" && item == "Index" && string.IsNullOrEmpty(routeData))
                return true;
            if (dataToken == "controller" && item == "Home" && string.IsNullOrEmpty(routeData))
                return true;
            if (routeData == null) return false;
            return routeData == item;
        }

        public static bool IsValueMatches(HtmlHelper helper, string item, string dataToken)
        {
            var routeData = (string)helper.ViewContext.RouteData.DataTokens[dataToken];

            if (routeData == null) return false;
            return routeData == item;
        }
    }

    public static class MemberShipHelper
    {

        public static List<string> GetEmailAddressUserInRole(string role)
        {
            string[] usersInRole = Roles.GetUsersInRole(role);
            List<string> userEmails = new List<string>();
            string emailId;
            foreach (String user in usersInRole)
            {
                emailId = Membership.GetUser(user).Email;
                if (Validators.ValidateEmail(emailId))
                    userEmails.Add(emailId);
                else
                    LogHelper.Info(emailId + " is not valid");
            }
            return userEmails;
        }

        public static string GetEmailAddress(Guid userId)
        {
            string email = Membership.GetUser(userId).Email;
            if (Validators.ValidateEmail(email))
                return email;
            else
            {
                LogHelper.Info(email + " is not valid");
                return "";
            }

        }

        public static List<string> GetUserIdsUserInRole(string role)
        {
            string[] usersInRole = Roles.GetUsersInRole(role);
            List<string> userIds = new List<string>();
            string userId;
            foreach (String user in usersInRole)
            {
                userId = Membership.GetUser(user).ProviderUserKey.ToString();
                userIds.Add(userId);
            }
            return userIds;
        }
    }
}