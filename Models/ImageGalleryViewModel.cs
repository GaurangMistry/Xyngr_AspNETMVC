using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class ImageGalleryViewModel
    {

        public Int64 ImageID { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }

        public String Business { get; set; }

        public String Branch { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public String Title { get; set; }

        public String Image { get; set; }

        public String ImageURL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }
    }


    public class ImageGalleryListingViewModel
    {
        public IEnumerable<ImageGalleryViewModel> ImageGalleryList { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }
    }
}
