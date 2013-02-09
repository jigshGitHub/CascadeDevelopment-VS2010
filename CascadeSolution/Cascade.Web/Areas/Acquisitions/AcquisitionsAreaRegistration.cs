using System.Web.Mvc;

namespace Cascade.Web.Areas.Acquisitions
{
    public class AcquisitionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Acquisitions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Acquisitions_default",
                "Acquisitions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Acquisitions.Controllers" } 
            );
        }
    }
}
