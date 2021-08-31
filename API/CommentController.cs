using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class CommentController : ApiController
    {
        // GET: api/Comments
        public IEnumerable<CommentsViewModel> Get()
        {
            return new DALComments().GetAllComments(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public IEnumerable<CommentsViewModel> GetAllCommentsByBranchID(Int64 id)
        {
            return new DALComments().GetAllCommentsByBranchID(id);
        }

        // GET: api/Comments/5
        public CommentsViewModel Get(int id)
        {
            return new DALComments().GetCommentsByID(id);
        }

        // POST: api/Comments
        public void Post(CommentsViewModel Comments)
        {
            new DALComments().SaveComments(Comments, "INSERT");
        }

        // PUT: api/Comments/5
        public void Put(CommentsViewModel Comments)
        {
            new DALComments().SaveComments(Comments, "UPDATE");
        }

        // DELETE: api/Comments/5
        public void Delete(int id)
        {
            new DALComments().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
