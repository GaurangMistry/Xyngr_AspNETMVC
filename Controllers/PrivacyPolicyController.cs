using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class PrivacyPolicyController : FrontBaseController
    {
        // GET: PrivacyPolicy
        public ActionResult Index()
        {
            var model = apiClient.Get<StaticPagesViewModel>("StaticPage", "GetStaticPagesByPageCode", "PageCode=PRIVACYPOLICY");

            return View("~/Views/PrivacyPolicy/PrivacyPolicy.cshtml", model);
        }
    }
}