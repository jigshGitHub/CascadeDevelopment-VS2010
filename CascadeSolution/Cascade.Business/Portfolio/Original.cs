using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cascade.Data.Models;
using Cascade.Helpers;
using System.Data.Entity;
using Cascade.Data.Repositories;
namespace Cascade.Business.Portfolio
{
    public class Original
    {
        private static readonly string thisClass = "Cascade.Business.Portfolio.Original";
        private DataQueries query;

        public Original()
        {
            query = new DataQueries();
        }

        public DataQueries Query
        {
            get
            {
                return query;
            }
        }

        public MSI_Port_Acq_Original Get(string portfolioNumber)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}", thisMethod, portfolioNumber);
            LogHelper.Info(logMessage); 
            
            MSI_Port_Acq_Original portfolio = null;

            try
            {
                portfolio = Query.GetPortfolioPurchaseSummary(portfolioNumber);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return portfolio;

        }

        public MSI_Port_Acq_Original Save(MSI_Port_Acq_Original inPortfolio)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}", thisMethod, inPortfolio.Portfolio_);
            LogHelper.Info(logMessage); 
            
            MSI_Port_Acq_Original portfolioToSave = null;
            bool editingRequired = true;
            try
            {                
                portfolioToSave = Query.GetPortfolioOriginal(inPortfolio.Portfolio_);
                if (portfolioToSave == null)
                {
                    editingRequired = false;
                    portfolioToSave = new MSI_Port_Acq_Original();
                }
                portfolioToSave.Portfolio_ = inPortfolio.Portfolio_;
                portfolioToSave.Company = inPortfolio.Company;
                portfolioToSave.Cut_OffDate = inPortfolio.Cut_OffDate;
                portfolioToSave.ClosingDate = inPortfolio.ClosingDate;
                portfolioToSave.Lender_FileDescription = inPortfolio.Lender_FileDescription;
                portfolioToSave.Seller = inPortfolio.Seller;
                portfolioToSave.CostBasis = inPortfolio.CostBasis;
                portfolioToSave.Face = inPortfolio.Face;
                portfolioToSave.C_ofAccts = inPortfolio.C_ofAccts;
                portfolioToSave.PutbackDeadline = inPortfolio.PutbackDeadline;
                portfolioToSave.PutbackTerm__days_ = inPortfolio.PutbackTerm__days_;
                portfolioToSave.PurchasePrice = inPortfolio.PurchasePrice;
                portfolioToSave.ResaleRestrictionId = inPortfolio.ResaleRestrictionId;
                portfolioToSave.Notes = inPortfolio.Notes;
                portfolioToSave.CreatedBy = inPortfolio.CreatedBy;
                portfolioToSave.UpdatedBy = inPortfolio.UpdatedBy;
                portfolioToSave.CreatedDate = DateTime.Now;
                portfolioToSave.UpdatedDate = DateTime.Now;
                if (editingRequired)
                    Query.UpdatePortfolio(portfolioToSave);
                else
                    Query.AddPortfolio(portfolioToSave);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return portfolioToSave;
        }

        public IEnumerable<MSI_Port_SalesTrans_Original> GetSalesTransactions(string portfolioNumber, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}, userId={2}", thisMethod, portfolioNumber, userId);
            LogHelper.Info(logMessage); 
            
            IEnumerable<MSI_Port_SalesTrans_Original> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioSalesSummary(portfolioNumber, userId);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactions;


        }

        public IEnumerable<MSI_Port_CollectionsTrans> GetCollectionsTransactions(string portfolioNumber, bool isOriginal, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}, userId={2}", thisMethod, portfolioNumber, userId);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_Port_CollectionsTrans> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioCollectionsSummary(portfolioNumber, userId, isOriginal);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactions;
        }

        public IEnumerable<MSI_Port_InvestmentsTrans> GetInvestmentsTransactions(string portfolioNumber, bool isOriginal, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}, userId={2}", thisMethod, portfolioNumber, userId);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_Port_InvestmentsTrans> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioInvestmentsSummary(portfolioNumber, userId, isOriginal);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactions;
        }

        public IEnumerable<MSI_Port_DistributionTrans> GetIDistributionTransactions(string portfolioNumber, bool isOriginal, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}, userId={2}", thisMethod, portfolioNumber, userId);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_Port_DistributionTrans> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioDistributionSummary(portfolioNumber, userId, isOriginal);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactions;
        }

        public IEnumerable<MSI_Port_InterestTrans> GetInterestTransactions(string portfolioNumber, bool isOriginal, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}, userId={2}", thisMethod, portfolioNumber, userId);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_Port_InterestTrans> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioInterestSummary(portfolioNumber, userId, isOriginal);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactions;
        }



        public MSI_Port_SalesTrans_Original GetSalesTransaction(int id)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, id);
            LogHelper.Info(logMessage); 
            
            MSI_Port_SalesTrans_Original transaction = null;            
            try
            {
                transaction = Query.GetPortfolioSalesTransactionOriginal(id);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transaction;


        }

        public MSI_Port_SalesTrans_Original SaveSalesTransaction(MSI_Port_SalesTrans_Original inTransaction)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, inTransaction.ID);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_Original transactionToSave = null;
            bool editingRequired = true;

            try
            {
                
                if (inTransaction.ID == 0)
                {
                    editingRequired = false;
                    transactionToSave = new MSI_Port_SalesTrans_Original();
                }
                else{
                    transactionToSave = Query.GetPortfolioSalesTransactionOriginal(inTransaction.ID);
                }
                transactionToSave.PutbackDeadline = inTransaction.PutbackDeadline;
                transactionToSave.PutbackTerm_days_ = inTransaction.PutbackTerm_days_;
                transactionToSave.C_ofAccts = inTransaction.C_ofAccts;
                transactionToSave.FaceValue = inTransaction.FaceValue;
                transactionToSave.SalesBasis = inTransaction.SalesBasis;
                transactionToSave.SalesPrice = inTransaction.SalesPrice;
                transactionToSave.Buyer = inTransaction.Buyer;
                transactionToSave.Lender = inTransaction.Lender;
                transactionToSave.ClosingDate = inTransaction.ClosingDate;
                transactionToSave.Cut_OffDate = inTransaction.Cut_OffDate;
                transactionToSave.Notes = inTransaction.Notes;
                transactionToSave.Portfolio_ = inTransaction.Portfolio_;
                transactionToSave.C_ofAccts = inTransaction.C_ofAccts;
                transactionToSave.CreatedBy = inTransaction.CreatedBy;
                transactionToSave.CreatedDate = DateTime.Now;
                transactionToSave.UpdatedBy = inTransaction.UpdatedBy;
                transactionToSave.UpdatedDate = DateTime.Now;


                if (!editingRequired)
                    Query.AddSalesTransaction(transactionToSave);
                else
                    Query.UpdateSalesTransaction(transactionToSave);

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transactionToSave;
        }

    }
}
