using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class BranchController : ApiController
    {
        // GET: api/Branches
        public IEnumerable<BranchesViewModel> Get()
        {
            return new DALBranches().GetAllBranches(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: Search Branch by Parent Category ID, Category ID or Location
        public IEnumerable<BranchesViewModel> GetBranchesForFront(Int64 ParentCategoryID, Int64 CategoryID, string Location)
        {
            return new DALBranches().GetAllBranchesForFront(ParentCategoryID, CategoryID, Location);
        }

        // GET: Search Branch by Parent Category, Category or Location
        public IEnumerable<BranchesViewModel> GetBranchesForFront(string ParentCategory, string Category, string Location)
        {
            return new DALBranches().GetAllBranchesForFront(ParentCategory, Category, Location);
        }

        public IEnumerable<BranchesViewModel> GetBranchForNotification()
        {
            return new DALBranches().GetBranchForNotification();
        }


        // GET: Search Branch by Parent Category, Category or Location
        public IEnumerable<BranchesViewModel> GetPopularBranchesForFrontHome()
        {
            return new DALBranches().GetPopularBranchesForFrontHome();
        }

        public IEnumerable<BranchesViewModel> GetPopularBranchesForFrontDetailPage(int branchID, int categoryID)
        {
            return new DALBranches().GetPopularBranchesForFrontDetailPage(branchID,categoryID);
        }

        public IEnumerable<BranchesViewModel> GetRelatedBranchesForFrontDetailPage(int branchID, int categoryID, string location)
        {
            return new DALBranches().GetRelatedBranchesForFrontDetailPage( branchID, categoryID,location);
        }

        // GET: Search Branch by Parent Category, Category or Location
        public IEnumerable<BranchesViewModel> GetFeaturedBranchesForFrontHome()
        {
            return new DALBranches().GetFeaturedBranchesForFrontHome();
        }

        public IEnumerable<BranchesViewModel> GetNewBranchesForFrontHome()
        {
            return new DALBranches().GetNewBranchesForFrontHome();
        }

        

        // GET: api/Branches
        public IEnumerable<BranchesViewModel> GetAllBranchByBusinessIDForFront(Int64 id)
        {
            return new DALBranches().GetAllBranchByBusinessIDForFront(id);
        }

        // GET: api/Branches
        public IEnumerable<BranchesViewModel> GetAllBranchByBusinessID(Int64 id)
        {
            return new DALBranches().GetAllBranchByBusinessID(id);
        }

        // GET: api/Branches/5
        public BranchesViewModel Get(int id)
        {
            return new DALBranches().GetBranchesByID(id);
        }

        // GET: Get one Branch detail for Front 
        public BranchesViewModel GetBranchesByIDForFront(int id)
        {
            var objBranchesViewModel = new BranchesViewModel();
            objBranchesViewModel = new DALBranches().GetBranchesByID(id);

            objBranchesViewModel.ImageGallerylist = new DALImageGallery().GetAllImageGalleryByBranchIDForFront(id);
            objBranchesViewModel.MediaGallerylist = new DALMediaGallery().GetAllMediaGalleryByBranchIDForFront(id);
            objBranchesViewModel.Commentlist = new DALComments().GetAllCommentsByBranchIDForFront(id);

            return objBranchesViewModel;
        }

        // POST: api/Branches
        public void Post(BranchesViewModel Branches)
        {
            new DALBranches().SaveBranches(Branches, "INSERT");
        }

        // PUT: api/Branches/5
        public void Put(BranchesViewModel Branches)
        {
            new DALBranches().SaveBranches(Branches, "UPDATE");
        }

        // DELETE: api/Branches/5
        public void Delete(int id)
        {
            new DALBranches().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
