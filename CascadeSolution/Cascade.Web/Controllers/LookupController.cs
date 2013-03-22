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
    public class LookupController : ApiController
    {
        public IEnumerable<LookUp> Get(string id)
        {
            IEnumerable<LookUp> lookupData = null;
            List<LookUp> data = new List<LookUp>();
            //SupCompanyRepository supCompanyRepo = null;
            switch (id)
            {
                case "TransCode":
                    SupTransCodeRepository transCodeRepo = new SupTransCodeRepository();
                    lookupData = from transCode in transCodeRepo.GetAll().OrderBy(x => x.TransCode)
                                 select new LookUp(transCode.TransCode + GetDescriptionDetails(transCode.Description), transCode.TransCode);
                    break;
                case "TransSource":
                    SupTransSourceRepository transSourceRepo = new SupTransSourceRepository();
                    lookupData = from transSource in transSourceRepo.GetAll().OrderBy(x => x.TransSource)
                                 select new LookUp(transSource.TransSource.ToString() + GetDescriptionDetails(transSource.Description), transSource.TransSource.ToString());
                    break;
                case "PmtType":
                    SupPmtTypeRepository pmtTypeRepo = new SupPmtTypeRepository();
                    lookupData = from pmtType in pmtTypeRepo.GetAll().OrderBy(x => x.Payment_Type_ID_code)
                                 select new LookUp(pmtType.Payment_Type_ID_code.ToString() + GetDescriptionDetails(pmtType.Payment_Type_ID), pmtType.Payment_Type_ID_code.ToString());
                    break;
                case "Portfolio":
                    DataQueries query = new DataQueries();
                    lookupData = query.GetDistinctProductCodes();

                    break;
                case "PortfolioOriginal":
                    MSI_Port_Acq_OriginalRepository repository = new MSI_Port_Acq_OriginalRepository();
                    lookupData = from port_acq in repository.GetAll()
                                 select new LookUp(port_acq.Portfolio_, port_acq.Portfolio_);

                    break;
                case "ResaleRestriction":
                    //data = new List<LookUp>();
                    //data.Add(new LookUp("No Approval Required", "1"));
                    //data.Add(new LookUp("Approval Required", "2"));
                    //data.Add(new LookUp("Notice Required", "3"));
                    //lookupData = data.AsEnumerable<LookUp>();
                    ReSaleRestrictionRepository resaleRepo = new ReSaleRestrictionRepository();
                    lookupData = from reSale in resaleRepo.GetAll()
                                 select new LookUp(reSale.Restriction, reSale.Value.ToString());
                    break;
                case "PutbackTerm":
                    data = new List<LookUp>();
                    data.Add(new LookUp("90", "90"));
                    data.Add(new LookUp("120", "120"));
                    data.Add(new LookUp("180", "180"));
                    lookupData = data.AsEnumerable<LookUp>();
                    break;
                case "Responsibility":
                    SupCompanyRepository respoRepo = new SupCompanyRepository();
                    lookupData = from respo in respoRepo.GetAll().OrderBy(x => x.Name)
                                 select new LookUp(respo.Agency + GetDescriptionDetails(respo.Name), respo.Agency);
                    break;
                case "Buyer":
                    SupCompanyRepository supCompanyRepo = new SupCompanyRepository();
                    //lookupData = from buyer in supCompanyRepo.GetAll().OrderBy(x => x.Agency)
                    //             select new LookUp(buyer.Name, buyer.Agency);
                    query = new DataQueries();
                    lookupData = query.GetDistinctResponsibility();
                    break;
                case "Seller":
                    supCompanyRepo = new SupCompanyRepository();
                    lookupData = from buyer in supCompanyRepo.GetAll().OrderBy(x => x.Name)
                                 select new LookUp(buyer.Name, buyer.Agency);
                    break;
                case "SupBrockettCompanies":
                    BockettCompanyRepository companyRepository = new BockettCompanyRepository();
                    lookupData = from company in companyRepository.GetAll().OrderBy(x => x.Id)
                                 select new LookUp(company.Company, company.Id.ToString());
                    break;
                case "Investor":
                    supCompanyRepo = new SupCompanyRepository();
                    lookupData = from investor in supCompanyRepo.GetAll().Where(record => record.Type == id).OrderBy(x => x.Agency)
                                 select new LookUp(investor.Name, investor.Agency);
                    break;
                case "RProdCode":
                    RProductCodeRepository rprodCodeRepo = new RProductCodeRepository();
                    lookupData = from prodCode in rprodCodeRepo.GetAll().OrderBy(x => x.PRODUCT_CODE)
                                 select new LookUp(prodCode.PRODUCT_CODE + GetDescriptionDetails(prodCode.PortfolioOwner), prodCode.ProductID.ToString());
                    break;
                case "RTranCode":
                    RTranCodeRepository rtranCodeRepo = new RTranCodeRepository();
                    lookupData = from tranCode in rtranCodeRepo.GetAll().OrderBy(x => x.TRAN_CODE)
                                 select new LookUp(tranCode.TRAN_CODE + GetDescriptionDetails(tranCode.DESCR), tranCode.TRAN_CODE);
                    break;
                case "Status":
                    SupStatusRepository supStatusRepo = new SupStatusRepository();
                    lookupData = from status in supStatusRepo.GetAll().OrderBy(x => x.WorkGroup)
                                 select new LookUp(status.Status + GetDescriptionDetails(status.WorkGroup), status.Status);
                    break;
                case "Reason":
                    SupReasonRepository supReasonRepo = new SupReasonRepository();
                    lookupData = from reason in supReasonRepo.GetAll().OrderBy(x => x.Reason)
                                 select new LookUp(reason.Reason, reason.Reason);
                    break;

                case "MoneySource":
                    SourceDataRepository sourceRepo = new SourceDataRepository();
                    lookupData = from source in sourceRepo.GetAll().OrderBy(x => x.SText)
                                 select new LookUp(source.SText, source.SID.ToString());
                    break;

                case "People":
                    PeopleDataRepository peopleRepo = new PeopleDataRepository();
                    lookupData = from people in peopleRepo.GetAll().OrderBy(x => x.FName)
                                 select new LookUp(people.FName + " " + people.LName, people.PID.ToString());
                    break;
                //People

                case "Agencies":
                    RAgencyRepository agencyRepository = new RAgencyRepository();
                    lookupData = from agency in agencyRepository.GetAll()
                                 select new LookUp(agency.AGENCY_ID, agency.AGENCY_ID);
                    break;
                case "ComplaintIssues":
                    MSI_ComplaintIssuesRepository issuesRepository = new MSI_ComplaintIssuesRepository();
                    lookupData = from issue in issuesRepository.GetAll()
                                 select new LookUp(issue.Name, issue.Id.ToString());
                    break;
                case "ComplaintReceivedBy":
                    MSI_ComplaintReceviedByRepository receivedByRepository = new MSI_ComplaintReceviedByRepository();
                    lookupData = from receiver in receivedByRepository.GetAll()
                                 select new LookUp(receiver.Media, receiver.Id.ToString());
                    break;
                case "DebtorContactMethods":
                    MSI_DebtorContactMethodsRepository debtorContactMethodsRepository = new MSI_DebtorContactMethodsRepository();
                    lookupData = from method in debtorContactMethodsRepository.GetAll()
                                 select new LookUp(method.Method, method.Id.ToString());
                    break;
                case "DebtorContactTime":
                    MSI_DebtorContactTimeRepository debtorContactTimeRepository = new MSI_DebtorContactTimeRepository();
                    lookupData = from time in debtorContactTimeRepository.GetAll()
                                 select new LookUp(time.Time, time.Id.ToString());
                    break;
                case "DetorProducts":
                    MSI_DebtorProductsRepository debtorProductsRepository = new MSI_DebtorProductsRepository();
                    lookupData = from product in debtorProductsRepository.GetAll()
                                 select new LookUp(product.Product, product.Id.ToString());
                    break;
                case "OwnerResponse":
                    MSI_OwnerResponsesRepository ownerResponseRepository = new MSI_OwnerResponsesRepository();
                    lookupData = from response in ownerResponseRepository.GetAll()
                                 select new LookUp(response.Response, response.Id.ToString());
                    break;
                case "FinalActionSteps":
                    MSI_FinalActionstepsRepository actionStepsRepository = new MSI_FinalActionstepsRepository();
                    lookupData = from step in actionStepsRepository.GetAll()
                                 select new LookUp(step.Step, step.Id.ToString());
                    break;
                case "MediaTypes":
                    MSI_MediaTypesRepository mediaTypesRepository = new MSI_MediaTypesRepository();
                    lookupData = from type in mediaTypesRepository.GetAll().Where(record => record.IsActive == true)
                                 select new LookUp(type.Name, type.ID.ToString());
                    break;
                case "USStates":
                    MSI_USStatesRepository statesRepository = new MSI_USStatesRepository();
                    lookupData = from state in statesRepository.GetAll()
                                 select new LookUp(state.Name, state.Value);
                    //data.Add(new LookUp("Virginia", "VA"));
                    //data.Add(new LookUp("Texas", "TX"));
                    //data.Add(new LookUp("Ohio", "OH"));
                    //data.Add(new LookUp("Maryland", "MD"));
                    //data.Add(new LookUp("West Virginia", "WV"));
                    //data.Add(new LookUp("New York", "NY"));
                    //data.Add(new LookUp("Nevada", "NV"));
                    //data.Add(new LookUp("California", "CA"));
                    //data.Add(new LookUp("Alabama", "AL"));
                    //data.Add(new LookUp("Alaska", "AK"));
                    //data.Add(new LookUp("Arizona", "AZ"));
                    //data.Add(new LookUp("Arkansas", "AS"));
                    //data.Add(new LookUp("Colorado", "CO"));
                    //data.Add(new LookUp("Connecticut", "CT"));
                    //data.Add(new LookUp("Delaware", "DE"));
                    //data.Add(new LookUp("Florida", "FL"));
                    //data.Add(new LookUp("Georgia", "GA"));
                    //data.Add(new LookUp("Hawaii", "HW"));
                    //data.Add(new LookUp("Idaho", "ID"));
                    //data.Add(new LookUp("Illinois", "IL"));
                    //data.Add(new LookUp("Indiana", "IN"));
                    //data.Add(new LookUp("Iowa", "IA"));
                    //data.Add(new LookUp("Kansas", "KS"));
                    //data.Add(new LookUp("Kentuky", "KY"));
                    //data.Add(new LookUp("Louisiana", "LN"));
                    //data.Add(new LookUp("Maine", "MN"));
                    //data.Add(new LookUp("Massachusetts", "MA"));
                    //data.Add(new LookUp("Michigan", "MI"));
                    //data.Add(new LookUp("Minnesota", "MN"));
                    //data.Add(new LookUp("Mississippi", "MS"));
                    //data.Add(new LookUp("Missouri", "MI"));
                    //data.Add(new LookUp("Montana", "MO"));
                    //data.Add(new LookUp("Nebraska", "NA"));
                    //data.Add(new LookUp("New Hampshire", "NH"));
                    //data.Add(new LookUp("New Jersey", "NJ"));
                    //data.Add(new LookUp("New Mexico", "NM"));
                    //data.Add(new LookUp("North Carolina", "NC"));
                    //data.Add(new LookUp("North Dakota", "ND"));
                    //data.Add(new LookUp("Oklahoma", "OK"));
                    //data.Add(new LookUp("Oregon", "OR"));
                    //data.Add(new LookUp("Pennsylvania", "PA"));
                    //data.Add(new LookUp("Rhode Island", "RI"));
                    //data.Add(new LookUp("South Carolina", "SC"));
                    //data.Add(new LookUp("South Dakota", "SD"));
                    //data.Add(new LookUp("Tennessee", "TN"));
                    //data.Add(new LookUp("Utah", "UT"));
                    //data.Add(new LookUp("Vermont", "VT"));
                    //data.Add(new LookUp("Washington", "WA"));
                    //data.Add(new LookUp("Wisconsin", "WI"));
                    //data.Add(new LookUp("Wyoming", "WY"));
                    //data.Add(new LookUp("District Of Columbia", "DC"));

                    //lookupData = data.AsEnumerable<LookUp>();
                    break;
                case "RecallInitiatedBy":
                    data = new List<LookUp>();
                    data.Add(new LookUp("Cascade", "Cascade"));
                    data.Add(new LookUp("Seller", "Seller"));
                    lookupData = data.AsEnumerable<LookUp>();
                    break;
                case "PutbackInitiatedBy":
                    data = new List<LookUp>();
                    data.Add(new LookUp("Cascade", "Cascade"));
                    data.Add(new LookUp("Buyer", "Buyer"));
                    lookupData = data.AsEnumerable<LookUp>();
                    break;
                case "MediaRequestStatus":
                    MSI_MediaRequestStatusRepository statusData = new MSI_MediaRequestStatusRepository();
                    lookupData = from status in statusData.GetAll()
                                 select new LookUp(status.Name,status.Id.ToString());
                    break;
                default:
                    break;
            }
            return lookupData;
        }
        /// <summary>
        /// This is used to Add Decscription with the Text Property in DDL
        /// </summary>
        /// <param name="_descriptionText"></param>
        /// <returns></returns>
        public string GetDescriptionDetails(string _descriptionText)
        {
            if (!string.IsNullOrEmpty(_descriptionText))
            {
                //We have value
                return " | " + _descriptionText;
            }
            else
            {
                return "";
            }
        }
        public class Product_CodeDistinct : IEqualityComparer<RACCOUNT>
        {

            #region IEqualityComparer<Task> Members

            public bool Equals(RACCOUNT x, RACCOUNT y)
            {
                return x.PRODUCT_CODE == y.PRODUCT_CODE;
            }

            public int GetHashCode(RACCOUNT obj)
            {
                return obj.PRODUCT_CODE.GetHashCode();
            }

            #endregion
        }
    }
}