using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using Cascade.Web.Infracture;

namespace Cascade.Web.Presentation.ViewModels.DPS
{
    public class AddDPSViewModel
    {
        
        #region Original Request Data
        [Required]
        [Display(Name = "DateRec:")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime DateRec { get; set; }

        [Required]
        [Display(Name = "Original Account #")]
        public string OriginalAccount { get; set; }
        
        [Required]
        [Display(Name = "PIMS Account Number:")]
        public string PIMSAccount { get; set; }

        [Required]
        [Display(Name = "Amount:")]
        public Decimal Amount { get; set; }

        [Required]
        [Display(Name = "Net Payment:")]
        public Decimal NetPayment { get; set; }

        [Required]
        [Display(Name = "TransCode:")]
        public IList<Sup_TransCode> TransCodes { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Display(Name = "Tran Date:")]
        public DateTime TranDate { get; set; }

        [Required]
        [Display(Name = "Check Number:")]
        public string CheckNumber { get; set; }
        
        [Required]
        [Display(Name = "PmtType:")]
        public IList<Sup_PmtType> PmtTypes { get; set; }

        [Required]
        [Display(Name = "TransSource:")]
        public IList<Sup_TransSource> TransSources { get; set; }
        #endregion
        
        #region PIMS Data
        [Required]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Portfolio#:")]
        public IList<Port_Acq> Portfolios { get; set; }

        [Required]
        [Display(Name = "Current Responsibility:")]
        public IList<Sup_Company> Responsibilities { get; set; }
        #endregion

        #region Follow-up Data
        [Required]
        [Display(Name = "Our Check (LEAVE BLANK):")]
        public string OurCheck { get; set; }

        [Required]
        [Display(Name = "Uploaded(y/n):")]
        public bool Uploaded { get; set; }
        #endregion

    }
}