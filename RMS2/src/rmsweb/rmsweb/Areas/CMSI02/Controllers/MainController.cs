using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Configuration;
using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using rmsweb.Controllers;

namespace CMSI02.Controllers
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
                    DepartmenLogic departmenLogic = new DepartmenLogic();
                    Department department = new Department();
                    department.DepartmentId = Request.Form["companyid"];
                    DataTable dtExport = departmenLogic.SelectDepartment(department, Request.Form["selectCondition"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //using (FileStream fs = new FileStream(path + @"\DepartmentInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("部門代號,部門名稱,備註");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["DepartmentId"].ToString() + "," + dr["DepartmentName"].ToString() + "," + dr["Remark"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}


                    using (FileStream fs = new FileStream(path + @"\DepartmentInfo.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("DepartmentInfo");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("部門代號");
                        row.CreateCell(1).SetCellValue("部門名稱");
                        row.CreateCell(2).SetCellValue("備註");

                        int index = 1;

                        for (int i = 0; i < dtExport.Rows.Count; i++)
                        {

                            row = sheet.CreateRow(index);
                            row.CreateCell(0).SetCellValue(dtExport.Rows[i]["DepartmentId"].ToString());
                            row.CreateCell(1).SetCellValue(dtExport.Rows[i]["DepartmentName"].ToString());
                            row.CreateCell(2).SetCellValue(dtExport.Rows[i]["Remark"].ToString());
                            index++;

                        }

                        workbook.Write(fs);
                    }

                    return File(new FileStream(path + @"\DepartmentInfo.xls", FileMode.Open), "text/plain", "DepartmentInfo.xls");
                    //return File(new FileStream(path + @"\DepartmentInfo.csv", FileMode.Open), "text/plain", "DepartmentInfo.csv");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                DepartmenLogic departmenLogic = new DepartmenLogic();
                DataTable dtDisplay=departmenLogic.SearchDetailDepartment(col, condition, value);
                List<Department> lst = new List<Department>();
                Department department = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    department = new Department();
                    department.DepartmentId = dr["DepartmentId"].ToString();
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    department.Remark = dr["Remark"].ToString();
                    lst.Add(department);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                Department department = new Department();
                department.DepartmentId = Request.Form["companyid"];
                DataTable dtDisplay = departmenLogic.SelectDepartment(department, Request.Form["selectCondition"]);
                List<Department> lst = new List<Department>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    department = new Department();
                    department.DepartmentId = dr["DepartmentId"].ToString();
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    department.Remark = dr["Remark"].ToString();
                    lst.Add(department);
                }
                ViewBag.CompanyId = Request.Form["companyid"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                DataTable dtDisplay = departmenLogic.SelectDepartment(null,null);
                List<Department> lst = new List<Department>();
                Department department = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    department = new Department();
                    department.DepartmentId = dr["DepartmentId"].ToString();
                    department.DepartmentName = dr["DepartmentName"].ToString();
                    department.Remark = dr["Remark"].ToString();
                    lst.Add(department);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        public ActionResult CUR(string action,string id,string type)
        {
            if (action == "btnSaveNew")
            {
                Department departmentSave = new Department();
                departmentSave.DepartmentId = Request.Form["deptid"];
                departmentSave.DepartmentName = Request.Form["deptname"];
                departmentSave.Remark = Request.Form["Remark"];
                DepartmenLogic departmenLogic = new DepartmenLogic();
                string returnString = departmenLogic.InsertDepartment(departmentSave);
                if (returnString == "1")//部門代號已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('部門代號已存在');</script>";
                    ViewBag.Type = "New";
                    return View();
                }
                if (returnString == "2")//相同部門名稱已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('相同部門名稱已存在');</script>";
                    ViewBag.Type = "New";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextNew")
            {
                Department departmentSave = new Department();
                departmentSave.DepartmentId = Request.Form["deptid"];
                departmentSave.DepartmentName = Request.Form["deptname"];
                departmentSave.Remark = Request.Form["Remark"];
                DepartmenLogic departmenLogic = new DepartmenLogic();
                string returnString = departmenLogic.InsertDepartment(departmentSave);
                if (returnString == "1")//部門代號已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('部門代號已存在');</script>";
                }
                if (returnString == "2")//相同部門名稱已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('相同部門名稱已存在');</script>";
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnSaveEdit")
            {
                Department departmentSave = new Department();
                departmentSave.DepartmentId = Request.Form["deptid"];
                departmentSave.DepartmentName = Request.Form["deptname"];
                departmentSave.Remark = Request.Form["Remark"];
                DepartmenLogic departmenLogic = new DepartmenLogic();
                string returnString = departmenLogic.UpdateDepartment(departmentSave);
                if (returnString == "1")//部門代號不存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('部門代號不存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")//相同部門名稱已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('相同部門名稱已存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                Department departmentSave = new Department();
                departmentSave.DepartmentId = Request.Form["deptid"];
                departmentSave.DepartmentName = Request.Form["deptname"];
                departmentSave.Remark = Request.Form["Remark"];
                DepartmenLogic departmenLogic = new DepartmenLogic();
                string returnString = departmenLogic.UpdateDepartment(departmentSave);
                if (returnString == "1")//部門代號不存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('部門代號不存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")//相同部門名稱已存在
                {
                    ViewBag.ID = departmentSave.DepartmentId;
                    ViewBag.Name = departmentSave.DepartmentName;
                    ViewBag.Remark = departmentSave.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('相同部門名稱已存在');</script>";
                    ViewBag.Type = "Edit";
                    return View();
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnEdit")
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                Department department = new Department();
                department.DepartmentId = Request.Form["deptid"];
                DataTable dtDisplay = departmenLogic.SelectDepartment(department,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ID = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "Edit";
                return View();
            }
            if (action == "btnAdd")
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                Department department = new Department();
                department.DepartmentId = Request.Form["deptid"];
                DataTable dtDisplay = departmenLogic.SelectDepartment(department,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ID = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnDelete")
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                Department department = new Department();
                department.DepartmentId = Request.Form["deptid"];
                department.DepartmentName = Request.Form["deptname"];
                department.Remark = Request.Form["remark"];
                if (!departmenLogic.DeleteDepartment(department))
                {
                    ViewBag.ID = department.DepartmentId;
                    ViewBag.Name = department.DepartmentName;
                    ViewBag.Remark = department.Remark;
                    ViewBag.js = "<script type='text/javascript'>alert('部門代號不存在');</script>";
                    ViewBag.Type = "Detail";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                DepartmenLogic departmenLogic = new DepartmenLogic();
                Department department = new Department();
                department.DepartmentId = id;
                DataTable dtDisplay = departmenLogic.SelectDepartment(department,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.ID = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
            }
            ViewBag.Type = type??"New";
            return View();
        }

        public ActionResult Delete(string id)
        {
            DepartmenLogic departmenLogic = new DepartmenLogic();
            Department department = new Department();
            department.DepartmentId = id;
            if (!departmenLogic.DeleteDepartment(department))
            {
                ViewBag.js = "<script type='text/javascript'>alert('部門代號不存在');</script>";
            }
            return RedirectToAction("Index", "Main");
        }

        [HttpPost]
        public JsonResult CURMID(string deptid)
        {
            DepartmenLogic logic = new DepartmenLogic();
            bool type = logic.IsModuleId(deptid);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
    }
}