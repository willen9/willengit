using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;
using MustardSeedMission.ViewModels;

namespace MustardSeedMission.Controllers
{
    public class Cuscomplait02Controller : BaseController
    {
        // GET: Cuscomplait02
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A015";
            string sql = @"SELECT c.*,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName
                            FROM Complaints AS c
                            LEFT JOIN Store AS s ON c.StoreCode = s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode = z.Code
                            LEFT JOIN Region AS r ON z.RegionCode = r.Code
                            WHERE isnull(c.Finish,0) != 1";
            ViewData["DisplayData"] = db.Database.SqlQuery<ComplaintsViewModel>(sql);
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllStore"] = db.Store;
            ViewData["AllCategory"] = db.Category;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A015";
            ViewData["AllRegion"] = db.Region;
            ViewData["AllZone"] = db.Zone;
            ViewData["AllStore"] = db.Store;
            ViewData["AllCategory"] = db.Category;
            if (fc["action"] == "btnEnd")
            {
                string docEntrys = fc["chk"];
                Complaints complaints;
                foreach (string docEntry in docEntrys.Split(','))
                {
                    complaints = db.Complaints.Find(docEntry);
                    if (complaints != null)
                    {
                        complaints.Finish = true;
                        db.Entry(complaints).State=EntityState.Modified;
                        db.Complaints.AddOrUpdate(complaints);
                    }
                }
                db.SaveChanges();
                ViewBag.js = "<script>alert('結案成功！')</script>";
            }
            string sql = @"SELECT c.*,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName
                            FROM Complaints AS c
                            LEFT JOIN Store AS s ON c.StoreCode = s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode = z.Code
                            LEFT JOIN Region AS r ON z.RegionCode = r.Code
                            WHERE isnull(c.Finish,0) != 1";
            ViewData["DisplayData"] = db.Database.SqlQuery<ComplaintsViewModel>(sql);
            return View();
        }

        [HttpPost]
        public JsonResult GetModifyData(string docEntry)
        {
            string sql = @"SELECT c.*,
                            z.Code AS ZoneCode,z.Name AS ZoneName,
                            r.Code AS RegionCode,r.Name AS RegionName
                            FROM Complaints AS c
                            LEFT JOIN Store AS s ON c.StoreCode=s.Code
                            LEFT JOIN Zone AS z ON s.ZoneCode=z.Code
                            LEFT JOIN Region AS r ON z.RegionCode=r.Code
                            WHERE c.DocEntry='"+docEntry+"'";
            ComplaintsViewModel complaintsViewModels = db.Database.SqlQuery<ComplaintsViewModel>(sql).FirstOrDefault();
            return Json(complaintsViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdateData(Complaints complaints)
        {
            string docEntry = complaints.DocEntry;
            Complaints complaintsUpdate = db.Complaints.Find(docEntry);
            complaintsUpdate.StoreCode = complaints.StoreCode;
            complaintsUpdate.StoreName = complaints.StoreName;
            complaintsUpdate.Address = complaints.Address;
            complaintsUpdate.Tel = complaints.Tel;
            complaintsUpdate.Creator = complaints.Creator;
            complaintsUpdate.Categolgy = complaints.Categolgy;
            complaintsUpdate.Event = complaints.Event;
            complaintsUpdate.Reason = complaints.Reason;
            complaintsUpdate.Description = complaints.Description;
            complaintsUpdate.deal = complaints.deal;
            complaintsUpdate.Operator = complaints.Operator;
            complaintsUpdate.OperrateTime=DateTime.Now;
            db.Entry(complaintsUpdate).State=EntityState.Modified;
            db.Complaints.AddOrUpdate(complaintsUpdate);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}