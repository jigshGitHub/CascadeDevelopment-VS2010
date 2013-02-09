//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Cascade.Data.Models
{
    public partial class MSI_vwSearch
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public string ProductDescription { get; set; }
        public string WorkStatusDescription { get; set; }
        public string RespAgency { get; set; }
        public string StatusDescription { get; set; }
        public string DIVISION_ID { get; set; }
        public string ACCOUNT { get; set; }
        public string PrimaryAccount { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> ToolSetID { get; set; }
        public Nullable<int> WorkStatusID { get; set; }
        public Nullable<System.DateTime> DateWorked { get; set; }
        public string DEBT_TYPE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string BRANCH_NUMBER { get; set; }
        public Nullable<decimal> CREDIT_LIMIT { get; set; }
        public Nullable<System.DateTime> CONTRACT_DATE { get; set; }
        public string INT_RATE_CODE { get; set; }
        public Nullable<System.DateTime> INT_START_DATE { get; set; }
        public Nullable<decimal> CYCLE_CODE { get; set; }
        public Nullable<System.DateTime> LAST_POST_DATE { get; set; }
        public Nullable<System.DateTime> CHGOFF_DATE { get; set; }
        public string CHGOFF_RSN_CODE { get; set; }
        public Nullable<decimal> CHGOFF_PRINCPAL { get; set; }
        public Nullable<decimal> CHGOFF_INTEREST { get; set; }
        public Nullable<decimal> CHGOFF_OTH_AMTS { get; set; }
        public string REASON_INTO_COL { get; set; }
        public string FIRST_PAY_DEFAULT { get; set; }
        public Nullable<decimal> CYCLES_DELQ { get; set; }
        public Nullable<System.DateTime> LAST_PAY_DATE { get; set; }
        public Nullable<System.DateTime> LAST_CONT_DATE { get; set; }
        public Nullable<System.DateTime> LAST_NOTE_DATE { get; set; }
        public string LAST_NOTE_CODE { get; set; }
        public string Transfer_By { get; set; }
        public string Previous_Responsibility { get; set; }
        public Nullable<System.DateTime> Previous_Date_Placed { get; set; }
        public string RESPONSIBILITY { get; set; }
        public string RESP_TYPE { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> Status_Change_Date { get; set; }
        public string Previous_Status { get; set; }
        public Nullable<System.DateTime> Previous_Status_Date { get; set; }
        public string ALLOC_CODE { get; set; }
        public Nullable<decimal> TOT_CHARGES { get; set; }
        public Nullable<decimal> TOT_PRINCIPAL { get; set; }
        public Nullable<decimal> TOT_INTEREST { get; set; }
        public Nullable<decimal> TOT_COST { get; set; }
        public Nullable<decimal> TOT_OTHER { get; set; }
        public Nullable<decimal> TOT_NUM_PMTS { get; set; }
        public Nullable<decimal> TOT_PTD { get; set; }
        public Nullable<decimal> TOT_PRIN_PTD { get; set; }
        public Nullable<decimal> TOT_INT_PTD { get; set; }
        public Nullable<decimal> TOT_COST_PTD { get; set; }
        public Nullable<decimal> TOT_OTH_INC_PTD { get; set; }
        public Nullable<decimal> TOT_DELQ_AMT { get; set; }
        public Nullable<decimal> TOT_DUE_AMT { get; set; }
        public Nullable<decimal> TOT_PRIN_DUE { get; set; }
        public Nullable<decimal> TOT_INT_DUE { get; set; }
        public Nullable<decimal> TOT_COSTS_DUE { get; set; }
        public Nullable<decimal> TOT_OTHER_DUE { get; set; }
        public Nullable<double> SETTLE_ALLOW { get; set; }
        public Nullable<decimal> TOT_NSF_AMT { get; set; }
        public Nullable<decimal> NUM_NSF_PMNTS { get; set; }
        public Nullable<System.DateTime> DATE_LAST_NSF { get; set; }
        public Nullable<decimal> LAST_NSF_AMT { get; set; }
        public Nullable<System.DateTime> FRST_PMT_DATE { get; set; }
        public Nullable<decimal> FIRST_PMT_AMOUNT { get; set; }
        public string NEXT_CYCLE { get; set; }
        public Nullable<decimal> CALL_ATMPS_CONT { get; set; }
        public Nullable<decimal> WKDY_CALL_ATMPS { get; set; }
        public Nullable<decimal> WKNT_CALL_ATMPS { get; set; }
        public Nullable<decimal> WKND_CALL_ATMPS { get; set; }
        public string PLACEMENT_CODE { get; set; }
        public Nullable<System.DateTime> DATE_SENT_AGENCY { get; set; }
        public Nullable<System.DateTime> DATE_LAST_RECALL { get; set; }
        public string BAD_PHONE_FLAG { get; set; }
        public string BAD_ADDR_FLAG { get; set; }
        public string HOME_OWN_FLAG { get; set; }
        public string HOLD_FLAG { get; set; }
        public string FIRST_CONT_FLAG { get; set; }
        public string LAST_CALL_NUM { get; set; }
        public Nullable<System.DateTime> LAST_CALL_DATE { get; set; }
        public string LAST_CALL_TIME { get; set; }
        public string LAST_CALL_RESULT { get; set; }
        public string LAST_CALL_STATUS { get; set; }
        public string LAST_LETTER_GENERATED { get; set; }
        public Nullable<System.DateTime> LAST_LETTER_GENERATED_DATE { get; set; }
        public string LAST_LETTER_PRINTED { get; set; }
        public Nullable<System.DateTime> LAST_LETTER_PRINTED_DATE { get; set; }
        public string HEADER_NOTE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_TYPE { get; set; }
        public string UPDATE_ID { get; set; }
        public string FLAGS { get; set; }
        public Nullable<decimal> LAST_PMT_AMT { get; set; }
        public Nullable<bool> REPORT_TO_BUREAU { get; set; }
        public Nullable<System.DateTime> BUR_UPDATE_DATE { get; set; }
        public Nullable<decimal> BUR_UPDATE_TIME { get; set; }
        public string BUR_STATUS { get; set; }
        public string BUR_DISPUTE_STATUS { get; set; }
        public string BUR_BKT_STATUS { get; set; }
        public string BUR_SPECIAL_STATUS { get; set; }
        public Nullable<System.DateTime> BUR_STATUS_DATE { get; set; }
        public string SCORE_STRATEGY { get; set; }
        public string CREDIT_SCORE { get; set; }
        public Nullable<decimal> BEHAVIOR_SCORE { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<System.DateTime> PutbackDeadline { get; set; }
        public Nullable<bool> Putback { get; set; }
        public Nullable<System.DateTime> PutbackDate { get; set; }
        public string SoldTo { get; set; }
        public Nullable<bool> Sold { get; set; }
        public Nullable<System.DateTime> SoldDate { get; set; }
        public Nullable<bool> Buyback { get; set; }
        public Nullable<System.DateTime> BuybackDate { get; set; }
        public string PreviousOwner { get; set; }
        public string PreviousAcct { get; set; }
        public Nullable<decimal> OriginalBalance { get; set; }
        public Nullable<decimal> OriginalPrincipal { get; set; }
        public Nullable<decimal> OriginalInterest { get; set; }
        public string Originator { get; set; }
        public string OriginalAccount { get; set; }
        public Nullable<System.DateTime> oLastPayDate { get; set; }
        public Nullable<decimal> oLastPayAmt { get; set; }
        public string Client { get; set; }
        public string ClientAccount { get; set; }
        public Nullable<System.DateTime> cLastPayDate { get; set; }
        public Nullable<decimal> cLastPayAmt { get; set; }
        public string Commission { get; set; }
        public string Seller { get; set; }
        public string SellerAccount { get; set; }
        public Nullable<System.DateTime> sLastPayDate { get; set; }
        public Nullable<decimal> sLastPayAmt { get; set; }
        public Nullable<bool> Bankrupt { get; set; }
        public string POE { get; set; }
        public string Stage { get; set; }
        public Nullable<bool> Legal { get; set; }
        public Nullable<bool> LegalReject { get; set; }
        public Nullable<bool> PreLegalReject { get; set; }
        public string Note { get; set; }
        public string RetailVendor { get; set; }
        public Nullable<bool> WIP { get; set; }
        public Nullable<System.DateTime> WIPDate { get; set; }
        public Nullable<int> PortfolioID { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
        public string User3 { get; set; }
        public string User4 { get; set; }
        public string User5 { get; set; }
        public string User6 { get; set; }
        public string User7 { get; set; }
        public string User8 { get; set; }
        public string User9 { get; set; }
        public string User10 { get; set; }
        public Nullable<System.DateTime> LastChargeDate { get; set; }
        public Nullable<decimal> LastChargeAmt { get; set; }
        public Nullable<System.DateTime> DateOfOccur { get; set; }
        public Nullable<int> YearsSinceLastActivity { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> FileID { get; set; }
        public Nullable<System.DateTime> LastCashAdvance { get; set; }
        public Nullable<decimal> LastCashAdvanceAmt { get; set; }
        public Nullable<System.DateTime> LastPDCDate { get; set; }
        public Nullable<decimal> LastPDCAmount { get; set; }
        public string insight_score { get; set; }
        public string FieldParse { get; set; }
        public Nullable<bool> Archive { get; set; }
        public Nullable<bool> Purge { get; set; }
        public Nullable<decimal> MonthlyPayment { get; set; }
        public Nullable<int> Terms { get; set; }
        public Nullable<System.DateTime> MatureDate { get; set; }
        public int ScriptID { get; set; }
        public int StartingPayments { get; set; }
        public decimal StartingPaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentPlanDate { get; set; }
        public string LAST_LETTER_CODE { get; set; }
        public Nullable<System.DateTime> LAST_LETTER_DATE { get; set; }
        public string MEDIA_FLAG { get; set; }
        public Nullable<System.DateTime> OriginalInterestDate { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string CollectorCommission { get; set; }
        public string User11 { get; set; }
        public string User12 { get; set; }
        public string User13 { get; set; }
        public string User14 { get; set; }
        public string User15 { get; set; }
        public string User16 { get; set; }
        public string User17 { get; set; }
        public string User18 { get; set; }
        public string User19 { get; set; }
        public string User20 { get; set; }
        public Nullable<System.DateTime> LastCCDDate { get; set; }
        public Nullable<decimal> LastCCDAmount { get; set; }
        public Nullable<System.DateTime> LastPRMDate { get; set; }
        public Nullable<decimal> LastPRMAmount { get; set; }
        public Nullable<System.DateTime> DateLastPRMEntered { get; set; }
        public string FileNo { get; set; }
        public string Forw_File { get; set; }
        public string Masco_File { get; set; }
        public string Forw_ID { get; set; }
        public string Firm_ID { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<int> CheckFeeType { get; set; }
        public Nullable<int> CreditCardFeeType { get; set; }
        public Nullable<decimal> CheckFee { get; set; }
        public Nullable<decimal> CreditCardFee { get; set; }
        public Nullable<System.DateTime> Claim_Date { get; set; }
        public string Law_List { get; set; }
        public string sFee { get; set; }
        public string Rate_Pre { get; set; }
        public string Rates_Post { get; set; }
        public string CreditorName { get; set; }
        public string CreditorName2 { get; set; }
        public string CreditorStreet { get; set; }
        public string CreditorCS { get; set; }
        public string CreditorZip { get; set; }
        public string Debt_Bal { get; set; }
        public string Debtor_No { get; set; }
        public string Batch_No { get; set; }
        public string SOL_Date { get; set; }
        public string TU_Score { get; set; }
        public Nullable<System.DateTime> PimsLoadDate { get; set; }
        public Nullable<bool> Closed { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string LastTranCode { get; set; }
        public string LastTranSource { get; set; }
        public string LastReturnCode { get; set; }
        public Nullable<int> AnnualRateCodeID { get; set; }
        public Nullable<System.DateTime> LastMediaAttachDate { get; set; }
        public Nullable<int> StatuteTypeID { get; set; }
        public Nullable<bool> RestrictedAccount { get; set; }
        public Nullable<int> Previous_WorkStatusID { get; set; }
        public Nullable<System.DateTime> WorkStatusID_ChangeDate { get; set; }
        public string WorkStatusID_UpdateID { get; set; }
        public Nullable<int> CitationCount { get; set; }
        public bool DONOTACH { get; set; }
        public Nullable<decimal> Liquidation { get; set; }
        public string AgencyAccount { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SalesPrice { get; set; }
    }
    
}
