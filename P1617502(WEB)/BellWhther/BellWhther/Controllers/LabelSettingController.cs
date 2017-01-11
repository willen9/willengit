using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace BellWhther.Controllers
{
    public class LabelSettingController : Controller
    {
        // GET: LabelSetting
        public ActionResult Index(FormCollection formCollection,string action)
        {
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit = usersLogic.GetLimitByUid(Session["UserId"].ToString());
            bool isSuccess = false;
            LabelSettingLogic labelSettingLogic = new LabelSettingLogic();
            List<LabelSetting> lstLabelSettings = labelSettingLogic.GetAllBtw();

            SelectList selectList = new SelectList(lstLabelSettings, "Type", "Type",
                formCollection.Count == 0 ? "==請選擇==" : formCollection["slBtw"]);

            ViewData["slBtwDisplay"] = selectList;
            ViewData["DisplayData"] = new List<LabelSetting>();
            if (action == "search")
            {
                ViewData["DisplayData"] = Search(labelSettingLogic, formCollection);
            }
            if (action == "delete")
            {
                if (labelSettingLogic.DelLabelMaintains(formCollection["chk"]))
                {
                    ViewBag.js = "<script>alert('刪除成功');</script>";
                    lstLabelSettings = labelSettingLogic.GetAllBtw();
                    selectList = new SelectList(lstLabelSettings, "Type", "Type", "==請選擇==");
                    ViewData["slBtwDisplay"] = selectList;

                    formCollection["slBtw"] =
                        formCollection["txtCus"] =
                            formCollection["txtSale"] =
                                formCollection["txtAssign"] =
                                formCollection["txtDep"] =
                                formCollection["slAry"] =
                                    formCollection["slAryInitialCycle"] =
                                        formCollection["slBtwNext"] = formCollection["chk"] = "";
                    isSuccess = true;
                }
                else
                {
                    ViewBag.js = "<script>alert('刪除失敗');</script>";
                    ViewBag.Fail = "D";
                }
                ViewData["DisplayData"] = Search(labelSettingLogic, formCollection);
            }
            if (action == "save")
            {
                string msg = "";
                LabelSetting labelSettingOld = new LabelSetting();
                if (formCollection["mode"] == "M")
                {
                    string pkOld = formCollection["pkOld"];
                    string[] pkStr = pkOld.Split('-');
                    labelSettingOld.Type = pkStr[0];
                    labelSettingOld.CustomerCode = pkStr[1];
                    labelSettingOld.VKORG = pkStr[2];
                    labelSettingOld.VTWEG = pkStr[3];
                    labelSettingOld.SPART = pkStr[4];
                }
                LabelSetting labelSetting = new LabelSetting();
                labelSetting.Type = formCollection["slBtw"];
                labelSetting.CustomerCode = formCollection["txtCus"];
                labelSetting.VKORG = formCollection["txtSale"];
                labelSetting.VTWEG = formCollection["txtAssign"];
                labelSetting.SPART = formCollection["txtDep"];
                labelSetting.Ary = formCollection["slAry"];
                labelSetting.AryInitialCycle = formCollection["slAryInitialCycle"];
                if (formCollection["slBtwNext"] != "==請選擇==")
                {
                    labelSetting.NextLabel = formCollection["slBtwNext"];
                }
                labelSetting.Operator = Session["UserId"].ToString();
                if (labelSettingLogic.SaveLabelMaintain(labelSettingOld, labelSetting, formCollection["mode"], out msg))
                {
                    ViewBag.js = "<script>alert('保存成功');</script>";
                    lstLabelSettings = labelSettingLogic.GetAllBtw();
                    selectList = new SelectList(lstLabelSettings, "Type", "Type", "==請選擇==");
                    ViewData["slBtwDisplay"] = selectList;

                    formCollection["pkOld"] =
                   formCollection["slBtw"] =
                        formCollection["txtCus"] =
                             formCollection["txtSale"] =
                                 formCollection["txtAssign"] =
                                 formCollection["txtDep"] =
                                 formCollection["slAry"] =
                                     formCollection["slAryInitialCycle"] =
                                         formCollection["slBtwNext"] = formCollection["chk"] = "";
                    isSuccess = true;
                }
                else
                {
                    if (msg.Length > 0)
                    {
                        ViewBag.js = "<script>alert('" + msg + "');</script>";
                    }
                    else
                    {
                        ViewBag.js = "<script>alert('保存失敗');</script>";
                    }
                    ViewBag.Fail = formCollection["mode"];
                }
                ViewData["DisplayData"] = Search(labelSettingLogic, formCollection);
            }
            if (formCollection.Count > 0)
            {
                ViewBag.Cus = formCollection["txtCus"];
                ViewBag.Sale = formCollection["txtSale"];
                ViewBag.Assign = formCollection["txtAssign"];
                ViewBag.Dep = formCollection["txtDep"];
                if (isSuccess)
                {
                    ViewBag.slAry = "";
                    ViewBag.slAryInitialCycle = "";
                }
                else
                {
                    ViewBag.slAry = formCollection["slAry"];
                    ViewBag.slAryInitialCycle = formCollection["slAryInitialCycle"];
                }
                ViewBag.Chk = formCollection["chk"];
            }
            return View();
        }

        private List<LabelSetting> Search(LabelSettingLogic labelSettingLogic, FormCollection formCollection)
        {
            LabelSetting labelSetting = new LabelSetting();
            if (formCollection.Count > 0 && formCollection["slBtw"] != "==請選擇==")
            {
                labelSetting.Type = formCollection["slBtw"];
            }
            labelSetting.CustomerCode = formCollection["txtCus"];
            labelSetting.VKORG = formCollection["txtSale"];
            labelSetting.VTWEG = formCollection["txtAssign"];
            labelSetting.SPART = formCollection["txtDep"];
            labelSetting.Ary = formCollection["slAry"];
            labelSetting.AryInitialCycle = formCollection["slAryInitialCycle"];
            if (formCollection.Count > 0 && formCollection["slBtwNext"] != "==請選擇==")
            {
                labelSetting.NextLabel = formCollection["slBtwNext"];
            }
            return labelSettingLogic.GetInfo(labelSetting);
        }
    }
}