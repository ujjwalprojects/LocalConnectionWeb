using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Helpers;
using LocalConnWeb.Areas.Admin.Models;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class AgentView
    {
        public long AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string AgentEmail { get; set; }
        public string AgentMobile { get; set; }
        public string AgentDocumentPath { get; set; }
        public string Status { get; set; }
    }

    public class AgentAPIVM
    {
        public IEnumerable<AgentView> AgentList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class AgentVM
    {
        public IEnumerable<AgentView> AgentList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class SaveAgent
    {
        public utblAgent Agent { get; set; }
        public Cropper cropper { get; set; }
    }
}