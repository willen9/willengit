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

namespace PMAB03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = ",h.AssignCheck";
            string condition = ",=";
            string value = ",N";
            ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = ",h.AssignCheck";
            string condition = ",=";
            string value = "," + Request.Form["OrderStatus"];

            if (Request.Form["action"] == "btnSearch")
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col += ",h.RoutineSerOrderNo";
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

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
            }

            if (Request.Form["action"] == "btnAllot")
            {

                string SupportId = Request.Form["chk"];
                Assignment assignment = new Assignment();
                assignment.AssignDate = Request.Form["requesDate"].Replace("/", "");
                assignment.Designee = Request.Form["EmployeeId"];
                assignment.Assignor = "";
                assignment.AssignOrderType = Request.Form["ordertype"];
                if (!logic.UpdateAsign(assignment, SupportId))
                {
                    ViewBag.Msg = "分配失敗！";
                }
            }
            if (Request.Form["action"] == "btnAllotAgain")
            {

                string SupportId = Request.Form["chk"];
                AssignmentChange assignmentChange = new AssignmentChange();
                assignmentChange.Designee = Request.Form["EmployeeIdAgain"];
                assignmentChange.Assignor = "";
                assignmentChange.ModiReason = Request.Form["remarkAgain"];
                assignmentChange.Company = "";
                assignmentChange.UserGroup = "";
                assignmentChange.Creator = "";
                if (!logic.UpdateAsignAgain(assignmentChange, SupportId))
                {
                    ViewBag.Msg = "分配失敗！";
                }
            }
            if (Request.Form["action"] == "btnExport")
            {

                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
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
                    string OrderStatus = "";
                    string CustomerType = "";
                    int i = 0;
                    foreach (var obj in objSupportApl_H)
                    {
                        if (obj.OrderStatus == "0")
                        {
                            OrderStatus = "未處理";
                        }
                        else if (obj.OrderStatus == "1")
                        {
                            OrderStatus = "已派工";
                        }
                        else if (obj.OrderStatus == "2")
                        {
                            OrderStatus = "完工";
                        }
                        else
                        {
                            OrderStatus = "作廢";
                        }

                        if (obj.CustomerType == "A")
                        {
                            CustomerType = "一般客戶";
                        }
                        else
                        {
                            CustomerType = "經銷商";
                        }
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SupportAplOrderType);
                        row.CreateCell(1).SetCellValue(obj.SupportAplOrderNo);
                        row.CreateCell(2).SetCellValue(obj.CustomerName);
                        row.CreateCell(3).SetCellValue(obj.SupportDept);
                        row.CreateCell(4).SetCellValue(obj.SupportItemAll);
                        row.CreateCell(5).SetCellValue(obj.RequestDate);
                        row.CreateCell(6).SetCellValue(OrderStatus);
                        row.CreateCell(7).SetCellValue(obj.ProcessManName);
                        row.CreateCell(8).SetCellValue(obj.AsignManName);
                        row.CreateCell(9).SetCellValue(obj.CreatorName);
                        row.CreateCell(10).SetCellValue(obj.IsPicking);
                        row.CreateCell(11).SetCellValue(obj.SourceOrderNo);
                        row.CreateCell(12).SetCellValue(CustomerType);
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
            ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
            ViewBag.OrderStatus = Request.Form["OrderStatus"];
            ViewBag.selCondsupportaplorderno = Request.Form["selCondsupportaplorderno"];
            ViewBag.supportaplorderno = Request.Form["supportaplorderno"];
            ViewBag.selCondcustomerid = Request.Form["selCondcustomerid"];
            ViewBag.customerid = Request.Form["customerid"];
            return View();
        }

        public ActionResult CUR()
        {
            return View("CUR");
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

        public ActionResult Details(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
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