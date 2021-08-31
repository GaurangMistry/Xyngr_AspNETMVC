using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class AccountController : ApiController
    {
        //// GET: api/Account
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Account/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Account
        public LoginViewModel Post(LoginViewModel model)
        {
            return new DALUsers().LoginValidation(model);
        }

        //// PUT: api/Account/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Account/5
        //public void Delete(int id)
        //{
        //}
    }
}
