using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Configuration;
using Cascade.Web.Models;
using Cascade.Web.ApplicationIntegration;
namespace Cascade.Web.Controllers.API.UserAccount
{
    public class ProfileController : ApiController
    {

        
        public void Post(string userId, string firstName, string lastName, string email, string role, string roleEntityValue)
        {
            string userName = firstName.ToLower() + "." + lastName.ToLower();
            AccountProfile profile;
            if (!string.IsNullOrEmpty(userId))
            {

                profile = new AccountProfile(UserEntitiesDataFactory.GetUser(Guid.Parse(userId)).UserName);
            }
            else
            {
                if (UserEntitiesDataFactory.IsUserExits(userName))
                {
                    userName = userName + UserEntitiesDataFactory.UsersCount(userName).ToString();
                }

                //userId = UserEntitiesDataFactory.CreateUserWithRoles(userName, email, role);
                profile = new AccountProfile(userName);
            }

            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.RoleEntityValue = roleEntityValue;
        }
    }
}
