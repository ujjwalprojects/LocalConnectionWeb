using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.CustomModels;
using LocalConnWeb.Helpers;
using LocalConnWeb.Models;
using LocalConnWeb.ViewModels;

namespace LocalConnWeb.Controllers
{
    public class GenTourPackageController : Controller
    {
        ApiConnection objAPI = new ApiConnection("General");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IEnumerable<DestinationView> model = objAPI.GetAllRecords<DestinationView>("tourpackage", "destinations");
            var groupedDests = (from s in model group s by s.StateName).ToDictionary(x => x.Key, x => x.ToList());
            ViewBag.Dests = groupedDests;
        }
        public ActionResult TourPackageList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {

            ViewBag.ActiveURL = "/general/tourpackagelist";
            string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
            GenTourPackageAPIVM apiModel = objAPI.GetRecordByQueryString<GenTourPackageAPIVM>("tourpackage", "GenTourPackageList", query);
            GenTourPackageVM model = new GenTourPackageVM();
            model.PackageList = apiModel.TourPackageList;
            model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvtourpackageList", model);
            }
            return View(model);

        }
        public ActionResult TourPackageDetails(string name)
        {
            ViewBag.ActiveURL = "/general/tourpackagedetails";
            try
            {
                string query;
                query = "linktext=" + name;
                GenTourPackageAPIVM model = new GenTourPackageAPIVM();

                model.GenTourPackageDtlView = objAPI.GetRecordByQueryString<GenTourPackageDtlView>("tourpackage", "GenTourPackageDtl", query);
                long id = model.GenTourPackageDtlView.PackageID;
                model.GenTourPackageCoverImgView = objAPI.GetObjectByKey<GenTourPackageImage>("tourpackage", "GenTourPackageCoverImgView", id.ToString(), "id");
                model.Itineraries = objAPI.GetRecordsByID<PackageItineraryView>("tourpackage", "itinerariesview", id);
                model.Inclusions = objAPI.GetRecordsByID<PackageInclusions>("tourpackage", "packageinclusions", id).Where(x => x.IsSelected).ToList();
                model.Exclusions = objAPI.GetRecordsByID<PackageExclusions>("tourpackage", "packageexclusions", id).Where(x => x.IsSelected).ToList();
                model.AdvActivity = objAPI.GetRecordsByID<PackageActivities>("tourpackage", "PackageActivities", id).Where(x => x.IsSelected).ToList();
                model.GalleryImage = objAPI.GetRecordsByID<GenTourPackageImage>("tourpackage", "PackageGalleryImage", id);
                model.Terms = objAPI.GetRecordsByID<PackageTerms>("tourpackage", "packageterms", id).Where(x => x.IsSelected).ToList();
                model.Cancel = objAPI.GetRecordsByID<PackageCancellations>("tourpackage", "PackageCancellations", id).Where(x => x.IsSelected).ToList();
                model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
                model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
                model.linkname = name;

                return View(model);
            }
           
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitDirectBooking(GenTourPackageAPIVM model)
        {
            try
            {
                model.ClientEnquiry.RefPackageID = model.GenTourPackageDtlView.PackageID;
                model.ClientEnquiry.NoOfDays = model.GenTourPackageDtlView.Days;
                model.ClientEnquiry.IsDirectBooking = true;
                model.ClientEnquiry.TransDate = DateTime.Now;
                string jsonString = JsonConvert.SerializeObject(model.ClientEnquiry);
                string result = objAPI.PostRecordtoApI("clientenquiry", "SaveDirectClientEnq", jsonString);
                if (result.ToLower().Contains("error"))
                {
                    TempData["ErrMsg"] = result;
                    return RedirectToAction("SubmissionPage", "gentourpackage", new { Area = "", result = "failure" });
                }
                return RedirectToAction("SubmissionPage", "gentourpackage", new { Area = "", result = "Success" });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult SubmitCustomBooking(string name)
        {
            try
            {
                string query;
                query = "linktext=" + name;
                GenTourPackageAPIVM model = new GenTourPackageAPIVM();
                model.GenTourPackageDtlView = objAPI.GetRecordByQueryString<GenTourPackageDtlView>("tourpackage", "GenTourPackageDtl", query);
                long id = model.GenTourPackageDtlView.PackageID;
                model.Itineraries = objAPI.GetRecordsByID<PackageItineraryView>("tourpackage", "itinerariesview", id);

                List<PackageItineraryView> itineraries = new List<PackageItineraryView>();
                for (int i = 1; i <= model.GenTourPackageDtlView.Days; i++)
                {
                    PackageItineraryView itinerary = model.Itineraries.Where(x => x.DayNo == i).FirstOrDefault();
                    if (itinerary == null)
                        itinerary = new PackageItineraryView() { PackageID = id, DayNo = i };
                    itineraries.Add(itinerary);
                }

                //List<PackageItinerarypck> it = new List<PackageItinerarypck>();

                //for (int i = 1; i <= model.GenTourPackageDtlView.Days; i++)
                //{
                //    PackageItinerarypck itinerary = model.Itineraries.Where(x => x.DayNo == i).Select(x => new PackageItinerarypck()
                //    {
                //        PackageItineraryID = x.PackageItineraryID,
                //        PackageID = x.PackageID,
                //        ItineraryID = x.ItineraryID,
                //        ItineraryName = x.ItineraryName,
                //        DayNo = x.DayNo,
                //        ItineraryRemarks = x.ItineraryRemarks,
                //        OvernightDestinationID = x.OvernightDestinationID,
                //        DestinationName = x.DestinationName,
                //        OvernightDestination = x.OvernightDestination,
                //        SightSeeing = x.SightSeeing,
                //        Breakfast = x.Breakfast,
                //        Lunch = x.Lunch,
                //        Dinner = x.Dinner,
                //        Stay = x.Stay,
                //        IncludeDay = true,


                //    }).FirstOrDefault();
                //    if (itinerary == null)
                //        itinerary = new PackageItinerarypck() { PackageID = id, DayNo = i };
                //    it.Add(itinerary);
                //}

                //model.Itinerariespck = it;
                model.ItineraryList = objAPI.GetAllRecords<utblMstitinerarie>("clientenquiry", "allitineraries");
                model.DestinationList = objAPI.GetAllRecords<utblMstDestination>("clientenquiry", "alldestinations");
                model.ActivityList = objAPI.GetRecordsByID<PackageActivities>("clientenquiry", "packageactivities", id);
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        //first time client detail insertion
        public ActionResult ClientDetail(string name)
        {
            string query;
            query = "linktext=" + name;
            GenTourPackageAPIVM model = new GenTourPackageAPIVM();
            model.GenTourPackageDtlView = objAPI.GetRecordByQueryString<GenTourPackageDtlView>("tourpackage", "GenTourPackageDtl", query);
            model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
            model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
            model.linkname = name;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientDetail(GenTourPackageAPIVM model)
        {
            try
            {
                model.ClientEnquiry.RefPackageID = model.GenTourPackageDtlView.PackageID;
                //model.ClientEnquiry.NoOfDays = model.GenTourPackageDtlView.Days;
                model.ClientEnquiry.IsDirectBooking = false;
                model.ClientEnquiry.Status = "Open";
                model.ClientEnquiry.TransDate = DateTime.Now;
                string jsonString = JsonConvert.SerializeObject(model.ClientEnquiry);
                string result = objAPI.PostRecordtoApI("clientenquiry", "SaveDirectClientEnq", jsonString);
                string[] enquirycode = result.ToString().Split('_');
                if (enquirycode[0].ToString() == "error")
                {
                    TempData["ErrMsg"] = enquirycode[0].ToString();
                    model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
                    model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ItineraryDetail", "gentourpackage", new { Area = "", ec = enquirycode[1].ToString() });
                }
                //if (result.ToLower().Contains("error"))
                //{
                //    TempData["ErrMsg"] = result;
                //    model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
                //    model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
                //    return View(model);
                //}
                //return RedirectToAction("ItineraryDetail", "gentourpackage", new { Area = "", name = model.linkname });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        //client detail edit
        public ActionResult ClientDetails(string ec)
        {
            string query;
            query = "eqcode=" + ec;
            GenTourPackageAPIVM model = new GenTourPackageAPIVM();
            model.ClientEnquiry = objAPI.GetRecordByQueryString<utblClientEnquirie>("clientenquiry", "GenClientEnquiryInfo", query);
            model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
            model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientDetails(GenTourPackageAPIVM model)
        {
            try
            {
                model.ClientEnquiry.Status = "Open";
                model.ClientEnquiry.TransDate = DateTime.Now;
                string jsonString = JsonConvert.SerializeObject(model.ClientEnquiry);
                string result = objAPI.PostRecordtoApI("clientenquiry", "SaveDirectClientEnq", jsonString);
                string[] enquirycode = result.ToString().Split('_');
                if (enquirycode[0].ToString() == "error")
                {
                    TempData["ErrMsg"] = enquirycode[0].ToString();
                    model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("clientenquiry", "hoteltypes");
                    model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("clientenquiry", "cabtypes");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ItineraryDetail", "gentourpackage", new { Area = "", ec = enquirycode[1].ToString() });
                }
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult ItineraryDetail(string ec)
        {
            try
            {
                string query;
                query = "eqcode=" + ec;
                GenTourPackageAPIVM model = new GenTourPackageAPIVM();
                model.ClientEnquiry = objAPI.GetRecordByQueryString<utblClientEnquirie>("clientenquiry", "GenClientEnquiryInfo", query);
                long id = model.ClientEnquiry.RefPackageID;
                model.Itineraries = objAPI.GetRecordsByID<PackageItineraryView>("tourpackage", "itinerariesview", id);
                model.ClientEnqItinerary = objAPI.GetRecordsByQueryString<utblClientEnquiryItinerarie>("clientenquiry", "GenClientEqItineraryList", query);

                List<utblClientEnquiryItinerarie> clientenquiryitineraries = new List<utblClientEnquiryItinerarie>();

                if (model.ClientEnqItinerary.Count() > 0)
                {
                    //List<utblClientEnquiryItinerarie> itineraries = new List<utblClientEnquiryItinerarie>();
                    for (int i = 1; i <= model.ClientEnquiry.NoOfDays; i++)
                    {
                        utblClientEnquiryItinerarie itinerary = model.ClientEnqItinerary.Where(x => x.DayNo == i).FirstOrDefault();
                        if (itinerary == null)
                            itinerary = new utblClientEnquiryItinerarie() { EnquiryCode = ec, RefPackageID = id, DayNo = i };
                        clientenquiryitineraries.Add(itinerary);
                    }
                }
                else
                {
                    for (int i = 1; i <= model.ClientEnquiry.NoOfDays; i++)
                    {
                        utblClientEnquiryItinerarie itinerary = model.Itineraries.Where(x => x.DayNo == i).Select(x => new utblClientEnquiryItinerarie()
                        {
                            EnquiryCode = model.ClientEnquiry.EnquiryCode,
                            RefPackageID = x.PackageID,
                            DayNo = x.DayNo,
                            ItineraryID = x.ItineraryID,
                            ItineraryRemarks = x.ItineraryRemarks,
                            OvernightDestinationID = x.OvernightDestinationID,
                            OvernightDestination = x.OvernightDestination,
                            SightSeeing = x.SightSeeing,
                            Breakfast = x.Breakfast,
                            Lunch = x.Lunch,
                            Dinner = x.Dinner,
                            Stay = x.Stay

                        }).FirstOrDefault();
                        if (itinerary == null)
                            itinerary = new utblClientEnquiryItinerarie() { EnquiryCode = ec, RefPackageID = id, DayNo = i };
                        clientenquiryitineraries.Add(itinerary);
                    }
                }

                //List<PackageItineraryView> itineraries = new List<PackageItineraryView>();
                //for (int i = 1; i <= model.ClientEnquiry.NoOfDays; i++)
                //{
                //    PackageItineraryView itinerary = model.Itineraries.Where(x => x.DayNo == i).FirstOrDefault();
                //    if (itinerary == null)
                //        itinerary = new PackageItineraryView() { PackageID = id, DayNo = i };
                //    itineraries.Add(itinerary);
                //}

                model.ClientEnqItinerary = clientenquiryitineraries;
                model.ItineraryList = objAPI.GetAllRecords<utblMstitinerarie>("clientenquiry", "allitineraries");
                model.DestinationList = objAPI.GetAllRecords<utblMstDestination>("clientenquiry", "alldestinations");
                model.ActivityList = objAPI.GetRecordsByID<PackageActivities>("clientenquiry", "packageactivities", id);
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItineraryDetail(GenTourPackageAPIVM model)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(model.ClientEnqItinerary);
                string result = objAPI.PostRecordtoApI("clientenquiry", "SaveClientEnqItinerary", jsonString);
                string[] enquirycode = result.ToString().Split('_');
                if (enquirycode[0].ToString() == "error")
                {
                    TempData["ErrMsg"] = enquirycode[0].ToString(); ;
                    return View(model);
                }
                return RedirectToAction("AdvActivityDetail", "gentourpackage", new { Area = "", ec = enquirycode[1].ToString() });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult AdvActivityDetail(string ec)
        {
            try
            {
                string query;
                query = "eqcode=" + ec;
                GenTourPackageAPIVM model = new GenTourPackageAPIVM();
                model.ClientEnquiry = objAPI.GetRecordByQueryString<utblClientEnquirie>("clientenquiry", "GenClientEnquiryInfo", query);
                long id = model.ClientEnquiry.RefPackageID;
                model.ClientEnqActivityList = objAPI.GetRecordsByQueryString<ClientEnqActivities>("clientenquiry", "ClientEnqActivities", query);
                model.ClientEnqActivity = objAPI.GetRecordsByQueryString<utblClientEnquiryActivitie>("clientenquiry", "GenClientEqActivityList", query);

                List<ClientEnqActivities> clientenquiryactivities = new List<ClientEnqActivities>();

                if (model.ClientEnqActivity.Count() == 0)
                {
                    model.ActivityList = objAPI.GetRecordsByID<PackageActivities>("clientenquiry", "packageactivities", id);
                    for (int i = 1; i <= model.ActivityList.Count(); i++)
                    {
                        ClientEnqActivities activity = model.ActivityList.Where(x => x.ActivityID == i).Select(x => new ClientEnqActivities()
                        {
                            EnquiryCode = model.ClientEnquiry.EnquiryCode,
                            RefPackageID = x.PackageID,
                            ActivityID = x.ActivityID,
                            ActivityName = x.ActivityName,
                            ActivityDesc = x.ActivityDesc,
                            IsSelected = x.IsSelected

                        }).FirstOrDefault();
                        if (activity == null)
                            activity = new ClientEnqActivities() { EnquiryCode = ec, RefPackageID = id };
                        clientenquiryactivities.Add(activity);
                    }
                }
                else
                {
                    for (int i = 1; i <= model.ClientEnqActivityList.Count(); i++)
                    {
                        ClientEnqActivities activity = model.ClientEnqActivityList.Where(x => x.ActivityID == i).Select(x => new ClientEnqActivities()
                        {
                            EnquiryCode = model.ClientEnquiry.EnquiryCode,
                            RefPackageID = x.RefPackageID,
                            ActivityID = x.ActivityID,
                            ActivityName = x.ActivityName,
                            ActivityDesc = x.ActivityDesc,
                            IsSelected = x.IsSelected

                        }).FirstOrDefault();
                        if (activity == null)
                            activity = new ClientEnqActivities() { EnquiryCode = ec, RefPackageID = id };
                        clientenquiryactivities.Add(activity);
                    }
                }
                model.ClientEnqActivityList = clientenquiryactivities;
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvActivityDetail(GenTourPackageAPIVM model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                GenTourPackageAPIVM sendmodel = new GenTourPackageAPIVM();
                List<utblClientEnquiryActivitie> clientenquiryactivity = new List<utblClientEnquiryActivitie>();
                sendmodel.ClientEnqActivityList = model.ClientEnqActivityList.Where(x => x.IsSelected).ToList();
                foreach (var item in sendmodel.ClientEnqActivityList)
                {
                    utblClientEnquiryActivitie activity = sendmodel.ClientEnqActivityList.Where(x => x.ActivityID == item.ActivityID).Select(x => new utblClientEnquiryActivitie()
                    {
                        ClientActivityID = x.ClientActivityID,
                        EnquiryCode = x.EnquiryCode,
                        RefPackageID = x.RefPackageID,
                        ActivityID = x.ActivityID

                    }).FirstOrDefault();
                    if (activity == null)
                        activity = new utblClientEnquiryActivitie();
                    clientenquiryactivity.Add(activity);
                }
                sendmodel.ClientEnqActivity = clientenquiryactivity;
                string jsonStr = JsonConvert.SerializeObject(sendmodel.ClientEnqActivity);
                string result = objAPI.PostRecordtoApI("clientenquiry", "SaveClientEnqActivity", jsonStr);
                string[] enquirycode = result.ToString().Split('_');
                if (enquirycode[0].ToString() == "error")
                {
                    TempData["ErrMsg"] = enquirycode[0].ToString();
                    //return RedirectToAction("exclusionterms", new { id = model.Package.PackageID });
                    return View(model);
                }
                return RedirectToAction("ClientPackageSummary", "gentourpackage", new { Area = "", ec = enquirycode[1].ToString() });
                //}
                //return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult ClientPackageSummary(string ec)
        {
            try
            {
                string query = "eqcode=" + ec;
                TourPackageBookingManageModel model = new TourPackageBookingManageModel();
                model.ClientEnqInfoView = objAPI.GetRecordByQueryString<ClientEnquiryView>("clientenquiry", "ClientEnqInfoView", query);
                model.ClientEnqItineraryView = objAPI.GetRecordsByQueryString<ClientEnquiryItineraryView>("clientenquiry", "ClientEnqItineraryView", query);
                model.ClientEnqActivityList = objAPI.GetRecordsByQueryString<ClientEnqActivities>("clientenquiry", "ClientEnqActivities", query).Where(x => x.IsSelected).ToList();
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientPackageSummary(TourPackageBookingManageModel model)
        {
            try
            {
                ClientEnquiryView sendmodel = new ClientEnquiryView();
                sendmodel.EnquiryCode = model.ClientEnqInfoView.EnquiryCode;
                string jsonStr = JsonConvert.SerializeObject(sendmodel);
                string result = objAPI.PostRecordtoApI("clientenquiry", "UpdateClientEnqStatus", jsonStr);
                if (result.ToLower().Contains("error"))
                {
                    TempData["ErrMsg"] = result;
                    return RedirectToAction("SubmissionPage", "gentourpackage", new { Area = "", result = "failure" });
                }
                return RedirectToAction("SubmissionPage", "gentourpackage", new { Area = "", result = model.ClientEnqInfoView.EnquiryCode });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Confirm(string name)
        {
            return View();
        }
        public JsonResult GetItineraryDetails(long id)
        {
            try
            {
                var model = objAPI.GetObjectByKey<utblMstitinerarie>("clientenquiry", "itinerarybyid", id.ToString(), "ItineraryID");
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult SubmissionPage(string result)
        {
            if (result == "Failure")
            {
                return View("SubmissionFailure");
            }
            else
            {
                ViewBag.eqcode = result;
                return View("SubmissionSuccessfull");

            }
        }
        public ActionResult SearchPackage(List<long> TourType, string Where="", int PageNo=1, int PageSize=10)
        {
            GenSearchModel sModel = new GenSearchModel()
            {
                TourType = TourType,
                Where = Where,
                PageNo = PageNo,
                PageSize = PageSize
            };
            ViewBag.Where = objAPI.GetAllRecords<string>("tourpackage", "wherenames").ToArray(); 
            string jsonStr = JsonConvert.SerializeObject(sModel);
            GenTourPackageSearchModel model = objAPI.PostRecordtoApIForRecord<GenTourPackageSearchModel>("tourpackage", "GenTourPackageSearch", jsonStr);
            model.Search = sModel;
            
            model.TourTypes = objAPI.GetAllRecords<utblMstPackageType>("tourpackage", "tourtypes");
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvPackageList", model);
            }
            return View(model);
        }
        public ActionResult PackageOfferDetails(int po, int PageNo = 1, int PageSize = 10)
        {
            string query = "POID=" + po + "&PageNo=" + PageNo + "&PageSize=" + PageSize;
            GenTourPackageAPIVM apiModel = objAPI.GetRecordByQueryString<GenTourPackageAPIVM>("tourpackage", "GenOfferPackageByID", query);
            GenTourPackageVM model = new GenTourPackageVM();
            model.OfferPackageList = apiModel.OfferPackageList;
            model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvofferpackageList", model);
            }
            return View(model);
        }
    }
}