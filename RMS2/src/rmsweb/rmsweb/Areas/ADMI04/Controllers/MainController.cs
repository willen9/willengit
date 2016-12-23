using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace ADMI04.Controllers
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
                    ModuleLogic currencyLogic = new ModuleLogic();
                    Module currency = new Module();
                    currency.ModuleId = Request.Form["ModuleId"];
                    DataTable dtExport = currencyLogic.SelectModule(currency,Request.Form["selectCondition"], Request.Form["selectCondition1"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    #region
                    //using (
                    //    FileStream fs = new FileStream(path + @"\CurrencyInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("幣別代號,幣別名稱,生效日期,匯率");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["CurrencyId"].ToString() + "," + dr["Currency"].ToString() + "," +
                    //                     dr["EffectiveDate"].ToString() + "," + dr["ExchangeRate"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}
                    //return File(new FileStream(path + @"\CurrencyInfo.csv", FileMode.Open), "text/plain",
                    //    "CurrencyInfo.csv"); 
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
                        //幣別代號,幣別名稱,生效日期,匯率
                        row.CreateCell(0).SetCellValue("模組代號");
                        row.CreateCell(1).SetCellValue("模組名稱");
                        row.CreateCell(2).SetCellValue("備註");
                        //row.CreateCell(3).SetCellValue("匯率");
                        int i = 0;
                        foreach (DataRow dr in dtExport.Rows)
                        {
                            i++;
                            row = sheet.CreateRow(i);
                            row.CreateCell(0).SetCellValue(dr["ModuleId"].ToString());
                            row.CreateCell(1).SetCellValue(dr["ModuleName"].ToString());
                            row.CreateCell(2).SetCellValue(dr["Remark"].ToString());
                            //row.CreateCell(3).SetCellValue(dr["ExchangeRate"].ToString());
                        }
                        book.Write(ms);
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        ms.Close();
                        ms.Dispose();
                    }

                    return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "ModuleInfo.xls");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                ModuleLogic currencyLogic = new ModuleLogic();
                DataTable dtDisplay = currencyLogic.SearchDetailModule(col, condition, value);
                List<Module> lst = new List<Module>();
                Module module = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    module = new Module();
                    module.ModuleId = dr["ModuleId"].ToString();
                    module.ModuleName = dr["ModuleName"].ToString();
                    module.Remark = dr["Remark"].ToString();
                    lst.Add(module);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                ModuleLogic moduleLogic = new ModuleLogic();
                Module module = new Module();
                module.ModuleId = Request.Form["ModuleId"];
                DataTable dtDisplay = moduleLogic.SelectModule(module, Request.Form["selectCondition"], Request.Form["selectCondition1"]);
                List<Module> lst = new List<Module>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    module = new Module();
                    module.ModuleId = dr["ModuleId"].ToString();
                    module.ModuleName = dr["ModuleName"].ToString();
                    module.Remark = dr["Remark"].ToString();
                    lst.Add(module);
                }
                ViewBag.ModuleId = Request.Form["ModuleId"];
                ViewBag.ModuleName= Request.Form["ModuleName"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewBag.SelectCondition1 = Request.Form["selectCondition1"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                ModuleLogic ModuleLogic = new ModuleLogic();
                DataTable dtDisplay = ModuleLogic.SelectModule(null,null,null);
                List<Module> lst = new List<Module>();
                Module module = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    module = new Module();
                    module.ModuleId = dr["ModuleId"].ToString();
                    module.ModuleName = dr["ModuleName"].ToString();
                    module.Remark = dr["Remark"].ToString();
                    lst.Add(module);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        //刪除
        public ActionResult Delete(string ModuleId)
        {
            ModuleLogic logic = new ModuleLogic();

            if (!logic.DeleteProgram(ModuleId))
            {
                ViewBag.Msg = "刪除失敗！";
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult CUR(string action, string id, string type)
        {
            Module module = new Module();
            module.ModuleId = Request.Form["ModuleId"];
            module.ModuleName = Request.Form["ModuleName"];
            module.Remark = Request.Form["Remark"];
            ModuleLogic moduleLogic = new ModuleLogic();
            if (action == "btnEdit")
            {
                DataTable dtDisplay = moduleLogic.SelectModule(module, null, null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.ModuleName = dtDisplay.Rows[0]["ModuleName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "Edit";
                return View();
            }
            if (Request.Form["action"] == "ADD" || Request.Form["action"] == "SaveAgain")
            {
                string msgInfo = "";
                string msg = "";
                if (!moduleLogic.AddModule(module, out msgInfo))
                {
                    ViewBag.ModuleId = Request.Form["ModuleId"];
                    ViewBag.ModuleName = Request.Form["ModuleName"];
                    ViewBag.Remark = Request.Form["Remark"];
                    ViewBag.Msg = "新增失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "ADD")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }
            }
            if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
            {
                string msg = "";

                if (!moduleLogic.UpdateModule(module))
                {
                    module.ModuleId = Request.Form["ModuleId"];
                    module.ModuleName = Request.Form["ModuleName"];
                    module.Remark = Request.Form["Remark"];
                    ViewBag.Msg = "修改失敗！";
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

            if(Request.Form["action"] == "EditDetails")
            {
                ViewBag.ModuleId = Request.Form["ModuleId"];
                ViewBag.ModuleName = Request.Form["ModuleName"];
                ViewBag.Remark = Request.Form["Remark"];
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }

            if (!string.IsNullOrEmpty(id))
            {
                module.ModuleId = id;
                DataTable dtDisplay = moduleLogic.SelectModule(module, null, null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.ModuleName = dtDisplay.Rows[0]["ModuleName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
            }
            ViewBag.Type = type ?? "New";
            return View();
        }

        [HttpPost]
        public JsonResult CURMID(string ModuleId)
        {
            ModuleLogic logic = new ModuleLogic();
            bool type = logic.IsModuleId(ModuleId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult CUR(string action, string id, string type)
        //{
        //    if (action == "btnSaveNew")
        //    {
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        currency.CurrencyName = Request.Form["currencyName"];
        //        currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
        //        currency.ExchangeRate = Request.Form["rate"];

        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        string returnString = currencyLogic.InsertCurrency(currency);
        //        if (returnString != "0")
        //        {
        //            if (returnString == "1")
        //            {
        //                ViewBag.CurrencyId = currency.CurrencyId;
        //                ViewBag.CurrencyName = currency.CurrencyName;
        //                ViewBag.EffectiveDate = Request.Form["availableDate"];
        //                ViewBag.ExchangeRate = currency.ExchangeRate;
        //                ViewBag.js = "<script type='text/javascript'>alert('幣別代號已存在');</script>";
        //                ViewBag.Type = "New";
        //                return View();
        //            }
        //            if (returnString == "2")
        //            {
        //                ViewBag.CurrencyId = currency.CurrencyId;
        //                ViewBag.CurrencyName = currency.CurrencyName;
        //                ViewBag.EffectiveDate = Request.Form["availableDate"];
        //                ViewBag.ExchangeRate = currency.ExchangeRate;
        //                ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
        //                ViewBag.Type = "New";
        //                return View();
        //            }
        //        }
        //        return RedirectToAction("Index", "Main");
        //    }
        //    if (action == "btnNextNew")
        //    {
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        currency.CurrencyName = Request.Form["currencyName"];
        //        currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
        //        currency.ExchangeRate = Request.Form["rate"];

        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        string returnString = currencyLogic.InsertCurrency(currency);
        //        if (returnString != "0")
        //        {
        //            if (returnString == "1")
        //            {
        //                ViewBag.CurrencyId = currency.CurrencyId;
        //                ViewBag.CurrencyName = currency.CurrencyName;
        //                ViewBag.EffectiveDate = Request.Form["availableDate"];
        //                ViewBag.ExchangeRate = currency.ExchangeRate;
        //                ViewBag.js = "<script type='text/javascript'>alert('幣別代號已存在');</script>";
        //            }
        //            if (returnString == "2")
        //            {
        //                ViewBag.CurrencyId = currency.CurrencyId;
        //                ViewBag.CurrencyName = currency.CurrencyName;
        //                ViewBag.EffectiveDate = Request.Form["availableDate"];
        //                ViewBag.ExchangeRate = currency.ExchangeRate;
        //                ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
        //            }
        //        }
        //        ViewBag.Type = "New";
        //        return View();
        //    }
        //    if (action == "btnSaveEdit")
        //    {
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        currency.CurrencyName = Request.Form["currencyName"];
        //        currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
        //        currency.ExchangeRate = Request.Form["rate"];
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        string returnString = currencyLogic.UpdateCurrency(currency);
        //        if (returnString == "1")
        //        {
        //            ViewBag.CurrencyId = currency.CurrencyId;
        //            ViewBag.CurrencyName = currency.CurrencyName;
        //            ViewBag.EffectiveDate = Request.Form["availableDate"];
        //            ViewBag.ExchangeRate = currency.ExchangeRate;
        //            ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
        //            ViewBag.Type = "Edit";
        //            return View();
        //        }
        //        if (returnString == "2")
        //        {
        //            ViewBag.CurrencyId = currency.CurrencyId;
        //            ViewBag.CurrencyName = currency.CurrencyName;
        //            ViewBag.EffectiveDate = Request.Form["availableDate"];
        //            ViewBag.ExchangeRate = currency.ExchangeRate;
        //            ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
        //            ViewBag.Type = "Edit";
        //            return View();
        //        }
        //        return RedirectToAction("Index", "Main");
        //    }
        //    if (action == "btnNextEdit")
        //    {
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        currency.CurrencyName = Request.Form["currencyName"];
        //        currency.EffectiveDate = Request.Form["availableDate"].Length > 0 ? DateTime.Parse(Request.Form["availableDate"]).ToString("yyyyMMdd") : "";
        //        currency.ExchangeRate = Request.Form["rate"];
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        string returnString = currencyLogic.UpdateCurrency(currency);
        //        if (returnString == "1")
        //        {
        //            ViewBag.CurrencyId = currency.CurrencyId;
        //            ViewBag.CurrencyName = currency.CurrencyName;
        //            ViewBag.EffectiveDate = Request.Form["availableDate"];
        //            ViewBag.ExchangeRate = currency.ExchangeRate;
        //            ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
        //            ViewBag.Type = "Edit";
        //            return View();
        //        }
        //        if (returnString == "2")
        //        {
        //            ViewBag.CurrencyId = currency.CurrencyId;
        //            ViewBag.CurrencyName = currency.CurrencyName;
        //            ViewBag.EffectiveDate = Request.Form["availableDate"];
        //            ViewBag.ExchangeRate = currency.ExchangeRate;
        //            ViewBag.js = "<script type='text/javascript'>alert('幣別名稱已存在');</script>";
        //            ViewBag.Type = "Edit";
        //            return View();
        //        }
        //        ViewBag.Type = "New";
        //        return View();
        //    }
        //    if (action == "btnEdit")
        //    {
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"]; ;
        //        DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
        //        if (dtDisplay.Rows.Count > 0)
        //        {
        //            ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
        //            ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
        //            ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
        //            ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
        //        }
        //        ViewBag.Type = "Edit";
        //        return View();
        //    }
        //    if (action == "btnAdd")
        //    {
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
        //        if (dtDisplay.Rows.Count > 0)
        //        {
        //            ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
        //            ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
        //            ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
        //            ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
        //        }
        //        ViewBag.Type = "New";
        //        return View();
        //    }
        //    if (action == "btnDelete")
        //    {
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        Currency currency = new Currency();
        //        currency.CurrencyId = Request.Form["currencyId"];
        //        if (!currencyLogic.DeleteCurrency(currency))
        //        {
        //            ViewBag.CurrencyId = Request.Form["currencyId"];
        //            ViewBag.CurrencyName = Request.Form["currencyName"];
        //            ViewBag.EffectiveDate = Request.Form["availableDate"];
        //            ViewBag.ExchangeRate = Request.Form["rate"];
        //            ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
        //            ViewBag.Type = "Detail";
        //            return View();
        //        }
        //        return RedirectToAction("Index", "Main");
        //    }
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        CurrencyLogic currencyLogic = new CurrencyLogic();
        //        Currency currency = new Currency();
        //        currency.CurrencyId = id;
        //        DataTable dtDisplay = currencyLogic.SelectCurrency(currency,null);
        //        if (dtDisplay.Rows.Count > 0)
        //        {
        //            ViewBag.CurrencyId = dtDisplay.Rows[0]["CurrencyId"].ToString();
        //            ViewBag.CurrencyName = dtDisplay.Rows[0]["Currency"].ToString();
        //            ViewBag.EffectiveDate = ExchangeDateToDisplay(dtDisplay.Rows[0]["EffectiveDate"].ToString());
        //            ViewBag.ExchangeRate = dtDisplay.Rows[0]["ExchangeRate"].ToString();
        //        }
        //    }
        //    ViewBag.Type = type ?? "New";
        //    return View();
        //}

        //public ActionResult Delete(string id)
        //{
        //    CurrencyLogic currencyLogic = new CurrencyLogic();
        //    Currency currency = new Currency();
        //    currency.CurrencyId = id;
        //    if (!currencyLogic.DeleteCurrency(currency))
        //    {
        //        ViewBag.js = "<script type='text/javascript'>alert('幣別代號不存在');</script>";
        //    }
        //    return RedirectToAction("Index", "Main");
        //}

        //private string ExchangeDateToDisplay(string date)
        //{
        //    if (date.Length == 0)
        //    {
        //        return "";
        //    }
        //    if (date.Length == 8)
        //    {
        //        return date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
        //    }
        //    return date;
        //}
    }
}