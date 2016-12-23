using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace RMAI04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RMAAplChangeLogic logic = new RMAAplChangeLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RMAAplChange"] = logic.SearchRMAAplChange(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMAAplChangeLogic logic = new RMAAplChangeLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMAAplChange"] = logic.SearchRMAAplChange(col, condition, value);
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
                ViewData["RMAAplChange"] = logic.SearchRMAAplChange(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,版次,客戶簡稱,品號,品名,序號,變更人員,變更原因,確認碼");
                //    List<RMAAplChange> objRMAAplChange = ViewData["RMAAplChange"] as List<RMAAplChange>;
                //    foreach (var obj in objRMAAplChange)
                //    {
                //        sw.WriteLine(obj.RMAAplType + "," + obj.OrderSName + "," + obj.RMAAplNo + "," + obj.Version + "," + obj.CustomerName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.ConfirmorName + "," + obj.ChangeReason + "," + obj.Confirmed);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RMAApl");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);

       
                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("單號");
                    row.CreateCell(3).SetCellValue("客戶簡稱");
                    row.CreateCell(4).SetCellValue("品號");
                    row.CreateCell(5).SetCellValue("品名");
                    row.CreateCell(6).SetCellValue("序號");


                    int index = 1;

                    List<RMAAplChange> objRMAAplChange = ViewData["RMAAplChange"] as List<RMAAplChange>;

                    foreach (var obj in objRMAAplChange)
                    {
                        row = sheet.CreateRow(index);
                        
                        row.CreateCell(0).SetCellValue(obj.RMAAplType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RMAAplNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(6).SetCellValue(obj.SerialNo.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RMAApl.xls");

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAApl.csv");
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
                if (Request.Form["OldVersion"].Trim() != "")
                {
                    col += ",r.Version";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["OldVersion"];
                }
                ViewData["RMAAplChange"] = logic.SearchRMAAplChange(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rmaapltype = Request.Form["rmaapltype"];
                ViewBag.rmaaplno = Request.Form["rmaaplno"];
                ViewBag.OldVersion = Request.Form["OldVersion"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Confirmed = "N.未確認";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.Company = "";
            rMAAplChange.UserGroup = "";
            rMAAplChange.Creator = "";
            rMAAplChange.RMAAplType = Request.Form["RMAAplType"];
            rMAAplChange.RMAAplNo = Request.Form["RMAAplNo"];
            rMAAplChange.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rMAAplChange.Version = Request.Form["Version"];
            rMAAplChange.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rMAAplChange.SourceOrderType = Request.Form["SourceOrderType"];
            rMAAplChange.SourceOrderNo = Request.Form["SourceOrderNo"];
            rMAAplChange.CustomerId = Request.Form["CustomerId"];
            rMAAplChange.ProductNo = Request.Form["ProductNo"];
            rMAAplChange.ProductName = Request.Form["ProductName"];
            rMAAplChange.ChangeReason = Request.Form["ChangeReason"];
            rMAAplChange.IsClosed = Request.Form["IsClosed"] == null ? "N" : "Y";
            if (Request.Form["SerialNo"] != "")
            {
                rMAAplChange.SerialNo = Request.Form["SerialNo"];
                //if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                //{
                //    rMAAplChange.UnderWarranty = "Y";
                //}
                //else
                //{
                //    rMAAplChange.UnderWarranty = "N";
                //}
                rMAAplChange.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rMAAplChange.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
                rMAAplChange.VendorWStartDate = Request.Form["VendorWStartDate"] == null ? "" : Request.Form["VendorWStartDate"].ToString().Replace("/", "");
                rMAAplChange.VendorWExpiryDate = Request.Form["VendorWExpiryDate"] == null ? "" : Request.Form["VendorWExpiryDate"].ToString().Replace("/", "");
            }
            rMAAplChange.OptionDetail = Request.Form["OptionDetail"];
            rMAAplChange.Remark = Request.Form["Remark"];
            rMAAplChange.TestResult = Request.Form["TestResult"];
            rMAAplChange.Returned = Request.Form["Returned"] == null ? "N" : "Y";

            RMAAplChangeLogic logic = new RMAAplChangeLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddRMAAplChange(rMAAplChange, out infomsg))
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
            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.Company = "";
            rMAAplChange.UserGroup = "";
            rMAAplChange.Creator = "";
            rMAAplChange.RMAAplType = Request.Form["RMAAplType"];
            rMAAplChange.RMAAplNo = Request.Form["RMAAplNo"];
            rMAAplChange.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rMAAplChange.Version = Request.Form["Version"];
            rMAAplChange.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rMAAplChange.SourceOrderType = Request.Form["SourceOrderType"];
            rMAAplChange.SourceOrderNo = Request.Form["SourceOrderNo"];
            rMAAplChange.CustomerId = Request.Form["CustomerId"];
            rMAAplChange.ProductNo = Request.Form["ProductNo"];
            rMAAplChange.ProductName = Request.Form["ProductName"];
            rMAAplChange.ChangeReason = Request.Form["ChangeReason"];
            rMAAplChange.IsClosed = Request.Form["IsClosed"] == null ? "N" : "Y";
            if (Request.Form["SerialNo"] != "")
            {
                rMAAplChange.SerialNo = Request.Form["SerialNo"];
                //if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                //{
                //    rMAAplChange.UnderWarranty = "Y";
                //}
                //else
                //{
                //    rMAAplChange.UnderWarranty = "N";
                //}
                rMAAplChange.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rMAAplChange.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
                rMAAplChange.VendorWStartDate = Request.Form["VendorWStartDate"] == null ? "" : Request.Form["VendorWStartDate"].ToString().Replace("/", "");
                rMAAplChange.VendorWExpiryDate = Request.Form["VendorWExpiryDate"] == null ? "" : Request.Form["VendorWExpiryDate"].ToString().Replace("/", "");
            }
            rMAAplChange.OptionDetail = Request.Form["OptionDetail"];
            rMAAplChange.Remark = Request.Form["Remark"];
            rMAAplChange.TestResult = Request.Form["TestResult"];
            rMAAplChange.Returned = Request.Form["Returned"] == null ? "N" : "Y";

            RMAAplChangeLogic logic = new RMAAplChangeLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateRMAAplChange(rMAAplChange, out infomsg))
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
            RMAAplChangeLogic logic = new RMAAplChangeLogic();

            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.RMAAplType = RMAAplType;
            rMAAplChange.RMAAplNo = RMAAplNo;

            string msg = "";
            if (!logic.DelRMAAplChange(rMAAplChange, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string RMAAplType, string RMAAplNo)
        {
            RMAAplChangeLogic logic = new RMAAplChangeLogic();

            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.RMAAplType = RMAAplType;
            rMAAplChange.RMAAplNo = RMAAplNo;
            rMAAplChange = logic.GetRMAAplChangeInfo(rMAAplChange);
            ViewBag.RMAAplType = rMAAplChange.RMAAplType;
            ViewBag.RMAAplNo = rMAAplChange.RMAAplNo;
            ViewBag.OrderDate = rMAAplChange.OrderDate;
            ViewBag.Version = rMAAplChange.Version;
            ViewBag.NoOfPrints = rMAAplChange.NoOfPrints;
            ViewBag.SourceOrderType = rMAAplChange.SourceOrderType;
            ViewBag.SourceOrderNo = rMAAplChange.SourceOrderNo;
            ViewBag.CustomerId = rMAAplChange.CustomerId;
            ViewBag.ProductNo = rMAAplChange.ProductNo;
            ViewBag.ProductName = rMAAplChange.ProductName;
            ViewBag.SerialNo = rMAAplChange.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAAplChange.VendorWStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAAplChange.VendorWExpiryDate;
            ViewBag.WarrantyStartDate = rMAAplChange.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAAplChange.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAAplChange.Confirmed;
            ViewBag.OptionDetail = rMAAplChange.OptionDetail;
            ViewBag.Remark = rMAAplChange.Remark;
            ViewBag.TestResult = rMAAplChange.TestResult;
            ViewBag.Returned = rMAAplChange.Returned;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string RMAAplType, string RMAAplNo)
        {
            RMAAplChangeLogic logic = new RMAAplChangeLogic();

            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.RMAAplType = RMAAplType;
            rMAAplChange.RMAAplNo = RMAAplNo;
            rMAAplChange = logic.GetRMAAplChangeInfo(rMAAplChange);
            ViewBag.RMAAplType = rMAAplChange.RMAAplType;
            ViewBag.RMAAplNo = rMAAplChange.RMAAplNo;
            ViewBag.OrderDate = rMAAplChange.OrderDate;
            ViewBag.Version = rMAAplChange.Version;
            ViewBag.NoOfPrints = rMAAplChange.NoOfPrints;
            ViewBag.SourceOrderType = rMAAplChange.SourceOrderType;
            ViewBag.SourceOrderNo = rMAAplChange.SourceOrderNo;
            ViewBag.CustomerId = rMAAplChange.CustomerId;
            ViewBag.ProductNo = rMAAplChange.ProductNo;
            ViewBag.ProductName = rMAAplChange.ProductName;
            ViewBag.SerialNo = rMAAplChange.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAAplChange.VendorWStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAAplChange.VendorWExpiryDate;
            ViewBag.WarrantyStartDate = rMAAplChange.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAAplChange.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAAplChange.Confirmed;
            ViewBag.OptionDetail = rMAAplChange.OptionDetail;
            ViewBag.Remark = rMAAplChange.Remark;
            ViewBag.TestResult = rMAAplChange.TestResult;
            ViewBag.Returned = rMAAplChange.Returned;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string RMAAplType, string RMAAplNo)
        {
            RMAAplChangeLogic logic = new RMAAplChangeLogic();

            RMAAplChange rMAAplChange = new RMAAplChange();
            rMAAplChange.RMAAplType = RMAAplType;
            rMAAplChange.RMAAplNo = RMAAplNo;
            rMAAplChange = logic.GetRMAAplChangeInfo(rMAAplChange);
            ViewBag.RMAAplType = rMAAplChange.RMAAplType;
            ViewBag.RMAAplNo = rMAAplChange.RMAAplNo;
            ViewBag.OrderDate = rMAAplChange.OrderDate;
            ViewBag.Version = rMAAplChange.Version;
            ViewBag.NoOfPrints = rMAAplChange.NoOfPrints;
            ViewBag.SourceOrderType = rMAAplChange.SourceOrderType;
            ViewBag.SourceOrderNo = rMAAplChange.SourceOrderNo;
            ViewBag.CustomerId = rMAAplChange.CustomerId;
            ViewBag.ProductNo = rMAAplChange.ProductNo;
            ViewBag.ProductName = rMAAplChange.ProductName;
            ViewBag.SerialNo = rMAAplChange.SerialNo;
            ViewBag.VendorWarrantyStartDate = rMAAplChange.VendorWStartDate;
            ViewBag.VendorWarrantyExpiryDate = rMAAplChange.VendorWExpiryDate;
            ViewBag.WarrantyStartDate = rMAAplChange.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rMAAplChange.WarrantyExpiryDate;
            ViewBag.Confirmed = rMAAplChange.Confirmed;
            ViewBag.OptionDetail = rMAAplChange.OptionDetail;
            ViewBag.Remark = rMAAplChange.Remark;
            ViewBag.TestResult = rMAAplChange.TestResult;
            ViewBag.Returned = rMAAplChange.Returned;
            return View("CUR");
        }

        public JsonResult SearchRMAApl(string RMAAplType, string RMAAplNo, int Page)
        {
            RMAAplLogic logic = new RMAAplLogic();
            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;
            List<RMAApl> lst = logic.SearchRMAAplInfo(rMAApl, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRMAAplInfo(string RMAAplType, string RMAAplNo)
        {
            RMAAplLogic logic = new RMAAplLogic();
            RMAApl rMAApl = new RMAApl();
            rMAApl.RMAAplType = RMAAplType;
            rMAApl.RMAAplNo = RMAAplNo;
            return Json(logic.GetRMAAplInfo(rMAApl), JsonRequestBehavior.AllowGet);
        }
    }
}