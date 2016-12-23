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

namespace BOMI01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            BOM_HLogic logic = new BOM_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["BOM_H"] = logic.SearchBOM_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            BOM_H bOM_H = new BOM_H();
            bOM_H.Company = "";
            bOM_H.UserGroup = "";
            bOM_H.Creator = "";
            bOM_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            bOM_H.MajorComponentName = Request.Form["MajorComponentName"];
            bOM_H.Specification = Request.Form["Specification"];
            bOM_H.Unit = Request.Form["Unit"];
            bOM_H.StandardQTY = double.Parse(Request.Form["StandardQTY"]==""?"0": Request.Form["StandardQTY"]);
            bOM_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strComponentNo = Request.Form["strCComponentNo"];
            string strQTY = Request.Form["strCQTY"];
            string strRemark = Request.Form["strCRemark"];

            BOM_HLogic logic = new BOM_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddBOM_H(bOM_H, strItemId,
             strComponentNo, strQTY, strRemark,out infomsg))
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
            BOM_H bOM_H = new BOM_H();
            bOM_H.Company = "";
            bOM_H.UserGroup = "";
            bOM_H.Creator = "";
            bOM_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            bOM_H.MajorComponentName = Request.Form["MajorComponentName"];
            bOM_H.Specification = Request.Form["Specification"];
            bOM_H.Unit = Request.Form["Unit"];
            bOM_H.StandardQTY = double.Parse(Request.Form["StandardQTY"]==""?"0": Request.Form["StandardQTY"]);
            bOM_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strComponentNo = Request.Form["strCComponentNo"];
            string strQTY = Request.Form["strCQTY"];
            string strRemark = Request.Form["strCRemark"];

            BOM_HLogic logic = new BOM_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateBOM_H(bOM_H, strItemId,
             strComponentNo, strQTY, strRemark, out infomsg))
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
        public ActionResult Delete(string MajorComponentNo)
        {
            BOM_HLogic logic = new BOM_HLogic();

            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;

            string msg = "";
            if (!logic.DelBOM_H(bOM_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string MajorComponentNo)
        {
            BOM_HLogic logic = new BOM_HLogic();

            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;

            bOM_H = logic.GetBOM_HInfo(bOM_H);

            ViewBag.MajorComponentNo = bOM_H.MajorComponentNo;
            ViewBag.MajorComponentName = bOM_H.MajorComponentName;
            ViewBag.Specification = bOM_H.Specification;
            ViewBag.Unit = bOM_H.Unit;
            ViewBag.StandardQTY = bOM_H.StandardQTY;
            ViewBag.CreateDate = bOM_H.CreateDate;
            ViewBag.ModiDate = bOM_H.ModiDate;
            ViewBag.Confirmor = bOM_H.Confirmor;
            ViewBag.ConfirmDate = bOM_H.ConfirmDate;
            ViewBag.Remark = bOM_H.Remark;
            ViewBag.Confirmed = bOM_H.Confirmed;

            BOM_DLogic Dlogic = new BOM_DLogic();
            BOM_D bOM_D = new BOM_D();
            bOM_D.MajorComponentNo = MajorComponentNo;
            ViewData["BOM_D"] = Dlogic.SearchBOM_D(bOM_D,0);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string MajorComponentNo)
        {
            BOM_HLogic logic = new BOM_HLogic();

            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;

            bOM_H = logic.GetBOM_HInfo(bOM_H);

            ViewBag.MajorComponentNo = bOM_H.MajorComponentNo;
            ViewBag.MajorComponentName = bOM_H.MajorComponentName;
            ViewBag.Specification = bOM_H.Specification;
            ViewBag.Unit = bOM_H.Unit;
            ViewBag.StandardQTY = bOM_H.StandardQTY;
            ViewBag.CreateDate = bOM_H.CreateDate;
            ViewBag.ModiDate = bOM_H.ModiDate;
            ViewBag.Confirmor = bOM_H.Confirmor;
            ViewBag.ConfirmDate = bOM_H.ConfirmDate;
            ViewBag.Remark = bOM_H.Remark;
            ViewBag.Confirmed = bOM_H.Confirmed;
            //ViewBag.ConfirmDate = DateTime.Now.ToString("yyyy/MM/dd");

            BOM_DLogic Dlogic = new BOM_DLogic();
            BOM_D bOM_D = new BOM_D();
            bOM_D.MajorComponentNo = MajorComponentNo;
            ViewData["BOM_D"] = Dlogic.SearchBOM_D(bOM_D,0);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Confirmed(string MajorComponentNo,string Confirmed)
        {
            BOM_HLogic logic = new BOM_HLogic();

            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;
            bOM_H.Confirmed = Confirmed;
            bOM_H.Confirmor = "";
            bOM_H.ConfirmDate = "";

            string msg = "";
            if (!logic.ConfirmedBOM_H(bOM_H, out msg))
            {
                ViewBag.Msg = "操作失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            BOM_HLogic logic = new BOM_HLogic();
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
                ViewData["BOM_H"] = logic.SearchBOM_H(col, condition, value);
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
                ViewData["BOM_H"] = logic.SearchBOM_H(col, condition, value);
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
                //    sw.WriteLine("主件品號,品名,規格,單位,建立日期,確認日期,備註");
                //    List<BOM_H> objBOM_H = ViewData["BOM_H"] as List<BOM_H>;
                //    foreach (var obj in objBOM_H)
                //    {
                //        sw.WriteLine(obj.MajorComponentNo + "," + obj.MajorComponentName + ","
                //            + obj.Specification + "," + obj.Unit + "," + obj.CreateDate + ","
                //            + obj.ConfirmDate + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "BOM.csv");
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
                    //主件品號,品名,規格,單位,建立日期,確認日期,備註
                    row.CreateCell(0).SetCellValue("主件品號");
                    row.CreateCell(1).SetCellValue("品名");
                    row.CreateCell(2).SetCellValue("規格");
                    row.CreateCell(3).SetCellValue("單位");
                    row.CreateCell(4).SetCellValue("建立日期");
                    row.CreateCell(5).SetCellValue("確認日期");
                    row.CreateCell(6).SetCellValue("備註");
                    List<BOM_H> objBOM_H = ViewData["BOM_H"] as List<BOM_H>;
                    int i = 0;
                    foreach (var obj in objBOM_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.MajorComponentNo);
                        row.CreateCell(1).SetCellValue(obj.MajorComponentName);
                        row.CreateCell(2).SetCellValue(obj.Specification);
                        row.CreateCell(3).SetCellValue(obj.Unit);
                        row.CreateCell(4).SetCellValue(obj.CreateDate);
                        row.CreateCell(5).SetCellValue(obj.ConfirmDate);
                        row.CreateCell(6).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "BOM.xls");
            }
            else
            {
                if (Request.Form["strMajorComponentNo"].Trim() != "")
                {
                    col += ",MajorComponentNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strMajorComponentNo"];
                }
                ViewData["BOM_H"] = logic.SearchBOM_H(col, condition, value);
                ViewBag.strMajorComponentNo = Request.Form["strMajorComponentNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<BOM_D> objBOM_D = new List<BOM_D>();
            ViewData["BOM_D"] = objBOM_D;
            ViewBag.StandardQTY = "1";
            ViewBag.CreateDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Sure()
        {
            BOM_HLogic logic = new BOM_HLogic();

            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = Request.Form["MajorComponentNo"];
            bOM_H.Confirmed = "Y";
            bOM_H.Confirmor = "";
            bOM_H.ConfirmDate = Request.Form["sureConfirmDate"].Replace("/","");

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedBOM_H(bOM_H, out infomsg))
            {
                msg = "NO-操作失敗！" + infomsg;
            }else
            {
                msg = "OK-成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
            
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MajorComponentNo(string MajorComponentNo)
        {
            BOM_HLogic logic = new BOM_HLogic();
            BOM_H bOM_H = new BOM_H();
            bOM_H.MajorComponentNo = MajorComponentNo;
            return Json(logic.IsBOM_H(bOM_H), JsonRequestBehavior.AllowGet);
        }
    }
}