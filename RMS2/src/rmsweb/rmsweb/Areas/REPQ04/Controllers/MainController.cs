using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REPQ04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        
        public ActionResult Index()
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            QuestionList_H questionList_H = null;
            List<QuestionList_H> lst = new List<QuestionList_H>();
            DataTable dtDisplay = logic.Search_H(questionList_H, col, condition, value);
            foreach (DataRow dr in dtDisplay.Rows)
            {
                questionList_H = new QuestionList_H();
                questionList_H.ProductNo = dr["ProductNo"].ToString();
                questionList_H.ProductName = dr["ProductName"].ToString();
                questionList_H.ComponentNo = dr["ComponentNo"].ToString();
                questionList_H.MajorComponentName = dr["MajorComponentName"].ToString();
                questionList_H.QTY = dr["QTY"].ToString();
                questionList_H.Lifetime = dr["Lifetime"].ToString();
                lst.Add(questionList_H);
            }
            ViewBag.strQuestionNo = Request.Form["strQuestionNo"];
            ViewBag.strQuestionNo1 = Request.Form["strQuestionNo1"];
            ViewBag.strQuestionNo2 = Request.Form["strQuestionNo2"];
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            ViewData["QuestionList_H"] = lst;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            List<QuestionList_H> lst = new List<QuestionList_H>();
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("問題代號,問題描述,詳細內容,備註");
                    List<QuestionList_H> objQuestionList_H = ViewData["QuestionList_H"] as List<QuestionList_H>;
                    foreach (var obj in objQuestionList_H)
                    {
                        sw.WriteLine(obj.QuestionNo + "," + obj.QuestionTopic + ","
                            + obj.QuestionDetail + "," + obj.Remark);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "QuestionList.csv");
            }
            else if (Request.Form["action"] == "btnSearch")
            {
                    col = Request.Form["strQuestionNo"];
                    condition =Request.Form["strQuestionNo1"];
                    value = Request.Form["strQuestionNo2"];

                QuestionList_H customer = new QuestionList_H();
                customer.ProductNo = Request.Form["ProductNo"];
                customer.ComponentNo = Request.Form["RGA_DPartNo"];
                customer.CustomerId= Request.Form["CustomerId"];
                customer.StatusCode = Request.Form["StatusCode"];
                customer.SubstitutesType= Request.Form["SubstitutesType"];
                customer.RecSDate = Request.Form["RecSDate"];
                customer.RecEDate= Request.Form["RecEDate"];
                DataTable dtDisplay = logic.Search_H(customer, col, condition, value);

                foreach (DataRow dr in dtDisplay.Rows)
                {
                    customer = new QuestionList_H();
                    customer.ProductNo = dr["ProductNo"].ToString();
                    customer.ProductName = dr["ProductName"].ToString();
                    customer.ComponentNo = dr["ComponentNo"].ToString();
                    customer.MajorComponentName = dr["MajorComponentName"].ToString();
                    customer.QTY= dr["QTY"].ToString();
                    customer.Lifetime = dr["Lifetime"].ToString();
                    lst.Add(customer);
                }
                ViewBag.strQuestionNo = Request.Form["strQuestionNo"];
                ViewBag.strQuestionNo1 = Request.Form["strQuestionNo1"];
                ViewBag.strQuestionNo2 = Request.Form["strQuestionNo2"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.ProductNo = Request.Form["ProductNo"];
                ViewBag.CustomerId = Request.Form["CustomerId"];
                ViewBag.RGA_DPartNo = Request.Form["RGA_DPartNo"];
                ViewBag.StatusCode = Request.Form["StatusCode"];
            }
            ViewData["QuestionList_H"] = lst;
            return View();
        }

        //public ActionResult CUR()
        //{
        //    List<Contract_ProductD> objContract_ProductD = new List<Contract_ProductD>();
        //    ViewData["Contract_ProductD"] = objContract_ProductD;
        //    ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
        //    ViewBag.Confirmed = "N:未確認";
        //    ViewBag.Version = "0000";
        //    ViewBag.Discount = "100";
        //    ViewBag.WarrantyPeriod = "0";
        //    return View("CUR");
        //}

        //[HttpPost]
        public ActionResult DetailsP(string ProductNo,string ProductNo1,string ComponentNo,string CustomerId,string StatusCode)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            QuestionList_H questionList_H = null;
            QuestionList_H ques=new QuestionList_H();
            ques.ProductNo = ProductNo;
            List<QuestionList_H> lst = new List<QuestionList_H>();

            DataTable dtDisplay = logic.Search_HCUR(ques);
            foreach (DataRow dr in dtDisplay.Rows)
            {
                questionList_H = new QuestionList_H();
                questionList_H.StatusCode = dr["StatusCode"].ToString();
                switch (dr["StatusCode"].ToString())
                {
                    case "00":
                        questionList_H.StatusCode = "未處理";
                        break;
                    case "05":
                        questionList_H.StatusCode = "已派工";
                        break;
                    case "10":
                        questionList_H.StatusCode = "送廠申請";
                        break;
                    case "20":
                        questionList_H.StatusCode = "已檢測";
                        break;
                    case "25":
                        questionList_H.StatusCode = "送廠維修";
                        break;
                    case "30":
                        questionList_H.StatusCode = "原廠報價";
                        break;
                    case "35":
                        questionList_H.StatusCode = "已報價";
                        break;
                    case "40":
                        questionList_H.StatusCode = "已回復報價";
                        break;
                    case "45":
                        questionList_H.StatusCode = "送廠歸還";
                        break;
                    case "50":
                        questionList_H.StatusCode = "完工";
                        break;
                    case "55":
                        questionList_H.StatusCode = "結案";
                        break;
                }
                questionList_H.RGAType = dr["RGAType"].ToString();
                questionList_H.OrderSName = dr["OrderSName"].ToString();
                questionList_H.RGANo = dr["RGANo"].ToString();
                questionList_H.CustomerName = dr["CustomerName"].ToString();
                questionList_H.ProductNo = dr["ProductNo"].ToString();
                questionList_H.ProductName = dr["ProductName"].ToString();
                questionList_H.SerialNo = dr["SerialNo"].ToString();
                questionList_H.ComponentNo = dr["ComponentNo"].ToString();
                questionList_H.MajorComponentName = dr["MajorComponentName"].ToString();
                questionList_H.QTY = dr["QTY"].ToString();
                questionList_H.Lifetime = dr["Lifetime"].ToString();
                lst.Add(questionList_H);
            }
            ViewBag.ProductNo = ProductNo1;
            ViewBag.ComponentNo = ComponentNo;
            ViewBag.CustomerId = CustomerId;
            if (dtDisplay.Rows.Count > 0)
            {
                switch (dtDisplay.Rows[0]["StatusCode"].ToString())
                {
                    case "00":
                        StatusCode = "未處理";
                        break;
                    case "05":
                       StatusCode = "已派工";
                        break;
                    case "10":
                        StatusCode = "送廠申請";
                        break;
                    case "20":
                        StatusCode = "已檢測";
                        break;
                    case "25":
                        StatusCode = "送廠維修";
                        break;
                    case "30":
                        StatusCode = "原廠報價";
                        break;
                    case "35":
                        StatusCode = "已報價";
                        break;
                    case "40":
                        StatusCode = "已回復報價";
                        break;
                    case "45":
                        StatusCode = "送廠歸還";
                        break;
                    case "50":
                        StatusCode = "完工";
                        break;
                    case "55":
                        StatusCode = "結案";
                        break;
                }
                ViewBag.StatusCode = StatusCode;
            }

            ViewBag.Juect = "DetailsP";
            ViewData["QuestionList_H"] = lst;
            return View("CUR");
        }

        public ActionResult DetailsC(string ProductNo, string ComponentNo, string ComponentNo1, string CustomerId, string StatusCode)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            QuestionList_H questionList_H = null;
            QuestionList_H ques = new QuestionList_H();
            ques.ComponentNo = ComponentNo;
            List<QuestionList_H> lst = new List<QuestionList_H>();

            DataTable dtDisplay = logic.Search_CCUR(ques);
            foreach (DataRow dr in dtDisplay.Rows)
            {
                questionList_H = new QuestionList_H();
                questionList_H.StatusCode = dr["StatusCode"].ToString();
                switch (dr["StatusCode"].ToString())
                {
                    case "00":
                        questionList_H.StatusCode = "未處理";
                        break;
                    case "05":
                        questionList_H.StatusCode = "已派工";
                        break;
                    case "10":
                        questionList_H.StatusCode = "送廠申請";
                        break;
                    case "20":
                        questionList_H.StatusCode = "已檢測";
                        break;
                    case "25":
                        questionList_H.StatusCode = "送廠維修";
                        break;
                    case "30":
                        questionList_H.StatusCode = "原廠報價";
                        break;
                    case "35":
                        questionList_H.StatusCode = "已報價";
                        break;
                    case "40":
                        questionList_H.StatusCode = "已回復報價";
                        break;
                    case "45":
                        questionList_H.StatusCode = "送廠歸還";
                        break;
                    case "50":
                        questionList_H.StatusCode = "完工";
                        break;
                    case "55":
                        questionList_H.StatusCode = "結案";
                        break;
                }
                questionList_H.RGAType = dr["RGAType"].ToString();
                questionList_H.OrderSName = dr["OrderSName"].ToString();
                questionList_H.RGANo = dr["RGANo"].ToString();
                questionList_H.CustomerName = dr["CustomerName"].ToString();
                questionList_H.ProductNo = dr["ProductNo"].ToString();
                questionList_H.ProductName = dr["ProductName"].ToString();
                questionList_H.SerialNo = dr["SerialNo"].ToString();
                questionList_H.ComponentNo = dr["ComponentNo"].ToString();
                questionList_H.MajorComponentName = dr["MajorComponentName"].ToString();
                questionList_H.QTY = dr["QTY"].ToString();
                questionList_H.Lifetime = dr["Lifetime"].ToString();
                lst.Add(questionList_H);
            }
            ViewBag.ProductNo = ProductNo;
            ViewBag.ComponentNo = ComponentNo;
            ViewBag.CustomerId = CustomerId;
            if (dtDisplay.Rows.Count > 0)
            {
                switch (dtDisplay.Rows[0]["StatusCode"].ToString())
                {
                    case "00":
                        StatusCode = "未處理";
                        break;
                    case "05":
                        StatusCode = "已派工";
                        break;
                    case "10":
                        StatusCode = "送廠申請";
                        break;
                    case "20":
                        StatusCode = "已檢測";
                        break;
                    case "25":
                        StatusCode = "送廠維修";
                        break;
                    case "30":
                        StatusCode = "原廠報價";
                        break;
                    case "35":
                        StatusCode = "已報價";
                        break;
                    case "40":
                        StatusCode = "已回復報價";
                        break;
                    case "45":
                        StatusCode = "送廠歸還";
                        break;
                    case "50":
                        StatusCode = "完工";
                        break;
                    case "55":
                        StatusCode = "結案";
                        break;
                }
                ViewBag.StatusCode = StatusCode;
            }

            ViewBag.Juect = "DetailsP";
            ViewData["QuestionList_H"] = lst;
            return View("CUR");
        }

        public ActionResult CUR(string s)
        {
            Contract_HLogic logic = new Contract_HLogic();

            //編輯
            if (Request.Form["action"] == "EditDetails")
            {
                Contract_H contract_H = logic.Contract_HInfo(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.ContractType = contract_H.ContractType;       //合約單別
                ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
                ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
                ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
                ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
                ViewBag.Version = contract_H.Version;           //版次
                ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
                ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
                ViewBag.Discount = contract_H.Discount;           //零件折扣
                ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
                ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
                ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
                ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
                ViewBag.Renewal = contract_H.Renewal;           //續約否
                if (contract_H.Confirmed == "N")//確認碼
                {
                    ViewBag.Confirmed = "N.未確認";
                }
                else if (contract_H.Confirmed == "Y")
                {
                    ViewBag.Confirmed = "Y.已確認";
                }
                else
                {
                    ViewBag.Confirmed = "V.作廢";
                }

                ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
                ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
                ViewBag.Remark = contract_H.Remark;       //備註
                ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
                ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
                ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
                ViewBag.Address = contract_H.Address;       //地址
                ViewBag.Contact = contract_H.Contact;       //聯絡人
                ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
                ViewBag.Tel = contract_H.Tel;       //電話
                ViewBag.Fax = contract_H.Fax;       //傳真
                ViewBag.Email = contract_H.Email;       //E-mail

                Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
                ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.Type = "Edit";
                return View("CUR");
            }
            //複製并新增
            if (Request.Form["action"] == "CopyAddDetails")
            {
                Contract_H contract_H = logic.Contract_HInfo(Request.Form["ContractType"], Request.Form["ContractNo"]);
                //ViewBag.ContractType = contract_H.ContractType;       //合約單別
                //ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
                //ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
                ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
                ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
                ViewBag.Version = contract_H.Version;           //版次
                ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
                ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
                ViewBag.Discount = contract_H.Discount;           //零件折扣
                ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
                ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
                ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
                ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
                ViewBag.Renewal = contract_H.Renewal;           //續約否
                if (contract_H.Confirmed == "N")//確認碼
                {
                    ViewBag.Confirmed = "N.未確認";
                }
                else if (contract_H.Confirmed == "Y")
                {
                    ViewBag.Confirmed = "Y.已確認";
                }
                else
                {
                    ViewBag.Confirmed = "V.作廢";
                }

                ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
                ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
                ViewBag.Remark = contract_H.Remark;       //備註
                ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
                ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
                ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
                ViewBag.Address = contract_H.Address;       //地址
                ViewBag.Contact = contract_H.Contact;       //聯絡人
                ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
                ViewBag.Tel = contract_H.Tel;       //電話
                ViewBag.Fax = contract_H.Fax;       //傳真
                ViewBag.Email = contract_H.Email;       //E-mail

                Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
                ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.ContractType = "";       //合約單別
                ViewBag.ContractNo = "";           //合約單別名稱
                return View("CUR");
            }

            return RedirectToAction("Index");
        }
    }
}