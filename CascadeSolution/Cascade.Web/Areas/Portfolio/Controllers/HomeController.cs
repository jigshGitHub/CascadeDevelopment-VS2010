using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;

namespace Cascade.Web.Areas.Portfolio.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Portfolio/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
