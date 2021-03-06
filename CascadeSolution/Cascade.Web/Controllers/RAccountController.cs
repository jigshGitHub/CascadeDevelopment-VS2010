﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
namespace Cascade.Web.Controllers
{
    public class RAccountController : ApiController
    {
        [HttpGet]
        public IEnumerable<vwAccount> Get(string nameSearch)
        {
            DataQueries query = new DataQueries();
            IEnumerable<vwAccount> accounts = null;
            try
            {
                accounts = query.GetAccounts(nameSearch);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in GetAccount : {0}", ex.Message))
                });                
                
            }
            return accounts;
        }

        [HttpGet]
        public vwAccount Details(string accountNumber, string searchType)
        {
            DataQueries query = new DataQueries();
            vwAccount account = null;
            try
            {
                account = query.GetAccount(accountNumber, searchType);
                account.GUID = System.Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error occur in GetAccount : {0}", ex.Message))
                });

            }
            if (account == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No account information found for account number = {0}", accountNumber)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return account;
        }
    }
}
