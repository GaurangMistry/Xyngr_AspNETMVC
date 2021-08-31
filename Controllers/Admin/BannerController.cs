using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;
using System.Configuration;

namespace Xyngr.Controllers.Admin
{
    public class BannerController : BaseController
    {
        // GET: Banner
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

            var bannerList = apiClient.GetAll<BannersViewModel>("Banner", "Get", string.Empty);
            return View("~/Views/Admin/Banner/BannerListing.cshtml", bannerList);
        }

        // GET: Banner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Banner/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            return View("~/Views/Admin/Banner/AddEditBanner.cshtml", new BannersViewModel());
        }

        // POST: Banner/Create
        [HttpPost]
        public ActionResult Save(BannersViewModel banner)
        {
            string response;
            if (banner.BannerID == 0)
                response = apiClient.Post<BannersViewModel>("Banner", "Post", banner);
            else
                response = apiClient.PUT<BannersViewModel>("Banner", "PUT", banner);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public string FileUpload(FormCollection formdata) {
            string fName = formdata["filename"];
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    //fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["BannerImagePath"]), Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, fName);
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex){}
            return fName;
        }
        // GET: Banner/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var banner = apiClient.Get<BannersViewModel>("Banner", "get", "id=" + id);
            banner.ImageURL =string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BannerImagePath"], "imagepath", banner.Image);
            return View("~/Views/Admin/Banner/AddEditBanner.cshtml", banner);
        }

        // GET: Banner/Delete/5

        public ActionResult Delete(int id)
        {
            apiClient.Delete<BannersViewModel>("Banner", "Delete", "id=" + id);
            return Redirect("/admin/Banner/index/d");
        }
    }
}
