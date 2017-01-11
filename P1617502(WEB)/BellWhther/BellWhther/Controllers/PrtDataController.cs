using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BellWhther.Controllers
{
    public class PrtDataController : Controller
    {
        // GET: PrtData
        public ActionResult Index(FormCollection formCollection,string action)
        {
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit = usersLogic.GetLimitByUid(Session["UserId"].ToString());
            PrtDataLogic prtDataLogic = new PrtDataLogic();
            LabelSettingLogic labelSettingLogic = new LabelSettingLogic();
            List<LabelSetting> lstLabelSettings = labelSettingLogic.GetAllBtw();

            SelectList selectList = new SelectList(lstLabelSettings, "Type", "Type",
                formCollection.Count == 0 ? "==請選擇==" : formCollection["slBtw"]);

            ViewData["slBtwDisplay"] = selectList;
            ViewData["DisplayData"] = new List<LabelSetting>();
            if (action == "search")
            {
                ViewData["DisplayData"] = Search(prtDataLogic, formCollection);
            }
            if (action == "print")
            {
                if (prtDataLogic.PintRepeat(formCollection["chk"]))
                {
                    ViewBag.js = "<script>alert('補印成功');</script>";
                    lstLabelSettings = labelSettingLogic.GetAllBtw();
                    selectList = new SelectList(lstLabelSettings, "Type", "Type", "==請選擇==");
                    ViewData["slBtwDisplay"] = selectList;

                    formCollection["slBtw"] =
                        formCollection["txtCus"] =
                            formCollection["txtSale"] =
                                formCollection["txtAssign"] =
                                    formCollection["txtDep"] =
                                        formCollection["txtProdNo"] =formCollection["chk"]=  "";
                    formCollection["txtDateStart"] =formCollection["txtDateEnd"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    ViewBag.js = "<script>alert('補印失敗');</script>";
                    ViewBag.Fail = "P";
                }
                ViewData["DisplayData"] = Search(prtDataLogic, formCollection);
            }
            if (action == "export")
            {
                List<LabelSetting> lstExport= Search(prtDataLogic, formCollection);
                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = new FileStream(path + @"\History.xlsx", FileMode.Create, FileAccess.Write))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet=workbook.CreateSheet("歷程查詢");
                    sheet.SetColumnWidth(0,20*256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);
                    sheet.SetColumnWidth(8, 20 * 256);
                    sheet.SetColumnWidth(9, 20 * 256);
                    sheet.SetColumnWidth(10, 20 * 256);
                    sheet.SetColumnWidth(11, 20 * 256);
                    sheet.SetColumnWidth(12, 20 * 256);
                    sheet.SetColumnWidth(13, 20 * 256);
                    sheet.SetColumnWidth(14, 20 * 256);
                    sheet.SetColumnWidth(15, 20 * 256);
                    sheet.SetColumnWidth(16, 20 * 256);
                    sheet.SetColumnWidth(17, 20 * 256);
                    sheet.SetColumnWidth(18, 20 * 256);
                    sheet.SetColumnWidth(19, 20 * 256);
                    sheet.SetColumnWidth(20, 20 * 256);
                    sheet.SetColumnWidth(21, 20 * 256);
                    sheet.SetColumnWidth(22, 20 * 256);
                    sheet.SetColumnWidth(23, 20 * 256);
                    sheet.SetColumnWidth(24, 20 * 256);
                    sheet.SetColumnWidth(25, 20 * 256);
                    sheet.SetColumnWidth(26, 20 * 256);
                    sheet.SetColumnWidth(27, 20 * 256);
                    sheet.SetColumnWidth(28, 20 * 256);
                    sheet.SetColumnWidth(29, 20 * 256);
                    sheet.SetColumnWidth(30, 20 * 256);
                    sheet.SetColumnWidth(31, 20 * 256);
                    sheet.SetColumnWidth(32, 20 * 256);
                    sheet.SetColumnWidth(33, 20 * 256);
                    sheet.SetColumnWidth(34, 20 * 256);
                    sheet.SetColumnWidth(35, 20 * 256);
                    sheet.SetColumnWidth(36, 20 * 256);
                    sheet.SetColumnWidth(37, 20 * 256);
                    sheet.SetColumnWidth(38, 20 * 256);
                    sheet.SetColumnWidth(39, 20 * 256);
                    sheet.SetColumnWidth(40, 20 * 256);
                    sheet.SetColumnWidth(41, 20 * 256);
                    sheet.SetColumnWidth(42, 20 * 256);
                    sheet.SetColumnWidth(43, 20 * 256);
                    sheet.SetColumnWidth(44, 20 * 256);
                    sheet.SetColumnWidth(45, 20 * 256);
                    sheet.SetColumnWidth(46, 20 * 256);
                    sheet.SetColumnWidth(47, 20 * 256);
                    sheet.SetColumnWidth(48, 20 * 256);
                    sheet.SetColumnWidth(49, 20 * 256);
                    sheet.SetColumnWidth(40, 20 * 256);
                    sheet.SetColumnWidth(51, 20 * 256);
                    sheet.SetColumnWidth(52, 20 * 256);
                    sheet.SetColumnWidth(53, 20 * 256);
                    sheet.SetColumnWidth(54, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("BTW");
                    row.CreateCell(1).SetCellValue("客戶代碼");
                    row.CreateCell(2).SetCellValue("廠內料號");
                    row.CreateCell(3).SetCellValue("銷售組織");
                    row.CreateCell(4).SetCellValue("配銷通路");
                    row.CreateCell(5).SetCellValue("部門別");
                    row.CreateCell(6).SetCellValue("客戶名稱1");
                    row.CreateCell(7).SetCellValue("客戶名稱2");
                    row.CreateCell(8).SetCellValue("企業群組");
                    row.CreateCell(9).SetCellValue("Sorg");
                    row.CreateCell(10).SetCellValue("客戶料號");
                    row.CreateCell(11).SetCellValue("生產日期");
                    row.CreateCell(12).SetCellValue("數量");
                    row.CreateCell(13).SetCellValue("廠內物料描述");
                    row.CreateCell(14).SetCellValue("客戶物料描述");
                    row.CreateCell(15).SetCellValue("生產周別");
                    row.CreateCell(16).SetCellValue("採購單號");
                    row.CreateCell(17).SetCellValue("材質");
                    row.CreateCell(18).SetCellValue("MSL Level");
                    row.CreateCell(19).SetCellValue("原廠DC前1碼年");
                    row.CreateCell(20).SetCellValue("原廠DC前2碼月");
                    row.CreateCell(21).SetCellValue("原廠DC前3碼日");
                    row.CreateCell(22).SetCellValue("供應商代碼");
                    row.CreateCell(23).SetCellValue("客戶版本號");
                    row.CreateCell(24).SetCellValue("保固期");
                    row.CreateCell(25).SetCellValue("項目名稱");
                    row.CreateCell(26).SetCellValue("機種");
                    row.CreateCell(27).SetCellValue("國騰品號");
                    row.CreateCell(28).SetCellValue("客戶品號");
                    row.CreateCell(29).SetCellValue("LOT");
                    row.CreateCell(30).SetCellValue("REV");
                    row.CreateCell(31).SetCellValue("單量");
                    row.CreateCell(32).SetCellValue("淨量");
                    row.CreateCell(33).SetCellValue("毛量");
                    row.CreateCell(34).SetCellValue("箱號狀況");
                    row.CreateCell(35).SetCellValue("本廠版本號");
                    row.CreateCell(36).SetCellValue("流水碼進制");
                    row.CreateCell(37).SetCellValue("流水碼歸0時間");
                    row.CreateCell(38).SetCellValue("每季第一個月");
                    row.CreateCell(39).SetCellValue("每季第二個月");
                    row.CreateCell(40).SetCellValue("每季第三個月");
                    row.CreateCell(41).SetCellValue("季標籤確認第一個月");
                    row.CreateCell(42).SetCellValue("季標籤確認第二個月");
                    row.CreateCell(43).SetCellValue("季標籤確認第三個月");
                    row.CreateCell(44).SetCellValue("年");
                    row.CreateCell(45).SetCellValue("月");
                    row.CreateCell(46).SetCellValue("日");
                    row.CreateCell(47).SetCellValue("下一張BTW");
                    row.CreateCell(48).SetCellValue("發票編號");
                    row.CreateCell(49).SetCellValue("廠區代碼");
                    row.CreateCell(50).SetCellValue("其他1");
                    row.CreateCell(51).SetCellValue("其他2");
                    row.CreateCell(52).SetCellValue("其他3");
                    row.CreateCell(53).SetCellValue("其他4");
                    row.CreateCell(54).SetCellValue("其他5");
                   
                    int rowCount = 1;
                    foreach (LabelSetting model in lstExport)
                    {
                        row = sheet.CreateRow(rowCount++);
                        row.CreateCell(0).SetCellValue(model.Type);
                        row.CreateCell(1).SetCellValue(model.CustomerCode);
                        row.CreateCell(2).SetCellValue(model.SPEC);
                        row.CreateCell(3).SetCellValue(model.VKORG);
                        row.CreateCell(4).SetCellValue(model.VTWEG);
                        row.CreateCell(5).SetCellValue(model.SPART);
                        row.CreateCell(6).SetCellValue(model.CustomerName1);
                        row.CreateCell(7).SetCellValue(model.CustomerName2);
                        row.CreateCell(8).SetCellValue(model.EnterpriseGroup);
                        row.CreateCell(9).SetCellValue(model.Sorg);
                        row.CreateCell(10).SetCellValue(model.CustomerPartNo);
                        row.CreateCell(11).SetCellValue(model.DC);
                        row.CreateCell(12).SetCellValue(model.Qty);
                        row.CreateCell(13).SetCellValue(model.ItemDescreption1);
                        row.CreateCell(14).SetCellValue(model.ItemDescreption2);
                        row.CreateCell(15).SetCellValue(model.LN);
                        row.CreateCell(16).SetCellValue(model.PurchasOrder);
                        row.CreateCell(17).SetCellValue(model.LCP);
                        row.CreateCell(18).SetCellValue(model.MSLLevel);
                        row.CreateCell(19).SetCellValue(model.DC1);
                        row.CreateCell(20).SetCellValue(model.DC2);
                        row.CreateCell(21).SetCellValue(model.DC3);
                        row.CreateCell(22).SetCellValue(model.VC);
                        row.CreateCell(23).SetCellValue(model.CustomerVer);
                        row.CreateCell(24).SetCellValue(model.Warranty);
                        row.CreateCell(25).SetCellValue(model.ItemName);
                        row.CreateCell(26).SetCellValue(model.MachineType);
                        row.CreateCell(27).SetCellValue(model.ItemNo);
                        row.CreateCell(28).SetCellValue(model.customerItemNo);
                        row.CreateCell(29).SetCellValue(model.LotNo);
                        row.CreateCell(30).SetCellValue(model.Rev);
                        row.CreateCell(31).SetCellValue(model.Weight1);
                        row.CreateCell(32).SetCellValue(model.Weight2);
                        row.CreateCell(33).SetCellValue(model.Weight3);
                        row.CreateCell(34).SetCellValue(model.CartonStatus);
                        row.CreateCell(35).SetCellValue(model.OriginalVer);
                        row.CreateCell(36).SetCellValue(model.Ary);
                        row.CreateCell(37).SetCellValue(model.AryInitialCycle);
                        row.CreateCell(38).SetCellValue(model.Season1);
                        row.CreateCell(39).SetCellValue(model.Season2);
                        row.CreateCell(40).SetCellValue(model.Season3);
                        row.CreateCell(41).SetCellValue(model.CurrentSeason1);
                        row.CreateCell(42).SetCellValue(model.CurrentSeason2);
                        row.CreateCell(43).SetCellValue(model.CurrentSeason3);
                        row.CreateCell(44).SetCellValue(model.Year);
                        row.CreateCell(45).SetCellValue(model.Month);
                        row.CreateCell(46).SetCellValue(model.day);
                        row.CreateCell(47).SetCellValue(model.NextLabel);
                        row.CreateCell(48).SetCellValue(model.INV);
                        row.CreateCell(49).SetCellValue(model.ZoneCode);
                        row.CreateCell(50).SetCellValue(model.Other1);
                        row.CreateCell(51).SetCellValue(model.Other2);
                        row.CreateCell(52).SetCellValue(model.Other3);
                        row.CreateCell(53).SetCellValue(model.Other4);
                        row.CreateCell(54).SetCellValue(model.Other5);
                    }
                    workbook.Write(fs);
                }
                return File(new FileStream(path + @"\History.xlsx", FileMode.Open), "text/plain", "History.xlsx");
            }
            if (formCollection.Count > 0)
            {
                ViewBag.Cus = formCollection["txtCus"];
                ViewBag.Sale = formCollection["txtSale"];
                ViewBag.Assign = formCollection["txtAssign"];
                ViewBag.Dep = formCollection["txtDep"];
                ViewBag.ProdNo = formCollection["txtProdNo"];
                ViewBag.DateStart = formCollection["txtDateStart"];
                ViewBag.DateEnd = formCollection["txtDateEnd"];
                ViewBag.Chk = formCollection["chk"];
            }
            return View();
        }

        private List<LabelSetting> Search(PrtDataLogic prtDataLogic, FormCollection formCollection)
        {
            PrtData prtData = new PrtData();
            if (formCollection.Count > 0 && formCollection["slBtw"] != "==請選擇==")
            {
                prtData.LabelType = formCollection["slBtw"];
            }
            prtData.CustomerCode = formCollection["txtCus"];
            prtData.VKORG = formCollection["txtSale"];
            prtData.VTWEG = formCollection["txtAssign"];
            prtData.SPART = formCollection["txtDep"];
            prtData.SPEC = formCollection["txtProdNo"];
            prtData.OPDateStart = formCollection["txtDateStart"];
            prtData.OPDateEnd = formCollection["txtDateEnd"];
            return prtDataLogic.GetInfo(prtData);
        }
    }
}