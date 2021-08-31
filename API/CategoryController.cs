using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class CategoryController : ApiController
    {
        // GET: api/Category
        public IEnumerable<CategoryViewModel> Get()
        {
            return new DALCategory().GetAllCategory(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Category/5
        public CategoryViewModel Get(int id)
        {
            return new DALCategory().GetCategoryByID(id);
        }

        // GET: api/Category/5
        public IEnumerable<CategoryViewModel> GetAllCategoryForFront()
        {
            return new DALCategory().GetAllCategoryForFront();
        }

        // GET: api/Category/5
        public IEnumerable<CategoryViewModel> GetCategoryByParentIDForFront(int id)
        {
            return new DALCategory().GetCategoryByParentIDForFront(id);
        }

        // POST: api/Category
        public HttpResponseMessage Post(CategoryViewModel category)
        {
            Int64 CategoryID = 0;

            CategoryID = new DALCategory().SaveCategory(category, "INSERT");

            if (CategoryID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Category already exists");
            }

        }

        // PUT: api/Category/5
        public HttpResponseMessage Put(CategoryViewModel category)
        {
            Int64 CategoryID = 0;

            CategoryID = new DALCategory().SaveCategory(category, "UPDATE");

            if (CategoryID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Category already exists");
            }
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALCategory().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
