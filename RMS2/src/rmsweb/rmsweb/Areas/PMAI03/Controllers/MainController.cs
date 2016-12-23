using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace PMAI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["ServiceArrangeModi_H"] = logic.SearchServiceArrangeModi_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.Company = "";
            serviceArrangeModi_H.UserGroup = "";
            serviceArrangeModi_H.Creator = "";
            serviceArrangeModi_H.SerArrangeOrderType = Request.Form["SerArrangeOrderType"];
            serviceArrangeModi_H.SerArrangeOrderNo = Request.Form["SerArrangeOrderNo"];
            serviceArrangeModi_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.ConfirmedDate = Request.Form["ConfirmedDate"] == null ? "" : Request.Form["ConfirmedDate"].ToString().Replace("/", ""); 
            serviceArrangeModi_H.CustomerId = Request.Form["CustomerId"];
            serviceArrangeModi_H.Version = Request.Form["Version"];
            serviceArrangeModi_H.SourceOrderType = Request.Form["SourceOrderType"];
            serviceArrangeModi_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            serviceArrangeModi_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            serviceArrangeModi_H.ProductNo = Request.Form["ProductNo"];
            serviceArrangeModi_H.ProductName = Request.Form["ProductName"];
            serviceArrangeModi_H.SerialNo = Request.Form["SerialNo"];
            serviceArrangeModi_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.RoutineServiceFreq = int.Parse(Request.Form["RoutineServiceFreq"].ToString());
            serviceArrangeModi_H.WarrantyPeriod = int.Parse(Request.Form["WarrantyPeriod"].ToString() == "" ? "0" : Request.Form["WarrantyPeriod"].ToString());
            serviceArrangeModi_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            serviceArrangeModi_H.Confirmor = "";
            serviceArrangeModi_H.NewRemark = Request.Form["NewRemark"];
            serviceArrangeModi_H.NewInternalRemark = Request.Form["NewInternalRemark"];
            serviceArrangeModi_H.ForceToClose = Request.Form["ForceToClose"] == null ? "N" : "Y";
            serviceArrangeModi_H.ModiReason = Request.Form["ModiReason"];


            string strItemId = Request.Form["strCItemId"];
            string strArrangeMonth = Request.Form["strCArrangeMonth"];
            string strAddressSName = Request.Form["strCAddressSName"];
            string strAddress = Request.Form["strCAddress"];
            string strIsClosed = Request.Form["strCIsClosed"];
            string strRemark = Request.Form["strCRemark"];

            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();

            string infomsg = "";
            string msg = "";
            if (int.Parse(serviceArrangeModi_H.Version==""?"0": serviceArrangeModi_H.Version) >1)
            {
                if (!logic.UpdateServiceArrangeModi_H(serviceArrangeModi_H, strItemId,
                 strArrangeMonth, strAddressSName, strAddress,
                 strIsClosed, strRemark, out infomsg))
                {
                    msg = "NO-新增失敗;" + infomsg;
                }
                else
                {
                    msg = "OK-保存成功";
                }
            }else
            {
                if (!logic.AddServiceArrangeModi_H(serviceArrangeModi_H, strItemId,
                     strArrangeMonth, strAddressSName, strAddress,
                     strIsClosed, strRemark, out infomsg))
                {
                    msg = "NO-新增失敗;" + infomsg;
                }
                else
                {
                    msg = "OK-保存成功";
                }
            }
            
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Exit()
        {
            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.Company = "";
            serviceArrangeModi_H.UserGroup = "";
            serviceArrangeModi_H.Modifier = "";
            serviceArrangeModi_H.SerArrangeOrderType = Request.Form["SerArrangeOrderType"];
            serviceArrangeModi_H.SerArrangeOrderNo = Request.Form["SerArrangeOrderNo"];
            serviceArrangeModi_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.ConfirmedDate = Request.Form["ConfirmedDate"] == null ? "" : Request.Form["ConfirmedDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.CustomerId = Request.Form["CustomerId"];
            serviceArrangeModi_H.Version = Request.Form["Version"];
            serviceArrangeModi_H.SourceOrderType = Request.Form["SourceOrderType"];
            serviceArrangeModi_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            serviceArrangeModi_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            serviceArrangeModi_H.ProductNo = Request.Form["ProductNo"];
            serviceArrangeModi_H.ProductName = Request.Form["ProductName"];
            serviceArrangeModi_H.SerialNo = Request.Form["SerialNo"];
            serviceArrangeModi_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.RoutineServiceFreq = int.Parse(Request.Form["RoutineServiceFreq"].ToString());
            serviceArrangeModi_H.WarrantyPeriod = int.Parse(Request.Form["WarrantyPeriod"].ToString()==""?"0":Request.Form["WarrantyPeriod"].ToString());
            serviceArrangeModi_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
            serviceArrangeModi_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            serviceArrangeModi_H.Confirmor = "";
            serviceArrangeModi_H.NewRemark = Request.Form["NewRemark"];
            serviceArrangeModi_H.NewInternalRemark = Request.Form["NewInternalRemark"];
            serviceArrangeModi_H.ForceToClose = Request.Form["ForceToClose"] == null ? "N" : "Y";
            serviceArrangeModi_H.ModiReason = Request.Form["ModiReason"];


            string strItemId = Request.Form["strCItemId"];
            string strArrangeMonth = Request.Form["strCArrangeMonth"];
            string strAddressSName = Request.Form["strCAddressSName"];
            string strAddress = Request.Form["strCAddress"];
            string strIsClosed = Request.Form["strCIsClosed"];
            string strRemark = Request.Form["strCRemark"];

            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateServiceArrangeModi_H(serviceArrangeModi_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out infomsg))
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
        public ActionResult Delete(string SerArrangeOrderType, string SerArrangeOrderNo, string Version)
        {
            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();

            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrangeModi_H.Version = Version;

            string msg = "";
            if (!logic.DelServiceArrangeModi_H(serviceArrangeModi_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string SerArrangeOrderType, string SerArrangeOrderNo, string Version)
        {
            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();

            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrangeModi_H.Version = Version;

            serviceArrangeModi_H = logic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

            ViewBag.SerArrangeOrderType = serviceArrangeModi_H.SerArrangeOrderType;
            ViewBag.SerArrangeOrderNo = serviceArrangeModi_H.SerArrangeOrderNo;
            ViewBag.OrderDate = serviceArrangeModi_H.OrderDate;
            ViewBag.ConfirmedDate = serviceArrangeModi_H.ConfirmedDate;
            ViewBag.CustomerId = serviceArrangeModi_H.CustomerId;
            ViewBag.Version = serviceArrangeModi_H.Version;
            ViewBag.SourceOrderType = serviceArrangeModi_H.SourceOrderType;
            ViewBag.SourceOrderNo = serviceArrangeModi_H.SourceOrderNo;
            ViewBag.SourceOrderItemId = serviceArrangeModi_H.SourceOrderItemId;
            ViewBag.ProductNo = serviceArrangeModi_H.ProductNo;
            ViewBag.ProductName = serviceArrangeModi_H.ProductName;
            ViewBag.SerialNo = serviceArrangeModi_H.SerialNo;
            ViewBag.SaleDate = serviceArrangeModi_H.SaleDate;
            ViewBag.RoutineServiceFreq = serviceArrangeModi_H.RoutineServiceFreq;
            serviceArrangeModi_H.WarrantyPeriod = serviceArrangeModi_H.WarrantyPeriod;
            ViewBag.WarrantySDate = serviceArrangeModi_H.WarrantySDate;
            ViewBag.WarrantyEDate = serviceArrangeModi_H.WarrantyEDate;
            ViewBag.Confirmed = serviceArrangeModi_H.Confirmed;
            ViewBag.Confirmor = serviceArrangeModi_H.Confirmor;
            ViewBag.NewRemark = serviceArrangeModi_H.NewRemark;
            ViewBag.NewInternalRemark = serviceArrangeModi_H.NewInternalRemark;
            ViewBag.ForceToClose = serviceArrangeModi_H.ForceToClose;
            ViewBag.ModiReason = serviceArrangeModi_H.ModiReason;

            ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
            ViewData["ServiceArrangeModi_D"] = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo,Version);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string SerArrangeOrderType, string SerArrangeOrderNo, string Version)
        {
            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();

            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrangeModi_H.Version = Version;

            serviceArrangeModi_H = logic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

            ViewBag.SerArrangeOrderType = serviceArrangeModi_H.SerArrangeOrderType;
            ViewBag.SerArrangeOrderNo = serviceArrangeModi_H.SerArrangeOrderNo;
            ViewBag.OrderDate = serviceArrangeModi_H.OrderDate;
            ViewBag.ConfirmedDate = serviceArrangeModi_H.ConfirmedDate;
            ViewBag.CustomerId = serviceArrangeModi_H.CustomerId;
            ViewBag.Version = serviceArrangeModi_H.Version;
            ViewBag.SourceOrderType = serviceArrangeModi_H.SourceOrderType;
            ViewBag.SourceOrderNo = serviceArrangeModi_H.SourceOrderNo;
            ViewBag.SourceOrderItemId = serviceArrangeModi_H.SourceOrderItemId;
            ViewBag.ProductNo = serviceArrangeModi_H.ProductNo;
            ViewBag.ProductName = serviceArrangeModi_H.ProductName;
            ViewBag.SerialNo = serviceArrangeModi_H.SerialNo;
            ViewBag.SaleDate = serviceArrangeModi_H.SaleDate;
            ViewBag.RoutineServiceFreq = serviceArrangeModi_H.RoutineServiceFreq;
            serviceArrangeModi_H.WarrantyPeriod = serviceArrangeModi_H.WarrantyPeriod;
            ViewBag.WarrantySDate = serviceArrangeModi_H.WarrantySDate;
            ViewBag.WarrantyEDate = serviceArrangeModi_H.WarrantyEDate;
            ViewBag.Confirmed = serviceArrangeModi_H.Confirmed;
            ViewBag.Confirmor = serviceArrangeModi_H.Confirmor;
            ViewBag.NewRemark = serviceArrangeModi_H.NewRemark;
            ViewBag.NewInternalRemark = serviceArrangeModi_H.NewInternalRemark;
            ViewBag.ForceToClose = serviceArrangeModi_H.ForceToClose;
            ViewBag.ModiReason = serviceArrangeModi_H.ModiReason;

            ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
            ViewData["ServiceArrangeModi_D"] = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ServiceArrangeModi_HLogic logic = new ServiceArrangeModi_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["ServiceArrangeModi_H"] = logic.SearchServiceArrangeModi_H(col, condition, value);

            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["ServiceArrangeModi_H"] = logic.SearchServiceArrangeModi_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                #region
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單號,版次,客戶名稱,客戶形態,品號,品名,序號,保固起日,保固迄日");
                //    List<ServiceArrangeModi_H> objServiceArrangeModi_H = ViewData["ServiceArrangeModi_H"] as List<ServiceArrangeModi_H>;
                //    foreach (var obj in objServiceArrangeModi_H)
                //    {
                //        sw.WriteLine(obj.SerArrangeOrderType + "," + obj.SerArrangeOrderNo + ","
                //            + obj.Version + "," + obj.CustomerName + "," + obj.CustomerType + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.WarrantySDate + "," + obj.WarrantyEDate);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ServiceArrangeModi.csv");
                #endregion

                MemoryStream ms = null;
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //XSSFWorkbook workbook = new XSSFWorkbook();
                    HSSFWorkbook book = new HSSFWorkbook();
                    ISheet sheet = book.CreateSheet();
                    HSSFCellStyle lo_Style = (HSSFCellStyle)book.CreateCellStyle();
                    lo_Style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                    ms = new MemoryStream();
                    IRow row = null;
                    row = sheet.CreateRow(0);
                    //單別,單號,版次,客戶名稱,客戶形態,品號,品名,序號,保固起日,保固迄日
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("版次");
                    row.CreateCell(3).SetCellValue("客戶名稱");
                    row.CreateCell(4).SetCellValue("客戶形態");
                    row.CreateCell(5).SetCellValue("品號");
                    row.CreateCell(6).SetCellValue("品名");
                    row.CreateCell(7).SetCellValue("序號");
                    row.CreateCell(8).SetCellValue("保固起日");
                    row.CreateCell(9).SetCellValue("保固迄日");
                    List<ServiceArrangeModi_H> objServiceArrangeModi_H = ViewData["ServiceArrangeModi_H"] as List<ServiceArrangeModi_H>;
                    int i = 0;
                    foreach (var obj in objServiceArrangeModi_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SerArrangeOrderType);
                        row.CreateCell(1).SetCellValue(obj.SerArrangeOrderNo);
                        row.CreateCell(2).SetCellValue(obj.Version);
                        row.CreateCell(3).SetCellValue(obj.CustomerName);
                        row.CreateCell(4).SetCellValue(obj.CustomerType);
                        row.CreateCell(5).SetCellValue(obj.ProductNo);
                        row.CreateCell(6).SetCellValue(obj.ProductName);
                        row.CreateCell(7).SetCellValue(obj.SerialNo);
                        row.CreateCell(8).SetCellValue(obj.WarrantySDate);
                        row.CreateCell(9).SetCellValue(obj.WarrantyEDate);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "ServiceArrangeModi.xls");
            }
            else
            {
                if (Request.Form["strSerArrangeOrderType"].Trim() != "")
                {
                    col += ",s.SerArrangeOrderNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strSerArrangeOrderType"];
                }
                if (Request.Form["strCustomerId"].Trim() != "")
                {
                    col += ",s.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["strCustomerId"];
                }
                ViewData["ServiceArrangeModi_H"] = logic.SearchServiceArrangeModi_H(col, condition, value);
                ViewBag.strSerArrangeOrderType = Request.Form["strSerArrangeOrderType"];
                ViewBag.strCustomerId = Request.Form["strCustomerId"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<ServiceArrangeModi_D> objServiceArrangeModi_D = new List<ServiceArrangeModi_D>();
            ViewData["ServiceArrangeModi_D"] = objServiceArrangeModi_D;
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
        public JsonResult SerialNo(string SerialNo)
        {
            Warranty_HLogic logic = new Warranty_HLogic();
            return Json(logic.IsSerialNo(SerialNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "D1"), JsonRequestBehavior.AllowGet);
        }

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
    }
}