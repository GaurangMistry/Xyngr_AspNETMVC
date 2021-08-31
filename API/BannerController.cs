using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class BannerController : ApiController
    {
        // GET: api/Banners
        public IEnumerable<BannersViewModel> Get()
        {
            return new DALBanners().GetAllBanners(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public IEnumerable<BannersViewModel> GetAllBannersForFront()
        {
            return new DALBanners().GetAllBannersForFront();
        }

        // GET: api/Category/5
        public BannersViewModel Get(int id)
        {
            return new DALBanners().GetBannersByID(id);
        }

        // POST: api/Category
        public void Post(BannersViewModel Banners)
        {
            new DALBanners().SaveBanners(Banners, "INSERT");
        }

        // PUT: api/Category/5
        public void Put(BannersViewModel Banners)
        {
            new DALBanners().SaveBanners(Banners, "UPDATE");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALBanners().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
