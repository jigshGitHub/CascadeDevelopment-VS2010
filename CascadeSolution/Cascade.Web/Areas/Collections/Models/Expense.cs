using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cascade.Web.Areas.Collections.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public decimal? ExpenseAmount { get; set; }
        public string ExpenseDescription { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? CategoryId { get; set; }
    }
}