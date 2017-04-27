using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MustardSeedMission.Controllers
{
    public class Basic07Controller : BaseController
    {
        // GET: Basic07
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A007";
            ViewData["DisplayData"] = new List<Modification>();
            ViewData["AllModification"] = db.Modification;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A007";
            ViewData["DisplayData"] = new List<Modification>();
            ViewData["AllModification"] = db.Modification;
            if (fc["action"] == "delete")
            {
                string code = ViewBag.Basic07_Modal01_Code = fc["txtCode"];
                string name = ViewBag.Basic07_Modal01_Name = fc["txtName"];

                string codes = fc["chk"];
                Modification modification;
                int code2;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c);
                    if (db.Store.Where(x => x.ModificationCode == code2).Any())
                    {
                        ViewBag.js = "<script>alert('異動原因:" + c + "正在使用中，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    modification = db.Modification.Find(code2);
                    if (modification != null)
                    {
                        db.Modification.Remove(modification);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.Modification.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                ViewData["DisplayData"] = data;
                ViewData["AllModification"] = db.Modification;
            }
            if (fc["action"] == "Basic07_Modal01_btnConfirm")
            {
                string code = ViewBag.Basic07_Modal01_Code = fc["Basic07_Modal01_txtCode"];
                string name = ViewBag.Basic07_Modal01_Name = fc["Basic07_Modal01_txtName"];
                var data = db.Modification.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                ViewData["DisplayData"] = data;
            }
            if (fc["action"] == "export")
            {
                string code = fc["txtCode"];
                string name = fc["txtName"];

                var data = db.Modification.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                IEnumerable<Modification> lstExport = data;

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("異動原因主檔資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("異動代號");
                row.CreateCell(1).SetCellValue("異動原因");

                int rowIndex;
                rowIndex = 1;
                foreach (Modification s in lstExport)
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
                string fileName = "異動原因主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
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
            Modification modification = db.Modification.Where(x => x.Name == name).FirstOrDefault();
            if (modification != null)
            {
                return Json("異動原因已存在", JsonRequestBehavior.AllowGet);
            }
            modification = new Modification();
            int max = db.Modification.Count();
            if (max > 0)
            {
                max = db.Modification.Max(x => x.Code);
            }
            modification.Code = max + 1;
            modification.Name = name;
            modification.Creator = Session["UID"].ToString();
            modification.CreateTime = DateTime.Now;
            db.Modification.Add(modification);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(Modification modification)
        {
            int code = modification.Code;
            string name = modification.Name;
            Modification modificationSl = db.Modification.FirstOrDefault(x => x.Code == code);
            if (modificationSl == null)
            {
                return Json("異動原因代碼不存在", JsonRequestBehavior.AllowGet);
            }
            Modification modificationOth = db.Modification.FirstOrDefault(x => x.Name == name && x.Code != code);
            if (modificationOth != null)
            {
                return Json("存在相同異動原因", JsonRequestBehavior.AllowGet);
            }
            modificationSl.Name = name;
            modificationSl.Updater = Session["UID"].ToString();
            modificationSl.updateTime = DateTime.Now;
            db.Entry(modificationSl).State = EntityState.Modified;
            db.Modification.AddOrUpdate(modificationSl);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}