using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class BusinessController : ApiController
    {
        // GET: api/Business
        public IEnumerable<BusinessViewModel> Get()
        {
            return new DALBusiness().GetAllBusiness(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Business
        public IEnumerable<BusinessViewModel> GetAllBusinessForFront()
        {
            return new DALBusiness().GetAllBusinessForFront();
        }

        // GET: api/Business
        public IEnumerable<BusinessViewModel> GetAllBusinessForFrontHome(int NoOfBusiness)
        {
            return new DALBusiness().GetAllBusinessForFrontHome(NoOfBusiness);
        }

        // GET: api/Business
        public IEnumerable<BusinessViewModel> GetAllBusinessByCategoryID(Int64 id)
        {
            return new DALBusiness().GetAllBusinessByCategoryID(id);
        }

        public IEnumerable<BusinessViewModel> GetAllBusinessByCategoryIDForFront(Int64 id)
        {
            return new DALBusiness().GetAllBusinessByCategoryIDForFront(id);
        }

        // GET: api/Category/5
        public BusinessViewModel Get(int id)
        {
            return new DALBusiness().GetBusinessByID(id);
        }



        // GET: api/Category/5
        public BusinessViewModel GetBusinessByIDForFront(int id)
        {
            var objBusinessViewModel = new BusinessViewModel();

            objBusinessViewModel = new DALBusiness().GetBusinessByID(id);
            objBusinessViewModel.BranchList = new DALBranches().GetAllBranchByBusinessIDForFront(id);

            return objBusinessViewModel;
        }

        // POST: api/Category
        public HttpResponseMessage Post(BusinessViewModel business)
        {
            Int64 BusinessID = 0;

            BusinessID = new DALBusiness().SaveBusiness(business, "INSERT");

            if (BusinessID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Business already exists");
            }
        }

        // PUT: api/Category/5
        public HttpResponseMessage Put(BusinessViewModel business)
        {
            Int64 BusinessID = 0;

            BusinessID = new DALBusiness().SaveBusiness(business, "UPDATE");

            if (BusinessID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Business already exists");
            }
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALBusiness().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
