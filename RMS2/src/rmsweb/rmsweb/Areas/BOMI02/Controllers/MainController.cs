using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace BOMI02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Substitutes_H"] = logic.SearchSubstitutes_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.Company = "";
            substitutes_H.UserGroup = "";
            substitutes_H.Creator = "";
            substitutes_H.ComponentNo = Request.Form["ComponentNo"];
            substitutes_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            substitutes_H.SubstitutesType = Request.Form["SubstitutesType"].Substring(0,1);
            substitutes_H.Remark = Request.Form["Remark"];

            string strSubstitutesNo = Request.Form["strCSubstitutesNo"];
            string strQTY = Request.Form["strCQTY"];
            string strPriority = Request.Form["strCPriority"];
            string strRemark = Request.Form["strCRemark"];

            Substitutes_HLogic logic = new Substitutes_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddSubstitutes_H(substitutes_H, strSubstitutesNo,
             strQTY, strPriority, strRemark, out infomsg))
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
            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.Company = "";
            substitutes_H.UserGroup = "";
            substitutes_H.Modifier = "";
            substitutes_H.ComponentNo = Request.Form["ComponentNo"];
            substitutes_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            substitutes_H.SubstitutesType = Request.Form["SubstitutesType"].Substring(0, 1);
            substitutes_H.Remark = Request.Form["Remark"];

            string strSubstitutesNo = Request.Form["strCSubstitutesNo"];
            string strQTY = Request.Form["strCQTY"];
            string strPriority = Request.Form["strCPriority"];
            string strRemark = Request.Form["strCRemark"];

            Substitutes_HLogic logic = new Substitutes_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateSubstitutes_H(substitutes_H, strSubstitutesNo,
             strQTY, strPriority, strRemark, out infomsg))
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
        public ActionResult Delete(string ComponentNo, string MajorComponentNo, string SubstitutesType)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();

            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = ComponentNo;
            substitutes_H.MajorComponentNo = MajorComponentNo;
            substitutes_H.SubstitutesType = SubstitutesType;

            string msg = "";
            if (!logic.DelSubstitutes_H(substitutes_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ComponentNo, string MajorComponentNo, string SubstitutesType)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();

            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = ComponentNo;
            substitutes_H.MajorComponentNo = MajorComponentNo;
            substitutes_H.SubstitutesType = SubstitutesType;

            substitutes_H = logic.GetSubstitutes_HInfo(substitutes_H);

            ViewBag.ComponentNo = substitutes_H.ComponentNo;
            ViewBag.MajorComponentNo = substitutes_H.MajorComponentNo;
            ViewBag.SubstitutesType = substitutes_H.SubstitutesType;
            ViewBag.Remark = substitutes_H.Remark;
            ViewBag.ProductNameComponentNo = substitutes_H.ProductNameComponentNo;
            ViewBag.SpecificationComponentNo = substitutes_H.ProductNameComponentNo;
            ViewBag.UnitComponentNo = substitutes_H.UnitComponentNo;
            ViewBag.ProductNameMajorComponentNo = substitutes_H.ProductNameMajorComponentNo;
            ViewBag.SpecificationMajorComponentNo = substitutes_H.SpecificationMajorComponentNo;
            ViewBag.UnitMajorComponentNo = substitutes_H.UnitMajorComponentNo;

            Substitutes_DLogic Dlogic = new Substitutes_DLogic();
            Substitutes_D substitutes_D = new Substitutes_D();
            substitutes_D.ComponentNo = ComponentNo;
            substitutes_D.MajorComponentNo = MajorComponentNo;
            substitutes_D.SubstitutesType = SubstitutesType;
            ViewData["Substitutes_D"] = Dlogic.SearchSubstitutes_D(substitutes_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ComponentNo, string MajorComponentNo, string SubstitutesType)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();

            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = ComponentNo;
            substitutes_H.MajorComponentNo = MajorComponentNo;
            substitutes_H.SubstitutesType = SubstitutesType;

            substitutes_H = logic.GetSubstitutes_HInfo(substitutes_H);

            ViewBag.ComponentNo = substitutes_H.ComponentNo;
            ViewBag.MajorComponentNo = substitutes_H.MajorComponentNo;
            ViewBag.SubstitutesType = substitutes_H.SubstitutesType;
            ViewBag.Remark = substitutes_H.Remark;
            ViewBag.ProductNameComponentNo = substitutes_H.ProductNameComponentNo;
            ViewBag.SpecificationComponentNo = substitutes_H.ProductNameComponentNo;
            ViewBag.UnitComponentNo = substitutes_H.UnitComponentNo;
            ViewBag.ProductNameMajorComponentNo = substitutes_H.ProductNameMajorComponentNo;
            ViewBag.SpecificationMajorComponentNo = substitutes_H.SpecificationMajorComponentNo;
            ViewBag.UnitMajorComponentNo = substitutes_H.UnitMajorComponentNo;
            ViewBag.Confirmed = substitutes_H.Confirmed;

            Substitutes_DLogic Dlogic = new Substitutes_DLogic();
            Substitutes_D substitutes_D = new Substitutes_D();
            substitutes_D.ComponentNo = ComponentNo;
            substitutes_D.MajorComponentNo = MajorComponentNo;
            substitutes_D.SubstitutesType = SubstitutesType;
            ViewData["Substitutes_D"] = Dlogic.SearchSubstitutes_D(substitutes_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();
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
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Substitutes_H"] = logic.SearchSubstitutes_H(col, condition, value);
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
                ViewData["Substitutes_H"] = logic.SearchSubstitutes_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                #region
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("元件品號,元件品名,規格,單位,主件品號,主件品名,取替代件,備註");
                //    List<Substitutes_H> objSubstitutes_H = ViewData["Substitutes_H"] as List<Substitutes_H>;
                //    foreach (var obj in objSubstitutes_H)
                //    {
                //        sw.WriteLine(obj.ComponentNo + "," + obj.ProductNameComponentNo + ","
                //            + obj.SpecificationComponentNo + "," + obj.UnitComponentNo + "," + obj.MajorComponentNo + ","
                //            + obj.ProductNameMajorComponentNo+","+obj.SubstitutesType + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Substitutes.csv");
                #endregion
                MemoryStream ms = null;
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //XSSFWorkbook workbook = new XSSFWorkbook();
                    HSSFWorkbook book = new HSSFWorkbook();
                    ISheet sheet = book.CreateSheet();
                    HSSFCellStyle lo_Style = (HSSFCellStyle)book.CreateCellStyle();
                    lo_Style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                    ms = new MemoryStream();
                    IRow row = null;
                    row = sheet.CreateRow(0);
                    //元件品號,元件品名,規格,單位,主件品號,主件品名,取替代件,備註
                    row.CreateCell(0).SetCellValue("元件品號");
                    row.CreateCell(1).SetCellValue("元件品名");
                    row.CreateCell(2).SetCellValue("規格");
                    row.CreateCell(3).SetCellValue("單位");
                    row.CreateCell(4).SetCellValue("主件品號");
                    row.CreateCell(5).SetCellValue("主件品名");
                    row.CreateCell(6).SetCellValue("取替代件");
                    row.CreateCell(7).SetCellValue("備註");
                    List<Substitutes_H> objSubstitutes_H = ViewData["Substitutes_H"] as List<Substitutes_H>;
                    int i = 0;
                    foreach (var obj in objSubstitutes_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.ComponentNo);
                        row.CreateCell(1).SetCellValue(obj.ProductNameComponentNo);
                        row.CreateCell(2).SetCellValue(obj.SpecificationComponentNo);
                        row.CreateCell(3).SetCellValue(obj.UnitComponentNo);
                        row.CreateCell(4).SetCellValue(obj.MajorComponentNo);
                        row.CreateCell(5).SetCellValue(obj.ProductNameMajorComponentNo);
                        row.CreateCell(6).SetCellValue(obj.SubstitutesType);
                        row.CreateCell(7).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Substitutes.xls");
            }
            else
            {
                if (Request.Form["strComponentNo"].Trim() != "")
                {
                    col += ",h.ComponentNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strComponentNo"];
                }
                ViewData["Substitutes_H"] = logic.SearchSubstitutes_H(col, condition, value);
                ViewBag.strComponentNo = Request.Form["strComponentNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<Substitutes_D> objSubstitutes_D = new List<Substitutes_D>();
            ViewData["Substitutes_D"] = objSubstitutes_D;
            return View("CUR");
        }

        //通過元件品號查找
        public JsonResult SearchBOMDInfo(string ComponentNo)
        {
            BOM_DLogic logic = new BOM_DLogic();
            BOM_D bOM_D = logic.SearchBOM_DInfo(ComponentNo);
            return Json(bOM_D, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchBOMD(string ComponentNo, string ProductName, int Page)
        {
            BOM_DLogic logic = new BOM_DLogic();
            BOM_D bOM_D = new BOM_D();
            bOM_D.ComponentNo = ComponentNo;
            bOM_D.ProductName = ProductName;
            List<BOM_D> lst = logic.SearchBOM_D(bOM_D, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchBOMHInfo(string MajorComponentNo)
        {
            BOM_HLogic logic = new BOM_HLogic();
            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;
            bOM_H = logic.GetBOM_HInfo(bOM_H);
            return Json(bOM_H, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchBOMH(string MajorComponentNo, string MajorComponentName, int Page)
        {
            BOM_HLogic logic = new BOM_HLogic();
            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;
            bOM_H.MajorComponentName = MajorComponentName;
            List<BOM_H> lst = logic.SearchBOMH(bOM_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsSubstitutes_H(string ComponentNo, string MajorComponentNo,string SubstitutesType)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();
            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = ComponentNo;
            substitutes_H.MajorComponentNo = MajorComponentNo;
            substitutes_H.SubstitutesType = SubstitutesType;
            return Json(logic.IsSubstitutes_H(substitutes_H), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsBOM_D(string ComponentNo, string MajorComponentNo)
        {
            BOM_DLogic logic = new BOM_DLogic();
            BOM_D bOM_D = new BOM_D();
            bOM_D.ComponentNo = ComponentNo;
            bOM_D.MajorComponentNo = MajorComponentNo;
            return Json(logic.IsBOM_D(bOM_D), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirmed(string ComponentNo, string MajorComponentNo,string SubstitutesType, string Confirmed)
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();

            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = ComponentNo;
            substitutes_H.MajorComponentNo = MajorComponentNo;
            substitutes_H.SubstitutesType = SubstitutesType;
            substitutes_H.Confirmed = Confirmed;
            substitutes_H.Confirmor = "";

            string msg = "";
            if (!logic.ConfirmedSubstitutes_H(substitutes_H, out msg))
            {
                ViewBag.Msg = "操作失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public JsonResult Sure()
        {
            Substitutes_HLogic logic = new Substitutes_HLogic();

            Substitutes_H substitutes_H = new Substitutes_H();
            substitutes_H.ComponentNo = Request.Form["ComponentNo"];
            substitutes_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            substitutes_H.SubstitutesType = Request.Form["intSubstitutesType"];
            substitutes_H.Confirmed = "Y";
            substitutes_H.Confirmor = "";
            substitutes_H.ConfirmDate = Request.Form["sureConfirmDate"].Replace("/", "");

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedSubstitutes_H(substitutes_H, out infomsg))
            {
                msg = "NO-操作失敗！" + infomsg;
            }
            else
            {
                msg = "OK-成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
    }
}