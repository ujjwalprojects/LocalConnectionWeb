using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblPolicyPoint
    {
        [Key]
        public long PolicyPointID { get; set; }
        public long PolicyID { get; set; }
        public string PolicyPoints { get; set; }
    }
}