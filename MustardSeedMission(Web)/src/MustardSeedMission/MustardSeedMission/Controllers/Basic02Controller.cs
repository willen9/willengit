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
    public class Basic02Controller : BaseController
    {
        // GET: Basic02
        public ActionResult Index()
        {
            ViewBag.WorkValue = "A002";
            ViewData["DisplayData"] = new List<GroupsViewModel>();
            IEnumerable<Groups> lstGroups = db.Groups.GroupBy(x => x.GroupId).ToList().Select(x => new Groups() { GroupId = x.FirstOrDefault().GroupId }); ;
            ViewData["AllGroup"] = lstGroups;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.WorkValue = "A002";
            ViewData["DisplayData"] = new List<GroupsViewModel>();
            IEnumerable<Groups> lstGroups = db.Groups.GroupBy(x => x.GroupId).ToList().Select(x => new Groups() { GroupId = x.FirstOrDefault().GroupId }); ;
            ViewData["AllGroup"] = lstGroups;
            if (fc["action"] == "delete")
            {
                string groupId = ViewBag.Group = ViewBag.GroupShow = fc["txtGroup"];
                string functionIds = fc["chk"];
                Groups groups;
                foreach (string s in functionIds.Split(','))
                {
                    groups = db.Groups.Where(x => x.GroupId == groupId && x.SysFunctionsID == s).FirstOrDefault();
                    if (groups != null)
                    {
                        groups.AD = groups.De = groups.Up = groups.Query = groups.Ex = groups.Im = false;
                        db.Entry(groups).State = EntityState.Modified;
                        db.Groups.AddOrUpdate();
                    }
                }
                db.SaveChanges();
                string sql = @"SELECT g.*,sf.SysFunctionsName
                            FROM Groups AS g
                            RIGHT JOIN SysFunctions AS sf ON g.SysFunctionsID = sf.SysFunctionsID
                            WHERE g.GroupId = '" + groupId + "'";
                List<GroupsViewModel> lst = db.Database.SqlQuery<GroupsViewModel>(sql).ToList();
                ViewData["DisplayData"] = lst;
            }
            if (fc["action"] == "export")
            {
                string groupId = fc["txtGroup"];
                string sql = @"SELECT  g.GroupId,
                                g.SysFunctionsID,
                                CONVERT(BIT,ISNULL(g.AD,0)) AS AD,
                                CONVERT(BIT,ISNULL(g.De,0)) AS De,
                                CONVERT(BIT,ISNULL(g.Up,0)) AS Up,
                                CONVERT(BIT,ISNULL(g.[Query],0)) AS [Query],
                                CONVERT(BIT,ISNULL(g.Ex,0)) AS Ex,
                                CONVERT(BIT,ISNULL(g.Im,0)) AS Im,
                                sf.SysFunctionsName
                                FROM Groups AS g
                                RIGHT JOIN SysFunctions AS sf ON g.SysFunctionsID = sf.SysFunctionsID
                                WHERE g.GroupId = '" + groupId + "'";
                List<GroupsViewModel> lstExport = db.Database.SqlQuery<GroupsViewModel>(sql).ToList();

                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("群組權限設定");
                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 20 * 256);
                sheet.SetColumnWidth(2, 10 * 256);
                sheet.SetColumnWidth(3, 10 * 256);
                sheet.SetColumnWidth(4, 10 * 256);
                sheet.SetColumnWidth(5, 10 * 256);

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("群組名稱：");
                row.CreateCell(1).SetCellValue(groupId);
                row = sheet.CreateRow(1);
                row.CreateCell(0).SetCellValue("程式代號");
                row.CreateCell(1).SetCellValue("作業別");
                row.CreateCell(2).SetCellValue("查詢");
                row.CreateCell(3).SetCellValue("新增");
                row.CreateCell(4).SetCellValue("修改");
                row.CreateCell(5).SetCellValue("刪除");

                int rowIndex;
                rowIndex = 2;
                foreach (GroupsViewModel s in lstExport)
                {
                    row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(s.SysFunctionsID);
                    row.CreateCell(1).SetCellValue(s.SysFunctionsName);
                    row.CreateCell(2).SetCellValue(s.Query == true ? "V" : "");
                    row.CreateCell(3).SetCellValue(s.AD == true ? "V" : "");
                    row.CreateCell(4).SetCellValue(s.Up == true ? "V" : "");
                    row.CreateCell(5).SetCellValue(s.De == true ? "V" : "");
                }
                string path = Server.MapPath("~/Exports/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = "群組權限設定" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                using (FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            return View();
        }

        [HttpPost]
        public JsonResult PostGetData(string groupId)
        {
            string sql = @"SELECT g.*,sf.SysFunctionsName
                            FROM Groups AS g
                            RIGHT JOIN SysFunctions AS sf ON g.SysFunctionsID = sf.SysFunctionsID
                            WHERE g.GroupId = '" + groupId + "'";
            List<GroupsViewModel> lst = db.Database.SqlQuery<GroupsViewModel>(sql).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PostGetAllFunction()
        {
            string sql = @"SELECT sf.SysFunctionsID AS SysFunctionsID,
                            sf.SysFunctionsName AS SysFunctionsName,
                            NULL AS AD,
                            NULL AS De,
                            NULL AS Up,
                            NULL AS QUERY,
                            NULL AS Ex,
                            NULL AS Im
                            FROM  SysFunctions AS sf   ";
            List<GroupsViewModel> lst = db.Database.SqlQuery<GroupsViewModel>(sql).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostAdd(string GroupId, List<Groups> Modal)
        {
            var group = db.Groups.Where(x => x.GroupId == GroupId).FirstOrDefault();
            if (group != null)
            {
                return Json("群組名稱已存在", JsonRequestBehavior.AllowGet);
            }
            Groups groupsNew;
            foreach (Groups g in Modal)
            {
                groupsNew = new Groups();
                groupsNew.GroupId = GroupId;
                groupsNew.SysFunctionsID = g.SysFunctionsID;
                groupsNew.AD = g.AD;
                groupsNew.De = g.De;
                groupsNew.Query = g.Query;
                groupsNew.Up = g.Up;
                groupsNew.Ex = true;
                groupsNew.Im = false;
                db.Groups.AddOrUpdate(groupsNew);
            }
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostUpdate(string GroupId, List<Groups> Modal)
        {
            var group = db.Groups.Where(x => x.GroupId == GroupId).FirstOrDefault();
            if (group == null)
            {
                return Json("群組名稱不存在", JsonRequestBehavior.AllowGet);
            }
            Groups groupsNew;
            foreach (Groups g in Modal)
            {
                groupsNew =db.Groups.Where(x=>x.GroupId==GroupId&&x.SysFunctionsID==g.SysFunctionsID).FirstOrDefault();
                groupsNew.AD = g.AD;
                groupsNew.De = g.De;
                groupsNew.Query = g.Query;
                groupsNew.Up = g.Up;
                groupsNew.Ex = true;
                groupsNew.Im = false;
                db.Entry(groupsNew).State=EntityState.Modified;
                db.Groups.AddOrUpdate(groupsNew);
            }
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}