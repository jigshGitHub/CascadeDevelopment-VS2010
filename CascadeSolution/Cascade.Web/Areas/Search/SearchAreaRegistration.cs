﻿using System.Web.Mvc;

namespace Cascade.Web.Areas.Search
{
    public class SearchAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Search";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Search_default",
                "Search/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Cascade.Web.Areas.Search.Controllers" }
            );
        }
    }
}
