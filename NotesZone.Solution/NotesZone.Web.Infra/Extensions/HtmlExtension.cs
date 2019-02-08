using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra.Extensions
{
    
    using System.Linq.Expressions;
    using System.Web.Mvc;
    public static class HtmlExtension
    {
        public static MvcHtmlString RenderImageWithPath(this HtmlHelper html, string image, string cssClass, int width, int height)
        {
            var builder = new StringBuilder();
            //string path = ConfigurationManager.AppSettings["ImageItemPath"].ToString(CultureInfo.InvariantCulture);
            
            builder.AppendFormat(
                   "<img src=\"/ClientImages/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",  // presently hardcoading but it shoud be coming fro web.confg //"<img src=\"/Content/themes/base/ClientImages/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",                                                                                         //"<img src=\"/Content/themes/base/ClientImages/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",
                   image,
                   image,
                   cssClass,
                   width,
                   height);

            return new MvcHtmlString(builder.ToString());
        }


        public static MvcHtmlString RenderImageWithPath<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TModel model, Expression<Func<TModel, TProperty>> expression, string cssClass, int width, int height)
        {
            var func = expression.Compile();

            var value = func.Invoke(model);

            var builder = new StringBuilder();

            builder.AppendFormat(
                //"<img src=\"/Content/themes/base/ClientImages/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",
                "<img src=\"/ClientImages/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",
                   value,
                   value,
                   cssClass,
                   width,
                   height);

            return new MvcHtmlString(builder.ToString());
        }



    }
}
