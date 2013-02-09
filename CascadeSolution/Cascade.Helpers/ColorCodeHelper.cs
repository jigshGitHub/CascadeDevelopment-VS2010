using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Cascade.Helpers
{

    public static class ColorCodeHelper
    {
        public static List<string> GetColorCodes()
        {
            KnownColor enumColor = new KnownColor();
            Array Colors = Enum.GetValues(enumColor.GetType());
            Color knownColor;
            string hexColor;
            List<string> colorCodes = null;
            try
            {
                colorCodes = new List<string>();
                foreach (object clr in Colors)
                {
                    if (!Color.FromKnownColor((KnownColor)clr).IsSystemColor)
                    {
                        knownColor = ColorTranslator.FromHtml(clr.ToString());
                        hexColor = String.Format("{0:X2}{1:X2}{2:X2}", knownColor.R, knownColor.G, knownColor.B);
                        colorCodes.Add(hexColor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ColorCodeHelper.GetColorCodes:" + ex.Message);
            }
            return colorCodes;
        }
    }
}
