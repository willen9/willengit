using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;

namespace CMSI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index(string action)
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
                string path = Server.MapPath(@"~\ImportFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = hfc[0].FileName;
                //判断文件名字是否包含路径名，如果有则提取文件名
                if (name.LastIndexOf("\\") > -1)
                {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                }
                //Stream stream = hfc[0].InputStream;
                //using ( FileStream fs = new FileStream(path + "\\" + name, FileMode.Create, FileAccess.Write))
                //{
                //    StreamReader sr = new StreamReader(stream, Encoding.Default);
                //    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                //    while (sr.Peek()>-1)
                //    {
                //        sw.WriteLine(sr.ReadLine());
                //    }
                //    sr.Close();
                //    sw.Close();
                //}

                hfc[0].SaveAs(path + "\\" + name);
                EmployeeLogic employeeLogic = new EmployeeLogic();
                //return Json(Encoding.Default.EncodingName, "text/x-json");
                if (!employeeLogic.ImportFile(path + "\\" + name))
                {
                    return Json("匯入失敗", "text/x-json");
                }
                
                return Json("1", "text/x-json");
            }
            if (action == "btnAdd")
            {
                return RedirectToAction("CUR", "Main");
            }
            if (action == "btnExport")
            {
                object obj = new object();
                lock (obj)
                {
                    EmployeeLogic employeeLogic = new EmployeeLogic();
                    Employee employee = new Employee();
                    employee.EmployeeId = Request.Form["employeeId"];
                    DataTable dtExport = employeeLogic.SelectEmployee(employee, Request.Form["selectCondition"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //using (FileStream fs = new FileStream(path + @"\EmployeeInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("員工代號,員工姓名,部門名稱,電話號碼,手機號碼,通訊地址");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["EmployeeId"].ToString() + "," + dr["EmployeeName"].ToString() + "," + dr["DepartmentName"].ToString() + "," + dr["TelNo"].ToString() + "," + dr["MobilePhone"].ToString() + "," + dr["Address"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}

                    using (FileStream fs = new FileStream(path + @"\EmployeeInfo.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("EmployeeInfo");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);
                        sheet.SetColumnWidth(3, 20 * 256);
                        sheet.SetColumnWidth(4, 20 * 256);
                        sheet.SetColumnWidth(5, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("員工代號");
                        row.CreateCell(1).SetCellValue("員工姓名");
                        row.CreateCell(2).SetCellValue("部門名稱");
                        row.CreateCell(3).SetCellValue("電話號碼");
                        row.CreateCell(4).SetCellValue("手機號碼");
                        row.CreateCell(5).SetCellValue("通訊地址");


                        int index = 1;

                        for (int i = 0; i < dtExport.Rows.Count; i++)
                        {
                            row = sheet.CreateRow(index);
                            row.CreateCell(0).SetCellValue(dtExport.Rows[i]["EmployeeId"].ToString());
                            row.CreateCell(1).SetCellValue(dtExport.Rows[i]["EmployeeName"].ToString());
                            row.CreateCell(2).SetCellValue(dtExport.Rows[i]["DepartmentName"].ToString());
                            row.CreateCell(3).SetCellValue(dtExport.Rows[i]["TelNo"].ToString());
                            row.CreateCell(4).SetCellValue(dtExport.Rows[i]["MobilePhone"].ToString());
                            row.CreateCell(5).SetCellValue(dtExport.Rows[i]["Address"].ToString());
                            index++;

                        }

                        workbook.Write(fs);
                    }
                    //return File(new FileStream(path + @"\EmployeeInfo.csv", FileMode.Open), "text/plain", "EmployeeInfo.csv");
                    return File(new FileStream(path + @"\EmployeeInfo.xls", FileMode.Open), "text/plain", "EmployeeInfo.xls");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                EmployeeLogic employeeLogic = new EmployeeLogic();
                DataTable dtDisplay = employeeLogic.SearchDetailEmployee(col, condition, value);
                List<Employee> lst = new List<Employee>();
                Employee employee = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    employee = new Employee();
                    employee.Company = dr["Company"].ToString();
                    employee.UserGroup = dr["UserGroup"].ToString();
                    employee.Creator = dr["Creator"].ToString();
                    employee.CreateDate = dr["CreateDate"].ToString();
                    employee.Modifier = dr["Modifier"].ToString();
                    employee.ModiDate = dr["ModiDate"].ToString();
                    employee.EmployeeId = dr["EmployeeId"].ToString();
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.TelNo = dr["TelNo"].ToString();
                    employee.MobilePhone = dr["MobilePhone"].ToString();
                    employee.DepartmentId = dr["DepartmentId"].ToString();
                    employee.DepartmentName = dr["DepartmentName"].ToString();
                    employee.Address = dr["Address"].ToString();
                    lst.Add(employee);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                DataTable dtDisplay = employeeLogic.SelectEmployee(employee, Request.Form["selectCondition"]);
                List<Employee> lst = new List<Employee>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    employee = new Employee();
                    employee.Company = dr["Company"].ToString();
                    employee.UserGroup = dr["UserGroup"].ToString();
                    employee.Creator = dr["Creator"].ToString();
                    employee.CreateDate = dr["CreateDate"].ToString();
                    employee.Modifier = dr["Modifier"].ToString();
                    employee.ModiDate = dr["ModiDate"].ToString();
                    employee.EmployeeId = dr["EmployeeId"].ToString();
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.TelNo = dr["TelNo"].ToString();
                    employee.MobilePhone = dr["MobilePhone"].ToString();
                    employee.DepartmentId = dr["DepartmentId"].ToString();
                    employee.DepartmentName = dr["DepartmentName"].ToString();
                    employee.Address = dr["Address"].ToString();
                    lst.Add(employee);
                }
                ViewBag.EmployeeId = Request.Form["employeeId"];
                ViewBag.SelectCondition = Request.Form["selectCondition"];
                ViewData["DisplayData"] = lst;
            }
            else
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                DataTable dtDisplay = employeeLogic.SelectEmployee(null,null);
                List<Employee> lst = new List<Employee>();
                Employee employee = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    employee = new Employee();
                    employee.Company = dr["Company"].ToString();
                    employee.UserGroup = dr["UserGroup"].ToString();
                    employee.Creator = dr["Creator"].ToString();
                    employee.CreateDate = dr["CreateDate"].ToString();
                    employee.Modifier = dr["Modifier"].ToString();
                    employee.ModiDate = dr["ModiDate"].ToString();
                    employee.EmployeeId = dr["EmployeeId"].ToString().Trim();
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.TelNo = dr["TelNo"].ToString();
                    employee.MobilePhone = dr["MobilePhone"].ToString();
                    employee.DepartmentId = dr["DepartmentId"].ToString();
                    employee.DepartmentName = dr["DepartmentName"].ToString();
                    employee.Address = dr["Address"].ToString();
                    lst.Add(employee);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        public ActionResult CUR(string action, string id, string type)
        {
            if (action == "btnSaveNew")
            {
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                employee.EmployeeName = Request.Form["employeeName"];
                employee.DepartmentId = Request.Form["deptId"];
                employee.DepartmentName = Request.Form["dept"];
                employee.Address = Request.Form["address"];
                employee.TelNo = Request.Form["tel"];
                employee.MobilePhone = Request.Form["cell"];

                EmployeeLogic employeeLogic = new EmployeeLogic();
                string returnString = employeeLogic.InsertEmployee(employee);
                if (returnString != "0")
                {
                    if (returnString == "1")
                    {
                        ViewBag.EmployeeNumber = employee.EmployeeId;
                        ViewBag.Name = employee.EmployeeName;
                        ViewBag.MobilePhone = employee.MobilePhone;
                        ViewBag.DepartmentId = employee.DepartmentId;
                        ViewBag.DepartmentName = employee.DepartmentName;
                        ViewBag.Address = employee.Address;
                        ViewBag.TelNo = employee.TelNo;
                        ViewBag.js = "<script type='text/javascript'>alert('員工代號已存在');</script>";
                        //InitialDepartment();
                        ViewBag.Type = "New";
                        return View();
                    }
                    if (returnString == "2")
                    {
                        ViewBag.EmployeeNumber = employee.EmployeeId;
                        ViewBag.Name = employee.EmployeeName;
                        ViewBag.MobilePhone = employee.MobilePhone;
                        ViewBag.DepartmentId = employee.DepartmentId;
                        ViewBag.DepartmentName = employee.DepartmentName;
                        ViewBag.Address = employee.Address;
                        ViewBag.TelNo = employee.TelNo;
                        ViewBag.js = "<script type='text/javascript'>alert('員工姓名已存在');</script>";
                        //InitialDepartment();
                        ViewBag.Type = "New";
                        return View();
                    }
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextNew")
            {
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                employee.EmployeeName = Request.Form["employeeName"];
                employee.DepartmentId = Request.Form["deptId"];
                employee.DepartmentName = Request.Form["dept"];
                employee.Address = Request.Form["address"];
                employee.TelNo = Request.Form["tel"];
                employee.MobilePhone = Request.Form["cell"];

                EmployeeLogic employeeLogic = new EmployeeLogic();
                string returnString = employeeLogic.InsertEmployee(employee);
                if (returnString != "0")
                {
                    if (returnString == "1")
                    {
                        ViewBag.EmployeeNumber = employee.EmployeeId;
                        ViewBag.Name = employee.EmployeeName;
                        ViewBag.MobilePhone = employee.MobilePhone;
                        ViewBag.DepartmentId = employee.DepartmentId;
                        ViewBag.DepartmentName = employee.DepartmentName;
                        ViewBag.Address = employee.Address;
                        ViewBag.TelNo = employee.TelNo;
                        ViewBag.js = "<script type='text/javascript'>alert('員工代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.EmployeeNumber = employee.EmployeeId;
                        ViewBag.Name = employee.EmployeeName;
                        ViewBag.MobilePhone = employee.MobilePhone;
                        ViewBag.DepartmentId = employee.DepartmentId;
                        ViewBag.DepartmentName = employee.DepartmentName;
                        ViewBag.Address = employee.Address;
                        ViewBag.TelNo = employee.TelNo;
                        ViewBag.js = "<script type='text/javascript'>alert('員工姓名已存在');</script>";
                    }
                }
                //InitialDepartment();
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnSaveEdit")
            {
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                employee.EmployeeName = Request.Form["employeeName"];
                employee.DepartmentId = Request.Form["deptId"];
                employee.DepartmentName = Request.Form["dept"];
                employee.Address = Request.Form["address"];
                employee.TelNo = Request.Form["tel"];
                employee.MobilePhone = Request.Form["cell"];
                EmployeeLogic employeeLogic = new EmployeeLogic();
                string returnString = employeeLogic.UpdateEmployee(employee);
                if (returnString == "1")
                {
                    ViewBag.EmployeeNumber = employee.EmployeeId;
                    ViewBag.Name = employee.EmployeeName;
                    ViewBag.MobilePhone = employee.MobilePhone;
                    ViewBag.DepartmentId = employee.DepartmentId;
                    ViewBag.DepartmentName = employee.DepartmentName;
                    ViewBag.Address = employee.Address;
                    ViewBag.TelNo = employee.TelNo;
                    ViewBag.js = "<script type='text/javascript'>alert('員工代號不存在');</script>";
                    //InitialDepartment();
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")
                {
                    ViewBag.EmployeeNumber = employee.EmployeeId;
                    ViewBag.Name = employee.EmployeeName;
                    ViewBag.MobilePhone = employee.MobilePhone;
                    ViewBag.DepartmentId = employee.DepartmentId;
                    ViewBag.DepartmentName = employee.DepartmentName;
                    ViewBag.Address = employee.Address;
                    ViewBag.TelNo = employee.TelNo;
                    ViewBag.js = "<script type='text/javascript'>alert('員工姓名已存在');</script>";
                    //InitialDepartment();
                    ViewBag.Type = "Edit";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                employee.EmployeeName = Request.Form["employeeName"];
                employee.DepartmentId = Request.Form["deptId"];
                employee.DepartmentName = Request.Form["dept"];
                employee.Address = Request.Form["address"];
                employee.TelNo = Request.Form["tel"];
                employee.MobilePhone = Request.Form["cell"];
                EmployeeLogic employeeLogic = new EmployeeLogic();
                string returnString = employeeLogic.UpdateEmployee(employee);
                if (returnString == "1")
                {
                    ViewBag.EmployeeNumber = employee.EmployeeId;
                    ViewBag.Name = employee.EmployeeName;
                    ViewBag.MobilePhone = employee.MobilePhone;
                    ViewBag.DepartmentId = employee.DepartmentId;
                    ViewBag.DepartmentName = employee.DepartmentName;
                    ViewBag.Address = employee.Address;
                    ViewBag.TelNo = employee.TelNo;
                    ViewBag.js = "<script type='text/javascript'>alert('員工代號不存在');</script>";
                    //InitialDepartment();
                    ViewBag.Type = "Edit";
                    return View();
                }
                if (returnString == "2")
                {
                    ViewBag.EmployeeNumber = employee.EmployeeId;
                    ViewBag.Name = employee.EmployeeName;
                    ViewBag.MobilePhone = employee.MobilePhone;
                    ViewBag.DepartmentId = employee.DepartmentId;
                    ViewBag.DepartmentName = employee.DepartmentName;
                    ViewBag.Address = employee.Address;
                    ViewBag.TelNo = employee.TelNo;
                    ViewBag.js = "<script type='text/javascript'>alert('員工姓名已存在');</script>";
                    //InitialDepartment();
                    ViewBag.Type = "Edit";
                    return View();
                }
                //InitialDepartment();
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnEdit")
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                DataTable dtDisplay = employeeLogic.SelectEmployee(employee,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.Company = dtDisplay.Rows[0]["Company"].ToString();
                    ViewBag.UserGroup = dtDisplay.Rows[0]["UserGroup"].ToString();
                    ViewBag.Creator = dtDisplay.Rows[0]["Creator"].ToString();
                    ViewBag.CreateDate = dtDisplay.Rows[0]["CreateDate"].ToString();
                    ViewBag.Modifier = dtDisplay.Rows[0]["Modifier"].ToString();
                    ViewBag.ModiDate = dtDisplay.Rows[0]["ModiDate"].ToString();
                    ViewBag.EmployeeNumber = dtDisplay.Rows[0]["EmployeeId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.MobilePhone = dtDisplay.Rows[0]["MobilePhone"].ToString();
                    ViewBag.DepartmentId = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.DepartmentName = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["Address"].ToString();
                }
                //InitialDepartment();
                ViewBag.Type = "Edit";
                return View();
            }
            if (action == "btnAdd")
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"]; 
                DataTable dtDisplay = employeeLogic.SelectEmployee(employee,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.EmployeeNumber = dtDisplay.Rows[0]["EmployeeId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.MobilePhone = dtDisplay.Rows[0]["MobilePhone"].ToString();
                    ViewBag.DepartmentId = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.DepartmentName = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["Address"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                }
                //InitialDepartment();
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnDelete")
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                Employee employee = new Employee();
                employee.EmployeeId = Request.Form["employeeId"];
                if (!employeeLogic.DeleteEmployee(employee))
                {
                    ViewBag.EmployeeNumber = Request.Form["employeeId"];
                    ViewBag.Name = Request.Form["employeeName"];
                    ViewBag.MobilePhone = Request.Form["cell"];
                    ViewBag.DepartmentId = Request.Form["deptId"];
                    ViewBag.DepartmentName = Request.Form["dept"];
                    ViewBag.Address = Request.Form["address"];
                    ViewBag.TelNo = Request.Form["tel"];
                    ViewBag.js = "<script type='text/javascript'>alert('員工代號不存在');</script>";
                    ViewBag.Type = "Detail";
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                EmployeeLogic employeeLogic = new EmployeeLogic();
                Employee employee = new Employee();
                employee.EmployeeId = id;
                DataTable dtDisplay = employeeLogic.SelectEmployee(employee,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.EmployeeNumber = dtDisplay.Rows[0]["EmployeeId"].ToString();
                    ViewBag.Name = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.MobilePhone = dtDisplay.Rows[0]["MobilePhone"].ToString();
                    ViewBag.DepartmentId = dtDisplay.Rows[0]["DepartmentId"].ToString();
                    ViewBag.DepartmentName = dtDisplay.Rows[0]["DepartmentName"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["Address"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                }
            }
            //InitialDepartment();
            ViewBag.Type = type ?? "New";
            return View();
        }

        public ActionResult Delete(string id)
        {
            EmployeeLogic employeeLogic = new EmployeeLogic();
            Employee employee = new Employee();
            employee.EmployeeId = id;
            if (!employeeLogic.DeleteEmployee(employee))
            {
                ViewBag.js = "<script type='text/javascript'>alert('員工代號不存在');</script>";
            }
            return RedirectToAction("Index", "Main");
        }

        //public JsonResult SearchDepartment(string id)
        //{
        //    DepartmenLogic departmenLogic = new DepartmenLogic();
        //    Department department = new Department();
        //    department.DepartmentId = id;
        //    DataTable dtDisplay = null;
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        dtDisplay = departmenLogic.SelectDepartment(department);
        //    }
        //    else
        //    {
        //        dtDisplay = departmenLogic.SelectDepartment(null);
        //    }
        //    List<Department> lst = new List<Department>();
        //    foreach (DataRow dr in dtDisplay.Rows)
        //    {
        //        department = new Department();
        //        department.DepartmentId = dr["DepartmentId"].ToString();
        //        department.DepartmentName = dr["DepartmentName"].ToString();
        //        department.Remark = dr["Remark"].ToString();
        //        lst.Add(department);
        //    }
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchDepartment(string DepartmentId, string Col, string Condition, string conditionValue, int Page)
        {
            DepartmenLogic logic = new DepartmenLogic();
            Department department = new Department();
            department.DepartmentId = DepartmentId;
            //department.DepartmentName = DepartmentName;
            List<Department> lst = logic.GetDepartment(department,Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public void InitialDepartment()
        {
            DepartmenLogic departmenLogic = new DepartmenLogic();
            DataTable dtDepartments = departmenLogic.SelectDepartment(null,null);
            List<Department> lst = new List<Department>();
            Department department = null;
            foreach (DataRow dr in dtDepartments.Rows)
            {
                department = new Department();
                department.DepartmentId = dr["DepartmentId"].ToString();
                department.DepartmentName = dr["DepartmentName"].ToString();
                department.Remark = dr["Remark"].ToString();
                lst.Add(department);
            }
            ViewData["SelectData"] = lst;
        }

        [HttpPost]
        public JsonResult CURMID(string employeeId)
        {
           EmployeeLogic logic = new EmployeeLogic();
            bool type = logic.IsModuleId(employeeId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
    }
}