using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace SUPB01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();
            string col = ",h.OrderStatus";
            string condition = ",=";
            string value = ",0";
            ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();
            string col = ",h.OrderStatus";
            string condition = ",=";
            string value = "," + Request.Form["OrderStatus"];

            supportapl_h.OrderStatus = Request.Form["OrderStatus"];
            string msg = "";
            if (Request.Form["action"] == "btnSearch")
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col += ",h.SupportAplOrderNo";
                    condition += "," + Request.Form["selCondsupportaplorderno"];
                    value += "," + Request.Form["supportaplorderno"];
                }

                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCondcustomerid"];
                    value += "," + Request.Form["customerid"];
                }                
            }
            if (Request.Form["action"] == "btnAllot")
            {

                string SupportId = Request.Form["chk"];
                SupportAplAsign supportAplAsign=new SupportAplAsign();
                supportAplAsign.AsignDate = Request.Form["requesDate"].Replace("/", "");
                supportAplAsign.ProcessMan = Request.Form["EmployeeId"];
                supportAplAsign.AsignMan = "";
                supportAplAsign.AsignOrderType = Request.Form["ordertype"];
                if (!logic.UpdateAsign(supportAplAsign, SupportId,out msg))
                {
                    ViewBag.Msg = "分配失敗！"+ msg;
                }
            }
            if (Request.Form["action"] == "btnAllotAgain")
            {

                string SupportId = Request.Form["chk"];
                AssignmentChange assignmentChange = new AssignmentChange();
                assignmentChange.Designee = Request.Form["EmployeeIdAgain"];
                assignmentChange.Assignor = "";
                assignmentChange.ModiReason = Request.Form["remarkAgain"];
                if (!logic.UpdateAsignAgain(assignmentChange, SupportId,out msg))
                {
                    ViewBag.Msg = "分配失敗！"+msg;
                }
            }
            if (Request.Form["action"] == "btnExport")
            {

                ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);

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
                //    sw.WriteLine("單別,單號,客戶名稱,支援單位,支援項目,需求日期,狀態,處理人員,業務人員,建立人員,發料,來源單號,客戶狀態");
                //    List<SupportApl_H> objSupportApl_H = ViewData["SupportApl_H"] as List<SupportApl_H>;
                //    string OrderStatus = "";
                //    string CustomerType = "";
                //    foreach (var obj in objSupportApl_H)
                //    {
                //        if(obj.OrderStatus=="0")
                //        {
                //            OrderStatus = "未處理";
                //        }else if (obj.OrderStatus == "1")
                //        {
                //            OrderStatus = "已派工";
                //        }else if (obj.OrderStatus == "2")
                //        {
                //            OrderStatus = "完工";
                //        }else
                //        {
                //            OrderStatus = "作廢";
                //        }

                //        if (obj.CustomerType == "A")
                //        {
                //            CustomerType = "一般客戶";
                //        }
                //        else
                //        {
                //            CustomerType = "經銷商";
                //        }
                //        sw.WriteLine(obj.SupportAplOrderType + "," + obj.SupportAplOrderNo + ","
                //            + obj.CustomerName + "," + obj.SupportDept + "," + obj.SupportItemAll + ","
                //            + obj.RequestDate + "," + OrderStatus + "," + obj.ProcessMan+","
                //            +obj.SalesName+","+obj.Creator+","+obj.IsPicking+","+obj.SourceOrderNo+","
                //            + CustomerType);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "SupportApl.csv");
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
                    //單別,單號,客戶名稱,支援單位,支援項目,需求日期,狀態,處理人員,業務人員,建立人員,發料,來源單號,客戶狀態
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("客戶簡稱");
                    row.CreateCell(3).SetCellValue("支援單位");
                    row.CreateCell(4).SetCellValue("支援項目");
                    row.CreateCell(5).SetCellValue("需求日期");
                    row.CreateCell(6).SetCellValue("狀態");
                    row.CreateCell(7).SetCellValue("處理人員");
                    row.CreateCell(8).SetCellValue("業務員");
                    row.CreateCell(9).SetCellValue("建立人員");
                    row.CreateCell(10).SetCellValue("發料");
                    row.CreateCell(11).SetCellValue("來源單號");
                    row.CreateCell(12).SetCellValue("客戶形態");
                    List<SupportApl_H> objSupportApl_H = ViewData["SupportApl_H"] as List<SupportApl_H>;
                    //string OrderStatus = "";
                    //string CustomerType = "";
                    int i = 0;
                    foreach (var obj in objSupportApl_H)
                    {
                        //if (obj.OrderStatus == "0")
                        //{
                        //    OrderStatus = "未處理";
                        //}
                        //else if (obj.OrderStatus == "1")
                        //{
                        //    OrderStatus = "已派工";
                        //}
                        //else if (obj.OrderStatus == "2")
                        //{
                        //    OrderStatus = "完工";
                        //}
                        //else
                        //{
                        //    OrderStatus = "作廢";
                        //}

                        //if (obj.CustomerType == "A")
                        //{
                        //    CustomerType = "一般客戶";
                        //}
                        //else
                        //{
                        //    CustomerType = "經銷商";
                        //}
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SupportAplOrderType);
                        row.CreateCell(1).SetCellValue(obj.SupportAplOrderNo);
                        row.CreateCell(2).SetCellValue(obj.CustomerName);
                        row.CreateCell(3).SetCellValue(obj.SupportDept);
                        row.CreateCell(4).SetCellValue(obj.SupportItemAll);
                        row.CreateCell(5).SetCellValue(obj.RequestDate);
                        row.CreateCell(6).SetCellValue(obj.OrderStatus);
                        row.CreateCell(7).SetCellValue(obj.ProcessManName);
                        row.CreateCell(8).SetCellValue(obj.AsignManName);
                        row.CreateCell(9).SetCellValue(obj.CreatorName);
                        row.CreateCell(10).SetCellValue(obj.IsPicking);
                        row.CreateCell(11).SetCellValue(obj.SourceOrderNo);
                        row.CreateCell(12).SetCellValue(obj.CustomerType);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    //MessageBox.Show("匯出成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "SupportApl.xls");
            }
            ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);
            ViewBag.OrderStatus = Request.Form["OrderStatus"];
            ViewBag.selCondsupportaplorderno = Request.Form["selCondsupportaplorderno"];
            ViewBag.supportaplorderno = Request.Form["supportaplorderno"];
            ViewBag.selCondcustomerid = Request.Form["selCondcustomerid"];
            ViewBag.customerid = Request.Form["customerid"];
            return View();
        }

        [HttpPost]
        public JsonResult GetEmployeeName(string EmployeeId)
        {
            EmployeeLogic logic = new EmployeeLogic();
            return Json(logic.GetEmployeeName(EmployeeId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "A2"), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchOrderType(string OrderType, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "A2";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchPositionEmployee(string Col, string Condition, string conditionValue, int Page)
        {
            Position_DLogic logic = new Position_DLogic();
            List<Position_D> lst = logic.Position_DList(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}