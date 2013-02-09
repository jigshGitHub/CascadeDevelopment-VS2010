using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Cascade.Web.Presentation.ExtensionMethods
{
    public static class DecimalHelpers
    {
        public static string ToIntegerCurrency(this Decimal? dec)
        {
            var nfi = new System.Globalization.NumberFormatInfo();
            nfi.CurrencyDecimalDigits = 2;
            nfi.CurrencySymbol = "$";
            return String.Format(nfi, "{0:c}", dec);
       }

        public static string GetIntegerCurrency(this Decimal dec)
        {
            var nfi = new System.Globalization.NumberFormatInfo();
            nfi.CurrencyDecimalDigits = 2;
            nfi.CurrencySymbol = "$";
            return String.Format(nfi, "{0:c}", dec);

        }        

        public static int ToInteger(this Decimal? dec)
        {
            try
            {
                return System.Convert.ToInt32(dec);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static string ToPercentage(this Decimal? dec)
        {
            var nfi = new System.Globalization.NumberFormatInfo();
            nfi.PercentDecimalDigits = 2;
            return String.Format(nfi, "{0:P2}", dec);
        }

                
    }
}