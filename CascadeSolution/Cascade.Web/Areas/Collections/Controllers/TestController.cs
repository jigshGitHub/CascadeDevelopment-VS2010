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
    public class TestController : BaseController
    {
        private static List<Expense> expenses = new List<Expense>{
            new Expense { ID = 1, ExpenseAmount = 3,     ExpenseDate = new DateTime(2012, 10, 15), ExpenseDescription = "Apples", CategoryId = 1 },
            new Expense { ID = 2, ExpenseAmount = 34,    ExpenseDate = new DateTime(2012, 10, 16), ExpenseDescription = "Internet bill", CategoryId = 2 },
            new Expense { ID = 3, ExpenseAmount = 60,    ExpenseDate = new DateTime(2012, 10, 18), ExpenseDescription = "Jeans", CategoryId = 3 },
            new Expense { ID = 4, ExpenseAmount = 375,    ExpenseDate = new DateTime(2012, 10, 19), ExpenseDescription = "Ipad", CategoryId = 4 },
            new Expense { ID = 5, ExpenseAmount = 26,    ExpenseDate = new DateTime(2012, 10, 22), ExpenseDescription = "new keyboard", CategoryId = 4 },
            new Expense { ID = 6, ExpenseAmount = 75,    ExpenseDate = new DateTime(2012, 10, 25), ExpenseDescription = "webcam", CategoryId = 4 },
            new Expense { ID = 7, ExpenseAmount = 15,    ExpenseDate = new DateTime(2012, 10, 29), ExpenseDescription = "cellphone minutes", CategoryId = 2 },
            new Expense { ID = 8, ExpenseAmount = 31,    ExpenseDate = new DateTime(2012, 10, 8), ExpenseDescription = "T-shirt", CategoryId = 3 },
            new Expense { ID = 9, ExpenseAmount = 7,    ExpenseDate = new DateTime(2012, 10, 3), ExpenseDescription = "Pens", CategoryId = 6 },
            new Expense { ID = 10, ExpenseAmount = 1,   ExpenseDate = new DateTime(2012, 10, 16), ExpenseDescription = "Monster energy", CategoryId = 5 },
            new Expense { ID = 11, ExpenseAmount = 5,   ExpenseDate = new DateTime(2012, 10, 30), ExpenseDescription = "Subway Sandwich", CategoryId = 1 }
        };

        private static Dictionary<int, String> categories = new Dictionary<int, string>
        {
            {1,"Food"},
            {2,"Bills"},
            {3,"Clothing"},
            {4,"Gadget"},
            {5,"Misc"},
            {6,"Misc2"}
        };
        
        //private static int lastId = 11;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExpenses()
        {
            var allExpenses = from exp in DataRepositoryColl.GetAllExpenses()
                         //select new { ID = dps.ID, DateRec = dps.DateRec };
                         select new Expense 
                         {
                              ID = exp.Id,
                              ExpenseAmount = exp.ExpenseAmount,
                              ExpenseDate = exp.ExpenseDate,
                              ExpenseDescription = exp.ExpenseDescription,
                              CategoryId = exp.CategoryId
                             
                         };
            return Json(allExpenses.ToList(), JsonRequestBehavior.AllowGet);
            
            
            //return Json(expenses, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCategories()
        {
            var allCategories = from cat in DataRepositoryColl.GetAllCategories()
                                select new { Text = cat.Description, Value = cat.Id };


            return Json(allCategories.ToList(), JsonRequestBehavior.AllowGet);
            
        }


        //public ActionResult GetChartData()
        //{
        //    var groupedExpenses = from e in expenses group e by e.CategoryId 
        //                          into g 
        //                          select new { category = categories[g.Key], amount = g.Sum(x => x.ExpenseAmount) };
        //    return Json(groupedExpenses.ToList(), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult AddExpense(Expense newExpense)
        //{
        //    int newID = -1;
        //    if (newExpense.ExpenseAmount != 0 && newExpense.ExpenseDate != new DateTime() && newExpense.ExpenseDescription != null && newExpense.ExpenseDescription.Trim() != "")
        //    {
        //        newExpense.ID = newID = ++lastId;
        //        expenses.Add(newExpense);
        //    }
        //    return Json(newID);
        //}

        //[HttpPost]
        //public ActionResult EditExpense(Expense expense)
        //{
        //    string result;
        //    Expense existing = expenses.Find(item => item.ID == expense.ID);
        //    if (existing != null)
        //    {
        //        if (expense.ExpenseAmount == 0 || expense.ExpenseDate == new DateTime() || expense.ExpenseDescription == null || expense.ExpenseDescription.Trim() == "")
        //        {
        //            result = "Record not valid. Try again.";
        //        }
        //        else
        //        {
        //            existing.ExpenseDate = expense.ExpenseDate;
        //            existing.ExpenseDescription = expense.ExpenseDescription;
        //            existing.ExpenseAmount = expense.ExpenseAmount;
        //            existing.CategoryId = expense.CategoryId;
        //            result = "success";
        //        }
        //    }
        //    else
        //    {
        //        result = "record not found";
        //    }
        //    return Json(result);
        //}

        //[HttpPost]
        //public ActionResult DeleteExpense(Expense expense)
        //{
        //    string result;
        //    Expense existing = expenses.Find(item => item.ID == expense.ID);
        //    if (existing != null)
        //    {
        //        expenses.Remove(existing);
        //        result = "success";
        //    }
        //    else
        //    {
        //        result = "record not found";
        //    }
        //    return Json(result);
        //}
        
    }
}
