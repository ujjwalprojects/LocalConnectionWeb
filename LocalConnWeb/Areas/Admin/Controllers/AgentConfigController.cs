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
    public class AgentConfigController : BaseController
    {
        // GET: Admin/AgentConfig
         ApiConnection objAPI = new ApiConnection("Admin");
         private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        public ActionResult AgentList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/AgentList";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                AgentAPIVM apiModel = objAPI.GetRecordByQueryString<AgentAPIVM>("AgentConfig", "agentlist", query);
                AgentVM model = new AgentVM();
                model.AgentList = apiModel.AgentList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvAgentList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddAgent()
        {
            ViewBag.ActiveURL = "/Admin/Agentlist";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAgent(SaveAgent model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Agentlist";
                
                if (!(model.cropper.PhotoNormal == null || model.cropper.PhotoNormal == ""))
                {
                    Random rand = new Random();
                    string name = model.Agent.AgentName + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".jpg";
                    bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                    string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                    if (normal_result.Contains("Error"))
                    {
                        TempData["ErrMsg"] = normal_result;
                        return RedirectToAction("BannerList", "Banner");
                    }
                    else
                    {
                        model.Agent.AgentDocumentPath = FileUrl + "/AgentDocument/" + normal_result;
                    }
                }
                
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Agent);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("AgentConfig", "saveagent", jsonStr);
                     return RedirectToAction("AgentList", "AgentConfig", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditAgent(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/agentlist";
                SaveAgent model = new SaveAgent();
                model.Agent = objAPI.GetObjectByKey<utblAgent>("AgentConfig", "agentbyid", id.ToString(), "agentID");
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
        public ActionResult EditAgent(SaveAgent model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/agentlist";
                if (!(model.cropper.PhotoNormal == null || model.cropper.PhotoNormal == ""))
                {
                    Random rand = new Random();
                    string name = model.Agent.AgentName + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".jpg";
                    bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                    string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                    if (normal_result.Contains("Error"))
                    {
                        TempData["ErrMsg"] = normal_result;
                        return RedirectToAction("BannerList", "Banner");
                    }
                    else
                    {
                        model.Agent.AgentDocumentPath = FileUrl + "/AgentDocument/" + normal_result;
                    }
                }
             
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Agent);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("AgentConfig", "saveagent", jsonStr);
                    return RedirectToAction("AgentList", "AgentConfig", new { Area = "Admin" });
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
        public ActionResult DeleteAgent(long AgentID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("AgentConfig", "deleteagent", AgentID.ToString(), "AgentID");
             return RedirectToAction("AgentList", "AgentConfig", new { Area = "Admin" });
        }


        private string SaveImage(string imageStr, string name)
        {

            try
            {
                var path = ""; var folderpath = "";
                path = Path.Combine(Server.MapPath("~/UploadedFiles/AgentDocument"), name);
                folderpath = Server.MapPath("~/UploadedFiles/AgentDocument");

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