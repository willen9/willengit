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
    public class Basic05Controller : BaseController
    {
        // GET: Basic05
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A005";
            ViewData["DisplayData"] = new List<DonationBox>();
            ViewData["AllDonationBox"] = db.DonationBox;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A005";
            ViewData["DisplayData"] = new List<DonationBox>();
            ViewData["AllDonationBox"] = db.DonationBox;
            if (fc["action"] == "delete")
            {
                string code = ViewBag.Basic05_Modal01_Code= fc["txtCode"];
                string name = ViewBag.Basic05_Modal01_Name = fc["txtName"];

                string codes = fc["chk"];
                DonationBox donationBox;
                int code2;
                bool isOk = true;
                foreach (string c in codes.Split(','))
                {
                    code2 = int.Parse(c);
                    if (db.Store.Where(x => x.DBC == code2).Any())
                    {
                        ViewBag.js="<script>alert('盒型代碼:" + c + "正在使用中，無法刪除')</script>";
                        isOk = false;
                        break;
                    }
                    donationBox = db.DonationBox.Find(code2);
                    if (donationBox != null)
                    {
                        db.DonationBox.Remove(donationBox);
                    }
                }
                if (isOk)
                {
                    db.SaveChanges();
                }
                var data = db.DonationBox.ToList();
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
                ViewData["AllDonationBox"] = db.DonationBox;
            }
            if (fc["action"] == "Basic05_Modal01_btnConfirm")
            {
                string code = ViewBag.Basic05_Modal01_Code = fc["Basic05_Modal01_txtCode"];
                string name = ViewBag.Basic05_Modal01_Name = fc["Basic05_Modal01_txtName"];
                var data = db.DonationBox.ToList();
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

                var data = db.DonationBox.ToList();
                if (!string.IsNullOrEmpty(code))
                {
                    int cd = int.Parse(code);
                    data = data.Where(o => o.Code == cd).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.Name.Contains(name)).ToList();
                }

                IEnumerable<DonationBox> lstExport = data;

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("盒型主檔資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("盒型代號");
                row.CreateCell(1).SetCellValue("盒型名稱");

                int rowIndex;
                rowIndex = 1;
                foreach (DonationBox s in lstExport)
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
                string fileName = "盒型主檔資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
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
            DonationBox donationBox = db.DonationBox.Where(x => x.Name == name).FirstOrDefault();
            if (donationBox != null)
            {
                return Json("盒型名稱已存在", JsonRequestBehavior.AllowGet);
            }
            donationBox = new DonationBox();
            int max = db.DonationBox.Count();
            if (max > 0)
            {
                max = db.DonationBox.Max(x => x.Code);
            }
            donationBox.Code = max + 1;
            donationBox.Name = name;
            donationBox.Creator = Session["UID"].ToString();
            donationBox.CreateTime = DateTime.Now;
            db.DonationBox.Add(donationBox);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(DonationBox donationBox)
        {
            int code = donationBox.Code;
            string name = donationBox.Name;
            DonationBox donationBoxSl = db.DonationBox.FirstOrDefault(x => x.Code == code);
            if (donationBoxSl == null)
            {
                return Json("盒型代碼不存在", JsonRequestBehavior.AllowGet);
            }
            DonationBox donationBoxOth = db.DonationBox.FirstOrDefault(x => x.Name == name && x.Code != code);
            if (donationBoxOth != null)
            {
                return Json("存在相同盒型名稱", JsonRequestBehavior.AllowGet);
            }
            donationBoxSl.Name = name;
            donationBoxSl.Updater = Session["UID"].ToString();
            donationBoxSl.updateTime = DateTime.Now;
            db.Entry(donationBoxSl).State = EntityState.Modified;
            db.DonationBox.AddOrUpdate(donationBoxSl);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}