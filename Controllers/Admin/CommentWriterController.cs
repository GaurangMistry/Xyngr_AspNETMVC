using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;
using System.Configuration;

namespace Xyngr.Controllers.Admin
{
    public class CommentWriterController : BaseController
    {
        // GET: CommentWriter
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

            var CommentWriterList = apiClient.GetAll<CommentWriterViewModel>("CommentWriter", "Get", string.Empty);
            return View("~/Views/Admin/CommentWriter/CommentWriterListing.cshtml", CommentWriterList);
        }

        // GET: CommentWriter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentWriter/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            return View("~/Views/Admin/CommentWriter/AddEditCommentWriter.cshtml", new CommentWriterViewModel());
        }

        // POST: CommentWriter/Create
        [HttpPost]
        public ActionResult Save(CommentWriterViewModel CommentWriter)
        {
            string response;
            if (CommentWriter.CommentWriterID == 0)
                response = apiClient.Post<CommentWriterViewModel>("CommentWriter", "Post", CommentWriter);
            else
                response = apiClient.PUT<CommentWriterViewModel>("CommentWriter", "PUT", CommentWriter);

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
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + Convert.ToString(ConfigurationManager.AppSettings["CommentWriterImagePath"]), Server.MapPath(@"\")));

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
        // GET: CommentWriter/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var CommentWriter = apiClient.Get<CommentWriterViewModel>("CommentWriter", "get", "id=" + id);
            CommentWriter.ImageURL =string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["CommentWriterImagePath"], "imagepath", CommentWriter.Image);
            return View("~/Views/Admin/CommentWriter/AddEditCommentWriter.cshtml", CommentWriter);
        }

        // GET: CommentWriter/Delete/5

        public ActionResult Delete(int id)
        {
            apiClient.Delete<CommentWriterViewModel>("CommentWriter", "Delete", "id=" + id);
            return Redirect("/admin/CommentWriter/index/d");
        }
    }
}
