using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cascade.Data.Models;
using Cascade.Data.Repositories;
using Cascade.Helpers;
using System.Configuration;
namespace Cascade.Charts
{
    public static class ChartHelper
    {


    }

    public interface IChart
    {
        void LoadChart();
        string CreateCategories();
        string CreateDataSets();
        void CreateChart();
    }

    public enum ChartID
    {
        MediaPayble,
        MediaPaidByCheck,
        NotSubmittedNodateSubmit,
        NotSubmittedNodateConfirm,
        WorkStatusBreakdown,
        PortfolioOwnerBreakdown
    }

    public enum ChartTypes
    {
        Chart,
        PieChart,
        BarChart
    }

    public enum ChartSubType
    {

    }

    public class Chart : IChart
    {
        public string Caption { get; set; }
        public string NumberSuffix { get; set; }
        public string BGColor { get; set; }
        public string BGAlpha { get; set; }
        public string CanvasBGColor { get; set; }
        public string CanvasBGAlpha { get; set; }
        public string XaxisName { get; set; }
        public string YaxisName { get; set; }
        public string DrillChartIds { get; set; }
        public string DrillBy { get; set; }
        public List<Category> Categories { get; set; }
        public List<ChartDataSet> DataSetCollection { get; set; }
        public bool DrillOverride { get; set; }

        public string SWF { get; set; }
        public string ChartXML { get; set; }
        public ChartID Id { get; set; }
        public string Hight { get; set; }
        public string Width { get; set; }
        public string PieRadius { get; set; }
        public string showLabels { get; set; }
        public string enableRotation { get; set; }
        public string showLegend { get; set; }
        public string[] SearchParameters { get; set; }
        public Chart()
        {
            this.Categories = new List<Category>();
            this.DataSetCollection = new List<ChartDataSet>();
            this.DrillOverride = true;
        }


