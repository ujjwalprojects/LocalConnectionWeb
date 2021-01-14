using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblAgent
    {
        public long AgentID { get; set; }
        [Required(ErrorMessage="Enter Agent Name")]
        [Display(Name="Agent Name")]
        public string AgentName { get; set; }
        [Required(ErrorMessage = "Enter Agent Address")]
        [Display(Name = "Agent Address")]
        public string AgentAddress { get; set; }
        [Required(ErrorMessage = "Enter Agent Email")]
        [Display(Name = "Agent Email")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string AgentEmail { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Enter Valid Mobile No")]
        public string AgentMobile { get; set; }
        public string AgentDocumentPath { get; set; }
        public string Status { get; set; }
    }
}