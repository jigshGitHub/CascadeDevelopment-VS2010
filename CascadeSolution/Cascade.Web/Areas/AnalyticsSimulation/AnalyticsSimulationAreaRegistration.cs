using System.Web.Mvc;

namespace Cascade.Web.Areas.AnalyticsSimulation
{
    public class AnalyticsSimulationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AnalyticsSimulation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AnalyticsSimulation_default",
                "AnalyticsSimulation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.AnalyticsSimulation.Controllers" }
            );
        }
    }
}
