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

namespace PMAI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            AssignmentLogic logic = new AssignmentLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",D3";
            ViewData["Assignment"] = logic.SearchAssignment(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            AssignmentLogic logic = new AssignmentLogic();
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
                ViewData["Assignment"] = logic.SearchAssignment(col, condition, value);

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
                //    sw.WriteLine("單別,單號,單據日期,客戶簡稱,狀態,支援人員,派工日期,派工人員,備註,來源單別,來源單號");
                //    List<SupportAplAsign> objSupportAplAsign = ViewData["SupportAplAsign"] as List<SupportAplAsign>;
                //    string orderStatus="";
                //    foreach (var obj in objSupportAplAsign)
                //    {
                //        if(obj.OrderStatus=="0")
                //        {
                //            orderStatus = "未處理";
                //        }
                //        else if(obj.OrderStatus=="1")
                //        {
                //            orderStatus = "已派工";
                //        }
                //        else if (obj.OrderStatus == "2")
                //        {
                //            orderStatus = "完工";
                //        }
                //        else
                //        {
                //            orderStatus = "作廢";
                //        }

                //        sw.WriteLine(obj.AsignOrderType + "," + obj.AsignOrderNo +
                //            "," + obj.OrderDate + "," + obj.CustomerName + "," +
                //             orderStatus + "," +
                //            obj.DepartmentName + "," + obj.AsignDate + "," +
                //            obj.AsignManName + "," + obj.Remark + "," +
                //            obj.SupportAplOrderType + "," + obj.SupportAplOrderNo);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                //ViewBag.selCond1 = Request.Form["selCond1"];
                //ViewBag.selCond2 = Request.Form["selCond2"];
                //ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                //ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "SupportAplAsign.csv");
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
                    //單別,單號,單據日期,客戶簡稱,狀態,支援人員,派工日期,派工人員,備註,來源單別,來源單號
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("單據日期");
                    row.CreateCell(3).SetCellValue("客戶簡稱");
                    row.CreateCell(4).SetCellValue("狀態");
                    row.CreateCell(5).SetCellValue("支援人員");
                    row.CreateCell(6).SetCellValue("派工日期");
                    row.CreateCell(7).SetCellValue("派工人員");
                    row.CreateCell(8).SetCellValue("備註");
                    row.CreateCell(9).SetCellValue("來源單別");
                    row.CreateCell(10).SetCellValue("來源單號");
                    List<Assignment> objAssignment = ViewData["Assignment"] as List<Assignment>;
                    int i = 0;
                    foreach (var obj in objAssignment)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.AssignOrderType);
                        row.CreateCell(1).SetCellValue(obj.AssignOrderNo);
                        row.CreateCell(2).SetCellValue(obj.OrderDate);
                        row.CreateCell(3).SetCellValue(obj.CustomerName);
                        row.CreateCell(4).SetCellValue(obj.Confirmed);
                        row.CreateCell(5).SetCellValue(obj.CustomerName);
                        row.CreateCell(6).SetCellValue(obj.Assignor);
                        row.CreateCell(7).SetCellValue(obj.AssignDate);
                        row.CreateCell(8).SetCellValue(obj.Remark);
                        row.CreateCell(9).SetCellValue(obj.SourceOrderType);
                        row.CreateCell(10).SetCellValue(obj.SourceOrderNo);
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
                ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "SupportAplAsign.xls");
            }
            else
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col = ",a.AssignOrderType";
                    condition = "," + Request.Form["selCond1"];
                    value = "," + Request.Form["supportaplorderno"];
                }
                if (Request.Form["AsignOrderNo"].Trim() != "")
                {
                    col = ",a.AssignOrderNo";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["AsignOrderNo"];
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];

            }

            ViewData["Assignment"] = logic.SearchAssignment(col, condition, value);
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Version = "0000";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = Request.Form["AsignOrderType"];
            assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
            assignment.OrderDate = Request.Form["orderdate"].Replace("/", "");
            assignment.CustomerId = Request.Form["customerId"];
            assignment.Version = Request.Form["id_version"];
            assignment.SourceOrderType = Request.Form["SupportAplOrderType"];
            assignment.SourceOrderNo = Request.Form["SupportAplOrderNo"];
            assignment.Designee = Request.Form["ProcessMan"];
            assignment.Assignor = Request.Form["asignman"];
            assignment.AssignDate = Request.Form["id_asigndate"];
            assignment.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
            assignment.Remark = Request.Form["remark"];
            //supportAplAsign.Confirmed = Request.Form[""];
            string msg = "";
            if (Request.Form["action"] == "ConfirmedY")
            {
                assignment.Confirmed = "Y";
                if (!logic.Confirmed(assignment,"D3", out msg))
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

                    ViewBag.Msg = "派工確認失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            if (Request.Form["action"] == "ConfirmedV")
            {
                assignment.Confirmed = "V";
                if (!logic.Confirmed(assignment, "D3", out msg))
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
                    return View("CUR");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
            {
                if (!logic.UpdateAssignment(assignment, out msg))
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
                if (!logic.AddAssignment(assignment,"D3", out msg))
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
            else
            {
                assignment.AssignOrderType = Request.Form["AsignOrderType"];
                assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
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
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }
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

        public ActionResult Details(string AssignOrderType, string AssignOrderNo)
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
        //[HttpPost]
        //public JsonResult Delete(string AssignOrderType, string AssignOrderNo)
        //{
        //    AssignmentLogic logic = new AssignmentLogic();
        //    Assignment assignment = new Assignment();
        //    assignment.AssignOrderType = AssignOrderType;
        //    assignment.AssignOrderNo = AssignOrderNo;
        //    string msg = "";
        //    if (!logic.DelAssignment(assignment, out msg))
        //    {
        //        msg = "NO-刪除失敗!" + msg;
        //    }
        //    else
        //    {
        //        msg = "OK-刪除成功";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Delete(string AssignOrderType, string AssignOrderNo)
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = AssignOrderType;
            assignment.AssignOrderNo = AssignOrderNo;
            string msg = "";
            if (!logic.DelRoutineService(assignment, out msg))
            {
                msg = "NO-刪除失敗!" + msg;
            }
            else
            {
                msg = "OK-刪除成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA_H(string Col, string Condition, string conditionValue, int Page)
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            //Col += ",StatusCode";
            //Condition += ",=";
            //conditionValue += ",00";
            List<RoutineService_H> lst = logic.SearchRGA_H(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
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

        //[HttpPost]
        //public ActionResult CUR(string s)
        //{
        //    AssignmentLogic logic = new AssignmentLogic();
        //    Assignment assignment = new Assignment();
        //    assignment.AssignOrderType = Request.Form["AsignOrderType"];
        //    assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
        //    assignment.OrderDate = Request.Form["orderdate"].Replace("/", "");
        //    assignment.CustomerId = Request.Form["customerId"];
        //    assignment.Version = Request.Form["id_version"];
        //    assignment.SourceOrderType = Request.Form["SupportAplOrderType"];
        //    assignment.SourceOrderNo = Request.Form["SupportAplOrderNo"];
        //    assignment.Designee = Request.Form["ProcessMan"];
        //    assignment.Assignor = Request.Form["asignman"];
        //    assignment.AssignDate = Request.Form["id_asigndate"];
        //    assignment.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
        //    assignment.Remark = Request.Form["remark"];
        //    //supportAplAsign.Confirmed = Request.Form[""];
        //    string msg = "";
        //    if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
        //    {
        //        if (!logic.UpdateAssignment(assignment, out msg))
        //        {
        //            ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
        //            ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
        //            ViewBag.OrderDate = Request.Form["orderdate"];
        //            ViewBag.CustomerId = Request.Form["customerId"];
        //            ViewBag.CustomerName = Request.Form["idcustomerName"];
        //            ViewBag.Version = Request.Form["id_version"];
        //            ViewBag.SupportAplOrderType = Request.Form["SupportAplOrderType"];
        //            ViewBag.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
        //            ViewBag.ProcessMan = Request.Form["ProcessMan"];
        //            ViewBag.ProcessManName = Request.Form["idProcessManName"];
        //            ViewBag.AsignMan = Request.Form["asignman"];
        //            ViewBag.AsignManName = Request.Form["idAsignManName"];
        //            //ViewBag.AsignDate = Request.Form["id_asigndate"];
        //            ViewBag.NoOfPrints = Request.Form["noofprint"];
        //            ViewBag.Remark = Request.Form["remark"];
        //            ViewBag.Msg = "修改失敗！" + msg;
        //            return View("CUR");
        //        }
        //        else
        //        {
        //            if (Request.Form["action"] == "Edit")
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("CUR");
        //            }
        //        }
        //    }
        //    else if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
        //    {
        //        if (!logic.AddAssignment(assignment, "D2", out msg))
        //        {
        //            ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
        //            ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
        //            ViewBag.OrderDate = Request.Form["orderdate"];
        //            ViewBag.CustomerId = Request.Form["customerId"];
        //            ViewBag.CustomerName = Request.Form["idcustomerName"];
        //            ViewBag.Version = Request.Form["id_version"];
        //            ViewBag.SupportAplOrderType = Request.Form["SupportAplOrderType"];
        //            ViewBag.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
        //            ViewBag.ProcessMan = Request.Form["ProcessMan"];
        //            ViewBag.ProcessManName = Request.Form["idProcessManName"];
        //            ViewBag.AsignMan = Request.Form["asignman"];
        //            ViewBag.AsignManName = Request.Form["idAsignManName"];
        //            //ViewBag.AsignDate = Request.Form["id_asigndate"];
        //            ViewBag.NoOfPrints = Request.Form["noofprint"];
        //            ViewBag.Remark = Request.Form["remark"];
        //            ViewBag.AsignCheck = Request.Form["AsignCheck"];
        //            ViewBag.Msg = "新增失敗！" + msg;
        //            return View("CUR");
        //        }
        //        else
        //        {
        //            if (Request.Form["action"] == "Save")
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("CUR");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.AsignOrderType = Request.Form["AsignOrderType"];
        //        ViewBag.AsignOrderNo = Request.Form["AsignOrderNo"];
        //        ViewBag.OrderDate = Request.Form["orderdate"];
        //        ViewBag.CustomerId = Request.Form["customerId"];
        //        ViewBag.CustomerName = Request.Form["idcustomerName"];
        //        ViewBag.Version = Request.Form["id_version"];
        //        ViewBag.SupportAplOrderType = Request.Form["SupportAplOrderType"];
        //        ViewBag.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
        //        ViewBag.ProcessMan = Request.Form["ProcessMan"];
        //        ViewBag.ProcessManName = Request.Form["idProcessManName"];
        //        ViewBag.AsignMan = Request.Form["asignman"];
        //        ViewBag.AsignManName = Request.Form["idAsignManName"];
        //        //ViewBag.AsignDate = Request.Form["id_asigndate"];
        //        ViewBag.NoOfPrints = Request.Form["noofprint"];
        //        ViewBag.Remark = Request.Form["remark"];
        //        ViewBag.AsignCheck = Request.Form["AsignCheck"];
        //        if (Request.Form["action"] == "EditDetails")
        //        {
        //            ViewBag.Type = "Edit";
        //        }
        //        return View("CUR");
        //    }
        //}
    }
}