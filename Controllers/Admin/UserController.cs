using System;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers.Admin
{
    public class UserController : BaseController
    {
        // GET: User
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
            var userList = apiClient.GetAll<UsersViewModel>("user", "Get", string.Empty);
            return View("~/Views/Admin/User/UserListing.cshtml", userList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.IsEditMode = false;
            return View("~/Views/Admin/User/AddEditUser.cshtml");
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Save(UsersViewModel user)
        {
            string response;
            if (user.UserID == 0)
                response = apiClient.Post<UsersViewModel>("user", "Post", user);
            else
                response = apiClient.PUT<UsersViewModel>("user", "PUT", user);

            var errMessage = string.Empty;
            var isSuccess = true;
            if (response.Contains("User already exists with this email"))
            {
                errMessage = "User already exists with this email";
                isSuccess = false;
            }

            return Json(new { success = isSuccess, message = errMessage }, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IsEditMode = true;
            var user = apiClient.Get<UsersViewModel>("user", "get", "id=" + id);
            return View("~/Views/Admin/User/AddEditUser.cshtml", user);
        }

        public ActionResult Delete(int id)
        {
            apiClient.Delete<UsersViewModel>("user", "Delete", "id=" + id);
            return Redirect("/admin/user/index/d");
        }
    }
}
