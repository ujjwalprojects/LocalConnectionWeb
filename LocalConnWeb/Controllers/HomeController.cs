using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.CustomModels;
using LocalConnWeb.Helpers;
using LocalConnWeb.ViewModels;

namespace LocalConnWeb.Controllers
{
    public class HomeController : Controller
    {
        ApiConnection objAPI = new ApiConnection("general");
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    IEnumerable<DestinationView> model = objAPI.GetAllRecords<DestinationView>("tourpackage", "destinations");
        //    var groupedDests = (from s in model group s by s.StateName).ToDictionary(x => x.Key, x => x.ToList());
        //    ViewBag.Dests = groupedDests;
        //}

        public ActionResult Index()
        {
            HomePageVM obj = new HomePageVM();
            obj.cityLists= objAPI.GetRecordsByID<CityList>("webrequest", "getcitylist", 1);
            obj.hotelList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethotellist", 3);
            obj.homestayList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethomestaylist", 4);
            obj.resortList = objAPI.GetRecordsByID<HotelList>("webrequest", "getresortlist", 5);
            obj.lodgeList = objAPI.GetRecordsByID<HotelList>("webrequest", "getlodgelist", 6);
            obj.guestHouseList = objAPI.GetRecordsByID<HotelList>("webrequest", "getghouselist", 7);
            obj.FHotelList = objAPI.GetRecordsByQueryString<FtHotelList>("webrequest", "getFHotelList", "Dt="+ DateTime.Now.ToString("dd MMM yyyy"));
            return View(obj);
        }

        public ActionResult HotelDetails(string id)
        {
            HotelDetailsVM obj = new HotelDetailsVM();
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID="+id);
            obj.hAmenities = objAPI.GetRecordsByID<HAmenitiesList>("webrequest", "gethamenitieslist", Convert.ToInt64(id));
            obj.hotelRoomImgTab = objAPI.GetRecordByQueryString<HotelRoomTab>("webrequest", "gethroomimglist", "HotelID=" + id);
            obj.hotelRoomLists = objAPI.GetRecordsByID<HotelRoomList>("webrequest", "gethotelroomlist", Convert.ToInt64(id));
            obj.hotelPremises = objAPI.GetRecordsByID<HotelPremisesList>("webrequest", "gethotelpremises", Convert.ToInt64(id));
            return View(obj);
        }

        public ActionResult About()
        {
          

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}