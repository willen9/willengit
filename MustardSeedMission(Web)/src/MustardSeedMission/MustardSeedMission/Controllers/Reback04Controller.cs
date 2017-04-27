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
    public class Reback04Controller : BaseController
    {
        // GET: Reback04
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A012";
            ViewData["AllDonationHead"] = db.DonationHead.Where(x=>x.DonationLists.All(y=>y.Rechecker==null||y.Rechecker=="")).Where(x=>x.DonationLists.Any(y=>y.Typer!=null&&y.Typer!=""));
            ViewData["DisplayData"] = new List<DonationListViewModel>();
            ViewData["AllUser"] = db.Users.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A012";
            ViewData["AllDonationHead"] = db.DonationHead.Where(x => x.DonationLists.All(y => y.Rechecker == null || y.Rechecker == "")).Where(x => x.DonationLists.Any(y => y.Typer != null && y.Typer != ""));
            ViewData["DisplayData"] = new List<DonationListViewModel>();
            ViewData["AllUser"] = db.Users.ToList();
            string docEntry = ViewBag.DocEntry = fc["slOrderNo"];
            if (fc["action"] == "btnSearch")
            {
                string sql = @"SELECT dl.DocEntry,
                                dl.Serialno,
                                dh.WorkDay,
                                dl.Operator,u.UserName AS OperatorName,
                                dl.StoreCode,s.Name AS StoreName,
                                dl.CheckTotal,
                                dl.CheckInvoice,
                                dl.Checkcurrency
                                FROM DonationHead AS dh
                                LEFT JOIN DonationList AS dl ON dh.DocEntry=dl.DocEntry
                                LEFT JOIN Users AS u ON dl.Operator=u.UserId
                                LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                                WHERE dl.DocEntry=@DocEntry
                                ORDER BY dl.Serialno";
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@DocEntry", fc["slOrderNo"]) };
                ViewData["DisplayData"] = db.Database.SqlQuery<DonationListViewModel>(sql, paras).ToList();
                string typer = db.DonationList.Where(x => x.DocEntry == docEntry).FirstOrDefault().Typer;
                ViewBag.Typer = db.Users.Where(x => x.UserId == typer).FirstOrDefault().UserName;
                DateTime? typeTime= db.DonationList.Where(x => x.DocEntry == docEntry).FirstOrDefault().TypeTime;
                ViewBag.TypeTime = typeTime == null ? "" : DateTime.Parse(typeTime.ToString()).ToString("yyyy/MM/dd");
                ViewBag.PrintCount = db.DonationList.Where(x => x.DocEntry == docEntry && x.printYN == true).Count();
            }
            if (fc["action"] == "btnConfirm")
            {
                string[] chk = fc["chk"].Split(',');
                string[] CheckTotal = fc["CheckTotal"].Split(',');
                string[] CheckInvoice = fc["CheckInvoice"].Split(',');
                string[] Checkcurrency = fc["Checkcurrency"].Split(',');
                DonationList donationList;
                int serialNo;
                int storeCode;
                for (int i = 0; i < chk.Length; i++)
                {
                    serialNo = int.Parse(chk[i].Split('-')[0]);
                    storeCode = int.Parse(chk[i].Split('-')[1]);
                    donationList =
                        db.DonationList.Where(
                            x => x.DocEntry == docEntry && x.Serialno == serialNo && x.StoreCode == storeCode)
                            .FirstOrDefault();
                    if (donationList != null)
                    {
                        donationList.CheckTotal = int.Parse(CheckTotal[i]);
                        donationList.CheckInvoice = int.Parse(CheckInvoice[i]);
                        donationList.Checkcurrency = int.Parse(Checkcurrency[i]);
                        donationList.Rechecker = Session["UID"].ToString();
                        donationList.Rechecktime = DateTime.Now;
                        donationList.checker = fc["slChecker"];
                        donationList.CheckTime = DateTime.Parse(fc["txtChecktime"]);
                    }
                    db.Entry(donationList).State = EntityState.Modified;
                    db.DonationList.AddOrUpdate(donationList);
                }
                db.SaveChanges();

                ViewData["AllDonationHead"] = db.DonationHead.Where(x => x.DonationLists.All(y => y.Rechecker == null || y.Rechecker == "")).Where(x => x.DonationLists.Any(y => y.Typer != null && y.Typer != ""));
                ViewBag.js = "<script>alert('確認成功')</script>";
            }
            if (fc["action"] == "export")
            {
                string sql = @"SELECT dl.DocEntry,
                                dl.Serialno,
                                dh.WorkDay,
                                dl.Operator,u.UserName AS OperatorName,
                                dl.StoreCode,s.Name AS StoreName,
                                dl.CheckTotal,
                                dl.CheckInvoice,
                                dl.Checkcurrency,
                                dl.Typer,u2.UserName AS TyperName,
                                dl.TypeTime,
                                dl.printYN 
                                FROM DonationHead AS dh
                                LEFT JOIN DonationList AS dl ON dh.DocEntry=dl.DocEntry
                                LEFT JOIN Users AS u ON dl.Operator=u.UserId
                                LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                                LEFT JOIN Users AS u2 ON dl.Typer=u2.UserId
                                WHERE dl.DocEntry=@DocEntry
                                ORDER BY dl.Serialno";
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@DocEntry", fc["slOrderNo"]) };
                List<DonationListViewModel>  lstExport= db.Database.SqlQuery<DonationListViewModel>(sql, paras).ToList();

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("核對作業");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("回收單號");
                row.CreateCell(1).SetCellValue("回收日期");
                row.CreateCell(2).SetCellValue("回收人員");
                row.CreateCell(3).SetCellValue("店家代碼");
                row.CreateCell(4).SetCellValue("店家名稱");
                row.CreateCell(5).SetCellValue("總金額");
                row.CreateCell(6).SetCellValue("發票");
                row.CreateCell(7).SetCellValue("外鈔");
                row.CreateCell(8).SetCellValue("登打人員");
                row.CreateCell(9).SetCellValue("登打日期");
                row.CreateCell(10).SetCellValue("是否列印回收袋標籤");

                int rowIndex;
                rowIndex = 1;
                foreach (DonationListViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.DocEntry);
                    row.CreateCell(1).SetCellValue(s.WorkDay==null?"":DateTime.Parse(s.WorkDay.ToString()).ToString("yyyy/MM/dd"));
                    row.CreateCell(2).SetCellValue(s.OperatorName);
                    row.CreateCell(3).SetCellValue(s.StoreCode);
                    row.CreateCell(4).SetCellValue(s.StoreName);
                    row.CreateCell(5).SetCellValue(s.CheckTotal.ToString());
                    row.CreateCell(6).SetCellValue(s.CheckInvoice.ToString());
                    row.CreateCell(7).SetCellValue(s.Checkcurrency.ToString());
                    row.CreateCell(8).SetCellValue(s.TyperName);
                    row.CreateCell(9).SetCellValue(s.TypeTime==null?"":DateTime.Parse(s.TypeTime.ToString()).ToString("yyyy/MM/dd"));
                    row.CreateCell(10).SetCellValue(s.printYN==true?"Y":"N");
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "核對作業_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }
    }
}