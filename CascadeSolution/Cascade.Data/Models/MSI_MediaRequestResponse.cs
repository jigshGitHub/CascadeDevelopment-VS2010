//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cascade.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MSI_MediaRequestResponse
    {
        public MSI_MediaRequestResponse()
        {
            this.MSI_MediaRequestedTypes = new HashSet<MSI_MediaRequestedTypes>();
        }
    
        public System.Guid Id { get; set; }
        public string AgencyId { get; set; }
        public string Account { get; set; }
        public string OriginalAccount { get; set; }
        public string Portfolio { get; set; }
        public string Lender { get; set; }
        public string SSN { get; set; }
        public string AccountName { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<System.DateTime> CODate { get; set; }
        public string Seller { get; set; }
        public System.DateTime RequestedDate { get; set; }
        public Nullable<System.DateTime> RespondedDate { get; set; }
        public System.Guid RequestedByUserId { get; set; }
        public Nullable<System.Guid> RespondedByUserId { get; set; }
    
        public virtual ICollection<MSI_MediaRequestedTypes> MSI_MediaRequestedTypes { get; set; }
    }
}
