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
using System.Web.Mvc;

namespace CONI02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = new Contract_H();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Contract_H"] = logic.GetContract_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = new Contract_H();
            string col = "";
            string condition = "";
            string value = "";
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Contract_H"] = logic.GetContract_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            else if (Request.Form["action"] == "btnTossTurn")
            {
                string strchk = Request.Form["chk"];
                ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
                serviceArrange_H.Company = Session["Company"].ToString();
                serviceArrange_H.UserGroup = Session["UserGroup"].ToString();
                serviceArrange_H.Creator = Session["UserId"].ToString();
                serviceArrange_H.OrderDate = Request.Form["requesDate"].Replace("/", "");
                string msg = "";
                if (!logic.AddServiceArrange(serviceArrange_H, strchk, out msg))
                {
                    ViewBag.Msg = "分配失敗！"+ msg;
                }
            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["Contract_H"] = logic.GetContract_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("合約單別,單據名稱,合約單號,合約起日,合約迄日,客戶代號,客戶簡稱");
                //    List<Contract_H> objContract_H = ViewData["Contract_H"] as List<Contract_H>;
                //    foreach (var obj in objContract_H)
                //    {
                //        sw.WriteLine(obj.ContractType + "," + obj.OrderSName +
                //            "," + obj.ContractNo + "," + obj.ContractSDate + "," +
                //            obj.ContractEDate + "," + obj.CustomerId + "," +
                //            obj.CustomerName);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Contract_H");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("合約單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("合約單號");
                    row.CreateCell(3).SetCellValue("合約起日");
                    row.CreateCell(4).SetCellValue("合約迄日");
                    row.CreateCell(5).SetCellValue("客戶代號");
                    row.CreateCell(6).SetCellValue("客戶簡稱");


                    int index = 1;

                    List<Contract_H> objContract_H = ViewData["Contract_H"] as List<Contract_H>;
                    foreach (var obj in objContract_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.ContractType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.ContractNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.ContractSDate.ToString());
                        row.CreateCell(4).SetCellValue(obj.ContractEDate.ToString());
                        row.CreateCell(5).SetCellValue(obj.CustomerId.ToString());
                        row.CreateCell(6).SetCellValue(obj.CustomerName.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.ContractNo = Request.Form["query_ContractNo"];
                ViewBag.CustomerId = Request.Form["customerid"];

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Contract_H.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Contract_H.xls");
            }
            else
            {
                if (Request.Form["query_ContractNo"].Trim() != "")
                {
                    col = ",h.ContractNo";
                    condition = "," + Request.Form["selCond1"];
                    value = "," + Request.Form["query_ContractNo"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col = ",h.CustomerId";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["customerid"];
                }
            }
            ViewBag.selCond1 = Request.Form["selCond1"];
            ViewBag.selCond2 = Request.Form["selCond2"];
            ViewBag.ContractNo = Request.Form["query_ContractNo"];
            ViewBag.CustomerId = Request.Form["customerid"];
            ViewData["Contract_H"] = logic.GetContract_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;

            return View();
        }

        public ActionResult CUR()
        {
            List<Contract_ProductD> objContract_ProductD = new List<Contract_ProductD>();
            ViewData["Contract_ProductD"] = objContract_ProductD;
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Confirmed = "N:未確認";
            ViewBag.Version = "0000";
            ViewBag.Discount = "100";
            ViewBag.WarrantyPeriod = "0";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            Contract_HLogic logic = new Contract_HLogic();

            //編輯
            if (Request.Form["action"] == "EditDetails")
            {
                Contract_H contract_H = logic.Contract_HInfo(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.ContractType = contract_H.ContractType;       //合約單別
                ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
                ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
                ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
                ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
                ViewBag.Version = contract_H.Version;           //版次
                ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
                ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
                ViewBag.Discount = contract_H.Discount;           //零件折扣
                ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
                ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
                ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
                ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
                ViewBag.Renewal = contract_H.Renewal;           //續約否
                if (contract_H.Confirmed == "N")//確認碼
                {
                    ViewBag.Confirmed = "N.未確認";
                }
                else if (contract_H.Confirmed == "Y")
                {
                    ViewBag.Confirmed = "Y.已確認";
                }
                else
                {
                    ViewBag.Confirmed = "V.作廢";
                }

                ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
                ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
                ViewBag.Remark = contract_H.Remark;       //備註
                ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
                ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
                ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
                ViewBag.Address = contract_H.Address;       //地址
                ViewBag.Contact = contract_H.Contact;       //聯絡人
                ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
                ViewBag.Tel = contract_H.Tel;       //電話
                ViewBag.Fax = contract_H.Fax;       //傳真
                ViewBag.Email = contract_H.Email;       //E-mail

                Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
                ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.Type = "Edit";
                return View("CUR");
            }
            //複製并新增
            if (Request.Form["action"] == "CopyAddDetails")
            {
                Contract_H contract_H = logic.Contract_HInfo(Request.Form["ContractType"], Request.Form["ContractNo"]);
                //ViewBag.ContractType = contract_H.ContractType;       //合約單別
                //ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
                //ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
                ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
                ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
                ViewBag.Version = contract_H.Version;           //版次
                ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
                ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
                ViewBag.Discount = contract_H.Discount;           //零件折扣
                ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
                ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
                ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
                ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
                ViewBag.Renewal = contract_H.Renewal;           //續約否
                if (contract_H.Confirmed == "N")//確認碼
                {
                    ViewBag.Confirmed = "N.未確認";
                }
                else if (contract_H.Confirmed == "Y")
                {
                    ViewBag.Confirmed = "Y.已確認";
                }
                else
                {
                    ViewBag.Confirmed = "V.作廢";
                }

                ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
                ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
                ViewBag.Remark = contract_H.Remark;       //備註
                ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
                ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
                ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
                ViewBag.Address = contract_H.Address;       //地址
                ViewBag.Contact = contract_H.Contact;       //聯絡人
                ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
                ViewBag.Tel = contract_H.Tel;       //電話
                ViewBag.Fax = contract_H.Fax;       //傳真
                ViewBag.Email = contract_H.Email;       //E-mail

                Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
                ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(Request.Form["ContractType"], Request.Form["ContractNo"]);
                ViewBag.ContractType = "";       //合約單別
                ViewBag.ContractNo = "";           //合約單別名稱
                return View("CUR");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string ContractType, string ContractNo)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = logic.Contract_HInfo(ContractType, ContractNo);
            ViewBag.ContractType = contract_H.ContractType;       //合約單別
            ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
            ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
            ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
            ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
            ViewBag.Version = contract_H.Version;           //版次
            ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
            ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
            ViewBag.Discount = contract_H.Discount;           //零件折扣
            ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
            ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
            ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
            ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
            ViewBag.Renewal = contract_H.Renewal;           //續約否
            if (contract_H.Confirmed == "N")//確認碼
            {
                ViewBag.Confirmed = "N.未確認";
            }
            else if (contract_H.Confirmed == "Y")
            {
                ViewBag.Confirmed = "Y.已確認";
            }
            else
            {
                ViewBag.Confirmed = "V.作廢";
            }
            
            ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
            ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
            ViewBag.Remark = contract_H.Remark;       //備註
            ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
            ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
            ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
            ViewBag.Address = contract_H.Address;       //地址
            ViewBag.Contact = contract_H.Contact;       //聯絡人
            ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
            ViewBag.Tel = contract_H.Tel;       //電話
            ViewBag.Fax = contract_H.Fax;       //傳真
            ViewBag.Email = contract_H.Email;       //E-mail

            Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
            ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(ContractType, ContractNo);


            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ContractType, string ContractNo)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = logic.Contract_HInfo(ContractType, ContractNo);
            ViewBag.ContractType = contract_H.ContractType;       //合約單別
            ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
            ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
            ViewBag.OrderDate = contract_H.OrderDate;           //單據日期
            ViewBag.ConfirmedDate = contract_H.ConfirmedDate;           //合約日期
            ViewBag.Version = contract_H.Version;           //版次
            ViewBag.RoutineServiceFreq = contract_H.RoutineServiceFreq;           //定保次數
            ViewBag.D2DServiceFreq = contract_H.D2DServiceFreq;           //外出服務
            ViewBag.Discount = contract_H.Discount;           //零件折扣
            ViewBag.ContractPrice = contract_H.ContractPrice;           //合約費用
            ViewBag.WarrantyPeriod = contract_H.WarrantyPeriod;           //保固期限
            ViewBag.ContractSDate = contract_H.ContractSDate;     //合約起日
            ViewBag.ContractEDate = contract_H.ContractEDate;      //合約迄日
            ViewBag.Renewal = contract_H.Renewal;           //續約否
            if (contract_H.Confirmed == "N")//確認碼
            {
                ViewBag.Confirmed = "N.未確認";
            }
            else if (contract_H.Confirmed == "Y")
            {
                ViewBag.Confirmed = "Y.已確認";
            }
            else
            {
                ViewBag.Confirmed = "V.作廢";
            }

            ViewBag.ConfirmedMan = contract_H.ConfirmedMan;       //確認人員
            ViewBag.ConfirmedManName = contract_H.ConfirmedManName;       //確認人員名稱
            ViewBag.Remark = contract_H.Remark;       //備註
            ViewBag.CustomerId = contract_H.CustomerId;       //客戶代號
            ViewBag.CustomerName = contract_H.CustomerName;       //客戶名稱
            ViewBag.AddressSName = contract_H.AddressSName;       //地址簡稱
            ViewBag.Address = contract_H.Address;       //地址
            ViewBag.Contact = contract_H.Contact;       //聯絡人
            ViewBag.ContactName = contract_H.ContactName;       //聯絡人名稱
            ViewBag.Tel = contract_H.Tel;       //電話
            ViewBag.Fax = contract_H.Fax;       //傳真
            ViewBag.Email = contract_H.Email;       //E-mail

            Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
            ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(ContractType, ContractNo);


            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Confirmed(string ContractType, string ContractNo,string Confirmed)
        {
            Contract_H contract_H = new Contract_H();
            contract_H.Company = Session["Company"].ToString();
            contract_H.UserGroup= Session["UserGroup"].ToString();
            contract_H.Modifier = Session["UserId"].ToString();
            contract_H.Confirmed = Confirmed.ToUpper();
            if (Confirmed.ToUpper() == "Y")
            {
                contract_H.ConfirmedMan = Session["UserId"].ToString();
            }
            contract_H.ContractType=ContractType;
            contract_H.ContractNo=ContractNo;
            Contract_HLogic logic = new Contract_HLogic();
            string infomsg = "";
            string msg = "";
            if (!logic.TypeContract_H(contract_H, out infomsg))
            {
                msg = "NO-操作失敗;"+infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add()
        {
            Contract_H contract_H = new Contract_H();
            contract_H.Company = "";
            contract_H.UserGroup = "";
            contract_H.Creator = "";
            contract_H.ContractType = Request.Form["ContractType"];
            contract_H.ContractNo = Request.Form["ContractNo"];
            contract_H.OrderDate = Request.Form["OrderDate"].ToString().Replace("/", "");
            contract_H.Version = Request.Form["Version"];
            contract_H.RoutineServiceFreq = double.Parse(Request.Form["RoutineServiceFreq"].ToString()==""?"0":Request.Form["RoutineServiceFreq"].ToString());
            contract_H.D2DServiceFreq = double.Parse(Request.Form["D2DServiceFreq"].ToString()==""?"0":Request.Form["D2DServiceFreq"].ToString());
            contract_H.Discount = double.Parse(Request.Form["Discount"].ToString() == "" ? "0" : Request.Form["Discount"].ToString());
            contract_H.ContractPrice = double.Parse(Request.Form["ContractPrice"].ToString() == "" ? "0" : Request.Form["ContractPrice"].ToString());
            contract_H.WarrantyPeriod = double.Parse(Request.Form["WarrantyPeriod"].ToString() == "" ? "0" : Request.Form["WarrantyPeriod"].ToString());
            contract_H.ContractSDate = Request.Form["ContractSDate"].ToString().Replace("/", "");
            contract_H.ContractEDate = Request.Form["ContractEDate"].ToString().Replace("/", "");
            contract_H.Renewal = Request.Form["Renewal"] == null ? "N" : "Y";
            if(Request.Form["AutoConfirm"]=="Y")
            {
                contract_H.ConfirmedDate = DateTime.Now.ToString("yyyyMMdd");
                contract_H.Confirmed = "Y";
                contract_H.ConfirmedMan = "";
            }else
            {
                contract_H.Confirmed = "N";
                contract_H.ConfirmedDate = "";
                contract_H.ConfirmedMan = "";
            }
            
            contract_H.Remark = Request.Form["Remark"];
            contract_H.CustomerId = Request.Form["CustomerId"];
            contract_H.AddressSName = Request.Form["AddressSName"];
            contract_H.Address = Request.Form["Address"];
            contract_H.Contact = Request.Form["Contact"];
            contract_H.Tel = Request.Form["Tel"];
            contract_H.Fax = Request.Form["Fax"];
            contract_H.Email = Request.Form["Email"]==null?"": Request.Form["Email"];
            contract_H.Contact = Request.Form["Contact"];

            string strProductNo = Request.Form["strCProductNo"] == null ? "" : Request.Form["strCProductNo"].ToString();
            string strProductName = Request.Form["strCProductName"] == null ? "" : Request.Form["strCProductName"].ToString();
            string strQTY = Request.Form["strCQTY"] == null ? "" : Request.Form["strCQTY"].ToString();
            string strUnit = Request.Form["strCUnit"] == null ? "" : Request.Form["strCUnit"].ToString();
            string strWarrantyPeriod = Request.Form["strCWarrantyPeriod"] == null ? "" : Request.Form["strCWarrantyPeriod"].ToString();
            string strWarrantySDate = Request.Form["strCWarrantySDate"] == null ? "" : Request.Form["strCWarrantySDate"].ToString();
            string strWarrantyEDate = Request.Form["strCWarrantyEDate"] == null ? "" : Request.Form["strCWarrantyEDate"].ToString();
            string strRoutineServiceFreq = Request.Form["strCRoutineServiceFreq"] == null ? "" : Request.Form["strCRoutineServiceFreq"].ToString();
            string strRemark = Request.Form["strCRemark"] == null ? "" : Request.Form["strCRemark"].ToString();
            string strProductSerial = Request.Form["strProductSerial"] == null ? "" : Request.Form["strProductSerial"].ToString();

            Contract_HLogic logic = new Contract_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddContract_H(contract_H, strProductNo, strProductName,
             strQTY, strUnit, strWarrantyPeriod, strWarrantySDate,
             strWarrantyEDate, strRoutineServiceFreq, strRemark, strProductSerial, out infomsg))
            {
                msg = "NO-新增失敗;"+infomsg;
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
            Contract_H contract_H = new Contract_H();
            contract_H.Company = "";
            contract_H.UserGroup = "";
            contract_H.Modifier = "";
            contract_H.ContractType = Request.Form["ContractType"];
            contract_H.ContractNo = Request.Form["ContractNo"];
            contract_H.OrderDate = Request.Form["OrderDate"].ToString().Replace("/", "");
            contract_H.ConfirmedDate = Request.Form["ConfirmedDate"].ToString().Replace("/", "");
            contract_H.Version = Request.Form["Version"];
            contract_H.RoutineServiceFreq = double.Parse(Request.Form["RoutineServiceFreq"].ToString() == "" ? "0" : Request.Form["RoutineServiceFreq"].ToString());
            contract_H.D2DServiceFreq = double.Parse(Request.Form["D2DServiceFreq"].ToString() == "" ? "0" : Request.Form["D2DServiceFreq"].ToString());
            contract_H.Discount = double.Parse(Request.Form["Discount"].ToString() == "" ? "0" : Request.Form["Discount"].ToString());
            contract_H.ContractPrice = double.Parse(Request.Form["ContractPrice"].ToString() == "" ? "0" : Request.Form["ContractPrice"].ToString());
            contract_H.WarrantyPeriod = double.Parse(Request.Form["WarrantyPeriod"].ToString() == "" ? "0" : Request.Form["WarrantyPeriod"].ToString());
            contract_H.ContractSDate = Request.Form["ContractSDate"].ToString().Replace("/", "");
            contract_H.ContractEDate = Request.Form["ContractEDate"].ToString().Replace("/", "");
            contract_H.Renewal = Request.Form["Renewal"] == null ? "N" : "Y"; ;
            contract_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            contract_H.ConfirmedMan = Request.Form["ConfirmedMan"];
            contract_H.Remark = Request.Form["Remark"];
            contract_H.CustomerId = Request.Form["CustomerId"];
            contract_H.AddressSName = Request.Form["AddressSName"];
            contract_H.Address = Request.Form["Address"];
            contract_H.Contact = Request.Form["Contact"];
            contract_H.Tel = Request.Form["Tel"];
            contract_H.Fax = Request.Form["Fax"];
            contract_H.Email = Request.Form["Email"];
            contract_H.Contact = Request.Form["Contact"];

            string strProductNo = Request.Form["strCProductNo"] == null ? "" : Request.Form["strCProductNo"].ToString();
            string strProductName = Request.Form["strCProductName"] == null ? "" : Request.Form["strCProductName"].ToString();
            string strQTY = Request.Form["strCQTY"] == null ? "" : Request.Form["strCQTY"].ToString();
            string strUnit = Request.Form["strCUnit"] == null ? "" : Request.Form["strCUnit"].ToString();
            string strWarrantyPeriod = Request.Form["strCWarrantyPeriod"] == null ? "" : Request.Form["strCWarrantyPeriod"].ToString();
            string strWarrantySDate = Request.Form["strCWarrantySDate"] == null ? "" : Request.Form["strCWarrantySDate"].ToString();
            string strWarrantyEDate = Request.Form["strCWarrantyEDate"] == null ? "" : Request.Form["strCWarrantyEDate"].ToString();
            string strRoutineServiceFreq = Request.Form["strCRoutineServiceFreq"] == null ? "" : Request.Form["strCRoutineServiceFreq"].ToString();
            string strRemark = Request.Form["strCRemark"] == null ? "" : Request.Form["strCRemark"].ToString();
            string strProductSerial = Request.Form["strProductSerial"] == null ? "" : Request.Form["strProductSerial"].ToString();

            Contract_HLogic logic = new Contract_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateContract_H(contract_H, strProductNo, strProductName,
             strQTY, strUnit, strWarrantyPeriod, strWarrantySDate,
             strWarrantyEDate, strRoutineServiceFreq, strRemark,strProductSerial, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCustomerContact( string Col, string Condition, string conditionValue, int Page)
        {
            CustomerContactLogic logic = new CustomerContactLogic();
            List<CustomerContact> lst = logic.GetCustomerContact(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchVendorContact(string Col, string Condition, string conditionValue, int Page)
        {
            VendorContactLogic logic = new VendorContactLogic();
            List<VendorContact> lst = logic.GetVendorContact(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCustomerAddress(string Col, string Condition, string conditionValue, int Page)
        {
            CustomerAddressLogic logic = new CustomerAddressLogic();
            List<CustomerAddress> lst = logic.GetCustomerAddress(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContactInfo(string CustomerId, string Contact)
        {
            CustomerContactLogic logic = new CustomerContactLogic();
            return Json(logic.GetCustomerContactInfo(CustomerId,Contact), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchCustomer(string Col, string Condition, string conditionValue, int Page)
        {
            CustomerLogic logic = new CustomerLogic();
            Customer customer = new Customer();
            //customer.CustomerId = CustomerId;
            //customer.CustomerName = CustomerName;
            List<Customer> lst = logic.GetCustomer(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchCustomerInfo(string CustomerId)
        {
            CustomerLogic logic = new CustomerLogic();
            return Json(logic.GetCustomerInfo(CustomerId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAddressInfo(string CustomerId,string AddressSName)
        {
            CustomerAddressLogic logic = new CustomerAddressLogic();
            return Json(logic.GetCustomerAddressInfo(CustomerId, AddressSName), JsonRequestBehavior.AllowGet);
        }

        //刪除
        //public ActionResult Delete(string ContractType, string ContractNo)
        //{
        //    Contract_HLogic logic = new Contract_HLogic();

        //    if (!logic.DelContract_H(ContractType, ContractNo))
        //    {
        //        ViewBag.Msg = "刪除失敗！";
        //    }

        //    return RedirectToAction("Index", "Main");
        //}

        public JsonResult SearchProduct(string ProductNo, string Col, string Condition, string conditionValue, int Page)
        {
            ProductLogic logic = new ProductLogic();
            Product product = new Product();
            product.ProductNo = ProductNo;
            //product.ProductName = ProductName;
            List<Product> lst = logic.GetProduct(product, Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductName(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            string msg = "";
            if (product != null)
            {
                msg = "OK-"+product.ProductName;
            }else
            {
                msg = "NO-品號不存在";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchContract(string ContractType, string ContractNo, string CustomerId, int Page)
        //{
        //    Contract_HLogic logic = new Contract_HLogic();
        //    Contract_H contract_H = new Contract_H();
        //    contract_H.ContractType = ContractType;
        //    contract_H.ContractNo = ContractNo;
        //    contract_H.CustomerId = CustomerId;
        //    List<Contract_H> lst = logic.GetContractH(contract_H, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchOrderType(string OrderCategory, string Col, string Condition, string conditionValue, int Page)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            List<OrderCategory> lst = logic.GetOrderCategory( Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContractProduct(string Col, string Condition, string conditionValue, int Page)
        {
            Contract_ProductDLogic logic = new Contract_ProductDLogic();
            //Contract_ProductD contract_ProductD = new Contract_ProductD();
            //contract_ProductD.ContractType = ContractType;
            //contract_ProductD.ContractNo = ContractNo;
            //contract_ProductD.ItemId = ItemId;
            List<Contract_ProductD> lst = logic.GetContractProduct(Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContractSerial(string ContractType, string ContractNo, string ProductNo, string SerialNo, int Page)
        {
            Contract_ProductDLogic logic = new Contract_ProductDLogic();
            Contract_ProductD contract_ProductD = new Contract_ProductD();
            contract_ProductD.ContractType = ContractType;
            contract_ProductD.ContractNo = ContractNo;
            contract_ProductD.ProductNo = ProductNo;
            contract_ProductD.SerialNo = SerialNo;
            contract_ProductD.IsClosed = "N";
            List<Contract_ProductD> lst = logic.GetContractSerial(contract_ProductD, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tree(string ContractType, string ContractNo)
        {
            Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
            ViewData["Contract_ProductD"] = contract_ProductLogic.GetContract_ProductD(ContractType, ContractNo);

            ViewBag.ContractType = ContractType;
            ViewBag.ContractNo = ContractNo;

            //SupportApl_HLogic logic = new SupportApl_HLogic();
            //SupportApl_H supportApl_H = logic.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
            //ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
            //ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
            //ViewBag.OrderDate = supportApl_H.OrderDate == "" ? "" : DateTime.ParseExact(supportApl_H.OrderDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //ViewBag.CustomerId = supportApl_H.CustomerId;
            //ViewBag.CustomerName = supportApl_H.CustomerName;
            //if (supportApl_H.OrderStatus == "0")
            //{
            //    ViewBag.OrderStatus = "未處理";
            //}
            //if (supportApl_H.OrderStatus == "1")
            //{
            //    ViewBag.OrderStatus = "已派工";
            //}
            //if (supportApl_H.OrderStatus == "2")
            //{
            //    ViewBag.OrderStatus = "未處理";
            //}
            //if (supportApl_H.OrderStatus == "3")
            //{
            //    ViewBag.OrderStatus = "未處理";
            //}

            //SupportAplAsignLogic supportAplAsignLogic = new SupportAplAsignLogic();
            //SupportAplAsign supportAplAsign = supportAplAsignLogic.GetInfo(SupportAplOrderType, SupportAplOrderNo);
            //ViewBag.AsignOrderType = supportAplAsign.AsignOrderType;
            //ViewBag.AsignOrderNo = supportAplAsign.AsignOrderNo;
            //ViewBag.Version = supportAplAsign.Version;

            //Picking_HLogic picking_HLogic = new Picking_HLogic();
            //Picking_H picking_H = picking_HLogic.GetInfo(SupportAplOrderType, SupportAplOrderNo);
            //ViewBag.PickingOrderType = picking_H.PickingOrderType;
            //ViewBag.PickingOrderNo = picking_H.PickingOrderNo;

            return View("Tree");
        }

        public JsonResult IsContractChange_H(string ContractType, string ContractNo)
        {
            ContractChange_HLogic logic = new ContractChange_HLogic();
            ContractChange_H contractChange_H = new ContractChange_H();
            contractChange_H.ContractType = ContractType;
            contractChange_H.ContractNo = ContractNo;
            return Json(logic.IsContractChange_H(contractChange_H), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string ContractType, string ContractNo)
        {
            ContractChange_HLogic logic = new ContractChange_HLogic();
            ContractChange_H contractChange_H = new ContractChange_H();
            contractChange_H.ContractType = ContractType;
            contractChange_H.ContractNo = ContractNo;

            string msg = "";
            if (!logic.IsContractChange_H(contractChange_H))
            {
                msg = "NO-刪除失敗!請先刪除變更單！";
            }
            else
            {
                Contract_HLogic Clogic = new Contract_HLogic();
                if (!Clogic.DelContract_H(ContractType, ContractNo))
                {
                    msg = "NO-刪除失敗！";
                }else
                {
                    msg = "OK-刪除成功";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}