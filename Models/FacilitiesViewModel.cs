using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class FacilitiesViewModel
    {
        public Int64 FacilityID { get; set; }

        public Int64 ParentCategoryID { get; set; }

        public Int64 CategoryID { get; set; }

        public String ParentCategory { get; set; }

        public String Category { get; set; }

        [Required(ErrorMessage = "Facility is required.")]
        public String Facility { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
