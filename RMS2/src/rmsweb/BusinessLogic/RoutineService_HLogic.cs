using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RoutineService_HLogic
    {
        IRoutineService_HDAL objDAL = new RoutineService_HDAL();

        public bool AddRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg)
        {
            return objDAL.AddRoutineService_H( routineService_H,  strPItemId,
             strPProcessDate,  strPProcessExplanation,  strPProcessMan,
             strPRemark,  strDItemId,  strDProductNo,
             strDProductName,  strDQTY,  strDUnit,
             strDRemark, out  msg);
        }

        public bool UpdateRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg)
        {
            return objDAL.UpdateRoutineService_H(routineService_H, strPItemId,
             strPProcessDate, strPProcessExplanation, strPProcessMan,
             strPRemark, strDItemId, strDProductNo,
             strDProductName, strDQTY, strDUnit,
             strDRemark, out  msg);
        }

        public bool DelRoutineService_H(RoutineService_H routineService_H, out string msg)
        {
            return objDAL.DelRoutineService_H(routineService_H, out msg);
        }

        public List<RoutineService_H> SearchRoutineService_H(string col, string condition, string value)
        {
            return objDAL.SearchRoutineService_H(col, condition, value);
        }

        public List<RoutineService_H> SearchRGA_H(string col, string condition, string value, int Page)
        {
            return objDAL.SearchRGA_H(col, condition, value, Page);
        }

        public RoutineService_H GetRoutineService_HInfo(RoutineService_H routineService_H)
        {
            return objDAL.GetRoutineService_HInfo(routineService_H);
        }

        public bool UpdateAsign(Assignment rssignment, string SupportId)
        {
            return objDAL.UpdateAsign(rssignment, SupportId);
        }

        public bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId)
        {
            return objDAL.UpdateAsignAgain(assignmentChange, SupportId);
        }
    }
}