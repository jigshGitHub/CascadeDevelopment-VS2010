using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cascade.Data.Models;

namespace Cascade.Web.Areas.Recourse.Models
{
    public class MediaRequestTypes
    {
        public MSI_MediaRequestTypes MSIMediaRequestTypes;
        public MSI_MediaRequestResponse MSIMediaRequestResponse;
        public string MediaType;
        public string MediaStatus;

        public MediaRequestTypes(MSI_MediaRequestTypes mediaRequestTypes, MSI_MediaRequestResponse requestResponse){
            MSIMediaRequestTypes = mediaRequestTypes;
            MSIMediaRequestResponse = requestResponse;
        }
    }
}