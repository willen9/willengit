using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;
using MustardSeedMission.ViewModels;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WebGrease.Css.Extensions;

namespace MustardSeedMission.Controllers
{
    public class Basic01Controller : BaseController
    {
        // GET: Basic01
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A001";
            ViewData["DisplayData"] = new List<UsersViewModel>();
            IEnumerable<Groups> lstGroups = db.Groups.GroupBy(x => x.GroupId).ToList().Select(x => new Groups() { GroupId = x.FirstOrDefault().GroupId }); ;
            ViewData["AllGroup"] = lstGroups;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A001";
            ViewData["DisplayData"] = new List<UsersViewModel>();
            IEnumerable<Groups> lstGroups = db.Groups.GroupBy(x => x.GroupId).ToList().Select(x => new Groups() { GroupId = x.FirstOrDefault().GroupId }); ;
            ViewData["AllGroup"] = lstGroups;
            if (fc["action"] == "Basic01_Modal01_btnConfirm")
            {
                string uid = ViewBag.Basic01_Modal01_Uid = fc["Basic01_Modal01_txtUid"];
                string name = ViewBag.Basic01_Modal01_Name = fc["Basic01_Modal01_txtName"];
                string group = ViewBag.Basic01_Modal01_Group = fc["Basic01_Modal01_Group"];

                var data = db.Users.GroupJoin(db.SysPermission, x => x.UserId, y => y.UserId, (x, y) => new { x, y });
                if (!string.IsNullOrEmpty(uid))
                {
                    data = data.Where(o => o.x.UserId == uid);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.x.UserName.Contains(name));
                }
                if (!string.IsNullOrEmpty(group))
                {
                    data = data.Where(o => o.y.FirstOrDefault().GroupID == group);
                }
                ViewData["DisplayData"] =
                    data.Select(
                        o =>
                            new UsersViewModel()
                            {
                                UserId = o.x.UserId,
                                UserName = o.x.UserName,
                                Pwd = o.x.Pwd,
                                GroupID = o.y.FirstOrDefault().GroupID
                            });
            }
            if (fc["action"] == "export")
            {
                string uid = fc["txtUid"];
                string name = fc["txtName"];
                string group = fc["txtGroups"];
                var data = db.Users.GroupJoin(db.SysPermission, x => x.UserId, y => y.UserId, (x, y) => new { x, y });
                if (!string.IsNullOrEmpty(uid))
                {
                    data = data.Where(o => o.x.UserId == uid);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    data = data.Where(o => o.x.UserName == name);
                }
                if (!string.IsNullOrEmpty(group))
                {
                    data = data.Where(o => o.y.FirstOrDefault().GroupID == group);
                }
                IEnumerable<UsersViewModel> export = data.Select(
                      o =>
                          new UsersViewModel()
                          {
                              UserId = o.x.UserId,
                              UserName = o.x.UserName,
                              Pwd = o.x.Pwd,
                              GroupID = o.y.FirstOrDefault().GroupID
                          });
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("人員基本資料");
                sheet.SetColumnWidth(0, 20 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("員工帳號");
                row.CreateCell(1).SetCellValue("員工姓名");
                row.CreateCell(2).SetCellValue("員工密碼");
                row.CreateCell(3).SetCellValue("員工群組");

                int rowIndex;
                rowIndex = 1;
                foreach (UsersViewModel s in export)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.UserId);
                    row.CreateCell(1).SetCellValue(s.UserName);
                    row.CreateCell(2).SetCellValue(s.Pwd);
                    row.CreateCell(3).SetCellValue(s.GroupID);
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "人員基本資料_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostAdd(UsersViewModel usersViewModel)
        {
            string userId = usersViewModel.UserId;
            var user = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                return Json("員工帳號已存在", JsonRequestBehavior.AllowGet);
            }
            user = new Users();
            user.UserId = userId;
            user.UserName = usersViewModel.UserName;
            user.Pwd = usersViewModel.Pwd;
            user.Creator = Session["UID"].ToString();
            db.Users.Add(user);
            string creator = Session["UID"].ToString();

            if (db.SysPermission.Where(s => s.UserId == userId).Any())
            {
                db.SysPermission.Where(s => s.UserId == userId).ToList().ForEach(x => db.SysPermission.Remove(x));
            }
            if (!string.IsNullOrEmpty(usersViewModel.GroupID))
            {
                SysPermission sysPermissionNew = new SysPermission();
                sysPermissionNew.Creator = creator;
                sysPermissionNew.UserId = userId;
                sysPermissionNew.GroupID = usersViewModel.GroupID;
                db.Entry(sysPermissionNew).State = EntityState.Added;
                db.SysPermission.Add(sysPermissionNew);
            }
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(UsersViewModel usersViewModel)
        {
            try
            {
                string userId = usersViewModel.UserId;
                var user = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
                if (user == null)
                {
                    return Json("員工帳號不存在", JsonRequestBehavior.AllowGet);
                }
                user.UserName = usersViewModel.UserName;
                user.Pwd = usersViewModel.Pwd;
                db.Entry(user).State = EntityState.Modified;
                db.Users.AddOrUpdate(user);
                string creator = Session["UID"].ToString();
                if (db.SysPermission.Where(s => s.UserId == userId).Any())
                {
                    db.SysPermission.Where(s => s.UserId == userId).ToList().ForEach(x=>db.SysPermission.Remove(x));
                }
                if (!string.IsNullOrEmpty(usersViewModel.GroupID))
                {
                    SysPermission sysPermissionNew = new SysPermission();
                    sysPermissionNew.Creator = creator;
                    sysPermissionNew.UserId = userId;
                    sysPermissionNew.GroupID = usersViewModel.GroupID ?? "";
                    db.Entry(sysPermissionNew).State=EntityState.Added;
                    db.SysPermission.Add(sysPermissionNew);
                }
                db.SaveChanges();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult PostDelData(string ids)
        {
            string[] userId = ids.Split(',');
            Users user;
            foreach (string pk in userId)
            {
                if (pk == Session["UID"].ToString())
                {
                    return Json("不能刪除當前登錄帳號", JsonRequestBehavior.AllowGet);
                }
                db.SysPermission.Where(s => s.UserId == pk).ToList().ForEach(x => db.SysPermission.Remove(x));
                user = db.Users.Where(u => u.UserId == pk).FirstOrDefault();
                if (user != null)
                {
                    db.Users.Remove(user);
                }
            }
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}
