using System;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class StaticPageController : BaseController
    {
        // GET: StaticPage
        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.Equals("s", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Added Successfully.";
                else if (id.Equals("u", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Updated Successfully.";
                else if (id.Equals("d", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Deleted Successfully.";
            }

            var StaticPageList = apiClient.GetAll<StaticPagesViewModel>("StaticPage", "Get", string.Empty);
            return View("~/Views/Admin/StaticPage/StaticPageListing.cshtml", StaticPageList);
        }

        // GET: StaticPage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaticPage/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            return View("~/Views/Admin/StaticPage/AddEditStaticPage.cshtml");
        }

        // POST: StaticPage/Create
        [HttpPost]
        public ActionResult Save(StaticPagesViewModel StaticPage)
        {
            string response;
            if (StaticPage.PageId == 0)
                response = apiClient.Post<StaticPagesViewModel>("StaticPage", "Post", StaticPage);
            else
                response = apiClient.PUT<StaticPagesViewModel>("StaticPage", "PUT", StaticPage);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: StaticPage/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var StaticPage = apiClient.Get<StaticPagesViewModel>("StaticPage", "get", "id=" + id);
            return View("~/Views/Admin/StaticPage/AddEditStaticPage.cshtml", StaticPage);
        }

        // GET: StaticPage/Delete/5
        public ActionResult Delete(int id)
        {
            apiClient.Delete<UsersViewModel>("StaticPage", "Delete", "id=" + id);
            return Redirect("/admin/StaticPage/index/d");
        }
    }
}
