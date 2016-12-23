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

namespace SERI12.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            string col = ",Confirmed";
            string condition = ",<>";
            string value = ",V";
            ViewBag.confirmed = "V";
            ViewBag.selCond3 = "<>";
            ViewData["RGAReturn_H"] = logic.SearchRGAReturn_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            string col = "";
            string condition = "";
            string value = "";
            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RGAReturn_H"] = logic.SearchRGAReturn_H(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                col = ",Confirmed";
                condition = ",<>";
                value = ",V";
                ViewData["RGAReturn_H"] = logic.SearchRGAReturn_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("出件單據,單據名稱,出件單據,客戶名稱,品號,品名,序號,狀態");
                    List<RGAReturn_H> objRGAReturn_H = ViewData["RGAReturn_H"] as List<RGAReturn_H>;
                    foreach (var obj in objRGAReturn_H)
                    {
                        sw.WriteLine(obj.RGAReturnType + ","+obj.OrderSName+","+obj.RGAReturnNo + "," 
                            + obj.CustomerName + "," + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.Confirmed);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RGAReturn.csv");
            }
            else
            {
                if (Request.Form["parenttype"].Trim() != "")
                {
                    col += ",ProductCategoryType";
                    condition += ",=";
                    value += "," + Request.Form["parenttype"];
                }
                if (Request.Form["companyid"].Trim() != "")
                {
                    col += ",ProductTypeId";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["companyid"];
                }
                ViewData["RGAReturn_H"] = logic.SearchRGAReturn_H(col, condition, value);
                ViewBag.SelectCategoryType = Request.Form["parenttype"];
                ViewBag.findcompanyid = Request.Form["companyid"];
                ViewBag.selCond = Request.Form["selCond"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.NoOfPrints = "0";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = Request.Form["RGAReturnType"];
            rGAReturn_H.RGAReturnNo = Request.Form["RGAReturnNo"];
            rGAReturn_H.OrderDate = Request.Form["OrderDate"].Replace("/","");
            rGAReturn_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rGAReturn_H.PostalCategory = Request.Form["PostalCategory"];
            rGAReturn_H.ShippingNo = Request.Form["ShippingNo"];
            rGAReturn_H.Remark = Request.Form["Remark"];
            rGAReturn_H.CustomerId = Request.Form["CustomerId"];
            rGAReturn_H.AddressSName = Request.Form["AddressSName"];
            rGAReturn_H.Address = Request.Form["Address"];
            rGAReturn_H.Contact = Request.Form["Contact"];
            rGAReturn_H.TelNo = Request.Form["TelNo"];
            rGAReturn_H.FaxNo = Request.Form["FaxNo"];
            rGAReturn_H.Confirmed = Request.Form["Confirmed"];
            rGAReturn_H.Amount = Request.Form["Amount"];

            string strItemId = Request.Form["strCItemId"];
            string strSourceOrderType = Request.Form["strCSourceOrderType"];
            string strSourceOrderNo = Request.Form["strCSourceOrderNo"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strTestResult = Request.Form["strCTestResult"];
            string strInternalRemark = Request.Form["strCInternalRemark"];
            string strRemark = Request.Form["strCRemark"];

            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRGAReturn_H(rGAReturn_H, strItemId,
             strSourceOrderType, strSourceOrderNo, strRGAType,
             strRGANo, strProductNo, strProductName, strSerialNo,
             strTestResult, strInternalRemark, strRemark, out infomsg))
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
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = Request.Form["RGAReturnType"];
            rGAReturn_H.RGAReturnNo = Request.Form["RGAReturnNo"];
            rGAReturn_H.OrderDate = Request.Form["OrderDate"];
            rGAReturn_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rGAReturn_H.PostalCategory = Request.Form["PostalCategory"];
            rGAReturn_H.ShippingNo = Request.Form["ShippingNo"];
            rGAReturn_H.Remark = Request.Form["Remark"];
            rGAReturn_H.CustomerId = Request.Form["CustomerId"];
            rGAReturn_H.AddressSName = Request.Form["AddressSName"];
            rGAReturn_H.Address = Request.Form["Address"];
            rGAReturn_H.Contact = Request.Form["Contact"];
            rGAReturn_H.TelNo = Request.Form["TelNo"];
            rGAReturn_H.FaxNo = Request.Form["FaxNo"];
            rGAReturn_H.Confirmed = Request.Form["Confirmed"];
            rGAReturn_H.Amount = Request.Form["Amount"];

            string strItemId = Request.Form["strCItemId"];
            string strSourceOrderType = Request.Form["strCSourceOrderType"];
            string strSourceOrderNo = Request.Form["strCSourceOrderNo"];
            string strRGAType = Request.Form["strCRGAType"];
            string strRGANo = Request.Form["strCRGANo"];
            string strProductNo = Request.Form["strCProductNo"];
            string strProductName = Request.Form["strCProductName"];
            string strSerialNo = Request.Form["strCSerialNo"];
            string strTestResult = Request.Form["strCTestResult"];
            string strInternalRemark = Request.Form["strCInternalRemark"];
            string strRemark = Request.Form["strCRemark"];

            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateRGAReturn_H(rGAReturn_H, strItemId,
             strSourceOrderType, strSourceOrderNo, strRGAType,
             strRGANo, strProductNo, strProductName, strSerialNo,
             strTestResult, strInternalRemark, strRemark, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string RGAReturnType, string RGAReturnNo)
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = RGAReturnType;
            rGAReturn_H.RGAReturnNo = RGAReturnNo;

            string msg = "";
            if (!logic.DelRGAReturn_H(rGAReturn_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RGAReturnType, string RGAReturnNo)
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = RGAReturnType;
            rGAReturn_H.RGAReturnNo = RGAReturnNo;
            rGAReturn_H = logic.GetRGAReturn_HInfo(rGAReturn_H);
            ViewBag.RGAReturnType = rGAReturn_H.RGAReturnType;
            ViewBag.RGAReturnNo = rGAReturn_H.RGAReturnNo;
            ViewBag.OrderDate = rGAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rGAReturn_H.NoOfPrints;
            ViewBag.PostalCategory = rGAReturn_H.PostalCategory;
            ViewBag.ShippingNo = rGAReturn_H.ShippingNo;
            ViewBag.Remark = rGAReturn_H.Remark;
            ViewBag.CustomerId = rGAReturn_H.CustomerId;
            ViewBag.AddressSName = rGAReturn_H.AddressSName;
            ViewBag.Address = rGAReturn_H.Address;
            ViewBag.Contact = rGAReturn_H.Contact;
            ViewBag.TelNo = rGAReturn_H.TelNo;
            ViewBag.FaxNo = rGAReturn_H.FaxNo;
            ViewBag.Confirmed = rGAReturn_H.Confirmed;
            ViewBag.Amount = rGAReturn_H.Amount;

            RGAReturn_DLogic Dlogic = new RGAReturn_DLogic();
            RGAReturn_D rGAReturn_D = new RGAReturn_D();
            rGAReturn_D.RGAReturnType = RGAReturnType;
            rGAReturn_D.RGAReturnNo = RGAReturnNo;
            ViewData["RGAReturn_D"] = Dlogic.SearchRGAReturn_D(rGAReturn_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RGAReturnType, string RGAReturnNo)
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = RGAReturnType;
            rGAReturn_H.RGAReturnNo = RGAReturnNo;
            rGAReturn_H = logic.GetRGAReturn_HInfo(rGAReturn_H);
            ViewBag.RGAReturnType = rGAReturn_H.RGAReturnType;
            ViewBag.RGAReturnNo = rGAReturn_H.RGAReturnNo;
            ViewBag.OrderDate = rGAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rGAReturn_H.NoOfPrints;
            ViewBag.PostalCategory = rGAReturn_H.PostalCategory;
            ViewBag.ShippingNo = rGAReturn_H.ShippingNo;
            ViewBag.Remark = rGAReturn_H.Remark;
            ViewBag.CustomerId = rGAReturn_H.CustomerId;
            ViewBag.AddressSName = rGAReturn_H.AddressSName;
            ViewBag.Address = rGAReturn_H.Address;
            ViewBag.Contact = rGAReturn_H.Contact;
            ViewBag.TelNo = rGAReturn_H.TelNo;
            ViewBag.FaxNo = rGAReturn_H.FaxNo;
            ViewBag.Confirmed = rGAReturn_H.Confirmed;
            ViewBag.Amount = rGAReturn_H.Amount;

            RGAReturn_DLogic Dlogic = new RGAReturn_DLogic();
            RGAReturn_D rGAReturn_D = new RGAReturn_D();
            rGAReturn_D.RGAReturnType = RGAReturnType;
            rGAReturn_D.RGAReturnNo = RGAReturnNo;
            ViewData["RGAReturn_D"] = Dlogic.SearchRGAReturn_D(rGAReturn_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RGAReturnType, string RGAReturnNo)
        {
            RGAReturn_HLogic logic = new RGAReturn_HLogic();
            RGAReturn_H rGAReturn_H = new RGAReturn_H();
            rGAReturn_H.RGAReturnType = RGAReturnType;
            rGAReturn_H.RGAReturnNo = RGAReturnNo;
            rGAReturn_H = logic.GetRGAReturn_HInfo(rGAReturn_H);
            ViewBag.RGAReturnType = rGAReturn_H.RGAReturnType;
            ViewBag.RGAReturnNo = rGAReturn_H.RGAReturnNo;
            ViewBag.OrderDate = rGAReturn_H.OrderDate;
            ViewBag.NoOfPrints = rGAReturn_H.NoOfPrints;
            ViewBag.PostalCategory = rGAReturn_H.PostalCategory;
            ViewBag.ShippingNo = rGAReturn_H.ShippingNo;
            ViewBag.Remark = rGAReturn_H.Remark;
            ViewBag.CustomerId = rGAReturn_H.CustomerId;
            ViewBag.AddressSName = rGAReturn_H.AddressSName;
            ViewBag.Address = rGAReturn_H.Address;
            ViewBag.Contact = rGAReturn_H.Contact;
            ViewBag.TelNo = rGAReturn_H.TelNo;
            ViewBag.FaxNo = rGAReturn_H.FaxNo;
            ViewBag.Confirmed = rGAReturn_H.Confirmed;
            ViewBag.Amount = rGAReturn_H.Amount;

            RGAReturn_DLogic Dlogic = new RGAReturn_DLogic();
            RGAReturn_D rGAReturn_D = new RGAReturn_D();
            rGAReturn_D.RGAReturnType = RGAReturnType;
            rGAReturn_D.RGAReturnNo = RGAReturnNo;
            ViewData["RGAReturn_D"] = Dlogic.SearchRGAReturn_D(rGAReturn_D);

            return View("CUR");
        }
    }
}