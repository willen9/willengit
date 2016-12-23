using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRoutineService_HDAL
    {
        bool AddRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg);

        List<RoutineService_H> SearchRGA_H(string col, string condition, string value, int Page);

        bool UpdateRoutineService_H(RoutineService_H routineService_H, string strPItemId,
            string strPProcessDate, string strPProcessExplanation, string strPProcessMan,
            string strPRemark, string strDItemId, string strDProductNo,
            string strDProductName, string strDQTY, string strDUnit,
            string strDRemark, out string msg);

        bool DelRoutineService_H(RoutineService_H routineService_H, out string msg);

        List<RoutineService_H> SearchRoutineService_H(string col, string condition, string value);

        //List<RoutineService_H> SearchRoutineService_H(string orderType, string orderNo);

        RoutineService_H GetRoutineService_HInfo(RoutineService_H routineService_H);

        bool UpdateAsign(Assignment rssignment, string SupportId);

        bool UpdateAsignAgain(AssignmentChange assignmentChange, string SupportId);
    }
}