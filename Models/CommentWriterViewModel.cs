using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class CommentWriterViewModel
    {

        public Int64 CommentWriterID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String PhoneNo { get; set; }

        public String Image { get; set; }

        public String ImageURL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }
    }
}
