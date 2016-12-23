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

namespace RMAI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RMA_HLogic logic = new RMA_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RMA_H"] = logic.SearchRMA_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMA_HLogic logic = new RMA_HLogic();
            RMA_H rMA_H = new RMA_H();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMA_H"] = logic.SearchRMA_H(col, condition, value);
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

                ViewData["RMA_H"] = logic.SearchRMA_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單別,單據名稱,單號,廠商,狀態,送廠人員");
                    List<RMA_H> objRMA_H = ViewData["RMA_H"] as List<RMA_H>;
                    foreach (var obj in objRMA_H)
                    {
                        sw.WriteLine(obj.RMAType + ","+obj.OrderSName + "," + obj.RMANo + "," + obj.VendorName + "," + obj.Confirmed + "," + obj.ConfirmorName);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMA.csv");
            }
            else
            {
                if (Request.Form["rmatype"].Trim() != "")
                {
                    col += ",RMAType";
                    condition += ",=";
                    value += "," + Request.Form["rmatype"];
                }
                if (Request.Form["rmano"].Trim() != "")
                {
                    col += ",RMANo";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["rmano"];
                }
                ViewData["RMA_H"] = logic.SearchRMA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rmatype = Request.Form["rmatype"];
                ViewBag.rmano = Request.Form["rmano"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Confirmed = "N:未送廠";
            ViewBag.NoOfPrints = "0";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = Request.Form["RMAType"];
            rMA_H.RMANo = Request.Form["RMANo"];
            rMA_H.OrderDate = Request.Form["OrderDate"].Replace("/","");
            rMA_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rMA_H.VendorOrderNo = Request.Form["VendorOrderNo"];
            rMA_H.ShippingNo = Request.Form["ShippingNo"];
            rMA_H.Remark = Request.Form["Remark"];
            rMA_H.VendorId = Request.Form["VendorId"];
            rMA_H.Contact = Request.Form["Contact"];
            rMA_H.TelNo = Request.Form["TelNo"];
            rMA_H.FaxNo = Request.Form["FaxNo"];
            rMA_H.Address = Request.Form["Address"];
            rMA_H.Amount = Request.Form["Amount"];

            string strItemId= Request.Form["strCItemId"];
            string strSourceOrderType = Request.Form["strCSourceOrderType"];
            string strSourceOrderNo = Request.Form["strCSourceOrderNo"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strRemark = Request.Form["strCRemark"];
            string strTestResult = Request.Form["strCTestResult"];
            string strReturned = Request.Form["strCReturned"];
            string strClosed = Request.Form["strCClosed"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];

            RMA_HLogic logic = new RMA_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRMA_H(rMA_H, strItemId,
             strSourceOrderType, strSourceOrderNo,
             strProductNo, strProductName, strSerialNo,
             strRemark, strTestResult, strReturned,
             strClosed, strRGAType, strRGANo, out infomsg))
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
            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = Request.Form["RMAType"];
            rMA_H.RMANo = Request.Form["RMANo"];
            rMA_H.OrderDate = Request.Form["OrderDate"];
            rMA_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rMA_H.VendorOrderNo = Request.Form["VendorOrderNo"];
            rMA_H.ShippingNo = Request.Form["ShippingNo"];
            rMA_H.Remark = Request.Form["Remark"];
            rMA_H.VendorId = Request.Form["VendorId"];
            rMA_H.Contact = Request.Form["Contact"];
            rMA_H.TelNo = Request.Form["TelNo"];
            rMA_H.FaxNo = Request.Form["FaxNo"];
            rMA_H.Address = Request.Form["Address"];
            rMA_H.Amount = Request.Form["Amount"];

            string strItemId = Request.Form["strCItemId"];
            string strSourceOrderType = Request.Form["strCSourceOrderType"];
            string strSourceOrderNo = Request.Form["strCSourceOrderNo"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strRemark = Request.Form["strCRemark"];
            string strTestResult = Request.Form["strCTestResult"];
            string strReturned = Request.Form["strCReturned"];
            string strClosed = Request.Form["strCClosed"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];

            RMA_HLogic logic = new RMA_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateRMA_H(rMA_H, strItemId,
             strSourceOrderType, strSourceOrderNo,
             strProductNo, strProductName, strSerialNo,
             strRemark, strTestResult, strReturned,
             strClosed, strRGAType, strRGANo, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string RMAType, string RMANo)
        {
            RMA_HLogic logic = new RMA_HLogic();

            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = RMAType;
            rMA_H.RMANo = RMANo;

            string msg = "";
            if (!logic.DelRMA_H(rMA_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RMAType, string RMANo)
        {
            RMA_HLogic logic = new RMA_HLogic();

            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = RMAType;
            rMA_H.RMANo = RMANo;
            rMA_H = logic.GetRMA_HInfo(rMA_H);
            ViewBag.RMAType = rMA_H.RMAType;
            ViewBag.RMANo = rMA_H.RMANo;
            ViewBag.OrderDate = rMA_H.OrderDate;
            ViewBag.NoOfPrints = rMA_H.NoOfPrints;
            ViewBag.VendorOrderNo = rMA_H.VendorOrderNo;
            ViewBag.ShippingNo = rMA_H.ShippingNo;
            ViewBag.Remark = rMA_H.Remark;
            ViewBag.VendorId = rMA_H.VendorId;
            ViewBag.Contact = rMA_H.Contact;
            ViewBag.TelNo = rMA_H.TelNo;
            ViewBag.FaxNo = rMA_H.FaxNo;
            ViewBag.Address = rMA_H.Address;
            ViewBag.Confirmed = rMA_H.Confirmed;
            ViewBag.Amount = rMA_H.Amount;

            RMA_DLogic Dlogic = new RMA_DLogic();
            RMA_D rMA_D = new RMA_D();
            rMA_D.RMAType = RMAType;
            rMA_D.RMANo = RMANo;
            ViewData["RMA_D"] = Dlogic.SearchRMA_D(rMA_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RMAType, string RMANo)
        {
            RMA_HLogic logic = new RMA_HLogic();

            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = RMAType;
            rMA_H.RMANo = RMANo;
            rMA_H = logic.GetRMA_HInfo(rMA_H);
            ViewBag.RMAType = rMA_H.RMAType;
            ViewBag.RMANo = rMA_H.RMANo;
            ViewBag.OrderDate = rMA_H.OrderDate;
            ViewBag.NoOfPrints = rMA_H.NoOfPrints;
            ViewBag.VendorOrderNo = rMA_H.VendorOrderNo;
            ViewBag.ShippingNo = rMA_H.ShippingNo;
            ViewBag.Remark = rMA_H.Remark;
            ViewBag.VendorId = rMA_H.VendorId;
            ViewBag.Contact = rMA_H.Contact;
            ViewBag.TelNo = rMA_H.TelNo;
            ViewBag.FaxNo = rMA_H.FaxNo;
            ViewBag.Address = rMA_H.Address;
            ViewBag.Confirmed = rMA_H.Confirmed;
            ViewBag.Amount = rMA_H.Amount;

            RMA_DLogic Dlogic = new RMA_DLogic();
            RMA_D rMA_D = new RMA_D();
            rMA_D.RMAType = RMAType;
            rMA_D.RMANo = RMANo;
            ViewData["RMA_D"] = Dlogic.SearchRMA_D(rMA_D);
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RMAType, string RMANo)
        {
            RMA_HLogic logic = new RMA_HLogic();

            RMA_H rMA_H = new RMA_H();
            rMA_H.RMAType = RMAType;
            rMA_H.RMANo = RMANo;
            rMA_H = logic.GetRMA_HInfo(rMA_H);
            ViewBag.RMAType = rMA_H.RMAType;
            ViewBag.RMANo = rMA_H.RMANo;
            ViewBag.OrderDate = rMA_H.OrderDate;
            ViewBag.NoOfPrints = rMA_H.NoOfPrints;
            ViewBag.VendorOrderNo = rMA_H.VendorOrderNo;
            ViewBag.ShippingNo = rMA_H.ShippingNo;
            ViewBag.Remark = rMA_H.Remark;
            ViewBag.VendorId = rMA_H.VendorId;
            ViewBag.Contact = rMA_H.Contact;
            ViewBag.TelNo = rMA_H.TelNo;
            ViewBag.FaxNo = rMA_H.FaxNo;
            ViewBag.Address = rMA_H.Address;
            ViewBag.Confirmed = rMA_H.Confirmed;
            ViewBag.Amount = rMA_H.Amount;

            RMA_DLogic Dlogic = new RMA_DLogic();
            RMA_D rMA_D = new RMA_D();
            rMA_D.RMAType = RMAType;
            rMA_D.RMANo = RMANo;
            ViewData["RMA_D"] = Dlogic.SearchRMA_D(rMA_D);
            return View("CUR");
        }
    }
}