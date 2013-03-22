using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cascade.Business
{
    public class NotificationMessage
    {
        public NotificationMessage()
        {
        }

        public string Subject { get; set; }
        public string BodyText { get; set; }
    }
}
