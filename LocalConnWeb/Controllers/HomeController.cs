using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.CustomModels;
using LocalConnWeb.Helpers;
using LocalConnWeb.Models;
using LocalConnWeb.ViewModels;
using Newtonsoft.Json;
using Razorpay.Api;

namespace LocalConnWeb.Controllers
{
    public class HomeController : Controller
    {
        ApiConnection objAPI = new ApiConnection("general");
        protected SessionModel _sModel;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sModel = Session["SessionVar"] as SessionModel;
            if (_sModel != null)
            {
                SessionModel sessionModel = Session["SessionVar"] as SessionModel;
                ViewBag.CurrentUserName = sessionModel.UserName;
            }

        }
        private string razorKey = "rzp_test_O6952intGT2qTL";
        private string razorSecred = "P4VmQ2BVBz0x2tLOBsRlhvyY";

        public ActionResult Index()
        {
            HomePageVM obj = new HomePageVM();
            obj.cityLists = objAPI.GetRecordsByID<CityList>("webrequest", "getcitylist", 1);
            obj.hotelList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethotellist", 3);
            obj.homestayList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethomestaylist", 4);
            obj.resortList = objAPI.GetRecordsByID<HotelList>("webrequest", "getresortlist", 5);
            obj.lodgeList = objAPI.GetRecordsByID<HotelList>("webrequest", "getlodgelist", 6);
            obj.guestHouseList = objAPI.GetRecordsByID<HotelList>("webrequest", "getghouselist", 7);
            obj.FHotelList = objAPI.GetRecordsByQueryString<FtHotelList>("webrequest", "getFHotelList", "Dt=" + DateTime.Now.ToString("dd MMM yyyy"));
            return View(obj);
        }

        public ActionResult HotelDetails(string id)
        {
            HotelDetailsVM obj = new HotelDetailsVM();
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + id);
            obj.hAmenities = objAPI.GetRecordsByID<HAmenitiesList>("webrequest", "gethamenitieslist", Convert.ToInt64(id));
            obj.hotelRoomImgTab = objAPI.GetRecordByQueryString<HotelRoomTab>("webrequest", "gethroomimglist", "HotelID=" + id);
            obj.hotelRoomLists = objAPI.GetRecordsByID<HotelRoomList>("webrequest", "gethotelroomlist", Convert.ToInt64(id));
            obj.hotelPremises = objAPI.GetRecordsByID<HotelPremisesList>("webrequest", "gethotelpremises", Convert.ToInt64(id));
            obj.nearByVM = objAPI.GetRecordByQueryString<NearbyVM>("webrequest", "getnearbylist", "HotelID=" + id);
            obj.termCondVM = objAPI.GetRecordByQueryString<TermCondPolicyVM>("webrequest", "gettermncondpolicylist", "HotelID=" + id);
            return View(obj);
        }

        public ActionResult HotelBookingDtl(HotelDetailsVM obj)
        {
            HotelDetailsVM model = new HotelDetailsVM();
            model.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtl.HotelID);
            model.preBookDtl = obj.preBookDtl;
            return View(model);
        }

        public ActionResult PayNow(HotelDetailsVM obj)
        {
            HotelDetailsVM model = new HotelDetailsVM();

            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("Login", "Account", new { Areas = "" });
            }

            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorKey, razorSecred);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", "100");  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            ViewBag.RzpID = razorKey;
            ViewBag.TransactionID = transactionId;
            ViewBag.amount = 1;
            ViewBag.name = obj.preBookDtl.CustName;
            ViewBag.currency = "INR";
            ViewBag.orderDesc = obj.preBookDtl.CustDetails;
            ViewBag.orderid = orderId;
            ViewBag.phone = obj.preBookDtl.CustPhNo;
            model.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtl.HotelID);
            model.preBookDtl = obj.preBookDtl;
            Session["OrderDetails"] = obj.preBookDtl;
            return View(model);
        }

        [HttpPost]
        public ActionResult PaymentComplete()
        {
            string paymentId = Request.Params["rzp_paymentid"];
            // This is orderId
            string orderId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(razorKey, razorSecred);

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            PreBookingDtl obj = Session["OrderDetails"] as PreBookingDtl;
            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];



            if (paymentCaptured.Attributes["status"] == "captured")
            {
                obj.BookingStatus = "Booked";
                obj.PaymentGatewayCode = paymentId;
                string jsonStr = JsonConvert.SerializeObject(obj);
                string result = objAPI.PostRecordtoApI("webrequest", "PayNow", jsonStr);
                TempData["ErrMsg"] = result;
                return RedirectToAction("PaymentSuccess", "Home", new { BookingID = result });
            }
            else
            {
                obj.BookingStatus = "Failed";
                //obj.PaymentGatewayCode = paymentId;
                //string jsonStr = JsonConvert.SerializeObject(obj);
                //string result = objAPI.PostRecordtoApI("webrequest", "PayNow", jsonStr);
                TempData["ErrMsg"] = "Failed";
                return RedirectToAction("PaymentFailed", "Home", new { obj.HotelID });
            }
        }
        public ActionResult PaymentSuccess(string BookingID)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("Login", "Account", new { Areas = "" });
            }
            HotelDetailsVM obj = new HotelDetailsVM();
            obj.preBookDtl = objAPI.GetRecordByQueryString<PreBookingDtl>("webrequest", "getBookingDtl", "BookingID=" + BookingID);
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtl.HotelID);
            return View(obj);
        }

        public ActionResult PaymentFailed(long HotelID)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("Login", "Account", new { Areas = "" });
            }
            HotelDetailsVM obj = new HotelDetailsVM();
            obj.preBookDtl = Session["OrderDetails"] as PreBookingDtl;
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + HotelID);
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