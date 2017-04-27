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
    public class Reback03Controller : BaseController
    {
        // GET: Reback03
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A011";
            ViewData["AllDonationHead"] = db.DonationHead.Where(x => x.DonationLists.All(y => y.Typer ==null||y.Typer=="")).OrderBy(x => x.DocEntry);
            ViewData["DisplayData"]=new List<DonationListViewModel>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A011";
            string DocEntry=ViewBag.DocEntry = fc["slOrderNo"];
            ViewData["DisplayData"] = new List<DonationListViewModel>();
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
                SqlParameter[] paras = new SqlParameter[] {new SqlParameter("@DocEntry", fc["slOrderNo"])};
                ViewData["DisplayData"] = db.Database.SqlQuery<DonationListViewModel>(sql, paras).ToList();
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
                            x => x.DocEntry == DocEntry && x.Serialno == serialNo && x.StoreCode == storeCode)
                            .FirstOrDefault();
                    if (donationList != null)
                    {
                        donationList.CheckTotal = int.Parse(CheckTotal[i]);
                        donationList.CheckInvoice = int.Parse(CheckInvoice[i]);
                        donationList.Checkcurrency = int.Parse(Checkcurrency[i]);
                        donationList.Typer = Session["UID"].ToString();
                        donationList.TypeTime=DateTime.Now;
                    }
                    db.Entry(donationList).State = EntityState.Modified;
                    db.DonationList.AddOrUpdate(donationList);
                }
                db.SaveChanges();

                //string sql = @"SELECT dl.DocEntry,
                //                dl.Serialno,
                //                dh.WorkDay,
                //                dl.Operator,u.UserName  AS OperatorName,
                //                dl.StoreCode,s.Name AS StoreName,
                //                dl.CheckTotal,
                //                dl.CheckInvoice,
                //                dl.Checkcurrency
                //                FROM DonationHead AS dh
                //                LEFT JOIN DonationList AS dl ON dh.DocEntry=dl.DocEntry
                //                LEFT JOIN Users AS u ON dl.Operator=u.UserId
                //                LEFT JOIN Store AS s ON dl.StoreCode=s.Code
                //                WHERE dl.DocEntry=@DocEntry
                //                ORDER BY dl.Serialno";
                //SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@DocEntry", fc["slOrderNo"]) };
                //ViewData["DisplayData"] = db.Database.SqlQuery<DonationListViewModel>(sql, paras).ToList();
                ViewBag.js = "<script>alert('確定成功')</script>";
            }
            ViewData["AllDonationHead"] = db.DonationHead.Where(x => x.DonationLists.All(y => y.Typer == null || y.Typer == "")).OrderBy(x => x.DocEntry);
            return View();
        }
    }
}