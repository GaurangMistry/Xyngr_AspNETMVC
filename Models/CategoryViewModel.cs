using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class CategoryViewModel
    {

        public Int64 CategoryID { get; set; }

        public Int64 ParentCategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public String CategoryName { get; set; }

        public String ParentCategory { get; set; }

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

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
