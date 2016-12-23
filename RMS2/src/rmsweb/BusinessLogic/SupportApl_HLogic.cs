using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SupportApl_HLogic
    {
        ISupportApl_HDAL objDAL = new SupportApl_HDAL();

        public List<SupportApl_H> GetSupportItem(SupportApl_H supportApl_H, string col, string condition, string value)
        {
            return objDAL.GetSupportItem(supportApl_H,col, condition, value);
        }

        public string GetSupportAplOrderNo(string OrderType)
        {
            return objDAL.GetSupportAplOrderNo(OrderType);
        }

        public bool AddSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname, 
            string strremark, string strproductno, string strproductname, string strqty, 
            string strunit, string strproductdremark,string strProcessDate, string strProcessExplanation, string strRemark, out string msg)
        {
            return objDAL.AddSupportApl_H(supportApl_H, strsupportitemid, strsupportitemname,
                 strremark, strproductno, strproductname, strqty,
                 strunit, strproductdremark,strProcessDate,  strProcessExplanation,  strRemark, out msg);
        }

        public bool DeleteSupportApl_H(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.DeleteSupportApl_H(SupportAplOrderType, SupportAplOrderNo);
        }

        public SupportApl_H SupportItemInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            return objDAL.SupportItemInfo(SupportAplOrderType, SupportAplOrderNo);
        }

        public bool UpdateAsign(SupportAplAsign supportAplAsign, string SupportId, out string msg)
        {
            return objDAL.UpdateAsign(supportAplAsign, SupportId, out  msg);
        }

        public bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId, out string msg)
        {
            return objDAL.UpdateAsignAgain(assignmentChange, SupportId, out  msg);
        }

        public List<SupportApl_H> GetSupportInfo(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetSupportInfo(Col, Condition, conditionValue, Page);
        }

        public List<SupportApl_H> GetSupportItemPick(string col, string condition, string value)
        {
            return objDAL.GetSupportItemPick(col, condition, value);
        }

        public void UpdateConfirmedY(SupportApl_H supportApl_H, string type)
        {
            objDAL.UpdateConfirmedY(supportApl_H, type);
        }

        //public void UpdateConfirmedV(SupportApl_H supportApl_H)
        //{
        //    objDAL.UpdateConfirmedV(supportApl_H);
        //}

        public string countOrderStatus(SupportApl_H supportApl_H, string col, string condition, string value, string OrderStatus)
        {
            return objDAL.countOrderStatus(supportApl_H, col, condition, value,OrderStatus);
        }

        public List<SupportApl_H> GetSupportItemProcessD(string col, string condition, string value)
        {
            return objDAL.GetSupportItemProcessD(col, condition, value);
        }

        public void PrintSupprotApl_H(string SupportAplOrderType, string SupportAplOrderNo)
        {
            objDAL.PrintSupprotApl_H(SupportAplOrderType, SupportAplOrderNo);
        }

        public bool UpdateSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname,
            string strremark, string strproductno, string strproductname, string strqty,
            string strunit, string strproductdremark, string strProcessDate, string strProcessExplanation, string strRemark, out string msg)
        {
            return objDAL.UpdateSupportApl_H(supportApl_H, strsupportitemid, strsupportitemname,
                 strremark, strproductno, strproductname, strqty,
                 strunit, strproductdremark, strProcessDate, strProcessExplanation, strRemark, out msg);
        }

    }
}