using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cascade.Business
{
    public enum MediaRequestStatus
    {
        SentToOwner = 1,
        RequestInProgress = 2,
        OriginatorResearching = 3,
        Fulfilled = 4,
        OwnerUpdateRequested = 5,
        RequestComplete = 6,
        SentToOriginator = 7,
        OriginatorUpdateRequested = 8,
        OriginatorInProcess = 9,
        NoOriginatorMediaAvailable =10,
        Uploaded = 11,
        OriginatorFulfilled = 12,
        Downloaded = 13
    }

    public enum UserRoles
    {
        agency,
        user,
        buyer,
        mediaAdmin,
        originator
    }
    public enum MediaRequestLevel
    {
        Initiate,
        Update,
        Fulfill,
        Complete
    }    
}
