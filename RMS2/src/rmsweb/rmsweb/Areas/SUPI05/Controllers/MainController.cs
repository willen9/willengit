using Aspose.Cells;
using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace SUPI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportapl_h = new SupportApl_H();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);
            ViewBag.OrderStatusZero = logic.countOrderStatus(supportapl_h, col, condition, value,"0");
            ViewBag.OrderStatusOne = logic.countOrderStatus(supportapl_h, col, condition, value, "1");
            ViewBag.OrderStatusTwo = logic.countOrderStatus(supportapl_h, col, condition, value, "2");
            return View();
        }

        /*
         * 進階查詢
         * 匯出
         * 簡單查詢
         */
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
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);

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
                //    sw.WriteLine("單別,單號,客戶簡稱,支援單位,支援項目,需求日期,狀態,處理人員,業務員,建立人員,發料,來源單號,客戶形態");
                //    List<SupportApl_H> objSupportApl_H = ViewData["SupportApl_H"] as List<SupportApl_H>;
                //    string customerType = "";
                //    string orderStatus="";
                //    foreach (var obj in objSupportApl_H)
                //    {
                //        if (obj.CustomerType == "A")
                //        {
                //            customerType = "一般客戶";
                //        }
                //        else
                //        {
                //            customerType = "經銷商";
                //        }

                //        if (obj.OrderStatus == "0")
                //        {
                //            orderStatus = "未處理";
                //        }
                //        else if (obj.OrderStatus == "1")
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

                //        sw.WriteLine(obj.SupportAplOrderType + "," + obj.SupportAplOrderNo +
                //            "," + obj.CustomerName + "," + obj.SupportDept + "," +
                //            obj.SupportItemAll + "," + obj.RequestDate + "," +
                //            orderStatus + "," + obj.ProcessManName + "," +
                //            obj.AsignManName + "," + obj.CreatorName + "," +
                //            obj.IsPicking + "," + obj.SourceOrderNo + "," + customerType);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                //ViewBag.selCond1 = Request.Form["selCond1"];
                //ViewBag.selCond2 = Request.Form["selCond2"];
                //ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                //ViewBag.CustomerId = Request.Form["customerid"];

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "SupportApl_H.csv");
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
                    //單別,單號,客戶簡稱,支援單位,支援項目,需求日期,狀態,處理人員,業務員,建立人員,發料,來源單號,客戶形態
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
                    //string customerType = "";
                    //string orderStatus = "";
                    int i = 0;
                    foreach (var obj in objSupportApl_H)
                    {
                        //if (obj.CustomerType == "A")
                        //{
                        //    customerType = "一般客戶";
                        //}
                        //else
                        //{
                        //    customerType = "經銷商";
                        //}

                        //if (obj.OrderStatus == "0")
                        //{
                        //    orderStatus = "未處理";
                        //}
                        //else if (obj.OrderStatus == "1")
                        //{
                        //    orderStatus = "已派工";
                        //}
                        //else if (obj.OrderStatus == "2")
                        //{
                        //    orderStatus = "完工";
                        //}
                        //else
                        //{
                        //    orderStatus = "作廢";
                        //}
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SupportAplOrderType);
                        row.CreateCell(1).SetCellValue(obj.SupportAplOrderNo);
                        row.CreateCell(2).SetCellValue(obj.CustomerName);
                        row.CreateCell(3).SetCellValue(obj.SupportDept);
                        row.CreateCell(4).SetCellValue(obj.SupportItemAll);
                        row.CreateCell(5).SetCellValue(obj.RequestDate);
                        row.CreateCell(6).SetCellValue(obj.OrderStatus);
                        row.CreateCell(7).SetCellValue(obj.ProcessManName);
                        row.CreateCell(8).SetCellValue(obj.AsignManName);
                        row.CreateCell(9).SetCellValue(obj.CreatorName);
                        row.CreateCell(10).SetCellValue(obj.IsPicking);
                        row.CreateCell(11).SetCellValue(obj.SourceOrderNo);
                        row.CreateCell(12).SetCellValue(obj.CustomerType);
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
                ViewBag.CustomerId = Request.Form["customerid"];

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "SupportApl_H.xls");
            }
            else
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col += ",SupportAplOrderNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["supportaplorderno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SupportAplOrderNo = Request.Form["supportaplorderno"];
                ViewBag.CustomerId = Request.Form["customerid"];

            }
            ViewData["SupportApl_H"] = logic.GetSupportItem(supportapl_h, col, condition, value);
            ViewBag.OrderStatusZero = logic.countOrderStatus(supportapl_h, col, condition, value, "0");
            ViewBag.OrderStatusOne = logic.countOrderStatus(supportapl_h, col, condition, value, "1");
            ViewBag.OrderStatusTwo = logic.countOrderStatus(supportapl_h, col, condition, value, "2");
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            List<SupportApl_SupportItem> objSupportApl_SupportItem = new List<SupportApl_SupportItem>();
            ViewData["SupportApl_SupportItem"] = objSupportApl_SupportItem;
            List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();
            ViewData["SupportApl_ProductD"] = objSupportApl_ProductD;
            List<SupportApl_ProcessD> objSupportApl_ProcessD = new List<SupportApl_ProcessD>();
            ViewData["SupportApl_ProcessD"] = objSupportApl_ProcessD;
            ViewBag.NoOfPrints = "0";
            ViewBag.OrderStatus = "0:未處理";
            return View("CUR");
        }

        public ActionResult Tree(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
            ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
            ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
            ViewBag.OrderDate = supportApl_H.OrderDate == "" ? "" : DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.CustomerId = supportApl_H.CustomerId;
            ViewBag.CustomerName = supportApl_H.CustomerName;
            ViewBag.OrderStatus = supportApl_H.OrderStatus;


            AssignmentLogic assignmentLogic = new AssignmentLogic();
            Assignment assignment = new Assignment();
            assignment.SourceOrderType = SupportAplOrderType;
            assignment.SourceOrderNo = SupportAplOrderType;
            ViewData["Assignment"] = assignmentLogic.AssignmentInfo(assignment);
            //ViewBag.AssignOrderType = assignment.AssignOrderType;
            //ViewBag.AssignOrderNo = assignment.AssignOrderNo;
            //ViewBag.Version = assignment.Version;

            Picking_HLogic picking_HLogic = new Picking_HLogic();
            ViewData["Picking_H"] = picking_HLogic.GetInfo(SupportAplOrderType, SupportAplOrderNo);
            //ViewBag.PickingOrderType = picking_H.PickingOrderType;
            //ViewBag.PickingOrderNo = picking_H.PickingOrderNo;

            return View("Tree");
        }

        /*
         * 新增
         * 編輯
         */
        [HttpPost]
        public ActionResult CUR(string s)
        {
            SupportApl_H supportapl_h = new SupportApl_H();
            supportapl_h.SupportAplOrderType = Request.Form["ordertype"];
            supportapl_h.SupportAplOrderNo = Request.Form["supportaplorderno"];
            supportapl_h.OrderDate = Request.Form["orderdate"].ToString().Replace("/","");
            //supportapl_h.ApplyDate = Request.Form[""].ToString();
            supportapl_h.CustomerId = Request.Form["customerId"];
            supportapl_h.CustomerType = Request.Form["customertype"];
            supportapl_h.SalesId = Request.Form["sales"];
            supportapl_h.AddressSName = Request.Form["address1"];
            supportapl_h.Address = Request.Form["address2"];
            supportapl_h.Contact = Request.Form["contact"];
            supportapl_h.TelNo = Request.Form["telno"];
            supportapl_h.FaxNo = Request.Form["faxno"];
            supportapl_h.Remark = Request.Form["remark"];
            supportapl_h.NoOfPrints = int.Parse(Request.Form["nootprints"].ToString() == "" ? "0" : Request.Form["nootprints"].ToString());
            supportapl_h.Picking = Request.Form["picking"]==null?"N":"Y";
            supportapl_h.IsPicking = "N";
            supportapl_h.RequestDate = Request.Form["requesDate"].ToString()==""?"":Request.Form["requesDate"].ToString().Replace("/", "");
            supportapl_h.RequestTime = Request.Form["requesttime"];
            supportapl_h.SupportDept = Request.Form["departmentId"];
            supportapl_h.SourceOrderNo = Request.Form["sourceorderno"];
            supportapl_h.AsignDate = Request.Form["asigndate"].ToString().Replace("/", "");
            supportapl_h.AsignMan = Request.Form["asignman"];
            supportapl_h.ProcessMan = Request.Form["processman"];
            supportapl_h.CompletionDate = Request.Form["completiondate"].ToString().Replace("/", "");
            supportapl_h.CompletionMan = Request.Form["completionman"];
            supportapl_h.OrderStatus = Request.Form["orderstatusId"];
            supportapl_h.Contact = Request.Form["contact"];
            supportapl_h.Company = "";
            supportapl_h.UserGroup = "";
            supportapl_h.Creator = "";
            supportapl_h.Modifier = "";


            if (Request.Form["orderstatusId"]=="0")
            {
                supportapl_h.Confirmed = "N";
                supportapl_h.AsignCheck = "N";
            }
            else if (Request.Form["orderstatusId"] == "1")
            {
                supportapl_h.Confirmed = "N";
                supportapl_h.AsignCheck = "Y";
            }
            else if (Request.Form["orderstatusId"]== "2")
            {
                supportapl_h.Confirmed = "Y";
                supportapl_h.AsignCheck = "Y";
            }
            else
            {
                supportapl_h.Confirmed = "V";
                supportapl_h.AsignCheck = "";
            }

            string strsupportitemid = Request.Form["strsupportitemid"] == null ? "" : Request.Form["strsupportitemid"].ToString();
            string strsupportitemname = Request.Form["strsupportitemname"]== null ? "" : Request.Form["strsupportitemname"].ToString();
            string strremark = Request.Form["strremark"]== null ? "" : Request.Form["strremark"].ToString();

            //string stritemid = Request.Form["stritemid"].ToString();
            string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();
            string strproductname = Request.Form["strproductname"]== null ? "" : Request.Form["strproductname"].ToString();
            string strqty = Request.Form["strqty"]== null ? "" : Request.Form["strqty"].ToString();
            string strunit = Request.Form["strunit"]== null ? "" : Request.Form["strunit"].ToString();
            string strproductdremark = Request.Form["strproductdremark"] == null ? "" : Request.Form["strproductdremark"].ToString();

            string strProcessDate = Request.Form["strProcessDate"] == null ? "" : Request.Form["strProcessDate"].ToString();
            string strProcessExplanation = Request.Form["strProcessExplanation"] == null ? "" : Request.Form["strProcessExplanation"].ToString();
            string strRemark = Request.Form["strSRemark"] == null ? "" : Request.Form["strSRemark"].ToString();

            string msg = "";

            SupportApl_HLogic logic = new SupportApl_HLogic();

            if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
            {
                if (!logic.AddSupportApl_H(supportapl_h, strsupportitemid, strsupportitemname, 
                    strremark, strproductno, strproductname, strqty, strunit, strproductdremark, 
                    strProcessDate, strProcessExplanation, strRemark, out msg))
                {
                    List<SupportApl_SupportItem> objSupportApl_SupportItem = new List<SupportApl_SupportItem>();
                    ViewData["SupportApl_SupportItem"] = objSupportApl_SupportItem;
                    List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();
                    ViewData["SupportApl_ProductD"] = objSupportApl_ProductD;
                    List<SupportApl_ProcessD> objSupportApl_ProcessD = new List<SupportApl_ProcessD>();
                    ViewData["SupportApl_ProcessD"] = objSupportApl_ProcessD;
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
            //else if (Request.Form["action"] == "ConfirmedYDetails")
            //{
            //    logic.UpdateConfirmedY(supportapl_h);
            //}
            //else if (Request.Form["action"] == "ConfirmedNDetails")
            //{
            //    logic.UpdateConfirmedV(supportapl_h);
            //}
            else
            {
                SupportApl_H supportApl_H = logic.SupportItemInfo(Request.Form["ordertype"], Request.Form["supportaplorderno"]);
                ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
                ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
                ViewBag.OrderDate = supportApl_H.OrderDate == "" ? "" : DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                ViewBag.ApplyDate = supportApl_H.ApplyDate;
                ViewBag.CustomerId = supportApl_H.CustomerId;
                ViewBag.CustomerType = supportApl_H.CustomerType;
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
                ViewBag.AsignDate = supportApl_H.AsignDate;
                ViewBag.AsignMan = supportApl_H.AsignMan;
                ViewBag.ProcessMan = supportApl_H.ProcessMan;
                ViewBag.CompletionDate = supportApl_H.CompletionDate;
                ViewBag.CompletionMan = supportApl_H.CompletionMan;
                ViewBag.OrderStatus = supportApl_H.OrderStatus;
                ViewBag.Confirmed = supportApl_H.Confirmed;
                ViewBag.Contact = supportApl_H.Contact;

                SupportApl_ProcessDLogic processLogic = new SupportApl_ProcessDLogic();
                ViewData["SupportApl_ProcessD"] = processLogic.GetSupportApl_ProductD(Request.Form["ordertype"], Request.Form["supportaplorderno"]);

                SupportApl_ProductDLogic productLogic = new SupportApl_ProductDLogic();
                ViewData["SupportApl_ProductD"] = productLogic.GetSupportApl_ProductD(Request.Form["ordertype"], Request.Form["supportaplorderno"]);

                SupportApl_SupportItemLogic supportLogic = new SupportApl_SupportItemLogic();
                ViewData["SupportApl_SupportItem"] = supportLogic.GetSupportApl_SupportItem(Request.Form["ordertype"], Request.Form["supportaplorderno"]);

                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }

            //return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Add()
        {
            SupportApl_H supportapl_h = new SupportApl_H();
            supportapl_h.SupportAplOrderType = Request.Form["ordertype"];
            supportapl_h.SupportAplOrderNo = Request.Form["supportaplorderno"];
            supportapl_h.OrderDate = Request.Form["orderdate"].ToString().Replace("/", "");
            //supportapl_h.ApplyDate = Request.Form[""].ToString();
            supportapl_h.CustomerId = Request.Form["customerId"];
            supportapl_h.CustomerType = Request.Form["customertype"]==""?"":Request.Form["customertype"].Substring(0,1);
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
            supportapl_h.Company = "";
            supportapl_h.UserGroup = "";
            supportapl_h.Creator = "";

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
            string infomsg = "";
            SupportApl_HLogic logic = new SupportApl_HLogic();

            if (!logic.AddSupportApl_H(supportapl_h, strsupportitemid, strsupportitemname,
                    strremark, strproductno, strproductname, strqty, strunit, strproductdremark,
                    strProcessDate, strProcessExplanation, strRemark, out infomsg))
            {
                msg = "NO-新增失敗！"+ infomsg;
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
            supportapl_h.Company = "";
            supportapl_h.UserGroup = "";
            supportapl_h.Modifier = "";

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

        public ActionResult Confirmed(string SupportAplOrderType, string SupportAplOrderNo,string type)
        {
            SupportApl_H supportapl_h = new SupportApl_H();
            supportapl_h.SupportAplOrderType = SupportAplOrderType;
            supportapl_h.SupportAplOrderNo = SupportAplOrderNo;
            supportapl_h.Company = Session["Company"].ToString();
            supportapl_h.UserGroup = Session["UserGroup"].ToString();
            supportapl_h.Modifier = Session["UserId"].ToString();
            SupportApl_HLogic logic = new SupportApl_HLogic();
            logic.UpdateConfirmedY(supportapl_h, type.ToUpper());
            return RedirectToAction("Index");
        }

        //刪除
        public ActionResult Delete(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();

            if(!logic.DeleteSupportApl_H(SupportAplOrderType, SupportAplOrderNo))
            {
                ViewBag.Msg = "刪除失敗！";
            }

            return RedirectToAction("Index", "Main");
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
            ViewBag.SalesName= supportApl_H.SalesName;
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

        public ActionResult Copy(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
            //ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
            //ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
            ViewBag.OrderDate = supportApl_H.OrderDate == "" ? "" : DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.ApplyDate = supportApl_H.ApplyDate;
            ViewBag.CustomerId = supportApl_H.CustomerId;
            ViewBag.CustomerName = supportApl_H.CustomerName;
            ViewBag.CustomerType = supportApl_H.CustomerType;
            if (supportApl_H.CustomerType != "")
            {
                ViewBag.CustomerTypeName = supportApl_H.CustomerType == "A" ? "A:一般客戶" : "B:經銷商";
            }
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
            ViewBag.AsignDate = "";
            ViewBag.AsignMan = "";
            ViewBag.ProcessMan = "";
            ViewBag.CompletionDate = "";
            ViewBag.CompletionMan = "";
            ViewBag.OrderStatus = "0:未處理";
            ViewBag.Confirmed = supportApl_H.Confirmed;
            ViewBag.Contact = supportApl_H.Contact;

            //SupportApl_ProcessDLogic processLogic = new SupportApl_ProcessDLogic();
            //ViewData["SupportApl_ProcessD"] = processLogic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);

            List<SupportApl_ProcessD> objSupportApl_ProcessD = new List<SupportApl_ProcessD>();
            ViewData["SupportApl_ProcessD"] = objSupportApl_ProcessD;

            SupportApl_ProductDLogic productLogic = new SupportApl_ProductDLogic();
            ViewData["SupportApl_ProductD"] = productLogic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);

            SupportApl_SupportItemLogic supportLogic = new SupportApl_SupportItemLogic();
            ViewData["SupportApl_SupportItem"] = supportLogic.GetSupportApl_SupportItem(SupportAplOrderType, SupportAplOrderNo);

            return View("CUR");
        }

        public ActionResult Print(string SupportAplOrderType, string SupportAplOrderNo)
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

            Copypath = Server.MapPath(@"~\ExpotFile") + @"\" + strGUID + ".xls";

            System.IO.File.Copy(fileName, Copypath);


            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                ms = new MemoryStream();

                workbook = new Workbook(fs);
                sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;

                SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);

                cells[5, 1].PutValue(supportApl_H.ApplyDate);//申請日期
                cells[5, 4].PutValue(supportApl_H.RequestDate);//需求日期
                cells[5, 7].PutValue(supportApl_H.SupportAplOrderNo);//申請單號
                cells[6, 1].PutValue(supportApl_H.RequestTime);//指定日期
                cells[7, 1].PutValue(supportApl_H.CustomerId);//客戶編號
                cells[7, 4].PutValue(supportApl_H.CustomerName);//客戶名稱
                cells[8, 1].PutValue(supportApl_H.SalesName );//聯絡人
                cells[8, 5].PutValue(supportApl_H.TelNo);//電話
                cells[9, 1].PutValue(supportApl_H.FaxNo);//傳真
                cells[9, 5].PutValue(supportApl_H.AsignManName);//業務人員
                cells[10, 1].PutValue(supportApl_H.AddressSName);//地址
                cells[11, 1].PutValue("");//客戶環境
                //客戶屬性
                if(supportApl_H.CustomerType=="A")
                {
                    cells[11, 5].PutValue("一般客戶");
                }
                if(supportApl_H.CustomerType=="B")
                {
                    cells[11, 5].PutValue("經銷商");
                }
                   
                cells[12, 1].PutValue("");//需求項目

                SupportApl_ProductDLogic productLogic = new SupportApl_ProductDLogic();
                ViewData["SupportApl_ProductD"] = productLogic.GetSupportApl_ProductD(SupportAplOrderType, SupportAplOrderNo);
                List<SupportApl_ProductD> objSupportApl_ProductD = ViewData["SupportApl_ProductD"] as List<SupportApl_ProductD>;

                int index = 16;
                foreach (var obj in objSupportApl_ProductD)
                {
                    cells[index, 1].PutValue(obj.ProductName);//商品
                    cells[index, 6].PutValue(obj.QTY);//數量
                    index++;
                    cells[index, 1].PutValue("");//序號
                    index++;
                    cells[index, 1].PutValue("");//介面
                    cells[index, 6].PutValue("");//軟體
                    index++;
                    cells[index, 1].PutValue(obj.Remark);//備註
                    index++;
                    cells[index, 1].PutValue(obj.Unit);//耗材規格
                    index=index+2;
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
            logic.PrintSupprotApl_H(SupportAplOrderType, SupportAplOrderNo);
            return File(new FileStream(Copypath, FileMode.Open), "application/ms-excel", "申請單.xls");
        }

        public ActionResult Details(string SupportAplOrderType, string SupportAplOrderNo)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
            ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
            ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
            ViewBag.OrderDate = supportApl_H.OrderDate==""?"":DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            ViewBag.ApplyDate = supportApl_H.ApplyDate;
            ViewBag.CustomerId = supportApl_H.CustomerId;
            ViewBag.CustomerName = supportApl_H.CustomerName;
            ViewBag.CustomerType = supportApl_H.CustomerType;
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
            //ViewBag.AsignDate = supportApl_H.AsignDate == "" ? "" : DateTime.ParseExact(supportApl_H.AsignDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); 
            //ViewBag.AsignMan = supportApl_H.AsignMan;
            //ViewBag.ProcessMan = supportApl_H.ProcessMan;
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

            ViewBag.Type = "Details";
            return View("CUR");
        }

        //public JsonResult SearchOrderType(string OrderCategory, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    //orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = OrderCategory;
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult GetOrderTypeName(string OrderType)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    return Json(logic.GetOrderTypeName(OrderType,"A1"), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetOrderTypeNo(string OrderType)
        {
            SupportApl_HLogic logic = new SupportApl_HLogic();
            return Json(logic.GetSupportAplOrderNo(OrderType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountSysc(string CodeType, string CodeNumber)
        {
            SyscodeNumbersLogic logic = new SyscodeNumbersLogic();
            return Json(logic.CountSysc(CodeType, CodeNumber), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchCustomer(string CustomerId, string Col, string Condition, string conditionValue, int Page)
        //{
        //    CustomerLogic logic = new CustomerLogic();
        //    Customer customer = new Customer();
        //    customer.CustomerId = CustomerId;
        //    //customer.CustomerName = CustomerName;
        //    List<Customer> lst = logic.GetCustomer(customer,"","","", Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        

        public JsonResult SearchDepartment(string DepartmentId, string Col, string Condition, string conditionValue, int Page)
        {
            DepartmenLogic logic = new DepartmenLogic();
            Department department = new Department();
            department.DepartmentId = DepartmentId;
            //department.DepartmentName = DepartmentName;
            List<Department> lst = logic.GetDepartment(department, Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchCustomerContact(string Col, string Condition, string conditionValue, int Page)
        //{
        //    CustomerContactLogic logic = new CustomerContactLogic();
        //    //CustomerContact customercontact = new CustomerContact();
        //    //customercontact.CustomerId = CustomerId;
        //    //customercontact.ContactId = ContactId;
        //    //customercontact.Contact = Contact;
        //    List<CustomerContact> lst = logic.GetCustomerContact(Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchSupportItem(string SupportItemId, string SupportItemName, int Page)
        {
            SupportItemLogic logic = new SupportItemLogic();
            SupportItem supportItem = new SupportItem();
            supportItem.SupportItemId = SupportItemId;
            supportItem.SupportItemName = SupportItemName;
            List<SupportItem> lst = logic.GetSupportItem(supportItem, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchProduct(string ProductNo, string ProductName, int Page)
        //{
        //    ProductLogic logic = new ProductLogic();
        //    Product product = new Product();
        //    product.ProductNo = ProductNo;
        //    product.ProductName = ProductName;
        //    List<Product> lst = logic.GetProduct(product, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult UpFile(string fileOrderType, string fileOrderNo)
        {
            FileArchiveLogic logic = new FileArchiveLogic();
            FileArchive fileArchive = new FileArchive();

            fileArchive.Company = "";
            fileArchive.Creator = "";
            fileArchive.FileKey = fileOrderType + "-" + fileOrderNo;

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
                string path = Server.MapPath(@"~\File");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = hfc[0].FileName;
                string newName = "";
                //判断文件名字是否包含路径名，如果有则提取文件名
                if (name.LastIndexOf("\\") > -1)
                {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                    newName = Guid.NewGuid().ToString() + name.Substring(name.LastIndexOf('.'));
                }else
                {
                    newName = Guid.NewGuid().ToString() + name.Substring(name.LastIndexOf('.'));
                }
                hfc[0].SaveAs(path + "\\" + newName);

                if(!logic.AddFileArchive(fileArchive, name, path + "\\" + newName))
                {
                    return Json("NO-匯入失败", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("YES-匯入成功", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchFile(string FileKey)
        {
            FileArchiveLogic logic = new FileArchiveLogic();
            List<FileArchive> lst = logic.SearchFile(FileKey);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DelFile(string FileKey)
        {
            FileArchiveLogic logic = new FileArchiveLogic();
            if(!logic.DelFile(FileKey))
            {
                return Json("NO-刪除失败", JsonRequestBehavior.AllowGet);
            }
            return Json("YES-刪除成功", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownFile(string FilePath,string FileName)
        {
            return File(new FileStream(FilePath, FileMode.Open), "application/x-zip-compressed", FileName);
        }
    }
}