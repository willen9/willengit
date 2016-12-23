using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Warranty_HLogic
    {
        IWarranty_HDAL objDAL = new Warranty_HDAL();

        public bool AddWarranty_H(Warranty_H warranty_H, string strChangeDate,
            string strChangeOrderType, string strChangeOrderNo, string strChangeOrderItemId,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate,
            string strWarrantyType, string strRemark, out string msg)
        {
            return objDAL.AddWarranty_H(warranty_H,strChangeDate,
             strChangeOrderType,  strChangeOrderNo,  strChangeOrderItemId,
             strTargetId,  strTargetName,
             strWarrantyPeriod,  strWarrantySDate, strWarrantyEDate,
             strWarrantyType,  strRemark, out msg);
        }

        public bool UpdateWarranty_H(Warranty_H warranty_H, string strChangeDate,
            string strChangeOrderType, string strChangeOrderNo, string strChangeOrderItemId,
            string strTargetId, string strTargetName,
            string strWarrantyPeriod, string strWarrantySDate, string strWarrantyEDate,
            string strWarrantyType, string strRemark, out string msg)
        {
            return objDAL.UpdateWarranty_H(warranty_H, strChangeDate,
             strChangeOrderType, strChangeOrderNo, strChangeOrderItemId,
             strTargetId, strTargetName,
             strWarrantyPeriod, strWarrantySDate, strWarrantyEDate,
             strWarrantyType, strRemark, out msg);
        }

        public bool DelWarranty_H(Warranty_H warranty_H, out string msg)
        {
            return objDAL.DelWarranty_H(warranty_H, out msg);
        }

        public List<Warranty_H> SearchWarranty_H(string col, string condition, string value)
        {
            return objDAL.SearchWarranty_H(col, condition, value);
        }

        public Warranty_H GetWarranty_HInfo(Warranty_H warranty_H)
        {
            return objDAL.GetWarranty_HInfo(warranty_H);
        }

        public bool IsSerialNo(string SerialNo)
        {
            return objDAL.IsSerialNo(SerialNo);
        }
    }
}