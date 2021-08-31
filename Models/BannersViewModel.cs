using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class BannersViewModel
    {

        public Int64 BannerID { get; set; }

        public Int32 Rating { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public String Title { get; set; }

        public String Comment { get; set; }

        public String Image { get; set; }

        public String ImageURL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public Int64 BranchID { get; set; }

        public String Address1 { get; set; }
    }
}
