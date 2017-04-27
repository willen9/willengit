using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;
using MustardSeedMission.ViewModels;

namespace MustardSeedMission.Controllers
{
    public class Cuscomplait01Controller : BaseController
    {
        // GET: Cuscomplait01
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A014";
            ViewData["AllStore"] = db.Store;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllRegion"] = db.Region;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A014";
            ViewData["AllStore"] = db.Store;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllRegion"] = db.Region;
            if (fc["action"] == "btnConfirm")
            {
                Complaints complaints = new Complaints();
                string date = DateTime.Now.ToString("yyyyMMdd");
                string maxdocEntry = db.Complaints.Where(x => x.DocEntry.StartsWith(date)).Max(x => x.DocEntry);
                if (string.IsNullOrEmpty(maxdocEntry))
                {
                    maxdocEntry = date + "000000";
                }
                complaints.DocEntry = date + (Int64.Parse(maxdocEntry.Substring(8)) + 1).ToString("000000");
                complaints.StoreCode = int.Parse(fc["slShopId"]);
                complaints.StoreName = fc["txtShopName"];
                complaints.EvenDate = DateTime.Now;
                complaints.EvenYM = DateTime.Now.AddYears(-1911).ToString("yyyMM");
                complaints.Creator = fc["txtCreator"];
                //complaints.CreateTime=DateTime.Now;
                if (!string.IsNullOrEmpty(fc["txtType"]))
                {
                    complaints.Categolgy = int.Parse(fc["txtType"]);
                }
                complaints.Event = fc["txtEvent"];
                complaints.Address = fc["txtAddress"];
                complaints.Tel = fc["txtTel"];
                complaints.Reason = fc["txtReason"];
                complaints.Description = fc["txtRemark"];
                complaints.Operator = Session["UID"].ToString();
                complaints.OperrateTime = DateTime.Now;
                complaints.Finish = false;
                db.Complaints.Add(complaints);
                db.SaveChanges();
                ViewBag.js = "<script>alert('保存成功')</script>";
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetZoneByRegion(string region)
        {
            ReturnValue returnValue = new ReturnValue();
            List<Zone> lstZone;
            List<Store> lstStore;
            string sqlZone;
            string sqlStore;
            if (string.IsNullOrEmpty(region))
            {
                sqlZone = "SELECT * FROM Zone AS z";
                sqlStore = @"SELECT * FROM Store AS s";
            }
            else
            {
                sqlZone = "SELECT * FROM Zone AS z WHERE z.RegionCode=" + region;
                sqlStore = @"SELECT *
                            FROM Store AS s 
                            WHERE s.ZoneCode IN 
                            (
	                            SELECT z.Code
	                            FROM Zone AS z 
	                            WHERE z.RegionCode=" + region + ")";
            }
            lstZone = db.Database.SqlQuery<Zone>(sqlZone).ToList();
            returnValue.lstZones = lstZone;
            lstStore = db.Database.SqlQuery<Store>(sqlStore).ToList();
            returnValue.lstStores = lstStore;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStoreByZone(string zone, string region)
        {
            ReturnValue returnValue = new ReturnValue();

            List<Store> lstStore;
            string sqlStore;
            if (string.IsNullOrEmpty(zone))
            {
                if (string.IsNullOrEmpty(region))
                {
                    sqlStore = @"SELECT * FROM Store AS s";
                    returnValue.regionCode = -999;
                }
                else
                {
                    sqlStore = @"SELECT *
                            FROM Store AS s 
                            WHERE s.ZoneCode IN 
                            (
	                            SELECT z.Code
	                            FROM Zone AS z 
	                            WHERE z.RegionCode=" + region + ")";
                    returnValue.regionCode = int.Parse(region);
                }
            }
            else
            {
                int zoneCode = int.Parse(zone);
                sqlStore = @"SELECT * FROM Store AS s WHERE s.ZoneCode =" + zone;
                returnValue.regionCode = db.Zone.Where(x => x.Code == zoneCode).FirstOrDefault().RegionCode;
            }
            lstStore = db.Database.SqlQuery<Store>(sqlStore).ToList();
            returnValue.lstStores = lstStore;
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataByStore(string storeId)
        {
            StoreViewModel storeViewModel = new StoreViewModel();

            int storeCode = int.Parse(storeId);
            Store store = db.Store.Where(x => x.Code == storeCode).FirstOrDefault();
            storeViewModel.Address = store.Address;
            storeViewModel.Tel = store.Tel;
            storeViewModel.Name = store.Name;
            storeViewModel.ZoneCode = store.ZoneCode;
            int? zoneCode = store.ZoneCode;
            storeViewModel.RegionCode = db.Zone.Where(x => x.Code == zoneCode).FirstOrDefault().RegionCode;
            return Json(storeViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCheckType(string type)
        {
            JsonResult json;
            try
            {
                int typeCode = int.Parse(type);
                Category category = db.Category.Find(typeCode);
                if (category != null)
                {
                    json = new JsonResult { Data = new { valid = true } };
                    return json;
                }
                json = new JsonResult { Data = new { valid = false } };
            }
            catch 
            {
                json = new JsonResult { Data = new { valid = false } };
            }
            return json;
        }
    }
}