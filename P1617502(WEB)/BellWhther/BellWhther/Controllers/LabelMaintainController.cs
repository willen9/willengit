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
    public class LabelMaintainController : Controller
    {
        // GET: LabelMaintain
        public ActionResult Index(FormCollection formCollection, string action)
        {
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit = usersLogic.GetLimitByUid(Session["UserId"].ToString());
            //IDictionary<string, string> searchConditions = null;
            LabelSettingLogic labelSettingLogic = new LabelSettingLogic();
            LabelMaintainLogic labelMaintainLogic = new LabelMaintainLogic();

            List<LabelSetting> lstLabelSettings = labelSettingLogic.GetAllBtw();

            SelectList selectList = new SelectList(lstLabelSettings, "Type", "Type",
                formCollection.Count == 0 ? "==請選擇==" : formCollection["slBtw"]);

            ViewData["slBtwDisplay"] = selectList;
            ViewData["DisplayData"] = new List<LabelMaintain>();
            //object values = null;
            //if (TempData.TryGetValue("SearchConditions", out values))
            //{
            //    searchConditions = values as Dictionary<string, string>;
            //}
            if (action == "search")
            {
                //if (formCollection.Count > 0)
                //{
                //    if (searchConditions == null)
                //    {
                //        searchConditions = new Dictionary<string, string>();
                //    }
                //    searchConditions.Clear();
                //    searchConditions.Add("slBtw", formCollection["slBtw"]);
                //    searchConditions.Add("txtCus", formCollection["txtCus"]);
                //    searchConditions.Add("txtProdNo", formCollection["txtProdNo"]);
                //    searchConditions.Add("txtSale", formCollection["txtSale"]);
                //    searchConditions.Add("txtAssign", formCollection["txtAssign"]);
                //    searchConditions.Add("txtDep", formCollection["txtDep"]);
                //    searchConditions.Add("txtMat", formCollection["txtMat"]);
                //    searchConditions.Add("txtMSL", formCollection["txtMSL"]);
                //    searchConditions.Add("txtCusVer", formCollection["txtCusVer"]);
                //    searchConditions.Add("txtREV", formCollection["txtREV"]);
                //    searchConditions.Add("txtOth1", formCollection["txtOth1"]);
                //    searchConditions.Add("txtOth2", formCollection["txtOth2"]);
                //    searchConditions.Add("txtOth3", formCollection["txtOth3"]);
                //    searchConditions.Add("txtOth4", formCollection["txtOth4"]);
                //    searchConditions.Add("txtOth5", formCollection["txtOth5"]);
                //}
                ViewData["DisplayData"] = Search(labelMaintainLogic, formCollection);
            }
            if (action == "delete")
            {
                if (labelMaintainLogic.DelLabelMaintains(formCollection["chk"]))
                {
                    ViewBag.js = "<script>alert('刪除成功');</script>";
                    lstLabelSettings = labelSettingLogic.GetAllBtw();
                    selectList = new SelectList(lstLabelSettings, "Type", "Type", "==請選擇==");
                    ViewData["slBtwDisplay"] = selectList;

                    formCollection["slBtw"] =
                  formCollection["txtCus"] =
                        formCollection["txtProdNo"] =
                            formCollection["txtSale"] =
                                formCollection["txtAssign"] =
                                formCollection["txtDep"] =
                                formCollection["txtMat"] =
                                    formCollection["txtMSL"] =
                                        formCollection["txtCusVer"] =
                                            formCollection["txtREV"] =
                                                formCollection["txtOth1"] =
                                                    formCollection["txtOth2"] =
                                                        formCollection["txtOth3"] =
                                                            formCollection["txtOth4"] = formCollection["txtOth5"] = "";
                    //if (searchConditions == null)
                    //{
                    //    searchConditions = new Dictionary<string, string>();
                    //}
                    //searchConditions.Clear();
                    //searchConditions.Add("slBtw", "");
                    //searchConditions.Add("txtCus", "");
                    //searchConditions.Add("txtProdNo", "");
                    //searchConditions.Add("txtSale", "");
                    //searchConditions.Add("txtAssign", "");
                    //searchConditions.Add("txtDep", "");
                    //searchConditions.Add("txtMat", "");
                    //searchConditions.Add("txtMSL", "");
                    //searchConditions.Add("txtCusVer", "");
                    //searchConditions.Add("txtREV", "");
                    //searchConditions.Add("txtOth1", "");
                    //searchConditions.Add("txtOth2", "");
                    //searchConditions.Add("txtOth3", "");
                    //searchConditions.Add("txtOth4", "");
                    //searchConditions.Add("txtOth5", "");
                }
                else
                {
                    ViewBag.js = "<script>alert('刪除失敗');</script>";
                    ViewBag.Fail = "D";
                }
                ViewData["DisplayData"] = Search(labelMaintainLogic, formCollection);
            }
            if (action == "save")
            {
                string msg = "";
                LabelMaintain labelMaintain = new LabelMaintain();
                labelMaintain.Type = formCollection["slBtw"];
                labelMaintain.CustomerCode = formCollection["txtCus"];
                labelMaintain.SPEC = formCollection["txtProdNo"];
                labelMaintain.VKORG = formCollection["txtSale"];
                labelMaintain.VTWEG = formCollection["txtAssign"];
                labelMaintain.SPART = formCollection["txtDep"];
                labelMaintain.LCP = formCollection["txtMat"];
                labelMaintain.MSLLevel = formCollection["txtMSL"];
                labelMaintain.CustomerVer = formCollection["txtCusVer"];
                labelMaintain.Rev = formCollection["txtREV"];
                labelMaintain.Other1 = formCollection["txtOth1"];
                labelMaintain.Other2 = formCollection["txtOth2"];
                labelMaintain.Other3 = formCollection["txtOth3"];
                labelMaintain.Other4 = formCollection["txtOth4"];
                labelMaintain.Other5 = formCollection["txtOth5"];
                if (labelMaintainLogic.SaveLabelMaintain(labelMaintain, formCollection["mode"], out msg))
                {
                    ViewBag.js = "<script>alert('保存成功');</script>";
                    lstLabelSettings = labelSettingLogic.GetAllBtw();
                    selectList = new SelectList(lstLabelSettings, "Type", "Type", "==請選擇==");
                    ViewData["slBtwDisplay"] = selectList;

                    formCollection["slBtw"] =
                    formCollection["txtCus"] =
                        formCollection["txtProdNo"] =
                            formCollection["txtSale"] =
                                formCollection["txtAssign"] =
                                formCollection["txtDep"] =
                                formCollection["txtMat"] =
                                    formCollection["txtMSL"] =
                                        formCollection["txtCusVer"] =
                                            formCollection["txtREV"] =
                                                formCollection["txtOth1"] =
                                                    formCollection["txtOth2"] =
                                                        formCollection["txtOth3"] =
                                                            formCollection["txtOth4"] = formCollection["txtOth5"] = "";
                    //if (searchConditions == null)
                    //{
                    //    searchConditions = new Dictionary<string, string>();
                    //}
                    //searchConditions.Clear();
                    //searchConditions.Add("slBtw", "");
                    //searchConditions.Add("txtCus", "");
                    //searchConditions.Add("txtProdNo", "");
                    //searchConditions.Add("txtSale", "");
                    //searchConditions.Add("txtAssign", "");
                    //searchConditions.Add("txtDep", "");
                    //searchConditions.Add("txtMat", "");
                    //searchConditions.Add("txtMSL", "");
                    //searchConditions.Add("txtCusVer", "");
                    //searchConditions.Add("txtREV", "");
                    //searchConditions.Add("txtOth1", "");
                    //searchConditions.Add("txtOth2", "");
                    //searchConditions.Add("txtOth3", "");
                    //searchConditions.Add("txtOth4", "");
                    //searchConditions.Add("txtOth5", "");
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
                ViewData["DisplayData"] = Search(labelMaintainLogic, formCollection);
            }
            if (formCollection.Count > 0)
            {
                ViewBag.Cus = formCollection["txtCus"];
                ViewBag.ProdNo = formCollection["txtProdNo"];
                ViewBag.Sale = formCollection["txtSale"];
                ViewBag.Assign = formCollection["txtAssign"];
                ViewBag.Dep = formCollection["txtDep"];
                ViewBag.Mat = formCollection["txtMat"];
                ViewBag.MSL = formCollection["txtMSL"];
                ViewBag.CusVer = formCollection["txtCusVer"];
                ViewBag.REV = formCollection["txtREV"];
                ViewBag.Oth1 = formCollection["txtOth1"];
                ViewBag.Oth2 = formCollection["txtOth2"];
                ViewBag.Oth3 = formCollection["txtOth3"];
                ViewBag.Oth4 = formCollection["txtOth4"];
                ViewBag.Oth5 = formCollection["txtOth5"];
                ViewBag.Chk = formCollection["chk"];
            }
            //if (searchConditions!=null&&searchConditions.Count > 0)
            //{
            //    ViewData["DisplayData"]=SearchSort(labelMaintainLogic, searchConditions);
            //}
            //TempData["SearchConditions"] = searchConditions;
            return View();
        }

        private List<LabelMaintain> Search(LabelMaintainLogic labelMaintainLogic, FormCollection formCollection)
        {
            LabelMaintain labelMaintain = new LabelMaintain();
            if (formCollection.Count > 0 && formCollection["slBtw"] != "==請選擇==")
            {
                labelMaintain.Type = formCollection["slBtw"];
            }
            labelMaintain.CustomerCode = formCollection["txtCus"];
            labelMaintain.SPEC = formCollection["txtProdNo"];
            labelMaintain.VKORG = formCollection["txtSale"];
            labelMaintain.VTWEG = formCollection["txtAssign"];
            labelMaintain.SPART = formCollection["txtDep"];
            labelMaintain.LCP = formCollection["txtMat"];
            labelMaintain.MSLLevel = formCollection["txtMSL"];
            labelMaintain.CustomerVer = formCollection["txtCusVer"];
            labelMaintain.Rev = formCollection["txtREV"];
            labelMaintain.Other1 = formCollection["txtOth1"];
            labelMaintain.Other2 = formCollection["txtOth2"];
            labelMaintain.Other3 = formCollection["txtOth3"];
            labelMaintain.Other4 = formCollection["txtOth4"];
            labelMaintain.Other5 = formCollection["txtOth5"];
            return labelMaintainLogic.GetInfo(labelMaintain);
        }

        private List<LabelMaintain> SearchSort(LabelMaintainLogic labelMaintainLogic, IDictionary<string, string> searchConditions)
        {
            LabelMaintain labelMaintain = new LabelMaintain();
            string btw = GetSearchConditionValue(searchConditions, "slBtw");
            if (btw != "==請選擇==")
            {
                labelMaintain.Type = btw;
            }
            labelMaintain.CustomerCode = GetSearchConditionValue(searchConditions, "txtCus");
            labelMaintain.SPEC = GetSearchConditionValue(searchConditions, "txtProdNo");
            labelMaintain.VKORG = GetSearchConditionValue(searchConditions, "txtSale");
            labelMaintain.VTWEG = GetSearchConditionValue(searchConditions, "txtAssign");
            labelMaintain.SPART = GetSearchConditionValue(searchConditions, "txtDep");
            labelMaintain.LCP = GetSearchConditionValue(searchConditions, "txtMat");
            labelMaintain.MSLLevel = GetSearchConditionValue(searchConditions, "txtMSL");
            labelMaintain.CustomerVer = GetSearchConditionValue(searchConditions, "txtCusVer");
            labelMaintain.Rev = GetSearchConditionValue(searchConditions, "txtREV");
            labelMaintain.Other1 = GetSearchConditionValue(searchConditions, "txtOth1");
            labelMaintain.Other2 = GetSearchConditionValue(searchConditions, "txtOth2");
            labelMaintain.Other3 = GetSearchConditionValue(searchConditions, "txtOth3");
            labelMaintain.Other4 = GetSearchConditionValue(searchConditions, "txtOth4");
            labelMaintain.Other5 = GetSearchConditionValue(searchConditions, "txtOth5");
            return labelMaintainLogic.GetInfo(labelMaintain);
        }

        private static string GetSearchConditionValue(IDictionary<string, string> searchConditions, string key)
        {
            string tempValue = string.Empty;

            if (searchConditions != null && searchConditions.Keys.Contains("slBtw"))
            {
                searchConditions.TryGetValue(key, out tempValue);
            }
            return tempValue;
        }
    }
}