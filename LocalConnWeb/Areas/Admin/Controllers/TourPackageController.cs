
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class TourPackageController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];

        #region Configuration
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                TourPackageAPIVM apiModel = objAPI.GetRecordByQueryString<TourPackageAPIVM>("packageconfig", "packagepaged", query);
                TourPackageVM model = new TourPackageVM();
                model.Packages = apiModel.Packages;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvPackageList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Add()
        {
            try
            {
                TourPackageManageModel1 model = new TourPackageManageModel1();
                model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
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
        public ActionResult Add(TourPackageManageModel1 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string linktext = Regex.Replace(model.Package.PackageName, @"[^0-9A-Za-z ,]", "").Replace(" ", "-");
                    linktext = linktext.Replace("--", "-").ToLower();

                    model.Package.LinkText = linktext;
                    string jsonStr = JsonConvert.SerializeObject(model.Package);

                    string result = objAPI.PostRecordtoApI("packageconfig", "savepackage", jsonStr);
                    if (result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = result;
                        model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
                        return View(model);
                    }
                    return RedirectToAction("packageitinerary", new { @id = Convert.ToInt64(result) });
                }
                model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
                ModelState.SetModelValue("Package.PackageDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.Package.PackageDesc), CultureInfo.InvariantCulture));
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Edit(long id)
        {
            try
            {
                TourPackageManageModel1 model = new TourPackageManageModel1();
                model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
                model.Package = objAPI.GetObjectByKey<utblTourPackage>("packageconfig", "packagebyid", id.ToString(), "id");
                model.Package.PackageDesc = HttpUtility.HtmlDecode(model.Package.PackageDesc);
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
        public ActionResult Edit(TourPackageManageModel1 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string linktext = Regex.Replace(model.Package.PackageName, @"[^0-9A-Za-z ,]", "").Replace(" ", "-");
                    linktext = linktext.Replace("--", "-").ToLower();

                    model.Package.LinkText = linktext;
                    string jsonStr = JsonConvert.SerializeObject(model.Package);

                    string result = objAPI.PostRecordtoApI("packageconfig", "savepackage", jsonStr);
                    if (result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = result;
                        model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
                        return View(model);
                    }
                    return RedirectToAction("packageitinerary", new { @id = Convert.ToInt64(result) });
                }
                ModelState.SetModelValue("Package.PackageDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.Package.PackageDesc), CultureInfo.InvariantCulture));
                model.PackageTypes = objAPI.GetAllRecords<utblMstPackageType>("configuration", "allpackagetypes");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult PackageItinerary(long id)
        {
            try
            {
                TourPackageManageModel2 model = new TourPackageManageModel2();
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", id.ToString(), "id");
                model.Itineraries = objAPI.GetRecordsByID<utblTourPackageItinerary>("packageconfig", "packageitineraries", id);
                //if (model.Itineraries.Count() == 0)
                //{
                List<utblTourPackageItinerary> itineraries = new List<utblTourPackageItinerary>();
                for (int i = 1; i <= model.Package.TotalDays; i++)
                {
                    utblTourPackageItinerary itinerary = model.Itineraries.Where(x => x.DayNo == i).FirstOrDefault();
                    if (itinerary == null)
                        itinerary = new utblTourPackageItinerary() { PackageID = id, DayNo = i };
                    itineraries.Add(itinerary);
                }
                model.Itineraries = itineraries;
                //}
                model.ItineraryList = objAPI.GetAllRecords<utblMstitinerarie>("configuration", "allitineraries");
                model.DestinationList = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
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
        public ActionResult PackageItinerary(TourPackageManageModel2 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Itineraries);
                    string result = objAPI.PostRecordtoApI("packageconfig", "saveitineraries", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "Tour Package Itineraries saved";
                        return RedirectToAction("images", new { id = model.Package.PackageID });
                    }
                    TempData["ErrMsg"] = result;
                }
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Package.PackageID.ToString(), "id");
                model.ItineraryList = objAPI.GetAllRecords<utblMstitinerarie>("configuration", "allitineraries");
                model.DestinationList = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Images(long id, int PageNo = 1, int PageSize = 10)
        {
            try
            {
                string query = "PackageID=" + id + "&PageNo=" + PageNo + "&PageSize=" + PageSize;
                TourPackageImageAPIVM apiModel = objAPI.GetRecordByQueryString<TourPackageImageAPIVM>("packageconfig", "packageimages", query);
                TourPackageImageVM model = new TourPackageImageVM();
                model.PackageImages = apiModel.PackageImages;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", id.ToString(), "id");
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvImageList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult AddImage(long PackageID)
        {
            try
            {
                PackageImageSaveModel model = new PackageImageSaveModel();
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", PackageID.ToString(), "id");
                model.Image = new utblTourPackageImage() { PackageID = PackageID };
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
        public ActionResult AddImage(PackageImageSaveModel model)
        {
            try
            {
                var validData = true;
                if (model.ImageStrs.PhotoThumb == null || model.ImageStrs.PhotoThumb == null)
                {
                    ModelState.AddModelError("PhotoStrs.PhotoThumb", "Select a Photo");
                    validData = false;
                }
                if (validData && ModelState.IsValid)
                {
                    Random rand = new Random();
                    string name = model.Image.PackageID + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                    string normal_result = SaveImage(model.ImageStrs.PhotoNormal, model.ImageStrs.PhotoThumb, name);
                    if (normal_result.Contains("Error"))
                    {
                        TempData["ErrMsg"] = normal_result;
                        model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Image.PackageID.ToString(), "id");
                        return View(model);
                    }
                    model.Image.PhotoNormalPath = FileUrl + "Photos/Normal/" + normal_result;
                    model.Image.PhotoThumbPath = FileUrl + "Photos/Thumb/" + normal_result;

                    string jsonStr = JsonConvert.SerializeObject(model.Image);
                    string result = objAPI.PostRecordtoApI("packageconfig", "saveimage", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "New Photo Added";
                        return RedirectToAction("images", "tourpackage", new { Area = "Admin", id = model.Image.PackageID });
                    }
                    TempData["ErrMsg"] = result;
                    DeleteFile(name);
                }
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Image.PackageID.ToString(), "id");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditImage(long PackageID, long ImageID)
        {
            try
            {
                PackageImageSaveModel model = new PackageImageSaveModel();
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", PackageID.ToString(), "id");
                model.Image = objAPI.GetObjectByKey<utblTourPackageImage>("packageconfig", "imagebyid", ImageID.ToString(), "id");
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
        public ActionResult EditImage(PackageImageSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string name = "";
                    if (model.ImageStrs.PhotoNormal != null)
                    {
                        Random rand = new Random();
                        name = model.Image.PackageID + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                        string normal_result = SaveImage(model.ImageStrs.PhotoNormal, model.ImageStrs.PhotoThumb, name);
                        if (normal_result.Contains("Error"))
                        {
                            TempData["ErrMsg"] = normal_result;
                            model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Image.PackageID.ToString(), "id");
                            return View(model);
                        }
                        model.Image.PhotoNormalPath = FileUrl + "Photos/Normal/" + normal_result;
                        model.Image.PhotoThumbPath = FileUrl + "Photos/Thumb/" + normal_result;
                    }

                    string jsonStr = JsonConvert.SerializeObject(model.Image);
                    string result = objAPI.PostRecordtoApI("packageconfig", "saveimage", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "Photo Updated";
                        return RedirectToAction("images", "tourpackage", new { Area = "Admin", id = model.Image.PackageID });
                    }
                    TempData["ErrMsg"] = result;
                    DeleteFile(name);
                }
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Image.PackageID.ToString(), "id");
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
        public ActionResult DeleteImage(long PackageID, long ImageID)
        {
            try
            {
                string result = objAPI.DeleteRecordByKey("packageconfig", "deleteimage", ImageID.ToString(), "id");
                if (result.Contains("Error"))
                    TempData["ErrMsg"] = result;
                else
                {
                    if (!(result == null || result == ""))
                        DeleteFile(result);
                    TempData["ErrMsg"] = "Photo Removed";
                }
                return RedirectToAction("images", "tourpackage", new { Area = "Admin", id = PackageID });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult MakeCoverImage(long PackageID, long ImageID)
        {
            string query = "PackageID=" + PackageID + "&ImageID=" + ImageID;
            TempData["ErrMsg"] = objAPI.PostRecordUsingQueryString("packageconfig", "makecoverimage", query);
            return RedirectToAction("images", "tourpackage", new { Area = "Admin", id = PackageID });
        }

        public ActionResult ActivityInclusions(long id)
        {
            try
            {
                TourPackageManageModel3 model = new TourPackageManageModel3();
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", id.ToString(), "id");
                model.Activities = objAPI.GetRecordsByID<PackageActivities>("packageconfig", "packageactivities", id);
                model.Inclusions = objAPI.GetRecordsByID<PackageInclusions>("packageconfig", "packageinclusions", id);
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
        public ActionResult ActivityInclusions(TourPackageManageModel3 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TourPackageManageModel3 sendModel = new TourPackageManageModel3();
                    sendModel.Inclusions = model.Inclusions.Where(x => x.IsSelected).ToList();
                    sendModel.Activities = model.Activities.Where(x => x.IsSelected).ToList();
                    sendModel.PackageID = model.Package.PackageID;

                    string jsonStr = JsonConvert.SerializeObject(sendModel);
                    string result = objAPI.PostRecordtoApI("packageconfig", "saveactivityinclusion", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = result;
                        return RedirectToAction("exclusionterms", new { id = model.Package.PackageID });
                    }
                    TempData["ErrMsg"] = result;
                }
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Package.PackageID.ToString(), "id");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult ExclusionTerms(long id)
        {
            try
            {
                TourPackageManageModel4 model = new TourPackageManageModel4();
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", id.ToString(), "id");
                model.Exclusions = objAPI.GetRecordsByID<PackageExclusions>("packageconfig", "packageexclusions", id);
                model.Terms = objAPI.GetRecordsByID<PackageTerms>("packageconfig", "packageterms", id);
                model.Cancellations = objAPI.GetRecordsByID<PackageCancellations>("packageconfig", "packagecancellations", id);
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
        public ActionResult ExclusionTerms(TourPackageManageModel4 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TourPackageManageModel4 sendModel = new TourPackageManageModel4();

                    if (model.Exclusions == null)
                        sendModel.Exclusions = new List<PackageExclusions>();
                    else
                        sendModel.Exclusions = model.Exclusions.Where(x => x.IsSelected).ToList();

                    if (model.Terms == null)
                        sendModel.Terms = new List<PackageTerms>();
                    else
                        sendModel.Terms = model.Terms.Where(x => x.IsSelected).ToList();


                    if (model.Cancellations == null)
                        sendModel.Cancellations = new List<PackageCancellations>();
                    else
                        sendModel.Cancellations = model.Cancellations.Where(x => x.IsSelected).ToList();
                    sendModel.PackageID = model.Package.PackageID;

                    string jsonStr = JsonConvert.SerializeObject(sendModel);
                    string result = objAPI.PostRecordtoApI("packageconfig", "saveexclusionterms", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "Tour Package Details Saved";
                        return RedirectToAction("index");
                    }
                    TempData["ErrMsg"] = result;
                }
                model.Package = objAPI.GetObjectByKey<PackageBriefInfo>("packageconfig", "PackageBrief", model.Package.PackageID.ToString(), "id");
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
        public ActionResult Delete(long id)
        {
            try
            {
                string result = objAPI.DeleteRecordByKey("packageconfig", "deletepackage", id.ToString(), "id");
                if (result.Contains("Error"))
                    TempData["ErrMsg"] = result;
                else
                {
                    DeleteAlbumFiles(id);
                    TempData["ErrMsg"] = "Tour Package Removed";
                }
                return RedirectToAction("index", "tourpackage", new { Area = "Admin" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Details(long id)
        {
            try
            {
                TourPackageFullVM model = new TourPackageFullVM();
                model.Package = objAPI.GetObjectByKey<TourPackageView>("packageconfig", "packageview", id.ToString(), "id");
                model.Itineraries = objAPI.GetRecordsByID<PackageItineraryView>("packageconfig", "itinerariesview", id);
                string query = "PackageID=" + id + "&PageNo=1&PageSize=10";
                model.Images = objAPI.GetRecordByQueryString<TourPackageImageAPIVM>("packageconfig", "packageimages", query).PackageImages;
                model.Activities = objAPI.GetRecordsByID<PackageActivities>("packageconfig", "packageactivities", id).Where(x => x.IsSelected).ToList();
                model.Inclusions = objAPI.GetRecordsByID<PackageInclusions>("packageconfig", "packageinclusions", id).Where(x => x.IsSelected).ToList();
                model.Exclusions = objAPI.GetRecordsByID<PackageExclusions>("packageconfig", "packageexclusions", id).Where(x => x.IsSelected).ToList();
                model.Terms = objAPI.GetRecordsByID<PackageTerms>("packageconfig", "packageterms", id).Where(x => x.IsSelected).ToList();
                model.Cancellations = objAPI.GetRecordsByID<PackageCancellations>("packageconfig", "PackageCancellations", id).Where(x => x.IsSelected).ToList();
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public JsonResult GetItineraryDetails(long id)
        {
            try
            {
                var model = objAPI.GetObjectByKey<utblMstitinerarie>("configuration", "itinerarybyid", id.ToString(), "ItineraryID");
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Helper
        private string SaveImage(string imageStrNormal, string imageStrThumb, string name)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Uploads/Photos/Normal"), name);
                var folderpath = Server.MapPath("~/Uploads/Photos/Normal");

                //Check if normal directory exist
                if (!System.IO.Directory.Exists(folderpath))
                {
                    System.IO.Directory.CreateDirectory(folderpath); //Create directory if it doesn't exist
                }
                string x = imageStrNormal.Replace("data:image/jpeg;base64,", "");
                byte[] imageBytes = Convert.FromBase64String(x);

                System.IO.File.WriteAllBytes(path, imageBytes);

                var thumb_path = Path.Combine(Server.MapPath("~/Uploads/Photos/Thumb"), name);
                var thumb_folderpath = Server.MapPath("~/Uploads/Photos/Thumb");

                //Check if thumb directory exist
                if (!System.IO.Directory.Exists(thumb_folderpath))
                {
                    System.IO.Directory.CreateDirectory(thumb_folderpath); //Create directory if it doesn't exist
                }
                string thumb_x = imageStrThumb.Replace("data:image/jpeg;base64,", "");
                byte[] thumb_imageBytes = Convert.FromBase64String(thumb_x);

                System.IO.File.WriteAllBytes(thumb_path, thumb_imageBytes);

                return name;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private void DeleteFile(string filepath)
        {
            try
            {
                string[] fileArr = filepath.Split('/');
                string filename = fileArr[fileArr.Length - 1];
                string serverPath = "";
                serverPath = Path.Combine(Server.MapPath("~/Uploads/Photos/Normal"), filename);
                if (System.IO.File.Exists(serverPath))
                {
                    System.IO.File.Delete(serverPath);
                }

                string thumb_serverPath = "";
                thumb_serverPath = Path.Combine(Server.MapPath("~/Uploads/Photos/Thumb"), filename);
                if (System.IO.File.Exists(thumb_serverPath))
                {
                    System.IO.File.Delete(thumb_serverPath);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void DeleteAlbumFiles(long id)
        {
            try
            {
                string serverNormalPath = @"" + Server.MapPath("~/Uploads/Photos/Normal");
                string albumfilename = @"" + id + "_";
                string[] NormalfileList = System.IO.Directory.GetFiles(serverNormalPath, albumfilename + "*.jpg");
                foreach (string file in NormalfileList)
                {
                    //System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion

        #region Enquiries
        public ActionResult Enquiries(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                TourPackageBookEnquiryAPIVM apiModel = objAPI.GetRecordByQueryString<TourPackageBookEnquiryAPIVM>("client", "enquiries", query);
                TourPackageBookEnquiryVM model = new TourPackageBookEnquiryVM();
                model.Enquiries = apiModel.Enquiries;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvEnquiryList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EnquiryDtls(string code)
        {
            try
            {
                EnquiryVM model = new EnquiryVM();
                model.Enquiry = objAPI.GetObjectByKey<TourPackageBookEnquiry>("client", "enquirybycode", code, "code");
                model.Itineraries = objAPI.GetRecordsByQueryString<EnquiryItineraryView>("client", "enquiryitineraries", "code=" + code);
                model.Activities = objAPI.GetRecordsByQueryString<EnquiryActivitiesView>("client", "EnquiryActivities", "code=" + code);
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        #endregion
    }
}