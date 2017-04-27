using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    public class Basic04Controller : BaseController
    {
        // GET: Basic04
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A004";
            ViewData["DisplayData"] = new List<ZoneViewModel>();
            ViewData["AllRegion"] = db.Region;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A004";
            ViewData["DisplayData"] = new List<ZoneViewModel>();
            ViewData["AllRegion"] = db.Region;
            if (fc["action"] == "delete")
            {
                string regionCode = ViewBag.Basic04_Modal01_SlName = fc["txtRName"];
                string serCode = ViewBag.Basic04_Modal01_SerId = fc["txtSerId"];
                string serName = ViewBag.Basic04_Modal01_SerName = fc["txtSerName"];
                string codes = fc["chk"];
                Zone zone;
                int code;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code = int.Parse(c);
                    if (db.Store.Where(x => x.ZoneCode == code).Any())
                    {
                        ViewBag.js= "<script>alert('服務區代碼:" + c + "正在使用中，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    zone = db.Zone.Find(code);
                    if (zone != null)
                    {
                        db.Zone.Remove(zone);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.Zone.ToList();
                if (!string.IsNullOrEmpty(regionCode))
                {
                    int cd = int.Parse(regionCode);
                    data = data.Where(o => o.RegionCode == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serCode))
                {
                    int cd = int.Parse(serCode);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serName))
                {
                    data = data.Where(o => o.Name.Contains(serName)).ToList();
                }

                ViewData["DisplayData"] = data.GroupJoin(db.Region, x => x.RegionCode, y => y.Code, (x, y) => new { x, y }).Select(o => new ZoneViewModel()
                {
                    Code = o.x.Code,
                    Name = o.x.Name,
                    RegionCode = o.x.RegionCode,
                    RName = o.y.FirstOrDefault().Name
                });
            }
            if (fc["action"] == "Basic04_Modal01_btnConfirm")
            {
                string regionCode = ViewBag.Basic04_Modal01_SlName = fc["Basic04_Modal01_SlName"];
                string serCode = ViewBag.Basic04_Modal01_SerId = fc["Basic04_Modal01_SerId"];
                string serName = ViewBag.Basic04_Modal01_SerName = fc["Basic04_Modal01_SerName"];
                var data = db.Zone.ToList();
                if (!string.IsNullOrEmpty(regionCode))
                {
                    int cd = int.Parse(regionCode);
                    data = data.Where(o => o.RegionCode == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serCode))
                {
                    int cd = int.Parse(serCode);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serName))
                {
                    data = data.Where(o => o.Name.Contains(serName)).ToList();
                }

                ViewData["DisplayData"] = data.GroupJoin(db.Region, x => x.RegionCode, y => y.Code, (x, y) => new { x, y }).Select(o => new ZoneViewModel()
                {
                    Code = o.x.Code,
                    Name = o.x.Name,
                    RegionCode = o.x.RegionCode,
                    RName = o.y.FirstOrDefault().Name
                });
            }
            if (fc["action"] == "export")
            {
                string regionCode = fc["txtRName"];
                string serCode = fc["txtSerId"];
                string serName = fc["txtSerName"];

                var data = db.Zone.ToList();
                if (!string.IsNullOrEmpty(regionCode))
                {
                    int cd = int.Parse(regionCode);
                    data = data.Where(o => o.RegionCode == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serCode))
                {
                    int cd = int.Parse(serCode);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(serName))
                {
                    data = data.Where(o => o.Name.Contains(serName)).ToList();
                }

                IEnumerable<ZoneViewModel> lstExport = data.GroupJoin(db.Region, x => x.RegionCode, y => y.Code, (x, y) => new { x, y }).Select(o => new ZoneViewModel()
                {
                    Code = o.x.Code,
                    Name = o.x.Name,
                    RegionCode = o.x.RegionCode,
                    RName = o.y.FirstOrDefault().Name
                });

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("服務區主檔資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("聯誼區名稱");
                row.CreateCell(1).SetCellValue("服務區代碼");
                row.CreateCell(2).SetCellValue("服務區名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (ZoneViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.RName);
                    row.CreateCell(1).SetCellValue(s.Code);
                    row.CreateCell(2).SetCellValue(s.Name);
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "服務區主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(string regionCode, string serName)
        {
            //int region = int.Parse(regionCode);
            //Zone zone = db.Zone.Where(x => x.Name == serName&&x.RegionCode== region).FirstOrDefault();
            Zone zone = db.Zone.Where(x => x.Name == serName).FirstOrDefault();
            if (zone != null)
            {
                return Json("服務區名稱已存在", JsonRequestBehavior.AllowGet);
            }
            zone = new Zone();
            int max = db.Zone.Count();
            if (max > 0)
            {
                max = db.Zone.Max(x => x.Code);
            }
            zone.Code = max + 1;
            zone.Name = serName;
            zone.RegionCode = int.Parse(regionCode);
            zone.Creator = Session["UID"].ToString();
            zone.CreateTime = DateTime.Now;
            db.Zone.Add(zone);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(Zone zone)
        {
            int code = zone.Code;
            string name = zone.Name;
            Zone zoneSl = db.Zone.FirstOrDefault(x => x.Code == code);
            if (zoneSl == null)
            {
                return Json("服務區代碼不存在", JsonRequestBehavior.AllowGet);
            }
            Zone zoneOth = db.Zone.FirstOrDefault(x => x.Name == name && x.Code != code);
            if (zoneOth != null)
            {
                return Json("存在相同服務區名稱", JsonRequestBehavior.AllowGet);
            }
            zoneSl.Name = zone.Name;
            zoneSl.RegionCode = zone.RegionCode;
            zoneSl.Updater = Session["UID"].ToString();
            zoneSl.updateTime = DateTime.Now;
            db.Entry(zoneSl).State = EntityState.Modified;
            db.Zone.AddOrUpdate(zoneSl);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}