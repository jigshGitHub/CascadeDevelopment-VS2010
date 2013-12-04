using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cascade.Data.Models;
using System.Data.SqlClient;
using System.Data;
using Cascade.Helpers;
namespace Cascade.Data.Repositories
{
    public class DataQueries
    {
        private static readonly string thisClass = "Cascade.Data.Repositories.DataQueries";

        public IEnumerable<Purchases> GetPurchases(DateTime? startDate, DateTime? endDate, string productCode)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<Purchases> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spPurchasesReport", new SqlParameter("@startDate", startDate), new SqlParameter("@endDate", endDate), new SqlParameter("@prodCode", productCode));
                data = new List<Purchases>();
                Purchases record;
                while (rdr.Read())
                {
                    record = new Purchases();
                    record.ACCOUNT = rdr["ACCOUNT"].ToString();
                    record.PRODUCT_CODE = rdr["PRODUCT_CODE"].ToString();
                    record.PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]);
                    record.purchaseprice = rdr["purchaseprice"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["purchaseprice"]);
                    record.OriginalBalance = rdr["OriginalBalance"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OriginalBalance"]);
                    record.Seller = rdr["Seller"].ToString();
                    if (record.purchaseprice == Convert.ToDecimal(0.0))
                    {
                        record.purchaseprice = null;
                    }
                    if (record.OriginalBalance == Convert.ToDecimal(0.0))
                    {
                        record.OriginalBalance = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPurchases:" + ex.Message);
            }
            return data.AsEnumerable<Purchases>();
        }

        public IEnumerable<Sales> GetSales(DateTime? startDate, DateTime? endDate, string productCode)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<Sales> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spSalesReport", new SqlParameter("@startDate", startDate), new SqlParameter("@endDate", endDate), new SqlParameter("@prodCode", productCode));
                data = new List<Sales>();
                Sales record;
                while (rdr.Read())
                {
                    record = new Sales();
                    record.ACCOUNT = rdr["ACCOUNT"].ToString();
                    record.PRODUCT_CODE = rdr["PRODUCT_CODE"].ToString();
                    record.SoldDate = Convert.ToDateTime(rdr["SoldDate"]);
                    record.FaceValue = rdr["FaceValue"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValue"]);
                    record.SalesPrice = rdr["SalesPrice"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesPrice"]);
                    record.Responsibility = rdr["Responsibility"].ToString();
                    if (record.FaceValue == Convert.ToDecimal(0.0))
                    {
                        record.FaceValue = null;
                    }
                    if (record.SalesPrice == Convert.ToDecimal(0.0))
                    {
                        record.SalesPrice = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetSales:" + ex.Message);
            }
            return data.AsEnumerable<Sales>();
        }

        public IEnumerable<PortfolioTransactions> GetPortfolioTransactionsReports(string productCode)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PortfolioTransactions> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spPortfolioTransactionsReport", new SqlParameter("@prodCode", productCode));
                data = new List<PortfolioTransactions>();
                PortfolioTransactions record;
                while (rdr.Read())
                {
                    record = new PortfolioTransactions();
                    record.ACCOUNT = rdr["ACCOUNT"].ToString();
                    record.PRODUCT_CODE = rdr["PRODUCT_CODE"].ToString();
                    record.Tran_Date = Convert.ToDateTime(rdr["Tran_Date"]);
                    record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    record.TRAN_CODE = rdr["TRAN_CODE"].ToString();
                    record.TRAN_SOURCE = rdr["TRAN_SOURCE"].ToString();
                    record.Responsibility = rdr["Responsibility"].ToString();
                    if (record.Amount == Convert.ToDecimal(0.0))
                    {
                        record.Amount = null;
                    }

                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPortfolioTransactionsReports:" + ex.Message);
            }
            return data.AsEnumerable<PortfolioTransactions>();
        }


        public IEnumerable<MSI_MediaTracker> GetExistingUploadMediaDetails(string accountNumber)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MSI_MediaTracker> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaTrackerByAccountNumber", new SqlParameter("@account", accountNumber));
                data = new List<MSI_MediaTracker>();
                MSI_MediaTracker record;
                while (rdr.Read())
                {
                    record = new MSI_MediaTracker();
                    record.Id = Convert.ToInt32(rdr["Id"].ToString());
                    record.Account = rdr["Account"].ToString();
                    record.OriginalAccount = rdr["OriginalAccount"].ToString();
                    record.MediaTypeId = Convert.ToInt32(rdr["MediaTypeId"].ToString());
                    record.MediaDocuments = rdr["MediaDocuments"].ToString();
                    record.TypeConstraints = rdr["TypeConstraints"].ToString();

                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetExistingUploadMediaDetails:" + ex.Message);
            }
            return data.AsEnumerable<MSI_MediaTracker>();
        }


        public IEnumerable<PortfolioSummary> GetPortfolioSummaryReports(string description)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PortfolioSummary> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spPortfolioSummary", new SqlParameter("@description", description));
                data = new List<PortfolioSummary>();
                PortfolioSummary record;
                while (rdr.Read())
                {
                    record = new PortfolioSummary();
                    record.Description = rdr["Description"].ToString();
                    record.Type = rdr["Type"].ToString();
                    record.Accounts = Convert.ToInt32(rdr["Accounts"]);
                    record.Balance = rdr["Balance"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Balance"]);
                    record.ShortDescription = rdr["ShortDescription"].ToString();
                    record.Seller = rdr["Seller"].ToString();
                    record.Status = rdr["Status"].ToString();
                    if (record.Balance == Convert.ToDecimal(0.0))
                    {
                        record.Balance = null;
                    }

                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPortfolioSummaryReports:" + ex.Message);
            }
            return data.AsEnumerable<PortfolioSummary>();
        }

