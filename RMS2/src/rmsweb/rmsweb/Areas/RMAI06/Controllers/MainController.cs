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

namespace RMAI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();
            RMAReturn_H rMAReturn_H = new RMAReturn_H();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
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

                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單號,單據名稱,單號,廠商,單據日期,狀態,建立人員");
                    List<RMAReturn_H> objRMAReturn_H = ViewData["RMAReturn_H"] as List<RMAReturn_H>;
                    foreach (var obj in objRMAReturn_H)
                    {
                        sw.WriteLine(obj.RMAReturnType + ","+obj.OrderSName + "," + obj.RMAReturnNo + "," + obj.VendorName + "," + obj.OrderDate + "," + obj.Confirmed + "," + obj.CreatorName );
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAReturn.csv");
            }
            else
            {
                if (Request.Form["rmareturntype"].Trim() != "")
                {
                    col += ",RMAReturnType";
                    condition += ",=";
                    value += "," + Request.Form["rmareturntype"];
                }
                if (Request.Form["rmareturnno"].Trim() != "")
                {
                    col += ",RMAReturnNo";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["rmareturnno"];
                }
                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rmareturntype = Request.Form["rmareturntype"];
                ViewBag.rmareturnno = Request.Form["rmareturnno"];
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
            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = Request.Form["RMAReturnType"];
            rMAReturn_H.RMAReturnNo = Request.Form["RMAReturnNo"];
            rMAReturn_H.OrderDate = Request.Form["OrderDate"].Replace("/","");
            rMAReturn_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rMAReturn_H.Remark = Request.Form["Remark"];
            rMAReturn_H.VendorNo = Request.Form["VendorNo"];
            rMAReturn_H.Contact = Request.Form["Contact"];
            rMAReturn_H.TelNo = Request.Form["TelNo"];
            rMAReturn_H.FaxNo = Request.Form["FaxNo"];
            rMAReturn_H.Address = Request.Form["Address"];
            rMAReturn_H.Amount = Request.Form["Amount"];

            string strItemId = Request.Form["strCItemId"];
            string strRMAType = Request.Form["strCRMAType"];
            string strRMANo = Request.Form["strCRMANo"];
            string strRMAItemId = Request.Form["strCRMAItemId"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strRemark = Request.Form["strCRemark"];
            string strReturned = Request.Form["strCReturned"];
            string strRepairDetail = Request.Form["strCRepairDetail"];
            string strMalfunctionReason = Request.Form["strCMalfunctionReason"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];

            RMAReturn_HLogic logic = new RMAReturn_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRMAReturn_H(rMAReturn_H, strItemId,
             strRMAType, strRMANo,
             strRMAItemId, strProductNo, strProductName,
             strSerialNo, strRemark, strReturned,
             strRepairDetail, strMalfunctionReason, strRGAType, strRGANo, out infomsg))
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
            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = Request.Form["RMAReturnType"];
            rMAReturn_H.RMAReturnNo = Request.Form["RMAReturnNo"];
            rMAReturn_H.OrderDate = Request.Form["OrderDate"];
            rMAReturn_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rMAReturn_H.Remark = Request.Form["Remark"];
            rMAReturn_H.VendorNo = Request.Form["VendorNo"];
            rMAReturn_H.Contact = Request.Form["Contact"];
            rMAReturn_H.TelNo = Request.Form["TelNo"];
            rMAReturn_H.FaxNo = Request.Form["FaxNo"];
            rMAReturn_H.Address = Request.Form["Address"];
            rMAReturn_H.Amount = Request.Form["Amount"];

            string strItemId = Request.Form["strCItemId"];
            string strRMAType = Request.Form["strCRMAType"];
            string strRMANo = Request.Form["strCRMANo"];
            string strRMAItemId = Request.Form["strCRMAItemId"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strRemark = Request.Form["strCRemark"];
            string strReturned = Request.Form["strCReturned"];
            string strRepairDetail = Request.Form["strCRepairDetail"];
            string strMalfunctionReason = Request.Form["strCMalfunctionReason"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];

            RMAReturn_HLogic logic = new RMAReturn_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateRMAReturn_H(rMAReturn_H, strItemId,
             strRMAType, strRMANo,
             strRMAItemId, strProductNo, strProductName,
             strSerialNo, strRemark, strReturned,
             strRepairDetail, strMalfunctionReason, strRGAType, strRGANo, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string RMAReturnType, string RMAReturnNo)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();

            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = RMAReturnType;
            rMAReturn_H.RMAReturnNo = RMAReturnNo;

            string msg = "";
            if (!logic.DelRMAReturn_H(rMAReturn_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RMAReturnType, string RMAReturnNo)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();

            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = RMAReturnType;
            rMAReturn_H.RMAReturnNo = RMAReturnNo;
            rMAReturn_H = logic.GetRMAReturn_HInfo(rMAReturn_H);
            ViewBag.RMAReturnType = rMAReturn_H.RMAReturnType;
            ViewBag.RMAReturnNo = rMAReturn_H.RMAReturnNo;
            ViewBag.OrderDate = rMAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rMAReturn_H.NoOfPrints;
            ViewBag.Remark = rMAReturn_H.Remark;
            ViewBag.VendorNo = rMAReturn_H.VendorNo;
            ViewBag.Contact = rMAReturn_H.Contact;
            ViewBag.TelNo = rMAReturn_H.TelNo;
            ViewBag.FaxNo = rMAReturn_H.FaxNo;
            ViewBag.Address = rMAReturn_H.Address;
            ViewBag.Confirmed = rMAReturn_H.Confirmed;
            ViewBag.Amount = rMAReturn_H.Amount;

            RMAReturn_DLogic Dlogic = new RMAReturn_DLogic();

            RMAReturn_D rMAReturn_D = new RMAReturn_D();
            rMAReturn_D.RMAReturnType = RMAReturnType;
            rMAReturn_D.RMAReturnNo = RMAReturnNo;

            ViewData["RMAReturn_D"] = Dlogic.SearchRMAReturn_D(rMAReturn_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RMAReturnType, string RMAReturnNo)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();

            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = RMAReturnType;
            rMAReturn_H.RMAReturnNo = RMAReturnNo;
            rMAReturn_H = logic.GetRMAReturn_HInfo(rMAReturn_H);
            ViewBag.RMAReturnType = rMAReturn_H.RMAReturnType;
            ViewBag.RMAReturnNo = rMAReturn_H.RMAReturnNo;
            ViewBag.OrderDate = rMAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rMAReturn_H.NoOfPrints;
            ViewBag.Remark = rMAReturn_H.Remark;
            ViewBag.VendorNo = rMAReturn_H.VendorNo;
            ViewBag.Contact = rMAReturn_H.Contact;
            ViewBag.TelNo = rMAReturn_H.TelNo;
            ViewBag.FaxNo = rMAReturn_H.FaxNo;
            ViewBag.Address = rMAReturn_H.Address;
            ViewBag.Confirmed = rMAReturn_H.Confirmed;
            ViewBag.Amount = rMAReturn_H.Amount;

            RMAReturn_DLogic Dlogic = new RMAReturn_DLogic();

            RMAReturn_D rMAReturn_D = new RMAReturn_D();
            rMAReturn_D.RMAReturnType = RMAReturnType;
            rMAReturn_D.RMAReturnNo = RMAReturnNo;

            ViewData["RMAReturn_D"] = Dlogic.SearchRMAReturn_D(rMAReturn_D);
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RMAReturnType, string RMAReturnNo)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();

            RMAReturn_H rMAReturn_H = new RMAReturn_H();
            rMAReturn_H.RMAReturnType = RMAReturnType;
            rMAReturn_H.RMAReturnNo = RMAReturnNo;
            rMAReturn_H = logic.GetRMAReturn_HInfo(rMAReturn_H);
            ViewBag.RMAReturnType = rMAReturn_H.RMAReturnType;
            ViewBag.RMAReturnNo = rMAReturn_H.RMAReturnNo;
            ViewBag.OrderDate = rMAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rMAReturn_H.NoOfPrints;
            ViewBag.Remark = rMAReturn_H.Remark;
            ViewBag.VendorNo = rMAReturn_H.VendorNo;
            ViewBag.Contact = rMAReturn_H.Contact;
            ViewBag.TelNo = rMAReturn_H.TelNo;
            ViewBag.FaxNo = rMAReturn_H.FaxNo;
            ViewBag.Address = rMAReturn_H.Address;
            ViewBag.Confirmed = rMAReturn_H.Confirmed;
            ViewBag.Amount = rMAReturn_H.Amount;

            RMAReturn_DLogic Dlogic = new RMAReturn_DLogic();

            RMAReturn_D rMAReturn_D = new RMAReturn_D();
            rMAReturn_D.RMAReturnType = RMAReturnType;
            rMAReturn_D.RMAReturnNo = RMAReturnNo;

            ViewData["RMAReturn_D"] = Dlogic.SearchRMAReturn_D(rMAReturn_D);
            return View("CUR");
        }
    }
}