using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRGA_HDAL
    {
        bool AddRGA_H(RGA_H rGA_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription,string strDItemId,
            string strDPartNo,string strDPartName,
            string strDQTY,string strDUnit,string strDRemark,
            string strDRepaired,string strDSourceOrderType,
            string strDSourceOrderNo,string strDSourceItemId, out string msg);

        bool UpdateRGA_H(RGA_H rGA_H,
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg);

        bool DelRGA_H(RGA_H rGA_H, out string msg);

        List<RGA_H> SearchRGA_H(string col, string condition, string value);

        List<RGA_H> SearchRGA(string Col, string Condition, string conditionValue ,int Page);

        RGA_H GetRGA_HInfo(RGA_H rGA_H);

        bool IsRGA_H(RGA_H rGA_H);

        bool ConfirmedRGA_H(RGA_H rGA_H, out string msg);

        bool UpdateAsign(Assignment assignment, string SupportId);

        bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId);

        List<RGA_H> SearchRGAInBom(RGA_H rGA_H);
        
        List<RGA_H> SearchPartLifetime();

        List<RGA_H> SearchPartLifetimeAerry();

        List<RGA_H> SearchPartLifetimeCover();

        List<RGA_H> Search_ContractCover();

        List<RGA_H> Search_Contract();

        string SearchPartSum(string Col, string Condition, string conditionValue);

        List<Warranty_D> SearchPartSumList(string Col, string Condition, string conditionValue);

        string SearchPartW(string Col, string Condition, string conditionValue);

        List<RGA_H> SearchPartWList(string Col, string Condition, string conditionValue);

        string SearchPartC(string Col, string Condition, string conditionValue);

        List<RGA_H> SearchPartCList(string Col, string Condition, string conditionValue);

        string Search_ContractSum(string Col, string Condition, string conditionValue);

        List<RGA_H> SearchRGAList_H();

        List<RGA_H> SearchRGAList_H(string col, string condition, string value);

        List<ProductLifetimeRecord> SearchRGA(string productno, string closed, string CustomerId);

        List<RGA_H> AvgRGA_H(string Col, string Condition, string conditionValue);

        List<RGA_H> RGA_H(string Col, string Condition, string conditionValue);
    }
}