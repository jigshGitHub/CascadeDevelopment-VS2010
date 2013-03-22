using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cascade.Data.Models;
using Cascade.Business.Portfolio;
using Cascade.Helpers;

namespace Cascade.Web.Controllers
{
    public class MSIPortfolioOriginalController : ApiController
    {
        private static readonly string thisClass = "Cascade.Web.Controllers.MSIPortfolioOriginalController";
        Original business;

        public MSIPortfolioOriginalController()
        {
            business = new Original();
        }

        public MSI_Port_Acq_Original Get(string portfolioNumber)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}", thisMethod, portfolioNumber);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_Original portfolio = null;

            try
            {
                portfolio = business.Get(portfolioNumber);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Gets MediaRequest : {0}", ex.Message))
                });
            }
            return portfolio;

        }

        public MSI_Port_Acq_Original Post(MSI_Port_Acq_Original inPortfolio)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}", thisMethod, inPortfolio.Portfolio_);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_Original portfolioToSave = null;
            try
            {
                portfolioToSave = business.Save(inPortfolio);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in POST MediaRequest : {0}", ex.Message))
                });
            }
            return portfolioToSave;
        }
    }

    public class MSIPortfolioSalesTransactionsOriginalController : ApiController
    {
        private static readonly string thisClass = "Cascade.Web.Controllers.MSIPortfolioSalesTransactionsOriginalController";
        Original business;

        public MSIPortfolioSalesTransactionsOriginalController()
        {
            business = new Original();
        }

        public IEnumerable<MSI_Port_SalesTrans_Original> Get(string portfolioNumber, string userId = "")
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}", thisMethod, portfolioNumber);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_Port_SalesTrans_Original> transactions = null;

            try
            {
                transactions = business.GetSalesTransactions(portfolioNumber, userId);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Getting Sales transactions : {0}", ex.Message))
                });
            }
            return transactions;


        }

        [HttpGet]
        public MSI_Port_SalesTrans_Original Details(int id)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, id);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_Original transaction = null;

            try
            {
                transaction = business.GetSalesTransaction(id);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in Getting Sales transaction : {0}", ex.Message))
                });
            }
            return transaction;


        }

        public MSI_Port_SalesTrans_Original Post(MSI_Port_SalesTrans_Original inTransaction)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, inTransaction.ID);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_Original transactionToSave = null;


            try
            {
                transactionToSave = business.SaveSalesTransaction(inTransaction);

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in saving Sales transaction : {0}", ex.Message))
                });
            }
            return inTransaction;


        }
    }

}