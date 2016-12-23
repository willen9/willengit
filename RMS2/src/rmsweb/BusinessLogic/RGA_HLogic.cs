using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RGA_HLogic
    {
        IRGA_HDAL objDAL = new RGA_HDAL();

        public bool AddRGA_H(RGA_H rGA_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            return objDAL.AddRGA_H(rGA_H,
             strQQuestionNo,  strQQuestion,
             strQDescription,  strDItemId,
             strDPartNo,  strDPartName,
             strDQTY,  strDUnit,  strDRemark,
             strDRepaired,  strDSourceOrderType,
             strDSourceOrderNo,  strDSourceItemId, out msg);
        }

        public bool UpdateRGA_H(RGA_H rGA_H, 
            string strQQuestionNo, string strQQuestion,
            string strQDescription, string strDItemId,
            string strDPartNo, string strDPartName,
            string strDQTY, string strDUnit, string strDRemark,
            string strDRepaired, string strDSourceOrderType,
            string strDSourceOrderNo, string strDSourceItemId, out string msg)
        {
            return objDAL.UpdateRGA_H(rGA_H,
             strQQuestionNo, strQQuestion,
             strQDescription, strDItemId,
             strDPartNo, strDPartName,
             strDQTY, strDUnit, strDRemark,
             strDRepaired, strDSourceOrderType,
             strDSourceOrderNo, strDSourceItemId, out msg);
        }

        public bool DelRGA_H(RGA_H rGA_H, out string msg)
        {
            return objDAL.DelRGA_H(rGA_H, out msg);
        }

        public List<RGA_H> SearchRGA_H(string col, string condition, string value)
        {
            return objDAL.SearchRGA_H(col, condition, value);
        }

        public List<RGA_H> SearchRGA(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.SearchRGA(Col,  Condition,  conditionValue, Page);
        }

        public RGA_H GetRGA_HInfo(RGA_H rGA_H)
        {
            return objDAL.GetRGA_HInfo(rGA_H);
        }

        public bool IsRGA_H(RGA_H rGA_H)
        {
            return objDAL.IsRGA_H(rGA_H);
        }

        public bool ConfirmedRGA_H(RGA_H rGA_H, out string msg)
        {
            return objDAL.ConfirmedRGA_H(rGA_H, out msg);
        }

        public bool UpdateAsign(Assignment assignment, string SupportId)
        {
            return objDAL.UpdateAsign(assignment, SupportId);
        }

        public bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId)
        {
            return objDAL.UpdateAsignAgain(assignmentChange, SupportId);
        }

        public List<RGA_H> SearchRGAInBom(RGA_H rGA_H)
        {
            return objDAL.SearchRGAInBom(rGA_H);
        }

        public List<RGA_H> SearchPartLifetime()
        {
            return objDAL.SearchPartLifetime();
        }

        public List<RGA_H> Search_Contract()
        {
            return objDAL.Search_Contract();
        }

        public List<RGA_H> Search_ContractCover()
        {
            return objDAL.Search_ContractCover();
        }

        public List<RGA_H> SearchPartLifetimeCover()
        {
            return objDAL.SearchPartLifetimeCover();
        }

        public string SearchPartSum(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartSum(Col, Condition, conditionValue);
        }

        public string SearchPartW(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartW(Col, Condition, conditionValue);
        }

        public string SearchPartC(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartC(Col, Condition, conditionValue);
        }

        public string Search_ContractSum(string Col, string Condition, string conditionValue)
        {
            return objDAL.Search_ContractSum(Col, Condition, conditionValue);
        }

        public List<RGA_H> SearchPartLifetimeAerry()
        {
            return objDAL.SearchPartLifetimeAerry();
        }

        public List<RGA_H> SearchRGAList_H()
        {
            return objDAL.SearchRGAList_H();
        }

        public List<RGA_H> SearchRGAList_H(string col, string condition, string value)
        {
            return objDAL.SearchRGAList_H(col, condition, value);
        }

        public List<ProductLifetimeRecord> SearchRGA(string productno, string closed, string CustomerId)
        {
            return objDAL.SearchRGA(productno,  closed,  CustomerId);
        }

        public List<RGA_H> AvgRGA_H(string Col, string Condition, string conditionValue)
        {
            return objDAL.AvgRGA_H(Col, Condition, conditionValue);
        }

        public List<Warranty_D> SearchPartSumList(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartSumList(Col, Condition, conditionValue);
        }
        
        public List<RGA_H> SearchPartWList(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartWList(Col, Condition, conditionValue);
        }
        
        public List<RGA_H> SearchPartCList(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchPartCList(Col, Condition, conditionValue);
        }
        
        public List<RGA_H> RGA_H(string Col, string Condition, string conditionValue)
        {
            return objDAL.RGA_H(Col, Condition, conditionValue);
        }
    }
}