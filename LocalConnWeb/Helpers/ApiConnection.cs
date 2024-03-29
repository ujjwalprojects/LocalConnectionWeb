﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using LocalConnWeb.Models;

namespace LocalConnWeb.Helpers
{
    public class ApiConnection
    {
        string apiurl = ConfigurationManager.AppSettings["ApiURL"];
        string url;
        private string token = "";
        public ApiConnection(string Area)
        {
            if (System.Web.HttpContext.Current != null)
            {
                bool authenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                if (authenticated)
                {
                    if (System.Web.HttpContext.Current.Session != null)
                    {
                        var session_model = System.Web.HttpContext.Current.Session["SessionVar"] as SessionModel;
                        if (session_model != null)
                            token = session_model.AccessToken;
                        //token = "Bearer " + ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("access_token").Value;
                    }
                }
            }

            url = apiurl + Area + "/";
        }
        //public ApiConnection(string endpoint, string Area)
        //{
        //    if (System.Web.HttpContext.Current != null)
        //    {
        //        bool authenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        //        if (authenticated)
        //        {
        //            token = "Bearer " + ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("access_token").Value;
        //        }
        //    }

        //    url = endpoint + Area + "/";
        //}

        public string PostRecordUsingQueryString(string ControllerName, string Actionname, string queryString)
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + queryString;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                Encoding encoding = new UTF8Encoding();

                request.Method = "Post";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                request.ContentLength = 0;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    HandleError = JsonConvert.DeserializeObject<String>(jsonresponse);
                }
                return HandleError;
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                    }
                }
            }
            return HandleError;
        }
        public string PostFilestoApIWithData(string ControllerName, string Actionname, Dictionary<string, string> dict, HttpPostedFileBase file)
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] Bytes = new byte[file.InputStream.Length + 1];
                        file.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                        content.Add(fileContent);
                        foreach (KeyValuePair<string, string> pair in dict)
                        {
                            if (pair.Value != "")
                                content.Add(new StringContent(pair.Value), pair.Key);
                        }
                        var result = httpClient.PostAsync(urlLocal, content).Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            HandleError = result.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //Console.WriteLine("Error code: ", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                        //Console.WriteLine(text);
                    }
                }
            }
            return HandleError;
        }
        //Method Inserts in the Db
        public string PostRecordtoApI(string ControllerName, string Actionname, string jsonString)
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(jsonString);

                request.Method = "Post";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    HandleError = JsonConvert.DeserializeObject<String>(jsonresponse);
                }
                return HandleError;
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                    }
                }
            }
            return HandleError;
        }
        public T PostRecordtoApIForRecord<T>(string ControllerName, string Actionname, string jsonString) where T : new()
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(jsonString);

                request.Method = "Post";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    return JsonConvert.DeserializeObject<T>(jsonresponse);
                }
                return default(T);
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                    }
                }
            }
            return default(T);
        }
        public T GetObjectByKey<T>(string ControllerName, string Actionname, string paramValue, string paramName) where T : new()
        {
            //var model = new();
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + paramName + "=" + paramValue;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    return JsonConvert.DeserializeObject<T>(jsonresponse);
                }
                return default(T);
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
                return default(T);
            }

        }
        public List<T> GetAllRecords<T>(string ControllerName, string Actionname)
        {
            var model = new List<T>();
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    model = JsonConvert.DeserializeObject<List<T>>(jsonresponse);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            return model;
        }
        public List<T> GetRecordsByID<T>(string ControllerName, string Actionname, long id)
        {
            var model = new List<T>();
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?id=" + id;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    model = JsonConvert.DeserializeObject<List<T>>(jsonresponse);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            return model;
        }
        public T GetRecord<T>(string ControllerName, string Actionname) where T : new()
        {
            //var model = new List<T>();
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    return JsonConvert.DeserializeObject<T>(jsonresponse);
                }
                return default(T);
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //Console.WriteLine("Error code: ", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        //Console.WriteLine(text);
                    }
                }
                return default(T);
            }
        }
        public string DeleteRecordByKey(string ControllerName, string Actionname, string paramValue, string paramName)
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + paramName + "=" + paramValue;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);

                request.Method = "Delete";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                request.ContentLength = 0;


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    HandleError = JsonConvert.DeserializeObject<String>(jsonresponse);
                }
                return HandleError;
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                    }
                }
            }
            return HandleError;
        }
        public string DeleteRecordByQuerystring(string ControllerName, string Actionname, string queryString)
        {
            String HandleError = "";
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + queryString;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);

                request.Method = "Delete";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                request.ContentLength = 0;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    HandleError = JsonConvert.DeserializeObject<String>(jsonresponse);
                }
                return HandleError;
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        HandleError = reader.ReadToEnd();
                    }
                }
            }
            return HandleError;
        }
        public T GetRecordByQueryString<T>(string ControllerName, string Actionname, string query) where T : new()
        {
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + query;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    return JsonConvert.DeserializeObject<T>(jsonresponse);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            return default(T);
        }
        public T GetNormalObjectByQueryString<T>(string ControllerName, string Actionname, string query)
        {
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + query;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    return JsonConvert.DeserializeObject<T>(jsonresponse);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            return default(T);
        }
        public List<T> GetRecordsByQueryString<T>(string ControllerName, string Actionname, string query)
        {
            var model = new List<T>();
            try
            {
                string urlLocal = url + ControllerName + "/" + Actionname + "?" + query;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLocal);
                request.Method = "Get";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string s = response.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    String jsonresponse = "";
                    String temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    model = JsonConvert.DeserializeObject<List<T>>(jsonresponse);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as System.Net.HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthorizationException();
                }
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                    }
                }
            }
            return model;
        }
    }
}