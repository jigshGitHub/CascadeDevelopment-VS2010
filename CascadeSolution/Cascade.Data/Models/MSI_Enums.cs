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
        RequestFulfillment = 4
    }
}
