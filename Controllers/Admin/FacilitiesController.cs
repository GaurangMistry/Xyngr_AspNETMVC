using System;
using System.Collections.Generic;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class FacilitiesController : BaseController
    {
        // GET: Facilities
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

            var FacilitiesList = apiClient.GetAll<FacilitiesViewModel>("Facilities", "Get", string.Empty);
            return View("~/Views/Admin/Facilities/FacilitiesListing.cshtml", FacilitiesList);
        }

        // GET: Facilities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Facilities/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            var Facilities = new FacilitiesViewModel();
            Facilities.Categories = categoryList;
            List<SelectListItem> listCategories = new List<SelectListItem>();
            listCategories.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            ViewBag.Categories = listCategories;
            return View("~/Views/Admin/Facilities/AddEditFacilities.cshtml", Facilities);
        }

        // POST: Facilities/Create
        [HttpPost]
        public ActionResult Save(FacilitiesViewModel Facilities)
        {
            string response;
            if (Facilities.FacilityID == 0)
                response = apiClient.Post<FacilitiesViewModel>("Facilities", "Post", Facilities);
            else
                response = apiClient.PUT<FacilitiesViewModel>("Facilities", "PUT", Facilities);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Facilities/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            var Facilities = apiClient.Get<FacilitiesViewModel>("Facilities", "get", "id=" + id);
            Facilities.Categories = categoryList;

            return View("~/Views/Admin/Facilities/AddEditFacilities.cshtml", Facilities);
        }

        // GET: Facilities/Delete/5
        public ActionResult Delete(int id)
        {
            apiClient.Delete<FacilitiesViewModel>("Facilities", "Delete", "id=" + id);
            return Redirect("/admin/Facilities/index/d");
        }
    }
}
