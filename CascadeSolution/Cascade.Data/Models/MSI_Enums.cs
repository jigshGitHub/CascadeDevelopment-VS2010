using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cascade.Data.Models
{
    public enum MediaRequestStatus
    {
        RequestReceived = 1,
        RequestInProgress = 2,
        PendingResearch = 3,
        RequestFulfillment = 4,
        ReSubmit = 5,
        RequestComplete = 6
    }

    public enum CascadeRoles
    {
        agency,
        user,
        buyer
    }
}
