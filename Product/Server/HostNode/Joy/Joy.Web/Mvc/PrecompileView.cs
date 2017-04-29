using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Joy.Web.Mvc
{
    public class PrecompileView : IView
    {
        protected static Action<WebViewPage, string> overrideLayoutSetter = CreateOverriddenLayoutSetterDelegate();
        protected bool isPartial;
        protected Type type;
        protected string master;
        protected string url;
        protected string[] exts = new[] { "cshtml" };
        public PrecompileView(Type viewtype, string vpath, bool ispartial = false, string masterpage = null)
        {
            isPartial = ispartial;
            type = viewtype;
            master = masterpage;
            url = vpath;
        }
        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            var webViewPage = DependencyResolver.Current.GetService(type) as WebViewPage;
            if (webViewPage == null)
            {
                throw new InvalidOperationException("Invalid view type");
            }

            if (!String.IsNullOrEmpty(master))
            {
                overrideLayoutSetter(webViewPage, master);
            }

            webViewPage.VirtualPath = url;
            webViewPage.ViewContext = viewContext;
            webViewPage.ViewData = viewContext.ViewData;
            webViewPage.InitHelpers();

            WebPageRenderingBase startPage = null;
            if (!this.isPartial)
            {
                startPage = StartPage.GetStartPage(webViewPage, "_ViewStart", exts);
            }

            var pageContext = new WebPageContext(viewContext.HttpContext, webViewPage, null);
            webViewPage.ExecutePageHierarchy(pageContext, writer, startPage);

        }

        private static Action<WebViewPage, string> CreateOverriddenLayoutSetterDelegate()
        {
            PropertyInfo property = typeof(WebViewPage).GetProperty("OverridenLayoutPath",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (property == null)
            {
                throw new NotSupportedException("The WebViewPage internal property \"OverridenLayoutPath\" does not exist, probably due to an unsupported run-time version.");
            }

            MethodInfo setter = property.GetSetMethod(nonPublic: true);
            if (setter == null)
            {
                throw new NotSupportedException("The WebViewPage internal property \"OverridenLayoutPath\" exists but is missing a set method, probably due to an unsupported run-time version.");
            }

            return (Action<WebViewPage, string>)Delegate.CreateDelegate(typeof(Action<WebViewPage, string>), setter, throwOnBindFailure: true);
        }

    }
}
