using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;
using Aspose.Cells;
using Aspose.Cells.Rendering;
//using Aspose.Words;
using Aspose.BarCode;
using rmsweb.Controllers;

namespace PMAB02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);

            }
            else if (Request.Form["action"] == "btnPrint")
            {
               

            }
            else
            {
                if (Request.Form["strRoutineSerOrderNo"].Trim() != "")
                {
                    col += ",h.RoutineSerOrderNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strRoutineSerOrderNo"];
                }
                if (Request.Form["strCustomerId"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["strCustomerId"];
                }
                if (Request.Form["strArrangeMonth"].Trim() != "")
                {
                    col += ",h.ArrangeMonth";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["strArrangeMonth"];
                }
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
                ViewBag.strRoutineSerOrderNo = Request.Form["strRoutineSerOrderNo"];
                ViewBag.strCustomerId = Request.Form["strCustomerId"];
                ViewBag.strArrangeMonth = Request.Form["strArrangeMonth"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Print(string OrderType, string OrderNo)
        {
            MemoryStream ms = null;
            Workbook workbook = null;
            Worksheet sheet = null;
            string Copypath = "";
            SupportApl_HLogic logic = new SupportApl_HLogic();
            //try
            //{

            string path = Server.MapPath(@"~\Template");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(Server.MapPath(@"~\exports")))
            {
                Directory.CreateDirectory(Server.MapPath(@"~\exports"));
            }
            string fileName = path + @"\GL4XX.xls";
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("文件未汇出,模板不存在！");
            }
            string strGUID = Guid.NewGuid().ToString();

            Copypath = Server.MapPath(@"~\exports") + @"\" + strGUID + ".xls";

            System.IO.File.Copy(fileName, Copypath);


            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                ms = new MemoryStream();

                workbook = new Workbook(fs);
                sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                //RoutineService_HLogic hlogic = new RoutineService_HLogic();
                //ViewData["RoutineService_H"] = hlogic.SearchRoutineService_H(OrderType, OrderNo);

                RoutineService_HLogic HLogic = new RoutineService_HLogic();

                RoutineService_H routineService_H = new RoutineService_H();
                routineService_H.RoutineSerOrderType = OrderType;
                routineService_H.RoutineSerOrderNo =OrderNo;

                routineService_H = HLogic.GetRoutineService_HInfo(routineService_H);

                cells[3, 7].PutValue(OrderNo);//單號

                cells[6, 1].PutValue("");//合約項次
                cells[6, 5].PutValue("");//定保次數
                cells[7, 1].PutValue(routineService_H.CustomerId);//客戶編號
                cells[7, 5].PutValue("");//客戶名稱
                cells[8, 1].PutValue(routineService_H.Contact);//聯絡人
                cells[8, 5].PutValue(routineService_H.SaleDate);//購買日期
                cells[9, 1].PutValue(routineService_H.TelNo);//電話
                cells[9, 5].PutValue(routineService_H.FaxNo);//傳真

                cells[10, 1].PutValue(routineService_H.Address);//地址

                cells[11, 1].PutValue(routineService_H.OrderDate);//定保日期
                cells[11, 5].PutValue("");//業務人員
                cells[12, 1].PutValue(routineService_H.WarrantySDate);//保固起日
                cells[12, 5].PutValue(routineService_H.WarrantyEDate);//保固止日
                cells[13, 1].PutValue(routineService_H.ProductNo+ routineService_H.ProductName);//保養機型
                cells[13, 5].PutValue(routineService_H.SerialNo);//序號

                workbook.Save(Copypath);

                //打印動作

                Workbook objWorkbook = new Workbook(Copypath);
                //ImageOrPrintOptions option = new ImageOrPrintOptions();

                //option.ImageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                //option.PrintingPage = PrintingPageType.Default;
                //SheetRender objRender = new SheetRender(objWorkbook.Worksheets[0], option);
                //PrintDocument prtdoc = new PrintDocument();
                //string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName; //获取默认的打印机名
                //objRender.ToPrinter(strDefaultPrinter);
            }

            //logic.PrintSupprotApl_H(OrderType, OrderNo);//改變列印次數
            return File(new FileStream(Copypath, FileMode.Open), "application/ms-excel", "定保憑證.xls");
        }

       
            //ViewBag.RoutineSerOrderType = routineService_H.RoutineSerOrderType;
            //ViewBag.RoutineSerOrderNo = routineService_H.RoutineSerOrderNo;
            //ViewBag.OrderDate = routineService_H.OrderDate;
            //ViewBag.ConfirmedDate = routineService_H.ConfirmedDate;
            //ViewBag.CustomerId = routineService_H.CustomerId;
            //ViewBag.ArrangeMonth = routineService_H.ArrangeMonth;
            //ViewBag.AddressSName = routineService_H.AddressSName;
            //ViewBag.Address = routineService_H.Address;
            //ViewBag.Contact = routineService_H.Contact;
            //ViewBag.TelNo = routineService_H.TelNo;
            //ViewBag.FaxNo = routineService_H.FaxNo;
            //ViewBag.SourceOrderType = routineService_H.SourceOrderType;
            //ViewBag.SourceOrderNo = routineService_H.SourceOrderNo;
            //ViewBag.SourceOrderItemId = routineService_H.SourceOrderItemId;
            //ViewBag.ProductNo = routineService_H.ProductNo;
            //ViewBag.ProductName = routineService_H.ProductName;
            //ViewBag.SerialNo = routineService_H.SerialNo;
            //ViewBag.SaleDate = routineService_H.SaleDate;
            //ViewBag.WarrantySDate = routineService_H.WarrantySDate;
            //ViewBag.WarrantyEDate = routineService_H.WarrantyEDate;
            //ViewBag.Confirmed = routineService_H.Confirmed;
            //ViewBag.Confirmor = routineService_H.Confirmor;
            //ViewBag.Remark = routineService_H.Remark;
            //ViewBag.InternalRemark = routineService_H.InternalRemark;
            //ViewBag.AssignCheck = routineService_H.AssignCheck;
            //ViewBag.AssignDate = routineService_H.AssignDate;
            //ViewBag.RoutineSerMan = routineService_H.RoutineSerMan;
            //ViewBag.NoOfPrints = routineService_H.NoOfPrints;

        /// <summary>
        /// 产生条码图片
        /// </summary>
        /// <param name="BarCode"></param>
        //private void CreateBarCode(string BarCode, bool isShow)
        //{
        //    string path = Server.MapPath(@"~\exports");
        //    BarCodeBuilder builder = new BarCodeBuilder(BarCode, Symbology.Code128);
        //    if (isShow == false)
        //    {
        //        builder.CodeLocation = CodeLocation.None;
        //    }
        //    builder.AutoSize = true;

        //    builder.ImageHeight = 40;
        //    builder.ImageWidth = 70;

        //    builder.Save(path + @"\Barcode.png");
        //}
        //[HttpPost]
        //public JsonResult Add()
        //{
        //    RoutineService_H routineService_H = new RoutineService_H();
        //    routineService_H.Company = "";
        //    routineService_H.UserGroup = "";
        //    routineService_H.Creator = "";
        //    routineService_H.RoutineSerOrderType = Request.Form["RoutineSerOrderType"];
        //    routineService_H.RoutineSerOrderNo = Request.Form["RoutineSerOrderNo"];
        //    routineService_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
        //    routineService_H.ConfirmedDate = Request.Form["ConfirmedDate"];
        //    routineService_H.CustomerId = Request.Form["CustomerId"];
        //    routineService_H.SourceOrderType = Request.Form["SourceOrderType"];
        //    routineService_H.SourceOrderNo = Request.Form["SourceOrderNo"];
        //    routineService_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
        //    routineService_H.ArrangeMonth = Request.Form["ArrangeMonth"];
        //    routineService_H.CustomerId = Request.Form["CustomerId"];
        //    routineService_H.AddressSName = Request.Form["AddressSName"];
        //    routineService_H.Address = Request.Form["Address"];
        //    routineService_H.Contact = Request.Form["Contact"];
        //    routineService_H.TelNo = Request.Form["TelNo"];
        //    routineService_H.FaxNo = Request.Form["FaxNo"];
        //    routineService_H.ProductNo = Request.Form["ProductNo"];
        //    routineService_H.ProductName = Request.Form["ProductName"];
        //    routineService_H.SerialNo = Request.Form["SerialNo"];
        //    routineService_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
        //    routineService_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
        //    routineService_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
        //    routineService_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
        //    routineService_H.Confirmor = "";
        //    routineService_H.Remark = Request.Form["Remark"];
        //    routineService_H.InternalRemark = Request.Form["InternalRemark"];
        //    routineService_H.NoOfPrints = 0;


        //    string strPItemId = Request.Form["strPItemId"];
        //    string strPProcessDate = Request.Form["strPProcessDate"];
        //    string strPProcessExplanation = Request.Form["strPProcessExplanation"];
        //    string strPProcessMan = Request.Form["strPProcessMan"];
        //    string strPRemark = Request.Form["strPRemark"];

        //    string strDItemId = Request.Form["strDItemId"];
        //    string strDProductNo = Request.Form["strDProductNo"];
        //    string strDProductName = Request.Form["strDProductName"];
        //    string strDQTY = Request.Form["strDQTY"];
        //    string strDUnit = Request.Form["strDUnit"];
        //    string strDRemark = Request.Form["strDRemark"];

        //    RoutineService_HLogic logic = new RoutineService_HLogic();

        //    string infomsg = "";
        //    string msg = "";

        //    if (!logic.AddRoutineService_H(routineService_H, strPItemId,
        //             strPProcessDate, strPProcessExplanation, strPProcessMan,
        //             strPRemark, strDItemId, strDProductNo,
        //             strDProductName, strDQTY, strDUnit,
        //             strDRemark, out infomsg))
        //    {
        //        msg = "NO-新增失敗;" + infomsg;
        //    }
        //    else
        //    {
        //        msg = "OK-保存成功";
        //    }

        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult Exit()
        //{
        //    RoutineService_H routineService_H = new RoutineService_H();
        //    routineService_H.Company = "";
        //    routineService_H.UserGroup = "";
        //    routineService_H.Creator = "";
        //    routineService_H.RoutineSerOrderType = Request.Form["RoutineSerOrderType"];
        //    routineService_H.RoutineSerOrderNo = Request.Form["RoutineSerOrderNo"];
        //    routineService_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
        //    routineService_H.ConfirmedDate = Request.Form["ConfirmedDate"];
        //    routineService_H.CustomerId = Request.Form["CustomerId"];
        //    routineService_H.SourceOrderType = Request.Form["SourceOrderType"];
        //    routineService_H.SourceOrderNo = Request.Form["SourceOrderNo"];
        //    routineService_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
        //    routineService_H.ArrangeMonth = Request.Form["ArrangeMonth"];
        //    routineService_H.CustomerId = Request.Form["CustomerId"];
        //    routineService_H.AddressSName = Request.Form["AddressSName"];
        //    routineService_H.Address = Request.Form["Address"];
        //    routineService_H.Contact = Request.Form["Contact"];
        //    routineService_H.TelNo = Request.Form["TelNo"];
        //    routineService_H.FaxNo = Request.Form["FaxNo"];
        //    routineService_H.ProductNo = Request.Form["ProductNo"];
        //    routineService_H.ProductName = Request.Form["ProductName"];
        //    routineService_H.SerialNo = Request.Form["SerialNo"];
        //    routineService_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
        //    routineService_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
        //    routineService_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
        //    routineService_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
        //    routineService_H.Confirmor = "";
        //    routineService_H.Remark = Request.Form["Remark"];
        //    routineService_H.InternalRemark = Request.Form["InternalRemark"];
        //    routineService_H.NoOfPrints = 0;


        //    string strPItemId = Request.Form["strPItemId"];
        //    string strPProcessDate = Request.Form["strPProcessDate"];
        //    string strPProcessExplanation = Request.Form["strPProcessExplanation"];
        //    string strPProcessMan = Request.Form["strPProcessMan"];
        //    string strPRemark = Request.Form["strPRemark"];

        //    string strDItemId = Request.Form["strDItemId"];
        //    string strDProductNo = Request.Form["strDProductNo"];
        //    string strDProductName = Request.Form["strDProductName"];
        //    string strDQTY = Request.Form["strDQTY"];
        //    string strDUnit = Request.Form["strDUnit"];
        //    string strDRemark = Request.Form["strDRemark"];

        //    RoutineService_HLogic logic = new RoutineService_HLogic();

        //    string infomsg = "";
        //    string msg = "";
        //    if (!logic.UpdateRoutineService_H(routineService_H, strPItemId,
        //     strPProcessDate, strPProcessExplanation, strPProcessMan,
        //     strPRemark, strDItemId, strDProductNo,
        //     strDProductName, strDQTY, strDUnit,
        //     strDRemark, out infomsg))
        //    {
        //        msg = "NO-更新失敗;" + infomsg;
        //    }
        //    else
        //    {
        //        msg = "OK-保存成功";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        //刪除
        //public ActionResult Delete(string RoutineSerOrderType, string RoutineSerOrderNo)
        //{
        //    RoutineService_HLogic logic = new RoutineService_HLogic();

        //    RoutineService_H routineService_H = new RoutineService_H();
        //    routineService_H.RoutineSerOrderType = RoutineSerOrderType;
        //    routineService_H.RoutineSerOrderNo = RoutineSerOrderNo;

        //    string msg = "";
        //    if (!logic.DelRoutineService_H(routineService_H, out msg))
        //    {
        //        ViewBag.Msg = "刪除失敗！" + msg;
        //    }

        //    return RedirectToAction("Index", "Main");
        //}

        //public ActionResult Edit(string RoutineSerOrderType, string RoutineSerOrderNo)
        //{
        //    RoutineService_HLogic logic = new RoutineService_HLogic();

        //    RoutineService_H routineService_H = new RoutineService_H();
        //    routineService_H.RoutineSerOrderType = RoutineSerOrderType;
        //    routineService_H.RoutineSerOrderNo = RoutineSerOrderNo;

        //    routineService_H = logic.GetRoutineService_HInfo(routineService_H);

        //    ViewBag.RoutineSerOrderType = routineService_H.RoutineSerOrderType;
        //    ViewBag.RoutineSerOrderNo = routineService_H.RoutineSerOrderNo;
        //    ViewBag.OrderDate = routineService_H.OrderDate;
        //    ViewBag.ConfirmedDate = routineService_H.ConfirmedDate;
        //    ViewBag.CustomerId = routineService_H.CustomerId;
        //    ViewBag.ArrangeMonth = routineService_H.ArrangeMonth;
        //    ViewBag.AddressSName = routineService_H.AddressSName;
        //    ViewBag.Address = routineService_H.Address;
        //    ViewBag.Contact = routineService_H.Contact;
        //    ViewBag.TelNo = routineService_H.TelNo;
        //    ViewBag.FaxNo = routineService_H.FaxNo;
        //    ViewBag.SourceOrderType = routineService_H.SourceOrderType;
        //    ViewBag.SourceOrderNo = routineService_H.SourceOrderNo;
        //    ViewBag.SourceOrderItemId = routineService_H.SourceOrderItemId;
        //    ViewBag.ProductNo = routineService_H.ProductNo;
        //    ViewBag.ProductName = routineService_H.ProductName;
        //    ViewBag.SerialNo = routineService_H.SerialNo;
        //    ViewBag.SaleDate = routineService_H.SaleDate;
        //    ViewBag.WarrantySDate = routineService_H.WarrantySDate;
        //    ViewBag.WarrantyEDate = routineService_H.WarrantyEDate;
        //    ViewBag.Confirmed = routineService_H.Confirmed;
        //    ViewBag.Confirmor = routineService_H.Confirmor;
        //    ViewBag.Remark = routineService_H.Remark;
        //    ViewBag.InternalRemark = routineService_H.InternalRemark;
        //    ViewBag.AssignCheck = routineService_H.AssignCheck;
        //    ViewBag.AssignDate = routineService_H.AssignDate;
        //    ViewBag.RoutineSerMan = routineService_H.RoutineSerMan;
        //    ViewBag.NoOfPrints = routineService_H.NoOfPrints;

        //    RoutineService_ProductDLogic Dlogic = new RoutineService_ProductDLogic();
        //    ViewData["RoutineService_ProductD"] = Dlogic.SearchRoutineService_ProductD(RoutineSerOrderType, RoutineSerOrderNo);

        //    RoutineService_ProcessDLogic Plogic = new RoutineService_ProcessDLogic();
        //    ViewData["RoutineService_ProcessD"] = Plogic.SearchRoutineService_ProcessD(RoutineSerOrderType, RoutineSerOrderNo);

        //    ViewBag.Type = "Edit";
        //    return View("CUR");
        //}

        //public ActionResult Details(string RoutineSerOrderType, string RoutineSerOrderNo)
        //{
        //    RoutineService_HLogic logic = new RoutineService_HLogic();

        //    RoutineService_H routineService_H = new RoutineService_H();
        //    routineService_H.RoutineSerOrderType = RoutineSerOrderType;
        //    routineService_H.RoutineSerOrderNo = RoutineSerOrderNo;

        //    routineService_H = logic.GetRoutineService_HInfo(routineService_H);

        //    ViewBag.RoutineSerOrderType = routineService_H.RoutineSerOrderType;
        //    ViewBag.RoutineSerOrderNo = routineService_H.RoutineSerOrderNo;
        //    ViewBag.OrderDate = routineService_H.OrderDate;
        //    ViewBag.ConfirmedDate = routineService_H.ConfirmedDate;
        //    ViewBag.CustomerId = routineService_H.CustomerId;
        //    ViewBag.ArrangeMonth = routineService_H.ArrangeMonth;
        //    ViewBag.AddressSName = routineService_H.AddressSName;
        //    ViewBag.Address = routineService_H.Address;
        //    ViewBag.Contact = routineService_H.Contact;
        //    ViewBag.TelNo = routineService_H.TelNo;
        //    ViewBag.FaxNo = routineService_H.FaxNo;
        //    ViewBag.SourceOrderType = routineService_H.SourceOrderType;
        //    ViewBag.SourceOrderNo = routineService_H.SourceOrderNo;
        //    ViewBag.SourceOrderItemId = routineService_H.SourceOrderItemId;
        //    ViewBag.ProductNo = routineService_H.ProductNo;
        //    ViewBag.ProductName = routineService_H.ProductName;
        //    ViewBag.SerialNo = routineService_H.SerialNo;
        //    ViewBag.SaleDate = routineService_H.SaleDate;
        //    ViewBag.WarrantySDate = routineService_H.WarrantySDate;
        //    ViewBag.WarrantyEDate = routineService_H.WarrantyEDate;
        //    ViewBag.Confirmed = routineService_H.Confirmed;
        //    ViewBag.Confirmor = routineService_H.Confirmor;
        //    ViewBag.Remark = routineService_H.Remark;
        //    ViewBag.InternalRemark = routineService_H.InternalRemark;
        //    ViewBag.AssignCheck = routineService_H.AssignCheck;
        //    ViewBag.AssignDate = routineService_H.AssignDate;
        //    ViewBag.RoutineSerMan = routineService_H.RoutineSerMan;
        //    ViewBag.NoOfPrints = routineService_H.NoOfPrints;

        //    RoutineService_ProductDLogic Dlogic = new RoutineService_ProductDLogic();
        //    ViewData["RoutineService_ProductD"] = Dlogic.SearchRoutineService_ProductD(RoutineSerOrderType, RoutineSerOrderNo);

        //    RoutineService_ProcessDLogic Plogic = new RoutineService_ProcessDLogic();
        //    ViewData["RoutineService_ProcessD"] = Plogic.SearchRoutineService_ProcessD(RoutineSerOrderType, RoutineSerOrderNo);

        //    ViewBag.Type = "Details";
        //    return View("CUR");
        //}

        //public ActionResult CUR()
        //{
        //    List<RoutineService_ProcessD> objRoutineService_ProcessD = new List<RoutineService_ProcessD>();
        //    ViewData["RoutineService_ProcessD"] = objRoutineService_ProcessD;
        //    List<RoutineService_ProductD> objRoutineService_ProductD = new List<RoutineService_ProductD>();
        //    ViewData["RoutineService_ProductD"] = objRoutineService_ProductD;
        //    ViewBag.NoOfPrints = "0";
        //    return View("CUR");
        //}

        ////通過品名查找
        //public JsonResult SearchProductInfo(string ProductNo)
        //{
        //    ProductLogic logic = new ProductLogic();
        //    Product product = logic.GetProductInfo(ProductNo);
        //    return Json(product, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult SerialNo(string SerialNo)
        //{
        //    Warranty_HLogic logic = new Warranty_HLogic();
        //    return Json(logic.IsSerialNo(SerialNo), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchServiceArrangeSerial(string ProductNo, string SerialNo, int Page)
        //{
        //    ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //    ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //    serviceArrange_H.ProductNo = ProductNo;
        //    serviceArrange_H.SerialNo = SerialNo;
        //    List<ServiceArrange_H> lst = logic.GetServiceArrange_HSerialNo(serviceArrange_H, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult GetOrderTypeName(string OrderType)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    return Json(logic.GetOrderTypeName(OrderType, "D2"), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchOrderType(string OrderType, string OrderSName, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "D2";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchServiceArrange(string SerArrangeOrderType, string SerArrangeOrderNo, int Page)
        //{
        //    ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //    ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //    serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
        //    serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //    List<ServiceArrange_H> lst = logic.GetServiceArrange_H(serviceArrange_H, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        ////通過定保計劃查詢信息
        //public JsonResult SearchServiceArrangeInfo(string SerArrangeOrderType, string SerArrangeOrderNo)
        //{
        //    ServiceArrangeModi_HLogic Hlogic = new ServiceArrangeModi_HLogic();
        //    ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
        //    serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
        //    serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //    serviceArrangeModi_H = Hlogic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

        //    if(serviceArrangeModi_H==null)
        //    {

        //        ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //        ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //        serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
        //        serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //        serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);
        //        return Json(serviceArrange_H, JsonRequestBehavior.AllowGet);
        //    }else
        //    {
        //        return Json(serviceArrangeModi_H, JsonRequestBehavior.AllowGet);
        //    }

        //}

        ////通過定保計劃查詢信息
        //public JsonResult SearchServiceArrangeDInfo(string SerArrangeOrderType, string SerArrangeOrderNo,string Version)
        //{
        //    ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
        //    List<ServiceArrangeModi_D> Dlst = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);
        //    if(Dlst.Count==0)
        //    {
        //        ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
        //        List<ServiceArrange_D> lst = logic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }else
        //    {
        //        return Json(Dlst, JsonRequestBehavior.AllowGet);
        //    }


        //}
    }
}