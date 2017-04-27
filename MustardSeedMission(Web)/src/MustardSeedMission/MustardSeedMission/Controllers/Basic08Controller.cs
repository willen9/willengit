using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class Basic08Controller : BaseController
    {
        // GET: Basic08
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A008";
            string uid = Session["UID"].ToString();
            ViewBag.Login =uid;
            ViewBag.LoginName = db.Users.Where(x=>x.UserId==uid).FirstOrDefault().UserName;
            ViewData["DisplayData"] = new List<StoreViewModel>();
            ViewData["AllModification"] = db.Modification;
            ViewData["AllBusinessCategory"] = db.BusinessCategory;
            ViewData["AllDonationBox"] = db.DonationBox;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllRegion"] = db.Region;
            List<Status> lstStatus = new List<Status>()
            {
                new Status() {Value = "",Name = "==請選擇=="},
                new Status() {Value = "PC",Name = "PC"},
                new Status() {Value = "PDA",Name = "PDA"}
            };
            ViewBag.Basic08_Modal01_BackYM = ViewBag.Basic08_Modal01_YM = ViewBag.Basic08_Modal01_Monday = ViewBag.Basic08_Modal01_Tuesday = ViewBag.Basic08_Modal01_Wednesday = ViewBag.Basic08_Modal01_Thursday = ViewBag.Basic08_Modal01_Friday = ViewBag.Basic08_Modal01_Saturday = ViewBag.Basic08_Modal01_Sunday = "on";
            ViewData["Status"] = new SelectList(lstStatus, "Value", "Name", "==請選擇==");
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A008";
            string uid = Session["UID"].ToString();
            ViewBag.Login = uid;
            ViewBag.LoginName = db.Users.Where(x => x.UserId == uid).FirstOrDefault().UserName;
            ViewData["DisplayData"] = new List<StoreViewModel>();
            ViewData["AllModification"] = db.Modification;
            ViewData["AllBusinessCategory"] = db.BusinessCategory;
            ViewData["AllDonationBox"] = db.DonationBox;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllRegion"] = db.Region;
            List<Status> lstStatus = new List<Status>()
            {
                new Status() {Value = "",Name = "==請選擇=="},
                new Status() {Value = "PC",Name = "PC"},
                new Status() {Value = "PDA",Name = "PDA"}
            };
            ViewData["Status"] = new SelectList(lstStatus, "Value", "Name", "");
            if (fc["action"] == "Basic08_Modal01_btnDel")
            {
                string codes = fc["Basic08_Modal01_Del_Chk"];
                Store store;
                int code2;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c);
                    store = db.Store.Find(code2);
                    if (store != null)
                    {
                        db.Store.Remove(store);
                    }
                }
                db.SaveChanges();

                ViewData["DisplayData"] = GetData(fc);
            }
            if (fc["action"] == "Basic08_Modal01_btnConfirm")
            {
                ViewData["DisplayData"] = GetData(fc);
            }
            if (fc["action"] == "Basic08_Modal01_btnExport")
            {
                IEnumerable<StoreViewModel> lstExport = GetData(fc);

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("店家主檔資料");
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
                row.CreateCell(3).SetCellValue("店家代碼");
                row.CreateCell(4).SetCellValue("店家名稱");
                row.CreateCell(5).SetCellValue("營業種類代碼");
                row.CreateCell(6).SetCellValue("異動原因");

                int rowIndex;
                rowIndex = 1;
                foreach (StoreViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.SerialNo.ToString());
                    row.CreateCell(1).SetCellValue(s.RegionName);
                    row.CreateCell(2).SetCellValue(s.ZoneName);
                    row.CreateCell(3).SetCellValue(s.Code);
                    row.CreateCell(4).SetCellValue(s.Name);
                    row.CreateCell(5).SetCellValue(s.BusinessName);
                    row.CreateCell(6).SetCellValue(s.ModificationName);
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "店家主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(Store store)
        {
            string name = store.Name;
            int zoneCode = store.ZoneCode ?? 0;
            Store storeSl = db.Store.Where(x => x.Name == name).FirstOrDefault();
            if (storeSl != null)
            {
                return Json("店家名稱已存在", JsonRequestBehavior.AllowGet);
            }
            int max = db.Store.Count();
            if (max > 0)
            {
                max = db.Store.Max(x => x.Code);
            }
            int serialNo = 0;
            if (store.ZoneCode != null)
            {
                if (db.Store.Any(x => x.ZoneCode == zoneCode))
                {
                    serialNo = db.Store.Where(x => x.ZoneCode == zoneCode).Max(x => x.SerialNo).Value;
                }
            }
            string sql1 = "INSERT INTO Store(Code,SerialNo,Creator,CreateTime,[Status]";
            string sql2 = " VALUES (@Code,@SerialNo,@Creator,getdate(),'PC'";
            List<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@Code", max + 1));
            lstSqlParameters.Add(new SqlParameter("@SerialNo", serialNo + 1));
            lstSqlParameters.Add(new SqlParameter("@Creator", Session["UID"].ToString()));
            if (store.Name != null)
            {
                sql1 += ",Name";
                sql2 += ",@Name";
                lstSqlParameters.Add(new SqlParameter("@Name", store.Name));
            }
            if (store.ZoneCode != null)
            {
                sql1 += ",ZoneCode";
                sql2 += ",@ZoneCode";
                lstSqlParameters.Add(new SqlParameter("@ZoneCode", store.ZoneCode));
            }
            if (store.BusinessCode != null)
            {
                sql1 += ",BusinessCode";
                sql2 += ",@BusinessCode";
                lstSqlParameters.Add(new SqlParameter("@BusinessCode", store.BusinessCode));
            }
            if (store.DevelopDate != null)
            {
                sql1 += ",DevelopDate";
                sql2 += ",@DevelopDate";
                lstSqlParameters.Add(new SqlParameter("@DevelopDate", store.DevelopDate));
            }
            if (store.Developer != null)
            {
                sql1 += ",Developer";
                sql2 += ",@Developer";
                lstSqlParameters.Add(new SqlParameter("@Developer", store.Developer));
            }
            if (store.DBC != null)
            {
                sql1 += ",DBC";
                sql2 += ",@DBC";
                lstSqlParameters.Add(new SqlParameter("@DBC", store.DBC));
            }
            if (store.Sunday != null)
            {
                sql1 += ",Sunday";
                sql2 += ",@Sunday";
                lstSqlParameters.Add(new SqlParameter("@Sunday", store.Sunday));
            }
            if (store.Monday != null)
            {
                sql1 += ",Monday";
                sql2 += ",@Monday";
                lstSqlParameters.Add(new SqlParameter("@Monday", store.Monday));
            }
            if (store.Tuesday != null)
            {
                sql1 += ",Tuesday";
                sql2 += ",@Tuesday";
                lstSqlParameters.Add(new SqlParameter("@Tuesday", store.Tuesday));
            }
            if (store.Wednesday != null)
            {
                sql1 += ",Wednesday";
                sql2 += ",@Wednesday";
                lstSqlParameters.Add(new SqlParameter("@Wednesday", store.Wednesday));
            }
            if (store.Thursday != null)
            {
                sql1 += ",Thursday";
                sql2 += ",@Thursday";
                lstSqlParameters.Add(new SqlParameter("@Thursday", store.Thursday));
            }
            if (store.Friday != null)
            {
                sql1 += ",Friday";
                sql2 += ",@Friday";
                lstSqlParameters.Add(new SqlParameter("@Friday", store.Friday));
            }
            if (store.Saturday != null)
            {
                sql1 += ",Saturday";
                sql2 += ",@Saturday";
                lstSqlParameters.Add(new SqlParameter("@Saturday", store.Saturday));
            }
            if (store.ReceiptZIP != null)
            {
                sql1 += ",ReceiptZIP";
                sql2 += ",@ReceiptZIP";
                lstSqlParameters.Add(new SqlParameter("@ReceiptZIP", store.ReceiptZIP));
            }
            if (store.ReceipAddress != null)
            {
                sql1 += ",ReceipAddress";
                sql2 += ",@ReceipAddress";
                lstSqlParameters.Add(new SqlParameter("@ReceipAddress", store.ReceipAddress));
            }
            if (store.ZIP != null)
            {
                sql1 += ",ZIP";
                sql2 += ",@ZIP";
                lstSqlParameters.Add(new SqlParameter("@ZIP", store.ZIP));
            }
            if (store.Address != null)
            {
                sql1 += ",Address";
                sql2 += ",@Address";
                lstSqlParameters.Add(new SqlParameter("@Address", store.Address));
            }
            if (store.Tel != null)
            {
                sql1 += ",Tel";
                sql2 += ",@Tel";
                lstSqlParameters.Add(new SqlParameter("@Tel", store.Tel));
            }
            if (store.Description != null)
            {
                sql1 += ",Description";
                sql2 += ",@Description";
                lstSqlParameters.Add(new SqlParameter("@Description", store.Description));
            }
            if (store.FSTime != null)
            {
                sql1 += ",FSTime";
                sql2 += ",@FSTime";
                lstSqlParameters.Add(new SqlParameter("@FSTime", store.FSTime));
            }
            if (store.FETime != null)
            {
                sql1 += ",FETime";
                sql2 += ",@FETime";
                lstSqlParameters.Add(new SqlParameter("@FETime", store.FETime));
            }
            if (store.SSTime != null)
            {
                sql1 += ",SSTime";
                sql2 += ",@SSTime";
                lstSqlParameters.Add(new SqlParameter("@SSTime", store.SSTime));
            }
            if (store.SETime != null)
            {
                sql1 += ",SETime";
                sql2 += ",@SETime";
                lstSqlParameters.Add(new SqlParameter("@SETime", store.SETime));
            }
            sql1 += ")";
            sql2 += ")";
            string sql = sql1 + sql2;
            db.Database.ExecuteSqlCommand(sql, lstSqlParameters.ToArray());
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(List<Store> Modal)
        {
            Store store;
            foreach (Store s in Modal)
            {
                store = db.Store.Where(x => x.Code != s.Code && x.Name == s.Name).FirstOrDefault();
                if (store != null)
                {
                    return Json("店家名稱："+s.Name+"已存在", JsonRequestBehavior.AllowGet);
                }
                store = db.Store.Find(s.Code);
                if (store == null)
                {
                    return Json("店家代碼：" + s.Code + "不存在", JsonRequestBehavior.AllowGet);
                }
                store.Name = s.Name;
                store.Level = s.Level;
                store.updateTime=DateTime.Now;
                store.Updater = Session["UID"].ToString();
                int? nullVal = null;
                if (s.BusinessCode != null)
                {
                    store.BusinessCode = s.BusinessCode;
                }
                else
                {
                    store.BusinessCode = nullVal;
                }
                if (s.ModificationCode != null)
                {
                    if (s.ModificationCode != store.ModificationCode)
                    {
                        store.ModificationDate = DateTime.Now;
                    }
                    store.ModificationCode = s.ModificationCode;
                }
                else
                {
                    store.ModificationCode = nullVal;
                }
                db.Entry(store).State=EntityState.Modified;
                db.Store.AddOrUpdate(store);
            }
   
                db.SaveChanges();
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
        public JsonResult GetRegionByZone(string zone)
        {
            int zoneCode = int.Parse(zone);
            int? regionCode=db.Zone.Where(x => x.Code == zoneCode).FirstOrDefault().RegionCode;
            return Json(regionCode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetModifyData(string region, string zone)
        {
            string sql = @"SELECT sf.SerialNo,
                                sf.Code,
                                sf.Name,
                                sf.ZoneCode,
                                sf.BusinessCode,
                                sf.ModificationCode,
                                bc.Name AS BusinessName,
                                m.Name AS ModificationName,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM Store AS sf
                                LEFT JOIN DonationBox AS db ON sf.DBC=db.Code
                                LEFT JOIN BusinessCategory AS bc ON sf.BusinessCode=bc.Code
                                LEFT JOIN Modification AS m ON sf.ModificationCode=m.Code
                                LEFT JOIN Zone AS z ON sf.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1 ";
            List<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(region))
            {
                sql += "  AND r.Code=@RegionCode ";
                lstSqlParameters.Add(new SqlParameter("@RegionCode", int.Parse(region)));
            }
            if (!string.IsNullOrEmpty(zone))
            {
                sql += " AND z.Code=@ZoneCode ";
                lstSqlParameters.Add(new SqlParameter("@ZoneCode", int.Parse(zone)));
            }
            sql += " ORDER BY r.Code,z.Code,LEN(sf.[Level]) DESC,sf.[Level],sf.SerialNo ";

            List<StoreViewModel> lstStoreViewModels = db.Database.SqlQuery<StoreViewModel>(sql, lstSqlParameters.ToArray()).ToList();
            return Json(lstStoreViewModels, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<StoreViewModel> GetData(FormCollection fc)
        {
            string RegionCode = ViewBag.Basic08_Modal01_RegionCode = fc["Basic08_Modal01_Region"];
            string DonationBoxCode = ViewBag.Basic08_Modal01_DonationBoxCode = fc["Basic08_Modal01_BoxName"];
            string ZoneCode = ViewBag.Basic08_Modal01_ZoneCode = fc["Basic08_Modal01_SerArea"];
            string Status = ViewBag.Basic08_Modal01_Status = fc["Basic08_Modal01_Status"];
            string ShopId = ViewBag.Basic08_Modal01_ShopId = fc["Basic08_Modal01_ShopId"];
            string Level = ViewBag.Basic08_Modal01_Level = fc["Basic08_Modal01_Level"];
            string ShopName = ViewBag.Basic08_Modal01_ShopName = fc["Basic08_Modal01_ShopName"];
            string Address = ViewBag.Basic08_Modal01_Address = fc["Basic08_Modal01_Address"];
            string BusinessCategoryCode = ViewBag.Basic08_Modal01_BusinessCategoryCode = fc["Basic08_Modal01_BusinessCategory"];
            string Tel = ViewBag.Basic08_Modal01_Tel = fc["Basic08_Modal01_Tel"];
            string ModificationCode = ViewBag.Basic08_Modal01_ModificationCode = fc["Basic08_Modal01_Reason"];
            string YMStart = ViewBag.Basic08_Modal01_YMStart = fc["Basic08_Modal01_YMStart"];
            string YMEnd = ViewBag.Basic08_Modal01_YMEnd = fc["Basic08_Modal01_YMEnd"];
            string Month = ViewBag.Basic08_Modal01_Month = fc["Basic08_Modal01_Month"];
            string BackYMStart = ViewBag.Basic08_Modal01_BackYMStart = fc["Basic08_Modal01_BackYMStart"];
            string BackYMEnd = ViewBag.Basic08_Modal01_BackYMEnd = fc["Basic08_Modal01_BackYMEnd"];
            string Creator = ViewBag.Basic08_Modal01_Creator = fc["Basic08_Modal01_Creator"];
            string Changer = ViewBag.Basic08_Modal01_Changer = fc["Basic08_Modal01_Changer"];
            string Developer = ViewBag.Basic08_Modal01_Developer = fc["Basic08_Modal01_Developer"];
            string Remark = ViewBag.Basic08_Modal01_Remark = fc["Basic08_Modal01_Remark"];
            string Monday = ViewBag.Basic08_Modal01_Monday = fc["Basic08_Modal01_Monday"];
            string Tuesday = ViewBag.Basic08_Modal01_Tuesday = fc["Basic08_Modal01_Tuesday"];
            string Wednesday = ViewBag.Basic08_Modal01_Wednesday = fc["Basic08_Modal01_Wednesday"];
            string Thursday = ViewBag.Basic08_Modal01_Thursday = fc["Basic08_Modal01_Thursday"];
            string Friday = ViewBag.Basic08_Modal01_Friday = fc["Basic08_Modal01_Friday"];
            string Saturday = ViewBag.Basic08_Modal01_Saturday = fc["Basic08_Modal01_Saturday"];
            string Sunday = ViewBag.Basic08_Modal01_Sunday = fc["Basic08_Modal01_Sunday"];
            string BackYM = ViewBag.Basic08_Modal01_BackYM = fc["Basic08_Modal01_BackYM"];
            string YM = ViewBag.Basic08_Modal01_YM = fc["Basic08_Modal01_YM"];
            List<Status> lstStatus = new List<Status>()
            {
                new Status() {Value = "",Name = "==請選擇=="},
                new Status() {Value = "PC",Name = "PC"},
                new Status() {Value = "PDA",Name = "PDA"}
            };
            ViewData["Status"] = new SelectList(lstStatus, "Value", "Name", Status);

            string sql = @"SELECT sf.SerialNo,
                                sf.Code,
                                sf.Name,
                                sf.ZoneCode,
                                sf.BusinessCode,
                                sf.ModificationCode,
                                bc.Name AS BusinessName,
                                m.Name AS ModificationName,
                                z.Name AS ZoneName,
                                r.Code AS RegionCode,
                                r.Name AS RegionName
                                FROM Store AS sf
                                LEFT JOIN DonationBox AS db ON sf.DBC=db.Code
                                LEFT JOIN BusinessCategory AS bc ON sf.BusinessCode=bc.Code
                                LEFT JOIN Modification AS m ON sf.ModificationCode=m.Code
                                LEFT JOIN Zone AS z ON sf.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code
                                WHERE 1=1 ";
            List<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(RegionCode))
            {
                sql += "  AND r.Code=@RegionCode ";
                lstSqlParameters.Add(new SqlParameter("@RegionCode", int.Parse(RegionCode)));
            }
            if (!string.IsNullOrEmpty(DonationBoxCode))
            {
                sql += " AND db.Code=@DonationBoxCode ";
                lstSqlParameters.Add(new SqlParameter("@DonationBoxCode", int.Parse(DonationBoxCode)));
            }
            if (!string.IsNullOrEmpty(ZoneCode))
            {
                sql += " AND z.Code=@ZoneCode ";
                lstSqlParameters.Add(new SqlParameter("@ZoneCode", int.Parse(ZoneCode)));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sql += " AND sf.[Status]=@Status ";
                lstSqlParameters.Add(new SqlParameter("@Status", Status));
            }
            if (!string.IsNullOrEmpty(ShopId))
            {
                sql += " AND sf.Code=@Code ";
                lstSqlParameters.Add(new SqlParameter("@Code", int.Parse(ShopId)));
            }
            if (!string.IsNullOrEmpty(Level))
            {
                sql += "  AND sf.[Level]=@Level ";
                lstSqlParameters.Add(new SqlParameter("@Level", int.Parse(Level)));
            }
            if (!string.IsNullOrEmpty(ShopName))
            {
                sql += "  AND sf.Name like @Name ";
                lstSqlParameters.Add(new SqlParameter("@Name", "%"+ShopName+"%"));
            }
            if (!string.IsNullOrEmpty(Address))
            {
                sql += "  AND sf.[Address]=@Address ";
                lstSqlParameters.Add(new SqlParameter("@Address", Address));
            }
            if (!string.IsNullOrEmpty(BusinessCategoryCode))
            {
                sql += "   AND bc.Code=@BusinessCode ";
                lstSqlParameters.Add(new SqlParameter("@BusinessCode", int.Parse(BusinessCategoryCode)));
            }
            if (!string.IsNullOrEmpty(Tel))
            {
                sql += "   AND sf.Tel=@Tel ";
                lstSqlParameters.Add(new SqlParameter("@Tel", Tel));
            }
            if (!string.IsNullOrEmpty(ModificationCode))
            {
                sql += "   AND m.Code=@Modification ";
                lstSqlParameters.Add(new SqlParameter("@Modification", int.Parse(ModificationCode)));
            }
            if (!string.IsNullOrEmpty(YM))
            {
                if (!string.IsNullOrEmpty(YMStart))
                {
                    sql += "  AND sf.ModificationDate>=@ModificationDateStart ";
                    lstSqlParameters.Add(new SqlParameter("@ModificationDateStart", DateTime.Parse(YMStart.Replace("-", "/"))));
                }
                if (!string.IsNullOrEmpty(YMEnd))
                {
                    sql += "  AND sf.ModificationDate<=@ModificationDateEnd ";
                    lstSqlParameters.Add(new SqlParameter("@ModificationDateEnd", DateTime.Parse(YMEnd.Replace("-", "/"))));
                }
            }
            if (!string.IsNullOrEmpty(Month))
            {
                sql += " AND sf.DevelopDate=@DevelopDate ";
                lstSqlParameters.Add(new SqlParameter("@DevelopDate", DateTime.Parse(Month.Replace("-", "/"))));
            }
            if (!string.IsNullOrEmpty(BackYM))
            {
                if (!string.IsNullOrEmpty(BackYMStart))
                {
                    sql += "   AND sf.LastTime>=@LastTimeStart ";
                    lstSqlParameters.Add(new SqlParameter("@LastTimeStart", DateTime.Parse(BackYMStart.Replace("-", "/"))));
                }
                if (!string.IsNullOrEmpty(BackYMEnd))
                {
                    sql += "  AND sf.LastTime<=@LastTimeEnd ";
                    lstSqlParameters.Add(new SqlParameter("@LastTimeEnd", DateTime.Parse(BackYMEnd.Replace("-", "/"))));
                }
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                sql += "  AND sf.Creator=@Creator ";
                lstSqlParameters.Add(new SqlParameter("@Creator", Creator));
            }
            if (!string.IsNullOrEmpty(Changer))
            {
                sql += "   AND sf.Updater=@Updater ";
                lstSqlParameters.Add(new SqlParameter("@Updater", Changer));
            }
            if (!string.IsNullOrEmpty(Developer))
            {
                sql += "  AND sf.Developer=@Developer ";
                lstSqlParameters.Add(new SqlParameter("@Developer", Developer));
            }
            if (!string.IsNullOrEmpty(Remark))
            {
                sql += "  AND sf.[Description]=@Description ";
                lstSqlParameters.Add(new SqlParameter("@Description", Remark));
            }
            if (!string.IsNullOrEmpty(Monday))
            {
                sql += "  AND sf.Monday=@Monday ";
                lstSqlParameters.Add(new SqlParameter("@Monday", true));
            }
            if (!string.IsNullOrEmpty(Tuesday))
            {
                sql += " AND sf.Tuesday=@Tuesday ";
                lstSqlParameters.Add(new SqlParameter("@Tuesday", true));
            }
            if (!string.IsNullOrEmpty(Wednesday))
            {
                sql += "  AND sf.Wednesday=@Wednesday ";
                lstSqlParameters.Add(new SqlParameter("@Wednesday", true));
            }
            if (!string.IsNullOrEmpty(Thursday))
            {
                sql += "  AND sf.Thursday=@Thursday ";
                lstSqlParameters.Add(new SqlParameter("@Thursday", true));
            }
            if (!string.IsNullOrEmpty(Friday))
            {
                sql += "  AND sf.Friday=@Friday ";
                lstSqlParameters.Add(new SqlParameter("@Friday", true));
            }
            if (!string.IsNullOrEmpty(Saturday))
            {
                sql += "  AND sf.Saturday=@Saturday ";
                lstSqlParameters.Add(new SqlParameter("@Saturday", true));
            }
            if (!string.IsNullOrEmpty(Sunday))
            {
                sql += " AND sf.Sunday=@Sunday ";
                lstSqlParameters.Add(new SqlParameter("@Sunday", true));
            }
            sql += " ORDER BY r.Code,z.Code,sf.SerialNo ";
            if (lstSqlParameters.Count == 0)
            {
                return db.Database.SqlQuery<StoreViewModel>(sql).ToList();
            }
            return db.Database.SqlQuery<StoreViewModel>(sql, lstSqlParameters.ToArray()).ToList();
        }
    }

    public class Status
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}