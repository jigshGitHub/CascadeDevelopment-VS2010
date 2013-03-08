using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cascade.Data.Models;
using Cascade.Data.Repositories;

namespace Cascade.Web.Controllers
{
    public class MSIPortfolioOriginalController : ApiController
    {
        public MSI_Port_Acq_Original Get(string portfolioNumber)
        {
            MSI_Port_Acq_Original portfolio = null;

            try
            {
                DataQueries query = new DataQueries();
                portfolio = query.GetPortfolioPurchaseSummary(portfolioNumber);
            }
            catch (Exception ex)
            {
            }
            return portfolio;

        }
        public MSI_Port_Acq_Original Post(MSI_Port_Acq_Original inPortfolio)
        {
            MSI_Port_Acq_Original portfolioToSave = null;
            MSI_Port_Acq_OriginalRepository repository = null;
            bool editingRequired = true;
            try
            {

                repository = new MSI_Port_Acq_OriginalRepository();
                portfolioToSave = repository.GetById(inPortfolio.Portfolio_);

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
                    repository.Update(portfolioToSave);
                else
                    repository.Add(portfolioToSave);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException validationException)
            {
                foreach (System.Data.Entity.Validation.DbEntityValidationResult errorResult in validationException.EntityValidationErrors)
                {
                    foreach (System.Data.Entity.Validation.DbValidationError error in errorResult.ValidationErrors)
                    {
                        string data = error.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return portfolioToSave;
        }
    }

    public class MSIPortfolioSalesTransactionsOriginalController : ApiController
    {
        public IEnumerable<MSI_Port_SalesTrans_Original> Get(string portfolioNumber, string userId = "")
        {
            IEnumerable<MSI_Port_SalesTrans_Original> transactions = null;

            try
            {
                DataQueries query = new DataQueries();
                transactions = query.GetPortfolioSalesSummary(portfolioNumber, userId);
            }
            catch (Exception ex)
            {
            }
            return transactions;


        }

        [HttpGet]
        public MSI_Port_SalesTrans_Original Details(int id)
        {
            MSI_Port_SalesTrans_Original transaction = null;
            MSI_Port_SalesTrans_OriginalRepository repository = null;
            try
            {
                repository = new MSI_Port_SalesTrans_OriginalRepository();
                transaction = repository.GetById(id);
            }
            catch (Exception ex)
            {
            }
            return transaction;


        }

        public MSI_Port_SalesTrans_Original Post(MSI_Port_SalesTrans_Original inTransaction)
        {
            MSI_Port_SalesTrans_Original transactionToSave = null;
            MSI_Port_SalesTrans_OriginalRepository repository = null;

            try
            {
                repository = new MSI_Port_SalesTrans_OriginalRepository();
                transactionToSave = repository.GetById(inTransaction.ID);

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

                repository.Update(transactionToSave);

            }
            catch (Exception ex)
            {
            }
            return inTransaction;


        }
    }

}