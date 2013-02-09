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
    public class PortfolioController : ApiController
    {
        public Port_Acq Get(string portfolioNumber)
        {
            Port_Acq portfolio = null;
            PortAcqRepository repository = null;

            try
            {
                repository = new PortAcqRepository();
                portfolio = repository.GetById(portfolioNumber);                
            }
            catch (Exception ex)
            {
            }
            return portfolio;

        }
        public Port_Acq Post(Port_Acq inPortfolio)
        {
            Port_Acq portfolioToSave = null;
            PortAcqRepository repository = null;

            try
            {
                repository = new PortAcqRepository();

                portfolioToSave = repository.GetById(inPortfolio.Portfolio_);
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

                repository.Update(portfolioToSave);
            }
            catch (Exception ex)
            {
            }
            return portfolioToSave;
        }
    }

    public class PortfolioTransactionsController : ApiController
    {
        public IEnumerable<Port_Trans> Get(string portfolioNumber, string transType)
        {
            IEnumerable<Port_Trans> transactions = null;
            PortTransRepository repository = null;

            try
            {
                repository = new PortTransRepository();
                transactions = repository.GetAll().Where(record => record.TransType == transType && record.Portfolio_ == portfolioNumber).ToList<Port_Trans>();
            }
            catch (Exception ex)
            {
            }
            return transactions;


        }

        public Port_Trans Post(Port_Trans inTransaction)
        {
            Port_Trans transactionToSave = null;
            PortTransRepository repository = null;

            try
            {
                repository = new PortTransRepository();
                transactionToSave = repository.GetById(inTransaction.ID);

                if (inTransaction.TransType == "Sale")
                {
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
                }
                else if (inTransaction.TransType == "Collection")
                {
                    transactionToSave.FaceValue = inTransaction.FaceValue;
                    transactionToSave.SalesPrice = inTransaction.SalesPrice;
                    transactionToSave.ClosingDate = inTransaction.ClosingDate;
                    transactionToSave.Inv_AgencyName = inTransaction.Inv_AgencyName;
                }
                else if (inTransaction.TransType == "Investment")
                {
                    transactionToSave.ProfitShare_after = inTransaction.ProfitShare_after;
                    transactionToSave.ProfitShare_before = inTransaction.ProfitShare_before;
                    transactionToSave.SalesPrice = inTransaction.SalesPrice;
                    transactionToSave.InterestRate = inTransaction.InterestRate;
                    transactionToSave.Inv_AgencyName = inTransaction.Inv_AgencyName;
                    transactionToSave.Notes = inTransaction.Notes;
                }
                else if (inTransaction.TransType == "Distribution")
                {
                    transactionToSave.Check_ = inTransaction.Check_;
                    transactionToSave.ClosingDate = inTransaction.ClosingDate;
                    transactionToSave.SalesPrice = inTransaction.SalesPrice;
                    transactionToSave.Inv_AgencyName = inTransaction.Inv_AgencyName;
                    transactionToSave.Notes = inTransaction.Notes;
                }
                else if (inTransaction.TransType == "Interest")
                {
                    transactionToSave.Check_ = inTransaction.Check_;
                    transactionToSave.ClosingDate = inTransaction.ClosingDate;
                    transactionToSave.SalesPrice = inTransaction.SalesPrice;
                    transactionToSave.Inv_AgencyName = inTransaction.Inv_AgencyName;
                }

                repository.Update(transactionToSave);
            }
            catch (Exception ex)
            {
            }
            return inTransaction;


        }
    }
}