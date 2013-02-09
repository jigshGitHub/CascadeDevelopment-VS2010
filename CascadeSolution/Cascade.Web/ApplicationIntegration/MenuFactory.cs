using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cascade.Web.ApplicationIntegration
{
    interface IMenuFactory
    {
        void InitializeMenu();
    }
    public class MenuFactory : IMenuFactory
    {
        private static ApplicationMenu currentMenu;
        public static ApplicationMenu CurrentMenu
        {
            get { return currentMenu; }
        }

        public void InitializeMenu()
        {
            if (currentMenu == null)
            {
                currentMenu = new ApplicationMenu();
                currentMenu.CreateApplicationMenu();
            }
        }
    }
}