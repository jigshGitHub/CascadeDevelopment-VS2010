using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.UI.Controls;
using Cascade.Web.ApplicationIntegration;
using System.Web.Security;
namespace Cascade.Web.Controllers
{
    public class BaseController : Controller
    {
        public Guid UserId { get { return (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey; } }
        public string UserName { get { return User.Identity.Name; } }
        public string[] UserRoles { get { return Roles.GetRolesForUser(); } }
        public string UserAgency
        {
            get
            {
                if (UserRoles.Contains("user"))
                    return "";
                else
                    return System.Configuration.ConfigurationManager.AppSettings["defaultAgencyId"];

            }
        }
        public BaseController()
        {
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FillMenu();
            base.OnActionExecuting(filterContext);
        }

        private void FillMenu()
        {
            ViewData["Menu"] = (CascadeMenuCollection)MenuFactory.CurrentMenu.MenuCollection;
        }

    }
}
