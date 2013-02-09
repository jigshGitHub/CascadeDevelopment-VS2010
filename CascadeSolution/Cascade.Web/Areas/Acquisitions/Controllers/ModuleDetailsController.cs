using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
namespace Cascade.Web.Areas.Acquisitions.Controllers
{
    [Authorize]
    public class ModuleDetailsController : BaseController
    {
        //
        // GET: /Acquisitions/ModuleDetails/

        public ActionResult Index()
        {
            return View("Index");
        }

        //
        // GET: /Acquisitions/ModuleDetails/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Acquisitions/ModuleDetails/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Acquisitions/ModuleDetails/Create

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
        // GET: /Acquisitions/ModuleDetails/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Acquisitions/ModuleDetails/Edit/5

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
        // GET: /Acquisitions/ModuleDetails/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Acquisitions/ModuleDetails/Delete/5

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
