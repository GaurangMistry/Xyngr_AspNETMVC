using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class ContactUsController : FrontBaseController
    {
        // GET: Venue
        public ActionResult Index(string id)
        {
            var model = new ContactUsViewModel();

            model.StaticPages = apiClient.Get<StaticPagesViewModel>("StaticPage", "GetStaticPagesByPageCode", "PageCode=CONTACTUS");
            model.GeneralSettings = apiClient.Get<GeneralSettingsViewModel>("GeneralSetting", "Get", "id=1");

            return View("~/Views/ContactUs/Contact.cshtml", model);
        }
    }
}