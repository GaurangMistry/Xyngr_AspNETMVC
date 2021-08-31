using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class TermsConditionsController : FrontBaseController
    {
        // GET: TermsConditions
        public ActionResult Index()
        {

            var model = apiClient.Get<StaticPagesViewModel>("StaticPage", "GetStaticPagesByPageCode", "PageCode=TERMSCONDITIONS");

            return View("~/Views/TermsConditions/TermsConditions.cshtml", model);

        }
    }
}