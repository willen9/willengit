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

namespace SUPI07.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["AssignmentChange"] = logic.SearchAssignmentChangeSUPI(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
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
                ViewData["AssignmentChange"] = logic.SearchAssignmentChangeSUPI(col, condition, value);

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
                //    sw.WriteLine("單別,單號,版次,單據日期,客戶名稱,狀態,支援人員,變更日期,變更人員,變更原因,備註");
                //    List<SupportAplAsignModi> objSupportAplAsignModi = ViewData["SupportAplAsignModi"] as List<SupportAplAsignModi>;

                //    string confirmed = "";
                //    foreach (var obj in objSupportAplAsignModi)
                //    {
                //        if (obj.Confirmed == "Y")
                //        {
                //            confirmed = "已派工";
                //        }
                //        else if (obj.Confirmed == "N")
                //        {
                //            confirmed = "未派工";
                //        }
                //        else
                //        {
                //            confirmed = "作廢";
                //        }

                //        sw.WriteLine(obj.AsignOrderType + "," + obj.AsignOrderNo +
                //            "," + obj.Version + "," + obj.OrderDate + "," +
                //            obj.CustomerName + "," + confirmed + "," +
                //            obj.ProcessManName + "," + obj.ConfirmedDate + "," +
                //            obj.Confirmor + "," + obj.ConfirmorName + "," +
                //            obj.ModiReason + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "SupportAplAsignModi.csv");
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
                    col = ",a.AsignOrderNo";
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
            ViewData["AssignmentChange"] = logic.SearchAssignmentChangeSUPI(col, condition, value);

            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
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
                assignmentChange.Designee = Request.Form["processman"];
                assignmentChange.Assignor = Request.Form["updateman"];
                assignmentChange.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
                assignmentChange.Remark = Request.Form["remark"];
                assignmentChange.ModiReason = Request.Form["modireason"];
                string msg = "";
                if (Request.Form["action"] == "ConfirmedY")
                {
                    assignmentChange.Confirmed = "Y";
                    if (!logic.Confirmed(assignmentChange, "A2", out msg))
                    {
                        assignmentChange.AssignOrderType = Request.Form["AsignOrderType"];
                        assignmentChange.AssignOrderNo = Request.Form["AsignOrderNo"];
                        assignmentChange.Version = Request.Form["version"];
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
                    if (!logic.Confirmed(assignmentChange, "A2", out msg))
                    {
                        assignmentChange.AssignOrderType = Request.Form["AsignOrderType"];
                        assignmentChange.AssignOrderNo = Request.Form["AsignOrderNo"];
                        assignmentChange.Version = Request.Form["version"];
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
                    if (!logic.UpdateAssignmentChange(assignmentChange, "A2", out msg))
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
                        ViewBag.Msg = "修改失敗！"+msg;
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
                        ViewBag.ProcessMan = Request.Form["processman"];
                        ViewBag.AsignMan = Request.Form["updateman"];
                        ViewBag.NoOfPrints = Request.Form["noofprint"];
                        ViewBag.ModiReason = Request.Form["modireason"];
                        ViewBag.Remark = Request.Form["remark"];
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
            catch(Exception ex)
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

        public JsonResult SearchSupportAplAsign(string Col, string Condition, string conditionValue, int Page)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            List<Assignment> lst = logic.GetAssignmentSupportApl(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string AsignOrderType, string AsignOrderNo,string Version)
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
            ViewBag.Version = assignmentChange.Version ;
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
            if (!logic.DelAssignmentChange(assignmentChange, out msg))
            {
                msg = "NO-刪除失敗!" + msg;
            }
            else
            {
                msg = "OK-刪除成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult voidModi(string AsignOrderType, string AsignOrderNo)
        {
            SupportAplAsignLogic logic = new SupportAplAsignLogic();

            try
            {
                logic.DeleteSupportAplAsign(AsignOrderType, AsignOrderNo);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            return RedirectToAction("Index", "Main");
        }
    }
}