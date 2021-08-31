using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class AboutUsController : FrontBaseController
    {
        // GET: Venue
        public ActionResult Index(string id)
        {
            var model = apiClient.Get<StaticPagesViewModel>("StaticPage", "GetStaticPagesByPageCode", "PageCode=ABOUTUS");
            return View("~/Views/AboutUs/About.cshtml", model);
        }
    }
}