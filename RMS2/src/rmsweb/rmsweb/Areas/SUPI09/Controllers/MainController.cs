using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace SUPI09.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Picking_HLogic logic = new Picking_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Picking_H"] = logic.GetPicking_H(col, condition, value);
            ViewBag.IsPicking = "";

            return View();
        }

        public ActionResult CUR()
        {
            List<Picking_ProductD> objPicking_ProductD = new List<Picking_ProductD>();
            ViewData["Picking_ProductD"] = objPicking_ProductD;
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.NoOfPrints = "0";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Picking_HLogic logic = new Picking_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();

            string col = "";
            string condition = "";
            string value = "";
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["Picking_H"] = logic.GetPicking_H("", "", "");

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
                //    sw.WriteLine("單別,單號,客戶名稱,支援單位,需求日期,業務人員,建立人員,客戶形態,來源單別,來源單號");
                //    List<Picking_H> objPicking_H = ViewData["Picking_H"] as List<Picking_H>;
                //    string customerType = "";
                //    foreach (var obj in objPicking_H)
                //    {
                //        if (customerType == "A")
                //        {
                //            customerType = "一般客戶";
                //        }else
                //        {
                //            customerType = "經銷商";
                //        }
                //        sw.WriteLine(obj.PickingOrderType + "," + obj.PickingOrderNo +
                //            "," + obj.CustomerName + "," + obj.SupportDeptName + "," +
                //            obj.RequestDate + "," + obj.SalesName + "," +
                //            obj.CreatorName + "," + customerType + "," +
                //            obj.SupportAplOrderType + "," + obj.SupportAplOrderNo);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                //ViewBag.selCond1 = Request.Form["selCond1"];
                //ViewBag.selCond2 = Request.Form["selCond2"];
                //ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                //ViewBag.CustomerId = Request.Form["customerid"];

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Picking_H.csv");
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
                    //單別,單號,客戶名稱,支援單位,需求日期,業務人員,建立人員,客戶形態,來源單別,來源單號
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("客戶名稱");
                    row.CreateCell(3).SetCellValue("支援單位");
                    row.CreateCell(4).SetCellValue("需求日期");
                    row.CreateCell(5).SetCellValue("業務人員");
                    row.CreateCell(6).SetCellValue("建立人員");
                    row.CreateCell(7).SetCellValue("客戶形態");
                    row.CreateCell(8).SetCellValue("來源單別");
                    row.CreateCell(9).SetCellValue("來源單號");
                    //row.CreateCell(10).SetCellValue("備註");
                    List<Picking_H> objPicking_H = ViewData["Picking_H"] as List<Picking_H>;
                    //string customerType = "";
                    int i = 0;
                    foreach (var obj in objPicking_H)
                    {
                        //if (customerType == "A")
                        //{
                        //    customerType = "一般客戶";
                        //}
                        //else
                        //{
                        //    customerType = "經銷商";
                        //}
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.PickingOrderType);
                        row.CreateCell(1).SetCellValue(obj.PickingOrderNo);
                        row.CreateCell(2).SetCellValue(obj.CustomerName);
                        row.CreateCell(3).SetCellValue(obj.SupportDeptName);
                        row.CreateCell(4).SetCellValue(obj.RequestDate);
                        row.CreateCell(5).SetCellValue(obj.SalesName);
                        row.CreateCell(6).SetCellValue(obj.CreatorName);
                        row.CreateCell(7).SetCellValue(obj.CustomerType);
                        row.CreateCell(8).SetCellValue(obj.SupportAplOrderType);
                        row.CreateCell(9).SetCellValue(obj.SupportAplOrderNo);
                        //row.CreateCell(10).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    //MessageBox.Show("匯出成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    ms.Close();
                    ms.Dispose();
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                ViewBag.CustomerId = Request.Form["customerid"];

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Picking_H.xls");
            }
            else
            {
                if (Request.Form["IsPicking"].Trim() != "")
                {
                    col += ",Confirmed";
                    condition += ",=";
                    value += "," + Request.Form["IsPicking"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col = ",c.CustomerId";
                    condition = "," + Request.Form["selCond1"];
                    value = "," + Request.Form["customerid"];
                }
                if (Request.Form["id_supportaplorderno"].Trim() != "")
                {
                    col = ",h.SupportAplOrderNo";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["id_supportaplorderno"];
                }

                ViewBag.IsPicking = Request.Form["IsPicking"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SupportAplOrderNo = Request.Form["id_supportaplorderno"];
                ViewBag.CustomerId = Request.Form["customerid"];
                ViewBag.IsPicking = Request.Form["IsPicking"];

            }

            ViewData["Picking_H"] = logic.GetPicking_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            Picking_H picking_H = new Picking_H();
            picking_H.PickingOrderType = Request.Form["AsignOrderType"];            //發料單別
            picking_H.PickingOrderNo = Request.Form["AsignOrderNo"];                //發料單號
            picking_H.OrderDate = Request.Form["orderdate"].Replace("/","");        //單據日期
            picking_H.SupportAplOrderType = Request.Form["SupportAplOrderType"];    //支援申請單別
            picking_H.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];        //支援申請單號
            picking_H.CustomerId = Request.Form["customerId"];                      //客戶代號
            picking_H.CustomerType = Request.Form["customertype"]==""?"":Request.Form["customertype"].Substring(0,1);                  //客戶型態
            picking_H.Sales = Request.Form["salesname"];                            //業務員
            picking_H.RequestDate = Request.Form["requesDate"];                     //需求日期
            picking_H.RequestTime = Request.Form["id_requesttime"];                 //指定時間
            picking_H.SupportDept = Request.Form["departmentId"];                   //支援單位
            picking_H.PickingDate = Request.Form["pickingdate"];                    //發料日期
            picking_H.PickingMan = Request.Form["pickingman"];                      //發料人員
            picking_H.Remark = Request.Form["remark"];                              //備註

            //品項明細
            string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();                  //品號
            string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();            //品名
            string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();                                    //發料數量
            string strpickingQTY = Request.Form["strpickingQTY"] == null ? "" : Request.Form["strpickingQTY"].ToString();                     //已發數量
            string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();                       //單位
            string strremark = Request.Form["strremark"] == null ? "" : Request.Form["strremark"].ToString();                           //備註
            
            Picking_HLogic logic = new Picking_HLogic();
            string msg="";
            if (Request.Form["action"] == "ConfirmedY")
            {
                picking_H.Confirmed = "Y";
                picking_H.PickingMan = "";
                if (!logic.Confirmed(picking_H, out msg))
                {
                    picking_H = logic.GetPicking_HByNo(picking_H.PickingOrderType, picking_H.PickingOrderNo);
                    ViewBag.PickingOrderType = picking_H.PickingOrderType;
                    ViewBag.PickingOrderNo = picking_H.PickingOrderNo;
                    ViewBag.OrderDate = picking_H.OrderDate;
                    ViewBag.SupportAplOrderType = picking_H.SupportAplOrderType;
                    ViewBag.SupportAplOrderNo = picking_H.SupportAplOrderNo;
                    ViewBag.CustomerId = picking_H.CustomerId;
                    ViewBag.CustomerType = picking_H.CustomerType;
                    ViewBag.Sales = picking_H.Sales;
                    ViewBag.SalesName = picking_H.SalesName;
                    ViewBag.RequestDate = picking_H.RequestDate;
                    ViewBag.RequestTime = picking_H.RequestTime;
                    ViewBag.SupportDept = picking_H.SupportDept;
                    ViewBag.PickingDate = picking_H.PickingDate;
                    ViewBag.PickingMan = picking_H.PickingMan;
                    ViewBag.Remark = picking_H.Remark;
                    ViewBag.Confirmed = picking_H.Confirmed;
                    ViewBag.CustomerName = picking_H.CustomerName;

                    Picking_ProductDLogic pickingLogic = new Picking_ProductDLogic();
                    ViewData["Picking_ProductD"] = pickingLogic.GetPicking_ProductD(picking_H.PickingOrderType, picking_H.PickingOrderNo);
                    ViewBag.Type = "Details";
                    ViewBag.Msg = "發料確認失敗！" + msg;
                    return View("CUR");
                }
            }
            if (Request.Form["action"] == "ConfirmedN")
            {
                picking_H.Confirmed = "N";
                if (!logic.Confirmed(picking_H, out msg))
                {
                    picking_H = logic.GetPicking_HByNo(picking_H.PickingOrderType, picking_H.PickingOrderNo);
                    ViewBag.PickingOrderType = picking_H.PickingOrderType;
                    ViewBag.PickingOrderNo = picking_H.PickingOrderNo;
                    ViewBag.OrderDate = picking_H.OrderDate;
                    ViewBag.SupportAplOrderType = picking_H.SupportAplOrderType;
                    ViewBag.SupportAplOrderNo = picking_H.SupportAplOrderNo;
                    ViewBag.CustomerId = picking_H.CustomerId;
                    ViewBag.CustomerType = picking_H.CustomerType;
                    ViewBag.Sales = picking_H.Sales;
                    ViewBag.SalesName = picking_H.SalesName;
                    ViewBag.RequestDate = picking_H.RequestDate;
                    ViewBag.RequestTime = picking_H.RequestTime;
                    ViewBag.SupportDept = picking_H.SupportDept;
                    ViewBag.PickingDate = picking_H.PickingDate;
                    ViewBag.PickingMan = picking_H.PickingMan;
                    ViewBag.Remark = picking_H.Remark;
                    ViewBag.Confirmed = picking_H.Confirmed;
                    ViewBag.CustomerName = picking_H.CustomerName;

                    Picking_ProductDLogic pickingLogic = new Picking_ProductDLogic();
                    ViewData["Picking_ProductD"] = pickingLogic.GetPicking_ProductD(picking_H.PickingOrderType, picking_H.PickingOrderNo);
                    ViewBag.Type = "Details";
                    ViewBag.Msg = "取消確認失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
            {
                if (!logic.AddPicking_H(picking_H, strproductno, strproductname, strqty, strpickingQTY, strunit, strremark, out msg))
                {
                    List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();
                    ViewData["SupportApl_ProductD"] = objSupportApl_ProductD;
                    ViewBag.Msg = "新增失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "Save")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }
            }
            else if (Request.Form["action"] == "EditDetails" || Request.Form["action"] == "CopyAddDetails")
            {
                picking_H = logic.GetPicking_HByNo(Request.Form["AsignOrderType"], Request.Form["AsignOrderNo"]);

                ViewBag.OrderDate = picking_H.OrderDate;
                ViewBag.SupportAplOrderType = picking_H.SupportAplOrderType;
                ViewBag.SupportAplOrderNo = picking_H.SupportAplOrderNo;
                ViewBag.CustomerId = picking_H.CustomerId;
                ViewBag.CustomerType = picking_H.CustomerType;
                ViewBag.Sales = picking_H.Sales;
                ViewBag.SalesName = picking_H.SalesName;
                ViewBag.RequestDate = picking_H.RequestDate;
                ViewBag.RequestTime = picking_H.RequestTime;
                ViewBag.SupportDept = picking_H.SupportDept;
                ViewBag.PickingDate = picking_H.PickingDate;
                ViewBag.PickingMan = picking_H.PickingMan;
                ViewBag.Remark = picking_H.Remark;
                ViewBag.Confirmed = picking_H.Confirmed;
                ViewBag.CustomerName = picking_H.CustomerName;

                Picking_ProductDLogic pickingLogic = new Picking_ProductDLogic();
                ViewData["Picking_ProductD"] = pickingLogic.GetPicking_ProductD(Request.Form["AsignOrderType"], Request.Form["AsignOrderNo"]);
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                    ViewBag.PickingOrderType = picking_H.PickingOrderType;
                    ViewBag.PickingOrderNo = picking_H.PickingOrderNo;
                }
                
                return View("CUR");
            }

            return RedirectToAction("Index");
        }

        //public JsonResult SearchOrderType(string OrderType, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "A3";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "A3"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeNo(string OrderType)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            return Json(logic.GetSupportAplOrderNo(OrderType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchSupportApl_H(string Col, string Condition, string conditionValue, string CustomerId, int Page)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            List<SupportApl_H> lst = logic.GetSupportInfo( Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_ProductDLogic logic = new SupportApl_ProductDLogic();
            List<SupportApl_ProductD> lst = logic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string PickingOrderType, string PickingOrderNo)
        {
            Picking_HLogic logic = new Picking_HLogic();

            if (!logic.DeletePicking_H(PickingOrderType, PickingOrderNo))
            {
                ViewBag.Msg = "刪除失敗！";
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string PickingOrderType, string PickingOrderNo)
        {
            Picking_HLogic logic = new Picking_HLogic();
            Picking_H picking_H = logic.GetPicking_HByNo(PickingOrderType, PickingOrderNo);
            ViewBag.PickingOrderType = picking_H.PickingOrderType;
            ViewBag.PickingOrderNo = picking_H.PickingOrderNo;
            ViewBag.OrderDate = picking_H.OrderDate;
            ViewBag.SupportAplOrderType = picking_H.SupportAplOrderType;
            ViewBag.SupportAplOrderNo = picking_H.SupportAplOrderNo;
            ViewBag.CustomerId = picking_H.CustomerId;
            ViewBag.CustomerType = picking_H.CustomerType;
            ViewBag.Sales = picking_H.Sales;
            ViewBag.SalesName = picking_H.SalesName;
            ViewBag.RequestDate = picking_H.RequestDate;
            ViewBag.RequestTime = picking_H.RequestTime;
            ViewBag.SupportDept = picking_H.SupportDept;
            ViewBag.PickingDate = picking_H.PickingDate;
            ViewBag.PickingMan = picking_H.PickingMan;
            ViewBag.Remark = picking_H.Remark;
            ViewBag.Confirmed = picking_H.Confirmed;
            ViewBag.CustomerName = picking_H.CustomerName;

            Picking_ProductDLogic pickingLogic = new Picking_ProductDLogic();
            ViewData["Picking_ProductD"] = pickingLogic.GetPicking_ProductD(PickingOrderType, PickingOrderNo);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string PickingOrderType, string PickingOrderNo)
        {
            Picking_HLogic logic = new Picking_HLogic();
            Picking_H picking_H = logic.GetPicking_HByNo(PickingOrderType, PickingOrderNo);
            ViewBag.PickingOrderType = picking_H.PickingOrderType;
            ViewBag.PickingOrderNo = picking_H.PickingOrderNo;
            ViewBag.OrderDate = picking_H.OrderDate;
            ViewBag.SupportAplOrderType = picking_H.SupportAplOrderType;
            ViewBag.SupportAplOrderNo = picking_H.SupportAplOrderNo;
            ViewBag.CustomerId = picking_H.CustomerId;
            ViewBag.CustomerType = picking_H.CustomerType;
            ViewBag.Sales = picking_H.Sales;
            ViewBag.SalesName = picking_H.SalesName;
            ViewBag.RequestDate = picking_H.RequestDate;
            ViewBag.RequestTime = picking_H.RequestTime;
            ViewBag.SupportDept = picking_H.SupportDept;
            ViewBag.PickingDate = picking_H.PickingDate;
            ViewBag.PickingMan = picking_H.PickingMan;
            ViewBag.Remark = picking_H.Remark;
            ViewBag.Confirmed = picking_H.Confirmed;
            ViewBag.CustomerName = picking_H.CustomerName;

            Picking_ProductDLogic pickingLogic = new Picking_ProductDLogic();
            ViewData["Picking_ProductD"] = pickingLogic.GetPicking_ProductD(PickingOrderType, PickingOrderNo);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public JsonResult SearchPicking_ProductSerial(string PickingOrderType, string PickingOrderNo,string ItemId)
        {
            Picking_ProductSerialLogic logic=new Picking_ProductSerialLogic();

            List<Picking_ProductSerial> lst = logic.SeachPicking_ProductSerial(PickingOrderType, PickingOrderNo, ItemId);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSerial(string PickingOrderType, string PickingOrderNo, string ItemId, string[] SerialNoValue)
        {
            Picking_ProductSerial picking_ProductSerial = new Picking_ProductSerial();
            picking_ProductSerial.PickingOrderType = PickingOrderType;
            picking_ProductSerial.PickingOrderNo = PickingOrderNo;
            picking_ProductSerial.ItemId = ItemId;

            Picking_ProductSerialLogic logic = new Picking_ProductSerialLogic();
            string msg = "";

            if(logic.AddPicking_ProductSerial(picking_ProductSerial, SerialNoValue,out msg))
            {
                msg = "OK-保存成功";
            }else
            {
                msg = "NO-保存失敗!"+msg;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}