using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;

namespace CMSI11.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Position_HLogic logic = new Position_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Position_H"] = logic.SearchPosition_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Position_HLogic logic = new Position_HLogic();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Position_H"] = logic.SearchPosition_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["Position_H"] = logic.SearchPosition_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("職務代號,職務名稱,職務分類,備註");
                //    List<Position_H> objPosition_H = ViewData["Position_H"] as List<Position_H>;
                //    foreach (var obj in objPosition_H)
                //    {
                //        sw.WriteLine(obj.PositionNo + "," + obj.Position + "," + obj.PositionCategory + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Position");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("職務代號");
                    row.CreateCell(1).SetCellValue("職務名稱");
                    row.CreateCell(2).SetCellValue("職務分類");
                    row.CreateCell(3).SetCellValue("備註");


                    int index = 1;

                    List<Position_H> objPosition_H = ViewData["Position_H"] as List<Position_H>;
                    foreach (var obj in objPosition_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.PositionNo.ToString());
                        row.CreateCell(1).SetCellValue(obj.Position.ToString());
                        row.CreateCell(2).SetCellValue(obj.PositionCategory.ToString());
                        row.CreateCell(3).SetCellValue(obj.Remark.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Position.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Position.xls");

            }
            else
            {
                if (Request.Form["positionno"].Trim() != "")
                {
                    col += ",PositionNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["positionno"];
                }

                ViewData["Position_H"] = logic.SearchPosition_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.positionno = Request.Form["positionno"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<Position_D> objPosition_D = new List<Position_D>();
            ViewData["Position_D"] = objPosition_D;
            return View("CUR");
        }

        [HttpPost]
        public JsonResult IsPositionNo(string PositionNo)
        {
            Position_HLogic logic = new Position_HLogic();
            Position_H position_H = new Position_H();
            position_H.PositionNo = PositionNo;
            return Json(logic.IsPositionNo(position_H), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add()
        {
            Position_H position_H = new Position_H();
            position_H.Company = "";
            position_H.UserGroup = "";
            position_H.Creator = "";
            position_H.PositionNo = Request.Form["PositionNo"];
            position_H.Position = Request.Form["Position"];
            position_H.PositionCategory = Request.Form["PositionCategory"];
            position_H.Remark = Request.Form["Remark"];

            string strEmployeeId = Request.Form["strCEmployeeId"];
            string strRemark = Request.Form["strCRemark"];

            Position_HLogic logic = new Position_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.AddPosition_H(position_H,
             strEmployeeId,
             strRemark, out infomsg))
            {
                msg = "NO-新增失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Exit()
        {
            Position_H position_H = new Position_H();
            position_H.Company = "";
            position_H.UserGroup = "";
            position_H.Modifier = "";
            position_H.PositionNo = Request.Form["PositionNo"];
            position_H.Position = Request.Form["Position"];
            position_H.PositionCategory = Request.Form["PositionCategory"];
            position_H.Remark = Request.Form["Remark"];

            string strEmployeeId = Request.Form["strCEmployeeId"];
            string strRemark = Request.Form["strCRemark"];

            Position_HLogic logic = new Position_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.UpdatePosition_H(position_H,
             strEmployeeId,
             strRemark, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string PositionNo)
        {
            Position_HLogic logic = new Position_HLogic();

            Position_H position_H = new Position_H();
            position_H.PositionNo = PositionNo;

            string msg = "";
            if (!logic.DelPosition_H(position_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string PositionNo)
        {
            Position_HLogic logic = new Position_HLogic();

            Position_H position_H = new Position_H();
            position_H.PositionNo = PositionNo;

            position_H = logic.GetPosition_HInfo(position_H);

            ViewBag.PositionNo = position_H.PositionNo;
            ViewBag.Position = position_H.Position;
            ViewBag.PositionCategory = position_H.PositionCategory;
            ViewBag.Remark = position_H.Remark;

            Position_DLogic Dlogic = new Position_DLogic();
            Position_D position_D = new Position_D();
            position_D.PositionNo = PositionNo;
            ViewData["Position_D"] = Dlogic.SearchPosition_D(position_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string PositionNo)
        {
            Position_HLogic logic = new Position_HLogic();

            Position_H position_H = new Position_H();
            position_H.PositionNo = PositionNo;

            position_H = logic.GetPosition_HInfo(position_H);

            ViewBag.PositionNo = position_H.PositionNo;
            ViewBag.Position = position_H.Position;
            ViewBag.PositionCategory = position_H.PositionCategory;
            ViewBag.Remark = position_H.Remark;

            Position_DLogic Dlogic = new Position_DLogic();
            Position_D position_D = new Position_D();
            position_D.PositionNo = PositionNo;
            ViewData["Position_D"] = Dlogic.SearchPosition_D(position_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Copy(string PositionNo)
        {
            Position_HLogic logic = new Position_HLogic();

            Position_H position_H = new Position_H();
            position_H.PositionNo = PositionNo;

            position_H = logic.GetPosition_HInfo(position_H);

            //ViewBag.PositionNo = position_H.PositionNo;
            ViewBag.Position = position_H.Position;
            ViewBag.PositionCategory = position_H.PositionCategory;
            ViewBag.Remark = position_H.Remark;

            Position_DLogic Dlogic = new Position_DLogic();
            Position_D position_D = new Position_D();
            position_D.PositionNo = PositionNo;
            ViewData["Position_D"] = Dlogic.SearchPosition_D(position_D);

            return View("CUR");
        }

        
    }
}