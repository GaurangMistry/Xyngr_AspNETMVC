using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class StaticPageController : ApiController
    {
        // GET: api/StaticPages
        public IEnumerable<StaticPagesViewModel> Get()
        {
            return new DALStaticPages().GetAllStaticPages(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Category/5
        public StaticPagesViewModel Get(int id)
        {
            return new DALStaticPages().GetStaticPagesByID(id);
        }

        public StaticPagesViewModel GetStaticPagesByPageCode(string PageCode)
        {
            return new DALStaticPages().GetStaticPagesByPageCode(PageCode);
        }

        // POST: api/Category
        public void Post(StaticPagesViewModel StaticPages)
        {
            new DALStaticPages().SaveStaticPages(StaticPages, "INSERT");
        }

        // PUT: api/Category/5
        public void Put(StaticPagesViewModel StaticPages)
        {
            new DALStaticPages().SaveStaticPages(StaticPages, "UPDATE");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALStaticPages().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
