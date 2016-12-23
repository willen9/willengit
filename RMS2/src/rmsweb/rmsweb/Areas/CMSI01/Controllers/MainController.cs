using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using rmsweb.Controllers;

namespace CMSI01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            CompanyLogic companyLogic = new CompanyLogic();
            Company company = companyLogic.GetCompany();
            ViewBag.CompanyId = company.CompanyId;
            ViewBag.CompanyName = company.CompanyName;
            ViewBag.CompanyFName = company.CompanyFName;
            ViewBag.TelNo = company.TelNo;
            ViewBag.FaxNo = company.FaxNo;
            ViewBag.Address = company.Address;
            ViewBag.Remark = company.Remark;

            //Company company = new Company();
            //string url = string.Format(ConfigurationManager.ConnectionStrings["strapi"].ToString() + "company/1");
            //var by = new WebClient().DownloadData(url);
            //var result = System.Text.Encoding.UTF8.GetString(by);
            //JObject array = (JObject)JsonConvert.DeserializeObject(result);
            //ViewBag.CompanyId = array["CompanyId"].ToString();
            //ViewBag.CompanyName = array["CompanyName"].ToString();
            //ViewBag.CompanyFName = array["CompanyFName"].ToString();
            //ViewBag.TelNo = array["TelNo"].ToString();
            //ViewBag.FaxNo = array["FaxNo"].ToString();
            //ViewBag.Address = array["Address"].ToString();
            //ViewBag.Remark = array["Remark"].ToString();
            return View();
        }
        
        [HttpPost]
        public JsonResult SaveCompany()
        {
            Company company = new Company();
            company.CompanyId = Request.Form["companyid"];
            company.CompanyName = Request.Form["shortname"];
            company.CompanyFName = Request.Form["fullname"];
            company.TelNo = Request.Form["tel"];
            company.FaxNo = Request.Form["fax"];
            company.Address = Request.Form["address"];
            company.Remark = Request.Form["remark"];

            CompanyLogic companyLogic = new CompanyLogic();

            string msg = "";
            if (!companyLogic.UpdateCompany(company))
            {
                msg = "NO-更新失敗";
            }
            else
            {
                msg = "OK-保存成功";
            }

            //**************************
            //WebClient client = new WebClient();
            //string postData = JsonConvert.SerializeObject(company);
            //byte[] bytes = Encoding.UTF8.GetBytes(postData); //转化为数据流
            //string url = string.Format(ConfigurationManager.ConnectionStrings["strapi"].ToString() + "UpdateCompany");
            //client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            //client.UploadData(url, "POST", bytes);
            //msg = "OK-保存成功";
            //**************************



            //string postData = JsonConvert.SerializeObject(company); //将对象序列化
            //byte[] bytes = Encoding.UTF8.GetBytes(postData); //转化为数据流
            //string url = string.Format(ConfigurationManager.ConnectionStrings["strapi"].ToString() + "company");
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url); //创建HttpWebRequest对象
            //httpWebRequest.Method = "POST";
            //httpWebRequest.ContentLength = bytes.Length;
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            
           
            //using (Stream requestStream = httpWebRequest.GetRequestStream())
            //{
            //    requestStream.Write(bytes, 0, bytes.Count()); //输出流中写入数据
            //}
            //var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //创建响应对象

            //if (httpWebResponse.StatusCode != HttpStatusCode.OK) //判断响应状态码
            //{
            //    //string message = String.Format("POST failed. Received HTTP {0}", httpWebResponse.StatusCode);
            //    //throw new ApplicationException(message);
            //    msg = "NO-更新失敗";
            //}
            //else
            //{
            //    msg = "OK-保存成功";
            //}


            //WebClient web = new WebClient();
            //web.Encoding = Encoding.UTF8;
            //string regUrl = string.Format(ConfigurationManager.ConnectionStrings["strapi"].ToString() + "company");
            ////byte[] sendData = Encoding.GetEncoding("GB2312").GetBytes(company.ToString());
            //web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //web.Headers.Add("ContentLength", bytes.Length.ToString());
            //byte[] recData = web.UploadData(regUrl, "POST", bytes);
            //msg = "OK-保存成功";

            

            //string url = string.Format(ConfigurationManager.ConnectionStrings["strapi"].ToString() + "company");
            //var by = new WebClient().UploadDataAsync(url, "POST", company);
            //var result = System.Text.Encoding.UTF8.GetString(by);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}