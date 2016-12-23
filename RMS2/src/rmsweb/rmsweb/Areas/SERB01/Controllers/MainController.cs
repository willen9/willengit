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

namespace SERB01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGA_HLogic logic = new RGA_HLogic();
            
            string col = ",h.StatusCode";
            string condition = ",=";
            string value = ",00";
            ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            RGA_H rGA_H = new RGA_H();
            string col = ",h.StatusCode";
            string condition = ",=";
            string value = "," + Request.Form["OrderStatus"];

            if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,客戶簡稱,品號,品名,序號,建立日期,建立人員");
                //    List<RMAApl> objRMAApl = ViewData["RMAApl"] as List<RMAApl>;
                //    foreach (var obj in objRMAApl)
                //    {
                //        sw.WriteLine(obj.RMAAplType + "," + obj.OrderSName + "," + obj.RMAAplNo + "," + obj.CustomerName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.CreateDate + "," + obj.CreatorName);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RGA_H");
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
                    row.CreateCell(3).SetCellValue("客戶名稱");
                    row.CreateCell(4).SetCellValue("地址簡稱");
                    row.CreateCell(5).SetCellValue("品號");
                    row.CreateCell(6).SetCellValue("品名");
                    row.CreateCell(7).SetCellValue("序號");
                    row.CreateCell(8).SetCellValue("備註");
                    row.CreateCell(9).SetCellValue("資產編號");
                    row.CreateCell(10).SetCellValue("預計完修日");
                    row.CreateCell(11).SetCellValue("建立人員");


                    int index = 1;

                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;

                    foreach (var obj in objRGA_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.RGAType.ToString());
                        //row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RGANo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.CustomerId.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(6).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(7).SetCellValue(obj.SerialNo.ToString());
                        //row.CreateCell(8).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(9).SetCellValue(obj.AssetNo.ToString());
                        row.CreateCell(10).SetCellValue(obj.PCompletionDate.ToString());
                        //row.CreateCell(11).SetCellValue(obj.UserGroup.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAApl.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA_H.xls");
            }

            if (Request.Form["action"] == "btnSearch")
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col += ",h.RGANo";
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
                if (!logic.UpdateAsignAgain(assignmentChange, SupportId))
                {
                    ViewBag.Msg = "分配失敗！";
                }
            }

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }

            ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
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