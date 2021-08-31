using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class VenueController : FrontBaseController
    {
        // GET: Venue
        public ActionResult Index(string id)
        {
            var model = new BusinessListingForFrontViewModel();

            var categoryID = 0;
            if (!string.IsNullOrEmpty(id))
            {
                var arr = id.Split('-');
                if (arr.Length > 0)
                    categoryID = Convert.ToInt32(arr[1]);
                model.BsinessList = apiClient.GetAll<BusinessViewModel>("Business", "GetAllBusinessByCategoryIDForFront", "id=" + categoryID);
                model.Category = apiClient.Get<CategoryViewModel>("Category", "Get", "id=" + categoryID);
            }

            return View("~/Views/Venue/Venues.cshtml", model);
        }
    }
}