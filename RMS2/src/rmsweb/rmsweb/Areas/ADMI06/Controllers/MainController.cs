using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;

namespace ADMI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index(string action)
        {
            if (action == "btnAdd")
            {
                return RedirectToAction("CUR", "Main");
            }
            if (action == "btnExport")
            {
                 object objLock = new object();
                lock (objLock)
                {
                    CurrencyLogic currencyLogic = new CurrencyLogic();
                    Currency currency = new Currency();
                    currency.CurrencyId = Request.Form["currencyId"];
                    DataTable dtExport = currencyLogic.SelectCurrency(currency,Request.Form["selectCondition"]);
                    string path = Server.MapPath(@"~\ExpotFile");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (
                        FileStream fs = new FileStream(path + @"\CurrencyInfo.csv", FileMode.Create, FileAccess.Write))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine("幣別代號,幣別名稱,生效日期,匯率");
                        foreach (DataRow dr in dtExport.Rows)
                        {
                            sw.WriteLine(dr["CurrencyId"].ToString() + "," + dr["Currency"].ToString() + "," +
                                         dr["EffectiveDate"].ToString() + "," + dr["ExchangeRate"].ToString());
                        }
                        sw.Close();
                        sw.Dispose();
                    }
                    return File(new FileStream(path + @"\CurrencyInfo.csv", FileMode.Open), "text/plain",
                        "CurrencyInfo.csv");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                CurrencyLogic currencyLogic = new CurrencyLogic();
                DataTable dtDisplay = currencyLogic.SearchDetailCurrency(col, condition, value);
                List<Currency> lst = new List<Currency>();
                Currency currency = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    currency = new Currency();
                    currency.Company = dr["Company"].ToString();
                    currency.UserGroup = dr["UserGroup"].ToString();
                    currency.Creator = dr["Creator"].ToString();
                    currency.CreateDate = dr["CreateDate"].ToString();
                    currency.Modifier = dr["Modifier"].ToString();
                    currency.ModiDate = dr["ModiDate"].ToString();
                    currency.CurrencyId = dr["CurrencyId"].ToString();
                    currency.CurrencyName = dr["Currency"].ToString();
                    currency.EffectiveDate = dr["EffectiveDate"].ToString();
                    currency.ExchangeRate = dr["ExchangeRate"].ToString();
                    currency.Remark = dr["Remark"].ToString();
                    lst.Add(currency);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                TableListLogic tableListLogic = new TableListLogic();
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableList.TableName = Request.Form["TableName"];
                DataTable dtDisplay = tableListLogic.SelectTableList(tableList, Request.Form["selectCondition"], Request.Form["selectCondition1"]);
                List<TableList> lst = new List<TableList>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    tableList = new TableList();
                    tableList.TableId = dr["TableId"].ToString();
                    tableList.TableName = dr["TableName"].ToString();
                    tableList.Mode = dr["Mode"].ToString();
                    tableList.ModuleId = dr["ModuleId"].ToString();
                    tableList.ModuleName = dr["ModuleName"].ToString();
                    tableList.Remark = dr["Remark"].ToString();
                    lst.Add(tableList);
                }
                ViewBag.TableId = Request.Form["TableId"];
                ViewBag.TableName = Request.Form["TableName"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewBag.SelectCondition1 = Request.Form["selectCondition1"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                TableListLogic tableListLogic = new TableListLogic();
                DataTable dtDisplay = tableListLogic.SelectTableList(null,null, null);
                List<TableList> lst = new List<TableList>();
                TableList tableList = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    tableList = new TableList();
                    tableList.TableId = dr["TableId"].ToString();
                    tableList.TableName = dr["TableName"].ToString();
                    switch (dr["Mode"].ToString())
                    {
                        case "1":
                            tableList.Mode = "主檔單頭";
                            break;
                        case "2":
                            tableList.Mode = "主檔單身";
                            break;
                        case "3":
                            tableList.Mode = "交易單頭";
                            break;
                        case "4":
                            tableList.Mode = "交易單身";
                            break;
                        case "5":
                            tableList.Mode = "交易明細";
                            break;
                        case "6":
                            tableList.Mode = "系統檔";
                            break;
                    }
                    tableList.ModuleId = dr["ModuleId"].ToString();
                    tableList.ModuleName = dr["ModuleName"].ToString();
                    tableList.Remark = dr["Remark"].ToString();
                    lst.Add(tableList);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        public ActionResult CUR(string action, string id, string type)
        {
            List<TableList> lstAddress = new List<TableList>();
            if (action == "btnSaveNew")
            {
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableList.TableName = Request.Form["TableName"];
                tableList.Mode = Request.Form["Mode"];
                tableList.ModuleId = Request.Form["ModuleId"];
                tableList.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string ColumnNo = Request.Form["txtColumnNo"];
                string ColumnName = Request.Form["txtColumnName"];
                string DataType = Request.Form["txtDataType"];
                string ColLength = Request.Form["txtColLength"];
                string Remark = Request.Form["txtRemark"];

                TableListLogic tableListLogic = new TableListLogic();
                tableListLogic.InsertCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);
             
                return RedirectToAction("Index", "Main");

            }
            if (action == "btnNextNew")
            {
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableList.TableName = Request.Form["TableName"];
                tableList.Mode = Request.Form["Mode"];
                tableList.ModuleId = Request.Form["ModuleId"];
                tableList.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string ColumnNo = Request.Form["txtColumnNo"];
                string ColumnName = Request.Form["txtColumnName"];
                string DataType = Request.Form["txtDataType"];
                string ColLength = Request.Form["txtColLength"];
                string Remark = Request.Form["txtRemark"];

                TableListLogic tableListLogic = new TableListLogic();
                tableListLogic.InsertCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);
         
                ViewBag.Type = "New";
                ViewData["DisplayData"] = lstAddress;
                return View();
            }
            if (action == "btnSaveEdit")
            {
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableList.TableName = Request.Form["TableName"];
                switch (Request.Form["Mode"])
                {
                    case "主檔單頭":
                        tableList.Mode = "1";
                        break;
                    case "主檔單身":
                        tableList.Mode = "2";
                        break;
                    case "交易單頭":
                        tableList.Mode = "3";
                        break;
                    case "交易單身":
                        tableList.Mode = "4";
                        break;
                    case "交易明細":
                        tableList.Mode = "5";
                        break;
                    case "系統檔":
                        tableList.Mode = "6";
                        break;
                }
                tableList.ModuleId = Request.Form["ModuleId"];
                tableList.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string ColumnNo = Request.Form["txtColumnNo"];
                string ColumnName = Request.Form["txtColumnName"];
                string DataType = Request.Form["txtDataType"];
                string ColLength = Request.Form["txtColLength"];
                string Remark = Request.Form["txtRemark"];

                TableListLogic tableListLogic = new TableListLogic();
                tableListLogic.UpdateCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableList.TableName = Request.Form["TableName"];
                switch (Request.Form["Mode"])
                {
                    case "主檔單頭":
                        tableList.Mode = "1";
                        break;
                    case "主檔單身":
                        tableList.Mode = "2";
                        break;
                    case "交易單頭":
                        tableList.Mode = "3";
                        break;
                    case "交易單身":
                        tableList.Mode = "4";
                        break;
                    case "交易明細":
                        tableList.Mode = "5";
                        break;
                    case "系統檔":
                        tableList.Mode = "6";
                        break;
                }
                tableList.ModuleId = Request.Form["ModuleId"];
                tableList.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string ColumnNo = Request.Form["txtColumnNo"];
                string ColumnName = Request.Form["txtColumnName"];
                string DataType = Request.Form["txtDataType"];
                string ColLength = Request.Form["txtColLength"];
                string Remark = Request.Form["txtRemark"];

                TableListLogic tableListLogic = new TableListLogic();
                tableListLogic.UpdateCurrency(tableList, AddressId, ColumnNo, ColumnName, DataType, ColLength, Remark);

                ViewBag.Type = "New";
                ViewData["DisplayData"] = lstAddress;
                return View();
            }
            if (action == "btnEdit")
            {
                TableListLogic tableListLogic = new TableListLogic();
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                DataTable dtDisplay = tableListLogic.SelectTableList(tableList, null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.TableId = dtDisplay.Rows[0]["TableId"].ToString();
                    ViewBag.TableName = dtDisplay.Rows[0]["TableName"].ToString();
                    ViewBag.Mode = dtDisplay.Rows[0]["Mode"].ToString();
                    switch (dtDisplay.Rows[0]["Mode"].ToString())
                    {
                        case "1":
                            ViewBag.Mode = "主檔單頭";
                            break;
                        case "2":
                            ViewBag.Mode = "主檔單身";
                            break;
                        case "3":
                            ViewBag.Mode = "交易單頭";
                            break;
                        case "4":
                            ViewBag.Mode = "交易單身";
                            break;
                        case "5":
                            ViewBag.Mode = "交易明細";
                            break;
                        case "6":
                            ViewBag.Mode = "系統檔";
                            break;
                    }
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewData["DisplayData"] = GetAddressInfo(tableList);
                ViewBag.Type = "Edit";
                return View();
            }
            if (action == "btnAdd")
            {
                TableListLogic tableListLogic = new TableListLogic();
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                DataTable dtDisplay = tableListLogic.SelectTableList(tableList, null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.TableId = dtDisplay.Rows[0]["TableId"].ToString();
                    ViewBag.TableName = dtDisplay.Rows[0]["TableName"].ToString();
                    ViewBag.Mode = dtDisplay.Rows[0]["Mode"].ToString();
                    switch (dtDisplay.Rows[0]["Mode"].ToString())
                    {
                        case "1":
                            ViewBag.Mode = "主檔單頭";
                            break;
                        case "2":
                            ViewBag.Mode = "主檔單身";
                            break;
                        case "3":
                            ViewBag.Mode = "交易單頭";
                            break;
                        case "4":
                            ViewBag.Mode = "交易單身";
                            break;
                        case "5":
                            ViewBag.Mode = "交易明細";
                            break;
                        case "6":
                            ViewBag.Mode = "系統檔";
                            break;
                    }
                    ViewBag.ModuleId= dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "New";
                ViewData["DisplayData"] = GetAddressInfo(tableList);
                return View();
            }
            if (action == "btnDelete")
            {
                TableListLogic tableListLogic = new TableListLogic();
                TableList tableList = new TableList();
                tableList.TableId = Request.Form["TableId"];
                tableListLogic.DeleteGroup(tableList.TableId);
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                TableListLogic tableListLogic = new TableListLogic();
                TableList tableList = new TableList();
                tableList.TableId = id;
                DataTable dtDisplay = tableListLogic.SelectTableList(tableList, null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.TableId = dtDisplay.Rows[0]["TableId"].ToString();
                    ViewBag.TableName = dtDisplay.Rows[0]["TableName"].ToString();
                    switch (dtDisplay.Rows[0]["Mode"].ToString())
                    {
                        case "1":
                            ViewBag.Mode = "主檔單頭";
                            break;
                        case "2":
                            ViewBag.Mode = "主檔單身";
                            break;
                        case "3":
                            ViewBag.Mode = "交易單頭";
                            break;
                        case "4":
                            ViewBag.Mode = "交易單身";
                            break;
                        case "5":
                            ViewBag.Mode = "交易明細";
                            break;
                        case "6":
                            ViewBag.Mode = "系統檔";
                            break;
                    }
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.ModuleName = dtDisplay.Rows[0]["ModuleName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewData["DisplayData"] = GetAddressInfo(tableList);
            }
            else
            {
                ViewData["DisplayData"] = lstAddress;
            }
            ViewBag.Type = type ?? "New";
            return View();
        }
        private List<TableList> GetAddressInfo(TableList tableList)
        {
            TableListLogic tableListLogic = new TableListLogic();
            TableList tableListInfo = null;
            DataTable dtAddress = tableListLogic.GetAddressInfo(tableList);
            List<TableList> lst = new List<TableList>();
            foreach (DataRow dr in dtAddress.Rows)
            {
                tableListInfo = new TableList();
                tableListInfo.ItemId = dr["ItemId"].ToString();
                tableListInfo.ColumnNo = dr["ColumnNo"].ToString();
                tableListInfo.ColumnName = dr["ColumnName"].ToString();
                tableListInfo.DataType = dr["DataType"].ToString();
                tableListInfo.ColLength = dr["ColLength"].ToString();
                tableListInfo.Remark = dr["Remark"].ToString();
                lst.Add(tableListInfo);
            }
            return lst;
        }
        public ActionResult Delete(string TableId)
        {
            TableListLogic tableListLogic = new TableListLogic();
            if (!tableListLogic.DeleteGroup(TableId))
            {
                ViewBag.Msg = "刪除失敗！";
            }
            return RedirectToAction("Index", "Main");
        }

        private string ExchangeDateToDisplay(string date)
        {
            if (date.Length == 0)
            {
                return "";
            }
            if (date.Length == 8)
            {
                return date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
            }
            return date;
        }

        [HttpPost]
        public JsonResult CURMID(string tableId)
        {
            TableListLogic logic = new TableListLogic();
            bool type = logic.IsModuleId(tableId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CURID(string ModuleId)
        {
            TableListLogic logic = new TableListLogic();
            bool type = logic.IsProgramId(ModuleId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
    }
}