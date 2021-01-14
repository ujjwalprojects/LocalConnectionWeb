using System.ComponentModel.DataAnnotations;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstCabDriver
    {
        public long DriverID { get; set; }
        [Required(ErrorMessage="Enter Driver Name")]
        [Display(Name="Driver Name")]
        public string DriverName { get; set; }
        [Required(ErrorMessage="Enter Driver Contact No")]
        [Display(Name="Contact No")]
        public string DriverContact { get; set; }
    }
}