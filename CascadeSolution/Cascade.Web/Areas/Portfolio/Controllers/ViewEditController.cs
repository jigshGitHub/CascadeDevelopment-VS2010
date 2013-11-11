using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
namespace Cascade.Web.Areas.Portfolio.Controllers
{
    public class ViewEditController : BaseController
    {
        //
        // GET: /Portfolio/AddForm/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Portfolio/AddForm/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Portfolio/AddForm/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Portfolio/AddForm/Create

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
        // GET: /Portfolio/AddForm/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Portfolio/AddForm/Edit/5

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
        // GET: /Portfolio/AddForm/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Portfolio/AddForm/Delete/5

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


        //For SSRS Reports
        public ActionResult Reports()
        {
            return View();
        }
    }
}
