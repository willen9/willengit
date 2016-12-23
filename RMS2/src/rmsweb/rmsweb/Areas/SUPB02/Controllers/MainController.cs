using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPB02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();
            string col = ",IsPicking";
            string condition = ",=";
            string value = ",N";
            ViewData["SupportApl_H"] = logic.GetSupportItemPick(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];

            }
            else
            {
                if (Request.Form["IsPicking"].Trim() != "ALL")
                {
                    col += ",IsPicking";
                    condition += ",=";
                    value += "," + Request.Form["IsPicking"];
                }
                if (Request.Form["ApplyDate"].Trim() != "")
                {
                    col += ",ApplyDate";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ApplyDate"];
                }
                if (Request.Form["CustomerId"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["CustomerId"];
                }

            }

            ViewBag.selCond1 = Request.Form["selCond1"];
            ViewBag.selCond2 = Request.Form["selCond2"];
            ViewBag.IsPicking = Request.Form["IsPicking"];
            ViewBag.ApplyDate = Request.Form["ApplyDate"];
            ViewBag.CustomerId = Request.Form["CustomerId"];
            ViewData["SupportApl_H"] = logic.GetSupportItemPick(col, condition, value);
            return View();
        }

        //public JsonResult SearchOrderType(string OrderCategoryID, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    //orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = OrderCategoryID;
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

        [HttpPost]
        public JsonResult Issue()
        {
            Picking_H picking_H = new Picking_H();
            picking_H.Company = "";
            picking_H.UserGroup = "";
            picking_H.Creator = "";
            picking_H.PickingOrderType = Request.Form["ordertype"];
            picking_H.PickingDate = Request.Form["requesDate"] == "" ? "" : Request.Form["requesDate"].Replace("/", "");
            picking_H.SupportAplOrderType = Request.Form["SupportAplOrderType"];
            picking_H.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];
            string msg = "";

            Picking_HLogic logic = new Picking_HLogic();

            if (!logic.IssuePicking_H(picking_H))
            {
                msg = "NO-更新失敗";
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CUR()
        {
            if(Request.Form["action"]== "btnAllot")
            {
                SupportApl_HLogic logic = new SupportApl_HLogic();
                string[] OrderNo = logic.GetSupportAplOrderNo(Request.Form["ordertype"]).Split('-');
                ViewBag.PickingOrderType = Request.Form["ordertype"];
                ViewBag.PickingOrderNo = OrderNo[1];

                SupportApl_HLogic Slogic = new SupportApl_HLogic();
                SupportApl_H supportApl_H = Slogic.SupportItemInfo(Request.Form["SupportAplOrderType"], Request.Form["SupportAplOrderNo"]);

                ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
                ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
                ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
                ViewBag.CustomerId = supportApl_H.CustomerId;
                ViewBag.CustomerType = supportApl_H.CustomerType;
                ViewBag.CustomerName = supportApl_H.CustomerName;
                //ViewBag.Sales = supportApl_H.Sales;
                ViewBag.RequestDate = supportApl_H.RequestDate;
                ViewBag.RequestTime = supportApl_H.RequestTime;
                ViewBag.SupportDept = supportApl_H.SupportDept;
                ViewBag.Remark = supportApl_H.Remark;
                ViewBag.PickingDate = Request.Form["requesDate"];
                ViewBag.NoOfPrints = "0";

                SupportApl_ProductDLogic Dlogic = new SupportApl_ProductDLogic();
                ViewData["SupportApl_ProductD"] = Dlogic.GetSupportApl_ProductD(Request.Form["SupportAplOrderType"], Request.Form["SupportAplOrderNo"]);

                return View("CUR");
            }else
            {
                Picking_H picking_H = new Picking_H();
                picking_H.PickingOrderType = Request.Form["AsignOrderType"];            //發料單別
                picking_H.PickingOrderNo = Request.Form["AsignOrderNo"];                //發料單號
                picking_H.OrderDate = Request.Form["orderdate"].Replace("/", "");        //單據日期
                picking_H.SupportAplOrderType = Request.Form["SupportAplOrderType"];    //支援申請單別
                picking_H.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];        //支援申請單號
                picking_H.CustomerId = Request.Form["customerId"];                      //客戶代號
                picking_H.CustomerType = Request.Form["customertype"];                  //客戶型態
                picking_H.Sales = Request.Form["salesname"];                            //業務員
                picking_H.RequestDate = Request.Form["requesDate"];                     //需求日期
                picking_H.RequestTime = Request.Form["id_requesttime"];                 //指定時間
                picking_H.SupportDept = Request.Form["departmentId"];                   //支援單位
                picking_H.PickingDate = Request.Form["pickingdate"];                    //發料日期
                picking_H.PickingMan = Request.Form["pickingman"];                      //發料人員
                picking_H.Remark = Request.Form["remark"];                              //備註

                //品項明細
                string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();                  //品號
                string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();            //品名
                string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();                                    //發料數量
                string strpickingQTY = Request.Form["strpickingQTY"] == null ? "" : Request.Form["strpickingQTY"].ToString();                     //已發數量
                string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();                       //單位
                string strremark = Request.Form["strremark"] == null ? "" : Request.Form["strremark"].ToString();                           //備註

                Picking_HLogic logic = new Picking_HLogic();
                string msg = "";
                if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
                {
                    if (!logic.AddPicking_H(picking_H, strproductno, strproductname, strqty, strpickingQTY, strunit, strremark, out msg))
                    {
                        List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();
                        ViewData["SupportApl_ProductD"] = objSupportApl_ProductD;
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
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public JsonResult Add()
        {
            Picking_H picking_H = new Picking_H();
            picking_H.PickingOrderType = Request.Form["AsignOrderType"];            //發料單別
            picking_H.PickingOrderNo = Request.Form["AsignOrderNo"];                //發料單號
            picking_H.OrderDate = Request.Form["orderdate"].Replace("/", "");        //單據日期
            picking_H.SupportAplOrderType = Request.Form["SupportAplOrderType"];    //支援申請單別
            picking_H.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];        //支援申請單號
            picking_H.CustomerId = Request.Form["customerId"];                      //客戶代號
            picking_H.CustomerType = Request.Form["customerTypeName"].Substring(0,1);                  //客戶型態
            picking_H.Sales = Request.Form["salesname"];                            //業務員
            picking_H.RequestDate = Request.Form["requesDate"];                     //需求日期
            picking_H.RequestTime = Request.Form["id_requesttime"];                 //指定時間
            picking_H.SupportDept = Request.Form["departmentId"];                   //支援單位
            picking_H.PickingDate = Request.Form["pickingdate"];                    //發料日期
            picking_H.PickingMan = Request.Form["pickingman"];                      //發料人員
            picking_H.Remark = Request.Form["remark"];                              //備註

            //品項明細
            //string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();                  //品號
            //string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();            //品名
            //string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();                                    //發料數量
           
            //string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();                       //單位
            //string strremark = Request.Form["strremark"] == null ? "" : Request.Form["strremark"].ToString();                           //備註


            string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();
            string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();
            string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();
            string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();
            string strproductdremark = Request.Form["strproductdremark"] == null ? "" : Request.Form["strproductdremark"].ToString();
            string strpickingQTY = Request.Form["strPickingQTY"] == null ? "" : Request.Form["strPickingQTY"].ToString();                     //已發數量

            Picking_HLogic logic = new Picking_HLogic();
            string infomsg = "";
            string msg = "";
            if (!logic.AddPicking_H(picking_H, strproductno, strproductname, strqty, strpickingQTY, strunit, strproductdremark, out infomsg))
            {
                msg = "NO-新增失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
            ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
            ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
            ViewBag.OrderDate = supportApl_H.OrderDate == "" ? "" : DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.ApplyDate = supportApl_H.ApplyDate;
            ViewBag.CustomerId = supportApl_H.CustomerId;
            ViewBag.CustomerName = supportApl_H.CustomerName;
            ViewBag.CustomerType = supportApl_H.CustomerType;
            ViewBag.CustomerTypeName = supportApl_H.CustomerType == "A" ? "A:一般客戶" : "B:經銷商";
            ViewBag.SalesId = supportApl_H.SalesId;
            ViewBag.AddressSName = supportApl_H.AddressSName;
            ViewBag.Address = supportApl_H.Address;
            ViewBag.TelNo = supportApl_H.TelNo;
            ViewBag.FaxNo = supportApl_H.FaxNo;
            ViewBag.Remark = supportApl_H.Remark;
            ViewBag.NoOfPrints = supportApl_H.NoOfPrints;
            ViewBag.Picking = supportApl_H.Picking;
            ViewBag.IsPicking = supportApl_H.IsPicking;
            ViewBag.RequestDate = supportApl_H.RequestDate == "" ? "" : DateTime.ParseExact(supportApl_H.RequestDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.RequestTime = supportApl_H.RequestTime;
            ViewBag.SupportDept = supportApl_H.SupportDept;
            ViewBag.SourceOrderNo = supportApl_H.SourceOrderNo;
            ViewBag.AsignDate = supportApl_H.AsignDate == "" ? "" : DateTime.ParseExact(supportApl_H.AsignDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.AsignMan = supportApl_H.AsignMan;
            ViewBag.ProcessMan = supportApl_H.ProcessMan;
            ViewBag.CompletionDate = supportApl_H.CompletionDate == "" ? "" : DateTime.ParseExact(supportApl_H.CompletionDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.CompletionMan = supportApl_H.CompletionMan;
            ViewBag.OrderStatus = supportApl_H.OrderStatus;
            ViewBag.Confirmed = supportApl_H.Confirmed;
            ViewBag.Contact = supportApl_H.Contact;

            SupportApl_ProcessDLogic processLogic = new SupportApl_ProcessDLogic();
            ViewData["SupportApl_ProcessD"] = processLogic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);

            SupportApl_ProductDLogic productLogic = new SupportApl_ProductDLogic();
            ViewData["SupportApl_ProductD"] = productLogic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);

            SupportApl_SupportItemLogic supportLogic = new SupportApl_SupportItemLogic();
            ViewData["SupportApl_SupportItem"] = supportLogic.GetSupportApl_SupportItem(SupportAplOrderType, SupportAplOrderNo);

            ViewBag.Type = "Details";
            return View("Show");
        }
    }
}