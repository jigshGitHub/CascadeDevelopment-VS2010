using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.Collections.Models
{
    public class Mappings
    {
        public Mappings()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// This helps to Map Client side model which is ViewModel to Server side Model(DB Model)
        /// </summary>
        /// <param name="dpsToSave"></param>
        /// <param name="_dpsform"></param>
        public static void ViewModelToModel(Port_DPS dpsToSave, Form _dpsform)
        {
            dpsToSave.DateRec = _dpsform.DateRec;
            dpsToSave.OrigAcct_ = _dpsform.OriginalAccount;
            dpsToSave.PIMSAcct_ = _dpsform.PIMSAccount;
            dpsToSave.Amount = _dpsform.Amount;
            dpsToSave.Net_Payment = _dpsform.NetPayment;
            dpsToSave.TransCode = _dpsform.TransCode;
            dpsToSave.TransDate = _dpsform.TranDate;
            dpsToSave.OurCheckNumber = _dpsform.OurCheck;
            dpsToSave.PmtType = _dpsform.PmtTypeId;
            dpsToSave.TransSource = _dpsform.TransSourceId;
            dpsToSave.AcctName = _dpsform.Name;
            dpsToSave.CurrentResp = _dpsform.Responsibility;
            dpsToSave.Portfolio_ = _dpsform.Portfolio;
            dpsToSave.Uploaded__y_n_ = _dpsform.Uploaded;
            dpsToSave.CheckNumber = _dpsform.CheckNumber;
        }

        public static void ViewModelToModelWithId(Port_DPS dpsToSave, Form _dpsform)
        {
            dpsToSave.ID = _dpsform.ID;
            dpsToSave.DateRec = _dpsform.DateRec;
            dpsToSave.OrigAcct_ = _dpsform.OriginalAccount;
            dpsToSave.PIMSAcct_ = _dpsform.PIMSAccount;
            dpsToSave.Amount = _dpsform.Amount;
            dpsToSave.Net_Payment = _dpsform.NetPayment;
            dpsToSave.TransCode = _dpsform.TransCode;
            dpsToSave.TransDate = _dpsform.TranDate;
            dpsToSave.OurCheckNumber = _dpsform.OurCheck;
            dpsToSave.PmtType = _dpsform.PmtTypeId;
            dpsToSave.TransSource = _dpsform.TransSourceId;
            dpsToSave.AcctName = _dpsform.Name;
            dpsToSave.CurrentResp = _dpsform.Responsibility;
            dpsToSave.Portfolio_ = _dpsform.Portfolio;
            dpsToSave.Uploaded__y_n_ = _dpsform.Uploaded;
            dpsToSave.CheckNumber = _dpsform.CheckNumber;
        }
    }
}