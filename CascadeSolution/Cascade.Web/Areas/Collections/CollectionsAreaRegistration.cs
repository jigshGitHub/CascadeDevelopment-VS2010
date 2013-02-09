using System.Web.Mvc;

namespace Cascade.Web.Areas.Collections
{
    public class CollectionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Collections";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Collections_default",
                "Collections/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Collections.Controllers" }
            );
        }
    }
}
