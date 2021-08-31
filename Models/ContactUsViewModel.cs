using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xyngr.Models
{
    public class ContactUsViewModel
    {

        public StaticPagesViewModel StaticPages { get; set; }
        public GeneralSettingsViewModel GeneralSettings { get; set; }

    }
}
