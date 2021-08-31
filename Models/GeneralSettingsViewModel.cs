using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class GeneralSettingsViewModel
    {

        public Int64 GeneralSettingID { get; set; }

        [Required(ErrorMessage = "Page Name is required.")]
        public String PageTitle { get; set; }

        [Required(ErrorMessage = "Support Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String SupportEmail { get; set; }

        [Required(ErrorMessage = "Support Phone is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String SupportPhone { get; set; }

        [Required(ErrorMessage = "Copyright text is required.")]
        public String CopyrightText { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public String Address { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Country { get; set; }

        public String Longitude { get; set; }

        public String Latitude { get; set; }

        public String GoogleAnalytic { get; set; }

        public String FBLink { get; set; }

        public String GPlusLink { get; set; }

        public String TwitterLink { get; set; }

        public String InstagramLink { get; set; }

        public String YoutubeLink { get; set; }

        public String MetaAuthorContent { get; set; }

        public String MetaDescContent { get; set; }

        public String MetaKeyWordContent { get; set; }
    }
}
