using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace SERI07.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGAChange_HLogic logic = new RGAChange_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RGAChange_H"] = logic.SearchRGAChange_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            RGAChange_H rGAChange_H = new RGAChange_H();
            rGAChange_H.Company = "";
            rGAChange_H.UserGroup = "";
            rGAChange_H.Creator = "";
            rGAChange_H.RGAType = Request.Form["RGAType"];
            rGAChange_H.RGANo = Request.Form["RGANo"];
            rGAChange_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rGAChange_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            rGAChange_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]==null?"0":Request.Form["NoOfPrints"]);
            rGAChange_H.SourceOrderType = Request.Form["SourceOrderType"];
            rGAChange_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rGAChange_H.Version = Request.Form["Version"];
            rGAChange_H.Repairman = Request.Form["Repairman"];
            rGAChange_H.NewCustomerId = Request.Form["CustomerId"];
            rGAChange_H.NewAddressSName = Request.Form["AddressSName"];
            rGAChange_H.NewAddress = Request.Form["Address"];
            rGAChange_H.NewContact = Request.Form["Contact"];
            rGAChange_H.NewTelNo = Request.Form["TelNo"];
            rGAChange_H.NewFaxNo = Request.Form["FaxNo"];
            rGAChange_H.NewProductNo = Request.Form["ProductNo"];
            rGAChange_H.NewProductName = Request.Form["ProductName"];
            rGAChange_H.NewSerialNo = Request.Form["SerialNo"];
            rGAChange_H.NewAssetNo = Request.Form["AssetNo"];
            rGAChange_H.NewContractType = Request.Form["ContractType"];
            rGAChange_H.NewContractNo = Request.Form["ContractNo"];
            rGAChange_H.NewUnderWarranty = Request.Form["UnderWarranty"];
            rGAChange_H.NewSaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            rGAChange_H.NewPurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
            rGAChange_H.NewWarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
            rGAChange_H.NewWarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            rGAChange_H.NewPCompletionDate = Request.Form["PCompletionDate"].Replace("/","");
            //rGAChange_H.NewCompletionDate = Request.Form["CompletionDate"];
            //rGAChange_H.NewClosed = Request.Form["Closed"];
            rGAChange_H.NewOptionDetail = Request.Form["OptionDetail"];
            rGAChange_H.NewReturned = Request.Form["Returned"]==null?"N":"Y";
            rGAChange_H.NewMalfunctionReason = Request.Form["MalfunctionReason"];
            rGAChange_H.NewTestResult = Request.Form["TestResult"];
            rGAChange_H.NewInternalRemark = Request.Form["InternalRemark"];
            rGAChange_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            rGAChange_H.Confirmor = "";
            rGAChange_H.NewRemark = Request.Form["Remark"];

            //string strQItemId = Request.Form["strQItemId"];
            string strQQuestionNo = Request.Form["strQQuestionNo"];
            string strQQuestion = Request.Form["strQQuestion"];
            string strQDescription = Request.Form["strQDescription"];

            string strDItemId = Request.Form["strDItemId"];
            string strDPartNo = Request.Form["strDPartNo"];
            string strDPartName = Request.Form["strDPartName"];
            string strDQTY = Request.Form["strDQTY"];
            string strDUnit = Request.Form["strDUnit"];
            string strDRemark = Request.Form["strDRemark"];
            string strDRepaired = Request.Form["strDRepaired"];
            string strDSourceOrderType = Request.Form["strDSourceOrderType"];
            string strDSourceOrderNo = Request.Form["strDSourceOrderNo"];
            string strDSourceItemId = Request.Form["strDSourceItemId"];
            //string strDResponseOrderType = Request.Form["strDResponseOrderType"];
            //string strDResponseOrderNo = Request.Form["strDResponseOrderNo"];
            //string strDResponseDate = Request.Form["strDResponseDate"];

            RGAChange_HLogic logic = new RGAChange_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.AddRGAChange_H(rGAChange_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out infomsg))
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
            RGAChange_H rGAChange_H = new RGAChange_H();
            rGAChange_H.Company = "";
            rGAChange_H.UserGroup = "";
            rGAChange_H.Creator = "";
            rGAChange_H.RGAType = Request.Form["RGAType"];
            rGAChange_H.RGANo = Request.Form["RGANo"];
            rGAChange_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            rGAChange_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            rGAChange_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            rGAChange_H.SourceOrderType = Request.Form["SourceOrderType"];
            rGAChange_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            rGAChange_H.Version = Request.Form["Version"];
            rGAChange_H.Repairman = Request.Form["Repairman"];
            rGAChange_H.NewCustomerId = Request.Form["CustomerId"];
            rGAChange_H.NewAddressSName = Request.Form["AddressSName"];
            rGAChange_H.NewAddress = Request.Form["Address"];
            rGAChange_H.NewContact = Request.Form["Contact"];
            rGAChange_H.NewTelNo = Request.Form["TelNo"];
            rGAChange_H.NewFaxNo = Request.Form["FaxNo"];
            rGAChange_H.NewProductNo = Request.Form["ProductNo"];
            rGAChange_H.NewProductName = Request.Form["ProductName"];
            rGAChange_H.NewSerialNo = Request.Form["SerialNo"];
            rGAChange_H.NewAssetNo = Request.Form["AssetNo"];
            rGAChange_H.NewContractType = Request.Form["ContractType"];
            rGAChange_H.NewContractNo = Request.Form["ContractNo"];
            rGAChange_H.NewUnderWarranty = Request.Form["UnderWarranty"];
            rGAChange_H.NewSaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            rGAChange_H.NewPurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
            rGAChange_H.NewWarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
            rGAChange_H.NewWarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            rGAChange_H.NewPCompletionDate = Request.Form["PCompletionDate"];
            //rGAChange_H.NewCompletionDate = Request.Form["CompletionDate"];
            //rGAChange_H.NewClosed = Request.Form["Closed"];
            rGAChange_H.NewOptionDetail = Request.Form["OptionDetail"];
            rGAChange_H.NewReturned = Request.Form["Returned"];
            rGAChange_H.NewMalfunctionReason = Request.Form["MalfunctionReason"];
            rGAChange_H.NewTestResult = Request.Form["TestResult"];
            rGAChange_H.NewInternalRemark = Request.Form["InternalRemark"];
            rGAChange_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            rGAChange_H.Confirmor = "";
            rGAChange_H.NewRemark = Request.Form["Remark"];

            //string strQItemId = Request.Form["strQItemId"];
            string strQQuestionNo = Request.Form["strQQuestionNo"];
            string strQQuestion = Request.Form["strQQuestion"];
            string strQDescription = Request.Form["strQDescription"];

            string strDItemId = Request.Form["strDItemId"];
            string strDPartNo = Request.Form["strDPartNo"];
            string strDPartName = Request.Form["strDPartName"];
            string strDQTY = Request.Form["strDQTY"];
            string strDUnit = Request.Form["strDUnit"];
            string strDRemark = Request.Form["strDRemark"];
            string strDRepaired = Request.Form["strDRepaired"];
            string strDSourceOrderType = Request.Form["strDSourceOrderType"];
            string strDSourceOrderNo = Request.Form["strDSourceOrderNo"];
            string strDSourceItemId = Request.Form["strDSourceItemId"];
            //string strDResponseOrderType = Request.Form["strDResponseOrderType"];
            //string strDResponseOrderNo = Request.Form["strDResponseOrderNo"];
            //string strDResponseDate = Request.Form["strDResponseDate"];

            RGAChange_HLogic logic = new RGAChange_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.UpdateRGAChange_H(rGAChange_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string RGAType, string RGANo,string Version)
        {
            RGAChange_HLogic logic = new RGAChange_HLogic();

            RGAChange_H rGAChange_H = new RGAChange_H();
            rGAChange_H.RGAType = RGAType;
            rGAChange_H.RGANo = RGANo;
            rGAChange_H.Version = Version;

            string msg = "";
            if (!logic.DelRGAChange_H(rGAChange_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string RGAType, string RGANo,string Version)
        {
            RGAChange_HLogic logic = new RGAChange_HLogic();

            RGAChange_H rGAChange_H = new RGAChange_H();
            rGAChange_H.RGAType = RGAType;
            rGAChange_H.RGANo = RGANo;
            rGAChange_H.Version = Version;

            rGAChange_H = logic.GetRGAChange_HInfo(rGAChange_H);

            ViewBag.RGAType = rGAChange_H.RGAType;
            ViewBag.RGANo = rGAChange_H.RGANo;
            ViewBag.OrderDate = rGAChange_H.OrderDate;
            //ViewBag.StatusCode = rGAChange_H.StatusCode;
            ViewBag.NoOfPrints = rGAChange_H.NoOfPrints;
            ViewBag.SourceOrderType = rGAChange_H.SourceOrderType;
            ViewBag.SourceOrderNo = rGAChange_H.SourceOrderNo;
            ViewBag.Version = rGAChange_H.Version;
            ViewBag.Repairman = rGAChange_H.Repairman;
            ViewBag.CustomerId = rGAChange_H.NewCustomerId;
            ViewBag.AddressSName = rGAChange_H.NewAddressSName;
            ViewBag.Address = rGAChange_H.NewAddress;
            ViewBag.Contact = rGAChange_H.NewContact;
            ViewBag.TelNo = rGAChange_H.NewTelNo;
            ViewBag.FaxNo = rGAChange_H.NewFaxNo;
            ViewBag.ProductNo = rGAChange_H.NewProductNo;
            ViewBag.ProductName = rGAChange_H.NewProductName;
            ViewBag.SerialNo = rGAChange_H.NewSerialNo;
            ViewBag.AssetNo = rGAChange_H.NewAssetNo;
            ViewBag.ContractType = rGAChange_H.NewContractType;
            ViewBag.ContractNo = rGAChange_H.NewContractNo;
            ViewBag.UnderWarranty = rGAChange_H.NewUnderWarranty;
            ViewBag.SaleDate = rGAChange_H.NewSaleDate;
            ViewBag.PurchaseDate = rGAChange_H.NewPurchaseDate;
            ViewBag.WarrantyStartDate = rGAChange_H.NewWarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rGAChange_H.NewWarrantyExpiryDate;
            ViewBag.PCompletionDate = rGAChange_H.NewPCompletionDate;
            //ViewBag.CompletionDate = rGAChange_H.NewCompletionDate;
            //ViewBag.Closed = rGAChange_H.NewClosed;
            ViewBag.OptionDetail = rGAChange_H.NewOptionDetail;
            ViewBag.Returned = rGAChange_H.NewReturned;
            ViewBag.MalfunctionReason = rGAChange_H.NewMalfunctionReason;
            ViewBag.TestResult = rGAChange_H.NewTestResult;
            ViewBag.InternalRemark = rGAChange_H.NewInternalRemark;
            ViewBag.Confirmed = rGAChange_H.Confirmed;
            ViewBag.Remark = rGAChange_H.NewRemark;

            RGAChange_QLogic Qlogic = new RGAChange_QLogic();
            RGAChange_Q rGAChange_Q = new RGAChange_Q();
            rGAChange_Q.RGAType = RGAType;
            rGAChange_Q.RGANo = RGANo;
            rGAChange_Q.RGANo = RGANo;
            rGAChange_Q.Version = Version;
            ViewData["RGAChange_Q"] = Qlogic.SearchRGAChange_Q(rGAChange_Q);

            RGAChange_DLogic Dlogic = new RGAChange_DLogic();
            RGAChange_D rGAChange_D = new RGAChange_D();
            rGAChange_D.RGAType = RGAType;
            rGAChange_D.RGANo = RGANo;
            rGAChange_D.Version = Version;
            ViewData["RGAChange_D"] = Dlogic.SearchRGAChange_D(rGAChange_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string RGAType, string RGANo,string Version)
        {
            RGAChange_HLogic logic = new RGAChange_HLogic();

            RGAChange_H rGAChange_H = new RGAChange_H();
            rGAChange_H.RGAType = RGAType;
            rGAChange_H.RGANo = RGANo;
            rGAChange_H.Version = Version;

            rGAChange_H = logic.GetRGAChange_HInfo(rGAChange_H);

            ViewBag.RGAType = rGAChange_H.RGAType;
            ViewBag.RGANo = rGAChange_H.RGANo;
            ViewBag.OrderDate = rGAChange_H.OrderDate;
            //ViewBag.StatusCode = rGAChange_H.StatusCode;
            ViewBag.NoOfPrints = rGAChange_H.NoOfPrints;
            ViewBag.SourceOrderType = rGAChange_H.SourceOrderType;
            ViewBag.SourceOrderNo = rGAChange_H.SourceOrderNo;
            ViewBag.Version = rGAChange_H.Version;
            ViewBag.Repairman = rGAChange_H.Repairman;
            ViewBag.CustomerId = rGAChange_H.NewCustomerId;
            ViewBag.AddressSName = rGAChange_H.NewAddressSName;
            ViewBag.Address = rGAChange_H.NewAddress;
            ViewBag.Contact = rGAChange_H.NewContact;
            ViewBag.TelNo = rGAChange_H.NewTelNo;
            ViewBag.FaxNo = rGAChange_H.NewFaxNo;
            ViewBag.ProductNo = rGAChange_H.NewProductNo;
            ViewBag.ProductName = rGAChange_H.NewProductName;
            ViewBag.SerialNo = rGAChange_H.NewSerialNo;
            ViewBag.AssetNo = rGAChange_H.NewAssetNo;
            ViewBag.ContractType = rGAChange_H.NewContractType;
            ViewBag.ContractNo = rGAChange_H.NewContractNo;
            ViewBag.UnderWarranty = rGAChange_H.NewUnderWarranty;
            ViewBag.SaleDate = rGAChange_H.NewSaleDate;
            ViewBag.PurchaseDate = rGAChange_H.NewPurchaseDate;
            ViewBag.WarrantyStartDate = rGAChange_H.NewWarrantyStartDate;
            ViewBag.WarrantyExpiryDate = rGAChange_H.NewWarrantyExpiryDate;
            ViewBag.PCompletionDate = rGAChange_H.NewPCompletionDate;
            //ViewBag.CompletionDate = rGAChange_H.NewCompletionDate;
            //ViewBag.Closed = rGAChange_H.NewClosed;
            ViewBag.OptionDetail = rGAChange_H.NewOptionDetail;
            ViewBag.Returned = rGAChange_H.NewReturned;
            ViewBag.MalfunctionReason = rGAChange_H.NewMalfunctionReason;
            ViewBag.TestResult = rGAChange_H.NewTestResult;
            ViewBag.InternalRemark = rGAChange_H.NewInternalRemark;
            ViewBag.Confirmed = rGAChange_H.Confirmed;
            ViewBag.Remark = rGAChange_H.NewRemark;

            RGAChange_QLogic Qlogic = new RGAChange_QLogic();
            RGAChange_Q rGAChange_Q = new RGAChange_Q();
            rGAChange_Q.RGAType = RGAType;
            rGAChange_Q.RGANo = RGANo;
            rGAChange_Q.RGANo = RGANo;
            rGAChange_Q.Version = Version;
            ViewData["RGAChange_Q"] = Qlogic.SearchRGAChange_Q(rGAChange_Q);

            RGAChange_DLogic Dlogic = new RGAChange_DLogic();
            RGAChange_D rGAChange_D = new RGAChange_D();
            rGAChange_D.RGAType = RGAType;
            rGAChange_D.RGANo = RGANo;
            rGAChange_D.Version = Version;
            ViewData["RGAChange_D"] = Dlogic.SearchRGAChange_D(rGAChange_D);

            ViewBag.Type = "Details";
            return View("CUR");
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
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單別,單號,客戶名稱,客戶形態,品號,品名,序號,保養月份,保固起日,保固迄日,完工日期");
                    List<RoutineService_H> objRoutineService_H = ViewData["RoutineService_H"] as List<RoutineService_H>;
                    foreach (var obj in objRoutineService_H)
                    {
                        sw.WriteLine(obj.RoutineSerOrderType + "," + obj.RoutineSerOrderNo + ","
                             + obj.CustomerName + "," + obj.CustomerType + ","
                            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + ","
                            + obj.ArrangeMonth + "," + obj.WarrantySDate + "," + obj.WarrantyEDate + "," + obj.ConfirmedDate);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RoutineService.csv");
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
                    value += "," + Request.Form["strSerialNo"];
                }
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
                ViewBag.strRoutineSerOrderNo = Request.Form["strRoutineSerOrderNo"];
                ViewBag.strCustomerId = Request.Form["strCustomerId"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<RGA_Q> objRGA_Q = new List<RGA_Q>();
            ViewData["RGA_Q"] = objRGA_Q;
            List<RGA_D> objRGA_D = new List<RGA_D>();
            ViewData["RGA_D"] = objRGA_D;
            ViewBag.Version = "0000";
            ViewBag.NoOfPrints = "0";
            ViewBag.Confirmed = "N.未處理";
            return View("CUR");
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsPhoneService(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;
            return Json(logic.IsPhoneService_H(phoneService_H), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchServiceArrangeSerial(string ProductNo, string SerialNo, int Page)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.ProductNo = ProductNo;
            serviceArrange_H.SerialNo = SerialNo;
            List<ServiceArrange_H> lst = logic.GetServiceArrange_HSerialNo(serviceArrange_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "B2"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA(string Col, string Condition, string conditionValue, int Page)
        {
            RGA_HLogic logic = new RGA_HLogic();
            List<RGA_H> lst = logic.SearchRGA(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRGAH(string RGAType, string RGANo)
        {
            RGA_HLogic logic = new RGA_HLogic();
            RGA_H rGA_H = new RGA_H();
            rGA_H.RGAType = RGAType;
            rGA_H.RGANo = RGANo;
            return Json(logic.GetRGA_HInfo(rGA_H), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRGAD(string RGAType, string RGANo)
        {
            RGA_DLogic logic = new RGA_DLogic();
            RGA_D rGA_D = new RGA_D();
            rGA_D.RGAType = RGAType;
            rGA_D.RGANo = RGANo;
            List<RGA_D> lst = logic.SearchRGA_D(rGA_D);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRGAQ(string RGAType, string RGANo)
        {
            RGA_QLogic logic = new RGA_QLogic();
            RGA_Q rGA_Q = new RGA_Q();
            rGA_Q.RGAType = RGAType;
            rGA_Q.RGANo = RGANo;
            List<RGA_Q> lst = logic.SearchRGA_Q(rGA_Q);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchServiceArrange(string SerArrangeOrderType, string SerArrangeOrderNo, int Page)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
            List<ServiceArrange_H> lst = logic.GetServiceArrange_H(serviceArrange_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //通過定保計劃查詢信息
        public JsonResult SearchServiceArrangeInfo(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrangeModi_HLogic Hlogic = new ServiceArrangeModi_HLogic();
            ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
            serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrangeModi_H = Hlogic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

            if(serviceArrangeModi_H==null)
            {

                ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
                ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
                serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
                serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
                serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);
                return Json(serviceArrange_H, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(serviceArrangeModi_H, JsonRequestBehavior.AllowGet);
            }

        }

        //通過定保計劃查詢信息
        public JsonResult SearchServiceArrangeDInfo(string SerArrangeOrderType, string SerArrangeOrderNo,string Version)
        {
            ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
            List<ServiceArrangeModi_D> Dlst = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);
            if(Dlst.Count==0)
            {
                ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
                List<ServiceArrange_D> lst = logic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(Dlst, JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}