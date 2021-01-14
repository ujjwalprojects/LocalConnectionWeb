using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class PackageController : BaseController
    {
        //
        // GET: /Admin/Package/
        ApiConnection objAPI = new ApiConnection("Admin");

        #region Package Master
        public ActionResult PackageList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                PackageAPIVM apiModel = objAPI.GetRecordByQueryString<PackageAPIVM>("configuration", "packagelist", query);
                PackageVM model = new PackageVM();
                model.PackageList = apiModel.PackageList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvPackageTypeList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddPackage()
        {
            ViewBag.ActiveURL = "/Admin/packagelist";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPackage(PackageSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.PackageType);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savepackagetype", jsonStr);
                    return RedirectToAction("Packagelist", "package", new { Area = "Admin" });
                }
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
                ViewBag.ActiveURL = "/Admin/packagelist";
                PackageSaveModel model = new PackageSaveModel();
                model.PackageType = objAPI.GetObjectByKey<utblMstPackageType>("configuration", "packagetypebyid", id.ToString(), "PackageTypeID");
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
        public ActionResult Edit(PackageSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.PackageType);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savepackagetype", jsonStr);
                    return RedirectToAction("packagelist", "Package", new { Area = "Admin" });
                }
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
        public ActionResult DeletePackage(long PackageTypeID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deletepackagetype", PackageTypeID.ToString(), "PackageTypeID");
            return RedirectToAction("PackageList", "Package", new { Area = "Admin" });
        }
        #endregion


        #region Package Offer
        public ActionResult PackageOfferList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packageofferlist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                PackageOfferAPIVM apiModel = objAPI.GetRecordByQueryString<PackageOfferAPIVM>("packageconfig", "packageofferlist", query);
                PackageOfferVM model = new PackageOfferVM();
                model.PackageOfferList = apiModel.PackageOfferList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvPackageOfferList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult AddPackageOffer()
        {
            ViewBag.ActiveURL = "/Admin/packageofferlist";
            try
            {

                SavePackageOffer model = new SavePackageOffer();
                model.PackageList = objAPI.GetAllRecords<PackageDD>("packageconfig", "packagelist");
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
        public ActionResult AddPackageOffer(SavePackageOffer model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packageofferlist";
                Random rand = new Random();
                string name = "OfferBanner_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                if (!isValidFile)
                {
                    TempData["ErrMsg"] = "Please Select A Valid Image File!";
                    return View(model);
                }
                string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                if (normal_result.Contains("Error"))
                {
                    TempData["ErrMsg"] = normal_result;
                    return RedirectToAction("AddPackageOffer", "package", new { Area = "Admin" });
                }
                else
                {
                    model.PackageOffer.OfferImagePath = "/Uploads/Offer/" + normal_result;
                }
                //if (ModelState.IsValid)
                //{
                string jsonStr = JsonConvert.SerializeObject(model);
                TempData["ErrMsg"] = objAPI.PostRecordtoApI("packageconfig", "savepackageoffer", jsonStr);
                return RedirectToAction("PackageOfferList", "package", new { Area = "Admin" });
                //}
                //return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditPackageOffer(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                SavePackageOffer model = new SavePackageOffer();
                utblPackageOffer packageoffer = objAPI.GetObjectByKey<utblPackageOffer>("packageconfig", "packageofferbyid", id.ToString(), "PackageOfferID");
                //model.PackageList = objAPI.GetAllRecords<PackageDD>("packageconfig", "packagelist");
                var OfferPkglist = objAPI.GetRecordsByQueryString<long>("packageconfig", "OfferPackagelist", "id=" + id);
                model.PackageOffer = new PackageOffer()
                {
                    PackageOfferID = packageoffer.PackageOfferID,
                    OfferDiscount = packageoffer.OfferDiscount,
                    OfferDesc = HttpUtility.HtmlDecode(packageoffer.OfferDesc),
                    OfferImagePath = packageoffer.OfferImagePath,
                    StartDate = packageoffer.StartDate,
                    EndDate = packageoffer.EndDate,
                    PackageID = OfferPkglist
                };
                model.PackageList = objAPI.GetAllRecords<PackageDD>("packageconfig", "packagelist");
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
        public ActionResult EditPackageOffer(SavePackageOffer model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                if (model.cropper.PhotoNormal != null)
                {
                    bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                    if (!isValidFile)
                    {
                        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                        return View();
                    }
                    Random rand = new Random();
                    string name = "OfferBanner_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                    string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                    if (normal_result.Contains("Error"))
                    {
                        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                        return View();
                    }
                    else
                    {
                        model.PackageOffer.OfferImagePath = "/Uploads/Offer/" + normal_result;
                    }
                }
                else
                {
                    model.PackageOffer.OfferImagePath = model.PackageOffer.OfferImagePath;
                }
                //if (ModelState.IsValid)
                //{
                string jsonStr = JsonConvert.SerializeObject(model);
                TempData["ErrMsg"] = objAPI.PostRecordtoApI("packageconfig", "savepackageoffer", jsonStr);
                return RedirectToAction("PackageOfferList", "Package", new { Area = "Admin" });
                //}
                //return View(model);

            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePackageOffer(long PackageOfferID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("packageconfig", "deletepackageoffer", PackageOfferID.ToString(), "PackageOfferID");
            return RedirectToAction("PackageOfferList", "Package", new { Area = "Admin" });
        }
        private string SaveImage(string imageStr, string name)
        {

            try
            {
                var path = ""; var folderpath = "";
                path = Path.Combine(Server.MapPath("~/Uploads/Offer"), name);
                folderpath = Server.MapPath("~/Uploads/Offer");

                //Check if directory exist
                if (!System.IO.Directory.Exists(folderpath))
                {
                    System.IO.Directory.CreateDirectory(folderpath); //Create directory if it doesn't exist
                }
                string x = imageStr.Replace("data:image/jpeg;base64,", "");
                byte[] imageBytes = Convert.FromBase64String(x);

                System.IO.File.WriteAllBytes(path, imageBytes);
                return name;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        #endregion
    }
}