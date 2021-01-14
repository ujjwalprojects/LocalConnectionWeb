using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class HotelView
    {
        public long HotelID { get; set; }
        public long DestinationID { get; set; }
        public string DestinationName { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelContact { get; set; }
        public string HotelEmail { get; set; }
        public string HotelTypes { get; set; }
    }
    public class HotelAPIVM
    {
        public IEnumerable<HotelView> Hotels { get; set; }
        public int TotalRecords { get; set; }
    }
    public class HotelVM
    {
        public IEnumerable<HotelView> Hotels { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class HotelSaveModel
    {
        public long HotelID { get; set; }
        [Required(ErrorMessage = "Select Destination")]
        [Display(Name = "Destination")]
        public long DestinationID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Name")]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Enter Hotel Address")]
        [Display(Name = "Hotel Address")]
        public string HotelAddress { get; set; }
        [Required(ErrorMessage = "Enter Hotel Contact No")]
        [Display(Name = "Contact No")]
        public string HotelContact { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string HotelEmail { get; set; }
        [Required(ErrorMessage = "Select at least single Accomodation Type")]
        [Display(Name = "Accomodation Types")]
        public List<long> HotelTypes { get; set; }
    }
    public class HotelManageModel
    {
        public HotelSaveModel Hotel { get; set; }
        public IEnumerable<utblMstDestination> Destinations { get; set; }
        public IEnumerable<utblMstHotelType> HotelTypes { get; set; }
    }
}