using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace SERB03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();
            string col = ",Confirmed,Closed";
            string condition = ",<>,=";
            string value = ",V,N";
            //ViewBag.closed = "N";
            //ViewBag.selCond1 = "=";
            ViewData["CompletionOrder"] = logic.SearchCompletionOrder(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = new ProductCategory();

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
                    //return Json("NO-匯入失敗", "text/x-json");
                    return Json("NO-匯入失敗", JsonRequestBehavior.AllowGet);
                }
                return Json("YES-匯入成功", JsonRequestBehavior.AllowGet);
            }

            if(Request.Form["action"]=="Advanced")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];
                ViewData["ProductCategory"] = logic.SearchProductCategory(col, condition, value);
                //TempData["ProductCategory"] = ViewData["ProductCategory"];
            }
            else if (Request.Form["action"] == "btnExport")
            {
                //List<ProductCategory> objProductCategory = TempData["ProductCategory"] as List<ProductCategory>;
                //ViewData["ProductCategory"] = TempData["ProductCategory"];

                ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory,0);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("分類方式,類別代號,類別名稱");
                    List<ProductCategory> objProductCategory = ViewData["ProductCategory"] as List<ProductCategory>;
                    string cateType="";
                    foreach (var obj in objProductCategory)
                    {
                        if (obj.ProductCategoryType == "1")
                        {
                            cateType = "分類一";
                        }
                        else if (obj.ProductCategoryType == "2")
                        {
                            cateType = "分類二";
                        }
                        else if (obj.ProductCategoryType == "3")
                        {
                            cateType = "分類三";
                        }
                        else
                        {
                            cateType = "分類四";
                        }
                        sw.WriteLine(cateType+","+obj.ProductTypeId+","+obj.ProductType);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ProductCategory.csv");
            }
            else
            {
                string col = "";
                string condition = "";
                string value = "";
                if (Request.Form["parenttype"].Trim() != "")
                {
                    col += ",ProductCategoryType";
                    condition += ",=";
                    value += "," + Request.Form["parenttype"];
                }
                if (Request.Form["companyid"].Trim() != "")
                {
                    col += ",ProductTypeId";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["companyid"];
                }
                //productCategory.ProductCategoryType = Request.Form["parenttype"];
                //productCategory.ProductTypeId = Request.Form["companyid"];
                //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory,0);
                ViewData["ProductCategory"] = logic.SearchProductCategory(col, condition, value);
                ViewBag.SelectCategoryType = Request.Form["parenttype"];
                ViewBag.findcompanyid = Request.Form["companyid"];
                ViewBag.selCond = Request.Form["selCond"];
                //TempData["ProductCategory"] = ViewData["ProductCategory"];
            }
            return View();
        }

        public ActionResult Details(string CompletionType, string CompletionNo)
        {
            CompletionOrderLogic logic = new CompletionOrderLogic();

            CompletionOrder completionOrder = new CompletionOrder();
            completionOrder.CompletionType = CompletionType;
            completionOrder.CompletionNo = CompletionNo;
            completionOrder = logic.GetCompletionOrderInfo(completionOrder);
            ViewBag.CompletionType = completionOrder.CompletionType;
            ViewBag.CompletionNo = completionOrder.CompletionNo;
            ViewBag.OrderDate = completionOrder.OrderDate;
            ViewBag.NoOfPrints = completionOrder.NoOfPrints;
            ViewBag.SourceOrderType = completionOrder.SourceOrderType;
            ViewBag.SourceOrderNo = completionOrder.SourceOrderNo;
            ViewBag.Remark = completionOrder.Remark;
            ViewBag.CustomerId = completionOrder.CustomerId;
            ViewBag.CustomerName = completionOrder.CustomerName;
            ViewBag.AddressSName = completionOrder.AddressSName;
            ViewBag.Address = completionOrder.Address;
            ViewBag.Contact = completionOrder.Contact;
            ViewBag.TelNo = completionOrder.TelNo;
            ViewBag.FaxNo = completionOrder.FaxNo;
            ViewBag.ProductNo = completionOrder.ProductNo;
            ViewBag.ProductName = completionOrder.ProductName;
            ViewBag.SerialNo = completionOrder.SerialNo;
            ViewBag.AssetNo = completionOrder.AssetNo;
            ViewBag.Contract = completionOrder.Contract;
            ViewBag.TestResult = completionOrder.TestResult;
            ViewBag.InternalRemark = completionOrder.InternalRemark;
            ViewBag.Confirmed = completionOrder.Confirmed;
            ViewBag.UnderWarranty = completionOrder.UnderWarranty;
            ViewBag.CustomerType = completionOrder.CustomerType;
            ViewBag.OrderSName = completionOrder.OrderSName;
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR()
        {
            if (Request.Form["action"] == "btnAllot")
            {
                SupportApl_HLogic logic = new SupportApl_HLogic();
                string[] OrderNo = logic.GetSupportAplOrderNo(Request.Form["ordertype"]).Split('-');
                ViewBag.RGAReturnType = Request.Form["ordertype"];
                ViewBag.RGAReturnNo = OrderNo[1];

                //SupportApl_HLogic Slogic = new SupportApl_HLogic();
                //SupportApl_H supportApl_H = Slogic.SupportItemInfo(Request.Form["SupportAplOrderType"], Request.Form["SupportAplOrderNo"]);

                //ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
                //ViewBag.SupportAplOrderType = supportApl_H.SupportAplOrderType;
                //ViewBag.SupportAplOrderNo = supportApl_H.SupportAplOrderNo;
                //ViewBag.CustomerId = supportApl_H.CustomerId;
                //ViewBag.CustomerType = supportApl_H.CustomerType;
                ////ViewBag.Sales = supportApl_H.Sales;
                //ViewBag.RequestDate = supportApl_H.RequestDate;
                //ViewBag.RequestTime = supportApl_H.RequestTime;
                //ViewBag.SupportDept = supportApl_H.SupportDept;
                //ViewBag.Remark = supportApl_H.Remark;
                //ViewBag.PickingDate = Request.Form["requesDate"];
                ViewBag.NoOfPrints = "0";

                //SupportApl_ProductDLogic Dlogic = new SupportApl_ProductDLogic();
                //ViewData["SupportApl_ProductD"] = Dlogic.GetSupportApl_ProductD(Request.Form["SupportAplOrderType"], Request.Form["SupportAplOrderNo"]);

                return View("CUR");
            }
            else
            {
                Picking_H picking_H = new Picking_H();
                picking_H.PickingOrderType = Request.Form["AsignOrderType"];            //發料單別
                picking_H.PickingOrderNo = Request.Form["AsignOrderNo"];                //發料單號
                picking_H.OrderDate = Request.Form["orderdate"].Replace("/", "");        //單據日期
                picking_H.SupportAplOrderType = Request.Form["SupportAplOrderType"];    //支援申請單別
                picking_H.SupportAplOrderNo = Request.Form["SupportAplOrderNo"];        //支援申請單號
                picking_H.CustomerId = Request.Form["customerId"];                      //客戶代號
                picking_H.CustomerType = Request.Form["customertype"];                  //客戶型態
                picking_H.Sales = Request.Form["salesname"];                            //業務員
                picking_H.RequestDate = Request.Form["requesDate"];                     //需求日期
                picking_H.RequestTime = Request.Form["id_requesttime"];                 //指定時間
                picking_H.SupportDept = Request.Form["departmentId"];                   //支援單位
                picking_H.PickingDate = Request.Form["pickingdate"];                    //發料日期
                picking_H.PickingMan = Request.Form["pickingman"];                      //發料人員
                picking_H.Remark = Request.Form["remark"];                              //備註

                //品項明細
                string strproductno = Request.Form["strproductno"] == null ? "" : Request.Form["strproductno"].ToString();                  //品號
                string strproductname = Request.Form["strproductname"] == null ? "" : Request.Form["strproductname"].ToString();            //品名
                string strqty = Request.Form["strqty"] == null ? "" : Request.Form["strqty"].ToString();                                    //發料數量
                string strpickingQTY = Request.Form["strpickingQTY"] == null ? "" : Request.Form["strpickingQTY"].ToString();                     //已發數量
                string strunit = Request.Form["strunit"] == null ? "" : Request.Form["strunit"].ToString();                       //單位
                string strremark = Request.Form["strremark"] == null ? "" : Request.Form["strremark"].ToString();                           //備註

                Picking_HLogic logic = new Picking_HLogic();
                string msg = "";
                if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
                {
                    if (!logic.AddPicking_H(picking_H, strproductno, strproductname, strqty, strpickingQTY, strunit, strremark, out msg))
                    {
                        List<SupportApl_ProductD> objSupportApl_ProductD = new List<SupportApl_ProductD>();
                        ViewData["SupportApl_ProductD"] = objSupportApl_ProductD;
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
                }
                return RedirectToAction("Index");
            }

        }
    }
}