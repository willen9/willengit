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
using rmsweb.Controllers;

namespace ADMI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main 
        //public ActionResult Index()
        //{
        //    ProgramLogic logic = new ProgramLogic();
        //    Program program = new Program();
        //    //ViewData["Program"] = logic.GetProgram(program, 0);
        //    return View();
        //}

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
                    ProgramLogic programLogic = new ProgramLogic();
                    Program program = new Program();
                    program.ProgId = Request.Form["ProgramId"];
                    DataTable dtExport = programLogic.SelectCurrency(program, Request.Form["selectCondition"], Request.Form["selectCondition1"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //using (
                    //    FileStream fs = new FileStream(path + @"\ProgramInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("程式代號,程式名稱,模組代號,備註");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["ProgId"].ToString() + "," + dr["ProgName"].ToString() + "," +
                    //                     dr["ModuleId"].ToString() + "," + dr["Remark"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}


                    using (FileStream fs = new FileStream(path + @"\ProgramInfo.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("ProgramInfo");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);
                        sheet.SetColumnWidth(3, 20 * 256);
                        
                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("程式代號");
                        row.CreateCell(1).SetCellValue("程式名稱");
                        row.CreateCell(2).SetCellValue("模組代號");
                        row.CreateCell(3).SetCellValue("備註");

                        int index = 1;

                        for (int i = 0; i < dtExport.Rows.Count; i++)
                        {

                            row = sheet.CreateRow(index);
                            row.CreateCell(0).SetCellValue(dtExport.Rows[i]["ProgId"].ToString());
                            row.CreateCell(1).SetCellValue(dtExport.Rows[i]["ProgName"].ToString());
                            row.CreateCell(2).SetCellValue(dtExport.Rows[i]["ModuleId"].ToString());
                            row.CreateCell(3).SetCellValue(dtExport.Rows[i]["Remark"].ToString());
                            index++;

                        }

                        workbook.Write(fs);
                    }


                    //return File(new FileStream(path + @"\ProgramInfo.csv", FileMode.Open), "text/plain",
                    //    "ProgramInfo.csv");
                    return File(new FileStream(path + @"\ProgramInfo.xls", FileMode.Open), "text/plain",
                  "ProgramInfo.xls");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                ProgramLogic currencyLogic = new ProgramLogic();
                DataTable dtDisplay = currencyLogic.SearchDetailProgram(col, condition, value);
                List<Program> lst = new List<Program>();
                Program program = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    program = new Program();
                    program.ProgId = dr["ProgId"].ToString();
                    program.ProgName = dr["ProgName"].ToString();
                    program.ModuleId = dr["ModuleId"].ToString();
                    program.ModuleName = dr["ModuleName"].ToString();
                    program.Remark = dr["Remark"].ToString();
                    lst.Add(program);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                ProgramLogic programLogic = new ProgramLogic();
                Program program = new Program();
                program.ProgId = Request.Form["txtProgId"];
                program.ProgName = Request.Form["txtProgName"];
                DataTable dtDisplay = programLogic.SelectCurrency(program,Request.Form["selectCondition"], Request.Form["selectCondition1"]);
                List<Program> lst = new List<Program>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    program = new Program();
                    program.ProgId = dr["ProgId"].ToString();
                    program.ProgName = dr["ProgName"].ToString();
                    program.ModuleId = dr["ModuleId"].ToString();
                    program.ModuleName = dr["ModuleName"].ToString();
                    program.Remark = dr["Remark"].ToString();
                    lst.Add(program);
                }
                ViewBag.ProgId = Request.Form["txtProgId"];
                ViewBag.ProgName= Request.Form["txtProgName"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewBag.SelectCondition1 = Request.Form["selectCondition1"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                ProgramLogic programLogic = new ProgramLogic();
                DataTable dtDisplay = programLogic.SelectCurrency(null,null,null);
                List<Program> lst = new List<Program>();
                Program program = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    program = new Program();
                    program.ProgId = dr["ProgId"].ToString();
                    program.ProgName = dr["ProgName"].ToString();
                    program.ModuleId = dr["ModuleId"].ToString();
                    program.ModuleName = dr["ModuleName"].ToString();
                    program.Remark = dr["Remark"].ToString();
                    lst.Add(program);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        [HttpPost]
        public JsonResult CURID(string ProgramId)
        {
            ProgramLogic logic = new ProgramLogic();
            bool type = logic.IsProgramId(ProgramId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CURMID(string ModuleId)
        {
            ProgramLogic logic = new ProgramLogic();
            bool type = logic.IsModuleId(ModuleId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string ProgId)
        {
            ProgramLogic logic = new ProgramLogic();

            if (!logic.DeleteProgram(ProgId))
            {
                ViewBag.Msg = "刪除失敗！";
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult CUR(string action, string id, string type)
        {
            Program program = new Program();
            program.ProgId = Request.Form["ProgramId"];
            program.ProgName = Request.Form["ProgramName"];
            program.ModuleId = Request.Form["ModuleId"];
            program.Remark = Request.Form["Remark"];
            ProgramLogic programLogic = new ProgramLogic();
            if (action == "btnEdit")
            {
                DataTable dtDisplay = programLogic.SelectCurrency(program, null, null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ProgramId = dtDisplay.Rows[0]["ProgramId"].ToString();
                    ViewBag.ProgramName = dtDisplay.Rows[0]["ProgramName"].ToString();
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "Edit";
                return View();
            }
            if (Request.Form["action"] == "ADD" || Request.Form["action"] == "SaveAgain")
            {
                string msgInfo = "";
                string msg = "";
                if (!programLogic.AddProgram(program, out msgInfo))
                {
                    ViewBag.ProgramId = Request.Form["ProgramId"];
                    ViewBag.ProgramName = Request.Form["ProgramName"];
                    ViewBag.ModuleId = Request.Form["ModuleId"];
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

                if (!programLogic.UpdateProgram(program))
                {
                    program.ProgId = Request.Form["ProgramId"];
                    program.ProgName = Request.Form["ProgramName"];
                    program.ModuleId = Request.Form["ModuleId"];
                    program.Remark = Request.Form["Remark"];
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

            if (Request.Form["action"] == "EditDetails")
            {
                ViewBag.ProgramId= Request.Form["ProgramId"];
                ViewBag.ProgramName= Request.Form["ProgramName"];
                ViewBag.ModuleId = Request.Form["ModuleId"];
                ViewBag.Remark = Request.Form["Remark"];
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }

            if (!string.IsNullOrEmpty(id))
            {
                program.ProgId = id;
                DataTable dtDisplay = programLogic.SelectCurrency(program, null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ProgramId = dtDisplay.Rows[0]["ProgId"].ToString();
                    ViewBag.ProgramName = dtDisplay.Rows[0]["ProgName"].ToString();
                    ViewBag.ModuleId = dtDisplay.Rows[0]["ModuleId"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
            }
            ViewBag.Type = type ?? "New";
            return View();
        }
        public ActionResult Edit(string ProgId)
        {
            ProgramLogic logic = new ProgramLogic();
            Program program = logic.GetProgram(ProgId);
            ViewBag.ProgramId = program.ProgId;
            ViewBag.ProgramName = program.ProgName;
            ViewBag.ModuleId = program.ModuleId;
            ViewBag.Remark = program.Remark;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

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