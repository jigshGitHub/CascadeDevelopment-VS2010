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
    public class MSIPortfolioEditedController : ApiController
    {
        public MSI_Port_Acq_Edited Get(string portfolioNumber)
        {
            MSI_Port_Acq_Edited portfolio = null;
            MSI_Port_Acq_EditedRepository repository = null;
            try
            {
                repository = new MSI_Port_Acq_EditedRepository();
                portfolio = repository.GetById(portfolioNumber);
                if (portfolio == null)
                {
                    MSI_Port_Acq_OriginalRepository originalRepository = new MSI_Port_Acq_OriginalRepository();
                    MSI_Port_Acq_Original originalPortfolio = originalRepository.GetById(portfolioNumber);
                    portfolio = new MSI_Port_Acq_Edited();
                    portfolio.C_ofAccts = originalPortfolio.C_ofAccts;
                    portfolio.ClosingDate = originalPortfolio.ClosingDate;
                    portfolio.Company = originalPortfolio.Company;
                    portfolio.CostBasis = originalPortfolio.CostBasis;
                    portfolio.Cut_OffDate = originalPortfolio.Cut_OffDate;
                    portfolio.Face = originalPortfolio.Face;
                    portfolio.Lender_FileDescription = originalPortfolio.Lender_FileDescription;
                    portfolio.Notes = originalPortfolio.Notes;
                    portfolio.Portfolio_ = originalPortfolio.Portfolio_;
                    portfolio.PurchasePrice = originalPortfolio.PurchasePrice;
                    portfolio.PutbackDeadline = originalPortfolio.PutbackDeadline;
                    portfolio.PutbackTerm__days_ = originalPortfolio.PutbackTerm__days_;
                    portfolio.ResaleRestrictionId = originalPortfolio.ResaleRestrictionId;
                    portfolio.Seller = originalPortfolio.Seller;
                    portfolio.UpdatedBy = originalPortfolio.UpdatedBy;
                    portfolio.UpdatedDate = originalPortfolio.UpdatedDate;
                    portfolio.CreatedBy = originalPortfolio.CreatedBy;
                    portfolio.CreatedDate = originalPortfolio.CreatedDate;
                }
            }
            catch (Exception ex)
            {
            }
            return portfolio;

        }
        public MSI_Port_Acq_Edited Post(MSI_Port_Acq_Edited inPortfolio)
        {
            MSI_Port_Acq_Edited portfolioToSave = null;
            MSI_Port_Acq_EditedRepository repository = null;
            bool editingRequired = true;
            try
            {

                repository = new MSI_Port_Acq_EditedRepository();
                portfolioToSave = repository.GetById(inPortfolio.Portfolio_);

                if (portfolioToSave == null)
                {
                    editingRequired = false;
                    portfolioToSave = new MSI_Port_Acq_Edited();
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
    public class MSIPortfolioSalesTransactionsEditedController : ApiController
    {
        public MSI_Port_SalesTrans_Edited Get(int id)
        {
            MSI_Port_SalesTrans_Edited transaction = null;
            MSI_Port_SalesTrans_EditedRepository repository = null;
            try
            {
                repository = new MSI_Port_SalesTrans_EditedRepository();
                transaction = repository.GetById(id);
                if (transaction == null)
                {
                    MSI_Port_SalesTrans_OriginalRepository originalRepository = new MSI_Port_SalesTrans_OriginalRepository();
                    MSI_Port_SalesTrans_Original originalTransaction = originalRepository.GetById(id);
                    transaction = new MSI_Port_SalesTrans_Edited();
                    transaction.C_ofAccts = originalTransaction.C_ofAccts;
                    transaction.ClosingDate = originalTransaction.ClosingDate;
                    transaction.Cut_OffDate = originalTransaction.Cut_OffDate;
                    transaction.FaceValue= originalTransaction.FaceValue;
                    transaction.Lender = originalTransaction.Lender;
                    transaction.Notes = originalTransaction.Notes;
                    transaction.Portfolio_ = originalTransaction.Portfolio_;
                    transaction.SalesPrice = originalTransaction.SalesPrice;
                    transaction.PutbackDeadline = originalTransaction.PutbackDeadline;
                    transaction.PutbackTerm_days_ = originalTransaction.PutbackTerm_days_;
                    transaction.ID = originalTransaction.ID;
                    transaction.Portfolio_ = originalTransaction.Portfolio_;
                    transaction.Buyer = originalTransaction.Buyer;
                    transaction.SalesBasis = originalTransaction.SalesBasis;
                    transaction.UpdatedBy = originalTransaction.UpdatedBy;
                    transaction.UpdatedDate = originalTransaction.UpdatedDate;
                    transaction.CreatedBy = originalTransaction.CreatedBy;
                    transaction.CreatedDate = originalTransaction.CreatedDate;
                }
            }
            catch (Exception ex)
            {
            }
            return transaction;

        }
        public MSI_Port_SalesTrans_Edited Post(MSI_Port_SalesTrans_Edited inTransaction)
        {
            MSI_Port_SalesTrans_Edited transactionToSave = null;
            MSI_Port_SalesTrans_EditedRepository repository = null;
            bool editingRequired = true;
            try
            {

                repository = new MSI_Port_SalesTrans_EditedRepository();
                transactionToSave = repository.GetById(inTransaction.ID);

                if (transactionToSave == null)
                {
                    editingRequired = false;
                    transactionToSave = new MSI_Port_SalesTrans_Edited();
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
                transactionToSave.ID = inTransaction.ID;
                transactionToSave.CreatedBy = inTransaction.CreatedBy;
                transactionToSave.UpdatedBy = inTransaction.UpdatedBy;
                transactionToSave.CreatedDate = DateTime.Now;
                transactionToSave.UpdatedDate = DateTime.Now;
                if (editingRequired)
                    repository.Update(transactionToSave);
                else
                    repository.Add(transactionToSave);
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
            return transactionToSave;
        }
    }
}