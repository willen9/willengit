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

namespace SERI11.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();
            string col = ",Confirmed";
            string condition = ",<>";
            string value = ",V";
            ViewBag.confirmed = "V";
            ViewBag.selCond3 ="<>";
            ViewData["CompletionOrder"] = logic.SearchCompletionOrder(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();
            CompletionOrder completionOrder = new CompletionOrder();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["CompletionOrder"] = logic.SearchCompletionOrder(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["CompletionOrder"] = logic.SearchCompletionOrder(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,客戶,品號,品名,序號,狀態");
                //    List<CompletionOrder> objCompletionOrder = ViewData["CompletionOrder"] as List<CompletionOrder>;
                //    foreach (var obj in objCompletionOrder)
                //    {
                //        sw.WriteLine(obj.CompletionType + "," + obj.OrderSName + "," + obj.CompletionNo + ","
                //            + obj.CustomerName + "," + obj.ProductNo + "," + obj.ProductName + ","
                //            + obj.SerialNo + "," + obj.Confirmed);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("CompletionOrder");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("單號");
                    row.CreateCell(3).SetCellValue("客戶");
                    row.CreateCell(4).SetCellValue("品號");
                    row.CreateCell(5).SetCellValue("品名");
                    row.CreateCell(6).SetCellValue("序號");
                    row.CreateCell(7).SetCellValue("狀態");


                    int index = 1;

                    List<CompletionOrder> objCompletionOrder = ViewData["CompletionOrder"] as List<CompletionOrder>;

                    foreach (var obj in objCompletionOrder)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.CompletionType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.CompletionNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(6).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(7).SetCellValue(obj.Confirmed.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "CompletionOrder.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "CompletionOrder.xls");
            }
            else
            {
                if (Request.Form["completionno"].Trim() != "")
                {
                    col += ",c.CompletionNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["completionno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",c.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }

                if (Request.Form["confirmed"].Trim() != "")
                {
                    col += ",c.Confirmed";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["confirmed"];
                }
                ViewData["CompletionOrder"] = logic.SearchCompletionOrder(col, condition, value);
                ViewBag.completionno = Request.Form["completionno"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.confirmed = Request.Form["confirmed"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.NoOfPrints = "0";
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Confirmed="N.未完工";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = Request.Form["CompletionType"];
            completionOrder.CompletionNo = Request.Form["CompletionNo"];
            completionOrder.OrderDate = Request.Form["OrderDate"].Replace("/","");
            completionOrder.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            completionOrder.SourceOrderType = Request.Form["SourceOrderType"];
            completionOrder.SourceOrderNo = Request.Form["SourceOrderNo"];
            completionOrder.Remark = Request.Form["Remark"];
            completionOrder.CustomerId = Request.Form["CustomerId"];
            completionOrder.AddressSName = Request.Form["AddressSName"];
            completionOrder.Address = Request.Form["Address"];
            completionOrder.Contact = Request.Form["Contact"];
            completionOrder.TelNo = Request.Form["TelNo"];
            completionOrder.FaxNo = Request.Form["FaxNo"];
            completionOrder.ProductNo = Request.Form["ProductNo"];
            completionOrder.ProductName = Request.Form["ProductName"];
            completionOrder.SerialNo = Request.Form["SerialNo"];
            completionOrder.TestResult = Request.Form["TestResult"];
            completionOrder.InternalRemark = Request.Form["InternalRemark"];
            completionOrder.Confirmed = Request.Form["Confirmed"].Substring(0,1);

            CompletionOrderLogic logic = new CompletionOrderLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddCompletionOrder(completionOrder, out infomsg))
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
            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = Request.Form["CompletionType"];
            completionOrder.CompletionNo = Request.Form["CompletionNo"];
            //completionOrder.OrderDate = Request.Form["OrderDate"];
            //completionOrder.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            //completionOrder.SourceOrderType = Request.Form["SourceOrderType"];
            //completionOrder.SourceOrderNo = Request.Form["SourceOrderNo"];
            completionOrder.Remark = Request.Form["Remark"];
            completionOrder.CustomerId = Request.Form["CustomerId"];
            completionOrder.AddressSName = Request.Form["AddressSName"];
            completionOrder.Address = Request.Form["Address"];
            completionOrder.Contact = Request.Form["Contact"];
            completionOrder.TelNo = Request.Form["TelNo"];
            completionOrder.FaxNo = Request.Form["FaxNo"];
            //completionOrder.ProductNo = Request.Form["ProductNo"];
            //completionOrder.ProductName = Request.Form["ProductName"];
            //completionOrder.SerialNo = Request.Form["SerialNo"];
            //completionOrder.TestResult = Request.Form["TestResult"];
            completionOrder.InternalRemark = Request.Form["InternalRemark"];
            //completionOrder.Confirmed = Request.Form["Confirmed"];

            CompletionOrderLogic logic = new CompletionOrderLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateCompletionOrder(completionOrder, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string CompletionType, string CompletionNo)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = CompletionType;
            completionOrder.CompletionNo = CompletionNo;

            string msg = "";

            if (!logic.DelCompletionOrder(completionOrder, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string CompletionType, string CompletionNo)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = CompletionType;
            completionOrder.CompletionNo = CompletionNo;
            completionOrder = logic.GetCompletionOrderInfo(completionOrder);
            ViewBag.CompletionType = completionOrder.CompletionType;
            ViewBag.CompletionNo = completionOrder.CompletionNo;
            ViewBag.OrderDate = completionOrder.OrderDate;
            ViewBag.NoOfPrints = completionOrder.NoOfPrints;
            ViewBag.SourceOrderType = completionOrder.SourceOrderType;
            ViewBag.SourceOrderNo = completionOrder.SourceOrderNo;
            ViewBag.Remark = completionOrder.Remark;
            ViewBag.CustomerId = completionOrder.CustomerId;
            ViewBag.CustomerName = completionOrder.CustomerName;
            ViewBag.AddressSName = completionOrder.AddressSName;
            ViewBag.Address = completionOrder.Address;
            ViewBag.Contact = completionOrder.Contact;
            ViewBag.TelNo = completionOrder.TelNo;
            ViewBag.FaxNo = completionOrder.FaxNo;
            ViewBag.ProductNo = completionOrder.ProductNo;
            ViewBag.ProductName = completionOrder.ProductName;
            ViewBag.SerialNo = completionOrder.SerialNo;
            ViewBag.AssetNo = completionOrder.AssetNo;
            ViewBag.Contract = completionOrder.Contract;
            ViewBag.TestResult = completionOrder.TestResult;
            ViewBag.InternalRemark = completionOrder.InternalRemark;
            ViewBag.Confirmed = completionOrder.Confirmed;
            ViewBag.UnderWarranty = completionOrder.UnderWarranty;
            ViewBag.CustomerType = completionOrder.CustomerType;
            ViewBag.OrderSName = completionOrder.OrderSName;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string CompletionType, string CompletionNo)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = CompletionType;
            completionOrder.CompletionNo = CompletionNo;
            completionOrder = logic.GetCompletionOrderInfo(completionOrder);
            ViewBag.CompletionType = completionOrder.CompletionType;
            ViewBag.CompletionNo = completionOrder.CompletionNo;
            ViewBag.OrderDate = completionOrder.OrderDate;
            ViewBag.NoOfPrints = completionOrder.NoOfPrints;
            ViewBag.SourceOrderType = completionOrder.SourceOrderType;
            ViewBag.SourceOrderNo = completionOrder.SourceOrderNo;
            ViewBag.Remark = completionOrder.Remark;
            ViewBag.CustomerId = completionOrder.CustomerId;
            ViewBag.CustomerName = completionOrder.CustomerName;
            ViewBag.AddressSName = completionOrder.AddressSName;
            ViewBag.Address = completionOrder.Address;
            ViewBag.Contact = completionOrder.Contact;
            ViewBag.TelNo = completionOrder.TelNo;
            ViewBag.FaxNo = completionOrder.FaxNo;
            ViewBag.ProductNo = completionOrder.ProductNo;
            ViewBag.ProductName = completionOrder.ProductName;
            ViewBag.SerialNo = completionOrder.SerialNo;
            ViewBag.AssetNo = completionOrder.AssetNo;
            ViewBag.Contract = completionOrder.Contract;
            ViewBag.TestResult = completionOrder.TestResult;
            ViewBag.InternalRemark = completionOrder.InternalRemark;
            ViewBag.Confirmed = completionOrder.Confirmed;
            ViewBag.UnderWarranty = completionOrder.UnderWarranty;
            ViewBag.CustomerType = completionOrder.CustomerType;
            ViewBag.OrderSName = completionOrder.OrderSName;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string CompletionType, string CompletionNo)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = CompletionType;
            completionOrder.CompletionNo = CompletionNo;
            completionOrder = logic.GetCompletionOrderInfo(completionOrder);
            ViewBag.CompletionType = completionOrder.CompletionType;
            ViewBag.CompletionNo = completionOrder.CompletionNo;
            ViewBag.OrderDate = completionOrder.OrderDate;
            ViewBag.NoOfPrints = completionOrder.NoOfPrints;
            ViewBag.SourceOrderType = completionOrder.SourceOrderType;
            ViewBag.SourceOrderNo = completionOrder.SourceOrderNo;
            ViewBag.Remark = completionOrder.Remark;
            ViewBag.CustomerId = completionOrder.CustomerId;
            ViewBag.CustomerName = completionOrder.CustomerName;
            ViewBag.AddressSName = completionOrder.AddressSName;
            ViewBag.Address = completionOrder.Address;
            ViewBag.Contact = completionOrder.Contact;
            ViewBag.TelNo = completionOrder.TelNo;
            ViewBag.FaxNo = completionOrder.FaxNo;
            ViewBag.ProductNo = completionOrder.ProductNo;
            ViewBag.ProductName = completionOrder.ProductName;
            ViewBag.SerialNo = completionOrder.SerialNo;
            ViewBag.AssetNo = completionOrder.AssetNo;
            ViewBag.Contract = completionOrder.Contract;
            ViewBag.TestResult = completionOrder.TestResult;
            ViewBag.InternalRemark = completionOrder.InternalRemark;
            ViewBag.Confirmed = completionOrder.Confirmed;
            ViewBag.UnderWarranty = completionOrder.UnderWarranty;
            ViewBag.CustomerType = completionOrder.CustomerType;
            ViewBag.OrderSName = completionOrder.OrderSName;
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Confirmed(string Type)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = Request.Form["CompletionType"];
            completionOrder.CompletionNo = Request.Form["CompletionNo"];
            if (Type == "Y")
            {
                completionOrder.Confirmed = "Y";
                completionOrder.Confirmor = "";
                completionOrder.ConfirmedDate = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            if (Type == "V")
            {
                completionOrder.Confirmed = "V";
                //phoneService_H.VoidRemark = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedCompletionOrder(completionOrder, out infomsg))
            {
                msg = "NO-操作失敗！" + infomsg;
            }
            else
            {
                msg = "OK-成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
    }
}