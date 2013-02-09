using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.Collections.Models
{
    public class Form
    {
        public Form()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        #region Original Request Data
        public int ID { get; set; }
        public DateTime? DateRec { get; set; }
        public string OriginalAccount { get; set; }
        public string PIMSAccount { get; set; }
        public Decimal? Amount { get; set; }
        public Decimal? NetPayment { get; set; }
        public string TransCode { get; set; }
        public DateTime? TranDate { get; set; }
        public string CheckNumber { get; set; }
        public int? PmtTypeId { get; set; }
        public int? TransSourceId { get; set; }
        #endregion

        #region PIMS Data
        public string Name { get; set; }
        public string Portfolio { get; set; }
        public string Responsibility { get; set; }
        #endregion

        #region Follow-up Data
        public string OurCheck { get; set; }
        public DateTime? Uploaded { get; set; }
        #endregion
    }
}