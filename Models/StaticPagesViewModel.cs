using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xyngr.Models
{
    public class StaticPagesViewModel
    {

        public Int64 PageId { get; set; }

        [Required(ErrorMessage = "Page Name is required.")]
        public String PageName { get; set; }

        [Required(ErrorMessage = "Page code is required.")]
        public String PageCode { get; set; }

        [AllowHtml]
        public String PageContent { get; set; }

        public String Image { get; set; }

        public String MetaAuthorContent { get; set; }

        public String MetaDescContent { get; set; }

        public String MetaKeyWordContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Int64 ModifiedBy { get; set; }

        public bool Status { get; set; }
    }
}
