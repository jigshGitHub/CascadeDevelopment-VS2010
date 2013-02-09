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
    public class DataRepositoryColl
    {
        /// <summary>
        /// Method that returns all Portfolios used in this example
        /// </summary>
        /// <returns>List of Portfolios</returns>
        public static IList<TBL_Portfolio> GetPortfolios()
        {
            PortfolioRepository portRepo = null;
            portRepo = new PortfolioRepository();
            var portfolios = portRepo.GetAll().ToList();
            return portfolios;
        }
        ////For States
        //public static IList<TBL_State> GetStates()
        //{
        //    StateRepository stateRepo = null;
        //    stateRepo = new StateRepository();
        //    var states = stateRepo.GetAll().OrderBy(x => x.Name).ToList();
        //    return states;
        //}
        ////For TransCodes
        //public static IList<Sup_TransCode> GetTransCodes()
        //{
        //    SupTransCodeRepository transCodeRepo = null;
        //    transCodeRepo = new SupTransCodeRepository();
        //    var transcodes = transCodeRepo.GetAll().OrderBy(x => x.TransCode).ToList();
        //    return transcodes;
        //}
        ////For TransSources
        //public static IList<Sup_TransSource> GetTransSources()
        //{
        //    SupTransSourceRepository transSourceRepo = null;
        //    transSourceRepo = new SupTransSourceRepository();
        //    var transsources = transSourceRepo.GetAll().OrderBy(x => x.TransSource).ToList();
        //    return transsources;
        //}
        ////For PmtTypes
        //public static IList<Sup_PmtType> GetPmtTypes()
        //{
        //    SupPmtTypeRepository pmtTypeRepo = null;
        //    pmtTypeRepo = new SupPmtTypeRepository();
        //    var pmtTypes = pmtTypeRepo.GetAll().OrderBy(x => x.Payment_Type_ID_code).ToList();
        //    return pmtTypes;
        //}
        ////For Portfolios
        //public static IList<Port_Acq> GetAllPortfolios()
        //{
        //    PortAcqRepository portAcqRepo = null;
        //    portAcqRepo = new PortAcqRepository();
        //    var portAcqs = portAcqRepo.GetAll().OrderBy(x => x.Portfolio_).ToList();
        //    return portAcqs;
        //}
        ////For Responsibilities
        //public static IList<Sup_Company> GetResponsibilities()
        //{
        //    SupCompanyRepository supCompanyRepo = null;
        //    supCompanyRepo = new SupCompanyRepository();
        //    var responsibilites = supCompanyRepo.GetAll().OrderBy(x => x.Agency).ToList();
        //    return responsibilites;
        //}
        ////For DPS Records
        //public static IList<Port_DPS> GetAllDPS()
        //{
        //    PortDPSRepository portDPSRepo = null;
        //    portDPSRepo = new PortDPSRepository();
        //    var portDPSs = portDPSRepo.GetAll().OrderBy(x => x.Portfolio_).ToList();
        //    return portDPSs;
        //}

        //For Expense Records
        //public static IList<Tbl_Expense> GetAllExpenses()
        //{
        //    ExpenseRepository expenseRepo = null;
        //    expenseRepo = new ExpenseRepository();
        //    var expenses = expenseRepo.GetAll().OrderBy(x => x.ExpenseDate).ToList();
        //    return expenses;
        //}
        ////For Categories 
        //public static IList<Tbl_Categories> GetAllCategories()
        //{
        //    CategoriesRepository categRepo = null;
        //    categRepo = new CategoriesRepository();
        //    var categories = categRepo.GetAll().OrderBy(x => x.Description).ToList();
        //    return categories;
        //}
    }
}