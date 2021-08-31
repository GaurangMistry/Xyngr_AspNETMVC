using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class BusinessViewModel
    {

        public Int64 BusinessID { get; set; }

        public Int64 UserID { get; set; }

        public Int64 ParentCategoryID { get; set; }

        public Int64 CategoryID { get; set; }

        public String ParentCategory { get; set; }

        public String Category { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public String Name { get; set; }

        public String Image { get; set; }

        public String ImageURL { get; set; }

        public String MetaAuthorContent { get; set; }

        public String MetaDescContent { get; set; }

        public String MetaKeyWordContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public int TotalBranch { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }
    }

    public class BusinessListingViewModel
    {
        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public Int64 ParentCategoryID { get; set; }
    }

    public class BusinessListingForFrontViewModel
    {
        public IEnumerable<BusinessViewModel> BsinessList { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
