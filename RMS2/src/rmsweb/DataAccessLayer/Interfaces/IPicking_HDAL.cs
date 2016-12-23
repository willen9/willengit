using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPicking_HDAL
    {
        bool AddPicking_H(Picking_H picking_H, string strPicking_ProductD_ProductNo, string strPicking_ProductD_ProductName,
            string strPicking_ProductD_QTY, string strPicking_ProductD_PickingQTY, string strPicking_ProductD_Unit, string strPicking_ProductD_Remark,
            out string msg);

        List<Picking_H> GetPicking_H( string col, string condition, string value);

        bool DeletePicking_H(string PickingOrderType, string PickingOrderNo);

        Picking_H GetPicking_HByNo(string PickingOrderType, string PickingOrderNo);

        List<Picking_H> GetInfo(string SupportAplOrderType, string SupportAplOrderNo);

        bool IssuePicking_H(Picking_H picking_H);

        bool Confirmed(Picking_H picking_H, out string msg);
    }
}