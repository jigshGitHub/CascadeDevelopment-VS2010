using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml;
using Cascade.UI.Controls;
namespace Cascade.Web.ApplicationIntegration
{
    interface IApplicationMenu
    {
        void CreateApplicationMenu();
    }
    public class ApplicationMenu : IApplicationMenu
    {
        public ICascadeMenuCollection MenuCollection { get; set; }

        public ApplicationMenu(ICascadeMenuCollection menuCollection)
        {
            MenuCollection = menuCollection;
        }

        public ApplicationMenu()
            : this(new CascadeMenuCollection())
        {
        }

        public void CreateApplicationMenu()
        {
            List<CascadeMenu> menuItems = (List<CascadeMenu>)ConfigurationManager.GetSection("cascadeCutomGroup/cascadeMenu");
            foreach (CascadeMenu item in menuItems)
                MenuCollection.CreateMenu(item);
        }
    }

    public class MenuHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            List<CascadeMenu> items = null;
            CascadeMenu menu = null;
            try
            {
                if (section == null)
                    throw new Exception("Menu section in web config is not defined");

                items = new List<CascadeMenu>();
                foreach (XmlNode menuNode in section.ChildNodes)
                {
                    menu = new CascadeMenu { Id = int.Parse(menuNode.Attributes["Id"].Value), Action = menuNode.Attributes["Action"].Value, Controller = menuNode.Attributes["Controller"].Value, IsActive = Convert.ToBoolean(menuNode.Attributes["IsActive"].Value), ParentId = int.Parse(menuNode.Attributes["ParentId"].Value), SortOrder = int.Parse(menuNode.Attributes["SortOrder"].Value), Text = menuNode.Attributes["Text"].Value, IsVisible = Convert.ToBoolean(menuNode.Attributes["IsVisible"].Value), Roles = menuNode.Attributes["Roles"].Value.Split(new char[] { ',' }) };                    
                    if (menuNode.Attributes["Area"] != null)
                        menu.Area = menuNode.Attributes["Area"].Value;

                    items.Add(menu);
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}