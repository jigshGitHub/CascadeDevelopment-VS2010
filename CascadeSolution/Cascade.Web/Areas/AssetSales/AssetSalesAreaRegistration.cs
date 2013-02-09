using System.Web.Mvc;

namespace Cascade.Web.Areas.AssetSales
{
    public class AssetSalesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AssetSales";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AssetSales_default",
                "AssetSales/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.AssetSales.Controllers" }
            );
        }
    }
}
