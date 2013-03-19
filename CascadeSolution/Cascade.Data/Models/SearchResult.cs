using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.Data.Models
{
    public class SearchResult
    {
        //public string Name { get; set; }
        //public string ProductDescription { get; set; }
        //public string WorkStatusDescription { get; set; }
        //public string RespAgency { get; set; }
        //public string StatusDescription { get; set; }
        //public string DIVISION_ID { get; set; }
        //public string ACCOUNT { get; set; }
        //public string PrimaryAccount { get; set; }
        //public string InvoiceID { get; set; }
        //public string ToolSetID { get; set; }
        //public string WorkStatusID { get; set; }
        //public string DateWorked { get; set; }
        //public string DEBT_TYPE { get; set; }
        //public string PRODUCT_CODE { get; set; }
        //public string BRANCH_NUMBER { get; set; }
        //public string CREDIT_LIMIT { get; set; }
        //public string CONTRACT_DATE { get; set; }
        //public string INT_RATE_CODE { get; set; }
        //public string INT_START_DATE { get; set; }
        //public string CYCLE_CODE { get; set; }
        //public string LAST_POST_DATE { get; set; }
        //public string CHGOFF_DATE { get; set; }
        //public string CHGOFF_RSN_CODE { get; set; }
        //public string CHGOFF_PRINCPAL { get; set; }
        //public string CHGOFF_INTEREST { get; set; }
        //public string CHGOFF_OTH_AMTS { get; set; }
        //public string REASON_INTO_COL { get; set; }
        //public string FIRST_PAY_DEFAULT { get; set; }
        //public string CYCLES_DELQ { get; set; }
        //public string LAST_PAY_DATE { get; set; }
        //public string LAST_CONT_DATE { get; set; }
        //public string LAST_NOTE_DATE { get; set; }
        //public string LAST_NOTE_CODE { get; set; }
        //public string Transfer_By { get; set; }
        //public string Previous_Responsibility { get; set; }
        //public string Previous_Date_Placed { get; set; }
        //public string RESPONSIBILITY { get; set; }
        //public string RESP_TYPE { get; set; }
        //public string STATUS { get; set; }
        //public string Status_Change_Date { get; set; }
        //public string Previous_Status { get; set; }
        //public string Previous_Status_Date { get; set; }
        //public string ALLOC_CODE { get; set; }
        //public string TOT_CHARGES { get; set; }
        //public string TOT_PRINCIPAL { get; set; }
        //public string TOT_INTEREST { get; set; }
        //public string TOT_COST { get; set; }
        //public string TOT_OTHER { get; set; }
        //public string TOT_NUM_PMTS { get; set; }
        //public string TOT_PTD { get; set; }
        //public string TOT_PRIN_PTD { get; set; }
        //public string TOT_INT_PTD { get; set; }
        //public string TOT_COST_PTD { get; set; }
        //public string TOT_OTH_INC_PTD { get; set; }
        //public string TOT_DELQ_AMT { get; set; }
        //public string TOT_DUE_AMT { get; set; }
        //public string TOT_PRIN_DUE { get; set; }
        //public string TOT_INT_DUE { get; set; }
        //public string TOT_COSTS_DUE { get; set; }
        //public string TOT_OTHER_DUE { get; set; }
        //public string SETTLE_ALLOW { get; set; }
        //public string TOT_NSF_AMT { get; set; }
        //public string NUM_NSF_PMNTS { get; set; }
        //public string DATE_LAST_NSF { get; set; }
        //public string LAST_NSF_AMT { get; set; }
        //public string FRST_PMT_DATE { get; set; }
        //public string FIRST_PMT_AMOUNT { get; set; }
        //public string NEXT_CYCLE { get; set; }
        //public string CALL_ATMPS_CONT { get; set; }
        //public string WKDY_CALL_ATMPS { get; set; }
        //public string WKNT_CALL_ATMPS { get; set; }
        //public string WKND_CALL_ATMPS { get; set; }
        //public string PLACEMENT_CODE { get; set; }
        //public string DATE_SENT_AGENCY { get; set; }
        //public string DATE_LAST_RECALL { get; set; }
        //public string BAD_PHONE_FLAG { get; set; }
        //public string BAD_ADDR_FLAG { get; set; }
        //public string HOME_OWN_FLAG { get; set; }
        //public string HOLD_FLAG { get; set; }
        //public string FIRST_CONT_FLAG { get; set; }
        //public string LAST_CALL_NUM { get; set; }
        //public string LAST_CALL_DATE { get; set; }
        //public string LAST_CALL_TIME { get; set; }
        //public string LAST_CALL_RESULT { get; set; }
        //public string LAST_CALL_STATUS { get; set; }
        //public string LAST_LETTER_GENERATED { get; set; }
        //public string LAST_LETTER_GENERATED_DATE { get; set; }
        //public string LAST_LETTER_PRINTED { get; set; }
        //public string LAST_LETTER_PRINTED_DATE { get; set; }
        //public string HEADER_NOTE { get; set; }
        //public string UPDATE_DATE { get; set; }
        //public string UPDATE_TYPE { get; set; }
        //public string UPDATE_ID { get; set; }
        //public string FLAGS { get; set; }
        //public string LAST_PMT_AMT { get; set; }
        //public string REPORT_TO_BUREAU { get; set; }
        //public string BUR_UPDATE_DATE { get; set; }
        //public string BUR_UPDATE_TIME { get; set; }
        //public string BUR_STATUS { get; set; }
        //public string BUR_DISPUTE_STATUS { get; set; }
        //public string BUR_BKT_STATUS { get; set; }
        //public string BUR_SPECIAL_STATUS { get; set; }
        //public string BUR_STATUS_DATE { get; set; }
        //public string SCORE_STRATEGY { get; set; }
        //public string CREDIT_SCORE { get; set; }
        //public string BEHAVIOR_SCORE { get; set; }
        //public string PurchaseDate { get; set; }
        //public string PutbackDeadline { get; set; }
        //public string Putback { get; set; }
        //public string PutbackDate { get; set; }
        //public string SoldTo { get; set; }
        //public string Sold { get; set; }
        //public string SoldDate { get; set; }
        //public string Buyback { get; set; }
        //public string BuybackDate { get; set; }
        //public string PreviousOwner { get; set; }
        //public string PreviousAcct { get; set; }
        //public string OriginalBalance { get; set; }
        //public string OriginalPrincipal { get; set; }
        //public string OriginalInterest { get; set; }
        //public string Originator { get; set; }
        //public string OriginalAccount { get; set; }
        //public string oLastPayDate { get; set; }
        //public string oLastPayAmt { get; set; }
        //public string Client { get; set; }
        //public string ClientAccount { get; set; }
        //public string cLastPayDate { get; set; }
        //public string cLastPayAmt { get; set; }
        //public string Commission { get; set; }
        //public string Seller { get; set; }
        //public string SellerAccount { get; set; }
        //public string sLastPayDate { get; set; }
        //public string sLastPayAmt { get; set; }
        //public string Bankrupt { get; set; }
        //public string POE { get; set; }
        //public string Stage { get; set; }
        //public string Legal { get; set; }
        //public string LegalReject { get; set; }
        //public string PreLegalReject { get; set; }
        //public string Note { get; set; }
        //public string RetailVendor { get; set; }
        //public string WIP { get; set; }
        //public string WIPDate { get; set; }
        //public string PortfolioID { get; set; }
        //public string User1 { get; set; }
        //public string User2 { get; set; }
        //public string User3 { get; set; }
        //public string User4 { get; set; }
        //public string User5 { get; set; }
        //public string User6 { get; set; }
        //public string User7 { get; set; }
        //public string User8 { get; set; }
        //public string User9 { get; set; }
        //public string User10 { get; set; }
        //public string LastChargeDate { get; set; }
        //public string LastChargeAmt { get; set; }
        //public string DateOfOccur { get; set; }
        //public string YearsSinceLastActivity { get; set; }
        //public string CreateDate { get; set; }
        //public string FileID { get; set; }
        //public string LastCashAdvance { get; set; }
        //public string LastCashAdvanceAmt { get; set; }
        //public string LastPDCDate { get; set; }
        //public string LastPDCAmount { get; set; }
        //public string insight_score { get; set; }
        //public string FieldParse { get; set; }
        //public string Archive { get; set; }
        //public string Purge { get; set; }
        //public string MonthlyPayment { get; set; }
        //public string Terms { get; set; }
        //public string MatureDate { get; set; }
        //public string ScriptID { get; set; }
        //public string StartingPayments { get; set; }
        //public string StartingPaymentAmount { get; set; }
        //public string PaymentPlanDate { get; set; }
        //public string LAST_LETTER_CODE { get; set; }
        //public string LAST_LETTER_DATE { get; set; }
        //public string MEDIA_FLAG { get; set; }
        //public string OriginalInterestDate { get; set; }
        //public string ClientID { get; set; }
        //public string CollectorCommission { get; set; }
        //public string User11 { get; set; }
        //public string User12 { get; set; }
        //public string User13 { get; set; }
        //public string User14 { get; set; }
        //public string User15 { get; set; }
        //public string User16 { get; set; }
        //public string User17 { get; set; }
        //public string User18 { get; set; }
        //public string User19 { get; set; }
        //public string User20 { get; set; }
        //public string LastCCDDate { get; set; }
        //public string LastCCDAmount { get; set; }
        //public string LastPRMDate { get; set; }
        //public string LastPRMAmount { get; set; }
        //public string DateLastPRMEntered { get; set; }
        //public string FileNo { get; set; }
        //public string Forw_File { get; set; }
        //public string Masco_File { get; set; }
        //public string Forw_ID { get; set; }
        //public string Firm_ID { get; set; }
        //public string AssignDate { get; set; }
        //public string CheckFeeType { get; set; }
        //public string CreditCardFeeType { get; set; }
        //public string CheckFee { get; set; }
        //public string CreditCardFee { get; set; }
        //public string Claim_Date { get; set; }
        //public string Law_List { get; set; }
        //public string sFee { get; set; }
        //public string Rate_Pre { get; set; }
        //public string Rates_Post { get; set; }
        //public string CreditorName { get; set; }
        //public string CreditorName2 { get; set; }
        //public string CreditorStreet { get; set; }
        //public string CreditorCS { get; set; }
        //public string CreditorZip { get; set; }
        //public string Debt_Bal { get; set; }
        //public string Debtor_No { get; set; }
        //public string Batch_No { get; set; }
        //public string SOL_Date { get; set; }
        //public string TU_Score { get; set; }
        //public string PimsLoadDate { get; set; }
        //public string Closed { get; set; }
        //public string DateClosed { get; set; }
        //public string LastTranCode { get; set; }
        //public string LastTranSource { get; set; }
        //public string LastReturnCode { get; set; }
        //public string AnnualRateCodeID { get; set; }
        //public string LastMediaAttachDate { get; set; }
        //public string StatuteTypeID { get; set; }
        //public string RestrictedAccount { get; set; }
        //public string Previous_WorkStatusID { get; set; }
        //public string WorkStatusID_ChangeDate { get; set; }
        //public string WorkStatusID_UpdateID { get; set; }
        //public string CitationCount { get; set; }
        //public string DONOTACH { get; set; }
        //public string Liquidation { get; set; }
        //public string AgencyAccount { get; set; }
        //public string PurchasePrice { get; set; }
        //public string SalesPrice { get; set; }

        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public string WorkStatusDescription { get; set; }
        public string Seller { get; set; }
        public string Originator { get; set; }
        public string ACCOUNT { get; set; }
        public string StatusDescription { get; set; }
        public string RespAgency { get; set; }
                
        public SearchResult()
        {
        }
    }

    public class Purchases
    {
        public string ACCOUNT;
        public string PRODUCT_CODE;
        public DateTime? PurchaseDate;
        public Decimal? OriginalBalance;
        public Decimal? purchaseprice;
        public string Seller;
        public Purchases()
        {
        }
    }

    public class Sales
    {
        public string ACCOUNT;
        public string PRODUCT_CODE;
        public string Responsibility;
        public DateTime? SoldDate;
        public Decimal? SalesPrice;
        public Decimal? FaceValue;
        public Sales()
        {
        }
    }

    public class PortfolioTransactions
    {
        public string ACCOUNT;
        public string PRODUCT_CODE;
        public string Responsibility;
        public DateTime? Tran_Date;
        public Decimal? Amount;
        public string TRAN_CODE;
        public string TRAN_SOURCE;
        public PortfolioTransactions()
        {
        }
    }

    public class PortfolioSummary
    {
        public string Description;
        public string Type;
        public int Accounts;
        public Decimal? Balance;
        public string ShortDescription;
        public string Seller;
        public string Status;
        public PortfolioSummary()
        {
        }
    }

    public class DPSViewEditResult
    {
        public string Portfolio;
        public string AcctName;
        public Decimal? Amount;
        public Decimal? NetPayment;
        public string CheckNumber;
        public string PmtType;
        public string TransSource;
        public string TransCode;
        public string CurrentResp;
        public string OurCheckNumber;
        public string Uploaded;
        public DateTime? TransDate;
        public DateTime? DateRec;
        public string PIMSAcct;
        public string OriginalAcct;
        public string GUID;
        public int ID;
        public Decimal? TotalAmount;
        public DPSViewEditResult()
        {
        }

    }

    public class ComplianceViewResult
    {
        public ComplianceViewResult()
        {
        }
        public string AgencyId;
        public string AgencyName;
        public string Account;
        public string LastName;
        public string FirstName;
        public string Address;
        public string City;
        public string State;
        public string Zip;
        public string HomePhone;
        public string WorkPhone;
        public string MobilePhone;
        public string LastFourSSN;
        public string DebtorIdentityVerified;
        public string ContactMethod;
        public string ContactTime;
        public string CreditorName;
        public string DebtProduct;
        public Decimal? DebtPurchaseBalance;
        public Decimal? DebtCurrentBalance;
        public string DisputeDebt;
        public string DisputeDebtAmount;
        public string DisputeDebtDueDate;
        public string ComplaintID;
        public DateTime? ComplaintDate;
        public string ComplaintReceivedByMethod;
        public string ComplaintIssue;
        public string ComplaintNotes;
        public string ComplaintSubmitedToAgency;
        public DateTime? ComplaintSubmitedToAgencyDate;
        public string MoreInfoReqdFromDebtor;
        public DateTime? MoreInfoRequestedDate;
        public string MoreInfoRequested;
        public string MoreInfoReceivedFromDebtor;
        public DateTime? MoreInfoReceivedDate;
        public string MoreInfoReceived;
        public string ComplaintSubmittedToOwner;
        public DateTime? ComplaintSubmittedDate;
        public string TimeToSubmitDays;
        public string MoreInfoFromAgency;
        public DateTime? MoreInfoFromAgencyRequestedDate;
        public string MoreInfoFromAgencyRequested;
        public string MoreInfoFromAgencyReceived;
        public DateTime? MoreInfoFromAgencyReceivedDate;
        public string OwnerResponse;
        public DateTime? OwnerResponseDate;
        public string OwnerResponseDays;
        public DateTime? AgencyResponseToDebtorDate;
        public string TotalResponseTimeDays;
        public string DebtorAgree;
        public string NeedFurtherAction;
        public string FinalActionStep;
        public string IsViewedByOwner;
        public string CreatedBy;
        public string IsViewedByAgency;
    }

    public class MediaViewEditResult
    {
        public string Portfolio;
        public string AcctName;
        public Decimal? SellerFee;
        public Decimal? OurFee;
        public DateTime? OrderDate;
        public DateTime? OpenDate;
        public DateTime? CODate;
        public string PIMSAcct;
        public string OrigAcct;
        public string GUID;
        public int ID;
        public string CompanyRequesting;
        public string OriginalLender;
        public string SSN;
        public string IsApplication;
        public string IsAffidavitIssuer;
        public string IsAffidavitSeller;
        public string IsUnavailable;
        public string IsClosed;
        public string Explanation;
        public string MediaTypeReceived;
        public string Notes;
        public string SellerInvoice;
        public string BuyerCheck;
        public string OurCheck;
        public string Seller;
        public string OrderNumber;
        public string OurInvoice;
        public DateTime? StmtsFrom;
        public DateTime? StmtsTo;
        public DateTime? DateSubmitted;
        public DateTime? DateConfirmed;
        public DateTime? DateSellerPaid;
        public DateTime? DateReceived;
        public DateTime? DateForwarded;
        public DateTime? UnavailableDate;
        public DateTime? DatePayRec;
        public string FileName;
        public MediaViewEditResult()
        {
        }

    }

    public partial class vwAccount
    {
        public string GUID;
    }

    #region Recalls

    public class RecallViewEditResult
    {
        public string Portfolio;
        public string AcctName;
        public Decimal? FaceValueofAcct;
        public DateTime? DateNoteSent;
        public DateTime? DateAcctClosed;
        public DateTime? Date;
        public DateTime? UploadedDate;
        public string CurrentResp;
        public string RecallReason;
        public string NewStatus;
        public string NewResp;
        public string CheckNumber;
        public string Invoice;
        public string Explanation;
        public Decimal? CostBasis;
        public Decimal? SalesBasis;
        public string SellerCheck;
        public string Putback;
        public Decimal? AmtReceivable;
        public Decimal? AmtPayable;
        public string Seller;
        public string PIMSAcct;
        public string OrigAcct;
        public string GUID;
        public int ID;
        public Decimal? TotalAmount;
        public string InitiatedBy;
        public string CheckDocuments;
        public RecallViewEditResult()
        {
        }
    }

    public class PutBackViewEditResult
    {
        public string Portfolio;
        public string AcctName;
        public Decimal? FaceValueofAcct;
        public DateTime? DateNoteSent;
        public DateTime? DateAcctClosed;
        public DateTime? Date;
        public DateTime? UploadedDate;
        public string CurrentResp;
        public string PutBackReason;
        public string NewStatus;
        public string NewResp;
        public string CheckNumber;
        public string Invoice;
        public string Explanation;
        public Decimal? CostBasis;
        public Decimal? SalesBasis;
        public string SellerCheck;
        public Decimal? AmtReceivable;
        public Decimal? AmtPayable;
        public string Seller;
        public string PIMSAcct;
        public string OrigAcct;
        public string GUID;
        public int ID;
        public Decimal? TotalAmount;
        public string InitiatedBy;
        public string CheckDocuments;
        public PutBackViewEditResult()
        {
        }
    }

    #endregion



    #region Testing
    public class PeopleViewEditResult
    {
        public string FName;
        public string LName;
        public string Address;
        public DateTime? CreatedAt;
        public int PID;
        public PeopleViewEditResult()
        {
        }
    }

    public class MoneyViewEditResult
    {
        public DateTime? CreatedAt;
        public int MID;
        public Decimal? Amount;
        public string SText;
        public string FName;
        public string LName;
        public MoneyViewEditResult()
        {
        }
    }

    public class SourceViewEditResult
    {
        public DateTime? CreatedAt;
        public int SID;
        public string  SText;
        public SourceViewEditResult()
        {
        }
    }
    #endregion
    

}
