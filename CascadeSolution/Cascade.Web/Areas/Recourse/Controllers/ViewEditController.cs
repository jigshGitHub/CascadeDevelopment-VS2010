using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using Cascade.Web.Presentation.ViewModels.DPS;
using Cascade.Web.Areas.Recourse.Models;
using Cascade.Web.ApplicationIntegration;

namespace Cascade.Web.Areas.Recourse.Controllers
{
    public class ViewEditController : BaseController
    {
        public ActionResult ReportsDPS()
        {
            return View();
        }
        public ActionResult ReportsMedia()
        {
            return View();
        }
        public ActionResult ReportsRecall()
        {
            return View();
        }
    }
}
