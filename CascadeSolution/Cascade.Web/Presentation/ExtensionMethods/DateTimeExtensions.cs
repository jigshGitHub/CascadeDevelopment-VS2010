using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cascade.Web.Presentation.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.AddHours(date.Hour * -1).AddMinutes(date.Minute * -1);
        }
        public static DateTime BeginningOfMonth(this DateTime date)
        {
            return date.AddDays((date.Day * -1) + 1).BeginningOfDay();
        }

        public static string ConverToDate(this DateTime? date)
        {
            //return String.Format("{0:M/d/yyyy}", date);
            return String.Format("{0:MM/dd/yyyy}", date);
        }
    }
}