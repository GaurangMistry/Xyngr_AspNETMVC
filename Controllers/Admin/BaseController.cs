using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Xyngr.helper;

namespace Xyngr.Controllers.Admin
{
    [Authorize]
    public class BaseController : Controller
    {
        public ApiClient apiClient;
        public BaseController()
        {
            //if (Session != null && Session["UserID"] != null)
                apiClient = new ApiClient(ConfigurationManager.AppSettings["WebAPIURL"].ToString());
            //else
            //    RedirectToAction("Account", "Login");
        }
    }
}