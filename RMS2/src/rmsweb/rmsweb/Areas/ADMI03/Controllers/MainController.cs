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

namespace ADMI03.Controllers
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
                CurrencyLogic currencyLogic = new CurrencyLogic();
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                DataTable dtDisplay = currencyLogic.SelectCurrency(currency, Request.Form["selectCondition"]);
                List<Currency> lst = new List<Currency>();
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
                ViewBag.CurrencyId = Request.Form["currencyId"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                CurrencyLogic currencyLogic = new CurrencyLogic();
                DataTable dtDisplay = currencyLogic.SelectCurrency(null,null);
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
            }
            return View();
        }

        public ActionResult CUR(string action, string id, string type)
        {
            if (action == "btnSaveNew")
            {
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                currency.CurrencyName = Request.Form["currencyName"];
                currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
                currency.ExchangeRate = Request.Form["rate"];

                CurrencyLogic currencyLogic = new CurrencyLogic();
                string returnString = currencyLogic.InsertCurrency(currency);
                if (returnString != "0")
                {
                    if (returnString == "1")
                    {
                        ViewBag.CurrencyId = currency.CurrencyId;
                        ViewBag.CurrencyName = currency.CurrencyName;
                        ViewBag.EffectiveDate = Request.Form["availableDate"];
                        ViewBag.ExchangeRate = currency.ExchangeRate;
                        ViewBag.js = "<script type='text/javascript'>alert('幣別代號已存在');</script>";
                        ViewBag.Type = "New";
                        return View();
                    }
                    if (returnString == "2")
                    {
                        ViewBag.CurrencyId = currency.CurrencyId;
                        ViewBag.CurrencyName = currency.CurrencyName;
                        ViewBag.EffectiveDate = Request.Form["availableDate"];
                        ViewBag.ExchangeRate = currency.ExchangeRate;
                        ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
                        ViewBag.Type = "New";
                        return View();
                    }
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextNew")
            {
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                currency.CurrencyName = Request.Form["currencyName"];
                currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
                currency.ExchangeRate = Request.Form["rate"];

                CurrencyLogic currencyLogic = new CurrencyLogic();
                string returnString = currencyLogic.InsertCurrency(currency);
                if (returnString != "0")
                {
                    if (returnString == "1")
                    {
                        ViewBag.CurrencyId = currency.CurrencyId;
                        ViewBag.CurrencyName = currency.CurrencyName;
                        ViewBag.EffectiveDate = Request.Form["availableDate"];
                        ViewBag.ExchangeRate = currency.ExchangeRate;
                        ViewBag.js = "<script type='text/javascript'>alert('幣別代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.CurrencyId = currency.CurrencyId;
                        ViewBag.CurrencyName = currency.CurrencyName;
                        ViewBag.EffectiveDate = Request.Form["availableDate"];
                        ViewBag.ExchangeRate = currency.ExchangeRate;
                        ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
                    }
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnSaveEdit")
            {
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                currency.CurrencyName = Request.Form["currencyName"];
                currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
                currency.ExchangeRate = Request.Form["rate"];
                CurrencyLogic currencyLogic = new CurrencyLogic();
                string returnString = currencyLogic.UpdateCurrency(currency);
                if (returnString == "1")
                {
                    ViewBag.CurrencyId = currency.CurrencyId;
                    ViewBag.CurrencyName = currency.CurrencyName;
                    ViewBag.EffectiveDate = Request.Form["availableDate"];
                    ViewBag.ExchangeRate = currency.ExchangeRate;
                    ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")
                {
                    ViewBag.CurrencyId = currency.CurrencyId;
                    ViewBag.CurrencyName = currency.CurrencyName;
                    ViewBag.EffectiveDate = Request.Form["availableDate"];
                    ViewBag.ExchangeRate = currency.ExchangeRate;
                    ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                currency.CurrencyName = Request.Form["currencyName"];
                currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
                currency.ExchangeRate = Request.Form["rate"];
                CurrencyLogic currencyLogic = new CurrencyLogic();
                string returnString = currencyLogic.UpdateCurrency(currency);
                if (returnString == "1")
                {
                    ViewBag.CurrencyId = currency.CurrencyId;
                    ViewBag.CurrencyName = currency.CurrencyName;
                    ViewBag.EffectiveDate = Request.Form["availableDate"];
                    ViewBag.ExchangeRate = currency.ExchangeRate;
                    ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")
                {
                    ViewBag.CurrencyId = currency.CurrencyId;
                    ViewBag.CurrencyName = currency.CurrencyName;
                    ViewBag.EffectiveDate = Request.Form["availableDate"];
                    ViewBag.ExchangeRate = currency.ExchangeRate;
                    ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnEdit")
            {
                CurrencyLogic currencyLogic = new CurrencyLogic();
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"]; ;
                DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
                    ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
                    ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
                    ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
                }
                ViewBag.Type = "Edit";
                return View();
            }
            if (action == "btnAdd")
            {
                CurrencyLogic currencyLogic = new CurrencyLogic();
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
                    ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
                    ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
                    ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnDelete")
            {
                CurrencyLogic currencyLogic = new CurrencyLogic();
                Currency currency = new Currency();
                currency.CurrencyId = Request.Form["currencyId"];
                if (!currencyLogic.DeleteCurrency(currency))
                {
                    ViewBag.CurrencyId = Request.Form["currencyId"];
                    ViewBag.CurrencyName = Request.Form["currencyName"];
                    ViewBag.EffectiveDate = Request.Form["availableDate"];
                    ViewBag.ExchangeRate = Request.Form["rate"];
                    ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
                    ViewBag.Type = "Detail";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                CurrencyLogic currencyLogic = new CurrencyLogic();
                Currency currency = new Currency();
                currency.CurrencyId = id;
                DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
                    ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
                    ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
                    ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
                }
            }
            ViewBag.Type = type ?? "New";
            return View();
        }

        public ActionResult Delete(string id)
        {
            CurrencyLogic currencyLogic = new CurrencyLogic();
            Currency currency = new Currency();
            currency.CurrencyId = id;
            if (!currencyLogic.DeleteCurrency(currency))
            {
                ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
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
    }
}