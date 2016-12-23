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

namespace CONI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ContractChange_HLogic logic = new ContractChange_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["ContractChange_H"] = logic.GetContractChange_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ContractChange_HLogic logic = new ContractChange_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["ContractChange_H"] = logic.GetContractChange_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("合約單別,單據名稱,合約單號,版次,客戶代號,客戶簡稱,合約起日,合約迄日");
                //    List<ContractChange_H> objContract_H = ViewData["ContractChange_H"] as List<ContractChange_H>;
                //    foreach (var obj in objContract_H)
                //    {
                //        sw.WriteLine(obj.ContractType + "," + obj.OrderSName +
                //            "," + obj.ContractNo + "," + obj.Version + "," + 
                //            obj.CustomerId + "," + obj.CustomerName + "," + 
                //            obj.ContractSDate + "," +obj.ContractEDate );
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("ContractChange_H");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("合約單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("合約單號");
                    row.CreateCell(3).SetCellValue("版次");
                    row.CreateCell(4).SetCellValue("客戶代號");
                    row.CreateCell(5).SetCellValue("客戶簡稱");
                    row.CreateCell(6).SetCellValue("合約起日");
                    row.CreateCell(7).SetCellValue("合約迄日");


                    int index = 1;

                    List<ContractChange_H> objContract_H = ViewData["ContractChange_H"] as List<ContractChange_H>;
                    foreach (var obj in objContract_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.ContractType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.ContractNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.Version.ToString());
                        row.CreateCell(4).SetCellValue(obj.CustomerId.ToString());
                        row.CreateCell(5).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(6).SetCellValue(obj.ContractSDate.ToString());
                        row.CreateCell(7).SetCellValue(obj.ContractEDate.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.ContractNo = Request.Form["query_ContractNo"];
                ViewBag.CustomerId = Request.Form["customerid"];

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ContractChange_H.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "ContractChange_H.xls");
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
                    col = ",ch.CustomerId";
                    condition = "," + Request.Form["selCond2"];
                    value = "," + Request.Form["customerid"];
                }
            }
            ViewBag.selCond1 = Request.Form["selCond1"];
            ViewBag.selCond2 = Request.Form["selCond2"];
            ViewBag.ContractNo = Request.Form["query_ContractNo"];
            ViewBag.CustomerId = Request.Form["customerid"];
            ViewData["ContractChange_H"] = logic.GetContractChange_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;

            return View();
        }

        public ActionResult CUR()
        {
            List<ContractChange_ProductSerial> objContractChange_ProductSerial = new List<ContractChange_ProductSerial>();
            ViewData["ContractChange_ProductSerial"] = objContractChange_ProductSerial;
            return View("CUR");
        }

        //合約
        public JsonResult SearchContract(string ContractType, string Col, string Condition, string conditionValue, int Page)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = new Contract_H();
            contract_H.ContractType = ContractType;
            //contract_H.ContractNo = ContractNo;
            List<Contract_H> lst = logic.GetContractH(contract_H, Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //通過合約查詢信息
        public JsonResult SearchContractInfo(string ContractType, string ContractNo)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = logic.Contract_HInfo(ContractType, ContractNo);
            return Json(contract_H, JsonRequestBehavior.AllowGet);
        }

        //通過合約查詢品項
        public JsonResult SearchContractProductDInfo(string ContractType, string ContractNo)
        {
            Contract_ProductDLogic contract_ProductLogic = new Contract_ProductDLogic();
            List<Contract_ProductD> lst = contract_ProductLogic.GetContract_ProductD(ContractType, ContractNo);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add()
        {
            ContractChange_H contractChange_H = new ContractChange_H();
            contractChange_H.Company = "";
            contractChange_H.UserGroup = "";
            contractChange_H.Creator = "";
            contractChange_H.ContractType = Request.Form["ContractType"];
            contractChange_H.ContractNo = Request.Form["ContractNo"];
            contractChange_H.OrderDate = Request.Form["OrderDate"].ToString().Replace("/", "");
            contractChange_H.TerminationOfService = Request.Form["TerminationOfService"] == null ? "N" : "Y";
            contractChange_H.ModiReason = Request.Form["ModiReason"];
            contractChange_H.Confirmed = Request.Form["Confirmed"].Substring(0,1);

            string strCItemId = Request.Form["strCItemId"] == null ? "" : Request.Form["strCItemId"].ToString();
            string strProductNo = Request.Form["strCProductNo"] == null ? "" : Request.Form["strCProductNo"].ToString();
            string strSerialNo = Request.Form["strProductSerial"] == null ? "" : Request.Form["strProductSerial"].ToString();
            string strNewIsClosed = Request.Form["strProductSerialClosed"] == null ? "" : Request.Form["strProductSerialClosed"].ToString();

            ContractChange_HLogic logic = new ContractChange_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddContractChange_H(contractChange_H, strCItemId, strProductNo,
             strSerialNo, strNewIsClosed, out infomsg))
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
            ContractChange_H contractChange_H = new ContractChange_H();
            contractChange_H.Company = "";
            contractChange_H.UserGroup = "";
            contractChange_H.Creator = "";
            contractChange_H.ContractType = Request.Form["ContractType"];
            contractChange_H.ContractNo = Request.Form["ContractNo"];
            contractChange_H.Version = Request.Form["Version"];
            contractChange_H.OrderDate = Request.Form["OrderDate"].ToString().Replace("/", "");
            contractChange_H.TerminationOfService = Request.Form["TerminationOfService"] == null ? "N" : "Y";
            contractChange_H.ModiReason = Request.Form["ModiReason"];
            contractChange_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);

            string strCItemId = Request.Form["strCItemId"] == null ? "" : Request.Form["strCItemId"].ToString();
            string strProductNo = Request.Form["strCProductNo"] == null ? "" : Request.Form["strCProductNo"].ToString();
            string strSerialNo = Request.Form["strProductSerial"] == null ? "" : Request.Form["strProductSerial"].ToString();
            string strNewIsClosed = Request.Form["strProductSerialClosed"] == null ? "" : Request.Form["strProductSerialClosed"].ToString();

            ContractChange_HLogic logic = new ContractChange_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateContractChange_H(contractChange_H, strCItemId, strProductNo,
             strSerialNo, strNewIsClosed, out infomsg))
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
        public ActionResult Delete(string ContractType, string ContractNo, string Version)
        {
            ContractChange_HLogic logic = new ContractChange_HLogic();

            if (!logic.DelContractChange_H(ContractType, ContractNo, Version))
            {
                ViewBag.Msg = "刪除失敗！";
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ContractType, string ContractNo, string Version)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = logic.Contract_HInfo(ContractType, ContractNo);
            ViewBag.ContractType = contract_H.ContractType;       //合約單別
            ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
            ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
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

            ContractChange_HLogic hlogic = new ContractChange_HLogic();
            ContractChange_H contractChange_H = hlogic.ContractChange_HInfo(ContractType, ContractNo, Version);
            
            if (contractChange_H.Confirmed == "N")//確認碼
            {
                ViewBag.Confirmed = "N.未變更";
            }
            else if (contractChange_H.Confirmed == "Y")
            {
                ViewBag.Confirmed = "Y.已變更";
            }else
            {
                ViewBag.Confirmed = "V.作廢";
            }
            ViewBag.OrderDate = contractChange_H.OrderDate;           //單據日期
            ViewBag.TerminationOfService = contractChange_H.TerminationOfService;
            ViewBag.ModiReason = contractChange_H.ModiReason;
            ViewBag.ConfirmedMan = contractChange_H.Confirmor;       //確認人員
            ViewBag.ConfirmedManName = contractChange_H.ConfirmorName;       //確認人員名稱

            ContractChange_ProductSerialLogic ContractChange_ProductSerialLogic = new ContractChange_ProductSerialLogic();
            ViewData["ContractChange_ProductSerial"] = ContractChange_ProductSerialLogic.GetContractChange_ProductSerial(ContractType, ContractNo);


            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ContractType, string ContractNo, string Version)
        {
            Contract_HLogic logic = new Contract_HLogic();
            Contract_H contract_H = logic.Contract_HInfo(ContractType, ContractNo);
            ViewBag.ContractType = contract_H.ContractType;       //合約單別
            ViewBag.OrderSName = contract_H.OrderSName;           //合約單別名稱
            ViewBag.ContractNo = contract_H.ContractNo;           //合約單號
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

            ContractChange_HLogic hlogic = new ContractChange_HLogic();
            ContractChange_H contractChange_H = hlogic.ContractChange_HInfo(ContractType, ContractNo, Version);

            if (contractChange_H.Confirmed == "N")//確認碼
            {
                ViewBag.Confirmed = "N.未變更";
            }
            else if (contractChange_H.Confirmed == "Y")
            {
                ViewBag.Confirmed = "Y.已變更";
            }
            else
            {
                ViewBag.Confirmed = "V.作廢";
            }
            ViewBag.OrderDate = contractChange_H.OrderDate;           //單據日期
            ViewBag.TerminationOfService = contractChange_H.TerminationOfService;
            ViewBag.ModiReason = contractChange_H.ModiReason;

            ContractChange_ProductSerialLogic ContractChange_ProductSerialLogic = new ContractChange_ProductSerialLogic();
            ViewData["ContractChange_ProductSerial"] = ContractChange_ProductSerialLogic.GetContractChange_ProductSerial(ContractType, ContractNo);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Confirmed(string ContractType, string ContractNo, string Version)
        {
            ContractChange_H contractChange_H = new ContractChange_H();
            contractChange_H.Confirmed = "Y";
            contractChange_H.Confirmor = "";
            contractChange_H.ContractType = ContractType;
            contractChange_H.ContractNo = ContractNo;
            contractChange_H.Version = Version;
            ContractChange_HLogic logic = new ContractChange_HLogic();
            string infomsg = "";
            string msg = "";
            if (!logic.TypeContractChange_H(contractChange_H, out infomsg))
            {
                msg = "NO-確認失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}