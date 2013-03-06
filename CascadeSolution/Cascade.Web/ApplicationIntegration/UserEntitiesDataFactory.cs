using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
namespace Cascade.Web.ApplicationIntegration
{
    public class UserEntitiesDataFactory
    {
        public static void DeleteUserWithRoles(string userName, string role)
        {
            Roles.RemoveUserFromRole(userName, role);
            Membership.DeleteUser(userName);
        }

        public static Guid CreateUserWithRoles(string userName, string email, string role)
        {
            MembershipUser user = Membership.CreateUser(userName, "pa$$word", email);
            if(!string.IsNullOrEmpty(role))
                Roles.AddUserToRole(user.UserName, role);
            return (Guid)user.ProviderUserKey;
        }

        public static void UpdateUser(string userId, string email)
        {
            MembershipUser user = Membership.GetUser(new Guid(userId));
            user.Email = email;
            Membership.UpdateUser(user);
        }

        public static bool IsUserExits(string userName)
        {
            return (Membership.GetUser(userName) != null);
        }

        public static int UsersCount(string userName)
        {
            return Membership.FindUsersByName(userName).Count;
        }

        public static MembershipUser GetUser(Guid userId)
        {
            return Membership.GetUser(userId);
        }
    }
}