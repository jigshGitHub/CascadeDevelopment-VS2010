using System.Web.Mvc;

namespace Cascade.Web.Areas.Compliance
{
    public class ComplianceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Compliance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Compliance_default",
                "Compliance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Compliance.Controllers" }
            );
        }
    }
}
