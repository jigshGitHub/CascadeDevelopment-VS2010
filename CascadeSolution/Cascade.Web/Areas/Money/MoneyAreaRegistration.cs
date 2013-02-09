using System.Web.Mvc;

namespace Cascade.Web.Areas.Money
{
    public class MoneyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Money";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Money_default",
                "Money/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Money.Controllers" }
            );
        }
    }
}
