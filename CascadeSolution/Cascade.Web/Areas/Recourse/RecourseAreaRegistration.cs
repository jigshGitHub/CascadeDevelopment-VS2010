using System.Web.Mvc;

namespace Cascade.Web.Areas.Recourse
{
    public class RecourseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Recourse";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Recourse_default",
                "Recourse/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Recourse.Controllers" }
            );
        }
    }
}
