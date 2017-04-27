using System;
using System.Collections.Generic;
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
    public class Reback02Controller : BaseController
    {
        // GET: Reback02
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A010";
            ViewData["DisplayData"] = new List<DonationHeadViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllPlanList"] = db.PlanList.GroupBy(x => new { x.Code, x.Name }).Select(x => x.FirstOrDefault());
            ViewData["AllStore"] = db.Store;
            ViewData["AllUser"] = db.Users;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A010";
            ViewData["DisplayData"] = new List<DonationHeadViewModel>();
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllPlanList"] = db.PlanList.GroupBy(x => new { x.Code, x.Name }).Select(x => x.FirstOrDefault());
            ViewData["AllStore"] = db.Store;
            ViewData["AllUser"] = db.Users;
            if (fc["action"] == "delete")
            {
                string oper = ViewBag.Reback02_Modal01_SlAssiger = fc["txtOperator"];
                string workDate = ViewBag.Reback02_Modal01_RebackDate = fc["txtWorkDate"];
                string planCode = ViewBag.Reback02_Modal01_SlPlan = fc["txtPlanCode"];
                ViewBag.DocEntry = "";

                string docEntrys = fc["chk"];
                DonationHead donationHead;
                IEnumerable<DonationList> lstDonationLists;
                DonationList donationList;
                int serialNo, storeCode;
                foreach (string docEntry in docEntrys.Split(','))
                {
                    donationHead = db.DonationHead.Where(x => x.DocEntry==docEntry).FirstOrDefault();
                    if (donationHead != null)
                    {
                        db.DonationHead.Remove(donationHead);
                    }
                    lstDonationLists = db.DonationList.Where(x => x.DocEntry == docEntry);
                    foreach (DonationList d in lstDonationLists )
                    {
                        serialNo = d.Serialno;
                        storeCode = d.StoreCode;
                        donationList =db.DonationList.Where(x => x.DocEntry == docEntry && x.Serialno == serialNo&& x.StoreCode == storeCode).FirstOrDefault();
                        if (donationList != null)
                        {
                            db.DonationList.Remove(donationList);
                        }
                    }
                }
                db.SaveChanges();

                string sql = @"SELECT dh.DocEntry,
                                dh.PlanCode,temp1.Name AS PlanName,
                                dh.Operator,u.UserName AS OperatorName,
                                dh.WorkDay,
                                dh.Creator,u2.UserName AS CreatorName,
                                dh.CreateTime,
                                dh.Updater,u3.UserName AS UpdaterName,
                                dh.updateTime
                                FROM DonationHead AS dh
                                LEFT JOIN Users AS u ON dh.Operator=u.UserId
                                LEFT JOIN 
                                (
	                                SELECT pl.Code,pl.Name
	                                FROM PlanList AS pl
	                                GROUP BY pl.Code,pl.Name
                                ) temp1 ON dh.PlanCode=temp1.Code
                                LEFT JOIN Users AS u2 ON dh.Creator=u2.UserId
                                LEFT JOIN Users AS u3 ON dh.Updater=u3.UserId
                                WHERE 1=1 ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(oper))
                {
                    sql += "  AND dh.Operator=@Operator ";
                    lstParameters.Add(new SqlParameter("Operator", oper));
                }
                if (!string.IsNullOrEmpty(workDate))
                {
                    sql += "  AND dh.WorkDay=@WorkDay ";
                    lstParameters.Add(new SqlParameter("WorkDay", workDate));
                }
                if (!string.IsNullOrEmpty(planCode))
                {
                    sql += "   AND dh.PlanCode=@PlanCode ";
                    lstParameters.Add(new SqlParameter("PlanCode", planCode));
                }
                sql += " ORDER BY dh.DocEntry ";
                ViewData["DisplayData"] = db.Database.SqlQuery<DonationHeadViewModel>(sql, lstParameters.ToArray()).ToList();
            }
            if (fc["action"] == "Reback02_Modal01_btnConfirm")
            {
                string oper = ViewBag.Reback02_Modal01_SlAssiger = fc["Reback02_Modal01_SlAssiger"];
                string workDate = ViewBag.Reback02_Modal01_RebackDate = fc["Reback02_Modal01_txtRebackDate"];
                string planCode = ViewBag.Reback02_Modal01_SlPlan = fc["Reback02_Modal01_SlPlan"];
                ViewBag.DocEntry ="";
                string sql = @"SELECT dh.DocEntry,
                                dh.PlanCode,temp1.Name AS PlanName,
                                dh.Operator,u.UserName AS OperatorName,
                                dh.WorkDay,
                                dh.Creator,u2.UserName AS CreatorName,
                                dh.CreateTime,
                                dh.Updater,u3.UserName AS UpdaterName,
                                dh.updateTime
                                FROM DonationHead AS dh
                                LEFT JOIN Users AS u ON dh.Operator=u.UserId
                                LEFT JOIN 
                                (
	                                SELECT pl.Code,pl.Name
	                                FROM PlanList AS pl
	                                GROUP BY pl.Code,pl.Name
                                ) temp1 ON dh.PlanCode=temp1.Code
                                LEFT JOIN Users AS u2 ON dh.Creator=u2.UserId
                                LEFT JOIN Users AS u3 ON dh.Updater=u3.UserId
                                WHERE 1=1 ";
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(oper))
                {
                    sql += "  AND dh.Operator=@Operator ";
                    lstParameters.Add(new SqlParameter("Operator", oper));
                }
                if (!string.IsNullOrEmpty(workDate))
                {
                    sql += "  AND dh.WorkDay=@WorkDay ";
                    lstParameters.Add(new SqlParameter("WorkDay", workDate));
                }
                if (!string.IsNullOrEmpty(planCode))
                {
                    sql += "   AND dh.PlanCode=@PlanCode ";
                    lstParameters.Add(new SqlParameter("PlanCode", planCode));
                }
                sql += " ORDER BY dh.DocEntry ";
                ViewData["DisplayData"] = db.Database.SqlQuery<DonationHeadViewModel>(sql, lstParameters.ToArray()).ToList();
            }
            if (fc["action"] == "export")
            {
                string docEntry =fc["txtDocEntry"];

                string sql = @"SELECT dl.DocEntry,
                                dl.Serialno,
                                dh.PlanCode,temp1.Name AS PlanName,
                                dl.StoreCode,s.Name AS StoreName,
                                s.ZoneCode,z.Name AS ZoneName,
                                z.RegionCode,r.Name AS RegionName
                                FROM DonationList AS dl
                                LEFT JOIN DonationHead AS dh ON dl.DocEntry=dh.DocEntry
                                LEFT JOIN 
                                (
	                                SELECT pl.Code,pl.Name
	                                FROM PlanList AS pl
	                                GROUP BY pl.Code,pl.Name
                                ) temp1 ON dh.PlanCode=temp1.Code
                                LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code	
                                WHERE 1=1";
                                
                List<SqlParameter> lstParameters = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(docEntry))
                {
                    sql += "  And dl.DocEntry=@DocEntry ";
                    lstParameters.Add(new SqlParameter("DocEntry", docEntry));
                }
             
                sql += " ORDER BY dl.Serialno ";
                IEnumerable<DonationListViewModel> lstExport = db.Database.SqlQuery<DonationListViewModel>(sql, lstParameters.ToArray()).ToList();

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("回收單建立");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("計劃代碼");
                row.CreateCell(1).SetCellValue("所屬聯誼區");
                row.CreateCell(2).SetCellValue("所屬服務區");
                row.CreateCell(3).SetCellValue("回收序號");
                row.CreateCell(4).SetCellValue("店家代號");
                row.CreateCell(5).SetCellValue("店家名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (DonationListViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.PlanName);
                    row.CreateCell(1).SetCellValue(s.RegionName);
                    row.CreateCell(2).SetCellValue(s.ZoneName);
                    row.CreateCell(3).SetCellValue(s.Serialno);
                    row.CreateCell(4).SetCellValue(s.StoreCode);
                    row.CreateCell(5).SetCellValue(s.StoreName);
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "回收單建立_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(string oper, string workDate, List<int> PlanList)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string maxdocEntry = db.DonationHead.Where(x => x.DocEntry.StartsWith(date)).Max(x => x.DocEntry);
            if (string.IsNullOrEmpty(maxdocEntry))
            {
                maxdocEntry = date + "000000";
            }
            DonationHead donationHead;
            DonationList donationList;
            IEnumerable<PlanList> lstPlanLists;
            int serialNo = 1;
            foreach (int s in PlanList)
            {
                donationHead = new DonationHead();
                donationHead.DocEntry = maxdocEntry= date + (Int64.Parse(maxdocEntry.Substring(8)) + 1).ToString("000000");
                donationHead.PlanCode = s;
                donationHead.Operator = oper;
                donationHead.WorkDay = DateTime.Parse(workDate);
                donationHead.Creator = Session["UID"].ToString();
                donationHead.CreateTime = DateTime.Now;
                db.DonationHead.Add(donationHead);
                lstPlanLists=db.PlanList.Where(x => x.Code == s);
                serialNo = 1;
                foreach (PlanList p in lstPlanLists)
                {
                    donationList = new DonationList();
                    donationList.DocEntry = maxdocEntry;
                    donationList.Serialno = serialNo++;
                    donationList.StoreCode = p.StoreCode;
                    donationList.printYN =false;
                    donationList.Additional = false;
                    db.DonationList.Add(donationList);
                }
            }
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDetail(string docEntry)
        {
            string sql = @"SELECT dl.DocEntry,
                                dl.Serialno,
                                dh.PlanCode,temp1.Name AS PlanName,
                                dl.StoreCode,s.Name AS StoreName,
                                s.ZoneCode,z.Name AS ZoneName,
                                z.RegionCode,r.Name AS RegionName
                                FROM DonationList AS dl
                                LEFT JOIN DonationHead AS dh ON dl.DocEntry=dh.DocEntry
                                LEFT JOIN 
                                (
	                                SELECT pl.Code,pl.Name
	                                FROM PlanList AS pl
	                                GROUP BY pl.Code,pl.Name
                                ) temp1 ON dh.PlanCode=temp1.Code
                                LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                                LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                                LEFT JOIN Region AS r ON z.RegionCode=r.Code	
                                WHERE 1=1";

            List<SqlParameter> lstParametersDt = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(docEntry))
            {
                sql += "  And dl.DocEntry=@DocEntry ";
                lstParametersDt.Add(new SqlParameter("@DocEntry", docEntry));
            }

            sql += " ORDER BY dl.Serialno ";
            List<DonationListViewModel> lstDonationListViewModel = db.Database.SqlQuery<DonationListViewModel>(sql, lstParametersDt.ToArray()).ToList();
            return Json(lstDonationListViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetModifyData(string docEntry)
        {
            string sql = @"SELECT dl.DocEntry,
                            dl.Serialno,
                            dh.PlanCode,temp1.Name AS PlanName,
                            dl.StoreCode,s.Name AS StoreName,
                            s.ZoneCode,z.Name AS ZoneName,
                            z.RegionCode,r.Name AS RegionName
                            FROM DonationList AS dl
                            LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                            LEFT JOIN Region AS r ON z.RegionCode=r.Code
                            LEFT JOIN DonationHead AS dh ON dl.DocEntry=dh.DocEntry
                            LEFT JOIN 
                            (
	                            SELECT pl.Code,pl.Name
	                            FROM PlanList AS pl
	                            GROUP BY pl.Code,pl.Name	
                            ) temp1 ON dh.PlanCode=temp1.Code
                            WHERE dl.DocEntry='"+docEntry+@"'
                            ORDER BY dl.Serialno ";
            List<DonationListViewModel> lstDonationListViewModel =db.Database.SqlQuery<DonationListViewModel>(sql).ToList();
            return Json(lstDonationListViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(List<DonationListViewModel> lstDonationList)
        {
            List<int> lstCheck = new List<int>();
            DonationList donationList;
            string  docEntry;
            string planName;
            int serialNo;
            int storeCode;
            foreach (DonationListViewModel d in lstDonationList)
            {
                docEntry = d.DocEntry;
                planName = d.PlanName;
              
                if (string.IsNullOrEmpty(planName))
                {
                    if (lstCheck.Contains(d.StoreCode))
                    {
                        return Json("回收單號：" + docEntry + ", 店家代號:" + d.StoreCode + "重複", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    serialNo = d.SerialnoOld;
                    storeCode = d.StoreCodeOld;
                    donationList =
                        db.DonationList.Where(
                            x => x.DocEntry == docEntry && x.Serialno == serialNo && x.StoreCode == storeCode)
                            .FirstOrDefault();
                    if (donationList == null)
                    {
                        return Json("回收單號：" + docEntry + ",回收序號：" + serialNo + ",店家代號:" + storeCode + "不存在", JsonRequestBehavior.AllowGet);
                    }
                    if (lstCheck.Contains(d.StoreCode))
                    {
                        return Json("回收單號：" + docEntry + ", 店家代號:" + d.StoreCode + "重複", JsonRequestBehavior.AllowGet);
                    }
                }
                lstCheck.Add(d.StoreCode);
            }
            docEntry = lstDonationList[0].DocEntry;
            IEnumerable<DonationList> lstDel = db.DonationList.Where(x => x.DocEntry == docEntry).ToList();
            foreach (DonationList d in lstDel)
            {
                db.DonationList.Remove(d);
            }
            DonationHead donationHead = db.DonationHead.Where(x => x.DocEntry == docEntry).FirstOrDefault();
            if (donationHead != null)
            {
                donationHead.updateTime=DateTime.Now;
                donationHead.Updater = Session["UID"].ToString();
                db.Entry(donationHead).State=EntityState.Modified;
                db.DonationHead.AddOrUpdate(donationHead);
            }
            DonationList daDonationListOld;
            foreach (DonationListViewModel d in lstDonationList)
            {
                planName = d.PlanName;
                donationList = new DonationList();
                donationList.DocEntry = d.DocEntry;
                donationList.Serialno = d.Serialno;
                donationList.StoreCode = d.StoreCode;
                if (!string.IsNullOrEmpty(planName))
                {
                    daDonationListOld = lstDel.Where(x => x.StoreCode == d.StoreCodeOld).FirstOrDefault();
                    donationList.Coin = daDonationListOld.Coin;
                    donationList.money1000 = daDonationListOld.money1000;
                    donationList.money500 = daDonationListOld.money500;
                    donationList.money200 = daDonationListOld.money200;
                    donationList.money100 = daDonationListOld.money100;
                    donationList.Operator = daDonationListOld.Operator;
                    donationList.OperationTime = daDonationListOld.OperationTime;
                    donationList.CheckTotal = daDonationListOld.CheckTotal;
                    donationList.CheckInvoice = daDonationListOld.CheckInvoice;
                    donationList.Checkcurrency = daDonationListOld.Checkcurrency;
                    donationList.CheckCurrencyCoin = daDonationListOld.CheckCurrencyCoin;
                    donationList.Typer = daDonationListOld.Typer;
                    donationList.TypeTime = daDonationListOld.TypeTime;
                    donationList.ReCheckCoin = daDonationListOld.ReCheckCoin;
                    donationList.ReCheckmoney1000 = daDonationListOld.ReCheckmoney1000;
                    donationList.ReCheckmoney500 = daDonationListOld.ReCheckmoney500;
                    donationList.ReCheckmoney200 = daDonationListOld.ReCheckmoney200;
                    donationList.ReCheckmoney100 = daDonationListOld.ReCheckmoney100;
                    donationList.Rechecker = daDonationListOld.Rechecker;
                    donationList.Rechecktime = daDonationListOld.Rechecktime;
                    donationList.checker = daDonationListOld.checker;
                    donationList.CheckTime = daDonationListOld.CheckTime;
                    donationList.Description = daDonationListOld.Description;
                    donationList.printYN = daDonationListOld.printYN;
                    donationList.Additional = daDonationListOld.Additional;
                }
                else
                {
                    donationList.printYN = false;
                    donationList.Additional = true;
                }
                db.Entry(donationList).State = EntityState.Added;
                db.DonationList.Add(donationList);
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

        [HttpPost]
        public JsonResult GetZoneByRegion2(string region)
        {
            ReturnValue returnValue = new ReturnValue();
            List<Zone> lstZone;
            List<Store> lstStores;
            string sqlZone,sqlStore;
            if (string.IsNullOrEmpty(region))
            {
                sqlZone = "SELECT * FROM Zone AS z";
                sqlStore = "SELECT * FROM Store AS s";
            }
            else
            {
                sqlZone = "SELECT * FROM Zone AS z WHERE z.RegionCode=" + region;
                sqlStore ="  SELECT * FROM Store AS s WHERE s.ZoneCode IN (SELECT z.Code FROM Zone AS z WHERE z.RegionCode=" +region + ")";
            }
            lstZone = db.Database.SqlQuery<Zone>(sqlZone).ToList();
            returnValue.lstZones = lstZone;
            lstStores= db.Database.SqlQuery<Store>(sqlStore).ToList();
            returnValue.lstStores = lstStores;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStoreData2(string region,string zone)
        {
            ReturnValue returnValue = new ReturnValue();
            returnValue.zoneValue = zone;
            string sqlStore;
            if (string.IsNullOrEmpty(zone))
            {
                returnValue.regionValue = region;
                if (string.IsNullOrEmpty(region))
                {
                    sqlStore = "SELECT * FROM Store AS s";
                }
                else
                {
                    sqlStore = "SELECT * FROM Store AS s WHERE s.ZoneCode IN (SELECT z.Code FROM Zone AS z WHERE z.RegionCode=" + region + ")";
                }
            }
            else
            {
                int zoneCode = int.Parse(zone);
                returnValue.regionValue =Convert.ToString(db.Zone.Where(x => x.Code == zoneCode).FirstOrDefault().RegionCode);
                sqlStore= "SELECT * FROM Store AS s WHERE s.ZoneCode=" + zone;
            }
            List<Store> lstStores;
            lstStores = db.Database.SqlQuery<Store>(sqlStore).ToList();
            returnValue.lstStores = lstStores;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRegionAndZoneByStore2(int store)
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
}