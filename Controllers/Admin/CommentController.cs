using System;
using System.Collections.Generic;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class CommentController : BaseController
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
            var CommentList = new List<CommentsViewModel>();

            //var CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "GetAllCommentsByBranchID", "id=" + Request.QueryString["bid"]);
            else
                CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);

            var Businesslist = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);
            
            var objCommentListing = new CommentListingViewModel();
            objCommentListing.CommentList = CommentList;
            objCommentListing.BusinessList = Businesslist;
            objCommentListing.BranchList = BranchList;


            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                objCommentListing.BranchID = Convert.ToInt64(Request.QueryString["bid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                objCommentListing.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);


            return View("~/Views/Admin/Comment/CommentsListing.cshtml", objCommentListing);
        }


        public ActionResult GetCommentsViewByBranchID(int id)
        {
            var objCommentListing = new CommentListingViewModel();

            var CommentList = new List<CommentsViewModel>();

            //var CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);
            if (id != 0)
                CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "GetAllCommentsByBranchID", "id=" + id);
            //else
            //    CommentList = apiClient.GetAll<CommentsViewModel>("Comment", "Get", string.Empty);

            objCommentListing.CommentList = CommentList;

            return PartialView("~/Views/Admin/Comment/_CommentsListPartial.cshtml", objCommentListing);
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
            var CommentWriterList = apiClient.GetAll<CommentWriterViewModel>("CommentWriter", "Get", string.Empty);

            var Comments = new CommentsViewModel();
            Comments.BusinessList = Businesslist;
            Comments.BranchList = BranchList;
            Comments.CommentWriterList = CommentWriterList;

            if (!string.IsNullOrEmpty(Request.QueryString["buid"]))
                Comments.BusinessID = Convert.ToInt64(Request.QueryString["buid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["brid"]))
                Comments.BranchID = Convert.ToInt64(Request.QueryString["brid"]);

            return View("~/Views/Admin/Comment/AddEditComments.cshtml", Comments);
        }

        // POST: Branch/Create
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Save(CommentsViewModel commentModel)
        {
            string response;
            if (commentModel.CommentID == 0)
                response = apiClient.Post<CommentsViewModel>("Comment", "Post", commentModel);
            else
                response = apiClient.PUT<CommentsViewModel>("Comment", "PUT", commentModel);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var BusinessList = apiClient.GetAll<BusinessViewModel>("Business", "Get", string.Empty);
            var BranchList = apiClient.GetAll<BranchesViewModel>("Branch", "Get", string.Empty);
            var CommentWriterList = apiClient.GetAll<CommentWriterViewModel>("CommentWriter", "Get", string.Empty);

            var Comments = apiClient.Get<CommentsViewModel>("Comment", "get", "id=" + id);
            Comments.BusinessList = BusinessList;
            Comments.BranchList = BranchList;
            Comments.CommentWriterList = CommentWriterList;

            return View("~/Views/Admin/Comment/AddEditComments.cshtml", Comments);
        }

        public ActionResult Delete(int id , string buID, string bid)
        {
            apiClient.Delete<CommentsViewModel>("Comment", "Delete", "id=" + id);
            
            return Redirect("/admin/Comment/index?id=d&buid=" + buID + "&bid=" + bid + "");
        }
    }
}
