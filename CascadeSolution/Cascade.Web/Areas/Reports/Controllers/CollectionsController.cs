using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
namespace Cascade.Web.Areas.Reports.Controllers
{
    public class CollectionsController : BaseController
    {
        //
        // GET: /Reports/Collections/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PortfolioWorkStatusBreakDown()
        {
            return View();
        }
        public ActionResult PortfolioOwnerBreakDown()
        {
            return View();
        }


        //
        // GET: /Reports/Collections/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reports/Collections/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reports/Collections/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reports/Collections/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Collections/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reports/Collections/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Collections/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
