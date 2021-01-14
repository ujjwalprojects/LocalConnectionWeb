using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Models
{
    public class SessionModel
    {
        public string AccessToken { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserImage { get; set; }
        public string Role { get; set; }
    }
}