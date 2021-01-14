using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Models
{
    public class utblClientEnquirie
    {
        [Key]
        public string EnquiryCode { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression("^([0]|\\+91)?\\d{10}", ErrorMessage = "Invalid phone number")]
        public string ClientPhoneNo { get; set; }
        public long RefPackageID { get; set; }
        [Required(ErrorMessage = "Please enter number of days")]
        public int NoOfDays { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Please select your travel date")]
        public DateTime DateOfArrival { get; set; }
        [Required(ErrorMessage = "Please select number of adults travelling")]
        public string NoOfAdult { get; set; }
        [Required(ErrorMessage = "Please select number of children travelling")]
        public string NoOfChildren { get; set; }
        [Required(ErrorMessage = "Please select your preferred hotel type")]
        public long HotelTypeID { get; set; }
        [Required(ErrorMessage = "Please select your preferred cab type")]
        public long CabTypeID { get; set; }
        public bool IsDirectBooking { get; set; }
        public string Status { get; set; }
        public DateTime TransDate { get; set; }
    }
}