using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xyngr.helper;

namespace Xyngr.Controllers
{
    public class FrontBaseController : Controller
    {
        public ApiClient apiClient;
        public FrontBaseController()
        {
            apiClient = new ApiClient(ConfigurationManager.AppSettings["WebAPIURL"].ToString());
        }
    }
}