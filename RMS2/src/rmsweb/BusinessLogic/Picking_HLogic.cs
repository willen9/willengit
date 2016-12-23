using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Picking_HLogic
    {
        IPicking_HDAL objDAL = new Picking_HDAL();

        public bool AddPicking_H(Picking_H picking_H, string strPicking_ProductD_ProductNo, string strPicking_ProductD_ProductName,
            string strPicking_ProductD_QTY, string strPicking_ProductD_PickingQTY, string strPicking_ProductD_Unit, string strPicking_ProductD_Remark,
            out string msg)
        {
            return objDAL.AddPicking_H(picking_H, strPicking_ProductD_ProductNo, strPicking_ProductD_ProductName,
            strPicking_ProductD_QTY, strPicking_ProductD_PickingQTY, strPicking_ProductD_Unit, strPicking_ProductD_Remark,
            out msg);
        }

        public List<Picking_H> GetPicking_H(string col, string condition, string value)
        {
            return objDAL.GetPicking_H(col, condition, value);
        }

        public bool DeletePicking_H(string PickingOrderType, string PickingOrderNo)
        {
            return objDAL.DeletePicking_H(PickingOrderType, PickingOrderNo);
        }

        public Picking_H GetPicking_HByNo(string PickingOrderType, string PickingOrderNo)
        {
            return objDAL.GetPicking_HByNo(PickingOrderType,PickingOrderNo);
        }

        public List<Picking_H> GetInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.GetInfo(SupportAplOrderType,SupportAplOrderNo);
        }

        public bool IssuePicking_H(Picking_H picking_H)
        {
            return objDAL.IssuePicking_H(picking_H);
        }

        public bool Confirmed(Picking_H picking_H, out string msg)
        {
            return objDAL.Confirmed(picking_H, out msg);
        }
    }
}