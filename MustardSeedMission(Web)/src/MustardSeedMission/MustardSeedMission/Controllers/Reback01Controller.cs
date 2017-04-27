using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;
using MustardSeedMission.ViewModels;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MustardSeedMission.Controllers
{
    public class Reback01Controller : BaseController
    {
        // GET: Reback01
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A009";
            ViewData["DisplayData"] = new List<PlanListViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllPlanList"] = db.PlanList.GroupBy(x => new { x.Code, x.Name }).Select(x => x.FirstOrDefault());
            ViewData["AllStore"] = db.Store;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A009";
            ViewData["DisplayData"] = new List<PlanListViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllPlanList"] = db.PlanList.GroupBy(x => new { x.Code, x.Name }).Select(x => x.FirstOrDefault());
            ViewData["AllStore"] = db.Store;
            if (fc["action"] == "delete")
            {
                string code = ViewBag.Reback01_Modal01_SlPlanList = fc["txtCode"];
                string regionCode = ViewBag.Reback01_Modal01_SlRegion = fc["txtRegion"];
                string zoneCode = ViewBag.Reback01_Modal01_SlZone = fc["txtZone"];

                string codes = fc["chk"];
                PlanList planList;
                int code2;
                int number;
                int storeCode;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c.Split('-')[0]);
                    number = int.Parse(c.Split('-')[1]);
                    storeCode = int.Parse(c.Split('-')[2]);
                    planList =
                        db.PlanList.Where(x => x.Code == code2 && x.Number == number && x.StoreCode == storeCode)
                            .FirstOrDefault();
                    if (planList != null)
                    {
                        db.PlanList.Remove(planList);
                    }
                }
                db.SaveChanges();

                string sql = @"SELECT pl.*,
                                s.Name AS StoreName,
                                z.Code AS ZoneCode,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM PlanList AS pl 
                                LEFT JOIN Store AS s ON pl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1  ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(code))
                {
                    sql += "  AND pl.Code=@Code ";
                    lstParameters.Add(new SqlParameter("Code", code));
                }
                if (!string.IsNullOrEmpty(regionCode))
                {
                    sql += "  AND r.Code=@RegionCode ";
                    lstParameters.Add(new SqlParameter("RegionCode", regionCode));
                }
                if (!string.IsNullOrEmpty(zoneCode))
                {
                    sql += "  AND z.Code=@ZoneCode ";
                    lstParameters.Add(new SqlParameter("ZoneCode", zoneCode));
                }
                sql += " ORDER BY pl.Code,pl.Number ";
                ViewData["DisplayData"] = db.Database.SqlQuery<PlanListViewModel>(sql, lstParameters.ToArray()).ToList();
                ViewData["AllPlanList"] = db.PlanList.GroupBy(x => new { x.Code, x.Name }).Select(x => x.FirstOrDefault());
            }
            if (fc["action"] == "Reback01_Modal01_btnConfirm")
            {
                string code = ViewBag.Reback01_Modal01_SlPlanList = fc["Reback01_Modal01_Plan"];
                string zoneCode = ViewBag.Reback01_Modal01_SlZone = fc["Reback01_Modal01_SerArea"];
                string regionCode = ViewBag.Reback01_Modal01_SlRegion = fc["Reback01_Modal01_LinkArea"];
                string sql = @"SELECT pl.*,
                                s.Name AS StoreName,
                                z.Code AS ZoneCode,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM PlanList AS pl 
                                LEFT JOIN Store AS s ON pl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1  ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(code))
                {
                    sql += "  AND pl.Code=@Code ";
                    lstParameters.Add(new SqlParameter("Code", code));
                }
                if (!string.IsNullOrEmpty(regionCode))
                {
                    sql += "  AND r.Code=@RegionCode ";
                    lstParameters.Add(new SqlParameter("RegionCode", regionCode));
                }
                if (!string.IsNullOrEmpty(zoneCode))
                {
                    sql += "  AND z.Code=@ZoneCode ";
                    lstParameters.Add(new SqlParameter("ZoneCode", zoneCode));
                }
                sql += " ORDER BY pl.Code,pl.Number ";
                ViewData["DisplayData"] = db.Database.SqlQuery<PlanListViewModel>(sql, lstParameters.ToArray()).ToList();
            }
            if (fc["action"] == "export")
            {
                string code = fc["txtCode"];
                string regionCode = fc["txtRegion"];
                string zoneCode = fc["txtZone"];

                string sql = @"SELECT pl.*,
                                s.Name AS StoreName,
                                z.Code AS ZoneCode,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM PlanList AS pl 
                                LEFT JOIN Store AS s ON pl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1  ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(code))
                {
                    sql += "  AND pl.Code=@Code ";
                    lstParameters.Add(new SqlParameter("Code", code));
                }
                if (!string.IsNullOrEmpty(regionCode))
                {
                    sql += "  AND r.Code=@RegionCode ";
                    lstParameters.Add(new SqlParameter("RegionCode", regionCode));
                }
                if (!string.IsNullOrEmpty(zoneCode))
                {
                    sql += "  AND z.Code=@ZoneCode ";
                    lstParameters.Add(new SqlParameter("ZoneCode", zoneCode));
                }
                sql += " ORDER BY pl.Code,pl.Number ";
                IEnumerable<PlanListViewModel> lstExport =
                    db.Database.SqlQuery<PlanListViewModel>(sql, lstParameters.ToArray()).ToList();

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("工作計畫建立");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("序號");
                row.CreateCell(1).SetCellValue("所屬聯誼區");
                row.CreateCell(2).SetCellValue("所屬服務區");
                row.CreateCell(3).SetCellValue("計劃代碼");
                row.CreateCell(4).SetCellValue("計劃名稱");
                row.CreateCell(5).SetCellValue("店家代號");
                row.CreateCell(6).SetCellValue("店家名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (PlanListViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.Number);
                    row.CreateCell(1).SetCellValue(s.RegionName);
                    row.CreateCell(2).SetCellValue(s.ZoneName);
                    row.CreateCell(3).SetCellValue(s.Code);
                    row.CreateCell(4).SetCellValue(s.Name);
                    row.CreateCell(5).SetCellValue(s.StoreCode);
                    row.CreateCell(6).SetCellValue(s.StoreName.ToString());
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "工作計畫建立_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(string name, string region, List<string> Modal)
        {
            PlanList planList = db.PlanList.Where(x => x.Name == name).FirstOrDefault();
            if (planList != null)
            {
                return Json("計劃名稱已存在", JsonRequestBehavior.AllowGet);
            }

            int max = db.PlanList.Count();
            if (max > 0)
            {
                max = db.PlanList.Max(x => x.Code);
            }
            int zonCode;
            int number = 1;
            for (int i = 0; i < Modal.Count; i++)
            {
                zonCode = int.Parse(Modal[i]);
                var stores = db.Store.Where(x => x.ZoneCode == zonCode).ToList();
                foreach (Store s in stores)
                {
                    planList = new PlanList();
                    planList.Code = max + 1;
                    planList.Name = name;
                    planList.Number = number++;
                    planList.StoreCode = s.Code;
                    planList.Creator = Session["UID"].ToString();
                    planList.CreateTime = DateTime.Now;
                    db.Entry(planList).State = EntityState.Added;
                    db.PlanList.Add(planList);
                }
            }

            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetModifyData(string code)
        {
            string sql = @"SELECT pl.*,
                                s.Name AS StoreName,
                                z.Code AS ZoneCode,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM PlanList AS pl 
                                LEFT JOIN Store AS s ON pl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1  ";
            List<SqlParameter> lstParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(code))
            {
                sql += "  AND pl.Code=@Code ";
                lstParameters.Add(new SqlParameter("Code", code));
            }
            sql += " ORDER BY pl.Code,pl.Number ";
            List<PlanListViewModel> lstPlanListViewModels =
                db.Database.SqlQuery<PlanListViewModel>(sql, lstParameters.ToArray()).ToList();
            return Json(lstPlanListViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(List<PlanListViewModel> lstPlanLists)
        {
            List<int> lstCheck = new List<int>();
            PlanList planList;
            int code;
            int number;
            int storeCode;
            foreach (PlanListViewModel p in lstPlanLists)
            {
                code = p.Code;
                number = p.NumberOld;
                storeCode = p.StoreCodeOld;
                planList = db.PlanList.Where(x => x.Code == code && x.Number == number && x.StoreCode == storeCode).FirstOrDefault();
                if (planList == null)
                {
                    return Json("計劃代碼：" + code + ",序號：" + number + ",店家代號:" + storeCode + "不存在", JsonRequestBehavior.AllowGet);
                }
                if (lstCheck.Contains(p.StoreCode))
                {
                    return Json("計劃代碼：" + code + ", 店家代號:" + p.StoreCode + "重複", JsonRequestBehavior.AllowGet);
                }
                lstCheck.Add(p.StoreCode);
            }
            code = lstPlanLists[0].Code;
            IEnumerable<PlanList> lstDel = db.PlanList.Where(x => x.Code == code).ToList();
            foreach (PlanList p in lstDel)
            {
                db.PlanList.Remove(p);
            }

            foreach (PlanListViewModel p in lstPlanLists)
            {
                planList = new PlanList();
                planList.Code = p.Code;
                planList.Name = p.Name;
                planList.Number = p.Number;
                planList.StoreCode = p.StoreCode;
                planList.Creator = lstDel.Where(x => x.StoreCode == p.StoreCodeOld).FirstOrDefault().Creator;
                planList.CreateTime = lstDel.Where(x => x.StoreCode == p.StoreCodeOld).FirstOrDefault().CreateTime;
                planList.Updater = Session["UID"].ToString();
                planList.updateTime = DateTime.Now;
                db.Entry(planList).State = EntityState.Added;
                db.PlanList.Add(planList);
            }

            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostDelete(int code, int number, int storeCode)
        {
            var planList =
                db.PlanList.Where(x => x.Code == code && x.Number == number && x.StoreCode == storeCode)
                    .FirstOrDefault();
            if (planList != null)
            {
                db.PlanList.Remove(planList);
                db.SaveChanges();
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetZoneByRegion(string region)
        {
            IEnumerable<Zone> lstZone;
            string sql;
            if (string.IsNullOrEmpty(region))
            {
                sql = "SELECT * FROM Zone AS z";
            }
            else
            {
                sql = "SELECT * FROM Zone AS z WHERE z.RegionCode=" + region;
            }
            lstZone = db.Database.SqlQuery<Zone>(sql);
            return Json(lstZone, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStoreData(int zone)
        {
            ReturnValue returnValue = new ReturnValue();
            returnValue.zoneCode = zone;
            returnValue.regionCode = db.Zone.Where(x => x.Code == zone).FirstOrDefault().RegionCode;
            List<Store> lstStores;
            string sql = "SELECT * FROM Store AS s WHERE s.ZoneCode=" + zone;
            lstStores = db.Database.SqlQuery<Store>(sql).ToList();
            returnValue.lstStores = lstStores;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRegionValue(int zone)
        {
            Zone zoneSl = db.Zone.Where(x => x.Code == zone).FirstOrDefault();
            if (zoneSl != null)
            {
                return Json(zoneSl.RegionCode, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRegionAndZoneByStore(int store)
        {
            ReturnValue returnValue = new ReturnValue();
            returnValue.storeCode = store;
            Store storeSl = db.Store.Where(x => x.Code == store).FirstOrDefault();
            int? zoneCode = storeSl.ZoneCode;
            returnValue.zoneCode = zoneCode;
            Zone zoneSl = db.Zone.Where(x => x.Code == zoneCode).FirstOrDefault();
            int? regionCode = zoneSl.RegionCode;
            returnValue.regionCode = regionCode;
            List<Zone> lstZones;
            string sql = "SELECT * FROM Zone AS z WHERE z.RegionCode=" + regionCode;
            lstZones = db.Database.SqlQuery<Zone>(sql).ToList();
            returnValue.lstZones = lstZones;
            List<Store> lstStores;
            sql = "SELECT * FROM Store AS s WHERE s.ZoneCode=" + zoneCode;
            lstStores = db.Database.SqlQuery<Store>(sql).ToList();
            returnValue.lstStores = lstStores;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
    }

    public class ReturnValue
    {
        public int? regionCode { get; set; }
        public int? zoneCode { get; set; }
        public int? storeCode { get; set; }
        public string regionValue{ get; set; }
        public string zoneValue { get; set; }
        public string storeValue { get; set; }
        public List<Zone> lstZones { get; set; }
        public List<Store> lstStores { get; set; }
    }
}