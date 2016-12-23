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
using System.Web.Mvc;

namespace CMSI08.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            BrandLogic logic = new BrandLogic();
            Brand brand = new Brand();
            ViewData["Brand"] = logic.GetBrand(brand,"","","",0);
            return View();
        }

        [HttpPost]
        public JsonResult CURMID(string brandId)
        {
            BrandLogic logic = new BrandLogic();
            bool type = logic.IsModuleId(brandId);
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            BrandLogic logic = new BrandLogic();
            Brand brand = new Brand();
            if(Request.Form["action"]=="Advanced")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];
                ViewData["Brand"] = logic.SearchBrand(col, condition, value);
                //TempData["ProductCategory"] = ViewData["ProductCategory"];
            }
            else if (Request.Form["action"] == "btnExport")
            {
                //List<Brand> objBrand = TempData["Brand"] as List<Brand>;
                //ViewData["Brand"] = TempData["Brand"];

                ViewData["Brand"] = logic.GetBrand(brand,"","","",0);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("品牌代號,品牌名稱,備註");
                //    List<Brand> objBrand = ViewData["Brand"] as List<Brand>;
                //    foreach (var obj in objBrand)
                //    {
                //        sw.WriteLine(obj.BrandId + "," + obj.BrandName + "," );
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Brand");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("品牌代號");
                    row.CreateCell(1).SetCellValue("品牌名稱");
                    row.CreateCell(2).SetCellValue("備註");

                    int index = 1;

                    List<Brand> objBrand = ViewData["Brand"] as List<Brand>;
                    foreach (var obj in objBrand)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.BrandId.ToString());
                        row.CreateCell(1).SetCellValue(obj.BrandName.ToString());
                        row.CreateCell(2).SetCellValue(obj.Remark.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Brand.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Brand.xls");
            }
            else
            {
                string col = "";
                string condition = "";
                string value = "";
                if (Request.Form["brandId"].Trim() != "")
                {
                    col += ",BrandId";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["brandId"];
                }
                ViewData["Brand"] = logic.SearchBrand(col, condition, value);
                //brand.BrandId = Request.Form["brandId"];
                ViewBag.BrandId = Request.Form["brandId"];
                ViewBag.selCond = Request.Form["selCond"];
                //ViewData["Brand"] = logic.GetBrand(brand,0);
                
            }
            return View();
        }

        public ActionResult CUR()
        {
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            Brand brand = new Brand();
            brand.BrandId = Request.Form["brandId"];
            brand.BrandName = Request.Form["brandName"];
            brand.Remark = Request.Form["remark"];

            BrandLogic logic = new BrandLogic();
            string msg = "";
            if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
            {
                if (!logic.UpdateBrand(brand))
                {
                    ViewBag.BrandId = Request.Form["brandId"];
                    ViewBag.BrandName = Request.Form["brandName"];
                    ViewBag.Remark = Request.Form["remark"];
                    ViewBag.Msg = "修改失敗！";
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "Edit")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }

            }
            else if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
            {
                if (!logic.AddBrand(brand, out msg))
                {
                    ViewBag.BrandId = Request.Form["brandId"];
                    ViewBag.BrandName = Request.Form["brandName"];
                    ViewBag.Remark = Request.Form["remark"];
                    ViewBag.Msg = "新增失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "Save")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }
            }else
            {
                ViewBag.BrandId = Request.Form["brandId"];
                ViewBag.BrandName = Request.Form["brandName"];
                ViewBag.Remark = Request.Form["remark"];
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }

        }

        public ActionResult Delete(string BrandId)
        {
            BrandLogic logic = new BrandLogic();

            try
            {
                logic.DeleteBrand(BrandId);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            //Brand brand = new Brand();
            //ViewData["Brand"] = logic.GetBrand(brand);

            //return View("Index");
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string BrandId)
        {
            BrandLogic logic = new BrandLogic();
            Brand brand = logic.GetBrand(BrandId);
            ViewBag.BrandId = brand.BrandId;
            ViewBag.BrandName = brand.BrandName;
            ViewBag.Remark = brand.Remark;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        //[HttpPost]
        //public ActionResult Details()
        //{
        //    ViewBag.BrandId = Request.Form["brandId"];
        //    ViewBag.BrandName = Request.Form["brandName"];
        //    if (Request.Form["action"] == "Edit")
        //    {
        //        ViewBag.Type = "Edit";
        //    }
        //    return View("CUR");

        //}

        public ActionResult Edit(string BrandId)
        {
            BrandLogic logic = new BrandLogic();
            Brand brand = logic.GetBrand(BrandId);
            ViewBag.BrandId = brand.BrandId;
            ViewBag.BrandName = brand.BrandName;
            ViewBag.Remark = brand.Remark;
            ViewBag.Type = "Edit";
            return View("CUR");
        }
    }
}