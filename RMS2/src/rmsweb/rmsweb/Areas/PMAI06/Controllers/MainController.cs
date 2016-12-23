using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using System.Web.Mvc;
using rmsweb.Controllers;

namespace PMAI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",D3";
            ViewData["AssignmentChange"] = logic.SearchAssignmentChange(col, condition, value);
            return View();
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
                    col = ",a.AssignOrderNo";
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

        public JsonResult SearchSupportAplAsign(string AsignOrderType, string AsignOrderNo, int Page)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = AsignOrderType;
            assignment.AssignOrderNo = AsignOrderNo;
            List<Assignment> lst = logic.GetAssignmentRoutineService_H(assignment, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
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
                //supportAplAsignModi.SupportAplOrderType = Request.Form["SupportAplOrderType"];
                //supportAplAsignModi.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
                assignmentChange.Designee = Request.Form["processman"];
                assignmentChange.Assignor = Request.Form["updateman"];
                assignmentChange.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
                assignmentChange.Remark = Request.Form["remark"];
                assignmentChange.ModiReason = Request.Form["modireason"];
                string msg = "";
                if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
                {
                    if (!logic.UpdateAssignmentChange(assignmentChange, "D3", out msg))
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
                    if (!logic.AddAssignmentServiceArrangeChange(assignmentChange, out msg))
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
                        ViewBag.Remark = Request.Form["OldRemark"];
                        ViewBag.Type = "Save";
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
        public JsonResult GetAssignmentServiceApl(string Col, string Condition, string conditionValue, int Page)
        {
            AssignmentChangeLogic logic = new AssignmentChangeLogic();
            List<Assignment> lst = logic.GetAssignmentServiceApl(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add()
        {
            ProductCategory productCategory = new ProductCategory();
            productCategory.ProductCategoryType = Request.Form["parenttype"];
            productCategory.ProductTypeId = Request.Form["typeid"];
            productCategory.ProductType = Request.Form["typename"];

            ProductCategoryLogic logic = new ProductCategoryLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddProductCategory(productCategory, out infomsg))
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
            ProductCategory productCategory = new ProductCategory();
            productCategory.ProductCategoryType = Request.Form["parenttype"];
            productCategory.ProductTypeId = Request.Form["typeid"];
            productCategory.ProductType = Request.Form["typename"];

            ProductCategoryLogic logic = new ProductCategoryLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateProductCategory(productCategory,out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();

            try
            {
                logic.DeleteProductCategory(ProductCategoryType, ProductTypeId);
            }catch(Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            //ProductCategory productCategory = new ProductCategory();
            //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory);

            //return View("Index");
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string AssignOrderType, string AssignOrderNo)
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = AssignOrderType;
            assignment.AssignOrderNo = AssignOrderNo;
            assignment = logic.GetAssignmentInfo(assignment);
            ViewBag.OrderSName = assignment.OrderSName;
            ViewBag.AutoConfirm = assignment.AutoConfirm;
            ViewBag.CheckExOrder = assignment.CheckExOrder;
            ViewBag.AssignOrderType = assignment.AssignOrderType;
            ViewBag.AssignOrderNo = assignment.AssignOrderNo;
            ViewBag.OrderDate = assignment.OrderDate;
            ViewBag.CustomerId = assignment.CustomerId;
            ViewBag.CustomerName = assignment.CustomerName;
            ViewBag.Version = assignment.Version;
            ViewBag.SourceOrderType = assignment.SourceOrderType;
            ViewBag.SourceOrderNo = assignment.SourceOrderNo;
            ViewBag.Designee = assignment.Designee;
            ViewBag.DesigneeName = assignment.DesigneeName;
            ViewBag.Assignor = assignment.Assignor;
            ViewBag.AssignorName = assignment.AssignorName;
            ViewBag.AssignDate = assignment.AssignDate;
            ViewBag.NoOfPrints = assignment.NoOfPrints;
            ViewBag.Remark = assignment.Remark;
            ViewBag.Confirmed = assignment.Confirmed;
            ViewBag.ConfirmedDate = assignment.ConfirmedDate;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string AssignOrderType, string AssignOrderNo)
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = AssignOrderType;
            assignment.AssignOrderNo = AssignOrderNo;
            assignment = logic.GetAssignmentInfo(assignment);
            ViewBag.AssignOrderType = assignment.AssignOrderType;
            ViewBag.OrderSName = assignment.OrderSName;
            ViewBag.AutoConfirm = assignment.AutoConfirm;
            ViewBag.CheckExOrder = assignment.CheckExOrder;
            ViewBag.AssignOrderNo = assignment.AssignOrderNo;
            ViewBag.OrderDate = assignment.OrderDate;
            ViewBag.CustomerId = assignment.CustomerId;
            ViewBag.CustomerName = assignment.CustomerName;
            ViewBag.Version = assignment.Version;
            ViewBag.SourceOrderType = assignment.SourceOrderType;
            ViewBag.SourceOrderNo = assignment.SourceOrderNo;
            ViewBag.Designee = assignment.Designee;
            ViewBag.DesigneeName = assignment.DesigneeName;
            ViewBag.Assignor = assignment.Assignor;
            ViewBag.AssignorName = assignment.AssignorName;
            ViewBag.AssignDate = assignment.AssignDate;
            ViewBag.NoOfPrints = assignment.NoOfPrints;
            ViewBag.Remark = assignment.Remark;
            ViewBag.Confirmed = assignment.Confirmed;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
            return View("CUR");
        }
    }
}