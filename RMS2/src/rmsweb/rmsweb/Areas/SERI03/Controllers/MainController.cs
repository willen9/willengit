using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace SERI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.Company = "";
            questionList_H.UserGroup = "";
            questionList_H.Creator = "";
            questionList_H.QuestionNo = Request.Form["QuestionNo"];
            questionList_H.QuestionTopic = Request.Form["QuestionTopic"];
            questionList_H.QuestionDetail = Request.Form["QuestionDetail"];
            questionList_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strSolution = Request.Form["strCSolution"];
            string strRemark = Request.Form["strCRemark"];

            QuestionList_HLogic logic = new QuestionList_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddQuestionList_H(questionList_H, strItemId,
             strSolution, strRemark, out infomsg))
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
            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.Company = "";
            questionList_H.UserGroup = "";
            questionList_H.Modifier = "";
            questionList_H.QuestionNo = Request.Form["QuestionNo"];
            questionList_H.QuestionTopic = Request.Form["QuestionTopic"];
            questionList_H.QuestionDetail = Request.Form["QuestionDetail"];
            questionList_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strSolution = Request.Form["strCSolution"];
            string strRemark = Request.Form["strCRemark"];

            QuestionList_HLogic logic = new QuestionList_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateQuestionList_H(questionList_H, strItemId,
             strSolution, strRemark, out infomsg))
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
        public ActionResult Delete(string QuestionNo)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();

            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;

            string msg = "";
            if (!logic.DelQuestionList_H(questionList_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string QuestionNo,string cur)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();

            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;

            questionList_H = logic.GetQuestionList_HInfo(questionList_H);

            ViewBag.QuestionNo = questionList_H.QuestionNo;
            ViewBag.QuestionTopic = questionList_H.QuestionTopic;
            ViewBag.QuestionDetail = questionList_H.QuestionDetail;
            ViewBag.Remark = questionList_H.Remark;

            QuestionList_DLogic Dlogic = new QuestionList_DLogic();
            QuestionList_D questionList_D = new QuestionList_D();
            questionList_D.QuestionNo = QuestionNo;
            ViewData["QuestionList_D"] = Dlogic.SearchQuestionList_D(questionList_D);

            ViewBag.cur = cur;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string QuestionNo)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();

            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;

            questionList_H = logic.GetQuestionList_HInfo(questionList_H);

            ViewBag.QuestionNo = questionList_H.QuestionNo;
            ViewBag.QuestionTopic = questionList_H.QuestionTopic;
            ViewBag.QuestionDetail = questionList_H.QuestionDetail;
            ViewBag.Remark = questionList_H.Remark;

            QuestionList_DLogic Dlogic = new QuestionList_DLogic();
            QuestionList_D questionList_D = new QuestionList_D();
            questionList_D.QuestionNo = QuestionNo;
            ViewData["QuestionList_D"] = Dlogic.SearchQuestionList_D(questionList_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
                string path = Server.MapPath(@"~\ImportFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = hfc[0].FileName;
                //判断文件名字是否包含路径名，如果有则提取文件名
                if (name.LastIndexOf("\\") > -1)
                {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                }
                hfc[0].SaveAs(path + "\\" + name);

                if (!logic.ImportFile(path + "\\" + name))
                {
                    return Json("NO-匯入失敗", "text/x-json");
                }

                return Json("YES-匯入成功", "text/x-json");
            }
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
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
                ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("問題代號,問題描述,詳細內容,備註");
                //    List<QuestionList_H> objQuestionList_H = ViewData["QuestionList_H"] as List<QuestionList_H>;
                //    foreach (var obj in objQuestionList_H)
                //    {
                //        sw.WriteLine(obj.QuestionNo + "," + obj.QuestionTopic + ","
                //            + obj.QuestionDetail + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("QuestionList");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("問題代號");
                    row.CreateCell(1).SetCellValue("問題描述");
                    row.CreateCell(2).SetCellValue("詳細內容");
                    row.CreateCell(3).SetCellValue("備註");


                    int index = 1;

                    List<QuestionList_H> objQuestionList_H = ViewData["QuestionList_H"] as List<QuestionList_H>;

                    foreach (var obj in objQuestionList_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.QuestionNo.ToString());
                        row.CreateCell(1).SetCellValue(obj.QuestionTopic.ToString());
                        row.CreateCell(2).SetCellValue(obj.QuestionDetail.ToString());
                        row.CreateCell(3).SetCellValue(obj.Remark.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "QuestionList.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "QuestionList.xls");
            }
            else
            {
                if (Request.Form["strQuestionNo"].Trim() != "")
                {
                    col += ",QuestionNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strQuestionNo"];
                }
                ViewData["QuestionList_H"] = logic.SearchQuestionList_H(col, condition, value);
                ViewBag.strQuestionNo = Request.Form["strQuestionNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<QuestionList_D> objQuestionList_D = new List<QuestionList_D>();
            ViewData["QuestionList_D"] = objQuestionList_D;
            return View("CUR");
        }

        [HttpPost]
        public JsonResult IsQuestionNo(string QuestionNo)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;
            return Json(logic.IsQuestionList_H(questionList_H), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show(string QuestionNo)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();

            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;

            questionList_H = logic.GetQuestionList_HInfo(questionList_H);

            ViewBag.QuestionNo = questionList_H.QuestionNo;
            ViewBag.QuestionTopic = questionList_H.QuestionTopic;
            ViewBag.QuestionDetail = questionList_H.QuestionDetail;
            ViewBag.Remark = questionList_H.Remark;

            QuestionList_DLogic Dlogic = new QuestionList_DLogic();
            QuestionList_D questionList_D = new QuestionList_D();
            questionList_D.QuestionNo = QuestionNo;
            ViewData["QuestionList_D"] = Dlogic.SearchQuestionList_D(questionList_D);
            return View("CUR");
        }

        public JsonResult SearchQuestionList(string QuestionNo, string QuestionTopic, int Page)
        {
            QuestionList_HLogic logic = new QuestionList_HLogic();
            QuestionList_H questionList_H = new QuestionList_H();
            questionList_H.QuestionNo = QuestionNo;
            questionList_H.QuestionTopic = QuestionTopic;
            List<QuestionList_H> lst = logic.SearchQuestionList(questionList_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}