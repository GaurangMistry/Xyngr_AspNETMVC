using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xyngr.Models
{
    public class HomeViewModel
    {

        public IEnumerable<BannersViewModel> Banners { get; set; }
        public IEnumerable<BusinessViewModel> Business { get; set; }
        public IEnumerable<BranchesViewModel> PopularBranches { get; set; }
        public IEnumerable<BranchesViewModel> FeaturedBranches { get; set; }
        public IEnumerable<BranchesViewModel> NewBranches { get; set; }
        public GeneralSettingsViewModel GeneralSetting { get; set; }
    }
}
