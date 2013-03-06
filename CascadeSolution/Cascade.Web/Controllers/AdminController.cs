using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cascade.Web.Models;
using Cascade.Web.ApplicationIntegration;
namespace Cascade.Web.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult ManageUsers()
        {
            return View();
        }


        public ActionResult GetUsers()
        {
            return PartialView("_users");
        }

        public JsonResult GetUser(string id)
        {
            UserModel user = new UserModel(Guid.Parse(id));
            return Json(user, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetApplicationRoles()
        {
            return Json(Roles.GetAllRoles(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateUser(string userId, string firstName, string lastName, string email, string role, string roleEntityValue)
        {
            string userName = firstName.ToLower() + "." + lastName.ToLower();
            AccountProfile profile;
            if (!string.IsNullOrEmpty(userId))
            {
                profile = new AccountProfile(UserEntitiesDataFactory.GetUser(Guid.Parse(userId)).UserName);
                UserEntitiesDataFactory.UpdateUser(userId, email);
            }
            else
            {
                if (UserEntitiesDataFactory.IsUserExits(userName))
                {
                    userName = userName + UserEntitiesDataFactory.UsersCount(userName).ToString();
                }

                userId = UserEntitiesDataFactory.CreateUserWithRoles(userName, email, role).ToString();
                profile = new AccountProfile(userName);
            }

            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.RoleEntityValue = roleEntityValue;
        }
    }
}
