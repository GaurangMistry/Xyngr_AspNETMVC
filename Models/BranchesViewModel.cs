using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xyngr.Models
{
    public class BranchesViewModel
    {
        public Int64 BranchID { get; set; }

        public Int64 BusinessID { get; set; }

        [Required(ErrorMessage = "Landmark is required.")]
        public String Address1 { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public String Address2 { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Country { get; set; }

        public String Longitude { get; set; }

        public String Latitude { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone is required.")]
        public String Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Small description is required.")]
        public String SmallDescription { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Description is required.")]
        public String Description { get; set; }

        [AllowHtml]
        public String OpeningHours { get; set; }

        public int Rating { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsNew { get; set; }

        public String SalePeriod { get; set; }

        public String ProfileImage { get; set; }

        public String ExternalURL { get; set; }

        public Int64 BannerID { get; set; }

        public String MetaAuthorContent { get; set; }

        public String MetaDescContent { get; set; }

        public String MetaKeyWordContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<BusinessViewModel> Businesslist { get; set; }

        public int TotalImage { get; set; }

        public int TotalMedia { get; set; }

        public int TotalComment { get; set; }

        public String Comment { get; set; }

        public String ImageURL { get; set; }

        public String BusinessName { get; set; }

        public String ParentCategory { get; set; }

        public String Category { get; set; }

        public Int32 CategoryID { get; set; }

        public IEnumerable<BannersViewModel> Bannerlist { get; set; }

        public IEnumerable<ImageGalleryViewModel> ImageGallerylist { get; set; }

        public IEnumerable<MediaGalleryViewModel> MediaGallerylist { get; set; }

        public IEnumerable<CommentsViewModel> Commentlist { get; set; }
    }

    public class BranchListingViewModel
    {
        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }
    }

    public class BranchListingForFrontViewModel
    {
        public IEnumerable<BranchesViewModel> BranchList { get; set; }
        public BusinessViewModel Business { get; set; }
        public List<Location> Locations { get; set; }
    }

    public class BranchesViewModelForDetailPage
    {
        public BranchesViewModel Branch { get; set; }

        public List<BranchesViewModel> PopularBranches { get; set; }

        public List<BranchesViewModel> RelatedBranches { get; set; }
    }


    public class Location
    {
        public String City { get; set; }

        public String State { get; set; }

        public String Country { get; set; }

        public String Longitude { get; set; }

        public String Latitude { get; set; }
    }

}
