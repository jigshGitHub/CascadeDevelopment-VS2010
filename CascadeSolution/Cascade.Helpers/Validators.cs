using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cascade.Helpers
{
    public static class Validators
    {
        public const string EmailStandard = @"^[a-zA-Z0-9._-]+@([a-zA-Z0-9.-]+\.)+[a-zA-Z0-9.-]{2,4}$";
        public static bool ValidateEmail(string emailID)
        {
            if (emailID != null)
                return System.Text.RegularExpressions.Regex.IsMatch(emailID, EmailStandard);
            else
                return false;
        }
    }
}
