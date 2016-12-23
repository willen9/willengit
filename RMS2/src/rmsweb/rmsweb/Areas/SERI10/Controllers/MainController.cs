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

namespace SERI10.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Quotation_HLogic logic = new Quotation_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Quotation_H"] = logic.SearchQuotation_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Quotation_HLogic logic = new Quotation_HLogic();
            Quotation_H quotation_H = new Quotation_H();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Quotation_H"] = logic.SearchQuotation_H(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["Quotation_H"] = logic.SearchQuotation_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("報價單別,單據名稱,報價單號,客戶,品號,品名,序號,報價狀態,回復狀態,報價人員,報價金額,來源單別,來源單號");
                //    List<Quotation_H> objQuotation_H = ViewData["Quotation_H"] as List<Quotation_H>;
                //    foreach (var obj in objQuotation_H)
                //    {
                //        sw.WriteLine(obj.QuotationType + ","+obj.OrderSName + "," + obj.QuotationNo + "," 
                //            + obj.CustomerName + "," + obj.ProductNo + "," + obj.ProductName + "," 
                //            + obj.SerialNo + "," + obj.StatusCode + "," + obj.StatusOfResponse + "," 
                //            + obj.CreatorName + "," + obj.TaxExcluded + obj.SourceOrderType + "," + obj.SourceOrderNo);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Quotation");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);
                    sheet.SetColumnWidth(8, 20 * 256);
                    sheet.SetColumnWidth(9, 20 * 256);
                    sheet.SetColumnWidth(10, 20 * 256);
                    sheet.SetColumnWidth(11, 20 * 256);
                    sheet.SetColumnWidth(12, 20 * 256);
                    

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("報價單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("報價單號");
                    row.CreateCell(3).SetCellValue("客戶");
                    row.CreateCell(4).SetCellValue("品號");
                    row.CreateCell(5).SetCellValue("品名");
                    row.CreateCell(6).SetCellValue("序號");
                    row.CreateCell(7).SetCellValue("報價狀態");
                    row.CreateCell(8).SetCellValue("回復狀態");
                    row.CreateCell(9).SetCellValue("報價人員");
                    row.CreateCell(10).SetCellValue("報價金額");
                    row.CreateCell(11).SetCellValue("來源單別");
                    row.CreateCell(12).SetCellValue("來源單號");

                    int index = 1;

                    List<Quotation_H> objQuotation_H = ViewData["Quotation_H"] as List<Quotation_H>;

                    foreach (var obj in objQuotation_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.QuotationType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.QuotationNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(6).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(7).SetCellValue(obj.StatusCode.ToString());
                        row.CreateCell(8).SetCellValue(obj.StatusOfResponse.ToString());
                        row.CreateCell(9).SetCellValue(obj.CreatorName.ToString());
                        row.CreateCell(10).SetCellValue(obj.TaxExcluded.ToString());
                        row.CreateCell(11).SetCellValue(obj.SourceOrderType.ToString());
                        row.CreateCell(12).SetCellValue(obj.SourceOrderNo.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }


                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Quotation.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Quotation.xls");
            }
            else
            {
                if (Request.Form["quotationno"].Trim() != "")
                {
                    col += ",h.QuotationNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["quotationno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                ViewData["Quotation_H"] = logic.SearchQuotation_H(col, condition, value);
                ViewBag.quotationno = Request.Form["quotationno"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.NoOfPrints = "0";
            ViewBag.StatusCode = "0.未報價";
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = Request.Form["QuotationType"];
            quotation_H.QuotationNo = Request.Form["QuotationNo"];
            quotation_H.OrderDate = Request.Form["OrderDate"].Replace("/","");
            quotation_H.StatusCode = Request.Form["StatusCode"].Substring(0,1);
            quotation_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            quotation_H.Repairman = Request.Form["Repairman"];
            quotation_H.SourceOrderType = Request.Form["SourceOrderType"];
            quotation_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            quotation_H.Responser = Request.Form["Responser"];
            quotation_H.StatusOfResponse = "0";
            quotation_H.ResponseDate = Request.Form["ResponseDate"];
            quotation_H.Response = Request.Form["Response"];
            quotation_H.CustomerId = Request.Form["CustomerId"];
            quotation_H.TaxRate = Request.Form["TaxRate"];
            quotation_H.AddressSName = Request.Form["AddressSName"];
            quotation_H.Address = Request.Form["Address"];
            quotation_H.Contact = Request.Form["Contact"];
            quotation_H.TelNo = Request.Form["TelNo"];
            quotation_H.FaxNo = Request.Form["FaxNo"];
            quotation_H.ProductNo = Request.Form["ProductNo"];
            quotation_H.ProductName = Request.Form["ProductName"];
            if(Request.Form["SerialNo"]!="")
            {
                quotation_H.SerialNo = Request.Form["SerialNo"];
                quotation_H.AssetNo = Request.Form["AssetNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    quotation_H.UnderWarranty = "Y";
                }
                else
                {
                    quotation_H.UnderWarranty = "N";
                }
                quotation_H.SaleDate = Request.Form["SaleDate"].Replace("/", "");
                quotation_H.PurchaseDate = Request.Form["PurchaseDate"].Replace("/", "");
                quotation_H.WarrantyStartDate = Request.Form["WarrantyStartDate"].Replace("/", "");
                quotation_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"].Replace("/", "");
            }
            
            quotation_H.Remark = Request.Form["Remark"];
            quotation_H.TestResult = Request.Form["TestResult"];
            quotation_H.InternalRemark = Request.Form["InternalRemark"];
            quotation_H.TaxExcluded = Request.Form["TaxExcluded"];
            quotation_H.Tax = Request.Form["Tax"];
            quotation_H.TaxIncluded = Request.Form["TaxIncluded"];
            quotation_H.Confirmed = Request.Form["Confirmed"];

            string strItemId = Request.Form["strCItemId"] == null ? "" : Request.Form["strCItemId"].ToString();
            string strPartNo = Request.Form["strCPartNo"] == null ? "" : Request.Form["strCPartNo"].ToString();
            string strPartName = Request.Form["strCPartName"] == null ? "" : Request.Form["strCPartName"].ToString();
            string strQTY = Request.Form["strCQTY"] == null ? "" : Request.Form["strCQTY"].ToString();
            string strUnit = Request.Form["strCUnit"] == null ? "" : Request.Form["strCUnit"].ToString();
            string strUnitPrice = Request.Form["strCUnitPrice"] == null ? "" : Request.Form["strCUnitPrice"].ToString();
            string strPreferentialPrice = Request.Form["strCPreferentialPrice"] == null ? "" : Request.Form["strCPreferentialPrice"].ToString();
            string strRemark = Request.Form["strCRemark"] == null ? "" : Request.Form["strCRemark"].ToString();

            Quotation_HLogic logic = new Quotation_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddQuotation_H(quotation_H, strItemId, strPartNo, strPartName, strQTY,
             strUnit, strUnitPrice, strPreferentialPrice,
              strRemark, out infomsg))
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
            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = Request.Form["QuotationType"];
            quotation_H.QuotationNo = Request.Form["QuotationNo"];
            quotation_H.OrderDate = Request.Form["OrderDate"].Replace("/", "");
            quotation_H.StatusCode = Request.Form["StatusCode"].Substring(0, 1);
            quotation_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]);
            quotation_H.Repairman = Request.Form["Repairman"];
            quotation_H.SourceOrderType = Request.Form["SourceOrderType"];
            quotation_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            quotation_H.Responser = Request.Form["Responser"];
            quotation_H.StatusOfResponse = Request.Form["StatusOfResponse"];
            quotation_H.ResponseDate = Request.Form["ResponseDate"];
            quotation_H.Response = Request.Form["Response"];
            quotation_H.CustomerId = Request.Form["CustomerId"];
            quotation_H.TaxRate = Request.Form["TaxRate"];
            quotation_H.AddressSName = Request.Form["AddressSName"];
            quotation_H.Address = Request.Form["Address"];
            quotation_H.Contact = Request.Form["Contact"];
            quotation_H.TelNo = Request.Form["TelNo"];
            quotation_H.FaxNo = Request.Form["FaxNo"];
            quotation_H.ProductNo = Request.Form["ProductNo"];
            quotation_H.ProductName = Request.Form["ProductName"];
            if (Request.Form["SerialNo"] != "")
            {
                quotation_H.SerialNo = Request.Form["SerialNo"];
                quotation_H.AssetNo = Request.Form["AssetNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    quotation_H.UnderWarranty = "Y";
                }
                else
                {
                    quotation_H.UnderWarranty = "N";
                }
                quotation_H.SaleDate = Request.Form["SaleDate"].Replace("/", "");
                quotation_H.PurchaseDate = Request.Form["PurchaseDate"].Replace("/", "");
                quotation_H.WarrantyStartDate = Request.Form["WarrantyStartDate"].Replace("/", "");
                quotation_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"].Replace("/", "");
            }
            quotation_H.Remark = Request.Form["Remark"];
            quotation_H.TestResult = Request.Form["TestResult"];
            quotation_H.InternalRemark = Request.Form["InternalRemark"];
            quotation_H.TaxExcluded = Request.Form["TaxExcluded"];
            quotation_H.Tax = Request.Form["Tax"];
            quotation_H.TaxIncluded = Request.Form["TaxIncluded"];
            quotation_H.Confirmed = Request.Form["Confirmed"];

            string strItemId = Request.Form["strCItemId"] == null ? "" : Request.Form["strCItemId"].ToString();
            string strPartNo = Request.Form["strCPartNo"] == null ? "" : Request.Form["strCPartNo"].ToString();
            string strPartName = Request.Form["strCPartName"] == null ? "" : Request.Form["strCPartName"].ToString();
            string strQTY = Request.Form["strCQTY"] == null ? "" : Request.Form["strCQTY"].ToString();
            string strUnit = Request.Form["strCUnit"] == null ? "" : Request.Form["strCUnit"].ToString();
            string strUnitPrice = Request.Form["strCUnitPrice"] == null ? "" : Request.Form["strCUnitPrice"].ToString();
            string strPreferentialPrice = Request.Form["strCPreferentialPrice"] == null ? "" : Request.Form["strCPreferentialPrice"].ToString();
            string strRemark = Request.Form["strCRemark"] == null ? "" : Request.Form["strCRemark"].ToString();

            Quotation_HLogic logic = new Quotation_HLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateQuotation_H(quotation_H, strItemId, strPartNo, strPartName, strQTY,
             strUnit, strUnitPrice, strPreferentialPrice,
              strRemark, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string QuotationType, string QuotationNo)
        {
            Quotation_HLogic logic = new Quotation_HLogic();

            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = QuotationType;
            quotation_H.QuotationNo = QuotationNo;

            string msg = "";

            if(!logic.DelQuotation_H(quotation_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string QuotationType, string QuotationNo)
        {
            Quotation_HLogic logic = new Quotation_HLogic();
            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = QuotationType;
            quotation_H.QuotationNo = QuotationNo;
            quotation_H = logic.GetQuotation_HInfo(quotation_H);
            ViewBag.QuotationType = quotation_H.QuotationType;
            ViewBag.QuotationNo = quotation_H.QuotationNo;
            ViewBag.OrderSName = quotation_H.OrderSName;
            ViewBag.OrderDate = quotation_H.OrderDate;
            ViewBag.StatusCode = quotation_H.StatusCode;
            ViewBag.NoOfPrints = quotation_H.NoOfPrints;
            ViewBag.Repairman = quotation_H.Repairman;
            ViewBag.SourceOrderType = quotation_H.SourceOrderType;
            ViewBag.SourceOrderNo = quotation_H.SourceOrderNo;
            ViewBag.Responser = quotation_H.Responser;
            ViewBag.StatusOfResponse = quotation_H.StatusOfResponse;
            ViewBag.ResponseDate = quotation_H.ResponseDate;
            ViewBag.Response = quotation_H.Response;
            ViewBag.CustomerId = quotation_H.CustomerId;
            ViewBag.TaxRate = quotation_H.TaxRate;
            ViewBag.AddressSName = quotation_H.AddressSName;
            ViewBag.Address = quotation_H.Address;
            ViewBag.Contact = quotation_H.Contact;
            ViewBag.TelNo = quotation_H.TelNo;
            ViewBag.FaxNo = quotation_H.FaxNo;
            ViewBag.ProductNo = quotation_H.ProductNo;
            ViewBag.ProductName = quotation_H.ProductName;
            ViewBag.SerialNo = quotation_H.SerialNo;
            ViewBag.AssetNo = quotation_H.AssetNo;
            ViewBag.UnderWarranty = quotation_H.UnderWarranty;
            ViewBag.SaleDate = quotation_H.SaleDate;
            ViewBag.PurchaseDate = quotation_H.PurchaseDate;
            ViewBag.WarrantyStartDate = quotation_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = quotation_H.WarrantyExpiryDate;
            ViewBag.Remark = quotation_H.Remark;
            ViewBag.InternalRemark = quotation_H.InternalRemark;
            ViewBag.TaxExcluded = quotation_H.TaxExcluded;
            ViewBag.TestResult = quotation_H.TestResult;
            ViewBag.Tax = quotation_H.Tax;
            ViewBag.TaxIncluded = quotation_H.TaxIncluded;
            ViewBag.Confirmed = quotation_H.Confirmed;

            Quotation_DLogic DLogic = new Quotation_DLogic();

            Quotation_D quotation_D = new Quotation_D();
            quotation_D.QuotationType = QuotationType;
            quotation_D.QuotationNo = QuotationNo;
            ViewData["Quotation_D"] = DLogic.SearchQuotation_D(quotation_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string QuotationType, string QuotationNo)
        {
            Quotation_HLogic logic = new Quotation_HLogic();
            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = QuotationType;
            quotation_H.QuotationNo = QuotationNo;
            quotation_H = logic.GetQuotation_HInfo(quotation_H);
            ViewBag.QuotationType = quotation_H.QuotationType;
            ViewBag.QuotationNo = quotation_H.QuotationNo;
            ViewBag.OrderSName = quotation_H.OrderSName;
            ViewBag.OrderDate = quotation_H.OrderDate;
            ViewBag.StatusCode = quotation_H.StatusCode;
            ViewBag.NoOfPrints = quotation_H.NoOfPrints;
            ViewBag.Repairman = quotation_H.Repairman;
            ViewBag.SourceOrderType = quotation_H.SourceOrderType;
            ViewBag.SourceOrderNo = quotation_H.SourceOrderNo;
            ViewBag.Responser = quotation_H.Responser;
            ViewBag.StatusOfResponse = quotation_H.StatusOfResponse;
            ViewBag.ResponseDate = quotation_H.ResponseDate;
            ViewBag.Response = quotation_H.Response;
            ViewBag.CustomerId = quotation_H.CustomerId;
            ViewBag.TaxRate = quotation_H.TaxRate;
            ViewBag.AddressSName = quotation_H.AddressSName;
            ViewBag.Address = quotation_H.Address;
            ViewBag.Contact = quotation_H.Contact;
            ViewBag.TelNo = quotation_H.TelNo;
            ViewBag.FaxNo = quotation_H.FaxNo;
            ViewBag.ProductNo = quotation_H.ProductNo;
            ViewBag.ProductName = quotation_H.ProductName;
            ViewBag.SerialNo = quotation_H.SerialNo;
            ViewBag.AssetNo = quotation_H.AssetNo;
            ViewBag.UnderWarranty = quotation_H.UnderWarranty;
            ViewBag.SaleDate = quotation_H.SaleDate;
            ViewBag.PurchaseDate = quotation_H.PurchaseDate;
            ViewBag.WarrantyStartDate = quotation_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = quotation_H.WarrantyExpiryDate;
            ViewBag.Remark = quotation_H.Remark;
            ViewBag.InternalRemark = quotation_H.InternalRemark;
            ViewBag.TaxExcluded = quotation_H.TaxExcluded;
            ViewBag.TestResult = quotation_H.TestResult;
            ViewBag.Tax = quotation_H.Tax;
            ViewBag.TaxIncluded = quotation_H.TaxIncluded;
            ViewBag.Confirmed = quotation_H.Confirmed;

            Quotation_DLogic DLogic = new Quotation_DLogic();

            Quotation_D quotation_D = new Quotation_D();
            quotation_D.QuotationType = QuotationType;
            quotation_D.QuotationNo = QuotationNo;
            ViewData["Quotation_D"] = DLogic.SearchQuotation_D(quotation_D);
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string QuotationType, string QuotationNo)
        {
            Quotation_HLogic logic = new Quotation_HLogic();
            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = QuotationType;
            quotation_H.QuotationNo = QuotationNo;
            ViewBag.OrderSName = quotation_H.OrderSName;
            quotation_H = logic.GetQuotation_HInfo(quotation_H);
            ViewBag.QuotationType = quotation_H.QuotationType;
            ViewBag.QuotationNo = quotation_H.QuotationNo;
            ViewBag.OrderDate = quotation_H.OrderDate;
            ViewBag.StatusCode = quotation_H.StatusCode;
            ViewBag.NoOfPrints = quotation_H.NoOfPrints;
            ViewBag.Repairman = quotation_H.Repairman;
            ViewBag.SourceOrderType = quotation_H.SourceOrderType;
            ViewBag.SourceOrderNo = quotation_H.SourceOrderNo;
            ViewBag.Responser = quotation_H.Responser;
            ViewBag.StatusOfResponse = quotation_H.StatusOfResponse;
            ViewBag.ResponseDate = quotation_H.ResponseDate;
            ViewBag.Response = quotation_H.Response;
            ViewBag.CustomerId = quotation_H.CustomerId;
            ViewBag.TaxRate = quotation_H.TaxRate;
            ViewBag.AddressSName = quotation_H.AddressSName;
            ViewBag.Address = quotation_H.Address;
            ViewBag.Contact = quotation_H.Contact;
            ViewBag.TelNo = quotation_H.TelNo;
            ViewBag.FaxNo = quotation_H.FaxNo;
            ViewBag.ProductNo = quotation_H.ProductNo;
            ViewBag.ProductName = quotation_H.ProductName;
            ViewBag.SerialNo = quotation_H.SerialNo;
            ViewBag.AssetNo = quotation_H.AssetNo;
            ViewBag.UnderWarranty = quotation_H.UnderWarranty;
            ViewBag.SaleDate = quotation_H.SaleDate;
            ViewBag.PurchaseDate = quotation_H.PurchaseDate;
            ViewBag.WarrantyStartDate = quotation_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = quotation_H.WarrantyExpiryDate;
            ViewBag.Remark = quotation_H.Remark;
            ViewBag.InternalRemark = quotation_H.InternalRemark;
            ViewBag.TaxExcluded = quotation_H.TaxExcluded;
            ViewBag.TestResult = quotation_H.TestResult;
            ViewBag.Tax = quotation_H.Tax;
            ViewBag.TaxIncluded = quotation_H.TaxIncluded;
            ViewBag.Confirmed = quotation_H.Confirmed;

            Quotation_DLogic DLogic = new Quotation_DLogic();

            Quotation_D quotation_D = new Quotation_D();
            quotation_D.QuotationType = QuotationType;
            quotation_D.QuotationNo = QuotationNo;
            ViewData["Quotation_D"] = DLogic.SearchQuotation_D(quotation_D);
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Confirmed(string Type)
        {
            Quotation_HLogic logic = new Quotation_HLogic();

            Quotation_H quotation_H = new Quotation_H();
            quotation_H.QuotationType = Request.Form["QuotationType"];
            quotation_H.QuotationNo = Request.Form["QuotationNo"];
            if (Type == "Y")
            {
                quotation_H.Confirmed = "Y";
                quotation_H.StatusCode = "1";
                quotation_H.Confirmor = "";
                quotation_H.ConfirmedDate = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            if (Type == "V")
            {
                quotation_H.Confirmed = "V";
                quotation_H.StatusCode = "3";
                //phoneService_H.VoidRemark = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedQuotation_H(quotation_H, out infomsg))
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