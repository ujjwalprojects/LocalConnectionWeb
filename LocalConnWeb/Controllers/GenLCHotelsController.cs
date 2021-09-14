using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using LocalConnWeb.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Controllers
{
    public class GenLCHotelsController : Controller
    {
        ApiConnection objAPI = new ApiConnection("general");

        public ActionResult SearchHotels(List<long> HomeType, string Where = "", int PageNo = 1, int PageSize = 10)
        {
            GenLCSearchModel sModel = new GenLCSearchModel()
            {
                HomeType = HomeType,
                Where = Where,
                PageNo = PageNo, 
                PageSize = PageSize
            };
            ViewBag.Where = objAPI.GetAllRecords<string>("webrequest", "WhereLCNames").ToArray();
            string jsonStr = JsonConvert.SerializeObject(sModel);
            GenLCHotelSearchModel model = objAPI.PostRecordtoApIForRecord<GenLCHotelSearchModel>("webrequest", "GenLCHotelSearch", jsonStr);
            model.Search = sModel;
            model.HomeTypes = objAPI.GetAllRecords<utblLCMstHomeType>("webrequest", "HomeTypes");
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvSearchHotelList", model);
            }
            return View(model);
        }
        public ActionResult OfferHotelList(long id)
        {
            GenLCHotelSearchModel model = new GenLCHotelSearchModel();
            List<HotelList_Offer> modellist = objAPI.GetRecordsByID<HotelList_Offer>("webrequest", "getofferhotellist", id);
            model.hotelofferlist = modellist;
            return View(model); 
        }
    }
}