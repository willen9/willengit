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

namespace SERI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = ",h.Confirmed";
            string condition = ",<>";
            string value = ",V";
            ViewBag.selCond3 = "<>";
            ViewBag.comfirmed = "V";
            ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            RGA_H rGA_H = new RGA_H();
            rGA_H.Company = "";
            rGA_H.UserGroup = "";
            rGA_H.Creator = "";
            rGA_H.RGAType = Request.Form["RGAType"];
            rGA_H.RGANo = Request.Form["RGANo"];
            rGA_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rGA_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            rGA_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]==null?"0":Request.Form["NoOfPrints"]);
            rGA_H.SourceOrderType = Request.Form["SourceOrderType"];
            rGA_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rGA_H.Version = Request.Form["Version"];
            rGA_H.Repairman = Request.Form["Repairman"];
            rGA_H.CustomerId = Request.Form["CustomerId"];
            rGA_H.AddressSName = Request.Form["AddressSName"];
            rGA_H.Address = Request.Form["Address"];
            rGA_H.Contact = Request.Form["Contact"];
            rGA_H.TelNo = Request.Form["TelNo"];
            rGA_H.FaxNo = Request.Form["FaxNo"];
            rGA_H.ProductNo = Request.Form["ProductNo"];
            rGA_H.ProductName = Request.Form["ProductName"];
            rGA_H.AssetNo = Request.Form["AssetNo"];
            if (Request.Form["SerialNo"] != "")
            {
                rGA_H.SerialNo = Request.Form["SerialNo"];
                rGA_H.ContractType = Request.Form["ContractType"];
                rGA_H.ContractNo = Request.Form["ContractNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    rGA_H.UnderWarranty = "Y";
                }
                else
                {
                    rGA_H.UnderWarranty = "N";
                }
                rGA_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
                rGA_H.PurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
                rGA_H.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rGA_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            }
            rGA_H.PCompletionDate = Request.Form["PCompletionDate"].Replace("/","");
            rGA_H.CompletionDate = Request.Form["CompletionDate"];
            //rGA_H.Closed = Request.Form["Closed"];
            rGA_H.OptionDetail = Request.Form["OptionDetail"];
            rGA_H.Returned = Request.Form["Returned"]==null?"N":"Y";
            rGA_H.MalfunctionReason = Request.Form["MalfunctionReason"];
            rGA_H.TestResult = Request.Form["TestResult"];
            rGA_H.InternalRemark = Request.Form["InternalRemark"];
            //rGA_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            rGA_H.Confirmor = "";
            rGA_H.Remark = Request.Form["Remark"];

            //string strQItemId = Request.Form["strQItemId"];
            string strQQuestionNo = Request.Form["strQQuestionNo"];
            string strQQuestion = Request.Form["strQQuestion"];
            string strQDescription = Request.Form["strQDescription"];

            string strDItemId = Request.Form["strDItemId"];
            string strDPartNo = Request.Form["strDPartNo"];
            string strDPartName = Request.Form["strDPartName"];
            string strDQTY = Request.Form["strDQTY"];
            string strDUnit = Request.Form["strDUnit"];
            string strDRemark = Request.Form["strDRemark"];
            string strDRepaired = Request.Form["strDRepaired"];
            string strDSourceOrderType = Request.Form["strDSourceOrderType"];
            string strDSourceOrderNo = Request.Form["strDSourceOrderNo"];
            string strDSourceItemId = Request.Form["strDSourceItemId"];
            //string strDResponseOrderType = Request.Form["strDResponseOrderType"];
            //string strDResponseOrderNo = Request.Form["strDResponseOrderNo"];
            //string strDResponseDate = Request.Form["strDResponseDate"];

            RGA_HLogic logic = new RGA_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.AddRGA_H(rGA_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out infomsg))
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
            RGA_H rGA_H = new RGA_H();
            rGA_H.Company = "";
            rGA_H.UserGroup = "";
            rGA_H.Modifier = "";
            rGA_H.RGAType = Request.Form["RGAType"];
            rGA_H.RGANo = Request.Form["RGANo"];
            rGA_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rGA_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            rGA_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rGA_H.SourceOrderType = Request.Form["SourceOrderType"];
            rGA_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rGA_H.Version = Request.Form["Version"];
            rGA_H.Repairman = Request.Form["Repairman"];
            rGA_H.CustomerId = Request.Form["CustomerId"];
            rGA_H.AddressSName = Request.Form["AddressSName"];
            rGA_H.Address = Request.Form["Address"];
            rGA_H.Contact = Request.Form["Contact"];
            rGA_H.TelNo = Request.Form["TelNo"];
            rGA_H.FaxNo = Request.Form["FaxNo"];
            rGA_H.ProductNo = Request.Form["ProductNo"];
            rGA_H.ProductName = Request.Form["ProductName"];
            rGA_H.AssetNo = Request.Form["AssetNo"];
            if (Request.Form["SerialNo"] != "")
            {
                rGA_H.SerialNo = Request.Form["SerialNo"];
                rGA_H.ContractType = Request.Form["ContractType"];
                rGA_H.ContractNo = Request.Form["ContractNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    rGA_H.UnderWarranty = "Y";
                }
                else
                {
                    rGA_H.UnderWarranty = "N";
                }
                rGA_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
                rGA_H.PurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
                rGA_H.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                rGA_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            }
            rGA_H.PCompletionDate = Request.Form["PCompletionDate"].Replace("/", "");
            rGA_H.CompletionDate = Request.Form["CompletionDate"];
            rGA_H.Closed = Request.Form["Closed"];
            rGA_H.OptionDetail = Request.Form["OptionDetail"];
            rGA_H.Returned = Request.Form["Returned"] == null ? "N" : "Y";
            rGA_H.MalfunctionReason = Request.Form["MalfunctionReason"];
            rGA_H.TestResult = Request.Form["TestResult"];
            rGA_H.InternalRemark = Request.Form["InternalRemark"];
            //rGA_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            rGA_H.Confirmor = "";
            rGA_H.Remark = Request.Form["Remark"];

            //string strQItemId = Request.Form["strQItemId"];
            string strQQuestionNo = Request.Form["strQQuestionNo"];
            string strQQuestion = Request.Form["strQQuestion"];
            string strQDescription = Request.Form["strQDescription"];

            string strDItemId = Request.Form["strDItemId"];
            string strDPartNo = Request.Form["strDPartNo"];
            string strDPartName = Request.Form["strDPartName"];
            string strDQTY = Request.Form["strDQTY"];
            string strDUnit = Request.Form["strDUnit"];
            string strDRemark = Request.Form["strDRemark"];
            string strDRepaired = Request.Form["strDRepaired"];
            string strDSourceOrderType = Request.Form["strDSourceOrderType"];
            string strDSourceOrderNo = Request.Form["strDSourceOrderNo"];
            string strDSourceItemId = Request.Form["strDSourceItemId"];
            //string strDResponseOrderType = Request.Form["strDResponseOrderType"];
            //string strDResponseOrderNo = Request.Form["strDResponseOrderNo"];
            //string strDResponseDate = Request.Form["strDResponseDate"];

            RGA_HLogic logic = new RGA_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.UpdateRGA_H(rGA_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string RGAType, string RGANo)
        {
            RGA_HLogic logic = new RGA_HLogic();

            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = RGAType;
            rGA_H.RGANo = RGANo;

            string msg = "";
            if (!logic.DelRGA_H(rGA_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string RGAType, string RGANo)
        {
            RGA_HLogic logic = new RGA_HLogic();

            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = RGAType;
            rGA_H.RGANo = RGANo;

            rGA_H = logic.GetRGA_HInfo(rGA_H);

            ViewBag.RGAType = rGA_H.RGAType;
            ViewBag.OrderSName = rGA_H.OrderSName;
            ViewBag.RGANo = rGA_H.RGANo;
            ViewBag.OrderDate = rGA_H.OrderDate;
            ViewBag.StatusCode = rGA_H.StatusCode;
            ViewBag.NoOfPrints = rGA_H.NoOfPrints;
            ViewBag.SourceOrderType = rGA_H.SourceOrderType;
            ViewBag.SourceOrderNo = rGA_H.SourceOrderNo;
            ViewBag.Version = rGA_H.Version;
            ViewBag.Repairman = rGA_H.Repairman;
            ViewBag.CustomerId = rGA_H.CustomerId;
            ViewBag.CustomerName = rGA_H.CustomerName;
            ViewBag.CustomerType = rGA_H.CustomerType;
            ViewBag.AddressSName = rGA_H.AddressSName;
            ViewBag.Address = rGA_H.Address;
            ViewBag.Contact = rGA_H.Contact;
            ViewBag.TelNo = rGA_H.TelNo;
            ViewBag.FaxNo = rGA_H.FaxNo;
            ViewBag.ProductNo = rGA_H.ProductNo;
            ViewBag.ProductName = rGA_H.ProductName;
            ViewBag.SerialNo = rGA_H.SerialNo;
            ViewBag.AssetNo = rGA_H.AssetNo;
            ViewBag.ContractType = rGA_H.ContractType;
            ViewBag.ContractNo = rGA_H.ContractNo;
            ViewBag.UnderWarranty = rGA_H.UnderWarranty;
            ViewBag.SaleDate = rGA_H.SaleDate;
            ViewBag.PurchaseDate = rGA_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rGA_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rGA_H.WarrantyExpiryDate;
            ViewBag.PCompletionDate = rGA_H.PCompletionDate;
            ViewBag.CompletionDate = rGA_H.CompletionDate;
            ViewBag.Closed = rGA_H.Closed;
            ViewBag.OptionDetail = rGA_H.OptionDetail;
            ViewBag.Returned = rGA_H.Returned;
            ViewBag.MalfunctionReason = rGA_H.MalfunctionReason;
            ViewBag.TestResult = rGA_H.TestResult;
            ViewBag.InternalRemark = rGA_H.InternalRemark;
            ViewBag.Confirmed = rGA_H.Confirmed;
            ViewBag.Remark = rGA_H.Remark;
            ViewBag.AssetNo = rGA_H.AssetNo;

            RGA_QLogic Qlogic = new RGA_QLogic();
            RGA_Q rGA_Q = new RGA_Q();
            rGA_Q.RGAType = RGAType;
            rGA_Q.RGANo = RGANo;
            ViewData["RGA_Q"] = Qlogic.SearchRGA_Q(rGA_Q);

            RGA_DLogic Dlogic = new RGA_DLogic();
            RGA_D rGA_D = new RGA_D();
            rGA_D.RGAType = RGAType;
            rGA_D.RGANo = RGANo;
            ViewData["RGA_D"] = Dlogic.SearchRGA_D(rGA_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string RGAType, string RGANo)
        {
            RGA_HLogic logic = new RGA_HLogic();

            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = RGAType;
            rGA_H.RGANo = RGANo;

            rGA_H = logic.GetRGA_HInfo(rGA_H);

            ViewBag.RGAType = rGA_H.RGAType;
            ViewBag.OrderSName = rGA_H.OrderSName;
            ViewBag.RGANo = rGA_H.RGANo;
            ViewBag.OrderDate = rGA_H.OrderDate;
            ViewBag.StatusCode = rGA_H.StatusCode;
            ViewBag.NoOfPrints = rGA_H.NoOfPrints;
            ViewBag.SourceOrderType = rGA_H.SourceOrderType;
            ViewBag.SourceOrderNo = rGA_H.SourceOrderNo;
            ViewBag.Version = rGA_H.Version;
            ViewBag.Repairman = rGA_H.Repairman;
            ViewBag.CustomerId = rGA_H.CustomerId;
            ViewBag.CustomerName = rGA_H.CustomerName;
            ViewBag.CustomerType = rGA_H.CustomerType;
            ViewBag.AddressSName = rGA_H.AddressSName;
            ViewBag.Address = rGA_H.Address;
            ViewBag.Contact = rGA_H.Contact;
            ViewBag.TelNo = rGA_H.TelNo;
            ViewBag.FaxNo = rGA_H.FaxNo;
            ViewBag.ProductNo = rGA_H.ProductNo;
            ViewBag.ProductName = rGA_H.ProductName;
            ViewBag.SerialNo = rGA_H.SerialNo;
            ViewBag.AssetNo = rGA_H.AssetNo;
            ViewBag.ContractType = rGA_H.ContractType;
            ViewBag.ContractNo = rGA_H.ContractNo;
            ViewBag.UnderWarranty = rGA_H.UnderWarranty;
            ViewBag.SaleDate = rGA_H.SaleDate;
            ViewBag.PurchaseDate = rGA_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rGA_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rGA_H.WarrantyExpiryDate;
            ViewBag.PCompletionDate = rGA_H.PCompletionDate;
            ViewBag.CompletionDate = rGA_H.CompletionDate;
            ViewBag.Closed = rGA_H.Closed;
            ViewBag.OptionDetail = rGA_H.OptionDetail;
            ViewBag.Returned = rGA_H.Returned;
            ViewBag.MalfunctionReason = rGA_H.MalfunctionReason;
            ViewBag.TestResult = rGA_H.TestResult;
            ViewBag.InternalRemark = rGA_H.InternalRemark;
            ViewBag.Confirmed = rGA_H.Confirmed;
            ViewBag.Remark = rGA_H.Remark;
            ViewBag.AssetNo = rGA_H.AssetNo;

            RGA_QLogic Qlogic = new RGA_QLogic();
            RGA_Q rGA_Q = new RGA_Q();
            rGA_Q.RGAType = RGAType;
            rGA_Q.RGANo = RGANo;
            ViewData["RGA_Q"] = Qlogic.SearchRGA_Q(rGA_Q);

            RGA_DLogic Dlogic = new RGA_DLogic();
            RGA_D rGA_D = new RGA_D();
            rGA_D.RGAType = RGAType;
            rGA_D.RGANo = RGANo;
            ViewData["RGA_D"] = Dlogic.SearchRGA_D(rGA_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Copy(string RGAType, string RGANo)
        {
            RGA_HLogic logic = new RGA_HLogic();

            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = RGAType;
            rGA_H.RGANo = RGANo;

            rGA_H = logic.GetRGA_HInfo(rGA_H);

            //ViewBag.RGAType = rGA_H.RGAType;
            //ViewBag.OrderSName = rGA_H.OrderSName;
            //ViewBag.RGANo = rGA_H.RGANo;
            ViewBag.OrderDate = rGA_H.OrderDate;
            ViewBag.StatusCode = rGA_H.StatusCode;
            ViewBag.NoOfPrints = rGA_H.NoOfPrints;
            ViewBag.SourceOrderType = rGA_H.SourceOrderType;
            ViewBag.SourceOrderNo = rGA_H.SourceOrderNo;
            ViewBag.Version = rGA_H.Version;
            ViewBag.Repairman = rGA_H.Repairman;
            ViewBag.CustomerId = rGA_H.CustomerId;
            ViewBag.CustomerName = rGA_H.CustomerName;
            ViewBag.CustomerType = rGA_H.CustomerType;
            ViewBag.AddressSName = rGA_H.AddressSName;
            ViewBag.Address = rGA_H.Address;
            ViewBag.Contact = rGA_H.Contact;
            ViewBag.TelNo = rGA_H.TelNo;
            ViewBag.FaxNo = rGA_H.FaxNo;
            ViewBag.ProductNo = rGA_H.ProductNo;
            ViewBag.ProductName = rGA_H.ProductName;
            ViewBag.SerialNo = rGA_H.SerialNo;
            ViewBag.AssetNo = rGA_H.AssetNo;
            ViewBag.ContractType = rGA_H.ContractType;
            ViewBag.ContractNo = rGA_H.ContractNo;
            ViewBag.UnderWarranty = rGA_H.UnderWarranty;
            ViewBag.SaleDate = rGA_H.SaleDate;
            ViewBag.PurchaseDate = rGA_H.PurchaseDate;
            ViewBag.WarrantyStartDate = rGA_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rGA_H.WarrantyExpiryDate;
            ViewBag.PCompletionDate = rGA_H.PCompletionDate;
            ViewBag.CompletionDate = rGA_H.CompletionDate;
            ViewBag.Closed = rGA_H.Closed;
            ViewBag.OptionDetail = rGA_H.OptionDetail;
            ViewBag.Returned = rGA_H.Returned;
            ViewBag.MalfunctionReason = rGA_H.MalfunctionReason;
            ViewBag.TestResult = rGA_H.TestResult;
            ViewBag.InternalRemark = rGA_H.InternalRemark;
            ViewBag.Confirmed = rGA_H.Confirmed;
            ViewBag.Remark = rGA_H.Remark;
            ViewBag.AssetNo = rGA_H.AssetNo;

            RGA_QLogic Qlogic = new RGA_QLogic();
            RGA_Q rGA_Q = new RGA_Q();
            rGA_Q.RGAType = RGAType;
            rGA_Q.RGANo = RGANo;
            ViewData["RGA_Q"] = Qlogic.SearchRGA_Q(rGA_Q);

            RGA_DLogic Dlogic = new RGA_DLogic();
            RGA_D rGA_D = new RGA_D();
            rGA_D.RGAType = RGAType;
            rGA_D.RGANo = RGANo;
            ViewData["RGA_D"] = Dlogic.SearchRGA_D(rGA_D);

            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
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
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("維修單別,單據名稱,維修單號,客戶簡稱,地址簡稱,品號,品名,序號,狀態,維修人員,資產編號,預計完工日期");
                //    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;
                //    foreach (var obj in objRGA_H)
                //    {
                //        sw.WriteLine(obj.RGAType + "," + obj.OrderSName + ","
                //             + obj.RGANo + "," + obj.CustomerName + "," + obj.AddressSName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + ","
                //            + obj.Confirmed + "," + obj.RepairmanName + "," + obj.AssetNo + "," + obj.PCompletionDate);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RGA");
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

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("維修單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("維修單號");
                    row.CreateCell(3).SetCellValue("客戶簡稱");
                    row.CreateCell(4).SetCellValue("地址簡稱");
                    row.CreateCell(5).SetCellValue("品號");
                    row.CreateCell(6).SetCellValue("品名");
                    row.CreateCell(7).SetCellValue("序號");
                    row.CreateCell(8).SetCellValue("狀態");
                    row.CreateCell(9).SetCellValue("維修人員");
                    row.CreateCell(10).SetCellValue("資產編號");
                    row.CreateCell(11).SetCellValue("預計完工日期");


                    int index = 1;

                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;

                    foreach (var obj in objRGA_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.RGAType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RGANo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.AddressSName.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(6).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(7).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(8).SetCellValue(obj.StatusCode.ToString());
                        row.CreateCell(9).SetCellValue(obj.RepairmanName.ToString());
                        row.CreateCell(10).SetCellValue(obj.AssetNo.ToString());
                        row.CreateCell(11).SetCellValue(obj.PCompletionDate.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RGA.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA.xls");
            }
            else
            {
                if (Request.Form["rgano"].Trim() != "")
                {
                    col += ",h.RGANo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rgano"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                if (Request.Form["comfirmed"].Trim() != "")
                {
                    col += ",h.Confirmed";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["comfirmed"];
                }
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rgano = Request.Form["rgano"];
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
            List<RGA_Q> objRGA_Q = new List<RGA_Q>();
            ViewData["RGA_Q"] = objRGA_Q;
            List<RGA_D> objRGA_D = new List<RGA_D>();
            ViewData["RGA_D"] = objRGA_D;
            ViewBag.Version = "0000";
            ViewBag.NoOfPrints = "0";
            ViewBag.StatusCode = "00.未處理";
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsPhoneService(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;
            return Json(logic.IsPhoneService_H(phoneService_H), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchWarrantySerialNo(string Col, string Condition, string conditionValue, int Page)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchSerialNo(Col,  Condition,  conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchWarrantyDInfo(string ProductNo, string SerialNo, string ChangeDate, string WarrantyType, string TargetId)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            return Json(logic.SearchWarranty_DInfo(ProductNo, SerialNo, ChangeDate.Replace("/", ""), WarrantyType, TargetId), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchWarrantySerialNoInfo(string SerialNo, string ChangeDate, string WarrantyType)
        //{
        //    Warranty_DLogic logic = new Warranty_DLogic();
        //    return Json(logic.SearchSerialNoInfo(SerialNo, ChangeDate.Replace("/","")), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "B2"), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchOrderType(string OrderType, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "B2";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchServiceArrange(string SerArrangeOrderType, string SerArrangeOrderNo, int Page)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
            List<ServiceArrange_H> lst = logic.GetServiceArrange_H(serviceArrange_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //通過定保計劃查詢信息
        public JsonResult SearchServiceArrangeInfo(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrangeModi_HLogic Hlogic = new ServiceArrangeModi_HLogic();
            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrangeModi_H = Hlogic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

            if(serviceArrangeModi_H==null)
            {

                ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
                ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
                serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
                serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
                serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);
                return Json(serviceArrange_H, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(serviceArrangeModi_H, JsonRequestBehavior.AllowGet);
            }

        }

        //通過定保計劃查詢信息
        public JsonResult SearchServiceArrangeDInfo(string SerArrangeOrderType, string SerArrangeOrderNo,string Version)
        {
            ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
            List<ServiceArrangeModi_D> Dlst = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);
            if(Dlst.Count==0)
            {
                ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
                List<ServiceArrange_D> lst = logic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(Dlst, JsonRequestBehavior.AllowGet);
            }

            
        }

        [HttpPost]
        public JsonResult Confirmed(string Type)
        {
            RGA_HLogic logic = new RGA_HLogic();

            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = Request.Form["RGAType"];
            rGA_H.RGANo = Request.Form["RGANo"];
            if (Type == "Y")
            {
                rGA_H.Confirmed = "Y";
                rGA_H.StatusCode = "20";
                rGA_H.Confirmor = "";
                rGA_H.ConfirmedDate = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            if (Type == "V")
            {
                rGA_H.Confirmed = "V";
                rGA_H.StatusCode = "D";
                //phoneService_H.VoidRemark = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedRGA_H(rGA_H, out infomsg))
            {
                msg = "NO-操作失敗！" + infomsg;
            }
            else
            {
                msg = "OK-成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SearchBOMD(string MajorComponentNo, string Col,string Condition, string conditionValue, int Page)
        {
            BOM_DLogic logic = new BOM_DLogic();
            List<BOM_D> lst = logic.SearchBOMD(MajorComponentNo, Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}