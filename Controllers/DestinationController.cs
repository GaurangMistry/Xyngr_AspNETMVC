using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class DestinationController : FrontBaseController
    {
        // GET: Venue
        public ActionResult Index(string id)
        {
            Session["BranchListingForFront"] = null;

            var model = new BranchListingForFrontViewModel();

            List<BranchesViewModel> destinations = new List<BranchesViewModel>();
            List<Location> locations = new List<Location>();
            var businessID = 0;
            if (!string.IsNullOrEmpty(id))
            {
                var arr = id.Split('-');
                if (arr.Length > 0)
                    businessID = Convert.ToInt32(arr[1]);
                destinations = apiClient.GetAll<BranchesViewModel>("Branch", "GetAllBranchByBusinessIDForFront", "id=" + businessID);

                foreach (var item in destinations)
                    locations.Add(new Location() { City = item.City, Country = item.Country, State = item.State });

                model.Locations = locations;
            }

            model.BranchList = destinations;
            model.Business = apiClient.Get<BusinessViewModel>("Business", "Get", "id=" + businessID);
            ViewBag.id = id;
            ViewBag.isfromSearchPage = false;
            return View("~/Views/Destination/Destinations.cshtml", model);
        }

        [HttpPost]
        public ActionResult FilterDestination(string id, List<int> ratings , List<string> locations)
        {
            var model = new BranchListingForFrontViewModel();
            List<BranchesViewModel> destinations = new List<BranchesViewModel>();
            if (Session["BranchListingForFront"] != null)
            {
               var BranchListingForFront = JsonConvert.DeserializeObject<BranchListingForFrontViewModel>((string)Session["BranchListingForFront"]);
                model = BranchListingForFront;
                destinations = model.BranchList.ToList();
                //Session["BranchListingForFront"] = null;
                //Session["BranchListingForFront"] = BranchListingForFront;
            }
            else
            {
                var businessID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    var arr = id.Split('-');
                    if (arr.Length > 0)
                        businessID = Convert.ToInt32(arr[1]);
                    destinations = apiClient.GetAll<BranchesViewModel>("Branch", "GetAllBranchByBusinessIDForFront", "id=" + businessID);

                    
                }
            }

            if (destinations != null && destinations.Count > 0)
            {
                if (ratings != null && ratings.Count > 0)
                    destinations = destinations.Where(x => ratings.Contains(x.Rating)).ToList();

                if (locations != null && locations.Count > 0)
                    destinations = destinations.Where(x => locations.Contains(x.City)).Distinct().ToList();
            }

            model.BranchList = destinations;
            return PartialView("~/Views/Destination/_DestinationListPartial.cshtml", model);
        }

        public ActionResult NotificationDestination()
        {
            List<BranchesViewModel> destinations = new List<BranchesViewModel>();

            destinations = apiClient.GetAll<BranchesViewModel>("Branch", "GetBranchForNotification", string.Empty);

            return PartialView("~/Views/Destination/_DestinationSearchPartial.cshtml", destinations);
        }

    }
}