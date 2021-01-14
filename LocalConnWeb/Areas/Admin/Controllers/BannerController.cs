using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class BannerController : BaseController
    {
        // GET: Admin/Banner
       ApiConnection objAPI = new ApiConnection("Admin");
       private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        public ActionResult BannerList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Bannerlist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                BannerAPIVM apiModel = objAPI.GetRecordByQueryString<BannerAPIVM>("configuration", "bannerlist", query);
                BannerVM model = new BannerVM();
                model.BannerList = apiModel.BannerList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvBannerList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddBanner()
        {
            ViewBag.ActiveURL = "/Admin/BannerList";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBanner(SaveBanner model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Bannerlist";
                model.Banner = new utblMstBanner();
                Random rand = new Random();
                string name = "Banner_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
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
                    return RedirectToAction("BannerList", "Banner");
                }
                else
                {
                    model.Banner.BannerPath = "/Uploads/Banner/" + normal_result;
                }

                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Banner);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveBanner", jsonStr);
                    return RedirectToAction("BannerList", "Banner", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditBanner(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/bannerlist";
                SaveBanner model = new SaveBanner();
                model.Banner = objAPI.GetObjectByKey<utblMstBanner>("configuration", "bannerbyid", id.ToString(), "BannerID");
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
        public ActionResult EditBanner(SaveBanner model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/bannerlist";

                if (model.cropper.PhotoNormal != null)
                {
                    bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                    //bool isValidThumb = FileTypeCheck.DataImage(model.cropper.PhotoThumb);
                    if (!isValidFile)
                    {
                        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                        return View();
                    }
                    Random rand = new Random();
                    string name = "Banner_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                    string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                    if (normal_result.Contains("Error"))
                    {
                        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                        return View();
                    }
                    else
                    {
                        model.Banner.BannerPath = "/Uploads/Banner/" + normal_result;
                    }
                }
                else
                {
                    model.Banner.BannerPath = model.Banner.BannerPath;
                }


                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Banner);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savebanner", jsonStr);
                    return RedirectToAction("BannerList", "Banner", new { Area = "Admin" });
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
        public ActionResult DeleteBanner(long BannerID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deletebanner", BannerID.ToString(), "BannerID");
            return RedirectToAction("Bannerlist", "Banner", new { Area = "Admin" });
        }
        private string SaveImage(string imageStr, string name)
        {

            try
            {
                var path = ""; var folderpath = "";
                path = Path.Combine(Server.MapPath("~/Uploads/Banner"), name);
                folderpath = Server.MapPath("~/Uploads/Banner");

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
    }
}