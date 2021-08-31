using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class BranchController : BaseController
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

            var objBranchListing = new BranchListingViewModel();

            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);
            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);

            objBranchListing.BranchList = BranchList;
            objBranchListing.BusinessList = Businesslist;

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                objBranchListing.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            return View("~/Views/Admin/Branch/BranchListing.cshtml", objBranchListing);
        }

        public ActionResult GetBranchViewByBusinessID(int id)
        {
            var objBranchListing = new BranchListingViewModel();

            var BranchList = new List<BranchesViewModel>();

            if (id != 0)
                BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "GetAllBranchByBusinessID", "id=" + id);

            objBranchListing.BranchList = BranchList;

            return PartialView("~/Views/Admin/Branch/_BranchListPartial.cshtml", objBranchListing);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public ActionResult Create(string id)
        {
            ViewBag.IsEditMode = false;
            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var Bannerlist = apiClient.GetAll<BannersViewModel>("Banner", "Get", string.Empty);

            var branch = new BranchesViewModel();
            branch.Businesslist = Businesslist;
            branch.Bannerlist = Bannerlist;

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                branch.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            return View("~/Views/Admin/Branch/AddEditBranch.cshtml", branch);
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
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["BranchImagePath"]), Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "branchpath");

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
        public ActionResult Save(BranchesViewModel Branch)
        {
            string response;
            if (Branch.BranchID == 0)
                response = apiClient.Post<BranchesViewModel>("Branch", "Post", Branch);
            else
                response = apiClient.PUT<BranchesViewModel>("Branch", "PUT", Branch);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;

            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var Bannerlist = apiClient.GetAll<BannersViewModel>("Banner", "Get", string.Empty);

            var Branch = apiClient.Get<BranchesViewModel>("Branch", "get", "id=" + id);
            Branch.Businesslist = Businesslist;
            Branch.Bannerlist = Bannerlist;

            Branch.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BranchImagePath"], "branchpath", Branch.ProfileImage);

            return View("~/Views/Admin/Branch/AddEditBranch.cshtml", Branch);
        }

        public ActionResult Delete(int id)
        {
            apiClient.Delete<BranchesViewModel>("Branch", "Delete", "id=" + id);
            return Redirect("/admin/Branch/index/d");
        }

        public ActionResult GetBranchByBusinessID(int BusinessID)
        {
            var branchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);
            branchList = branchList.Where(o => o.BusinessID == BusinessID).ToList();

            return Json(branchList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBranchDropdownByBusinessID(int id)
        {
            var branchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);
            branchList = branchList.Where(o => o.BusinessID == id).ToList();

            var result = new SelectList(branchList, "BranchID", "Address2");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
