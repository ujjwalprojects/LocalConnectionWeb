using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            else
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }

        }
        //private string razorKey = "rzp_test_O6952intGT2qTL";
        private string razorKey = "rzp_live_st9W6cjqvYYTrO";
        private string razorSecred = "0GnAx9umE3DnA0LdKJMBlLfy";

        public ActionResult Index()
        {
            HomePageVM obj = new HomePageVM();
            obj.cityLists = objAPI.GetRecordsByID<CityList>("webrequest", "getcitylist", 1);
            obj.hotelList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethotellist", 3).Take(5).ToList();
            obj.homestayList = objAPI.GetRecordsByID<HotelList>("webrequest", "gethomestaylist", 4);
            obj.resortList = objAPI.GetRecordsByID<HotelList>("webrequest", "getresortlist", 5);
            obj.lodgeList = objAPI.GetRecordsByID<HotelList>("webrequest", "getlodgelist", 6);
            obj.guestHouseList = objAPI.GetRecordsByID<HotelList>("webrequest", "getghouselist", 7);
            obj.FHotelList = objAPI.GetAllRecords<FtHotelList_Web>("webrequest", "getFHotelList");
            obj.offerLists = objAPI.GetRecordsByQueryString<OfferList>("webrequest", "getofferlist", "Dt=" + DateTime.Now);
            obj.bannerlist = objAPI.GetAllRecords<utblMstBanner>("webrequest", "genbannerlist");
            ViewBag.HomeTypes = objAPI.GetAllRecords<utblLCMstHomeType>("webrequest", "HomeTypes");
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
            ViewBag.startDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.endDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return View(obj);
        }

        public ActionResult HotelBookingDtl(HotelDetailsVM obj)
        {
            HotelDetailsVM model = new HotelDetailsVM();

            obj.preBookDtl.BookingFrom = DateTime.ParseExact(obj.BookFrom, "d/M/yyyy", CultureInfo.InvariantCulture);
            obj.preBookDtl.BookingUpto = DateTime.ParseExact(obj.BookUpTo, "d/M/yyyy", CultureInfo.InvariantCulture);
            model.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtl.HotelID);
            obj.hAmenities = objAPI.GetRecordsByID<HAmenitiesList>("webrequest", "gethamenitieslist", Convert.ToInt64(obj.preBookDtl.HotelID));
            string noOfRooms = obj.preBookDtl.RoomSelect;
            string noOfAdults = obj.preBookDtl.AdultSelect;
            string noOfChild = obj.preBookDtl.ChildrenSelect;
            if (obj.NoofDays == 0)
            { obj.NoofDays = 1; }
            string[] custDetail = obj.preBookDtl.CustDetails.Split(',');
            model.amenitesDisplay = new List<string>();
            for (int i = 0; i < custDetail.Length; i++)
            {
                if (custDetail[i] != "Meals")
                {
                    foreach (var item in obj.hAmenities)
                    {
                        if (item.AmenitiesName.Equals(custDetail[i].Trim()))
                        {
                            model.amenitesDisplay.Add(custDetail[i] + " x " + obj.preBookDtl.AdultSelect + "p" + " x " + obj.NoofDays + "d" + ": " + item.AmenitiesBasePrice.ToString("Rs 0."));
                            break;
                        }

                    }
                }

            }
            string amenitiesdtlstostring = string.Join(" || ", model.amenitesDisplay);


            obj.preBookDtl.CustDetails = noOfRooms + "Room(" + obj.selectedRoomType + ") " + noOfAdults + "(Adult(s)) " + noOfChild + "(Child(s)) " + "{ " + amenitiesdtlstostring + " }";


            model.preBookDtls = obj.preBookDtl;
            model.selectedRoomType = obj.selectedRoomType;
            return View(model);
        }
        [HttpPost]
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
            options.Add("amount", (obj.preBookDtls.FinalFare * 100));  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            ViewBag.RzpID = razorKey;
            ViewBag.TransactionID = transactionId;
            ViewBag.amount = obj.preBookDtls.FinalFare;
            ViewBag.name = obj.preBookDtls.CustName;
            ViewBag.currency = "INR";
            ViewBag.orderDesc = _sModel.UserName ?? obj.preBookDtls.CustName;
            ViewBag.orderid = orderId;
            ViewBag.phone = obj.preBookDtls.CustPhNo;
            ViewBag.email = _sModel.Email ?? obj.preBookDtls.CustEmail;
            model.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtls.HotelID);
            model.preBookDtls = obj.preBookDtls;
            Session["OrderDetails"] = obj.preBookDtls;
            //model.preBookDtl.BookingID = "TEst001";
            //model.preBookDtl.FinalFare = 200;
            //model.preBookDtl.BookingStatus = "Booked";
            //string jsonStr = JsonConvert.SerializeObject(model.preBookDtl);
            //TempData["ErrMsg"] = objAPI.PostRecordtoApI("webrequest", "SendEmail", jsonStr);
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
            PreBookingDtl obj = new PreBookingDtl();
            obj = Session["OrderDetails"] as PreBookingDtl;
            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];



            if (paymentCaptured.Attributes["status"] == "captured")
            {
                PreBookingDtl paymodel = new PreBookingDtl();
                paymodel.CustName = obj.CustName;
                paymodel.CustEmail = obj.CustEmail;
                paymodel.AdultSelect = obj.AdultSelect;
                paymodel.BookingDate = obj.BookingDate;
                paymodel.BookingFrom = obj.BookingFrom;
                paymodel.BookingUpto = obj.BookingUpto;
                paymodel.ChildrenSelect = obj.ChildrenSelect;
                paymodel.CustDetails = obj.CustDetails;
                paymodel.CustPhNo = obj.CustPhNo;
                paymodel.FinalFare = obj.FinalFare;
                paymodel.HotelID = obj.HotelID;
                paymodel.RoomSelect = obj.RoomSelect;
                paymodel.BookingStatus = "Booked";
                paymodel.PaymentGatewayCode = paymentId;
                paymodel.UserID = _sModel.UserID;
                paymodel.UserName = _sModel.UserName;
                string jsonStr = JsonConvert.SerializeObject(paymodel);
                string result = objAPI.PostRecordtoApI("webrequest", "PayNow", jsonStr);
                TempData["ErrMsg"] = result;
                return RedirectToAction("PaymentSuccess", "Home", new { BookingID = result });
            }
            else
            {
                PreBookingDtl paymodel = new PreBookingDtl();
                paymodel.BookingStatus = "Failed";
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
            string jsonStr = JsonConvert.SerializeObject(obj.preBookDtl);
            //TempData["ErrMsg"] = objAPI.PostRecordtoApI("webrequest", "SendEmail", jsonStr);
            //TempData["ErrMsg"] = objAPI.PostRecordtoApI("webrequest", "SendEmailAdmin", jsonStr);
            return View(obj);
        }

        public ActionResult PaymentFailed(long HotelID)
        {
            //if (!(User.Identity.IsAuthenticated))
            //{
            //    return RedirectToAction("Login", "Account", new { Areas = "" });
            //}
            HotelDetailsVM obj = new HotelDetailsVM();
            obj.preBookDtl = Session["OrderDetails"] as PreBookingDtl;
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + 2);
            string jsonStr = JsonConvert.SerializeObject(obj.preBookDtl);
            //TempData["ErrMsg"] = objAPI.PostRecordtoApI("webrequest", "SendEmail", jsonStr);
            return View(obj);
        }


        public ActionResult orderList()
        {
            OrderListVM orderList = new OrderListVM();
            orderList.orderLists = objAPI.GetRecordsByQueryString<OrderList>("webrequest", "getorderlist", "UserID=" + _sModel.UserID);
            return View(orderList);
        }

        public ActionResult orderDetails(string id)
        {
            HotelDetailsVM obj = new HotelDetailsVM();

            obj.preBookDtl = objAPI.GetRecordByQueryString<PreBookingDtl>("webrequest", "getBookingDtl", "BookingID=" + id);
            obj.hotelDtl = objAPI.GetRecordByQueryString<HotelDtl>("webrequest", "gethoteldtl", "HotelID=" + obj.preBookDtl.HotelID);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult orderDetails(HotelDetailsVM model)
        {
            string jsonStr = JsonConvert.SerializeObject(model.preBookDtl.BookingID);
            TempData["ErrMsg"] = objAPI.PostRecordUsingQueryString("webrequest", "cancelbooking", "BookingID="+ model.preBookDtl.BookingID);
            return RedirectToAction("orderList", "home", new { Area = "" });
        }

        public ActionResult About()
        {
            AboutUsDetails model = new AboutUsDetails();
            model = objAPI.GetRecord<AboutUsDetails>("webrequest", "getaboutusdtl");
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult Error(int id)
        {
            ViewBag.StatusCode = id;

            return View();
        }

        public ActionResult RenderCitiesMenuView()
        {
            HomePageVM model = new HomePageVM();
            model.cityLists = objAPI.GetAllRecords<CityList>("webrequest", "getcitylist");
            return PartialView("_pvCitiesMenu", model);
        }
    }
}