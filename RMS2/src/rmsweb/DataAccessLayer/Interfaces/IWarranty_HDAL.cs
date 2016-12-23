using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IWarranty_HDAL
    {
        bool AddWarranty_H(Warranty_H warranty_H, string strChangeDate,
            string strChangeOrderType, string strChangeOrderNo, string strChangeOrderItemId,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate,
            string strWarrantyType, string strRemark, out string msg);

        bool UpdateWarranty_H(Warranty_H warranty_H, string strChangeDate,
            string strChangeOrderType, string strChangeOrderNo, string strChangeOrderItemId,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate,
            string strWarrantyType, string strRemark, out string msg);

        bool DelWarranty_H(Warranty_H warranty_H, out string msg);

        List<Warranty_H> SearchWarranty_H(string col, string condition, string value);

        Warranty_H GetWarranty_HInfo(Warranty_H warranty_H);

        bool IsSerialNo(string SerialNo);
    }
}