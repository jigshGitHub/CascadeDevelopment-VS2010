using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;
using System.Web.Profile;

namespace Cascade.Web.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The New Password entered is not-consistent.  Please ensure the “New Password” and “Confirm New Password” match, and try again.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    [Serializable]
    public class UserModel
    {
        MembershipUser user { get; set; }
        AccountProfile profile;
        public UserModel(Guid userId)
        {
            user = Membership.GetUser(userId);
            profile = new AccountProfile(UserName);
        }

        public string UserId
        {
            get
            {
                return user.ProviderUserKey.ToString();
            }
        }

        public string UserName
        {
            get { return user.UserName; }
        }

        public string Email
        {
            get { return user.Email; }
        }


        public string  FirstName
        {
            get
            {
                return profile.FirstName;
            }
        }
        public string LastName
        {
            get
            {
                return profile.LastName;
            }
        }
        public string RoleKeyValue
        {
            get
            {
                return profile.RoleEntityValue;
            }
        }

        public string Role
        {
            get
            {
                string [] roles = Roles.GetRolesForUser(user.UserName);
                if (roles.Length > 0)
                    return roles.GetValue(0).ToString();
                else
                    return "";
            }
        }

    }
    [Serializable]
    public class AccountProfile : ProfileBase
    {
        //static public AccountProfile Current
        //{
        //    get
        //    {
        //        if (Membership.GetUser() != null)
        //            return (AccountProfile)(ProfileBase.Create(Membership.GetUser().UserName));
        //        else
        //            return null;
        //    }
        //}
        public AccountProfile()
        {
        }
        public AccountProfile(string userName)
        {
            base.Initialize(userName, true);
        }

        public string FirstName
        {
            get { return ((string)(base["FirstName"])); }
            set { base["FirstName"] = value; Save(); }
        }
        public string LastName
        {
            get { return ((string)(base["LastName"])); }
            set { base["LastName"] = value; Save(); }
        }
        public string RoleEntityValue
        {
            get { return ((string)(base["RoleEntityValue"])); }
            set { base["RoleEntityValue"] = value; Save(); }
        }

        // add additional properties here
    }
}
