using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.Data.Models
{
    public class MSI_Debtor
    {
        public string Account { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string LastFourSSN { get; set; }
        public string DOB { get; set; }
        public string CreditorName { get; set; }
        public string DebtPurchaseBalance { get; set; }
        public string DebtCurrentBalance { get; set; }
    }
}
