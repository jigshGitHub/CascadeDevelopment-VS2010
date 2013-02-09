using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using JQueryDataTables.Models;
using JQueryDataTables.Models.Repository;

namespace Cascade.Web.Areas.CRM.Controllers
{
    [Authorize]
    public class CompanyController : BaseController
    {
        //
        // GET: /CRM/Company/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllCompanies()
        {
            //Get the View Repository
            CompanyRepository companyRepo = new CompanyRepository();
            //Get the Report Data
            var _companyData = from company in companyRepo.GetAll().Distinct()
                               select company;
            //return the data
            return PartialView("_companyRecords", _companyData.ToList());

        }


        /// <summary>
        /// Returns data by the criterion
        /// </summary>
        /// <param name="param">Request sent by DataTables plugin</param>
        /// <returns>JSON text used to display data
        /// <list type="">
        /// <item>sEcho - same value as in the input parameter</item>
        /// <item>iTotalRecords - Total number of unfiltered data. This value is used in the message: 
        /// "Showing *start* to *end* of *iTotalDisplayRecords* entries (filtered from *iTotalDisplayRecords* total entries)
        /// </item>
        /// <item>iTotalDisplayRecords - Total number of filtered data. This value is used in the message: 
        /// "Showing *start* to *end* of *iTotalDisplayRecords* entries (filtered from *iTotalDisplayRecords* total entries)
        /// </item>
        /// <item>aoData - Twodimensional array of values that will be displayed in table. 
        /// Number of columns must match the number of columns in table and number of rows is equal to the number of records that should be displayed in the table</item>
        /// </list>
        /// </returns>
        //public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        //{
        //    var allCompanies = DataRepository.GetCompanies();
        //    IEnumerable<TBL_COMPANIES> filteredCompanies;
        //    //Check whether the companies should be filtered by keyword
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        //Used if particulare columns are filtered 
        //        var nameFilter = Convert.ToString(Request["sSearch_1"]);
        //        var cityFilter = Convert.ToString(Request["sSearch_2"]);
        //        var countryFilter = Convert.ToString(Request["sSearch_3"]);

        //        //Optionally check whether the columns are searchable at all 
        //        var isNameSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
        //        //var isCitySearchable = Convert.ToBoolean(Request["bSearchable_2"]);
        //        //var isCountrySearchable = Convert.ToBoolean(Request["bSearchable_3"]);

        //        filteredCompanies = DataRepository.GetCompanies()
        //           .Where(c => isNameSearchable && c.COMPANYNAME.ToLower().Contains(param.sSearch.ToLower())
        //                       //||
        //                       //isCitySearchable && c.CITY.ToLower().Contains(param.sSearch.ToLower())
        //                       //||
        //                       //isCountrySearchable && c.Country.ToLower().Contains(param.sSearch.ToLower())
        //                       );
        //    }
        //    else
        //    {
        //        filteredCompanies = allCompanies;
        //    }

        //    var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
        //    var isPOCLastNameSortable = Convert.ToBoolean(Request["bSortable_2"]);
        //    var isPOCFirstNameSortable = Convert.ToBoolean(Request["bSortable_3"]);
        //    var isCitySortable = Convert.ToBoolean(Request["bSortable_4"]);
        //    var isCountrySortable = Convert.ToBoolean(Request["bSortable_5"]);

        //    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
        //    Func<TBL_COMPANIES, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.COMPANYNAME :
        //                                                   sortColumnIndex == 2 && isPOCLastNameSortable ? c.POCLastName :
        //                                                   sortColumnIndex == 3 && isPOCFirstNameSortable ? c.POCFirstName :
        //                                                   sortColumnIndex == 4 && isCitySortable ? c.CITY :
        //                                                   sortColumnIndex == 5 && isCountrySortable ? c.Country :
        //                                                   "");

        //    var sortDirection = Request["sSortDir_0"]; // asc or desc
        //    if (sortDirection == "asc")
        //        filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
        //    else
        //        filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

        //    var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
        //    //var result = from c in displayedCompanies select new[] { Convert.ToString(c.ID), c.Name, c.Address, c.Town };
        //    var result = from c in displayedCompanies select new[] { Convert.ToString(c.COMPANIESID), c.COMPANYNAME, c.POCLastName, c.POCFirstName, c.CITY, c.Country };
        //    return Json(new
        //                    {
        //                        sEcho = param.sEcho,
        //                        iTotalRecords = allCompanies.Count(),
        //                        iTotalDisplayRecords = filteredCompanies.Count(),
        //                        aaData = result
        //                    },
        //                JsonRequestBehavior.AllowGet);
        //}

    }
}
