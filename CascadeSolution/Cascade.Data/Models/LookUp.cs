using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.Data.Models
{
    public class LookUp
    {
        public string Text;
        public string Value;

        public LookUp(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
