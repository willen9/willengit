using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.ViewModels;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MustardSeedMission.Controllers
{
    public class Cuscomplait03Controller : BaseController
    {
        // GET: Cuscomplait03
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A016";
            ViewData["DisplayData"]=new List<ComplaintsViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllStore"] = db.Store;
            ViewData["AllCategory"] = db.Category;
            ViewData["AllCreator"] = db.Complaints.GroupBy(o => o.Creator).Select(x => x.FirstOrDefault());
            ViewData["AllOperator"] = db.Complaints.GroupBy(o => o.Operator).Select(x => x.FirstOrDefault());
            List<Status> lstStatus = new List<Status>()
            {
                new Status() {Value = "",Name = "==請選擇=="},
                new Status() {Value = "Y",Name = "已結案"},
                new Status() {Value = "N",Name = "未結案"}
            };
            ViewData["Status"] = new SelectList(lstStatus, "Value", "Name", "");
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A016";
            ViewData["DisplayData"] = new List<ComplaintsViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllStore"] = db.Store;
            ViewData["AllCategory"] = db.Category;
            ViewData["AllCreator"] = db.Complaints.GroupBy(o => o.Creator).Select(x => x.FirstOrDefault());
            ViewData["AllOperator"] = db.Complaints.GroupBy(o => o.Operator).Select(x => x.FirstOrDefault());
            if (fc["action"] == "Cuscomplait03_Modal01_btnConfirm")
            {
                ViewData["DisplayData"] = GetData(fc);
            }
            if (fc["action"] == "Cuscomplait03_Modal01_btnExport")
            {
                IEnumerable<ComplaintsViewModel> lstExport = GetData(fc);

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("客訴單查詢");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("客訴單號");
                row.CreateCell(1).SetCellValue("所屬聯誼區");
                row.CreateCell(2).SetCellValue("所屬服務區");
                row.CreateCell(3).SetCellValue("店家代碼");
                row.CreateCell(4).SetCellValue("店家名稱");
                row.CreateCell(5).SetCellValue("地址");
                row.CreateCell(6).SetCellValue("連絡電話");
                row.CreateCell(7).SetCellValue("日期");

                int rowIndex;
                rowIndex = 1;
                foreach (ComplaintsViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.DocEntry);
                    row.CreateCell(1).SetCellValue(s.RegionName);
                    row.CreateCell(2).SetCellValue(s.ZoneName);
                    row.CreateCell(3).SetCellValue(s.StoreCode.ToString());
                    row.CreateCell(4).SetCellValue(s.StoreName);
                    row.CreateCell(5).SetCellValue(s.Address);
                    row.CreateCell(6).SetCellValue(s.Tel);
                    row.CreateCell(7).SetCellValue(s.EvenDate==null?"":DateTime.Parse(s.EvenDate.ToString()).ToString("yyyy/MM/dd"));
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "客訴單查詢_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        private IEnumerable<ComplaintsViewModel> GetData(FormCollection fc)
        {
            string DocEntry = ViewBag.DocEntry = fc["Cuscomplait03_Modal01_No"];
            string Category = ViewBag.Cuscomplait03_Modal01_Category = fc["Cuscomplait03_Modal01_Type"];
            string RegionCode = ViewBag.Cuscomplait03_Modal01_RegionCode = fc["Cuscomplait02_Modal01_Link"];
            string Creator = ViewBag.Cuscomplait03_Modal01_Creator = fc["Cuscomplait03_Modal01_Receiver"];
            string ZoneCode = ViewBag.Cuscomplait03_Modal01_ZoneCode = fc["Cuscomplait03_Modal01_SerArea"];
            string Operator = ViewBag.Cuscomplait03_Modal01_Operator = fc["Cuscomplait03_Modal01_Dealer"];
            string Store = ViewBag.Cuscomplait03_Modal01_Store = fc["Cuscomplait03_Modal01_ShopId"];
            string chk = ViewBag.chk = fc["Cuscomplait03_Modal01_Date"];
            string DateStart = ViewBag.Cuscomplait03_Modal01_DateStart = fc["Cuscomplait03_Modal01_DateStart"];
            string DateEnd = ViewBag.Cuscomplait03_Modal01_DateEnd = fc["Cuscomplait03_Modal01_DateEnd"];
            string Finish = ViewBag.Cuscomplait03_Modal01_Finish = fc["Cuscomplait03_Modal01_End"];

            List<Status> lstStatus = new List<Status>()
            {
                new Status() {Value = "",Name = "==請選擇=="},
                new Status() {Value = "Y",Name = "已結案"},
                new Status() {Value = "N",Name = "未結案"}
            };
            ViewData["Status"] = new SelectList(lstStatus, "Value", "Name", Finish);

            string sql = @"SELECT c.*,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName
                            FROM Complaints AS c
                            LEFT JOIN Store AS s ON c.StoreCode=s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                            LEFT JOIN Region AS r ON z.RegionCode=r.Code
                            WHERE 1=1 ";
            List<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(DocEntry))
            {
                sql += "  AND c.DocEntry=@DocEntry ";
                lstSqlParameters.Add(new SqlParameter("@DocEntry", DocEntry));
            }
            if (!string.IsNullOrEmpty(Category))
            {
                sql += " AND c.Categolgy=@Categolgy ";
                lstSqlParameters.Add(new SqlParameter("@Categolgy", int.Parse(Category)));
            }
            if (!string.IsNullOrEmpty(RegionCode))
            {
                sql += " AND r.Code=@RegionCode ";
                lstSqlParameters.Add(new SqlParameter("@RegionCode", int.Parse(RegionCode)));
            }
            if (!string.IsNullOrEmpty(ZoneCode))
            {
                sql += " AND z.Code=@ZoneCode ";
                lstSqlParameters.Add(new SqlParameter("@ZoneCode", int.Parse(ZoneCode)));
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                sql += " AND c.Creator=@Creator ";
                lstSqlParameters.Add(new SqlParameter("@Creator", Creator));
            }
            if (!string.IsNullOrEmpty(Operator))
            {
                sql += "  AND c.Operator=@Operator ";
                lstSqlParameters.Add(new SqlParameter("@Operator", Operator));
            }
            if (!string.IsNullOrEmpty(Store))
            {
                sql += "  AND c.StoreCode=@StoreCode ";
                lstSqlParameters.Add(new SqlParameter("@StoreCode", Store));
            }
            if (!string.IsNullOrEmpty(chk))
            {
                if (!string.IsNullOrEmpty(DateStart))
                {
                    sql += "  AND c.EvenDate>=@EvenDateStart ";
                    lstSqlParameters.Add(new SqlParameter("@EvenDateStart", DateTime.Parse(DateStart.Replace("-", "/"))));
                }
                if (!string.IsNullOrEmpty(DateEnd))
                {
                    sql += "  AND c.EvenDate<=@EvenDateEnd ";
                    lstSqlParameters.Add(new SqlParameter("@EvenDateEnd", DateTime.Parse(DateEnd.Replace("-", "/"))));
                }
            }
            if (!string.IsNullOrEmpty(Finish))
            {
                sql += "  AND isnull(c.Finish,0)=@Finish ";
                lstSqlParameters.Add(new SqlParameter("@Finish", Finish == "Y"));
            }
            sql += " ORDER BY c.DocEntry,c.StoreCode ";
            if (lstSqlParameters.Count == 0)
            {
                return db.Database.SqlQuery<ComplaintsViewModel>(sql).ToList();
            }
            return db.Database.SqlQuery<ComplaintsViewModel>(sql, lstSqlParameters.ToArray()).ToList();
        }
    }
}