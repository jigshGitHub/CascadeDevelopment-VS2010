﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cascade.Data.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CascadeDBEntities : DbContext
    {
        public CascadeDBEntities()
            : base("name=CascadeDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MSI_ComplaintIssues> MSI_ComplaintIssues { get; set; }
        public DbSet<MSI_ComplaintReceviedBy> MSI_ComplaintReceviedBy { get; set; }
        public DbSet<MSI_DebtorContactMethods> MSI_DebtorContactMethods { get; set; }
        public DbSet<MSI_DebtorContactTime> MSI_DebtorContactTime { get; set; }
        public DbSet<MSI_DebtorProducts> MSI_DebtorProducts { get; set; }
        public DbSet<MSI_FinalActionsteps> MSI_FinalActionsteps { get; set; }
        public DbSet<MSI_MediaTypes> MSI_MediaTypes { get; set; }
        public DbSet<MSI_OwnerResponses> MSI_OwnerResponses { get; set; }
        public DbSet<MSI_RecallForm> MSI_RecallForm { get; set; }
        public DbSet<MSI_ResaleRestriction> MSI_ResaleRestriction { get; set; }
        public DbSet<MSI_USStates> MSI_USStates { get; set; }
        public DbSet<Port_Acq> Port_Acq { get; set; }
        public DbSet<Port_DPS> Port_DPS { get; set; }
        public DbSet<Port_Media> Port_Media { get; set; }
        public DbSet<Port_Recall> Port_Recall { get; set; }
        public DbSet<Port_Trans> Port_Trans { get; set; }
        public DbSet<RACCBKRP> RACCBKRPs { get; set; }
        public DbSet<RACCOUNT> RACCOUNTs { get; set; }
        public DbSet<RACCTLGL> RACCTLGLs { get; set; }
        public DbSet<RACCTREL> RACCTRELs { get; set; }
        public DbSet<RAGENCY> RAGENCies { get; set; }
        public DbSet<RINTRATE> RINTRATEs { get; set; }
        public DbSet<RPRODCDE> RPRODCDEs { get; set; }
        public DbSet<RSTATU> RSTATUS { get; set; }
        public DbSet<RTRANCDE> RTRANCDEs { get; set; }
        public DbSet<Sup_BrockettCompanies> Sup_BrockettCompanies { get; set; }
        public DbSet<Sup_Company> Sup_Company { get; set; }
        public DbSet<Sup_PmtType> Sup_PmtType { get; set; }
        public DbSet<Sup_Reason> Sup_Reason { get; set; }
        public DbSet<Sup_Status> Sup_Status { get; set; }
        public DbSet<Sup_TransCode> Sup_TransCode { get; set; }
        public DbSet<Sup_TransSource> Sup_TransSource { get; set; }
        public DbSet<TBL_CHART> TBL_CHART { get; set; }
        public DbSet<TBL_COMPANIES> TBL_COMPANIES { get; set; }
        public DbSet<Tbl_Money> Tbl_Money { get; set; }
        public DbSet<Tbl_People> Tbl_People { get; set; }
        public DbSet<TBL_Portfolio> TBL_Portfolio { get; set; }
        public DbSet<Tbl_Source> Tbl_Source { get; set; }
        public DbSet<MSI_vwDebtors> MSI_vwDebtors { get; set; }
        public DbSet<MSI_vwMediaData> MSI_vwMediaData { get; set; }
        public DbSet<MSI_vwPurchases> MSI_vwPurchases { get; set; }
        public DbSet<MSI_vwRecallData> MSI_vwRecallData { get; set; }
        public DbSet<MSI_vwSales> MSI_vwSales { get; set; }
        public DbSet<MSI_vwSearch> MSI_vwSearch { get; set; }
        public DbSet<vw_AddDPSCheck> vw_AddDPSCheck { get; set; }
        public DbSet<vw_CollectionsRecon> vw_CollectionsRecon { get; set; }
        public DbSet<vw_PortfolioCashFlow> vw_PortfolioCashFlow { get; set; }
        public DbSet<vw_PortfolioCashPosition> vw_PortfolioCashPosition { get; set; }
        public DbSet<vw_PortfolioSummary> vw_PortfolioSummary { get; set; }
        public DbSet<vw_PortfolioTransactions> vw_PortfolioTransactions { get; set; }
        public DbSet<vwAccount> vwAccounts { get; set; }
        public DbSet<MSI_MediaForm> MSI_MediaForm { get; set; }
        public DbSet<MSI_MediaRequestResponse> MSI_MediaRequestResponse { get; set; }
        public DbSet<MSI_MediaRequestStatus> MSI_MediaRequestStatus { get; set; }
        public DbSet<MSI_MediaRequestTypes> MSI_MediaRequestTypes { get; set; }
        public DbSet<MSI_MediaTracker> MSI_MediaTracker { get; set; }
        public DbSet<MSI_ComplaintMain> MSI_ComplaintMain { get; set; }
        public DbSet<MSI_DPSForm> MSI_DPSForm { get; set; }
        public DbSet<MSI_Port_Acq_Edited> MSI_Port_Acq_Edited { get; set; }
        public DbSet<MSI_Port_Acq_Original> MSI_Port_Acq_Original { get; set; }
        public DbSet<MSI_Port_SalesTrans_Edited> MSI_Port_SalesTrans_Edited { get; set; }
        public DbSet<MSI_Port_SalesTrans_Original> MSI_Port_SalesTrans_Original { get; set; }
    }
}
