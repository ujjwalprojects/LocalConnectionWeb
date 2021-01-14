using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCMstHomeType
    {
        [Key]
        public long HomeTypeID { get; set; }
        public string HomeTypeName { get; set; }
    }
}