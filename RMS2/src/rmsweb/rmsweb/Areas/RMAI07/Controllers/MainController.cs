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

namespace RMAI07.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RFQ_HLogic logic = new RFQ_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RFQ_HLogic logic = new RFQ_HLogic();
            RFQ_H rFQ_H = new RFQ_H();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
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

                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單別,單據名稱,單號,廠商,品名,序號,狀態,回復狀態,建立人員");
                    List<RFQ_H> objRFQ_H = ViewData["RFQ_H"] as List<RFQ_H>;

                    foreach (var obj in objRFQ_H)
                    {
                        sw.WriteLine(obj.RFQType + ","+obj.OrderSName + "," + obj.RFQNo + "," + obj.VendorName + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.Confirmed + "," + obj.CreatorName);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ProductCategory.csv");
            }
            else
            {
                if (Request.Form["rfqtype"].Trim() != "")
                {
                    col += ",RFQType";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rfqtype"];
                }
                if (Request.Form["rfqno"].Trim() != "")
                {
                    col += ",RFQNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["rfqno"];
                }
                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rfqtype = Request.Form["rfqtype"];
                ViewBag.rfqno = Request.Form["rfqno"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Confirmed = "N:未報價";
            ViewBag.NoOfPrints = "0";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = Request.Form["RFQType"];
            rFQ_H.RFQNo = Request.Form["RFQNo"];
            rFQ_H.OrderDate = Request.Form["OrderDate"].Replace("/","");
            rFQ_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rFQ_H.SourceOrderType = Request.Form["SourceOrderType"];
            rFQ_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rFQ_H.Remark = Request.Form["Remark"];
            rFQ_H.VendorId = Request.Form["VendorId"];
            rFQ_H.TaxRate = double.Parse(Request.Form["TaxRate"]==""?"0": Request.Form["TaxRate"]);
            rFQ_H.Contact = Request.Form["Contact"];
            rFQ_H.TelNo = Request.Form["TelNo"];
            rFQ_H.FaxNo = Request.Form["FaxNo"];
            rFQ_H.Address = Request.Form["Address"];
            rFQ_H.ProductNo = Request.Form["ProductNo"];
            rFQ_H.ProductName = Request.Form["ProductName"];
            rFQ_H.SerialNo = Request.Form["SerialNo"];
            rFQ_H.PurchaseDate = Request.Form["PurchaseDate"].Replace("/","");
            rFQ_H.WarrantyStartDate = Request.Form["WarrantyStartDate"].Replace("/", "");
            rFQ_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"].Replace("/", "");
            //rFQ_H.TaxExcluded = double.Parse(Request.Form["TaxExcluded"] == "" ? "0" : Request.Form["TaxExcluded"]);
            //rFQ_H.Tax = double.Parse(Request.Form["Tax"] == "" ? "0" : Request.Form["Tax"]);
            //rFQ_H.Responser = Request.Form["Responser"];
            //rFQ_H.ResponseDate = Request.Form["ResponseDate"];
            //rFQ_H.StatusOfResponse = Request.Form["StatusOfResponse"];

            string strItemId=Request.Form["strCItemId"];
            string strPartNo = Request.Form["strCPartNo"];
            string strPartName = Request.Form["strCPartName"];
            string strQTY = Request.Form["strCQTY"];
            string strUnit = Request.Form["strCUnit"];
            string strUnitPrice = Request.Form["strCUnitPrice"];
            string strPrice = Request.Form["strCPrice"];
            string strListPrice = Request.Form["strCListPrice"];
            string strRemark = Request.Form["strCRemark"];
            string strRepaired = Request.Form["strCRepaired"];

            RFQ_HLogic logic = new RFQ_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRFQ_H(rFQ_H, strItemId,
             strPartNo, strPartName,
             strQTY, strUnit, strUnitPrice,
             strPrice, strListPrice, strRemark,
             strRepaired, out infomsg))
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
            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = Request.Form["RFQType"];
            rFQ_H.RFQNo = Request.Form["RFQNo"];
            rFQ_H.OrderDate = Request.Form["OrderDate"];
            rFQ_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            rFQ_H.SourceOrderType = Request.Form["SourceOrderType"];
            rFQ_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rFQ_H.Remark = Request.Form["Remark"];
            rFQ_H.VendorId = Request.Form["VendorId"];
            rFQ_H.TaxRate = double.Parse(Request.Form["TaxRate"] == "" ? "0" : Request.Form["TaxRate"]);
            rFQ_H.Contact = Request.Form["Contact"];
            rFQ_H.TelNo = Request.Form["TelNo"];
            rFQ_H.FaxNo = Request.Form["FaxNo"];
            rFQ_H.Address = Request.Form["Address"];
            rFQ_H.ProductNo = Request.Form["ProductNo"];
            rFQ_H.ProductName = Request.Form["ProductName"];
            rFQ_H.SerialNo = Request.Form["SerialNo"];
            rFQ_H.PurchaseDate = Request.Form["PurchaseDate"].Replace("/", "");
            rFQ_H.WarrantyStartDate = Request.Form["WarrantyStartDate"].Replace("/", "");
            rFQ_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"].Replace("/", "");
            rFQ_H.TaxExcluded = double.Parse(Request.Form["TaxExcluded"] == "" ? "0" : Request.Form["TaxExcluded"]);
            rFQ_H.Tax = double.Parse(Request.Form["Tax"] == "" ? "0" : Request.Form["Tax"]);
            rFQ_H.Responser = Request.Form["Responser"];
            rFQ_H.ResponseDate = Request.Form["ResponseDate"];
            rFQ_H.StatusOfResponse = Request.Form["StatusOfResponse"];

            string strItemId = Request.Form["strCItemId"];
            string strPartNo = Request.Form["strCPartNo"];
            string strPartName = Request.Form["strCPartName"];
            string strQTY = Request.Form["strCQTY"];
            string strUnit = Request.Form["strCUnit"];
            string strUnitPrice = Request.Form["strCUnitPrice"];
            string strPrice = Request.Form["strCPrice"];
            string strListPrice = Request.Form["strCListPrice"];
            string strRemark = Request.Form["strCRemark"];
            string strRepaired = Request.Form["strCRepaired"];

            RFQ_HLogic logic = new RFQ_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRFQ_H(rFQ_H, strItemId,
             strPartNo, strPartName,
             strQTY, strUnit, strUnitPrice,
             strPrice, strListPrice, strRemark,
             strRepaired, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string RFQType, string RFQNo)
        {
            RFQ_HLogic logic = new RFQ_HLogic();

            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = RFQType;
            rFQ_H.RFQNo = RFQNo;

            string msg = "";
            if (!logic.DelRFQ_H(rFQ_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RFQType, string RFQNo)
        {
            RFQ_HLogic logic = new RFQ_HLogic();

            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = RFQType;
            rFQ_H.RFQNo = RFQNo;
            rFQ_H = logic.GetRFQ_HInfo(rFQ_H);
            ViewBag.RFQType = rFQ_H.RFQType;
            ViewBag.RFQNo = rFQ_H.RFQNo;
            ViewBag.OrderDate = rFQ_H.OrderDate;
            ViewBag.StatusCode = rFQ_H.StatusCode;
            ViewBag.NoOfPrints = rFQ_H.NoOfPrints;
            ViewBag.SourceOrderType = rFQ_H.SourceOrderType;
            ViewBag.SourceOrderNo = rFQ_H.SourceOrderNo;
            ViewBag.Remark = rFQ_H.Remark;
            ViewBag.VendorId = rFQ_H.VendorId;
            ViewBag.TaxRate = rFQ_H.TaxRate;
            ViewBag.Contact = rFQ_H.Contact;
            ViewBag.TelNo = rFQ_H.TelNo;
            ViewBag.FaxNo = rFQ_H.FaxNo;
            ViewBag.Address = rFQ_H.Address;
            ViewBag.ProductNo = rFQ_H.ProductNo;
            ViewBag.ProductName = rFQ_H.ProductName;
            ViewBag.SerialNo = rFQ_H.SerialNo;
            ViewBag.PurchaseDate = rFQ_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rFQ_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rFQ_H.WarrantyExpiryDate;
            ViewBag.TaxExcluded = rFQ_H.TaxExcluded;
            ViewBag.Tax = rFQ_H.Tax;
            ViewBag.TaxIncluded = rFQ_H.TaxIncluded;
            ViewBag.Confirmed = rFQ_H.Confirmed;
            ViewBag.Responser = rFQ_H.Responser;
            ViewBag.ResponseDate = rFQ_H.ResponseDate;
            ViewBag.StatusOfResponse = rFQ_H.StatusOfResponse;

            RFQ_DLogic Dlogic = new RFQ_DLogic();

            RFQ_D rFQ_D = new RFQ_D();
            rFQ_D.RFQType = RFQType;
            rFQ_D.RFQNo = RFQNo;
            ViewData["RFQ_D"] = Dlogic.SearchRFQ_D(rFQ_D);
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RFQType, string RFQNo)
        {
            RFQ_HLogic logic = new RFQ_HLogic();

            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = RFQType;
            rFQ_H.RFQNo = RFQNo;
            rFQ_H = logic.GetRFQ_HInfo(rFQ_H);
            ViewBag.RFQType = rFQ_H.RFQType;
            ViewBag.RFQNo = rFQ_H.RFQNo;
            ViewBag.OrderDate = rFQ_H.OrderDate;
            ViewBag.StatusCode = rFQ_H.StatusCode;
            ViewBag.NoOfPrints = rFQ_H.NoOfPrints;
            ViewBag.SourceOrderType = rFQ_H.SourceOrderType;
            ViewBag.SourceOrderNo = rFQ_H.SourceOrderNo;
            ViewBag.Remark = rFQ_H.Remark;
            ViewBag.VendorId = rFQ_H.VendorId;
            ViewBag.TaxRate = rFQ_H.TaxRate;
            ViewBag.Contact = rFQ_H.Contact;
            ViewBag.TelNo = rFQ_H.TelNo;
            ViewBag.FaxNo = rFQ_H.FaxNo;
            ViewBag.Address = rFQ_H.Address;
            ViewBag.ProductNo = rFQ_H.ProductNo;
            ViewBag.ProductName = rFQ_H.ProductName;
            ViewBag.SerialNo = rFQ_H.SerialNo;
            ViewBag.PurchaseDate = rFQ_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rFQ_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rFQ_H.WarrantyExpiryDate;
            ViewBag.TaxExcluded = rFQ_H.TaxExcluded;
            ViewBag.Tax = rFQ_H.Tax;
            ViewBag.TaxIncluded = rFQ_H.TaxIncluded;
            ViewBag.Confirmed = rFQ_H.Confirmed;
            ViewBag.Responser = rFQ_H.Responser;
            ViewBag.ResponseDate = rFQ_H.ResponseDate;
            ViewBag.StatusOfResponse = rFQ_H.StatusOfResponse;

            RFQ_DLogic Dlogic = new RFQ_DLogic();

            RFQ_D rFQ_D = new RFQ_D();
            rFQ_D.RFQType = RFQType;
            rFQ_D.RFQNo = RFQNo;
            ViewData["RFQ_D"] = Dlogic.SearchRFQ_D(rFQ_D);
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RFQType, string RFQNo)
        {
            RFQ_HLogic logic = new RFQ_HLogic();

            RFQ_H rFQ_H = new RFQ_H();
            rFQ_H.RFQType = RFQType;
            rFQ_H.RFQNo = RFQNo;
            rFQ_H = logic.GetRFQ_HInfo(rFQ_H);
            ViewBag.RFQType = rFQ_H.RFQType;
            ViewBag.RFQNo = rFQ_H.RFQNo;
            ViewBag.OrderDate = rFQ_H.OrderDate;
            ViewBag.StatusCode = rFQ_H.StatusCode;
            ViewBag.NoOfPrints = rFQ_H.NoOfPrints;
            ViewBag.SourceOrderType = rFQ_H.SourceOrderType;
            ViewBag.SourceOrderNo = rFQ_H.SourceOrderNo;
            ViewBag.Remark = rFQ_H.Remark;
            ViewBag.VendorId = rFQ_H.VendorId;
            ViewBag.TaxRate = rFQ_H.TaxRate;
            ViewBag.Contact = rFQ_H.Contact;
            ViewBag.TelNo = rFQ_H.TelNo;
            ViewBag.FaxNo = rFQ_H.FaxNo;
            ViewBag.Address = rFQ_H.Address;
            ViewBag.ProductNo = rFQ_H.ProductNo;
            ViewBag.ProductName = rFQ_H.ProductName;
            ViewBag.SerialNo = rFQ_H.SerialNo;
            ViewBag.PurchaseDate = rFQ_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rFQ_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rFQ_H.WarrantyExpiryDate;
            ViewBag.TaxExcluded = rFQ_H.TaxExcluded;
            ViewBag.Tax = rFQ_H.Tax;
            ViewBag.TaxIncluded = rFQ_H.TaxIncluded;
            ViewBag.Confirmed = rFQ_H.Confirmed;
            ViewBag.Responser = rFQ_H.Responser;
            ViewBag.ResponseDate = rFQ_H.ResponseDate;
            ViewBag.StatusOfResponse = rFQ_H.StatusOfResponse;

            RFQ_DLogic Dlogic = new RFQ_DLogic();

            RFQ_D rFQ_D = new RFQ_D();
            rFQ_D.RFQType = RFQType;
            rFQ_D.RFQNo = RFQNo;
            ViewData["RFQ_D"] = Dlogic.SearchRFQ_D(rFQ_D);
            return View("CUR");
        }
    }
}