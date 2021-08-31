using System;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class GeneralSettingController : BaseController
    {
        // GET: GeneralSetting
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

            var GeneralSettingList = apiClient.GetAll<GeneralSettingsViewModel>("GeneralSetting", "Get", string.Empty);
            return View("~/Views/Admin/GeneralSetting/GeneralSettingListing.cshtml", GeneralSettingList);
        }

        // GET: GeneralSetting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GeneralSetting/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            return View("~/Views/Admin/GeneralSetting/AddEditGeneralSetting.cshtml");
        }

        // POST: GeneralSetting/Create
        [HttpPost]
        public ActionResult Save(GeneralSettingsViewModel GeneralSetting)
        {
            string response;
            //if (GeneralSetting.GeneralSettingID == 0)
            //    response = apiClient.Post<GeneralSettingsViewModel>("GeneralSetting", "Post", GeneralSetting);
            //else

            response = apiClient.PUT<GeneralSettingsViewModel>("GeneralSetting", "PUT", GeneralSetting);

            return Redirect("/Admin/GeneralSetting/Edit/1");

            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: GeneralSetting/Edit/5
        public ActionResult Edit(int id,string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status.Equals("s", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Added Successfully.";
                else if (status.Equals("u", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Updated Successfully.";
                else if (status.Equals("d", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.Message = "Record Deleted Successfully.";
            }

            ViewBag.IsEditMode = true;
            var GeneralSetting = apiClient.Get<GeneralSettingsViewModel>("GeneralSetting", "get", "id=" + id);
            return View("~/Views/Admin/GeneralSetting/AddEditGeneralSetting.cshtml", GeneralSetting);
        }

        // GET: GeneralSetting/Delete/5

        public ActionResult Delete(int id)
        {
            apiClient.Delete<UsersViewModel>("GeneralSetting", "Delete", "id=" + id);
            return Redirect("/admin/GeneralSetting/index/d");
        }
    }
}
