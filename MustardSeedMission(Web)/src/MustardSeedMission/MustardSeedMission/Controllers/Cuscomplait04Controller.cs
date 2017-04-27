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
    public class Cuscomplait04Controller : BaseController
    {
        // GET: Cuscomplait04
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A017";
            ViewData["AllCategory"] = db.Category;
            ViewData["DisplayData"] = new List<Category>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A017";
            ViewData["AllCategory"] = db.Category;
            ViewData["DisplayData"] = new List<Category>();
            if (fc["action"] == "delete")
            {
                string code = ViewBag.Cuscomplait03_Modal01_Category = fc["txtCode"];
                string name = ViewBag.Cuscomplait04_Modal01_TypeName = fc["txtName"];

                string codes = fc["chk"];
                Category category;
                int code2;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c);
                    if (db.Complaints.Where(x => x.Categolgy == code2).Any())
                    {
                        ViewBag.js = "<script>alert('客訴單類別代碼:"+c+"已使用，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    category = db.Category.Find(code2);
                    if (category != null)
                    {
                        db.Category.Remove(category);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.Category.ToList();
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
                ViewData["AllCategory"] = db.Category;
            }
            if (fc["action"] == "Cuscomplait04_Modal01_btnConfirm")
            {
                string code = ViewBag.Cuscomplait03_Modal01_Category = fc["Cuscomplait04_Modal01_No"];
                string name = ViewBag.Cuscomplait04_Modal01_TypeName = fc["Cuscomplait04_Modal01_TypeName"];
                var data = db.Category.ToList();
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

                var data = db.Category.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                IEnumerable<Category> lstExport = data;

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("客訴單類別主檔");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("客訴單類別代碼");
                row.CreateCell(1).SetCellValue("客訴單類別名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (Category s in lstExport)
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
                string fileName = "客訴單類別主檔_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
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
            Category category = db.Category.Where(x => x.Name == name).FirstOrDefault();
            if (category != null)
            {
                return Json("客訴單類別名稱已存在", JsonRequestBehavior.AllowGet);
            }
            category = new Category();
            int max = db.Category.Count();
            if (max > 0)
            {
                max = db.Category.Max(x => x.Code);
            }
            category.Code = max + 1;
            category.Name = name;
            category.Creator = Session["UID"].ToString();
            category.CreateTime = DateTime.Now;
            db.Category.Add(category);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(Category category)
        {
            int code = category.Code;
            string name = category.Name;
            Category categorySl = db.Category.FirstOrDefault(x => x.Code == code);
            if (categorySl == null)
            {
                return Json("客訴單類別代碼不存在", JsonRequestBehavior.AllowGet);
            }
            Category categoryOth = db.Category.FirstOrDefault(x => x.Name == name && x.Code != code);
            if (categoryOth != null)
            {
                return Json("存在相同客訴單類別名稱", JsonRequestBehavior.AllowGet);
            }
            categorySl.Name = name;
            categorySl.Updater = Session["UID"].ToString();
            categorySl.updateTime = DateTime.Now;
            db.Entry(categorySl).State = EntityState.Modified;
            db.Category.AddOrUpdate(categorySl);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}