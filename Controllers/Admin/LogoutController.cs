using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Xyngr.Controllers.Admin
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}