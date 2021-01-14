using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstTourCancellation
    {
        [Key]
        public long CancellationID { get; set; }
        public string CancellationDesc { get; set; }
    }
}