using System.Web.Mvc;
using System.Web.SessionState;
using Joy.Core;
using Joy.Web.Mvc;
using Joy.Web.Mvc.Security;

namespace Joy.Web.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class JoyController : Controller
    {
        public JoyController(bool identify = true, string da = null )
        {
            if (identify)
            {
#if DEBUG
                var s = this.GetType().Name;
                Logger.Log("Controller {0} identifying".To(s));
#endif
                var cx = System.Web.HttpContext.Current;
                Identifier.Instance.ParseRequest(cx);
                Identifier.Instance.Identify();
            }
            var ai = new JoyActionInvoker(da);
            ActionInvoker = ai;
        }
    }
}