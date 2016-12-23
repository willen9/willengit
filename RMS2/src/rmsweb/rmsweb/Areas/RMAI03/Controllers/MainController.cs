using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace RMAI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RMAAplLogic logic = new RMAAplLogic();
            string col = ",r.StatusCode";
            string condition = ",<>";
            string value = ",4";
            ViewBag.selCond3 = "<>";
            ViewBag.statuscode = "4";
            ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMAAplLogic logic = new RMAAplLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
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
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單別,單據名稱,單號,客戶簡稱,品號,品名,序號,狀態,建立人員");
                    List<RMAApl> objRMAApl = ViewData["RMAApl"] as List<RMAApl>;
                    foreach (var obj in objRMAApl)
                    {
                        sw.WriteLine(obj.RMAAplType + ","+obj.OrderSName + "," + obj.RMAAplNo + "," + obj.CustomerName + "," 
                            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.StatusCode + "," + obj.CreatorName);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAApl.csv");
            }
            else
            {
                if (Request.Form["rmaapltype"].Trim() != "")
                {
                    col += ",r.RMAAplType";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rmaapltype"];
                }
                if (Request.Form["rmaaplno"].Trim() != "")
                {
                    col += ",r.RMAAplNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["rmaaplno"];
                }
                if (Request.Form["statuscode"].Trim() != "")
                {
                    col += ",r.StatusCode";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["statuscode"];
                }
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.phoneserno = Request.Form["phoneserno"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.comfirmed = Request.Form["comfirmed"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Version = "0000";
            ViewBag.NoOfPrints = "0";
            ViewBag.StatusCode = "0.未處理";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            RMAApl rMAApl = new RMAApl();
            rMAApl.Company = "";
            rMAApl.UserGroup = "";
            rMAApl.Creator = "";
            rMAApl.RMAAplType = Request.Form["RMAAplType"];
            rMAApl.RMAAplNo = Request.Form["RMAAplNo"];
            rMAApl.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rMAApl.Version = Request.Form["Version"];
            rMAApl.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rMAApl.SourceOrderType = Request.Form["SourceOrderType"];
            rMAApl.SourceOrderNo = Request.Form["SourceOrderNo"];
            rMAApl.CustomerId = Request.Form["CustomerId"];
            rMAApl.ProductNo = Request.Form["ProductNo"];
            rMAApl.ProductName = Request.Form["ProductName"];
            rMAApl.ConfirmedDate = Request.Form["Add_sureConfirmDate"].Replace("/","");
            if(rMAApl.ConfirmedDate!="")
            {
                rMAApl.Confirmed = "Y";
                rMAApl.StatusCode = "1";
            }else
            {
                rMAApl.Confirmed = "N";
                rMAApl.StatusCode = "0";
            }
            if (Request.Form["SerialNo"] != "")
            {
                rMAApl.SerialNo = Request.Form["SerialNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    rMAApl.UnderWarranty = "Y";
                }
                else
                {
                    rMAApl.UnderWarranty = "N";
                }
                rMAApl.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rMAApl.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
                rMAApl.VendorWarrantyStartDate = Request.Form["VendorWarrantyStartDate"] == null ? "" : Request.Form["VendorWarrantyStartDate"].ToString().Replace("/", "");
                rMAApl.VendorWarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["VendorWarrantyExpiryDate"].ToString().Replace("/", "");
            }
            rMAApl.OptionDetail = Request.Form["OptionDetail"];
            rMAApl.Remark = Request.Form["Remark"];
            rMAApl.TestResult = Request.Form["TestResult"];
            rMAApl.Returned = Request.Form["Returned"] == null ? "N" : "Y";

            RMAAplLogic logic = new RMAAplLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.AddRMAApl(rMAApl, out infomsg))
            {
                msg = "NO-新增失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Exit()
        {
            RMAApl rMAApl = new RMAApl();
            rMAApl.Company = "";
            rMAApl.UserGroup = "";
            rMAApl.Creator = "";
            rMAApl.RMAAplType = Request.Form["RMAAplType"];
            rMAApl.RMAAplNo = Request.Form["RMAAplNo"];
            rMAApl.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rMAApl.Version = Request.Form["Version"];
            rMAApl.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rMAApl.SourceOrderType = Request.Form["SourceOrderType"];
            rMAApl.SourceOrderNo = Request.Form["SourceOrderNo"];
            rMAApl.CustomerId = Request.Form["CustomerId"];
            rMAApl.ProductNo = Request.Form["ProductNo"];
            rMAApl.ProductName = Request.Form["ProductName"];
            if (Request.Form["SerialNo"] != "")
            {
                rMAApl.SerialNo = Request.Form["SerialNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    rMAApl.UnderWarranty = "Y";
                }
                else
                {
                    rMAApl.UnderWarranty = "N";
                }
                rMAApl.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rMAApl.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
                rMAApl.VendorWarrantyStartDate = Request.Form["VendorWarrantyStartDate"] == null ? "" : Request.Form["VendorWarrantyStartDate"].ToString().Replace("/", "");
                rMAApl.VendorWarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["VendorWarrantyExpiryDate"].ToString().Replace("/", "");
            }
            rMAApl.OptionDetail = Request.Form["OptionDetail"];
            rMAApl.Remark = Request.Form["Remark"];
            rMAApl.TestResult = Request.Form["TestResult"];
            rMAApl.Returned = Request.Form["Returned"] == null ? "N" : "Y";

            RMAAplLogic logic = new RMAAplLogic();

            string msg = "";
            string infomsg = "";
            if (!logic.UpdateRMAApl(rMAApl, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string RMAAplType, string RMAAplNo)
        {
            RMAAplLogic logic = new RMAAplLogic();

            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;

            string msg = "";
            if (!logic.DelRMAApl(rMAApl, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RMAAplType, string RMAAplNo)
        {
            RMAAplLogic logic = new RMAAplLogic();

            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;
            rMAApl = logic.GetRMAAplInfo(rMAApl);
            ViewBag.RMAAplType = rMAApl.RMAAplType;
            ViewBag.RMAAplNo = rMAApl.RMAAplNo;
            ViewBag.OrderDate = rMAApl.OrderDate;
            ViewBag.Version = rMAApl.Version;
            ViewBag.NoOfPrints = rMAApl.NoOfPrints;
            ViewBag.SourceOrderType = rMAApl.SourceOrderType;
            ViewBag.SourceOrderNo = rMAApl.SourceOrderNo;
            ViewBag.CustomerId = rMAApl.CustomerId;
            ViewBag.ProductNo = rMAApl.ProductNo;
            ViewBag.ProductName = rMAApl.ProductName;
            ViewBag.SerialNo = rMAApl.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAApl.VendorWarrantyStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAApl.VendorWarrantyExpiryDate;
            ViewBag.UnderWarranty = rMAApl.UnderWarranty;
            ViewBag.WarrantyStartDate = rMAApl.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAApl.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAApl.Confirmed;
            ViewBag.Closed = rMAApl.Closed;
            ViewBag.StatusCode = rMAApl.StatusCode;
            ViewBag.OptionDetail = rMAApl.OptionDetail;
            ViewBag.Remark = rMAApl.Remark;
            ViewBag.TestResult = rMAApl.TestResult;
            ViewBag.Returned = rMAApl.Returned;
            ViewBag.OrderSName = rMAApl.OrderSName;
            ViewBag.Creator = rMAApl.Creator;
            ViewBag.CreatorName = rMAApl.CreatorName;
            ViewBag.Confirmor = rMAApl.Confirmor;
            ViewBag.ConfirmorName = rMAApl.ConfirmorName;
            ViewBag.CustomerName = rMAApl.CustomerName;
            ViewBag.CustomerType = rMAApl.CustomerType;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RMAAplType, string RMAAplNo)
        {
            RMAAplLogic logic = new RMAAplLogic();

            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;
            rMAApl = logic.GetRMAAplInfo(rMAApl);
            ViewBag.RMAAplType = rMAApl.RMAAplType;
            ViewBag.RMAAplNo = rMAApl.RMAAplNo;
            ViewBag.OrderDate = rMAApl.OrderDate;
            ViewBag.Version = rMAApl.Version;
            ViewBag.NoOfPrints = rMAApl.NoOfPrints;
            ViewBag.SourceOrderType = rMAApl.SourceOrderType;
            ViewBag.SourceOrderNo = rMAApl.SourceOrderNo;
            ViewBag.CustomerId = rMAApl.CustomerId;
            ViewBag.ProductNo = rMAApl.ProductNo;
            ViewBag.ProductName = rMAApl.ProductName;
            ViewBag.SerialNo = rMAApl.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAApl.VendorWarrantyStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAApl.VendorWarrantyExpiryDate;
            ViewBag.UnderWarranty = rMAApl.UnderWarranty;
            ViewBag.WarrantyStartDate = rMAApl.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAApl.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAApl.Confirmed;
            ViewBag.Closed = rMAApl.Closed;
            ViewBag.StatusCode = rMAApl.StatusCode;
            ViewBag.OptionDetail = rMAApl.OptionDetail;
            ViewBag.Remark = rMAApl.Remark;
            ViewBag.TestResult = rMAApl.TestResult;
            ViewBag.Returned = rMAApl.Returned;
            ViewBag.OrderSName = rMAApl.OrderSName;
            ViewBag.Creator = rMAApl.Creator;
            ViewBag.CreatorName = rMAApl.CreatorName;
            ViewBag.Confirmor = rMAApl.Confirmor;
            ViewBag.ConfirmorName = rMAApl.ConfirmorName;
            ViewBag.CustomerName = rMAApl.CustomerName;
            ViewBag.CustomerType = rMAApl.CustomerType;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RMAAplType, string RMAAplNo)
        {
            RMAAplLogic logic = new RMAAplLogic();

            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;
            rMAApl = logic.GetRMAAplInfo(rMAApl);
            ViewBag.RMAAplType = rMAApl.RMAAplType;
            ViewBag.RMAAplNo = rMAApl.RMAAplNo;
            ViewBag.OrderDate = rMAApl.OrderDate;
            ViewBag.Version = rMAApl.Version;
            ViewBag.NoOfPrints = rMAApl.NoOfPrints;
            ViewBag.SourceOrderType = rMAApl.SourceOrderType;
            ViewBag.SourceOrderNo = rMAApl.SourceOrderNo;
            ViewBag.CustomerId = rMAApl.CustomerId;
            ViewBag.ProductNo = rMAApl.ProductNo;
            ViewBag.ProductName = rMAApl.ProductName;
            ViewBag.SerialNo = rMAApl.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAApl.VendorWarrantyStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAApl.VendorWarrantyExpiryDate;
            ViewBag.UnderWarranty = rMAApl.UnderWarranty;
            ViewBag.WarrantyStartDate = rMAApl.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAApl.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAApl.Confirmed;
            ViewBag.Closed = rMAApl.Closed;
            ViewBag.StatusCode = rMAApl.StatusCode;
            ViewBag.OptionDetail = rMAApl.OptionDetail;
            ViewBag.Remark = rMAApl.Remark;
            ViewBag.TestResult = rMAApl.TestResult;
            ViewBag.Returned = rMAApl.Returned;
            ViewBag.OrderSName = rMAApl.OrderSName;
            ViewBag.Creator = rMAApl.Creator;
            ViewBag.CreatorName = rMAApl.CreatorName;
            ViewBag.Confirmor = rMAApl.Confirmor;
            ViewBag.ConfirmorName = rMAApl.ConfirmorName;
            ViewBag.CustomerName = rMAApl.CustomerName;
            ViewBag.CustomerType = rMAApl.CustomerType;
            return View("CUR");
        }
    }
}