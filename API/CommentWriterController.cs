using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class CommentWriterController : ApiController
    {
        // GET: api/CommentWriter
        public IEnumerable<CommentWriterViewModel> Get()
        {
            return new DALCommentWriter().GetAllCommentWriter(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public IEnumerable<CommentWriterViewModel> GetAllCommentWriterForFront()
        {
            return new DALCommentWriter().GetAllCommentWriterForFront(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Category/5
        public CommentWriterViewModel Get(int id)
        {
            return new DALCommentWriter().GetCommentWriterByID(id);
        }

        // POST: api/Category
        public void Post(CommentWriterViewModel CommentWriter)
        {
            new DALCommentWriter().SaveCommentWriter(CommentWriter, "INSERT");
        }

        // PUT: api/Category/5
        public void Put(CommentWriterViewModel CommentWriter)
        {
            new DALCommentWriter().SaveCommentWriter(CommentWriter, "UPDATE");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALCommentWriter().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