        public void LoadChart()
        {
            try
            {

                List<ChartParameter> chartParams = null;
                ChartDataSet lastDs = null;
                switch ((ChartID)Enum.Parse(typeof(ChartID), this.Id.ToString(), true))
                {
                    case ChartID.MediaPayble:
                        this.Categories.Add(new Category { Label = "Media Type1" });
                        this.Categories.Add(new Category { Label = "Media Type2" });
                        this.Categories.Add(new Category { Label = "Media Type3" });
                        this.Categories.Add(new Category { Label = "Media Type4" });
                        this.Categories.Add(new Category { Label = "Media Type5" });
                        this.Categories.Add(new Category { Label = "Media Type6" });

                        chartParams = new List<ChartParameter>();
                        chartParams.Add(new ChartParameter { Value = "-2", Color = "8B4513" });//#8B4513 Saddle Brown
                        chartParams.Add(new ChartParameter { Value = "-1", Color = "0000FF" });//#0000FF Blue
                        chartParams.Add(new ChartParameter { Value = "0", Color = "FFA07A" });//#FFA07A Ligh Salmon

                        string[] values = "20,25,15,22,20,11,13,15,15,23,26,25,12,22,18,20,23,26".Split(new char[] { ',' });
                        int index = 0;
                        foreach (ChartParameter parameter in chartParams)
                        {
                            try
                            {
                                this.DataSetCollection.Add(new ChartDataSet { Color = parameter.Color, SeriesName = DateTime.Now.AddMonths(int.Parse(parameter.Value)).ToString("MMM") });

                                foreach (Category category in this.Categories)
                                {
                                    lastDs = this.DataSetCollection.Last();
                                    lastDs.SetsCollection.Add(new SetValue { Label = category.Label });
                                }

                                do
                                {
                                    foreach (SetValue set in lastDs.SetsCollection)
                                    {
                                        set.Value = values[index].ToString();
                                        index++;
                                    }
                                } while (index % 6 != 0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error in ChartID.MediaPayble:" + ex.Message);
                            }
                            //}
                        }
                        break;
                    case ChartID.MediaPaidByCheck:
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(-2).ToString("MMM") });
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(-1).ToString("MMM") });
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(0).ToString("MMM") });

                        chartParams = new List<ChartParameter>();
                        chartParams.Add(new ChartParameter { Value = "Media Type 1", Color = "2908AD" });
                        chartParams.Add(new ChartParameter { Value = "Media Type 2", Color = "B54908" });

                        values = "30,20,35,30,45,50".Split(new char[] { ',' });
                        index = 0;
                        foreach (ChartParameter parameter in chartParams)
                        {
                            try
                            {
                                this.DataSetCollection.Add(new ChartDataSet { Color = parameter.Color, SeriesName = parameter.Value });

                                foreach (Category category in this.Categories)
                                {
                                    lastDs = this.DataSetCollection.Last();
                                    lastDs.SetsCollection.Add(new SetValue { Label = category.Label });
                                }

                                do
                                {
                                    foreach (SetValue set in lastDs.SetsCollection)
                                    {
                                        set.Value = values[index].ToString();
                                        index++;
                                    }
                                } while (index % 3 != 0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error in ChartID.MediaPaidByCheck:" + ex.Message);
                            }
                            //}
                        }
                        break;
                    case ChartID.NotSubmittedNodateSubmit:
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(-2).ToString("MMM") });
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(-1).ToString("MMM") });
                        this.Categories.Add(new Category { Label = DateTime.Now.AddMonths(0).ToString("MMM") });

                        chartParams = new List<ChartParameter>();
                        chartParams.Add(new ChartParameter { Value = "Seller1", Color = "2908AD" });
                        chartParams.Add(new ChartParameter { Value = "Seller2", Color = "F0E68C" });

                        values = "30,35,40,35,45,50".Split(new char[] { ',' });
                        index = 0;
                        foreach (ChartParameter parameter in chartParams)
                        {
                            try
                            {
                                this.DataSetCollection.Add(new ChartDataSet { Color = parameter.Color, SeriesName = parameter.Value });

                                foreach (Category category in this.Categories)
                                {
                                    lastDs = this.DataSetCollection.Last();
                                    lastDs.SetsCollection.Add(new SetValue { Label = category.Label });
                                }

                                do
                                {
                                    foreach (SetValue set in lastDs.SetsCollection)
                                    {
                                        set.Value = values[index].ToString();
                                        index++;
                                    }
                                } while (index % 3 != 0);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error in ChartID.MediaPaidByCheck:" + ex.Message);
                            }
                            //}
                        }
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateCategories()
        {
            StringBuilder categoriesXML = null;
            try
            {
                categoriesXML = new StringBuilder("<categories>");
                foreach (Category category in this.Categories)
                {
                    categoriesXML.Append("<category");
                    categoriesXML.Append((!(string.IsNullOrEmpty(category.Label)) ? " label='" + category.Label + "'" : ""));
                    categoriesXML.Append("/>");
                }
                categoriesXML.Append("</categories>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categoriesXML.ToString();
        }

        public string CreateDataSets()
        {
            StringBuilder datasetXML = null;
            try
            {
                datasetXML = new StringBuilder();

                foreach (ChartDataSet ds in this.DataSetCollection)
                {
                    datasetXML.Append("<dataset" + (!(string.IsNullOrEmpty(ds.SeriesName)) ? " seriesName='" + ds.SeriesName + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(ds.Color)) ? " color='" + ds.Color + "'" : ""));
                    datasetXML.Append(">");

                    foreach (SetValue set in ds.SetsCollection)
                    {
                        datasetXML.Append("<set");
                        datasetXML.Append((!(string.IsNullOrEmpty(set.Value)) ? " value='" + set.Value + "'" : ""));
                        datasetXML.Append((!(string.IsNullOrEmpty(set.Color)) ? " color='" + set.Color + "'" : ""));
                        datasetXML.Append((!(string.IsNullOrEmpty(set.Link)) ? " link='" + set.Link + "'" : ""));
                        datasetXML.Append("/>");
                    }
                    datasetXML.Append("</dataset>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datasetXML.ToString();
        }

        public void CreateChart()
        {
            StringBuilder chartXML = null;
            try
            {
                chartXML = new StringBuilder("<chart" + (!(string.IsNullOrEmpty(this.Caption)) ? " caption='" + this.Caption + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGColor)) ? " bgColor='" + this.BGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGAlpha)) ? " bgAlpha='" + this.BGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGColor)) ? " canvasBgColor='" + this.CanvasBGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGAlpha)) ? " canvasBgAlpha='" + this.CanvasBGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.XaxisName)) ? " xAxisName='" + this.XaxisName + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.YaxisName)) ? " yAxisName='" + this.YaxisName + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.NumberSuffix)) ? " numberSuffix='" + this.NumberSuffix + "'" : ""));
                chartXML.Append(">");

                chartXML.Append(CreateCategories());

                chartXML.Append(CreateDataSets());

                chartXML.Append("</chart>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ChartXML = chartXML.ToString();
        }

    }

    public class BarChart : Chart
    {
        public List<SetValue> SetsCollection;

        public BarChart()
            : base()
        {
            SetsCollection = new List<SetValue>();
        }

        public new void LoadChart()
        {
            try
            {
                switch ((ChartID)Enum.Parse(typeof(ChartID), this.Id.ToString(), true))
                {
                    case ChartID.NotSubmittedNodateConfirm:
                        try
                        {
                            string[] colorCodes = "087152,6B45AD,A58E42,08657B,0000FF".Split(new char[] { ',' });
                            string[] values = "30,35,40,35,45".Split(new char[] { ',' });
                            int index = 0;
                            var sellers = new string[] { "Seller 1", "Seller 2", "Seller 3", "Seller 4", "Seller 5" };

                            foreach (var record in sellers)
                            {
                                try
                                {
                                    this.SetsCollection.Add(new SetValue { Color = colorCodes[index].ToString(), Label = record.ToString(), Value = values[index].ToString() });
                                }
                                catch (System.InvalidOperationException)
                                {

                                }
                                index++;
                            }
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("Error in ChartID.NotSubmittedNodateConfirm:" + ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new string CreateDataSets()
        {
            StringBuilder datasetXML = null;
            try
            {
                datasetXML = new StringBuilder();

                foreach (SetValue set in SetsCollection)
                {
                    datasetXML.Append("<set");
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Label)) ? " label='" + set.Label + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Value)) ? " value='" + set.Value + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Color)) ? " color='" + set.Color + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Link)) ? " link='" + set.Link + "'" : ""));
                    datasetXML.Append("/>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datasetXML.ToString();
        }

        public new void CreateChart()
        {
            StringBuilder chartXML = null;
            try
            {
                chartXML = new StringBuilder("<chart" + (!(string.IsNullOrEmpty(this.Caption)) ? " caption='" + this.Caption + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGColor)) ? " bgColor='" + this.BGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGAlpha)) ? " bgAlpha='" + this.BGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGColor)) ? " canvasBgColor='" + this.CanvasBGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGAlpha)) ? " canvasBgAlpha='" + this.CanvasBGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.XaxisName)) ? " xAxisName='" + this.XaxisName + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.YaxisName)) ? " yAxisName='" + this.YaxisName + "'" : ""));
                chartXML.Append(">");

                chartXML.Append(CreateDataSets());

                chartXML.Append("</chart>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ChartXML = chartXML.ToString();
        }
    }

    public class PieChart : BarChart
    {
        public List<SetValue> SetsCollection;

        public PieChart()
            : base()
        {
            SetsCollection = new List<SetValue>();
        }
        public new string CreateDataSets()
        {
            StringBuilder datasetXML = null;
            try
            {
                datasetXML = new StringBuilder();

                foreach (SetValue set in SetsCollection)
                {
                    datasetXML.Append("<set");
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Label)) ? " label='" + set.Label + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Value)) ? " value='" + set.Value + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Color)) ? " color='" + set.Color + "'" : ""));
                    datasetXML.Append((!(string.IsNullOrEmpty(set.Link)) ? " link='" + set.Link + "'" : ""));
                    datasetXML.Append("/>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datasetXML.ToString();
        }

        public new void LoadChart()
        {
            DataQueries queries = null;
            //List<string> colorCodes = ColorCodeHelper.GetColorCodes();
            int index = 0;
            string[] colors = ConfigurationManager.AppSettings["colorCodes"].Split(new char[] { '#' });
            try
            {
                queries = new DataQueries();
                switch (this.Id)
                {
                    case ChartID.WorkStatusBreakdown:
                        //colors = new string[] { 
                        //    "CC6600", 
                        //    "9900CC", 
                        //    "FF3300", 
                        //    "0099FF", 
                        //    "00CC66", 
                        //    "FFFF00", 
                        //    "CC6600", 
                        //    "9900CC",
                        //    "800000",
                        //    "A52A2A",
                        //    "DC143c",
                        //    "FF6347",
                        //    "CD5C5C",
                        //    "F08080",
                        //    "FFA500",
                        //    "FFD700",
                        //    "B8860B",
                        //    "9ACD32",
                        //    "556B2F",
                        //    "00FF00",
                        //    "32CD32",
                        //    "90EE90",
                        //    "00FA9A",
                        //    "2E8B57",
                        //    "40E0D0",
                        //    "6495ED",
                        //    "1E90FF",
                        //    "191970",
                        //    "00008B",
                        //    "8A2BE2",
                        //    "8B008B",
                        //    "BA55D3",
                        //    "FF00FF",
                        //    "DA70D6",
                        //    "FF1493",
                        //    "F5F5DC",
                        //    "F5DEB3",
                        //    "8B4513",
                        //    "D2691E",
                        //    "D2B48C",
                        //    "708090",
                        //    "F5DEB3",
                        //    "BC8F8F",
                        //    "696969",
                        //    "808000",
                        //    "800080",
                        //    "008080",
                        //    "000080",
                        //    "FF0000",
                        //    "000000",
                        //    "FFFFFF"
                        //};
                        IEnumerable<PortfolioPieRpt> portfiloData = queries.GetPortfolioWorkStationDescription();
                        if (portfiloData != null)
                        {
                            int totalCount = portfiloData.Sum(r => int.Parse(r.Count));

                            foreach (var record in portfiloData)
                            {
                                this.SetsCollection.Add(new SetValue { Color = colors.GetValue(index).ToString(), Label = record.KeyText, Value = ((double.Parse(record.Count) / totalCount) * 100).ToString("f2") });
                                index++;
                            }

                        }
                        break;
                    case ChartID.PortfolioOwnerBreakdown:

                        portfiloData = queries.GetPortfolioOwner();
                        if (portfiloData != null)
                        {
                            int totalCount = portfiloData.Sum(r => int.Parse(r.Count));

                            foreach (var record in portfiloData)
                            {
                                this.SetsCollection.Add(new SetValue { Color = colors.GetValue(index).ToString(), Label = record.KeyText, Value = ((double.Parse(record.Count) / totalCount) * 100).ToString("f2") });
                                index++;
                            }

                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public new void CreateChart()
        {
            StringBuilder chartXML = null;
            try
            {
                string caption = (string.IsNullOrEmpty(this.DrillBy)) ? this.Caption + "'" : this.Caption + " (" + this.DrillBy + ")'";
                chartXML = new StringBuilder("<chart" + (!(string.IsNullOrEmpty(this.Caption)) ? " caption='" + caption : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGColor)) ? " bgColor='" + this.BGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.BGAlpha)) ? " bgAlpha='" + this.BGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGColor)) ? " canvasBgColor='" + this.CanvasBGColor + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.CanvasBGAlpha)) ? " canvasBgAlpha='" + this.CanvasBGAlpha + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.XaxisName)) ? " xAxisName='" + this.XaxisName + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.YaxisName)) ? " yAxisName='" + this.YaxisName + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.PieRadius)) ? " pieRadius='" + this.PieRadius + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.showLegend)) ? " showLegend='" + this.showLegend + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.enableRotation)) ? " enableRotation='" + this.enableRotation + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.showLabels)) ? " showLabels='" + this.showLabels + "'" : ""));
                chartXML.Append((!(string.IsNullOrEmpty(this.NumberSuffix)) ? " numberSuffix='" + this.NumberSuffix + "'" : ""));


                chartXML.Append(">");

                chartXML.Append(CreateDataSets());

                chartXML.Append("</chart>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ChartXML = chartXML.ToString();
        }
    }

    public class Category
    {
        public string Label { get; set; }
        public Category()
        {
        }
    }

    public class ChartDataSet
    {
        public string SeriesName { get; set; }
        public string Color { get; set; }
        public List<SetValue> SetsCollection { get; set; }

        public ChartDataSet()
        {
            this.SetsCollection = new List<SetValue>();
        }

    }

    public class SetValue
    {
        public string Value { get; set; }
        public string Link { get; set; }
        public string Color { get; set; }
        public string Label { get; set; }
    }

    public class ChartParameter
    {
        public string Color { get; set; }
        public string Value { get; set; }
    }
}
