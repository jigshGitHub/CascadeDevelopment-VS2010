using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.Recourse.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //
        // GET: /Recourse/Home/

        public ActionResult Index()
        {
            //return PartialView("Index");
            return View();
        }

    }
}
