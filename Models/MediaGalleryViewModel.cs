using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class MediaGalleryViewModel
    {

        public Int64 MediaID { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }

        public String Business { get; set; }

        public String Branch { get; set; }

        public String VideoType { get; set; }

        [Required(ErrorMessage = "Video Code is required.")]
        public String VideoCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }
    }

    public class MediaGalleryListingViewModel
    {
        public IEnumerable<MediaGalleryViewModel> MediaGalleryList { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }
    }
}
