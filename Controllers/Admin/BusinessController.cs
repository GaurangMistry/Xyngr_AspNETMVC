using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class BusinessController : BaseController
    {
        // GET: Business
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

            var objBusinessListing = new BusinessListingViewModel();

            var BusinessList = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            objBusinessListing.BusinessList = BusinessList;
            objBusinessListing.Categories = categoryList;

            if (!string.IsNullOrEmpty(Request.QueryString["pcid"]))
                objBusinessListing.ParentCategoryID = Convert.ToInt64(Request.QueryString["pcid"]);

            return View("~/Views/Admin/Business/BusinessListing.cshtml", objBusinessListing);
        }


        public ActionResult GetBusinessViewByCategoryID(int id)
        {
            var objBusinessListing = new BusinessListingViewModel();

            var BusinessList = new List<BusinessViewModel>();

            if (id != 0)
                BusinessList = apiClient.GetAll<BusinessViewModel>("Business", "GetAllBusinessByCategoryID", "id=" + id);

            objBusinessListing.BusinessList = BusinessList;

            return PartialView("~/Views/Admin/Business/_BusinessListPartial.cshtml", objBusinessListing);
        }

        // GET: Business/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Business/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            var business = new BusinessViewModel();
            business.Categories = categoryList;
            List<SelectListItem> listCategories = new List<SelectListItem>();
            listCategories.Add(new SelectListItem { Text = "-- Select --", Value = "0" });
            ViewBag.Categories = listCategories;

            if (!string.IsNullOrEmpty(Request.QueryString["pcid"]))
                business.ParentCategoryID = Convert.ToInt64(Request.QueryString["pcid"]);

            return View("~/Views/Admin/Business/AddEditBusiness.cshtml", business);
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
                    //fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["BusinessImagePath"]), Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "businesspath");

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

        // POST: Business/Create
        [HttpPost]
        public ActionResult Save(BusinessViewModel Business)
        {
            string response;
            if (Business.BusinessID == 0)
                response = apiClient.Post<BusinessViewModel>("Business", "Post", Business);
            else
                response = apiClient.PUT<BusinessViewModel>("Business", "PUT", Business);

            var errMessage = string.Empty;
            var isSuccess = true;
            if (response.Contains("Business already exists"))
            {
                errMessage = "Business already exists";
                isSuccess = false;
            }
            return Json(new { success = isSuccess ,message = errMessage}, JsonRequestBehavior.AllowGet);
        }

        // GET: Business/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            var Business = apiClient.Get<BusinessViewModel>("Business", "get", "id=" + id);
            Business.Categories = categoryList;

            Business.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BusinessImagePath"], "businesspath", Business.Image);

            return View("~/Views/Admin/Business/AddEditBusiness.cshtml", Business);
        }

        // GET: Business/Delete/5
        public ActionResult Delete(int id)
        {
            apiClient.Delete<BusinessViewModel>("Business", "Delete", "id=" + id);
            return Redirect("/admin/Business/index/d");
        }
    }
}
