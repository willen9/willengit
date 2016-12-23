using Aspose.Cells;
using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace SUPI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            AssignmentLogic logic = new AssignmentLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",A2";
            ViewData["Assignment"] = logic.SearchAssignment(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {

            AssignmentLogic logic = new AssignmentLogic();
            string col = ",o.OrderCategory";
            string condition = ",=";
            string value = ",A2";
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
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
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
            ViewBag.NoOfPrints = "0";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = Request.Form["AsignOrderType"];
            assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
            assignment.OrderDate = Request.Form["orderdate"].Replace("/","");
            assignment.CustomerId = Request.Form["customerId"];
            assignment.Version = Request.Form["id_version"];
            assignment.SourceOrderType = Request.Form["SupportAplOrderType"];
            assignment.SourceOrderNo = Request.Form["SupportAplOrderNo"];
            assignment.Designee = Request.Form["ProcessMan"];
            assignment.Assignor = "";
            assignment.AssignDate = DateTime.Now.ToString("yyyyMMdd");
            assignment.NoOfPrints = int.Parse(Request.Form["noofprint"] == "" ? "0" : Request.Form["noofprint"]);
            assignment.Remark = Request.Form["remark"];
            //supportAplAsign.Confirmed = Request.Form[""];
            string msg = "";
            if(Request.Form["action"]== "ConfirmedY")
            {
                assignment.Confirmed = "Y";
                if (!logic.Confirmed(assignment,"A2", out msg))
                {
                    assignment = new Assignment();
                    assignment.AssignOrderType = Request.Form["AsignOrderType"];
                    assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
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

                    ViewBag.Msg = "派工確認失敗！" + msg;
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
                assignment.Confirmed = "N";
                if (!logic.Confirmed(assignment, "A2", out msg))
                {
                    assignment = new Assignment();
                    assignment.AssignOrderType = Request.Form["AsignOrderType"];
                    assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
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

                    ViewBag.Msg = "取消確認失敗！" + msg;
                    ViewBag.Type = "Details";
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
                if (!logic.Confirmed(assignment, "A2", out msg))
                {
                    assignment = new Assignment();
                    assignment.AssignOrderType = Request.Form["AsignOrderType"];
                    assignment.AssignOrderNo = Request.Form["AsignOrderNo"];
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
                if (!logic.UpdateAssignment(assignment,out msg))
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

                    ViewBag.Msg = "修改失敗！"+ msg;
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
                if (!logic.AddAssignment(assignment, "A2", out msg))
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
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }
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

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "A2"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeNo(string OrderType)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            return Json(logic.GetSupportAplOrderNo(OrderType), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchCustomer(string CustomerId, string CustomerName, int Page)
        //{
        //    CustomerLogic logic = new CustomerLogic();
        //    Customer customer = new Customer();
        //    customer.CustomerId = CustomerId;
        //    customer.CustomerName = CustomerName;
        //    List<Customer> lst = logic.GetCustomer(customer,"","","", Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchSupportApl_H(string SupportAplOrderType, string Col, string Condition, string conditionValue, string CustomerId, int Page)
        //{
        //    SupportApl_HLogic logic = new SupportApl_HLogic();
        //    SupportApl_H supportApl_H = new SupportApl_H();
        //    supportApl_H.SupportAplOrderType = SupportAplOrderType;
        //    //supportApl_H.SupportAplOrderNo = SupportAplOrderNo;
        //    supportApl_H.CustomerId = CustomerId;
        //    supportApl_H.AsignCheck = "N";
        //    List<SupportApl_H> lst = logic.GetSupportInfo(supportApl_H, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchEmployee(string Col, string Condition, string conditionValue, int Page)
        {
            EmployeeLogic logic = new EmployeeLogic();
            List<Employee> lst = logic.GetEmployee(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string AssignOrderType, string AssignOrderNo) 
        {
            AssignmentLogic logic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.AssignOrderType = AssignOrderType;
            assignment.AssignOrderNo = AssignOrderNo;
            string msg = "";
            if (!logic.DelAssignment(assignment, out msg))
            {
                msg = "NO-刪除失敗!" + msg;
            }else
            {
                msg = "OK-刪除成功";
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

        public ActionResult Print(string AsignOrderType, string AsignOrderNo)
        {
            MemoryStream ms = null;
            Workbook workbook = null;
            Worksheet sheet = null;
            string Copypath = "";
            SupportAplAsignLogic Asignlogic = new SupportAplAsignLogic();
            SupportAplAsign supportAplAsign = Asignlogic.GetSupportAplAsignInfo(AsignOrderType, AsignOrderNo);
            SupportApl_HLogic logic = new SupportApl_HLogic();
            //try
            //{

            string path = Server.MapPath(@"~\Template");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(Server.MapPath(@"~\ExpotFile")))
            {
                Directory.CreateDirectory(Server.MapPath(@"~\ExpotFile"));
            }
            string fileName = path + @"\支援單.xls";
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("上传成功！文件未汇出,模板不存在！");
            }
            string strGUID = Guid.NewGuid().ToString();

            Copypath = Server.MapPath(@"~\ExpotFile") + @"\" + strGUID + ".xlsx";

            System.IO.File.Copy(fileName, Copypath);


            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                ms = new MemoryStream();

                workbook = new Workbook(fs);
                sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                SupportApl_H supportApl_H = logic.SupportItemInfo(supportAplAsign.SupportAplOrderType, supportAplAsign.SupportAplOrderNo);

                cells[5, 1].PutValue(supportApl_H.ApplyDate);//申請日期
                cells[5, 4].PutValue(supportApl_H.RequestDate);//需求日期
                cells[5, 7].PutValue(supportApl_H.SupportAplOrderNo);//申請單號
                cells[6, 1].PutValue(supportApl_H.RequestTime);//指定日期
                cells[7, 1].PutValue(supportApl_H.CustomerId);//客戶編號
                cells[7, 4].PutValue(supportApl_H.CustomerName);//客戶名稱
                cells[8, 1].PutValue(supportApl_H.SalesName);//聯絡人
                cells[8, 5].PutValue(supportApl_H.TelNo);//電話
                cells[9, 1].PutValue(supportApl_H.FaxNo);//傳真
                cells[9, 5].PutValue(supportApl_H.AsignManName);//業務人員
                cells[10, 1].PutValue(supportApl_H.AddressSName);//地址
                cells[11, 1].PutValue("");//客戶環境
                //客戶屬性
                if (supportApl_H.CustomerType == "A")
                {
                    cells[11, 5].PutValue("一般客戶");
                }
                if (supportApl_H.CustomerType == "B")
                {
                    cells[11, 5].PutValue("經銷商");
                }

                cells[12, 1].PutValue("");//需求項目

                SupportApl_ProductDLogic productLogic = new SupportApl_ProductDLogic();
                ViewData["SupportApl_ProductD"] = productLogic.GetSupportApl_ProductD(supportAplAsign.SupportAplOrderType, supportAplAsign.SupportAplOrderNo);
                DataTable dtSupportApl_ProductD = ViewData["SupportApl_ProductD"] as DataTable;
                if (dtSupportApl_ProductD != null && dtSupportApl_ProductD.Rows.Count > 0)
                {
                    int index = 16;
                    foreach (DataRow dr in (ViewData["SupportApl_ProductD"] as DataTable).Rows)
                    {
                        cells[index, 1].PutValue(dr["ProductName"].ToString());//商品
                        cells[index, 6].PutValue(dr["QTY"].ToString());//數量
                        index++;
                        cells[index, 1].PutValue("");//序號
                        index++;
                        cells[index, 1].PutValue("");//介面
                        cells[index, 6].PutValue("");//軟體
                        index++;
                        cells[index, 1].PutValue(dr["Remark"].ToString());//備註
                        index++;
                        cells[index, 1].PutValue(dr["Unit"].ToString());//耗材規格
                        index++;

                    }
                }

                cells[33, 1].PutValue(supportApl_H.Remark);//注意事項
                cells[40, 1].PutValue(supportApl_H.CreatorName);//申請人員
                cells[40, 4].PutValue("");//支援人員
                cells[40, 7].PutValue("");//支援主管
                workbook.Save(Copypath);

                //打印動作

                Workbook objWorkbook = new Workbook(Copypath);
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (ms != null)
            //    {
            //        ms.Close();
            //        ms.Dispose();
            //    }
            //}
            Asignlogic.PrintSupportAplAsign(AsignOrderType, AsignOrderNo);
            return File(new FileStream(Copypath, FileMode.Open), "application/ms-excel", "申請單.xls");
        }
    }
}