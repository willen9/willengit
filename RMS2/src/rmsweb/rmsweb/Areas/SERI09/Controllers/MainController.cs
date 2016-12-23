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

namespace SERI09.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",B3";
            ViewData["AssignmentChange"] = logic.SearchAssignmentChangeRGA_H(col, condition, value);
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",D3";
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["AssignmentChange"] = logic.SearchAssignmentChange(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["AssignmentChange"] = logic.SearchAssignmentChange(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
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
                    //單別,單號,版次,單據日期,客戶名稱,狀態,支援人員,變更日期,變更人員,變更原因,備註
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("版次");
                    row.CreateCell(3).SetCellValue("單據日期");
                    row.CreateCell(4).SetCellValue("客戶名稱");
                    row.CreateCell(5).SetCellValue("狀態");
                    row.CreateCell(6).SetCellValue("支援人員");
                    row.CreateCell(7).SetCellValue("變更日期");
                    row.CreateCell(8).SetCellValue("變更人員");
                    row.CreateCell(9).SetCellValue("變更原因");
                    row.CreateCell(10).SetCellValue("備註");
                    List<AssignmentChange> objAssignmentChange = ViewData["AssignmentChange"] as List<AssignmentChange>;
                    int i = 0;
                    foreach (var obj in objAssignmentChange)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.AssignOrderType);
                        row.CreateCell(1).SetCellValue(obj.AssignOrderNo);
                        row.CreateCell(2).SetCellValue(obj.Version);
                        row.CreateCell(3).SetCellValue(obj.OrderDate);
                        row.CreateCell(4).SetCellValue(obj.CustomerName);
                        row.CreateCell(5).SetCellValue("");
                        row.CreateCell(6).SetCellValue(obj.DesigneeName);
                        row.CreateCell(7).SetCellValue(obj.ConfirmedDate);
                        row.CreateCell(8).SetCellValue(obj.ConfirmorName);
                        row.CreateCell(9).SetCellValue(obj.ModiReason);
                        row.CreateCell(10).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    //MessageBox.Show("匯出成功！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "SupportAplAsignModi.xls");
            }
            else
            {
                if (Request.Form["AsignOrderNo"].Trim() != "")
                {
                    col = ",a.AssignOrderType";
                    condition = "," + Request.Form["selCond1"];
                    value = "," + Request.Form["AsignOrderNo"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col = ",a.CustomerId";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["customerid"];
                }

            }
            ViewBag.selCond1 = Request.Form["selCond1"];
            ViewBag.selCond2 = Request.Form["selCond2"];
            ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
            ViewBag.CustomerId = Request.Form["customerid"];
            ViewData["AssignmentChange"] = logic.SearchAssignmentChange(col, condition, value);

            return View();
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            try
            {
                AssignmentChangeLogic logic = new AssignmentChangeLogic();
                AssignmentChange assignmentChange = new AssignmentChange();
                assignmentChange.AssignOrderType = Request.Form["AsignOrderType"];
                assignmentChange.AssignOrderNo = Request.Form["AsignOrderNo"];
                assignmentChange.OrderDate = Request.Form["orderdate"].Replace("/", "");
                assignmentChange.CustomerId = Request.Form["customerId"];
                assignmentChange.Version = Request.Form["version"];
                //supportAplAsignModi.SupportAplOrderType = Request.Form["SupportAplOrderType"];
                //supportAplAsignModi.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
                assignmentChange.Designee = Request.Form["processman"];
                assignmentChange.Assignor = Request.Form["updateman"];
                assignmentChange.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
                assignmentChange.Remark = Request.Form["remark"];
                assignmentChange.ModiReason = Request.Form["modireason"];
                string msg = "";
                if (Request.Form["action"] == "ConfirmedY")
                {
                    assignmentChange.Confirmed = "Y";
                    if (!logic.Confirmed(assignmentChange, "B3", out msg))
                    {
                        ViewBag.AssignOrderType = Request.Form["AsignOrderType"];
                        ViewBag.AssignOrderNo = Request.Form["AsignOrderNo"];
                        ViewBag.OrderDate = Request.Form["orderdate"];
                        ViewBag.CustomerId = Request.Form["customerId"];
                        ViewBag.CustomerName = Request.Form["idcustomerName"];
                        ViewBag.Version = Request.Form["id_version"];
                        ViewBag.SourceOrderType = Request.Form["SupportAplOrderType"];
                        ViewBag.SourceOrderNo = Request.Form["SupportAplOrderNo"];
                        ViewBag.Designee = Request.Form["ProcessMan"];
                        ViewBag.DesigneeName = Request.Form["idProcessManName"];
                        ViewBag.Assignor = Request.Form["asignman"];
                        ViewBag.AssignorName = Request.Form["idAsignManName"];
                        ViewBag.NoOfPrints = Request.Form["noofprint"];
                        ViewBag.Remark = Request.Form["remark"];

                        ViewBag.Msg = "變更確認失敗！" + msg;
                        ViewBag.Type = "Details";
                        return View("CUR");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                if (Request.Form["action"] == "ConfirmedN")
                {
                    assignmentChange.Confirmed = "N";
                    if (!logic.Confirmed(assignmentChange, "B3", out msg))
                    {
                        ViewBag.AssignOrderType = Request.Form["AsignOrderType"];
                        ViewBag.AssignOrderNo = Request.Form["AsignOrderNo"];
                        ViewBag.OrderDate = Request.Form["orderdate"];
                        ViewBag.CustomerId = Request.Form["customerId"];
                        ViewBag.CustomerName = Request.Form["idcustomerName"];
                        ViewBag.Version = Request.Form["id_version"];
                        ViewBag.SourceOrderType = Request.Form["SupportAplOrderType"];
                        ViewBag.SourceOrderNo = Request.Form["SupportAplOrderNo"];
                        ViewBag.Designee = Request.Form["ProcessMan"];
                        ViewBag.DesigneeName = Request.Form["idProcessManName"];
                        ViewBag.Assignor = Request.Form["asignman"];
                        ViewBag.AssignorName = Request.Form["idAsignManName"];
                        ViewBag.NoOfPrints = Request.Form["noofprint"];
                        ViewBag.Remark = Request.Form["remark"];

                        ViewBag.Msg = "作廢失敗！" + msg;
                        ViewBag.Type = "Details";
                        return View("CUR");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
                {
                    if (!logic.UpdateAssignmentChange(assignmentChange,"B3", out msg))
                    {
                        ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
                        ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
                        ViewBag.OrderDate = Request.Form["orderdate"];
                        ViewBag.CustomerId = Request.Form["customerId"];
                        ViewBag.Version = Request.Form["version"];
                        ViewBag.ProcessMan = Request.Form["ProcessMan"];
                        ViewBag.AsignMan = Request.Form["updateman"];
                        ViewBag.NoOfPrints = Request.Form["noofprint"];
                        ViewBag.ModiReason = Request.Form["modireason"];
                        ViewBag.Remark = Request.Form["remark"];
                        ViewBag.Type = "Edit";
                        ViewBag.Msg = "修改失敗！" + msg;
                        return View("CUR");
                    }
                    else
                    {
                        if (Request.Form["action"] == "Edit")
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("CUR");
                        }
                    }
                }
                else if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
                {
                    if (!logic.AddAssignmentChange(assignmentChange, out msg))
                    {
                        ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
                        ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
                        ViewBag.OrderDate = Request.Form["orderdate"];
                        ViewBag.CustomerId = Request.Form["customerId"];
                        ViewBag.Version = Request.Form["version"];
                        ViewBag.ProcessMan = Request.Form["ProcessMan"];
                        ViewBag.AsignMan = Request.Form["updateman"];
                        ViewBag.NoOfPrints = Request.Form["noofprint"];
                        ViewBag.ModiReason = Request.Form["modireason"];
                        ViewBag.Remark = Request.Form["remark"];
                        ViewBag.Msg = "修改失敗！" + msg;
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
                    ViewBag.OrderDate = Request.Form["orderdate"];
                    ViewBag.CustomerId = Request.Form["customerId"];
                    ViewBag.Version = Request.Form["version"];
                    ViewBag.ProcessMan = Request.Form["ProcessMan"];
                    ViewBag.AsignMan = Request.Form["updateman"];
                    ViewBag.NoOfPrints = Request.Form["noofprint"];
                    ViewBag.ModiReason = Request.Form["modireason"];
                    ViewBag.Remark = Request.Form["remark"];
                    if (Request.Form["action"] == "EditDetails")
                    {
                        ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
                        ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
                        ViewBag.Type = "Edit";
                    }
                    return View("CUR");
                }
                //else if (Request.Form["action"] == "VoidModi")
                //{
                //    logic.voidModi(Request.Form["AsignOrderType"], Request.Form["AsignOrderNo"]);
                //    return RedirectToAction("Index");
                //}
                else
                {
                    return View("CUR");
                }
            }
            catch (Exception ex)
            {
                ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
                ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
                ViewBag.OrderDate = Request.Form["orderdate"];
                ViewBag.CustomerId = Request.Form["customerId"];
                //ViewBag.CustomerName = Request.Form["idcustomerName"];
                ViewBag.Version = Request.Form["version"];
                //ViewBag.SupportAplOrderType = Request.Form["SupportAplOrderType"];
                //ViewBag.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
                ViewBag.ProcessMan = Request.Form["ProcessMan"];
                ViewBag.AsignMan = Request.Form["updateman"];
                ViewBag.NoOfPrints = Request.Form["noofprint"];
                ViewBag.ModiReason = Request.Form["modireason"];
                ViewBag.Remark = Request.Form["remark"];
                ViewBag.Msg = ex.Message;
                return View("CUR");
            }

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

        public JsonResult SearchSupportAplAsign(string Col, string Condition, string conditionValue, int Page)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            List<Assignment> lst = logic.GetAssignmentRGA_H(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchSupportApl_H(string Col, string Condition, string conditionValue, int Page)
        //{
        //    SupportApl_HLogic logic = new SupportApl_HLogic();
        //    SupportApl_H supportApl_H = new SupportApl_H();
        //    supportApl_H.SupportAplOrderType = SupportAplOrderType;
        //    //supportApl_H.SupportAplOrderNo = SupportAplOrderNo;
        //    //supportApl_H.CustomerId = CustomerId;
        //    supportApl_H.Picking = "Y";
        //    supportApl_H.IsPicking = "N";
        //    List<SupportApl_H> lst = logic.GetSupportInfo(supportApl_H, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_ProductDLogic logic = new SupportApl_ProductDLogic();
            List<SupportApl_ProductD> lst = logic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Details(string AsignOrderType, string AsignOrderNo, string Version)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            AssignmentChange assignmentChange = new AssignmentChange();
            assignmentChange.AssignOrderType = AsignOrderType;
            assignmentChange.AssignOrderNo = AsignOrderNo;
            assignmentChange.Version = Version;
            assignmentChange = logic.GetAssignmentChangeInfo(assignmentChange);
            ViewBag.AsignOrderType = assignmentChange.AssignOrderType;
            ViewBag.AsignOrderNo = assignmentChange.AssignOrderNo;
            ViewBag.OrderDate = assignmentChange.OrderDate;
            ViewBag.CustomerId = assignmentChange.CustomerId;
            ViewBag.CustomerName = assignmentChange.CustomerName;
            ViewBag.Version = assignmentChange.Version;
            ViewBag.ConfirmedDate = assignmentChange.ConfirmedDate;
            ViewBag.ProcessMan = assignmentChange.Designee;
            ViewBag.DesigneeName = assignmentChange.DesigneeName;
            ViewBag.NoOfPrints = assignmentChange.NoOfPrints;
            ViewBag.Confirmor = assignmentChange.Confirmor;
            ViewBag.ConfirmorName = assignmentChange.ConfirmorName;
            ViewBag.ModiReason = assignmentChange.ModiReason;
            ViewBag.Remark = assignmentChange.Remark;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string AsignOrderType, string AsignOrderNo, string Version)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            AssignmentChange assignmentChange = new AssignmentChange();
            assignmentChange.AssignOrderType = AsignOrderType;
            assignmentChange.AssignOrderNo = AsignOrderNo;
            assignmentChange.Version = Version;
            assignmentChange = logic.GetAssignmentChangeInfo(assignmentChange);
            ViewBag.AsignOrderType = assignmentChange.AssignOrderType;
            ViewBag.AsignOrderNo = assignmentChange.AssignOrderNo;
            ViewBag.OrderDate = assignmentChange.OrderDate;
            ViewBag.CustomerId = assignmentChange.CustomerId;
            ViewBag.CustomerName = assignmentChange.CustomerName;
            ViewBag.Version = assignmentChange.Version;
            ViewBag.ConfirmedDate = assignmentChange.ConfirmedDate;
            ViewBag.ProcessMan = assignmentChange.Designee;
            ViewBag.DesigneeName = assignmentChange.DesigneeName;
            ViewBag.NoOfPrints = assignmentChange.NoOfPrints;
            ViewBag.Confirmor = assignmentChange.Confirmor;
            ViewBag.ConfirmorName = assignmentChange.ConfirmorName;
            ViewBag.ModiReason = assignmentChange.ModiReason;
            ViewBag.Remark = assignmentChange.Remark;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        //刪除
        [HttpPost]
        public JsonResult Delete(string AsignOrderType, string AsignOrderNo, string Version)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            AssignmentChange assignmentChange = new AssignmentChange();
            assignmentChange.AssignOrderType = AsignOrderType;
            assignmentChange.AssignOrderNo = AsignOrderNo;
            assignmentChange.Version = Version;
            string msg = "";
            if (!logic.DelAssignmentChangeRGA_H(assignmentChange, out msg))
            {
                msg = "NO-刪除失敗!" + msg;
            }
            else
            {
                msg = "OK-刪除成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchPicking_ProductSerial(string PickingOrderType, string PickingOrderNo,string ItemId)
        {
            Picking_ProductSerialLogic logic=new Picking_ProductSerialLogic();

            List<Picking_ProductSerial> lst = logic.SeachPicking_ProductSerial(PickingOrderType, PickingOrderNo, ItemId);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AddSerial(string PickingOrderType, string PickingOrderNo, string ItemId, string[] SerialNoValue)
        //{
        //    Picking_ProductSerial picking_ProductSerial = new Picking_ProductSerial();
        //    picking_ProductSerial.PickingOrderType = PickingOrderType;
        //    picking_ProductSerial.PickingOrderNo = PickingOrderNo;
        //    picking_ProductSerial.ItemId = ItemId;

        //    Picking_ProductSerialLogic logic = new Picking_ProductSerialLogic();
        //    string msg = "";
        //    try
        //    {
        //        logic.AddPicking_ProductSerial(picking_ProductSerial, SerialNoValue);
        //        msg = "OK-保存成功";
        //    }catch
        //    {
        //        msg = "NO-保存失敗";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}
    }
}