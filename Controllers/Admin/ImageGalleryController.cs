using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class ImageGalleryController : BaseController
    {
        // GET: Branch
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
            var ImageGalleryList = new List<ImageGalleryViewModel>();

            //var ImageGalleryList = apiClient.GetAll<ImageGalleryViewModel>("ImageGallery", "Get", string.Empty);
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                ImageGalleryList = apiClient.GetAll<ImageGalleryViewModel>("ImageGallery", "GetAllImageGalleryByBranchID", "id=" + Request.QueryString["bid"]);
            else
                ImageGalleryList = apiClient.GetAll<ImageGalleryViewModel>("ImageGallery", "Get", string.Empty);


            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);

            var objImageGalleryListing = new ImageGalleryListingViewModel();
            objImageGalleryListing.ImageGalleryList =  ImageGalleryList;
            objImageGalleryListing.BusinessList = Businesslist;
            objImageGalleryListing.BranchList = BranchList;

            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                objImageGalleryListing.BranchID = Convert.ToInt64(Request.QueryString["bid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                objImageGalleryListing.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            return View("~/Views/Admin/ImageGallery/ImageGalleryListing.cshtml", objImageGalleryListing);
        }

        public ActionResult GetImageGalleryViewByBranchID(int id)
        {
            var objImageGalleryListing = new ImageGalleryListingViewModel();

            var ImageGalleryList = new List<ImageGalleryViewModel>();

            //var CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);
            if (id != 0)
                ImageGalleryList = apiClient.GetAll<ImageGalleryViewModel>("ImageGallery", "GetAllImageGalleryByBranchID", "id=" + id);
            //else
            //    CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);

            objImageGalleryListing.ImageGalleryList = ImageGalleryList;

            return PartialView("~/Views/Admin/ImageGallery/_ImageGalleryListPartial.cshtml", objImageGalleryListing);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);

            var ImageGallery = new ImageGalleryViewModel();
            ImageGallery.BusinessList = Businesslist;
            ImageGallery.BranchList = BranchList;

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                ImageGallery.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["brid"]))
                ImageGallery.BranchID = Convert.ToInt64(Request.QueryString["brid"]);

            return View("~/Views/Admin/ImageGallery/AddEditImageGallery.cshtml", ImageGallery);
        }

        public string FileUpload(FormCollection formdata)
        {
            string fName = formdata["filename"];
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["GalleryImagePath"]), Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "gallerypath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, fName);
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex) { }
            return fName;
        }

        // POST: Branch/Create
        [HttpPost]
        public ActionResult Save(ImageGalleryViewModel ImageGallery)
        {
            string response;
            if (ImageGallery.ImageID == 0)
                response = apiClient.Post<ImageGalleryViewModel>("ImageGallery", "Post", ImageGallery);
            else
                response = apiClient.PUT<ImageGalleryViewModel>("ImageGallery", "PUT", ImageGallery);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var BusinessList = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);

            var ImageGallery = apiClient.Get<ImageGalleryViewModel>("ImageGallery", "get", "id=" + id);
            ImageGallery.BusinessList = BusinessList;
            ImageGallery.BranchList = BranchList;
            ImageGallery.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["GalleryImagePath"], "gallerypath", ImageGallery.Image);

            return View("~/Views/Admin/ImageGallery/AddEditImageGallery.cshtml", ImageGallery);
        }

        public ActionResult Delete(int id)
        {
            apiClient.Delete<ImageGalleryViewModel>("ImageGallery", "Delete", "id=" + id);
            return Redirect("/admin/ImageGallery/index/d");
        }
    }
}
