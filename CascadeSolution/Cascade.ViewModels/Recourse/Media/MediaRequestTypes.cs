using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cascade.Data.Models;
namespace Cascade.ViewModels.Recourse.Media
{
    public class MediaRequestTypes
    {
        public MSI_MediaRequestTypes MSIMediaRequestTypes;
        public MSI_MediaRequestResponse MSIMediaRequestResponse;
        public string MediaType;
        public string MediaStatus;

        public MediaRequestTypes(MSI_MediaRequestTypes mediaRequestTypes, MSI_MediaRequestResponse requestResponse)
        {
            MSIMediaRequestTypes = mediaRequestTypes;
            MSIMediaRequestResponse = requestResponse;
        }
    }
}
