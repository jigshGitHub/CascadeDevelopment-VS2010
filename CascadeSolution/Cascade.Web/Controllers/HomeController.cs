using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Description;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cascade.Web.Controllers
{
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";
            //return RedirectToAction("Index", "Home", new { area = "Recourse" });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

    }
}
