using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JQueryDataTables.Models;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;

namespace JQueryDataTables.Models.Repository
{
    /// <summary>
    /// Repository class - contains hardcoded data
    /// </summary>
    public class DataRepository
    {
        /// <summary>
        /// Method that returns all companies used in this example
        /// </summary>
        /// <returns>List of companies</returns>
        public static IList<TBL_COMPANIES> GetCompanies()
        {
            CompanyRepository compRepo = null;
            compRepo = new CompanyRepository();
            var companies = compRepo.GetAll().ToList();
            return companies;
        }
    }
}