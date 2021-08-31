using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class HomeController : FrontBaseController
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            model.Banners = apiClient.GetAll<BannersViewModel>("Banner", "GetAllBannersForFront", string.Empty);
            model.Business = apiClient.GetAll<BusinessViewModel>("Business", "GetAllBusinessForFrontHome", "NoOfBusiness=3");
            model.PopularBranches = apiClient.GetAll<BranchesViewModel>("Branch", "GetPopularBranchesForFrontHome", string.Empty);
            model.FeaturedBranches = apiClient.GetAll<BranchesViewModel>("Branch", "GetFeaturedBranchesForFrontHome", string.Empty);
            model.NewBranches = apiClient.GetAll<BranchesViewModel>("Branch", "GetNewBranchesForFrontHome", string.Empty);
            model.GeneralSetting = apiClient.Get<GeneralSettingsViewModel>("GeneralSetting", "Get", "id=1");

            return View(model);
        }

        public ActionResult Default()
        {
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult LoadMenu()
        //{

        //    var categoryList = apiClient.GetAll<CategoryViewModel>("Category", "GetAllCategoryForFront", string.Empty);
        //    return PartialView("~/Views/Shared/_MenuPartial.cshtml", categoryList);
        //}


        public ActionResult SearchData(string parentCategoryID, string location , string categoryID)
        {
            var model = new BranchListingForFrontViewModel();

            List<BranchesViewModel> destinations = new List<BranchesViewModel>();
            List<Location> locations = new List<Location>();

            destinations = apiClient.GetAll<BranchesViewModel>("Branch", "GetBranchesForFront", "ParentCategoryID=" + parentCategoryID + "&CategoryID="+ categoryID + "&Location=" + location);

            foreach (var item in destinations)
                locations.Add(new Location() { City = item.City, Country = item.Country, State = item.State });

            model.Locations = locations;
            model.BranchList = destinations;
            ViewBag.isfromSearchPage = true;

            Session["BranchListingForFront"] = JsonConvert.SerializeObject(model);
            return PartialView("~/Views/Destination/_DestinationPartial.cshtml", model);


        }


        public ActionResult GetCategoryByParentIDForFront(int id)
        {
            var categorylist = apiClient.GetAll<CategoryViewModel>("Category", "GetCategoryByParentIDForFront", "id=" + id);

            return PartialView("~/Views/Home/_SubCategoryPartial.cshtml", categorylist);
        }

    }
}