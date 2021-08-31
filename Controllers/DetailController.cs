using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyngr.Models;

namespace Xyngr.Controllers
{
    public class DetailController : FrontBaseController
    {
        // GET: Venue
        public ActionResult Index(string id)
        {
            var model = new BranchesViewModelForDetailPage();
            BranchesViewModel branch = new BranchesViewModel();
            var branchID = 0;
            if (!string.IsNullOrEmpty(id))
            {
                var arr = id.Split('-');
                if (arr.Length > 0)
                    branchID = Convert.ToInt32(arr[1]);
                branch = apiClient.Get<BranchesViewModel>("Branch", "GetBranchesByIDForFront", "id=" + branchID);
            }

            model.Branch = branch;
            model.PopularBranches = apiClient.GetAll<BranchesViewModel>("Branch", "GetPopularBranchesForFrontDetailPage", "branchID=" + model.Branch.BranchID + "&categoryID=" + model.Branch.CategoryID);

            model.RelatedBranches = apiClient.GetAll<BranchesViewModel>("Branch", "GetPopularBranchesForFrontDetailPage", "branchID=" + model.Branch.BranchID + "&categoryID=" + model.Branch.CategoryID +"&location=" + branch.City);


            return View("~/Views/Detail/DestinationDetail.cshtml", model);
        }
    }
}