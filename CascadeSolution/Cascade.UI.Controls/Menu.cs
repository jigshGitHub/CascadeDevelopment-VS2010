using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.UI.Controls
{
    public abstract class Menu
    {
        protected int id;
        protected int parentId;
        protected string text;
        protected int sortOrder;
        protected bool isActive;

        public abstract int Id { get; set; }
        public abstract int ParentId { get; set; }
        public abstract string Text { get; set; }
        public abstract int SortOrder { get; set; }
        public abstract bool IsActive { get; set; }

    }
        
    public class CascadeMenu : Menu
    {

        public override int Id
        {
            get { return id; }
            set { id = value; }
        }

        public override int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public override string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public override bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsVisible
        {
            get;
            set;
        }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string[] Roles { get; set; }
        public string[] Parameters { get; set; }
        public string[] ParametersVals { get; set; }
    }

    public interface IMenuCollection<T>
    {
        void CreateMenu(T item);
    }

    public class MenuCollection<T> : IMenuCollection<T>
    {
        public List<T> MenuItems { get; set; }

        public MenuCollection()
        {
            MenuItems = new List<T>();
        }

        public void CreateMenu(T item)
        {
            MenuItems.Add(item);
        }

    }

    //public interface ICascadeMenuCollection<T>
    //{
    //    void CreateMenu(T item);
    //    bool MenuItemHasChildren(int id);
    //    bool MenuItemHasParent(int parentId);
    //    IEnumerable<Menu> GetChildMenuItems(int parentId);
    //    IEnumerable<Menu> GetParentMenuItems(int parentId);
    //    Menu GetMenuItem(int id);
    //}

    public interface ICascadeMenuCollection : IMenuCollection<CascadeMenu>
    {
        CascadeMenu GetMenuItemForAController(ICascadeMenuCollection menuCollection, string controllerName, string area);
        bool MenuItemHasChildren(int id);
        bool MenuItemHasParent(int parentId);
        IEnumerable<CascadeMenu> GetChildMenuItems(int parentId);
        IEnumerable<CascadeMenu> GetParentMenuItems(int parentId);

    }

    public class CascadeMenuCollection : MenuCollection<CascadeMenu>, ICascadeMenuCollection
    {
        public CascadeMenu GetMenuItemForAController(ICascadeMenuCollection menuCollection, string controllerName, string area)
        {
            CascadeMenu returnMenu;
            returnMenu = (menuCollection as MenuCollection<CascadeMenu>).MenuItems.Find(r => r.Controller == controllerName && r.Area == area);
            return returnMenu;
        }

        public bool MenuItemHasChildren(int id)
        {
            return MenuItems.Any<CascadeMenu>(i => i.ParentId == id);

        }

        public bool MenuItemHasParent(int parentId)
        {
            return MenuItems.Any(i => i.Id == parentId);
        }

        public IEnumerable<CascadeMenu> GetChildMenuItems(int parentId)
        {
            return MenuItems.Where(i => i.ParentId == parentId).OrderBy(i => i.SortOrder);//.ThenBy(i => i.Text);
        }

        public IEnumerable<CascadeMenu> GetParentMenuItems(int parentId)
        {
            return MenuItems.Where(i => i.ParentId == parentId).OrderBy(i => i.SortOrder);//.ThenBy(i => i.Text);
        }


    }
}
