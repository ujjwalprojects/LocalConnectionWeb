using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblPolicie
    {
        [Key]
        public long PolicyID { get; set; }
        public string PolicyTitle { get; set; }
    }
}