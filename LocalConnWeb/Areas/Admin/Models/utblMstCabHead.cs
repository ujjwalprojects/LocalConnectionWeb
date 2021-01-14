using System.ComponentModel.DataAnnotations;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstCabHead
    {
        public long CabHeadID { get; set; }
        [Required(ErrorMessage="Enter Cab Head Name")]
        [Display(Name="Cab Head Name")]
        public string CabHeadName { get; set; }
        [Required(ErrorMessage="Enter Cab Head Contact No")]
        [Display(Name="Contact No")]
        public string CabHeadContact { get; set; }
    }
}