using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Joy.Web.Mvc
{
    public static class MvcExtensions
    {
        public static string Render(this ViewResult vr, ControllerContext context = null)
        {
            using (var sw = new StringWriter())
            {
                var engine = vr.ViewEngineCollection[0];
                var ver = engine.FindView(context, vr.ViewName, vr.MasterName, true);

                var view = ver.View;
                var viewContext = new ViewContext(context, view, vr.ViewData, vr.TempData, sw);
                view.Render(viewContext, sw);
                if (ver != null)
                {
                    ver.ViewEngine.ReleaseView(context, view);
                    //vr.ViewEngine.ReleaseView(context, view);
                }
                return sw.ToString();
            }
        }

        private static string RenderView(object model, ControllerContext Context, ViewEngineResult viewEngineResult, string result)
        {
            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            Context.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(Context, view, Context.Controller.ViewData, Context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }
            return result;
        }

    }
}
