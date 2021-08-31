using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<UsersViewModel> Get()
        {
           return new DALUsers().GetAllUsers(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET api/<controller>/5
        public UsersViewModel Get(long id)
        {
            return new DALUsers().GetUsersByID(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post(UsersViewModel user)
        {
            Int64 UserID = 0;

            UserID = new DALUsers().SaveUsers(user, "INSERT");

            if(UserID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK); 
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "User already exists with this email");
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(UsersViewModel user)
        {
            Int64 UserID = 0;

            UserID = new DALUsers().SaveUsers(user, "UPDATE");

            if (UserID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "User already exists with this email");
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            new DALUsers().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}