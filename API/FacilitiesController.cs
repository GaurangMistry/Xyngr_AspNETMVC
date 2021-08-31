using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class FacilitiesController : ApiController
    {
        // GET: api/Facilities
        public IEnumerable<FacilitiesViewModel> Get()
        {
            return new DALFacilities().GetAllFacilities(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Category/5
        public FacilitiesViewModel Get(int id)
        {
            return new DALFacilities().GetFacilitiesByID(id);
        }

        // POST: api/Category
        public void Post(FacilitiesViewModel Facilities)
        {
            new DALFacilities().SaveFacilities(Facilities, "INSERT");
        }

        // PUT: api/Category/5
        public void Put(FacilitiesViewModel Facilities)
        {
            new DALFacilities().SaveFacilities(Facilities, "UPDATE");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALFacilities().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
