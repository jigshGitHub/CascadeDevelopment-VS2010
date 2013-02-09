using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cascade.Web.Controllers;
using Cascade.Data.Repositories;
using Cascade.Data.Models;
using JQueryDataTables.Models.Repository;
using Cascade.Web.Presentation.ViewModels.DPS;
using Cascade.Web.Areas.Collections.Models;

namespace Cascade.Web.Areas.Collections.Controllers
{
    public class AddFormController : BaseController
    {
        //
        // GET: /Collections/AddForm/
        [HttpGet]
        public ActionResult Index()
        {
            //This will return blank view for ADD feature
            return View();
        }

        [HttpPost]
        //public Form Add(Form _dpsform)
        public JsonResult Add(Form _dpsform)
        {
            PortDPSRepository repository;
            Port_DPS dpsToSave;
            try
            {
                repository = new PortDPSRepository();
                if (_dpsform.ID > 0)
                {
                    //Edit Operation
                }
                else
                {
                    //Add Operation
                    dpsToSave = new Port_DPS();
                    Mappings.ViewModelToModel(dpsToSave, _dpsform);
                    repository.Add(dpsToSave);
                }
            }
            catch (Exception ex)
            {
            }
            //return _dpsform;
            return Json(_dpsform, JsonRequestBehavior.AllowGet);
        }
    }
}
