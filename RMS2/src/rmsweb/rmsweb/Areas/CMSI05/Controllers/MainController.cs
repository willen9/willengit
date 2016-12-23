using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CMSI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ProductLogic logic = new ProductLogic();
            Product product = new Product();
            ViewData["Product"] = logic.GetProduct(product,"","","",0);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ProductLogic logic = new ProductLogic();
            Product product = new Product();

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
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];
                ViewData["Product"] = logic.SearchProductCategory(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["Product"] = logic.GetProduct(product,"","","",0);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("品號,品名,規格,單位,類別一,廠商簡稱");
                //    List<Product> objProduct = ViewData["Product"] as List<Product>;
                //    foreach (var obj in objProduct)
                //    {
                //        sw.WriteLine(obj.ProductNo + "," + obj.ProductName + "," + obj.Specification + "," + obj.Unit + "," + obj.ProductTypeId1Name + "," + obj.VendorName);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Product");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("品號");
                    row.CreateCell(1).SetCellValue("品名");
                    row.CreateCell(2).SetCellValue("規格");
                    row.CreateCell(3).SetCellValue("單位");
                    row.CreateCell(4).SetCellValue("類別一");
                    row.CreateCell(5).SetCellValue("廠商簡稱");


                    int index = 1;

                    List<Product> objProduct = ViewData["Product"] as List<Product>;
                    foreach (var obj in objProduct)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(1).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(2).SetCellValue(obj.Specification.ToString());
                        row.CreateCell(3).SetCellValue(obj.Unit.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductTypeId1Name.ToString());
                        row.CreateCell(5).SetCellValue(obj.VendorName.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Product.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Product.xls");

            }
            else
            {
                string col = "";
                string condition = "";
                string value = "";
                if (Request.Form["ProductNo"].Trim() != "")
                {
                    col += ",ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ProductNo"];
                }
                if (Request.Form["productName"].Trim() != "")
                {
                    col += ",ProductName";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["ProductName"];
                }
                ViewData["Product"] = logic.SearchProductCategory(col, condition, value);
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.ProductNo = Request.Form["ProductNo"];
                ViewBag.ProductName = Request.Form["productName"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            return View("CUR");
        }

        public JsonResult SearchBrand(string BrandId, string Col, string Condition, string conditionValue, int Page)
        {
            BrandLogic logicBrand = new BrandLogic();
            Brand brand = new Brand();
            brand.BrandId = BrandId;
            //brand.BrandName = BrandName;
            List<Brand> lst = logicBrand.GetBrand(brand, Col, Condition, conditionValue, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchVendor(string Col, string Condition, string conditionValue, int Page)
        {
            VendorLogic logic = new VendorLogic();
            Vendor vendor = new Vendor();
            //vendor.VendorId = VendorId;
            //vendor.VendorName = VendorName;

            DataTable dtVendor = logic.SelectVendorList(Col, Condition, conditionValue, Page);
            List<Vendor> lst = new List<Vendor>();
            if (dtVendor != null && dtVendor.Rows.Count>0)
            {
                foreach (DataRow dr in dtVendor.Rows)
                {
                    vendor = new Vendor();
                    vendor.VendorId = dr["VendorId"].ToString();
                    vendor.VendorName = dr["VendorName"].ToString();
                    vendor.Contact = dr["Contact"].ToString();
                    vendor.TelNo = dr["TelNo"].ToString();
                    vendor.FaxNo = dr["FaxNo"].ToString();
                    vendor.AddressSName = dr["AddressSName"].ToString();
                    vendor.Remark = dr["Remark"].ToString();
                    lst.Add(vendor);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchProdType(string ProductCategoryType, string ProductTypeId, string ProductType, int Page)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory pro = new ProductCategory();
            pro.ProductCategoryType = ProductCategoryType;
            pro.ProductTypeId = ProductTypeId;
            pro.ProductType = ProductType;
            List<ProductCategory> lst = logic.GetProductCategoryALL(pro,Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult CUR(string s)
        //{
        //    ProductLogic logic = new ProductLogic();
        //    Product product = new Product();
        //    product.ProductNo = Request.Form["ProductNo"];
        //    product.ProductName = Request.Form["productName"];
        //    product.Specification = Request.Form["productSpec"];
        //    product.VendorId = Request.Form["vender"];
        //    product.ProductTypeId1 = Request.Form["prodType1"];
        //    product.ProductTypeId2 = Request.Form["prodType2"];
        //    product.ProductTypeId3 = Request.Form["prodType3"];
        //    product.ProductTypeId4 = Request.Form["prodType4"];
        //    product.Explanation = Request.Form["remark"];
        //    product.Unit = Request.Form["unit"];
        //    product.BrandId = Request.Form["brand"];

        //    string msg = "";

        //    if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
        //    {
        //        if (!logic.AddProduct(product, out msg))
        //        {
        //            ViewBag.ProductNo = Request.Form["ProductNo"];
        //            ViewBag.ProductName = Request.Form["productName"];
        //            ViewBag.Specification = Request.Form["productSpec"];

        //            string TypeIdName = "";

        //            ViewBag.VendorId = Request.Form["vender"];
        //            if(Request.Form["vender"].Trim().Length!=0)
        //            {
        //                VendorLogic logicVendor = new VendorLogic();
        //                TypeIdName = logicVendor.GetVendorName(Request.Form["vender"]);
        //                ViewBag.VendorName = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }
                    

        //            ViewBag.BrandId = Request.Form["brand"];
        //            if (Request.Form["brand"].Trim().Length != 0)
        //            {
        //                BrandLogic logicBrand = new BrandLogic();
        //                TypeIdName = logicBrand.GetBrandName(Request.Form["brand"]);
        //                ViewBag.BrandName = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }

        //            ProductCategoryLogic logicProductCategory = new ProductCategoryLogic();
        //            ViewBag.ProductTypeId1 = Request.Form["prodType1"];
                     
        //            if(Request.Form["prodType1"].Trim().Length!=0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("1", Request.Form["prodType1"]);
        //                ViewBag.ProductTypeId1Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-")+1);
        //            }
                    
        //            ViewBag.ProductTypeId2 = Request.Form["prodType2"];
        //            if (Request.Form["prodType2"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("2", Request.Form["prodType2"]);
        //                ViewBag.ProductTypeId2Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }
                    
        //            ViewBag.ProductTypeId3 = Request.Form["prodType3"];
        //            if (Request.Form["prodType3"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("3", Request.Form["prodType3"]);
        //                ViewBag.ProductTypeId3Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }
                    
        //            ViewBag.ProductTypeId4 = Request.Form["prodType4"];
        //            if (Request.Form["prodType3"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("4", Request.Form["prodType4"]);
        //                ViewBag.ProductTypeId4Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }
                    

        //            ViewBag.Explanation = Request.Form["remark"];
        //            ViewBag.Unit = Request.Form["unit"];

        //            ViewBag.Msg = "新增失敗！" + msg;
        //            return View("CUR");
        //        }
        //        else
        //        {
        //            if (Request.Form["action"] == "Save")
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("CUR");
        //            }
        //        }
        //    }
        //    else if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
        //    {
        //        if (!logic.UpdateProduct(product))
        //        {
        //            ViewBag.ProductNo = Request.Form["ProductNo"];
        //            ViewBag.ProductName = Request.Form["productName"];
        //            ViewBag.Specification = Request.Form["productSpec"];

        //            string TypeIdName = "";

        //            ViewBag.VendorId = Request.Form["vender"];
        //            if (Request.Form["vender"].Trim().Length != 0)
        //            {
        //                VendorLogic logicVendor = new VendorLogic();
        //                TypeIdName = logicVendor.GetVendorName(Request.Form["vender"]);
        //                ViewBag.VendorName = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }


        //            ViewBag.BrandId = Request.Form["brand"];
        //            if (Request.Form["brand"].Trim().Length != 0)
        //            {
        //                BrandLogic logicBrand = new BrandLogic();
        //                TypeIdName = logicBrand.GetBrandName(Request.Form["brand"]);
        //                ViewBag.BrandName = TypeIdName.Substring(TypeIdName.LastIndexOf("-") + 1);
        //            }

        //            ProductCategoryLogic logicProductCategory = new ProductCategoryLogic();
        //            ViewBag.ProductTypeId1 = Request.Form["prodType1"];

        //            if (Request.Form["prodType1"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("1", Request.Form["prodType1"]);
        //                ViewBag.ProductTypeId1Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-"));
        //            }

        //            ViewBag.ProductTypeId2 = Request.Form["prodType2"];
        //            if (Request.Form["prodType2"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("2", Request.Form["prodType2"]);
        //                ViewBag.ProductTypeId2Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-"));
        //            }

        //            ViewBag.ProductTypeId3 = Request.Form["prodType3"];
        //            if (Request.Form["prodType3"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("3", Request.Form["prodType3"]);
        //                ViewBag.ProductTypeId3Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-"));
        //            }

        //            ViewBag.ProductTypeId4 = Request.Form["prodType4"];
        //            if (Request.Form["prodType3"].Trim().Length != 0)
        //            {
        //                TypeIdName = logicProductCategory.GetProductType("4", Request.Form["prodType4"]);
        //                ViewBag.ProductTypeId4Name = TypeIdName.Substring(TypeIdName.LastIndexOf("-"));
        //            }

        //            ViewBag.Explanation = Request.Form["remark"];
        //            ViewBag.Unit = Request.Form["unit"];
        //            ViewBag.Type = "Edit";
        //            ViewBag.Msg = "更新失敗！";
        //            return View("CUR");
        //        }
        //        else
        //        {
        //            if (Request.Form["action"] == "Edit")
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View("CUR");
        //            }
        //        }
        //    }else
        //    {
        //        product = logic.GetProductInfo(Request.Form["ProductNo"]);
        //        ViewBag.ProductNo = product.ProductNo;
        //        ViewBag.ProductName = product.ProductName;
        //        ViewBag.Specification = product.Specification;
        //        ViewBag.ProductTypeId1 = product.ProductTypeId1;
        //        ViewBag.ProductTypeId1Name = product.ProductTypeId1Name;
        //        ViewBag.ProductTypeId2 = product.ProductTypeId2;
        //        ViewBag.ProductTypeId2Name = product.ProductTypeId2Name;
        //        ViewBag.ProductTypeId3 = product.ProductTypeId3;
        //        ViewBag.ProductTypeId3Name = product.ProductTypeId3Name;
        //        ViewBag.ProductTypeId4 = product.ProductTypeId4;
        //        ViewBag.ProductTypeId4Name = product.ProductTypeId4Name;
        //        ViewBag.Explanation = product.Explanation;
        //        ViewBag.VendorId = product.VendorId;
        //        ViewBag.Unit = product.Unit;
        //        ViewBag.VendorName = product.VendorName;
        //        ViewBag.BrandId = product.BrandId;
        //        if (Request.Form["action"] == "EditDetails")
        //        {
        //            ViewBag.Type = "Edit";
        //        }
        //        return View("CUR");
        //    }
        //}

        [HttpPost]
        public JsonResult Add()
        {
            ProductLogic logic = new ProductLogic();
            Product product = new Product();
            product.ProductNo = Request.Form["ProductNo"];
            product.ProductName = Request.Form["productName"];
            product.Specification = Request.Form["productSpec"];
            product.VendorId = Request.Form["vender"];
            product.ProductTypeId1 = Request.Form["prodType1"];
            product.ProductTypeId2 = Request.Form["prodType2"];
            product.ProductTypeId3 = Request.Form["prodType3"];
            product.ProductTypeId4 = Request.Form["prodType4"];
            product.Explanation = Request.Form["remark"];
            product.Unit = Request.Form["unit"];
            product.SerialControl = Request.Form["SerialControl"]==null?"N":"Y";
            product.BrandId = Request.Form["brand"];

            string msg = "";

            string infomsg = "";
            if (!logic.AddProduct(product, out infomsg))
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
            ProductLogic logic = new ProductLogic();
            Product product = new Product();
            product.ProductNo = Request.Form["ProductNo"];
            product.ProductName = Request.Form["productName"];
            product.Specification = Request.Form["productSpec"];
            product.VendorId = Request.Form["vender"];
            product.ProductTypeId1 = Request.Form["prodType1"];
            product.ProductTypeId2 = Request.Form["prodType2"];
            product.ProductTypeId3 = Request.Form["prodType3"];
            product.ProductTypeId4 = Request.Form["prodType4"];
            product.Explanation = Request.Form["remark"];
            product.Unit = Request.Form["unit"];
            product.SerialControl = Request.Form["SerialControl"] == null ? "N" : "Y";
            product.BrandId = Request.Form["brand"];
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateProduct(product, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]  //清除缓存
        public JsonResult ProductNo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            bool type = logic.IsProductNo(ProductNo);
            return Json(type,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetBrandName(string BrandId)
        {
            BrandLogic logic = new BrandLogic();
            return Json(logic.GetBrandName(BrandId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetVendorName(string VendorId)
        {
            VendorLogic logic = new VendorLogic();
            return Json(logic.GetVendorName(VendorId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductType(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            return Json(logic.GetProductType(ProductCategoryType, ProductTypeId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();

            try
            {
                logic.DeleteProduct(ProductNo);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            ViewBag.ProductNo = product.ProductNo;
            ViewBag.ProductName = product.ProductName;
            ViewBag.Specification = product.Specification;
            ViewBag.ProductTypeId1 = product.ProductTypeId1;
            ViewBag.ProductTypeId1Name = product.ProductTypeId1Name;
            ViewBag.ProductTypeId2 = product.ProductTypeId2;
            ViewBag.ProductTypeId2Name = product.ProductTypeId2Name;
            ViewBag.ProductTypeId3 = product.ProductTypeId3;
            ViewBag.ProductTypeId3Name = product.ProductTypeId3Name;
            ViewBag.ProductTypeId4 = product.ProductTypeId4;
            ViewBag.ProductTypeId4Name = product.ProductTypeId4Name;
            ViewBag.SerialControl = product.SerialControl;
            ViewBag.Explanation = product.Explanation;
            ViewBag.VendorId = product.VendorId;
            ViewBag.Unit = product.Unit;
            ViewBag.SerialControl = product.SerialControl;
            ViewBag.VendorName = product.VendorName;
            ViewBag.BrandId = product.BrandId;
            ViewBag.BrandName = product.BrandName;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            ViewBag.ProductNo = product.ProductNo;
            ViewBag.ProductName = product.ProductName;
            ViewBag.Specification = product.Specification;
            ViewBag.ProductTypeId1 = product.ProductTypeId1;
            ViewBag.ProductTypeId1Name = product.ProductTypeId1Name;
            ViewBag.ProductTypeId2 = product.ProductTypeId2;
            ViewBag.ProductTypeId2Name = product.ProductTypeId2Name;
            ViewBag.ProductTypeId3 = product.ProductTypeId3;
            ViewBag.ProductTypeId3Name = product.ProductTypeId3Name;
            ViewBag.ProductTypeId4 = product.ProductTypeId4;
            ViewBag.ProductTypeId4Name = product.ProductTypeId4Name;
            ViewBag.SerialControl = product.SerialControl;
            ViewBag.Explanation = product.Explanation;
            ViewBag.VendorId = product.VendorId;
            ViewBag.Unit = product.Unit;
            ViewBag.SerialControl = product.SerialControl;
            ViewBag.VendorName = product.VendorName;
            ViewBag.BrandId = product.BrandId;
            ViewBag.BrandName = product.BrandName;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Copy(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            //ViewBag.ProductNo = product.ProductNo;
            ViewBag.ProductName = product.ProductName;
            ViewBag.Specification = product.Specification;
            ViewBag.ProductTypeId1 = product.ProductTypeId1;
            ViewBag.ProductTypeId1Name = product.ProductTypeId1Name;
            ViewBag.ProductTypeId2 = product.ProductTypeId2;
            ViewBag.ProductTypeId2Name = product.ProductTypeId2Name;
            ViewBag.ProductTypeId3 = product.ProductTypeId3;
            ViewBag.ProductTypeId3Name = product.ProductTypeId3Name;
            ViewBag.ProductTypeId4 = product.ProductTypeId4;
            ViewBag.ProductTypeId4Name = product.ProductTypeId4Name;
            ViewBag.Explanation = product.Explanation;
            ViewBag.VendorId = product.VendorId;
            ViewBag.SerialControl = product.SerialControl == null ? "N" : "Y";
            ViewBag.Unit = product.Unit;
            ViewBag.VendorName = product.VendorName;
            ViewBag.BrandId = product.BrandId;
            ViewBag.BrandName = product.BrandName;
            return View("CUR");
        }
    }
}