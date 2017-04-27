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
    public class Basic03Controller : BaseController
    {
        // GET: Basic03
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A003";
            ViewData["DisplayData"]=new List<Region>();
            ViewData["AllRegion"] = db.Region;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A003";
            ViewData["DisplayData"] = new List<Region>();
            ViewData["AllRegion"] = db.Region;
            if (fc["action"] == "del")
            {
                string codeSl = ViewBag.Basic03_Modal01_SlName = fc["txtSlName"];
                string codeTxt = ViewBag.Basic03_Modal01_Code = fc["txtCode"];
                string codes = fc["chk"];
                Region region;
                int code;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code = int.Parse(c);
                    if (db.Zone.Where(x => x.RegionCode == code).Any())
                    {
                        ViewBag.js = "<script>alert('聯誼區代碼:"+c+"正在使用中，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    region=db.Region.Find(code);
                    if (region != null)
                    {
                        db.Region.Remove(region);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.Region.ToList();
                if (!string.IsNullOrEmpty(codeTxt))
                {
                    int cd = int.Parse(codeTxt);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(codeSl))
                    {
                        int cd = int.Parse(codeSl);
                        data = data.Where(o => o.Code == cd).ToList();
                    }
                }
                ViewData["DisplayData"] = data;
                ViewData["AllRegion"] = db.Region;
            }
            if (fc["action"] == "Basic03_Modal01_btnConfirm")
            {
                string codeSl = ViewBag.Basic03_Modal01_SlName = fc["Basic03_Modal01_SlName"];
                string codeTxt = ViewBag.Basic03_Modal01_Code = fc["Basic03_Modal01_txtCode"];

                var data = db.Region.ToList();
                if (!string.IsNullOrEmpty(codeTxt))
                {
                    int cd = int.Parse(codeTxt);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(codeSl))
                    {
                        int cd = int.Parse(codeSl);
                        data = data.Where(o => o.Code == cd).ToList();
                    }
                }
                ViewData["DisplayData"] = data;
            }
            if (fc["action"] == "export")
            {
                string codeSl = fc["txtSlName"];
                string codeTxt  = fc["txtCode"];
                var data = db.Region.ToList();
                if (!string.IsNullOrEmpty(codeTxt))
                {
                    int cd = int.Parse(codeTxt);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(codeSl))
                    {
                        int cd = int.Parse(codeSl);
                        data = data.Where(o => o.Code == cd).ToList();
                    }
                }

                IEnumerable<Region> lstExport = data;
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("聯誼區主檔資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("聯誼區代號");
                row.CreateCell(1).SetCellValue("聯誼區名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (Region s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.Code);
                    row.CreateCell(1).SetCellValue(s.Name);
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "聯誼區主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(string name)
        {
            Region region = db.Region.Where(x=>x.Name==name).FirstOrDefault();
            if (region != null)
            {
                return Json("聯誼區名稱已存在", JsonRequestBehavior.AllowGet);
            }
            region = new Region();
            int max = db.Region.Count();
            if (max > 0)
            {
                max = db.Region.Max(x => x.Code);
            }
            region.Code =max+1;
            region.Name = name;
            region.Creator = Session["UID"].ToString();
            region.CreateTime = DateTime.Now;
            db.Region.Add(region);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(Region region)
        {
            try
            {
                int code = region.Code;
                string name = region.Name;
                Region regionSl = db.Region.FirstOrDefault(x=>x.Code==code);
                if (regionSl == null)
                {
                    return Json("聯誼區代碼不存在", JsonRequestBehavior.AllowGet);
                }
                Region regionOth = db.Region.FirstOrDefault(x => x.Name == name && x.Code != code);
                if (regionOth!=null)
                {
                    return Json("存在相同聯誼區名稱", JsonRequestBehavior.AllowGet);
                }
                regionSl.Name = region.Name;
                regionSl.Updater = Session["UID"].ToString();
                regionSl.updateTime=DateTime.Now;
                db.Entry(regionSl).State = EntityState.Modified;
                db.Region.AddOrUpdate(regionSl);
                db.SaveChanges();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}