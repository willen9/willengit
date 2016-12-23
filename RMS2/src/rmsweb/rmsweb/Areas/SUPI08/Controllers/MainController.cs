using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPI08.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["SupportApl_H"] = logic.GetSupportItemProcessD(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
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
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col = ",SupportAplOrderNo";
                    condition = "," + Request.Form["selCond1"];
                    value = "," + Request.Form["supportaplorderno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col = ",h.CustomerId";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["customerid"];
                }
                if (Request.Form["ProcessMan"].Trim() != "")
                {
                    col = ",h.ProcessMan";
                    condition = "," + Request.Form["selCond3"];
                    value = "," + Request.Form["ProcessMan"];
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
                ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                ViewBag.CustomerId = Request.Form["customerid"];
                ViewBag.ProcessMan = Request.Form["ProcessMan"];

            }
            ViewData["SupportApl_H"] = logic.GetSupportItemProcessD(col, condition, value);
            return View();
        }

        public ActionResult Edit(string SupportAplOrderType, string SupportAplOrderNo)
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
            ViewBag.CustomerTypeName = supportApl_H.CustomerType;
            ViewBag.SalesId = supportApl_H.SalesId;
            ViewBag.SalesName = supportApl_H.SalesName;
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
            //ViewBag.AsignDate = supportApl_H.AsignDate;
            //ViewBag.AsignMan = supportApl_H.AsignMan;
            //ViewBag.ProcessMan = supportApl_H.ProcessMan;
            ViewBag.CompletionDate = supportApl_H.CompletionDate;
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

            //查看派工資料信息，已確認
            AssignmentLogic assLogic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.SourceOrderType = SupportAplOrderType;
            assignment.SourceOrderNo = SupportAplOrderNo;
            assignment.Confirmed = "Y";
            assignment = assLogic.GetAssignmentInfo(assignment);
            ViewBag.AsignDate = assignment.AssignDate;
            ViewBag.Assignor = assignment.Assignor;
            ViewBag.AssignorName = assignment.AssignorName;
            ViewBag.Designee = assignment.Designee;
            ViewBag.DesigneeName = assignment.DesigneeName;

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Exit()
        {
            SupportApl_H supportapl_h = new SupportApl_H();
            supportapl_h.SupportAplOrderType = Request.Form["ordertype"];
            supportapl_h.SupportAplOrderNo = Request.Form["supportaplorderno"];
            supportapl_h.OrderDate = Request.Form["orderdate"].ToString().Replace("/", "");
            //supportapl_h.ApplyDate = Request.Form[""].ToString();
            supportapl_h.CustomerId = Request.Form["customerId"];
            supportapl_h.CustomerType = Request.Form["customertype"] == "" ? "" : Request.Form["customertype"].Substring(0, 1);
            supportapl_h.SalesId = Request.Form["sales"];
            supportapl_h.AddressSName = Request.Form["address1"];
            supportapl_h.Address = Request.Form["address2"];
            supportapl_h.Contact = Request.Form["contact"];
            supportapl_h.TelNo = Request.Form["telno"];
            supportapl_h.FaxNo = Request.Form["faxno"];
            supportapl_h.Remark = Request.Form["remark"];
            supportapl_h.NoOfPrints = int.Parse(Request.Form["nootprints"].ToString() == "" ? "0" : Request.Form["nootprints"].ToString());
            supportapl_h.Picking = Request.Form["picking"] == null ? "N" : "Y";
            supportapl_h.IsPicking = "N";
            supportapl_h.RequestDate = Request.Form["requesDate"].ToString() == "" ? "" : Request.Form["requesDate"].ToString().Replace("/", "");
            supportapl_h.RequestTime = Request.Form["requesttime"];
            supportapl_h.SupportDept = Request.Form["departmentId"];
            supportapl_h.SourceOrderNo = Request.Form["sourceorderno"];
            supportapl_h.AsignDate = Request.Form["asigndate"].ToString().Replace("/", "");
            supportapl_h.AsignMan = Request.Form["asignman"];
            supportapl_h.ProcessMan = Request.Form["processman"];
            supportapl_h.CompletionDate = Request.Form["completiondate"].ToString().Replace("/", "");
            supportapl_h.CompletionMan = Request.Form["completionman"];
            supportapl_h.OrderStatus = "0";
            supportapl_h.Contact = Request.Form["contact"];
            supportapl_h.Company = Session["Company"].ToString();
            supportapl_h.UserGroup = Session["UserGroup"].ToString();
            supportapl_h.Modifier = Session["UserId"].ToString();

            supportapl_h.Confirmed = "N";
            supportapl_h.AsignCheck = "N";

            string strsupportitemid = Request.Form["strsupportitemid"] == null ? "" : Request.Form["strsupportitemid"].ToString();
            string strsupportitemname = Request.Form["strsupportitemname"] == null ? "" : Request.Form["strsupportitemname"].ToString();
            string strremark = Request.Form["strremark"] == null ? "" : Request.Form["strremark"].ToString();

            //string stritemid = Request.Form["stritemid"].ToString();
            string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();
            string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();
            string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();
            string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();
            string strproductdremark = Request.Form["strproductdremark"] == null ? "" : Request.Form["strproductdremark"].ToString();

            string strProcessDate = Request.Form["strPSProcessDate"] == null ? "" : Request.Form["strPSProcessDate"].ToString();
            string strProcessExplanation = Request.Form["strPSProcessExplanation"] == null ? "" : Request.Form["strPSProcessExplanation"].ToString();
            string strRemark = Request.Form["strPSRemark"] == null ? "" : Request.Form["strPSRemark"].ToString();

            string msg = "";

            SupportApl_HLogic logic = new SupportApl_HLogic();

            if (!logic.UpdateSupportApl_H(supportapl_h, strsupportitemid, strsupportitemname,
                    strremark, strproductno, strproductname, strqty, strunit, strproductdremark,
                    strProcessDate, strProcessExplanation, strRemark, out msg))
            {
                msg = "NO-更新失敗";
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}