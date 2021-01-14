using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class RoomsView
    {
        public long RoomID { get; set; }
        public string RoomType { get; set; }
        public decimal RoomBaseFare { get; set; }
        public int TotalCapacity { get; set; }
    }
    public class RoomsAPIVM
    {
        public IEnumerable<RoomsView> Rooms { get; set; }
        public int TotalRecords { get; set; }
    }
    public class RoomsVM
    {
        public IEnumerable<RoomsView> Rooms { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class RoomsDD
    {
        public long RoomID { get; set; }
        public string RoomType { get; set; }
    }
    public class RoomsSaveModel
    {
        public utblLCRoom Rooms { get; set; }
        public IEnumerable<RoomsDD> RoomsList { get; set; }
        public Cropper cropper { get; set; }
    }
}