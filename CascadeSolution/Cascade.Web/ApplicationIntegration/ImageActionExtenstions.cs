using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Cascade.Web.ApplicationIntegration
{
    public static class ImageActionExtenstions
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    //    #region ActionImage
    //    // href image link
    //    public static string ActionImage( this HtmlHelper helper, string href, string linkText, object htmlAttributes,
    //                                      string alternateText, string imageSrc, object imageAttributes )
    //    {
    //        var sb = new StringBuilder();
    //        const string format = "<a href=\"{0}\"{1}>{2}</a>";
    //        string image = helper.Image( imageSrc, alternateText, imageAttributes ).ToString();
    //        string content = string.IsNullOrWhiteSpace( linkText ) ? image : image + linkText;
    //        sb.AppendFormat( format, href, GetAttributeString( htmlAttributes ), content );
    //        return sb.ToString();
    //    }

    //    // controller/action image link
    //    public static string ActionImage( this HtmlHelper helper, string controller, string action, string linkText, object htmlAttributes,
    //                                      string alternateText, string imageSrc, object imageAttributes )
    //    {
    //        bool isDefaultAction = string.IsNullOrEmpty( action ) || action == "Index";
    //        string href = "/" + (controller ?? "Home") + (isDefaultAction ? string.Empty : "/" + action);
    //        return ActionImage( helper, href, linkText, htmlAttributes, alternateText, imageSrc, imageAttributes );
    //    }

    //    // T4MVC ActionResult image link
    //    public static string ActionImage( this HtmlHelper helper, ActionResult actionResult, string linkText, object htmlAttributes,
    //                                      string alternateText, string imageSrc, object imageAttributes )
    //    {
    //        var controller = (string) actionResult.GetPropertyValue( "Controller" );
    //        var action = (string) actionResult.GetPropertyValue( "Action" );
    //        return ActionImage( helper, controller, action, linkText, htmlAttributes, alternateText, imageSrc, imageAttributes );
    //    }       
    //    #endregion

    //    #region Helpers
    //    private static string GetAttributeString( object htmlAttributes )
    //    {
    //        if( htmlAttributes == null )
    //        {
    //            return string.Empty;
    //        }
    //        const string format = " {0}=\"{1}\"";
    //        var sb = new StringBuilder();
    //        htmlAttributes.GetType().Properties().ForEach( p => sb.AppendFormat( format, p.Name, p.Get( htmlAttributes ) ) );
    //        return sb.ToString();
    //    }

    //    public static MvcHtmlString Image(this HtmlHelper helper, string imageRelativeUrl, string alt, object htmlAttributes)
    //    {
    //        return Image(helper, imageRelativeUrl, alt, new RouteValueDictionary(htmlAttributes));
    //    }

    //    public static MvcHtmlString Image(this HtmlHelper helper, string imageRelativeUrl, string alt, IDictionary<string, object> htmlAttributes)
    //    {
    //        if (String.IsNullOrEmpty(imageRelativeUrl))
    //        {
    //            throw new ArgumentException(MvcResources.Common_NullOrEmpty, "imageRelativeUrl");
    //        }

    //        string imageUrl = UrlHelper.GenerateContentUrl(imageRelativeUrl, helper.ViewContext.HttpContext);
    //        return MvcHtmlString.Create(Image(imageUrl, alt, htmlAttributes).ToString(TagRenderMode.SelfClosing));
    //    }

    //    public static TagBuilder Image(string imageUrl, string alt, IDictionary<string, object> htmlAttributes)
    //    {
    //        if (String.IsNullOrEmpty(imageUrl))
    //        {
    //            throw new ArgumentException(MvcResources.Common_NullOrEmpty, "imageUrl");
    //        }

    //        TagBuilder imageTag = new TagBuilder("img");

    //        if (!String.IsNullOrEmpty(imageUrl))
    //        {
    //            imageTag.MergeAttribute("src", imageUrl);
    //        }

    //        if (!String.IsNullOrEmpty(alt))
    //        {
    //            imageTag.MergeAttribute("alt", alt);
    //        }

    //        imageTag.MergeAttributes(htmlAttributes, true);

    //        if (imageTag.Attributes.ContainsKey("alt") && !imageTag.Attributes.ContainsKey("title"))
    //        {
    //            imageTag.MergeAttribute("title", (imageTag.Attributes["alt"] ?? "").ToString());
    //        }
    //        return imageTag;
    //    }
    //    #endregion
    }
}