using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.ViewModels;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;

namespace MustardSeedMission.Controllers
{
    public class Reback05Controller : BaseController
    {
        // GET: Reback05
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A013";
            ViewData["DisplayData"]=new List<DonationListViewModel>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A013";
            ViewBag.Checker = fc["txtChecker"];
            ViewBag.Rechecker = fc["txtReChecker"];
            ViewBag.Operator = fc["txtOperator"];
            ViewBag.Checkbox = fc["chkBackDate"];
            ViewBag.OperationTimeStart = fc["txtBackDateStart"];
            ViewBag.OperationTimeEnd = fc["txtBackDateEnd"];
            if (fc["action"] == "btnSearch")
            {
                string sql = @"SELECT dl.StoreCode,s.Name AS StoreName,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName,
                            bc.Code AS BusinessCode,bc.Name AS BusinessName,
                            m.Code AS ModificationCode,m.Name AS ModificationName,
                            s.ModificationDate,
                            s.DevelopDate
                            FROM DonationList AS dl
                            LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                            LEFT JOIN Region AS r ON z.RegionCode=r.Code
                            LEFT JOIN BusinessCategory AS bc ON s.BusinessCode=bc.Code
                            LEFT JOIN Modification AS m ON s.ModificationCode=m.Code
                            WHERE 1=1 ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(fc["txtChecker"]))
                {
                    sql += "  AND dl.checker=@checker ";
                    lstParameters.Add(new SqlParameter("@checker", fc["txtChecker"]));
                }
                if (!string.IsNullOrEmpty(fc["txtReChecker"]))
                {
                    sql += "  AND dl.Rechecker=@Rechecker ";
                    lstParameters.Add(new SqlParameter("@Rechecker", fc["txtReChecker"]));
                }
                if (!string.IsNullOrEmpty(fc["txtOperator"]))
                {
                    sql += "  AND dl.Operator=@Operator ";
                    lstParameters.Add(new SqlParameter("@Operator", fc["txtOperator"]));
                }
                if (!string.IsNullOrEmpty(fc["chkBackDate"]))
                {
                    if (!string.IsNullOrEmpty(fc["txtBackDateStart"]))
                    {
                        sql += "  AND dl.OperationTime>=@OperationTimeStart ";
                        lstParameters.Add(new SqlParameter("@OperationTimeStart", DateTime.Parse(fc["txtBackDateStart"])));
                    }
                    if (!string.IsNullOrEmpty(fc["txtBackDateEnd"]))
                    {
                        sql += "   AND dl.OperationTime<=@OperationTimeEnd ";
                        lstParameters.Add(new SqlParameter("@OperationTimeEnd", DateTime.Parse(fc["txtBackDateEnd"])));
                    }
                }
                sql += " ORDER BY dl.DocEntry ";
                ViewData["DisplayData"] = db.Database.SqlQuery<DonationListViewModel>(sql, lstParameters.ToArray()).ToList();
            }
            if (fc["action"] == "export")
            {
                string sql = @"SELECT dl.StoreCode,s.Name AS StoreName,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName,
                            bc.Code AS BusinessCode,bc.Name AS BusinessName,
                            m.Code AS ModificationCode,m.Name AS ModificationName,
                            s.ModificationDate,
                            s.DevelopDate
                            FROM DonationList AS dl
                            LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                            LEFT JOIN Region AS r ON z.RegionCode=r.Code
                            LEFT JOIN BusinessCategory AS bc ON s.BusinessCode=bc.Code
                            LEFT JOIN Modification AS m ON s.ModificationCode=m.Code
                            WHERE 1=1 ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(fc["txtChecker"]))
                {
                    sql += "  AND dl.checker=@checker ";
                    lstParameters.Add(new SqlParameter("@checker", fc["txtChecker"]));
                }
                if (!string.IsNullOrEmpty(fc["txtReChecker"]))
                {
                    sql += "  AND dl.Rechecker=@Rechecker ";
                    lstParameters.Add(new SqlParameter("@Rechecker", fc["txtReChecker"]));
                }
                if (!string.IsNullOrEmpty(fc["txtOperator"]))
                {
                    sql += "  AND dl.Operator=@Operator ";
                    lstParameters.Add(new SqlParameter("@Operator", fc["txtOperator"]));
                }
                if (!string.IsNullOrEmpty(fc["chkBackDate"]))
                {
                    if (!string.IsNullOrEmpty(fc["txtBackDateStart"]))
                    {
                        sql += "  AND dl.OperationTime>=@OperationTimeStart ";
                        lstParameters.Add(new SqlParameter("@OperationTimeStart", DateTime.Parse(fc["txtBackDateStart"])));
                    }
                    if (!string.IsNullOrEmpty(fc["txtBackDateEnd"]))
                    {
                        sql += "   AND dl.OperationTime<=@OperationTimeEnd ";
                        lstParameters.Add(new SqlParameter("@OperationTimeEnd", DateTime.Parse(fc["txtBackDateEnd"])));
                    }
                }
                sql += " ORDER BY dl.DocEntry ";
                List<DonationListViewModel> lstExport= db.Database.SqlQuery<DonationListViewModel>(sql, lstParameters.ToArray()).ToList();

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("查詢作業");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("所屬聯誼區");
                row.CreateCell(1).SetCellValue("所屬服務區");
                row.CreateCell(2).SetCellValue("店家代碼");
                row.CreateCell(3).SetCellValue("店家名稱");
                row.CreateCell(4).SetCellValue("營業種類代碼");
                row.CreateCell(5).SetCellValue("異動原因");
                row.CreateCell(6).SetCellValue("異動年月");
                row.CreateCell(7).SetCellValue("開發月份");

                int rowIndex;
                rowIndex = 1;
                foreach (DonationListViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.RegionName);
                    row.CreateCell(1).SetCellValue(s.ZoneName);
                    row.CreateCell(2).SetCellValue(s.StoreCode);
                    row.CreateCell(3).SetCellValue(s.StoreName);
                    row.CreateCell(4).SetCellValue(s.BusinessName);
                    row.CreateCell(5).SetCellValue(s.ModificationName);
                    row.CreateCell(6).SetCellValue(s.ModificationDate == null ? "" : DateTime.Parse(s.ModificationDate.ToString()).ToString("yyyy/MM/dd"));
                    row.CreateCell(7).SetCellValue(s.DevelopDate==null?"":DateTime.Parse(s.DevelopDate.ToString()).ToString("yyyy/MM"));
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "查詢作業_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }
    }
}