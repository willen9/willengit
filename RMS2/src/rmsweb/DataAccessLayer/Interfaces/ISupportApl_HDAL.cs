using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISupportApl_HDAL
    {
        List<SupportApl_H> GetSupportItem(SupportApl_H supportApl_H, string col, string condition, string value);

        string GetSupportAplOrderNo(string OrderType);

        bool AddSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname,
            string strremark, string strproductno, string strproductname, string strqty,
            string strunit, string strproductdremark,string strProcessDate, string strProcessExplanation, string strRemark, out string msg);

        bool DeleteSupportApl_H(string SupportAplOrderType, string SupportAplOrderNo);

        SupportApl_H SupportItemInfo(string SupportAplOrderType, string SupportAplOrderNo);

        bool UpdateAsign(SupportAplAsign supportAplAsign, string SupportId, out string msg);

        bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId, out string msg);

        List<SupportApl_H> GetSupportInfo( string Col, string Condition, string conditionValue, int Page);

        List<SupportApl_H> GetSupportItemPick(string col, string condition, string value);

        void UpdateConfirmedY(SupportApl_H supportApl_H, string type);

        //void UpdateConfirmedV(SupportApl_H supportApl_H);

        string countOrderStatus(SupportApl_H supportApl_H, string col, string condition, string value,string OrderStatus);

        List<SupportApl_H> GetSupportItemProcessD(string col, string condition, string value);

        void PrintSupprotApl_H(string SupportAplOrderType, string SupportAplOrderNo);

        bool UpdateSupportApl_H(SupportApl_H supportApl_H, string strsupportitemid, string strsupportitemname,
            string strremark, string strproductno, string strproductname, string strqty,
            string strunit, string strproductdremark, string strProcessDate, string strProcessExplanation, string strRemark, out string msg);
    }
}