        public IEnumerable<PortfolioPieRpt> GetPortfolioWorkStationDescription()
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PortfolioPieRpt> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("sp_PortfolioWorkStationDescriptionReport");
                data = new List<PortfolioPieRpt>();
                while (rdr.Read())
                {
                    data.Add(new PortfolioPieRpt { Count = rdr["Count"].ToString(), KeyText = rdr["WorkStatusDescription"].ToString() });
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPortfolioWorkStationDescription:" + ex.Message);
            }
            return data.AsEnumerable<PortfolioPieRpt>();
        }

        public IEnumerable<PortfolioPieRpt> GetPortfolioOwner()
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PortfolioPieRpt> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("sp_PortfolioOwnerReport");
                data = new List<PortfolioPieRpt>();
                while (rdr.Read())
                {
                    data.Add(new PortfolioPieRpt { Count = rdr["Count"].ToString(), KeyText = rdr["PortfolioOwner"].ToString() });
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPortfolioOwner:" + ex.Message);
            }
            return data.AsEnumerable<PortfolioPieRpt>();
        }

        public IQueryable<SearchResult> GetSearchResults(string name)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<SearchResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spBasicSearch", new SqlParameter("@name", name));
                data = new List<SearchResult>();
                SearchResult record;
                while (rdr.Read())
                {
                    record = new SearchResult();

                    CreateSearchResult(rdr, record);

                    data.Add(record);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetSearchResults:" + ex.Message);
            }
            return data.AsQueryable<SearchResult>();
        }

        public IQueryable<SearchResult> GetSearchResults(string account, string originator, string seller, string investor)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<SearchResult> data = null;
            object _account, _originator, _seller, _investor = null;
            if (string.IsNullOrEmpty(account))
                _account = DBNull.Value;
            else
                _account = account;
            if (string.IsNullOrEmpty(originator))
                _originator = DBNull.Value;
            else
                _originator = originator;
            if (string.IsNullOrEmpty(seller))
                _seller = DBNull.Value;
            else
                _seller = seller;
            if (string.IsNullOrEmpty(investor))
                _investor = DBNull.Value;
            else
                _investor = investor;

            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spAdvanceSearch",
                    new SqlParameter("@accountNumber", _account),
                    new SqlParameter("@originator", _originator),
                    new SqlParameter("@seller", _seller),
                    new SqlParameter("@ssnFourDigits", _investor));
                data = new List<SearchResult>();
                SearchResult record;
                while (rdr.Read())
                {
                    record = new SearchResult();

                    CreateSearchResult(rdr, record);

                    data.Add(record);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetSearchResults:" + ex.Message);
            }
            return data.AsQueryable<SearchResult>();
        }

        public IEnumerable<DPSViewEditResult> GetDPSViewEditRecords(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<DPSViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetDPSViewEditRecords", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<DPSViewEditResult>();
                DPSViewEditResult record;
                while (rdr.Read())
                {
                    record = new DPSViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OriginalAcct = rdr["OriginalAcct"].ToString();
                    record.TransDate = Convert.ToDateTime(rdr["TransDate"]);
                    record.DateRec = Convert.ToDateTime(rdr["DateRec"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    if (record.Amount == Convert.ToDecimal(0.0))
                    {
                        record.Amount = null;
                    }
                    if (record.TransDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.TransDate = null;
                    }
                    if (record.DateRec.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateRec = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetDPSViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<DPSViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaViewEditRecords(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaViewEditRecords", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                    record.CODate = Convert.ToDateTime(rdr["CODate"]);
                    record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                    if (record.SellerFee == Convert.ToDecimal(0.0))
                    {
                        record.SellerFee = null;
                    }
                    record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                    if (record.OurFee == Convert.ToDecimal(0.0))
                    {
                        record.OurFee = null;
                    }
                    if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.OpenDate = null;
                    }
                    if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.CODate = null;
                    }
                    if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.OrderDate = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetRecallViewEditRecords(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallViewEditRecords", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                    record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.InitiatedBy = rdr["RecallInitiatedBy"].ToString();
                    record.CheckDocuments = rdr["CheckDocuments"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateAcctClosed = null;
                    }
                    if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateNoteSent = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<PutBackViewEditResult> GetPutBackViewEditRecords(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PutBackViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetPutBackViewEditRecords", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<PutBackViewEditResult>();
                PutBackViewEditResult record;
                while (rdr.Read())
                {
                    record = new PutBackViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                    record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.InitiatedBy = rdr["PutBackInitiatedBy"].ToString();
                    record.CheckDocuments = rdr["CheckDocuments"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateAcctClosed = null;
                    }
                    if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateNoteSent = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPutBackViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<PutBackViewEditResult>();
        }

        public IEnumerable<PeopleViewEditResult> GetPeopleViewEditRecords(string FirstName, string LastName)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PeopleViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetPeopleViewEditRecords", new SqlParameter("@FirstName", FirstName), new SqlParameter("@LastName", LastName));
                data = new List<PeopleViewEditResult>();
                PeopleViewEditResult record;
                while (rdr.Read())
                {
                    record = new PeopleViewEditResult();
                    record.PID = Convert.ToInt32(rdr["PID"].ToString());
                    record.FName = rdr["FName"].ToString();
                    record.LName = rdr["LName"].ToString();
                    record.Address = rdr["Address"].ToString();
                    record.CreatedAt = Convert.ToDateTime(rdr["CreatedAt"]);
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPeopleViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<PeopleViewEditResult>();
        }

        public IEnumerable<MoneyViewEditResult> GetMoneyViewEditRecords(Decimal? Amount, int Source)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MoneyViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMoneyViewEditRecords", new SqlParameter("@Amount", Amount), new SqlParameter("@Source", Source.ToString()));
                data = new List<MoneyViewEditResult>();
                MoneyViewEditResult record;
                while (rdr.Read())
                {
                    record = new MoneyViewEditResult();
                    record.MID = Convert.ToInt32(rdr["MID"].ToString());
                    record.FName = rdr["FName"].ToString();
                    record.LName = rdr["LName"].ToString();
                    record.SText = rdr["SText"].ToString();
                    record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    if (record.Amount == Convert.ToDecimal(0.0))
                    {
                        record.Amount = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMoneyViewEditRecords:" + ex.Message);
            }
            return data.AsEnumerable<MoneyViewEditResult>();
        }

        private void CreateSearchResult(SqlDataReader rdr, SearchResult record)
        {
            try
            {
                record.Name = rdr["Name"].ToString();
                record.ProductDescription = rdr["ProductDescription"].ToString();
                record.WorkStatusDescription = rdr["WorkStatusDescription"].ToString();
                record.RespAgency = rdr["RespAgency"].ToString();
                record.StatusDescription = rdr["StatusDescription"].ToString();
                record.ACCOUNT = rdr["ACCOUNT"].ToString();
                record.Originator = rdr["Originator"].ToString();
                record.Seller = rdr["Seller"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.CreateSearchResult:" + ex.Message);
            }

        }

        #region Portfolio Data
        public MSI_Port_Acq_Original GetPortfolioPurchaseSummary(string productCode)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}", thisMethod, productCode);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_Original portfolio = null;
            DBFactory db;
            System.Data.DataSet ds;
            System.Data.DataRow dr;
            try
            {
                portfolio = new MSI_Port_Acq_Original();
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioPurchaseSummary", "PurchaseSummary", new SqlParameter("@productCode", productCode));

                dr = ds.Tables["PurchaseSummary"].Rows[0];
                portfolio.Portfolio_ = dr["Portfolio#"].ToString();
                portfolio.Company = dr["Company"].ToString();
                portfolio.Seller = dr["Seller"].ToString();
                portfolio.CostBasis = Convert.ToDouble(dr["CostBasis"].ToString());
                portfolio.Face = Convert.ToDecimal(dr["Face"].ToString());
                portfolio.Cut_OffDate = DateTime.Parse(dr["Cut-OffDate"].ToString());
                portfolio.C_ofAccts = Convert.ToDouble(dr["#ofAccts"].ToString());
                portfolio.PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"].ToString());
                DateTime closingDate;
                if (DateTime.TryParse(dr["ClosingDate"].ToString(), out closingDate))
                    portfolio.ClosingDate = closingDate;
                portfolio.Lender_FileDescription = dr["Lender/FileDescription"].ToString();
                int putbackTermDays;
                if (int.TryParse(dr["PutBackTerm"].ToString(), out putbackTermDays))
                    portfolio.PutbackTerm__days_ = putbackTermDays;
                DateTime putbackDeadLine;
                if (DateTime.TryParse(dr["PutbackDeadLine"].ToString(), out putbackDeadLine))
                    portfolio.PutbackDeadline = putbackDeadLine;
                portfolio.Notes = dr["Notes"].ToString();
                int resaleId;
                if (int.TryParse(dr["ResaleRestrictionId"].ToString(), out resaleId))
                    portfolio.ResaleRestrictionId = resaleId;
                portfolio.CreatedBy = dr["CreatedBy"].ToString();
                portfolio.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                portfolio.UpdatedBy = dr["UpdatedBy"].ToString();
                portfolio.UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString());
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return portfolio;
        }

        public MSI_Port_Acq_Original GetPortfolioOriginal(string portfolioNumber)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters portfolioNumber={1}", thisMethod, portfolioNumber);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_Original portfolio = null;
            MSI_Port_Acq_OriginalRepository repository = null;
            try
            {
                repository = new MSI_Port_Acq_OriginalRepository();
                portfolio = repository.GetById(portfolioNumber);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return portfolio;
        }

        public MSI_Port_SalesTrans_Original GetPortfolioSalesTransactionOriginal(int id)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, id);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_Original transaction = null;
            MSI_Port_SalesTrans_OriginalRepository repository = null;
            try
            {
                repository = new MSI_Port_SalesTrans_OriginalRepository();
                transaction = repository.GetById(id);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return transaction;
        }

        public IEnumerable<MSI_Port_SalesTrans_Original> GetPortfolioSalesSummary(string productCode, string userId)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}, userId={2}", thisMethod, productCode, userId);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_Original salesTransaction = null;
            DBFactory db;
            List<MSI_Port_SalesTrans_Original> salesTransactions = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioSalesSummary", "PurchaseSalesSummary", new SqlParameter("@productCode", productCode), new SqlParameter("@userId", userId));

                if (ds.Tables["PurchaseSalesSummary"].Rows.Count > 0)
                {
                    salesTransactions = new List<MSI_Port_SalesTrans_Original>();
                    foreach (System.Data.DataRow dr in ds.Tables["PurchaseSalesSummary"].Rows)
                    {
                        salesTransaction = new MSI_Port_SalesTrans_Original();
                        salesTransaction.ID = int.Parse(dr["ID"].ToString());
                        salesTransaction.Portfolio_ = dr["Portfolio#"].ToString();
                        salesTransaction.Buyer = dr["Buyer"].ToString();
                        if (dr["SalesBasis"] != DBNull.Value)
                            salesTransaction.SalesBasis = Convert.ToDouble(dr["SalesBasis"].ToString());
                        if (dr["FaceValue"] != DBNull.Value)
                            salesTransaction.FaceValue = Convert.ToDecimal(dr["FaceValue"].ToString());
                        if (dr["Cut-OffDate"] != DBNull.Value)
                            salesTransaction.Cut_OffDate = DateTime.Parse(dr["Cut-OffDate"].ToString());
                        if (dr["#ofAccts"] != DBNull.Value)
                            salesTransaction.C_ofAccts = Convert.ToDouble(dr["#ofAccts"].ToString());
                        if (dr["SalesPrice"] != DBNull.Value)
                            salesTransaction.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                        DateTime closingDate;
                        if (DateTime.TryParse(dr["ClosingDate"].ToString(), out closingDate))
                            salesTransaction.ClosingDate = closingDate;
                        salesTransaction.Lender = dr["Lender"].ToString();
                        int putbackTermDays;
                        if (int.TryParse(dr["PutbackTerm"].ToString(), out putbackTermDays))
                            salesTransaction.PutbackTerm_days_ = putbackTermDays;
                        DateTime putbackDeadLine;
                        if (DateTime.TryParse(dr["PutbackDeadLine"].ToString(), out putbackDeadLine))
                            salesTransaction.PutbackDeadline = putbackDeadLine;
                        salesTransaction.Notes = dr["Notes"].ToString();
                        DateTime notNullDt;
                        if (DateTime.TryParse(dr["CreatedDate"].ToString(), out notNullDt))
                            salesTransaction.CreatedDate = notNullDt;

                        if (DateTime.TryParse(dr["UpdatedDate"].ToString(), out notNullDt))
                            salesTransaction.UpdatedDate = notNullDt;
                        salesTransaction.CreatedBy = dr["CreatedBy"].ToString();
                        salesTransaction.UpdatedBy = dr["UpdatedBy"].ToString();
                        salesTransactions.Add(salesTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return salesTransactions.AsEnumerable<MSI_Port_SalesTrans_Original>();
        }

        public IEnumerable<MSI_Port_CollectionsTrans> GetPortfolioCollectionsSummary(string productCode, string userId, bool isOriginal)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}, userId={2}", thisMethod, productCode, userId);
            LogHelper.Info(logMessage);

            MSI_Port_CollectionsTrans collectionTransaction = null;
            DBFactory db;
            List<MSI_Port_CollectionsTrans> collectionTransactions = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioCollectionsSummary", "PurchaseCollectionsSummary", new SqlParameter("@productCode", productCode), new SqlParameter("@userId", userId), new SqlParameter("@isOriginal", isOriginal));

                if (ds.Tables["PurchaseCollectionsSummary"].Rows.Count > 0)
                {
                    collectionTransactions = new List<MSI_Port_CollectionsTrans>();
                    foreach (System.Data.DataRow dr in ds.Tables["PurchaseCollectionsSummary"].Rows)
                    {
                        collectionTransaction = new MSI_Port_CollectionsTrans();
                        collectionTransaction.ID = int.Parse(dr["ID"].ToString());
                        collectionTransaction.Portfolio_ = dr["Portfolio#"].ToString();
                        if (dr["FaceValue"] != DBNull.Value)
                            collectionTransaction.FaceValue = Convert.ToDecimal(dr["FaceValue"].ToString());
                        if (dr["SalesPrice"] != DBNull.Value)
                            collectionTransaction.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                        DateTime closingDate;
                        if (DateTime.TryParse(dr["ClosingDate"].ToString(), out closingDate))
                            collectionTransaction.ClosingDate = closingDate;
                        collectionTransaction.Inv_AgencyName = dr["Inv_AgencyName"].ToString();
                        DateTime notNullDt;
                        if (DateTime.TryParse(dr["CreatedDate"].ToString(), out notNullDt))
                            collectionTransaction.CreatedDate = notNullDt;
                        if (DateTime.TryParse(dr["UpdatedDate"].ToString(), out notNullDt))
                            collectionTransaction.UpdatedDate = notNullDt;
                        collectionTransaction.CreatedBy = dr["CreatedBy"].ToString();
                        collectionTransaction.UpdatedBy = dr["UpdatedBy"].ToString();
                        collectionTransaction.TransType = dr["TransType"].ToString();
                        collectionTransactions.Add(collectionTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return collectionTransactions.AsEnumerable<MSI_Port_CollectionsTrans>();
        }

        public IEnumerable<MSI_Port_InvestmentsTrans> GetPortfolioInvestmentsSummary(string productCode, string userId, bool isOriginal)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}, userId={2}", thisMethod, productCode, userId);
            LogHelper.Info(logMessage);
            
            MSI_Port_InvestmentsTrans investmentTransaction = null;
            DBFactory db;
            List<MSI_Port_InvestmentsTrans> investmentTransactions = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioInvestmentsSummary", "PurchaseInvestmentsSummary", new SqlParameter("@productCode", productCode), new SqlParameter("@userId", userId), new SqlParameter("@isOriginal", isOriginal));

                if (ds.Tables["PurchaseInvestmentsSummary"].Rows.Count > 0)
                {
                    investmentTransactions = new List<MSI_Port_InvestmentsTrans>();
                    foreach (System.Data.DataRow dr in ds.Tables["PurchaseInvestmentsSummary"].Rows)
                    {
                        investmentTransaction = new MSI_Port_InvestmentsTrans();
                        investmentTransaction.ID = int.Parse(dr["ID"].ToString());
                        investmentTransaction.Portfolio_ = dr["Portfolio#"].ToString();
                        if (dr["ProfitShare_before"] != DBNull.Value)
                            investmentTransaction.ProfitShare_before = Convert.ToDouble(dr["ProfitShare_before"].ToString());
                        if (dr["ProfitShare_after"] != DBNull.Value)
                            investmentTransaction.ProfitShare_after = Convert.ToDouble(dr["ProfitShare_after"].ToString());
                        if (dr["SalesPrice"] != DBNull.Value)
                            investmentTransaction.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                        if (dr["Notes"] != DBNull.Value)
                            investmentTransaction.Notes = dr["Notes"].ToString();
                        if (dr["Interest"] != DBNull.Value)
                            investmentTransaction.Interest = Convert.ToDouble( dr["Interest"].ToString());
                        investmentTransaction.Inv_AgencyName = dr["Inv_AgencyName"].ToString();
                        DateTime notNullDt;
                        if (DateTime.TryParse(dr["CreatedDate"].ToString(), out notNullDt))
                            investmentTransaction.CreatedDate = notNullDt;
                        if (DateTime.TryParse(dr["UpdatedDate"].ToString(), out notNullDt))
                            investmentTransaction.UpdatedDate = notNullDt;
                        investmentTransaction.CreatedBy = dr["CreatedBy"].ToString();
                        investmentTransaction.UpdatedBy = dr["UpdatedBy"].ToString();
                        investmentTransaction.TransType = dr["TransType"].ToString();
                        investmentTransactions.Add(investmentTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return investmentTransactions.AsEnumerable<MSI_Port_InvestmentsTrans>();
        }

        public IEnumerable<MSI_Port_DistributionTrans> GetPortfolioDistributionSummary(string productCode, string userId, bool isOriginal)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}, userId={2}", thisMethod, productCode, userId);
            LogHelper.Info(logMessage);

            MSI_Port_DistributionTrans distributionTransaction = null;
            DBFactory db;
            List<MSI_Port_DistributionTrans> distributionTransactions = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioDistributionSummary", "DistributionSummary", new SqlParameter("@productCode", productCode), new SqlParameter("@userId", userId), new SqlParameter("@isOriginal", isOriginal));

                if (ds.Tables["DistributionSummary"].Rows.Count > 0)
                {
                    distributionTransactions = new List<MSI_Port_DistributionTrans>();
                    foreach (System.Data.DataRow dr in ds.Tables["DistributionSummary"].Rows)
                    {
                        distributionTransaction = new MSI_Port_DistributionTrans();
                        distributionTransaction.ID = int.Parse(dr["ID"].ToString());
                        distributionTransaction.Portfolio_ = dr["Portfolio#"].ToString();
                        if (dr["SalesPrice"] != DBNull.Value)
                            distributionTransaction.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                        if (dr["Notes"] != DBNull.Value)
                            distributionTransaction.Notes = dr["Notes"].ToString();
                        if (dr["CheckNo"] != DBNull.Value)
                            distributionTransaction.CheckNo = dr["CheckNo"].ToString();
                        distributionTransaction.Inv_AgencyName = dr["Inv_AgencyName"].ToString();
                        DateTime notNullDt;
                        if (DateTime.TryParse(dr["ClosingDate"].ToString(), out notNullDt))
                            distributionTransaction.ClosingDate = notNullDt;
                        if (DateTime.TryParse(dr["CreatedDate"].ToString(), out notNullDt))
                            distributionTransaction.CreatedDate = notNullDt;
                        if (DateTime.TryParse(dr["UpdatedDate"].ToString(), out notNullDt))
                            distributionTransaction.UpdatedDate = notNullDt;
                        distributionTransaction.CreatedBy = dr["CreatedBy"].ToString();
                        distributionTransaction.UpdatedBy = dr["UpdatedBy"].ToString();
                        distributionTransactions.Add(distributionTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return distributionTransactions.AsEnumerable<MSI_Port_DistributionTrans>();
        }

        public IEnumerable<MSI_Port_InterestTrans> GetPortfolioInterestSummary(string productCode, string userId, bool isOriginal)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters productCode={1}, userId={2}", thisMethod, productCode, userId);
            LogHelper.Info(logMessage);

            MSI_Port_InterestTrans interestTransaction = null;
            DBFactory db;
            List<MSI_Port_InterestTrans> interestTransactions = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_sp_GetPortfolioInterestSummary", "InterestSummary", new SqlParameter("@productCode", productCode), new SqlParameter("@userId", userId), new SqlParameter("@isOriginal", isOriginal));

                if (ds.Tables["InterestSummary"].Rows.Count > 0)
                {
                    interestTransactions = new List<MSI_Port_InterestTrans>();
                    foreach (System.Data.DataRow dr in ds.Tables["InterestSummary"].Rows)
                    {
                        interestTransaction = new MSI_Port_InterestTrans();
                        interestTransaction.ID = int.Parse(dr["ID"].ToString());
                        interestTransaction.Portfolio_ = dr["Portfolio#"].ToString();
                        if (dr["SalesPrice"] != DBNull.Value)
                            interestTransaction.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                        if (dr["Notes"] != DBNull.Value)
                            interestTransaction.Notes = dr["Notes"].ToString();
                        if (dr["CheckNo"] != DBNull.Value)
                            interestTransaction.CheckNo = dr["CheckNo"].ToString();
                        interestTransaction.Inv_AgencyName = dr["Inv_AgencyName"].ToString();
                        DateTime notNullDt;
                        if (DateTime.TryParse(dr["ClosingDate"].ToString(), out notNullDt))
                            interestTransaction.ClosingDate = notNullDt;
                        if (DateTime.TryParse(dr["CreatedDate"].ToString(), out notNullDt))
                            interestTransaction.CreatedDate = notNullDt;
                        if (DateTime.TryParse(dr["UpdatedDate"].ToString(), out notNullDt))
                            interestTransaction.UpdatedDate = notNullDt;
                        interestTransaction.CreatedBy = dr["CreatedBy"].ToString();
                        interestTransaction.UpdatedBy = dr["UpdatedBy"].ToString();
                        interestTransactions.Add(interestTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return interestTransactions.AsEnumerable<MSI_Port_InterestTrans>();
        }

        public void AddPortfolio(MSI_Port_Acq_Original portfolioToSave)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters Portfolio Number={1}", thisMethod, portfolioToSave.Portfolio_);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_OriginalRepository repository = null;
            try
            {

                repository = new MSI_Port_Acq_OriginalRepository();
                repository.Add(portfolioToSave);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
        }

        public void UpdatePortfolio(MSI_Port_Acq_Original portfolioToSave)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters Portfolio Number={1}", thisMethod, portfolioToSave.Portfolio_);
            LogHelper.Info(logMessage);

            MSI_Port_Acq_OriginalRepository repository = null;
            try
            {

                repository = new MSI_Port_Acq_OriginalRepository();
                repository.Update(portfolioToSave);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
        }

        public MSI_Port_SalesTrans_Original AddSalesTransaction(MSI_Port_SalesTrans_Original inTransaction)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, inTransaction.ID);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_OriginalRepository repository = null;

            try
            {
                repository = new MSI_Port_SalesTrans_OriginalRepository();
                repository.Add(inTransaction);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }

            return inTransaction;
        }

        public MSI_Port_SalesTrans_Original UpdateSalesTransaction(MSI_Port_SalesTrans_Original inTransaction)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, inTransaction.ID);
            LogHelper.Info(logMessage);

            MSI_Port_SalesTrans_OriginalRepository repository = null;

            try
            {
                repository = new MSI_Port_SalesTrans_OriginalRepository();
                repository.Update(inTransaction);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }

            return inTransaction;
        }

        #endregion
        public IEnumerable<LookUp> GetDistinctProductCodes()
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}", thisMethod);
            LogHelper.Info(logMessage);

            DBFactory db;
            SqlDataReader rdr;
            List<LookUp> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_sp_GetDistinctProductCode");
                data = new List<LookUp>();
                LookUp record;
                while (rdr.Read())
                {
                    if(data.Count == 0){
                        data.Add(new LookUp("Add New", ""));
                    }
                    record = new LookUp(rdr["Product_Code"].ToString(), rdr["Product_Code"].ToString());

                    data.Add(record);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return data.AsQueryable<LookUp>();
        }

        public IEnumerable<LookUp> GetDistinctResponsibility()
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}", thisMethod);
            LogHelper.Info(logMessage);

            DBFactory db;
            SqlDataReader rdr;
            List<LookUp> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_sp_GetDistinctResponsibility");
                data = new List<LookUp>();
                LookUp record;
                while (rdr.Read())
                {
                    record = new LookUp(rdr["RESPONSIBILITY"].ToString(), rdr["RESPONSIBILITY"].ToString());

                    data.Add(record);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return data.AsQueryable<LookUp>();
        }

        public IEnumerable<MSI_Debtor> GetDebtors(string accountNumber)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters accountNumber={1}", thisMethod, accountNumber);
            LogHelper.Info(logMessage);

            MSI_Debtor debtor = null;
            DBFactory db;
            List<MSI_Debtor> debtors = null;
            System.Data.DataSet ds;
            try
            {
                db = new DBFactory();
                ds = db.ExecuteDataset("MSI_spGetDebtors", "Debtors", new SqlParameter("@pimsAccountNumber", accountNumber));

                if (ds.Tables["Debtors"].Rows.Count > 0)
                {
                    debtors = new List<MSI_Debtor>();
                    foreach (System.Data.DataRow dr in ds.Tables["Debtors"].Rows)
                    {
                        debtor = new MSI_Debtor();
                        debtor.Account = dr["ACCOUNT"].ToString();
                        debtor.FirstName = dr["FirstName"].ToString();
                        debtor.LastName = dr["LastName"].ToString();
                        debtor.Address1 = dr["ADDRESS1"].ToString();
                        debtor.Address2 = dr["ADDRESS2"].ToString();
                        debtor.City = dr["CITY"].ToString();
                        debtor.State = dr["STATE"].ToString();
                        debtor.Zip = dr["ZIP_CODE"].ToString();
                        string ssn = dr["SSN"].ToString().Trim();
                        debtor.LastFourSSN = ssn.Substring(7, 4);
                        debtor.MobilePhone = dr["PHONE_CELL"].ToString();
                        debtor.HomePhone = dr["PHONE_HOME"].ToString();
                        debtor.WorkPhone = dr["PHONE_WORK"].ToString();
                        debtor.DOB = dr["DOB"].ToString();
                        debtor.DebtCurrentBalance = dr["DebtCurrentBalance"].ToString();
                        debtor.DebtPurchaseBalance = dr["DebtorPurchaseBalance"].ToString();
                        debtor.CreditorName = dr["CreditorName"].ToString();

                        debtors.Add(debtor);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return debtors.AsEnumerable<MSI_Debtor>();
        }
        public IEnumerable<ComplianceViewResult> GetComplianceReportRecords(string AgencyId, string reportType)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters AgencyId={1}, reportType={2}", thisMethod, AgencyId, reportType);
            LogHelper.Info(logMessage);

            DBFactory db;
            SqlDataReader rdr = null;
            string reportName = "";
            List<ComplianceViewResult> data = null;
            try
            {

                db = new DBFactory();
                switch (reportType)
                {
                    case "NCRA": reportName = "MSI_spGetComplianceNCRAReportData";
                        break;
                    case "ORP": reportName = "MSI_spGetComplianceORPReportData";
                        break;
                    case "RC": reportName = "MSI_spGetComplianceRCReportData";
                        break;
                    case "SOA": reportName = "MSI_spGetComplianceSOAReportData";
                        break;
                    case "AAI": reportName = "MSI_spGetComplianceAAIReportData";
                        break;
                    case "NCP": reportName = "MSI_spGetComplianceNCPReportData";
                        break;

                }


                rdr = db.ExecuteReader(reportName, new SqlParameter("@AgencyId", AgencyId));
                data = new List<ComplianceViewResult>();
                ComplianceViewResult record;
                while (rdr.Read())
                {
                    record = new ComplianceViewResult();
                    record.AgencyId = rdr["AgencyId"].ToString();
                    record.ComplaintID = rdr["ComPlaintId"].ToString();
                    record.Account = rdr["PIMSAccount"].ToString();
                    record.LastName = rdr["LastName"].ToString();
                    record.FirstName = rdr["FirstName"].ToString();
                    record.LastFourSSN = rdr["LastFourSSN"].ToString();
                    if (rdr["ComplaintDate"] != DBNull.Value)
                        record.ComplaintDate = Convert.ToDateTime(rdr["ComplaintDate"]);
                    if (rdr["MoreInfoRequestedDate"] != DBNull.Value)
                        record.MoreInfoRequestedDate = Convert.ToDateTime(rdr["MoreInfoRequestedDate"]);
                    if (rdr["ComplaintSubmittedDate"] != DBNull.Value)
                        record.ComplaintSubmittedDate = Convert.ToDateTime(rdr["ComplaintSubmittedDate"]);
                    if (rdr["AgencyResponseToDebtorDate"] != DBNull.Value)
                        record.AgencyResponseToDebtorDate = Convert.ToDateTime(rdr["AgencyResponseToDebtorDate"]);
                    if (rdr["TotalResponseTimeDays"] != DBNull.Value)
                        record.TotalResponseTimeDays = rdr["TotalResponseTimeDays"].ToString();

                    if (record.ComplaintDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.ComplaintDate = null;
                    }
                    if (record.MoreInfoRequestedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.MoreInfoRequestedDate = null;
                    }
                    if (record.ComplaintSubmittedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.ComplaintSubmittedDate = null;
                    }
                    if (record.AgencyResponseToDebtorDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.AgencyResponseToDebtorDate = null;
                    }


                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new Exception("Exception in DataQueries.ReportType:" + reportType + "GetComplianceReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<ComplianceViewResult>();
        }
        public IEnumerable<ComplianceViewResult> GetComplianceReportRecordsExport(string AgencyId, string reportType)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters AgencyId={1}, reportType={2}", thisMethod, AgencyId, reportType);
            LogHelper.Info(logMessage);

            DBFactory db;
            SqlDataReader rdr;
            List<ComplianceViewResult> data = null;
            string reportName = "";
            try
            {
                db = new DBFactory();
                switch (reportType)
                {
                    case "NCRA": reportName = "MSI_spGetComplianceNCRAReportDataExport";
                        break;
                    case "ORP": reportName = "MSI_spGetComplianceORPReportDataExport";
                        break;
                    case "RC": reportName = "MSI_spGetComplianceRCReportDataExport";
                        break;
                    case "SOA": reportName = "MSI_spGetComplianceSOAReportDataExport";
                        break;
                    case "AAI": reportName = "MSI_spGetComplianceAAIReportDataExport";
                        break;
                    case "NCP": reportName = "MSI_spGetComplianceNCPReportDataExport";
                        break;

                }
                //rdr = db.ExecuteReader("MSI_spGetComplianceNCRAReportDataExport", new SqlParameter("@AgencyId", AgencyId));
                rdr = db.ExecuteReader(reportName, new SqlParameter("@AgencyId", AgencyId));
                data = new List<ComplianceViewResult>();
                ComplianceViewResult record;
                while (rdr.Read())
                {
                    record = new ComplianceViewResult();
                    record.AgencyId = rdr["AgencyId"].ToString();
                    record.AgencyName = rdr["AgencyName"].ToString();
                    record.LastName = rdr["LastName"].ToString();
                    record.FirstName = rdr["FirstName"].ToString();
                    record.Account = rdr["PIMSAccount"].ToString();
                    record.ComplaintID = rdr["ComPlaintId"].ToString();
                    record.HomePhone = rdr["HomePhone"].ToString();
                    record.WorkPhone = rdr["WorkPhone"].ToString();
                    record.LastFourSSN = rdr["LastFourSSN"].ToString();
                    record.MobilePhone = rdr["MobilePhone"].ToString();
                    record.Address = rdr["Address"].ToString();
                    record.City = rdr["City"].ToString();
                    record.State = rdr["State"].ToString();
                    record.Zip = rdr["Zip"].ToString();
                    record.CreditorName = rdr["CreditorName"].ToString();
                    record.DebtorIdentityVerified = rdr["DebtorIdentityVerifiedText"].ToString();
                    record.ContactMethod = rdr["ContactMethod"].ToString();
                    record.ContactTime = rdr["ContactTime"].ToString();
                    record.DebtProduct = rdr["DebtProduct"].ToString();
                    if (rdr["DebtPurchaseBalance"].ToString() != "")
                        record.DebtPurchaseBalance = rdr["DebtPurchaseBalance"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["DebtPurchaseBalance"]);
                    if (rdr["DebtCurrentBalance"].ToString() != "")
                        record.DebtCurrentBalance = rdr["DebtCurrentBalance"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["DebtCurrentBalance"]);
                    record.DisputeDebt = rdr["DisputeDebtText"].ToString();
                    record.DisputeDebtAmount = rdr["DisputeDebtAmountText"].ToString();
                    record.DisputeDebtDueDate = rdr["DisputeDebtDueDateText"].ToString();

                    record.ComplaintID = rdr["ComplaintId"].ToString();
                    if (rdr["ComplaintDate"] != DBNull.Value)
                        record.ComplaintDate = Convert.ToDateTime(rdr["ComplaintDate"]);
                    record.ComplaintReceivedByMethod = rdr["ComplaintReceivedByMethod"].ToString();
                    record.ComplaintIssue = rdr["ComplaintIssue"].ToString();
                    record.ComplaintNotes = rdr["ComplaintNotes"].ToString();
                    record.ComplaintSubmitedToAgency = rdr["ComplaintSubmitedToAgencyText"].ToString();
                    if (rdr["ComplaintSubmitedToAgencyDate"] != DBNull.Value)
                        record.ComplaintSubmitedToAgencyDate = Convert.ToDateTime(rdr["ComplaintSubmitedToAgencyDate"]);
                    record.MoreInfoReqdFromDebtor = rdr["MoreInfoReqdFromDebtorText"].ToString();
                    if (rdr["MoreInfoRequestedDate"] != DBNull.Value)
                        record.MoreInfoRequestedDate = Convert.ToDateTime(rdr["MoreInfoRequestedDate"]);
                    record.MoreInfoRequested = rdr["MoreInfoRequested"].ToString();
                    record.MoreInfoReceivedFromDebtor = rdr["MoreInfoReceivedFromDebtorText"].ToString();
                    if (rdr["MoreInfoReceivedDate"] != DBNull.Value)
                        record.MoreInfoReceivedDate = Convert.ToDateTime(rdr["MoreInfoReceivedDate"]);
                    record.MoreInfoReceived = rdr["MoreInfoReceived"].ToString();
                    record.ComplaintSubmittedToOwner = rdr["ComplaintSubmittedToOwnerText"].ToString();
                    if (rdr["ComplaintSubmittedDate"] != DBNull.Value)
                        record.ComplaintSubmittedDate = Convert.ToDateTime(rdr["ComplaintSubmittedDate"]);
                    record.TimeToSubmitDays = rdr["TimeToSubmitDays"].ToString();
                    record.MoreInfoFromAgency = rdr["MoreInfoFromAgencyText"].ToString();
                    if (rdr["MoreInfoFromAgencyRequestedDate"] != DBNull.Value)
                        record.MoreInfoFromAgencyRequestedDate = Convert.ToDateTime(rdr["MoreInfoFromAgencyRequestedDate"]);
                    record.MoreInfoFromAgencyRequested = rdr["MoreInfoFromAgencyRequested"].ToString();
                    record.MoreInfoFromAgencyReceived = rdr["MoreInfoFromAgencyReceived"].ToString();
                    if (rdr["MoreInfoFromAgencyReceivedDate"] != DBNull.Value)
                        record.MoreInfoFromAgencyReceivedDate = Convert.ToDateTime(rdr["MoreInfoFromAgencyReceivedDate"]);
                    record.OwnerResponse = rdr["OwnerResponse"].ToString();
                    if (rdr["OwnerResponseDate"] != DBNull.Value)
                        record.OwnerResponseDate = Convert.ToDateTime(rdr["OwnerResponseDate"]);
                    record.OwnerResponseDays = rdr["OwnerResponseDays"].ToString();
                    if (rdr["AgencyResponseToDebtorDate"] != DBNull.Value)
                        record.AgencyResponseToDebtorDate = Convert.ToDateTime(rdr["AgencyResponseToDebtorDate"]);
                    record.TotalResponseTimeDays = rdr["TotalResponseTimeDays"].ToString();
                    record.DebtorAgree = rdr["DebtorAgreeText"].ToString();
                    record.NeedFurtherAction = rdr["NeedFurtherActionText"].ToString();
                    record.FinalActionStep = rdr["FinalActionStep"].ToString();
                    record.IsViewedByOwner = rdr["IsViewedByOwnerText"].ToString();
                    record.IsViewedByAgency = rdr["IsViewedByAgencyText"].ToString();
                    record.CreatedBy = rdr["CreatedBy"].ToString();

                    if (record.DebtPurchaseBalance == Convert.ToDecimal(0.0))
                    {
                        record.DebtPurchaseBalance = null;
                    }
                    if (record.DebtCurrentBalance == Convert.ToDecimal(0.0))
                    {
                        record.DebtCurrentBalance = null;
                    }
                    if (record.ComplaintDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.ComplaintDate = null;
                    }
                    if (record.ComplaintSubmitedToAgencyDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.ComplaintSubmitedToAgencyDate = null;
                    }
                    if (record.MoreInfoRequestedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.MoreInfoRequestedDate = null;
                    }
                    if (record.MoreInfoReceivedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.MoreInfoReceivedDate = null;
                    }
                    if (record.ComplaintSubmittedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.ComplaintSubmittedDate = null;
                    }
                    if (record.MoreInfoFromAgencyRequestedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.MoreInfoFromAgencyRequestedDate = null;
                    }
                    if (record.MoreInfoFromAgencyReceivedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.MoreInfoFromAgencyReceivedDate = null;
                    }
                    if (record.OwnerResponseDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.OwnerResponseDate = null;
                    }
                    if (record.AgencyResponseToDebtorDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.AgencyResponseToDebtorDate = null;
                    }

                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
                throw new Exception("Exception in DataQueries.GetComplianceNCRAReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<ComplianceViewResult>();
        }

        public IEnumerable<MSI_MediaTypes> GetMediaTypes()
        {
            return new MSI_MediaTypesRepository().GetAll();
        }

        #region Media Request Response Data

        public IEnumerable<MSI_MediaRequestResponse> GetMediaRequestResponses()
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}", thisMethod);
            LogHelper.Info(logMessage);

            IEnumerable<MSI_MediaRequestResponse> data = null;
            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                data = from requestResponse in repository.GetAll()
                       select requestResponse;
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);

            }
            return data;

        }

        public IEnumerable<MSI_MediaRequestResponse> GetMediaRequestResponses(string agency, Guid userId)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters agency={1}, userId={2}", thisMethod, agency, userId.ToString());
            LogHelper.Info(logMessage);

            IEnumerable<MSI_MediaRequestResponse> data = null;
            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                if (string.IsNullOrEmpty(agency))
                    data = repository.GetAll();
                else
                    data = from requestResponse in repository.GetAll().Where(record => record.AgencyId == agency)
                           select requestResponse;
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return data;

        }

        public MSI_MediaRequestResponse GetMediaRequestResponse(string id)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters id={1}", thisMethod, id);
            LogHelper.Info(logMessage);

            MSI_MediaRequestResponse data = null;
            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                data = repository.GetById(id);
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return data;

        }

        public MSI_MediaRequestResponse GetMediaRequestResponse(string accountNumber, string agency, Guid userId)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters accountNumber={1}, agency={2}, userId={3}", thisMethod, accountNumber, agency, userId.ToString());
            LogHelper.Info(logMessage);

            MSI_MediaRequestResponse data = null;
            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                if (string.IsNullOrEmpty(agency))
                    data = repository.GetAll().Where(record => (record.ACCOUNT == accountNumber || record.OriginalAccount == accountNumber) && record.RequestedByUserId == userId).SingleOrDefault();
                else
                    data = repository.GetAll().Where(record => (record.ACCOUNT == accountNumber || record.OriginalAccount == accountNumber) && record.AgencyId == agency).SingleOrDefault();
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return data;

        }

        public MSI_MediaRequestResponse AddMediaRequestResponse(MSI_MediaRequestResponse submittedRequest)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters AccountNumber={1}", thisMethod, submittedRequest.ACCOUNT);
            LogHelper.Info(logMessage);

            MSI_MediaRequestResponseRepository repository = new MSI_MediaRequestResponseRepository();
            try
            {
                repository.Add(submittedRequest);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException validationException)
            {
                foreach (System.Data.Entity.Validation.DbEntityValidationResult errorResult in validationException.EntityValidationErrors)
                {
                    foreach (System.Data.Entity.Validation.DbValidationError error in errorResult.ValidationErrors)
                    {
                        ErrorLogHelper.Error(error.ErrorMessage, validationException);
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return submittedRequest;
        }

        #endregion

        #region Media Request Types Data

        public IEnumerable<MSI_MediaRequestTypes> GetMediaRequestTypes(string agency)
        {
            IEnumerable<MSI_MediaRequestTypes> data = null;
            MSI_MediaRequestTypesRepository repository = new MSI_MediaRequestTypesRepository();
            try
            {
                if (string.IsNullOrEmpty(agency))
                    data = from requestType in repository.GetAll()
                           select requestType;
                else
                    data = from requestType in repository.GetAll().Where(record => record.MSI_MediaRequestResponse.AgencyId == agency)
                           select requestType;
            }

            catch (Exception ex)
            {
                throw ex;

            }
            return data;

        }

        public void UpdateMediaRequestdType(MSI_MediaRequestTypes submittedMediatype)
        {
            MSI_MediaRequestTypesRepository repository = new MSI_MediaRequestTypesRepository();
            try
            {
                repository.Update(submittedMediatype);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddMediaRequestdType(MSI_MediaRequestTypes submittedMediatype)
        {
            MSI_MediaRequestTypesRepository repository = new MSI_MediaRequestTypesRepository();
            try
            {
                repository.Add(submittedMediatype);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MSI_MediaRequestTypes GetMediaRequestdType(string requestedId, int typeId)
        {
            MSI_MediaRequestTypesRepository repository = new MSI_MediaRequestTypesRepository();
            MSI_MediaRequestTypes mediaType = null;
            try
            {
                mediaType = repository.GetAll().Where(record => record.RequestedId == requestedId && record.TypeId == typeId).SingleOrDefault();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return mediaType;
        }

        public MSI_MediaRequestTypes GetMediaRequestdType(string id)
        {
            MSI_MediaRequestTypesRepository repository = new MSI_MediaRequestTypesRepository();
            MSI_MediaRequestTypes mediaType = null;
            try
            {
                mediaType = repository.GetById(id);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return mediaType;
        }

        #endregion

        #region Media Tracker Data

        public IEnumerable<MSI_MediaTracker> GetMediaTrackers(string accountNumber)
        {
            IEnumerable<MSI_MediaTracker> data = null;
            MSIMediaTrackerRepository repository = new MSIMediaTrackerRepository();
            try
            {
                data = from requestType in repository.GetAll().Where(record => record.Account == accountNumber)
                       select requestType;
            }

            catch (Exception ex)
            {
                throw ex;

            }
            return data;

        }

        #endregion

        #region Message Notification

        public IEnumerable<MSI_MessageNotification> GetMessageNotifications()
        {
            return new MSI_MessageNotificationRepository().GetAll();
        }

        public MSI_MessageNotification GetMessageNotification(string id)
        {
            return new MSI_MessageNotificationRepository().GetById(id);
        }

        public MSI_MessageNotification AddMessageNotification(MSI_MessageNotification notification)
        {
            string thisMethod = string.Format("{0}.{1}", thisClass, System.Reflection.MethodBase.GetCurrentMethod().Name);
            string logMessage = string.Format("{0}|Method incoming parameters RecipientUserId={1}", thisMethod, notification.RecipientUserId);
            LogHelper.Info(logMessage);

            MSI_MessageNotificationRepository repository = new MSI_MessageNotificationRepository();
            try
            {
                repository.Add(notification);
            }

            catch (Exception ex)
            {
                ErrorLogHelper.Error(logMessage, ex);
            }
            return notification;
        }

        #endregion

        /// <summary>
        /// Return account information from vwAccounts based on either 'pims' account or 'original' account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="searchType">'pims' or 'origina'</param>
        /// <returns></returns>
        /// 
        public vwAccount GetAccount(string accountNumber, string searchType)
        {
            vwAccount account = null;
            vwAccountRepository data = null;

            try
            {
                data = new vwAccountRepository();
                if (searchType.ToLower() == "pims")
                    account = data.Get(record => record.ACCOUNT == accountNumber);
                else
                    account = data.Get(record => record.OriginalAccount == accountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetAccount for id=" + accountNumber + " and type = " + searchType + "\n Message:" + ex.Message);
            }
            return account;
        }

        public IEnumerable<vwAccount> GetAccounts(string nameLike)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<vwAccount> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetVWAccountNameSearch", new SqlParameter("@nameSearch", nameLike));
                data = new List<vwAccount>();
                vwAccount record;
                while (rdr.Read())
                {
                    record = new vwAccount();

                    record.FirstName = rdr["FirstName"].ToString();
                    record.LastName = rdr["LastName"].ToString();
                    record.NAME = rdr["NAME"].ToString();
                    record.ACCOUNT = rdr["ACCOUNT"].ToString();
                    record.OriginalAccount = rdr["OriginalAccount"].ToString();
                    if (!string.IsNullOrEmpty(rdr["OpenDate"].ToString()))
                        record.OpenDate = Convert.ToDateTime(rdr["OpenDate"].ToString());
                    if (!string.IsNullOrEmpty(rdr["ChargeOffDate"].ToString()))
                        record.ChargeOffDate = Convert.ToDateTime(rdr["ChargeOffDate"].ToString());
                    record.SSN = rdr["SSN"].ToString();

                    data.Add(record);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetSearchResults:" + ex.Message);
            }
            return data.AsQueryable<vwAccount>();
        }

        #region Export to Excel Methods
        public IEnumerable<DPSViewEditResult> GetDPSViewEditRecordsExport(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<DPSViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetDPSViewEditRecordsExport", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<DPSViewEditResult>();
                DPSViewEditResult record;
                while (rdr.Read())
                {
                    #region Data field Assignment
                    record = new DPSViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OriginalAcct = rdr["OriginalAcct"].ToString();
                    record.TransDate = Convert.ToDateTime(rdr["TransDate"]);
                    record.DateRec = Convert.ToDateTime(rdr["DateRec"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.CheckNumber = rdr["CheckNumber"].ToString();
                    record.PmtType = rdr["PmtType"].ToString();
                    record.TransSource = rdr["TransSource"].ToString();
                    record.CurrentResp = rdr["CurrentResp"].ToString();
                    record.OurCheckNumber = rdr["OurCheckNumber"].ToString();
                    record.Uploaded = rdr["Uploaded"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    record.NetPayment = rdr["NetPayment"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["NetPayment"]);
                    #endregion

                    #region Data Modification
                    if (record.Amount == Convert.ToDecimal(0.0))
                    {
                        record.Amount = null;
                    }
                    if (record.TransDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.TransDate = null;
                    }
                    if (record.DateRec.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateRec = null;
                    }
                    if (record.NetPayment == Convert.ToDecimal(0.0))
                    {
                        record.NetPayment = null;
                    }
                    if (record.Uploaded.ToUpper() == "TRUE")
                    {
                        record.Uploaded = "Yes";
                    }
                    else
                    {
                        record.Uploaded = "No";
                    }
                    #endregion
                    //Add record
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetDPSViewEditRecordsExport:" + ex.Message);
            }
            return data.AsEnumerable<DPSViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaViewEditRecordsExport(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaViewEditRecordsExport", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                    record.CODate = Convert.ToDateTime(rdr["CODate"]);
                    record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                    record.GUID = rdr["GUID"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.CompanyRequesting = rdr["CompanyRequesting"].ToString();
                    record.OriginalLender = rdr["OriginalLender"].ToString();
                    record.SSN = rdr["SSN"].ToString();
                    record.IsApplication = rdr["IsApplication"].ToString();
                    record.IsAffidavitIssuer = rdr["IsAffidavitIssuer"].ToString();
                    record.IsAffidavitSeller = rdr["IsAffidavitSeller"].ToString();
                    record.IsUnavailable = rdr["IsUnavailable"].ToString();
                    record.IsClosed = rdr["IsClosed"].ToString();
                    record.Explanation = rdr["Explanation"].ToString();
                    record.MediaTypeReceived = rdr["MediaTypeReceived"].ToString();
                    record.Notes = rdr["Notes"].ToString();
                    record.SellerInvoice = rdr["SellerInvoice"].ToString();
                    record.BuyerCheck = rdr["BuyerCheck"].ToString();
                    record.OurCheck = rdr["OurCheck"].ToString();
                    record.Seller = rdr["Seller"].ToString();
                    record.OrderNumber = rdr["OrderNumber"].ToString();
                    record.OurInvoice = rdr["OurInvoice"].ToString();
                    record.StmtsFrom = Convert.ToDateTime(rdr["StmtsFrom"]);
                    record.StmtsTo = Convert.ToDateTime(rdr["StmtsTo"]);
                    record.DateSubmitted = Convert.ToDateTime(rdr["DateSubmitted"]);
                    record.DateConfirmed = Convert.ToDateTime(rdr["DateConfirmed"]);
                    record.DateSellerPaid = Convert.ToDateTime(rdr["DateSellerPaid"]);
                    record.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                    record.UnavailableDate = Convert.ToDateTime(rdr["UnavailableDate"]);
                    record.DatePayRec = Convert.ToDateTime(rdr["DatePayRec"]);
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                    record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                    record.FileName = rdr["FileName"].ToString();

                    if (record.SellerFee == Convert.ToDecimal(0.0))
                    {
                        record.SellerFee = null;
                    }
                    if (record.OurFee == Convert.ToDecimal(0.0))
                    {
                        record.OurFee = null;
                    }
                    if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.OpenDate = null;
                    }
                    if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.CODate = null;
                    }
                    if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.OrderDate = null;
                    }

                    if (record.StmtsFrom.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.StmtsFrom = null;
                    }
                    if (record.StmtsTo.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.StmtsTo = null;
                    }
                    if (record.DateSubmitted.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateSubmitted = null;
                    }
                    if (record.DateConfirmed.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateConfirmed = null;
                    }
                    if (record.DateSellerPaid.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateSellerPaid = null;
                    }
                    if (record.DateReceived.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateReceived = null;
                    }
                    if (record.UnavailableDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.UnavailableDate = null;
                    }
                    if (record.DatePayRec.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DatePayRec = null;
                    }

                    if (record.IsApplication.ToUpper() == "TRUE")
                    {
                        record.IsApplication = "Yes";
                    }
                    else
                    {
                        record.IsApplication = "No";
                    }

                    if (record.IsAffidavitIssuer.ToUpper() == "TRUE")
                    {
                        record.IsAffidavitIssuer = "Yes";
                    }
                    else
                    {
                        record.IsAffidavitIssuer = "No";
                    }

                    if (record.IsAffidavitSeller.ToUpper() == "TRUE")
                    {
                        record.IsAffidavitSeller = "Yes";
                    }
                    else
                    {
                        record.IsAffidavitSeller = "No";
                    }

                    if (record.IsUnavailable.ToUpper() == "TRUE")
                    {
                        record.IsUnavailable = "Yes";
                    }
                    else
                    {
                        record.IsUnavailable = "No";
                    }

                    if (record.IsClosed.ToUpper() == "TRUE")
                    {
                        record.IsClosed = "Yes";
                    }
                    else
                    {
                        record.IsClosed = "No";
                    }

                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaViewEditRecordsExport:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetRecallViewEditRecordsExport(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallViewEditRecordsExport", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                    record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);

                    record.Date = Convert.ToDateTime(rdr["Date"]);
                    record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);

                    record.CurrentResp = rdr["CurrentResp"].ToString();
                    record.RecallReason = rdr["RecallReason"].ToString();
                    record.NewStatus = rdr["NewStatus"].ToString();
                    record.NewResp = rdr["NewResp"].ToString();
                    record.CheckNumber = rdr["CheckNumber"].ToString();
                    record.Explanation = rdr["Explanation"].ToString();
                    record.Seller = rdr["Seller"].ToString();
                    record.SellerCheck = rdr["SellerCheck"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Putback = rdr["Putback"].ToString();
                    record.InitiatedBy = rdr["RecallInitiatedBy"].ToString();
                    record.CheckDocuments = rdr["CheckDocuments"].ToString();

                    record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                    record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                    record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                    record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);

                    record.GUID = rdr["GUID"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }

                    if (record.Putback.ToUpper() == "TRUE")
                    {
                        record.Putback = "Yes";
                    }
                    else
                    {
                        record.Putback = "No";
                    }

                    if (record.CostBasis == Convert.ToDecimal(0.0))
                    {
                        record.CostBasis = null;
                    }
                    if (record.SalesBasis == Convert.ToDecimal(0.0))
                    {
                        record.SalesBasis = null;
                    }
                    if (record.AmtReceivable == Convert.ToDecimal(0.0))
                    {
                        record.AmtReceivable = null;
                    }
                    if (record.AmtPayable == Convert.ToDecimal(0.0))
                    {
                        record.AmtPayable = null;
                    }

                    if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateAcctClosed = null;
                    }
                    if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateNoteSent = null;
                    }
                    if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.Date = null;
                    }
                    if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.UploadedDate = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallViewEditRecordsExport:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<PutBackViewEditResult> GetPutBackViewEditRecordsExport(DateTime? StartDate, DateTime? EndDate, string PortfolioOwner, string Responsibility, string Account, string GUID)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<PutBackViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetPutBackViewEditRecordsExport", new SqlParameter("@StartDate", StartDate),
                    new SqlParameter("@EndDate", EndDate), new SqlParameter("@PortfolioOwner", PortfolioOwner),
                    new SqlParameter("@Responsibility", Responsibility), new SqlParameter("@Account", Account), new SqlParameter("@GUID", GUID));
                data = new List<PutBackViewEditResult>();
                PutBackViewEditResult record;
                while (rdr.Read())
                {
                    record = new PutBackViewEditResult();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                    record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);

                    record.Date = Convert.ToDateTime(rdr["Date"]);
                    record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);

                    record.CurrentResp = rdr["CurrentResp"].ToString();
                    record.PutBackReason = rdr["PutBackReason"].ToString();
                    record.NewStatus = rdr["NewStatus"].ToString();
                    record.NewResp = rdr["NewResp"].ToString();
                    record.CheckNumber = rdr["CheckNumber"].ToString();
                    record.Explanation = rdr["Explanation"].ToString();
                    record.Seller = rdr["Seller"].ToString();
                    record.SellerCheck = rdr["SellerCheck"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();

                    record.InitiatedBy = rdr["PutBackInitiatedBy"].ToString();
                    record.CheckDocuments = rdr["CheckDocuments"].ToString();

                    record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                    record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                    record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                    record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);

                    record.GUID = rdr["GUID"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }

                    if (record.CostBasis == Convert.ToDecimal(0.0))
                    {
                        record.CostBasis = null;
                    }
                    if (record.SalesBasis == Convert.ToDecimal(0.0))
                    {
                        record.SalesBasis = null;
                    }
                    if (record.AmtReceivable == Convert.ToDecimal(0.0))
                    {
                        record.AmtReceivable = null;
                    }
                    if (record.AmtPayable == Convert.ToDecimal(0.0))
                    {
                        record.AmtPayable = null;
                    }

                    if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateAcctClosed = null;
                    }
                    if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateNoteSent = null;
                    }
                    if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.Date = null;
                    }
                    if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.UploadedDate = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetPutBackViewEditRecordsExport:" + ex.Message);
            }
            return data.AsEnumerable<PutBackViewEditResult>();
        }
        #endregion


        #region For Reports
        public IEnumerable<RecallViewEditResult> GetRecallsUploadReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallsUploadReportData", new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.NewStatus = rdr["NewStatus"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.NewResp = rdr["NewResp"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.AcctName = rdr["AcctName"].ToString();
                        record.OrigAcct = rdr["OrigAcct"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        record.InitiatedBy = rdr["RecallInitiatedBy"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                        if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                        {
                            record.FaceValueofAcct = null;
                        }

                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallsUploadReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetAddSellerCheckReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetAddSellerCheckReportData", new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.NewResp = rdr["NewResp"].ToString();
                        record.NewStatus = rdr["NewStatus"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetAddSellerCheckReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetRecallsNotClosedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallsNotClosedReportData", new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.NewResp = rdr["NewResp"].ToString();
                        record.NewStatus = rdr["NewStatus"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallsNotClosedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetRecallsNoNoteSentReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallsNoNoteSentReportData", new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.NewResp = rdr["NewResp"].ToString();
                        record.NewStatus = rdr["NewStatus"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallsNoNoteSentReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<RecallViewEditResult> GetRecallsNotUploadedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetRecallsNotUploadedReportData", new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.NewResp = rdr["NewResp"].ToString();
                        record.NewStatus = rdr["NewStatus"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallsNotUploadedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaNotSubmittedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaNotSubmittedReportData", new SqlParameter("@Export", Export));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.OriginalLender = rdr["OriginalLender"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                        record.CODate = Convert.ToDateTime(rdr["CODate"]);
                        record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                        record.GUID = rdr["GUID"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.CompanyRequesting = rdr["CompanyRequesting"].ToString();
                        record.SSN = rdr["SSN"].ToString();
                        record.IsApplication = rdr["IsApplication"].ToString();
                        record.IsAffidavitIssuer = rdr["IsAffidavitIssuer"].ToString();
                        record.IsAffidavitSeller = rdr["IsAffidavitSeller"].ToString();
                        record.IsUnavailable = rdr["IsUnavailable"].ToString();
                        record.IsClosed = rdr["IsClosed"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.MediaTypeReceived = rdr["MediaTypeReceived"].ToString();
                        record.Notes = rdr["Notes"].ToString();
                        record.SellerInvoice = rdr["SellerInvoice"].ToString();
                        record.BuyerCheck = rdr["BuyerCheck"].ToString();
                        record.OurCheck = rdr["OurCheck"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.OrderNumber = rdr["OrderNumber"].ToString();
                        record.OurInvoice = rdr["OurInvoice"].ToString();
                        record.StmtsFrom = Convert.ToDateTime(rdr["StmtsFrom"]);
                        record.StmtsTo = Convert.ToDateTime(rdr["StmtsTo"]);
                        record.DateSubmitted = Convert.ToDateTime(rdr["DateSubmitted"]);
                        record.DateConfirmed = Convert.ToDateTime(rdr["DateConfirmed"]);
                        record.DateSellerPaid = Convert.ToDateTime(rdr["DateSellerPaid"]);
                        record.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        record.UnavailableDate = Convert.ToDateTime(rdr["UnavailableDate"]);
                        record.DatePayRec = Convert.ToDateTime(rdr["DatePayRec"]);
                        record.ID = Convert.ToInt32(rdr["ID"].ToString());
                        record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                        record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                        if (record.SellerFee == Convert.ToDecimal(0.0))
                        {
                            record.SellerFee = null;
                        }
                        if (record.OurFee == Convert.ToDecimal(0.0))
                        {
                            record.OurFee = null;
                        }
                        if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OpenDate = null;
                        }
                        if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.CODate = null;
                        }
                        if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OrderDate = null;
                        }

                        if (record.StmtsFrom.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsFrom = null;
                        }
                        if (record.StmtsTo.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsTo = null;
                        }
                        if (record.DateSubmitted.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSubmitted = null;
                        }
                        if (record.DateConfirmed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateConfirmed = null;
                        }
                        if (record.DateSellerPaid.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSellerPaid = null;
                        }
                        if (record.DateReceived.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateReceived = null;
                        }
                        if (record.UnavailableDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UnavailableDate = null;
                        }
                        if (record.DatePayRec.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DatePayRec = null;
                        }

                        if (record.IsApplication.ToUpper() == "TRUE")
                        {
                            record.IsApplication = "Yes";
                        }
                        else
                        {
                            record.IsApplication = "No";
                        }

                        if (record.IsAffidavitIssuer.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitIssuer = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitIssuer = "No";
                        }

                        if (record.IsAffidavitSeller.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitSeller = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitSeller = "No";
                        }

                        if (record.IsUnavailable.ToUpper() == "TRUE")
                        {
                            record.IsUnavailable = "Yes";
                        }
                        else
                        {
                            record.IsUnavailable = "No";
                        }

                        if (record.IsClosed.ToUpper() == "TRUE")
                        {
                            record.IsClosed = "Yes";
                        }
                        else
                        {
                            record.IsClosed = "No";
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaNotSubmittedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaNotReceivedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaNotReceivedReportData", new SqlParameter("@Export", Export));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.OriginalLender = rdr["OriginalLender"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                        record.CODate = Convert.ToDateTime(rdr["CODate"]);
                        record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                        record.GUID = rdr["GUID"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.CompanyRequesting = rdr["CompanyRequesting"].ToString();
                        record.SSN = rdr["SSN"].ToString();
                        record.IsApplication = rdr["IsApplication"].ToString();
                        record.IsAffidavitIssuer = rdr["IsAffidavitIssuer"].ToString();
                        record.IsAffidavitSeller = rdr["IsAffidavitSeller"].ToString();
                        record.IsUnavailable = rdr["IsUnavailable"].ToString();
                        record.IsClosed = rdr["IsClosed"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.MediaTypeReceived = rdr["MediaTypeReceived"].ToString();
                        record.Notes = rdr["Notes"].ToString();
                        record.SellerInvoice = rdr["SellerInvoice"].ToString();
                        record.BuyerCheck = rdr["BuyerCheck"].ToString();
                        record.OurCheck = rdr["OurCheck"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.OrderNumber = rdr["OrderNumber"].ToString();
                        record.OurInvoice = rdr["OurInvoice"].ToString();
                        record.StmtsFrom = Convert.ToDateTime(rdr["StmtsFrom"]);
                        record.StmtsTo = Convert.ToDateTime(rdr["StmtsTo"]);
                        record.DateSubmitted = Convert.ToDateTime(rdr["DateSubmitted"]);
                        record.DateConfirmed = Convert.ToDateTime(rdr["DateConfirmed"]);
                        record.DateSellerPaid = Convert.ToDateTime(rdr["DateSellerPaid"]);
                        record.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        record.UnavailableDate = Convert.ToDateTime(rdr["UnavailableDate"]);
                        record.DatePayRec = Convert.ToDateTime(rdr["DatePayRec"]);
                        record.ID = Convert.ToInt32(rdr["ID"].ToString());
                        record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                        record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                        if (record.SellerFee == Convert.ToDecimal(0.0))
                        {
                            record.SellerFee = null;
                        }
                        if (record.OurFee == Convert.ToDecimal(0.0))
                        {
                            record.OurFee = null;
                        }
                        if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OpenDate = null;
                        }
                        if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.CODate = null;
                        }
                        if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OrderDate = null;
                        }

                        if (record.StmtsFrom.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsFrom = null;
                        }
                        if (record.StmtsTo.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsTo = null;
                        }
                        if (record.DateSubmitted.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSubmitted = null;
                        }
                        if (record.DateConfirmed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateConfirmed = null;
                        }
                        if (record.DateSellerPaid.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSellerPaid = null;
                        }
                        if (record.DateReceived.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateReceived = null;
                        }
                        if (record.UnavailableDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UnavailableDate = null;
                        }
                        if (record.DatePayRec.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DatePayRec = null;
                        }

                        if (record.IsApplication.ToUpper() == "TRUE")
                        {
                            record.IsApplication = "Yes";
                        }
                        else
                        {
                            record.IsApplication = "No";
                        }

                        if (record.IsAffidavitIssuer.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitIssuer = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitIssuer = "No";
                        }

                        if (record.IsAffidavitSeller.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitSeller = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitSeller = "No";
                        }

                        if (record.IsUnavailable.ToUpper() == "TRUE")
                        {
                            record.IsUnavailable = "Yes";
                        }
                        else
                        {
                            record.IsUnavailable = "No";
                        }

                        if (record.IsClosed.ToUpper() == "TRUE")
                        {
                            record.IsClosed = "Yes";
                        }
                        else
                        {
                            record.IsClosed = "No";
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaNotReceivedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaNotForwardedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaNotForwardedReportData", new SqlParameter("@Export", Export));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.OriginalLender = rdr["OriginalLender"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                        record.CODate = Convert.ToDateTime(rdr["CODate"]);
                        record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                        record.GUID = rdr["GUID"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.CompanyRequesting = rdr["CompanyRequesting"].ToString();
                        record.SSN = rdr["SSN"].ToString();
                        record.IsApplication = rdr["IsApplication"].ToString();
                        record.IsAffidavitIssuer = rdr["IsAffidavitIssuer"].ToString();
                        record.IsAffidavitSeller = rdr["IsAffidavitSeller"].ToString();
                        record.IsUnavailable = rdr["IsUnavailable"].ToString();
                        record.IsClosed = rdr["IsClosed"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.MediaTypeReceived = rdr["MediaTypeReceived"].ToString();
                        record.Notes = rdr["Notes"].ToString();
                        record.SellerInvoice = rdr["SellerInvoice"].ToString();
                        record.BuyerCheck = rdr["BuyerCheck"].ToString();
                        record.OurCheck = rdr["OurCheck"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.OrderNumber = rdr["OrderNumber"].ToString();
                        record.OurInvoice = rdr["OurInvoice"].ToString();
                        record.StmtsFrom = Convert.ToDateTime(rdr["StmtsFrom"]);
                        record.StmtsTo = Convert.ToDateTime(rdr["StmtsTo"]);
                        record.DateSubmitted = Convert.ToDateTime(rdr["DateSubmitted"]);
                        record.DateConfirmed = Convert.ToDateTime(rdr["DateConfirmed"]);
                        record.DateSellerPaid = Convert.ToDateTime(rdr["DateSellerPaid"]);
                        record.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        record.UnavailableDate = Convert.ToDateTime(rdr["UnavailableDate"]);
                        record.DatePayRec = Convert.ToDateTime(rdr["DatePayRec"]);
                        record.ID = Convert.ToInt32(rdr["ID"].ToString());
                        record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                        record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                        if (record.SellerFee == Convert.ToDecimal(0.0))
                        {
                            record.SellerFee = null;
                        }
                        if (record.OurFee == Convert.ToDecimal(0.0))
                        {
                            record.OurFee = null;
                        }
                        if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OpenDate = null;
                        }
                        if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.CODate = null;
                        }
                        if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OrderDate = null;
                        }

                        if (record.StmtsFrom.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsFrom = null;
                        }
                        if (record.StmtsTo.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsTo = null;
                        }
                        if (record.DateSubmitted.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSubmitted = null;
                        }
                        if (record.DateConfirmed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateConfirmed = null;
                        }
                        if (record.DateSellerPaid.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSellerPaid = null;
                        }
                        if (record.DateReceived.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateReceived = null;
                        }
                        if (record.UnavailableDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UnavailableDate = null;
                        }
                        if (record.DatePayRec.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DatePayRec = null;
                        }

                        if (record.IsApplication.ToUpper() == "TRUE")
                        {
                            record.IsApplication = "Yes";
                        }
                        else
                        {
                            record.IsApplication = "No";
                        }

                        if (record.IsAffidavitIssuer.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitIssuer = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitIssuer = "No";
                        }

                        if (record.IsAffidavitSeller.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitSeller = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitSeller = "No";
                        }

                        if (record.IsUnavailable.ToUpper() == "TRUE")
                        {
                            record.IsUnavailable = "Yes";
                        }
                        else
                        {
                            record.IsUnavailable = "No";
                        }

                        if (record.IsClosed.ToUpper() == "TRUE")
                        {
                            record.IsClosed = "Yes";
                        }
                        else
                        {
                            record.IsClosed = "No";
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaNotForwardedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        public IEnumerable<MediaViewEditResult> GetMediaNotConfirmedReportRecords(string Export)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<MediaViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                rdr = db.ExecuteReader("MSI_spGetMediaNotConfirmedReportData", new SqlParameter("@Export", Export));
                data = new List<MediaViewEditResult>();
                MediaViewEditResult record;
                while (rdr.Read())
                {
                    record = new MediaViewEditResult();
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.OriginalLender = rdr["OriginalLender"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    if (Export != null)
                    {
                        record.OpenDate = Convert.ToDateTime(rdr["OpenDate"]);
                        record.CODate = Convert.ToDateTime(rdr["CODate"]);
                        record.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                        record.GUID = rdr["GUID"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.CompanyRequesting = rdr["CompanyRequesting"].ToString();
                        record.SSN = rdr["SSN"].ToString();
                        record.IsApplication = rdr["IsApplication"].ToString();
                        record.IsAffidavitIssuer = rdr["IsAffidavitIssuer"].ToString();
                        record.IsAffidavitSeller = rdr["IsAffidavitSeller"].ToString();
                        record.IsUnavailable = rdr["IsUnavailable"].ToString();
                        record.IsClosed = rdr["IsClosed"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.MediaTypeReceived = rdr["MediaTypeReceived"].ToString();
                        record.Notes = rdr["Notes"].ToString();
                        record.SellerInvoice = rdr["SellerInvoice"].ToString();
                        record.BuyerCheck = rdr["BuyerCheck"].ToString();
                        record.OurCheck = rdr["OurCheck"].ToString();
                        record.Seller = rdr["Seller"].ToString();
                        record.OrderNumber = rdr["OrderNumber"].ToString();
                        record.OurInvoice = rdr["OurInvoice"].ToString();
                        record.StmtsFrom = Convert.ToDateTime(rdr["StmtsFrom"]);
                        record.StmtsTo = Convert.ToDateTime(rdr["StmtsTo"]);
                        record.DateSubmitted = Convert.ToDateTime(rdr["DateSubmitted"]);
                        record.DateConfirmed = Convert.ToDateTime(rdr["DateConfirmed"]);
                        record.DateSellerPaid = Convert.ToDateTime(rdr["DateSellerPaid"]);
                        record.DateReceived = Convert.ToDateTime(rdr["DateReceived"]);
                        record.UnavailableDate = Convert.ToDateTime(rdr["UnavailableDate"]);
                        record.DatePayRec = Convert.ToDateTime(rdr["DatePayRec"]);
                        record.ID = Convert.ToInt32(rdr["ID"].ToString());
                        record.SellerFee = rdr["SellerFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SellerFee"]);
                        record.OurFee = rdr["OurFee"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["OurFee"]);
                        if (record.SellerFee == Convert.ToDecimal(0.0))
                        {
                            record.SellerFee = null;
                        }
                        if (record.OurFee == Convert.ToDecimal(0.0))
                        {
                            record.OurFee = null;
                        }
                        if (record.OpenDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OpenDate = null;
                        }
                        if (record.CODate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.CODate = null;
                        }
                        if (record.OrderDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.OrderDate = null;
                        }

                        if (record.StmtsFrom.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsFrom = null;
                        }
                        if (record.StmtsTo.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.StmtsTo = null;
                        }
                        if (record.DateSubmitted.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSubmitted = null;
                        }
                        if (record.DateConfirmed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateConfirmed = null;
                        }
                        if (record.DateSellerPaid.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateSellerPaid = null;
                        }
                        if (record.DateReceived.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateReceived = null;
                        }
                        if (record.UnavailableDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UnavailableDate = null;
                        }
                        if (record.DatePayRec.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DatePayRec = null;
                        }

                        if (record.IsApplication.ToUpper() == "TRUE")
                        {
                            record.IsApplication = "Yes";
                        }
                        else
                        {
                            record.IsApplication = "No";
                        }

                        if (record.IsAffidavitIssuer.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitIssuer = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitIssuer = "No";
                        }

                        if (record.IsAffidavitSeller.ToUpper() == "TRUE")
                        {
                            record.IsAffidavitSeller = "Yes";
                        }
                        else
                        {
                            record.IsAffidavitSeller = "No";
                        }

                        if (record.IsUnavailable.ToUpper() == "TRUE")
                        {
                            record.IsUnavailable = "Yes";
                        }
                        else
                        {
                            record.IsUnavailable = "No";
                        }

                        if (record.IsClosed.ToUpper() == "TRUE")
                        {
                            record.IsClosed = "Yes";
                        }
                        else
                        {
                            record.IsClosed = "No";
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetMediaNotConfirmedReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<MediaViewEditResult>();
        }

        #endregion

        public IEnumerable<RecallViewEditResult> GetRecallsReceivableReportRecords(string Export, string reportName)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<RecallViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                string reportProcedureName = "";

                switch (reportName)
                {
                    case "RecallsReceivable": reportProcedureName = "MSI_spGetRecallsReceivableReportData";
                        break;
                    case "RecallsInvoiceLookup": reportProcedureName = "MSI_spGetRecallsInvoiceLookupReportData";
                        break;
                    case "RecallsSellerCheckLookup": reportProcedureName = "MSI_spGetRecallsSellerCheckLookupReportData";
                        break;
                    case "RecallsPayable": reportProcedureName = "MSI_spGetRecallsPayableReportData";
                        break;
                    case "RecallsPaidByOurCheck": reportProcedureName = "MSI_spGetRecallsPaidByOurCheckReportData";
                        break;

                }
                rdr = db.ExecuteReader(reportProcedureName, new SqlParameter("@Export", Export));
                data = new List<RecallViewEditResult>();
                RecallViewEditResult record;
                while (rdr.Read())
                {
                    record = new RecallViewEditResult();
                    record.TotalAmount = rdr["TotalAmount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["TotalAmount"]);
                    if (record.TotalAmount == Convert.ToDecimal(0.0))
                    {
                        record.TotalAmount = null;
                    }
                    record.OrigAcct = rdr["OrigAcct"].ToString();
                    record.Date = Convert.ToDateTime(rdr["Date"]);
                    record.AcctName = rdr["AcctName"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                    if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                    {
                        record.FaceValueofAcct = null;
                    }
                    record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                    record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                    record.Invoice = rdr["Invoice"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.SellerCheck = rdr["SellerCheck"].ToString();
                    record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                    record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                    record.CheckNumber = rdr["CheckNumber"].ToString();
                    if (Export != null)
                    {
                        record.OrigAcct = rdr["OrigAcct"].ToString();
                        record.Date = Convert.ToDateTime(rdr["Date"]);
                        record.AcctName = rdr["AcctName"].ToString();
                        record.Portfolio = rdr["Portfolio"].ToString();
                        record.PIMSAcct = rdr["PIMSAcct"].ToString();
                        record.FaceValueofAcct = rdr["FaceValueofAcct"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["FaceValueofAcct"]);
                        if (record.FaceValueofAcct == Convert.ToDecimal(0.0))
                        {
                            record.FaceValueofAcct = null;
                        }
                        record.CostBasis = rdr["CostBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["CostBasis"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.Invoice = rdr["Invoice"].ToString();
                        record.CurrentResp = rdr["CurrentResp"].ToString();
                        record.RecallReason = rdr["RecallReason"].ToString();
                        record.DateNoteSent = Convert.ToDateTime(rdr["DateNoteSent"]);
                        record.NewStatus = rdr["NewStatus"].ToString();
                        record.NewResp = rdr["NewResp"].ToString();
                        record.UploadedDate = Convert.ToDateTime(rdr["UploadedDate"]);
                        record.CheckNumber = rdr["CheckNumber"].ToString();
                        record.Explanation = rdr["Explanation"].ToString();
                        record.SalesBasis = rdr["SalesBasis"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["SalesBasis"]);
                        record.Seller = rdr["Seller"].ToString();
                        record.SellerCheck = rdr["SellerCheck"].ToString();
                        record.Putback = rdr["Putback"].ToString();
                        record.DateAcctClosed = Convert.ToDateTime(rdr["DateAcctClosed"]);
                        record.AmtReceivable = rdr["AmtReceivable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtReceivable"]);
                        record.AmtPayable = rdr["AmtPayable"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["AmtPayable"]);
                        record.GUID = rdr["GUID"].ToString();
                        if (record.Putback.ToUpper() == "TRUE")
                        {
                            record.Putback = "Yes";
                        }
                        else
                        {
                            record.Putback = "No";
                        }

                        if (record.CostBasis == Convert.ToDecimal(0.0))
                        {
                            record.CostBasis = null;
                        }
                        if (record.SalesBasis == Convert.ToDecimal(0.0))
                        {
                            record.SalesBasis = null;
                        }
                        if (record.AmtReceivable == Convert.ToDecimal(0.0))
                        {
                            record.AmtReceivable = null;
                        }
                        if (record.AmtPayable == Convert.ToDecimal(0.0))
                        {
                            record.AmtPayable = null;
                        }

                        if (record.DateAcctClosed.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateAcctClosed = null;
                        }
                        if (record.DateNoteSent.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.DateNoteSent = null;
                        }
                        if (record.Date.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.Date = null;
                        }
                        if (record.UploadedDate.ToString() == "1/1/1900 12:00:00 AM")
                        {
                            record.UploadedDate = null;
                        }
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetRecallsReceivableReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<RecallViewEditResult>();
        }

        public IEnumerable<DPSViewEditResult> GetDPSReportRecords(string Export, string reportName)
        {
            DBFactory db;
            SqlDataReader rdr;
            List<DPSViewEditResult> data = null;
            try
            {
                db = new DBFactory();
                string reportProcedureName = "";

                switch (reportName)
                {
                    case "DPSCheckDetail": reportProcedureName = "MSI_spGetDPSCheckDetailReportData";
                        break;
                    case "DPSPayable": reportProcedureName = "MSI_spGetDPSPayableReportData";
                        break;
                    case "DPSPaidByOurCheck": reportProcedureName = "MSI_spGetDPSPaidByOurCheckReportData";
                        break;

                }
                rdr = db.ExecuteReader(reportProcedureName, new SqlParameter("@Export", Export));

                data = new List<DPSViewEditResult>();
                DPSViewEditResult record;
                while (rdr.Read())
                {
                    record = new DPSViewEditResult();
                    //record.OriginalAcct = rdr["OriginalAcct"].ToString();
                    //record.TransDate = Convert.ToDateTime(rdr["TransDate"]);
                    //record.AcctName = rdr["AcctName"].ToString();
                    //record.Portfolio = rdr["Portfolio"].ToString();
                    //record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    //record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    //record.NetPayment = rdr["NetPayment"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["NetPayment"]);
                    //record.PmtType = rdr["PmtType"].ToString();
                    //record.CurrentResp = rdr["CurrentResp"].ToString();
                    //record.GUID = rdr["GUID"].ToString();

                    //record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    //if (Export != null)
                    //{
                    record.AcctName = rdr["AcctName"].ToString();
                    record.PIMSAcct = rdr["PIMSAcct"].ToString();
                    record.OriginalAcct = rdr["OriginalAcct"].ToString();
                    record.TransDate = Convert.ToDateTime(rdr["TransDate"]);
                    record.DateRec = Convert.ToDateTime(rdr["DateRec"]);
                    record.TransCode = rdr["TransCode"].ToString();
                    record.GUID = rdr["GUID"].ToString();
                    record.CheckNumber = rdr["CheckNumber"].ToString();
                    record.PmtType = rdr["PmtType"].ToString();
                    record.TransSource = rdr["TransSource"].ToString();
                    record.CurrentResp = rdr["CurrentResp"].ToString();
                    record.OurCheckNumber = rdr["OurCheckNumber"].ToString();
                    record.Uploaded = rdr["Uploaded"].ToString();
                    record.Portfolio = rdr["Portfolio"].ToString();
                    record.ID = Convert.ToInt32(rdr["ID"].ToString());
                    record.Amount = rdr["Amount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["Amount"]);
                    record.NetPayment = rdr["NetPayment"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["NetPayment"]);

                    record.TotalAmount = rdr["TotalAmount"] == DBNull.Value ? Convert.ToDecimal(0.0) : Convert.ToDecimal(rdr["TotalAmount"]);
                    if (record.TotalAmount == Convert.ToDecimal(0.0))
                    {
                        record.TotalAmount = null;
                    }

                    //}
                    if (record.Amount == Convert.ToDecimal(0.0))
                    {
                        record.Amount = null;
                    }
                    if (record.NetPayment == Convert.ToDecimal(0.0))
                    {
                        record.NetPayment = null;
                    }
                    if (record.TransDate.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.TransDate = null;
                    }
                    if (record.DateRec.ToString() == "1/1/1900 12:00:00 AM")
                    {
                        record.DateRec = null;
                    }
                    data.Add(record);
                }
                //Close the datareader
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in DataQueries.GetDPSReportRecords:" + ex.Message);
            }
            return data.AsEnumerable<DPSViewEditResult>();
        }

    }

}
