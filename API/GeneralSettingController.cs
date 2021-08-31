using System;
using System.Collections.Generic;
using System.Web.Http;
using xyngr.Models;
using xyngr.Services;

namespace Xyngr.API
{
    public class GeneralSettingController : ApiController
    {
        // GET: api/GeneralSettings
        public IEnumerable<GeneralSettingsViewModel> Get()
        {
            return new DALGeneralSettings().GetAllGeneralSettings(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        // GET: api/Category/5
        public GeneralSettingsViewModel Get(int id)
        {
            return new DALGeneralSettings().GetGeneralSettingsByID(id);
        }

        // POST: api/Category
        public void Post(GeneralSettingsViewModel GeneralSettings)
        {
            new DALGeneralSettings().SaveGeneralSettings(GeneralSettings, "INSERT");
        }

        // PUT: api/Category/5
        public void Put(GeneralSettingsViewModel GeneralSettings)
        {
            new DALGeneralSettings().SaveGeneralSettings(GeneralSettings, "UPDATE");
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            new DALGeneralSettings().MultiProcess("multidelete", Convert.ToString(id));
        }
    }
}
