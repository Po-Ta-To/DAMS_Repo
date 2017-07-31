using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace DAMS_03.HtmlHelpers
{
    public class BootstrapHtml
    {

        public static MvcHtmlString Dropdown(string id, List<SelectListItem> selectListItems)
        {
            return dropdownfunction(id, selectListItems);
        }

        public static MvcHtmlString Dropdown(string id, List<SelectListItem> selectListItems, string selection)
        {
            return dropdownfunction(id, selectListItems, selection);
        }

        public static MvcHtmlString Dropdown(string id, List<SelectListItem> selectListItems, int selection)
        {
            return dropdownfunction(id, selectListItems, selection.ToString());
        }

        public static MvcHtmlString Dropdown(string id, List<SelectListItem> selectListItems, int? selection)
        {
            if (selection != null)
            {
                return dropdownfunction(id, selectListItems, selection.ToString());
            }
            else
            {
                return dropdownfunction(id, selectListItems);
            }

        }

        private static MvcHtmlString dropdownfunction(string id, List<SelectListItem> selectListItems, string selection)
        {
            var select = new TagBuilder("select")
            {
                Attributes =
            {
                {"id", id},
                {"name", id},
                {"type", "select"}
            }
            };

            select.AddCssClass("form-control");
            select.AddCssClass("input-group-sm");

            foreach (SelectListItem item in selectListItems)
            {
                var listitem = new TagBuilder("option");
                listitem.Attributes.Add("value", item.Value);
                listitem.SetInnerText(item.Text);

                if (item.Selected == true || item.Value.Equals(selection))
                {
                    listitem.Attributes.Add("selected", "selected");
                }
                select.InnerHtml += listitem;
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("dropdown");

            wrapper.InnerHtml += select;

            return new MvcHtmlString(wrapper.ToString());
        }

        private static MvcHtmlString dropdownfunction(string id, List<SelectListItem> selectListItems)
        {
            var select = new TagBuilder("select")
            {
                Attributes =
            {
                {"id", id},
                {"name", id},
                {"type", "select"}
            }
            };

            select.AddCssClass("form-control");
            select.AddCssClass("input-group-sm");

            foreach (SelectListItem item in selectListItems)
            {
                var listitem = new TagBuilder("option");
                listitem.Attributes.Add("value", item.Value);
                listitem.SetInnerText(item.Text);

                if (item.Selected == true)
                {
                    listitem.Attributes.Add("selected", "selected");
                }
                select.InnerHtml += listitem;
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("dropdown");

            wrapper.InnerHtml += select;

            return new MvcHtmlString(wrapper.ToString());
        }

    }
}