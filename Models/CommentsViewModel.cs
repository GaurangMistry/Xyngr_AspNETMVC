using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xyngr.Models
{
    public class CommentsViewModel
    {
        public Int64 CommentID { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }

        public Int64 CommentWriterID { get; set; }

        public String CommentWriterName { get; set; }

        public String CommentWriterImageUrl { get; set; }


        public String Business { get; set; }

        public String Branch { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Comment is required.")]
        public String Comments { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }

        public IEnumerable<CommentWriterViewModel> CommentWriterList { get; set; }

    }

    public class CommentListingViewModel
    {
        public IEnumerable<CommentsViewModel> CommentList { get; set; }

        public IEnumerable<BusinessViewModel> BusinessList { get; set; }

        public IEnumerable<BranchesViewModel> BranchList { get; set; }

        public Int64 BusinessID { get; set; }

        public Int64 BranchID { get; set; }
    }
}
