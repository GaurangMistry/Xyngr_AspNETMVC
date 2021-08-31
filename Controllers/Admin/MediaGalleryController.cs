using System;
using System.Collections.Generic;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class MediaGalleryController : BaseController
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

            var MediaGalleryList = new List<MediaGalleryViewModel>();
            //var MediaGalleryList = apiClient.GetAll<MediaGalleryViewModel>("MediaGallery", "Get", string.Empty);
            //var CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                MediaGalleryList = apiClient.GetAll<MediaGalleryViewModel>("MediaGallery", "GetAllMediaGalleryByBranchID", "id=" + Request.QueryString["bid"]);
            else
                MediaGalleryList = apiClient.GetAll<MediaGalleryViewModel>("MediaGallery", "Get", string.Empty);

            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);

            var objMediaGalleryListing = new MediaGalleryListingViewModel();
            objMediaGalleryListing.MediaGalleryList = MediaGalleryList;
            objMediaGalleryListing.BusinessList = Businesslist;
            objMediaGalleryListing.BranchList = BranchList;

            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                objMediaGalleryListing.BranchID = Convert.ToInt64(Request.QueryString["bid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                objMediaGalleryListing.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);


            return View("~/Views/Admin/MediaGallery/MediaGalleryListing.cshtml", objMediaGalleryListing);
        }

        public ActionResult GetMediaGalleryViewByBranchID(int id)
        {
            var objMediaGalleryListing = new MediaGalleryListingViewModel();

            var MediaGalleryList = new List<MediaGalleryViewModel>();

            if (id != 0)
                MediaGalleryList = apiClient.GetAll<MediaGalleryViewModel>("MediaGallery", "GetAllMediaGalleryByBranchID", "id=" + id);

            objMediaGalleryListing.MediaGalleryList = MediaGalleryList;

            return PartialView("~/Views/Admin/MediaGallery/_MediaGalleryListPartial.cshtml", objMediaGalleryListing);
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

            var MediaGallery = new MediaGalleryViewModel();
            MediaGallery.BusinessList = Businesslist;
            MediaGallery.BranchList = BranchList;

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                MediaGallery.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["brid"]))
                MediaGallery.BranchID = Convert.ToInt64(Request.QueryString["brid"]);

            return View("~/Views/Admin/MediaGallery/AddEditMediaGallery.cshtml", MediaGallery);
        }

        // POST: Branch/Create
        [HttpPost]
        public ActionResult Save(MediaGalleryViewModel MediaGallery)
        {
            string response;
            if (MediaGallery.MediaID == 0)
                response = apiClient.Post<MediaGalleryViewModel>("MediaGallery", "Post", MediaGallery);
            else
                response = apiClient.PUT<MediaGalleryViewModel>("MediaGallery", "PUT", MediaGallery);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var BusinessList = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);

            var MediaGallery = apiClient.Get<MediaGalleryViewModel>("MediaGallery", "get", "id=" + id);
            MediaGallery.BusinessList = BusinessList;
            MediaGallery.BranchList = BranchList;

            return View("~/Views/Admin/MediaGallery/AddEditMediaGallery.cshtml", MediaGallery);
        }

        public ActionResult Delete(int id)
        {
            apiClient.Delete<MediaGalleryViewModel>("MediaGallery", "Delete", "id=" + id);
            return Redirect("/admin/MediaGallery/index/d");
        }
    }
}
