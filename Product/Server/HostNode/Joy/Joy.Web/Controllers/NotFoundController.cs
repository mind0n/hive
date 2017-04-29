using Joy.Web.Mvc;
using System.Web.Mvc;

namespace Joy.Web.Controllers
{
    public class NotFoundController : JoyController
    {
        public NotFoundController() : base(false, "Display")
        {
            
        }

        //
        // GET: /NotFound/
        [AllowAnonymous]
        public ActionResult Display()
        {
            return new HttpStatusCodeResult(404);
        }
	}
}