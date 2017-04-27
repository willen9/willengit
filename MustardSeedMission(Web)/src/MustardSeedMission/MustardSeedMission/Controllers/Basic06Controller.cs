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
    public class Basic06Controller : BaseController
    {
        // GET: Basic06
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A006";
            ViewData["DisplayData"] = new List<BusinessCategory>();
            ViewData["AllBusinessCategory"] = db.BusinessCategory;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A006";
            ViewData["DisplayData"] = new List<BusinessCategory>();
            ViewData["AllBusinessCategory"] = db.BusinessCategory;
            if (fc["action"] == "delete")
            {
                string code = ViewBag.Basic06_Modal01_Code = fc["txtCode"];
                string name = ViewBag.Basic06_Modal01_Name = fc["txtName"];

                string codes = fc["chk"];
                BusinessCategory businessCategory;
                int code2;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c);
                    if (db.Store.Where(x => x.BusinessCode == code2).Any())
                    {
                        ViewBag.js = "<script>alert('營業種類:" + c + "正在使用中，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    businessCategory = db.BusinessCategory.Find(code2);
                    if (businessCategory != null)
                    {
                        db.BusinessCategory.Remove(businessCategory);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.BusinessCategory.ToList();
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
                ViewData["AllBusinessCategory"] = db.BusinessCategory;
            }
            if (fc["action"] == "Basic06_Modal01_btnConfirm")
            {
                string code = ViewBag.Basic06_Modal01_Code = fc["Basic06_Modal01_txtCode"];
                string name = ViewBag.Basic06_Modal01_Name = fc["Basic06_Modal01_txtName"];
                var data = db.BusinessCategory.ToList();
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

                var data = db.BusinessCategory.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                IEnumerable<BusinessCategory> lstExport = data;

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("營業種類主檔資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("營業種類代號");
                row.CreateCell(1).SetCellValue("營業種類名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (BusinessCategory s in lstExport)
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
                string fileName = "營業種類主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
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
            BusinessCategory businessCategory = db.BusinessCategory.Where(x => x.Name == name).FirstOrDefault();
            if (businessCategory != null)
            {
                return Json("營業種類名稱已存在", JsonRequestBehavior.AllowGet);
            }
            businessCategory = new BusinessCategory();
            int max = db.BusinessCategory.Count();
            if (max > 0)
            {
                max = db.BusinessCategory.Max(x => x.Code);
            }
            businessCategory.Code = max + 1;
            businessCategory.Name = name;
            businessCategory.Creator = Session["UID"].ToString();
            businessCategory.CreateTime = DateTime.Now;
            db.BusinessCategory.Add(businessCategory);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(BusinessCategory businessCategory)
        {
            int code = businessCategory.Code;
            string name = businessCategory.Name;
            BusinessCategory businessCategorySl = db.BusinessCategory.FirstOrDefault(x => x.Code == code);
            if (businessCategorySl == null)
            {
                return Json("營業種類代碼不存在", JsonRequestBehavior.AllowGet);
            }
            BusinessCategory businessCategoryOth = db.BusinessCategory.FirstOrDefault(x => x.Name == name && x.Code != code);
            if (businessCategoryOth != null)
            {
                return Json("存在相同營業種類名稱", JsonRequestBehavior.AllowGet);
            }
            businessCategorySl.Name = name;
            businessCategorySl.Updater = Session["UID"].ToString();
            businessCategorySl.updateTime = DateTime.Now;
            db.Entry(businessCategorySl).State = EntityState.Modified;
            db.BusinessCategory.AddOrUpdate(businessCategorySl);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}