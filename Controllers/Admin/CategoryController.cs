using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class CategoryController : BaseController
    {
        // GET: Category
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
                else if (id.Equals("ae", StringComparison.CurrentCultureIgnoreCase))
                    ViewBag.ErrorMessage = "We can't delete category as it is associated with venues.";
            }

            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);
            return View("~/Views/Admin/Category/CategoryListing.cshtml", categoryList);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);

            var category = new CategoryViewModel();
            category.Categories = categoryList.Where(o => o.ParentCategoryID == 0);

            return View("~/Views/Admin/Category/AddEditCategory.cshtml", category);
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
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["CategoryImagePath"]), Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "categorypath");

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

        // POST: Category/Create
        [HttpPost]
        public ActionResult Save(long categoryID, long parentCatID, string categoryName, bool status, string Image, string MetaAuthorContent, string MetaDescContent, string MetaKeyWordContent)
        {
            var category = new CategoryViewModel();
            category.ParentCategoryID = parentCatID;
            category.Status = status;
            category.CategoryName = categoryName;
            category.CategoryID = categoryID;
            category.Image = Image;
            category.MetaAuthorContent = MetaAuthorContent;
            category.MetaDescContent = MetaDescContent;
            category.MetaKeyWordContent = MetaKeyWordContent;

            string response;
            if (category.CategoryID == 0)
                response = apiClient.Post<CategoryViewModel>("Category", "Post", category);
            else
                response = apiClient.PUT<CategoryViewModel>("Category", "PUT", category);

            var errMessage = string.Empty;
            var isSuccess = true;
            if (response.Contains("Category already exists"))
            {
                errMessage = "Category already exists";
                isSuccess = false;
            }

            return Json(new { success = isSuccess, message = errMessage }, JsonRequestBehavior.AllowGet);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var category = apiClient.Get<CategoryViewModel>("Category", "get", "id=" + id);

            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);
            category.Categories = categoryList.Where(o => o.ParentCategoryID == 0);

            category.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["CategoryImagePath"], "categorypath", category.Image);

            return View("~/Views/Admin/Category/AddEditCategory.cshtml", category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {

            var result = apiClient.Delete<CategoryViewModel>("Category", "Delete", "id=" + id);
            if (result.Contains("error"))
                return Redirect("/admin/Category/index/ae");
            else
                return Redirect("/admin/Category/index/d");

        }

        public ActionResult GetCategoriesByParentID(int parentCategoryID)
        {
            var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "Get", string.Empty);
            categoryList = categoryList.Where(o => o.ParentCategoryID == parentCategoryID).ToList();

            // var result = new SelectList(categoryList, "Value", "Text", tm.PROJ_ID);
            return Json(categoryList, JsonRequestBehavior.AllowGet);
        }
    }
}
