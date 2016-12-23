using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class AssignmentChangeLogic
    {
        IAssignmentChangeDAL objDAL = new AssignmentChangeDAL();

        public bool AddAssignmentChange(AssignmentChange assignmentChange, out string msg)
        {
            return objDAL.AddAssignmentChange(assignmentChange, out msg);
        }

        public bool AddAssignmentServiceArrangeChange(AssignmentChange assignmentChange, out string msg)
        {
            return objDAL.AddAssignmentServiceArrangeChange(assignmentChange, out msg);
        }

        public bool UpdateAssignmentChange(AssignmentChange assignmentChange, string type, out string msg)
        {
            return objDAL.UpdateAssignmentChange(assignmentChange,type, out msg);
        }

        public bool DelAssignmentChange(AssignmentChange assignmentChange, out string msg)
        {
            return objDAL.DelAssignmentChange(assignmentChange, out msg);
        }

        public List<AssignmentChange> SearchAssignmentChangeSUPI(string col, string condition, string value)
        {
            return objDAL.SearchAssignmentChangeSUPI(col, condition, value);
        }

        public List<AssignmentChange> SearchAssignmentChange(string col, string condition, string value)
        {
            return objDAL.SearchAssignmentChange(col, condition, value);
        }

        public List<AssignmentChange> SearchAssignmentChangeRoutineService_H(string col, string condition, string value)
        {
            return objDAL.SearchAssignmentChangeRoutineService_H(col, condition, value);
        }

        public AssignmentChange GetAssignmentChangeInfo(AssignmentChange assignmentChange)
        {
            return objDAL.GetAssignmentChangeInfo(assignmentChange);
        }
        
        public List<Assignment> GetAssignmentSupportApl(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetAssignmentSupportApl(Col, Condition, conditionValue, Page);
        }

        public List<Assignment> GetAssignmentServiceApl(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetAssignmentServiceApl(Col, Condition, conditionValue, Page);
        }

        public List<Assignment> GetAssignmentRoutineService_H(Assignment assignment, int Page)
        {
            return objDAL.GetAssignmentRoutineService_H(assignment, Page);
        }

        public List<Assignment> GetAssignmentRGA_H(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetAssignmentRGA_H(Col, Condition, conditionValue, Page);
        }

        public bool Confirmed(AssignmentChange assignmentChange, string type, out string msg)
        {
            return objDAL.Confirmed(assignmentChange, type, out msg);
        }

        public List<AssignmentChange> SearchAssignmentChangeRGA_H(string col, string condition, string value)
        {
            return objDAL.SearchAssignmentChangeRGA_H(col, condition, value);
        }

        public bool DelAssignmentChangeRGA_H(AssignmentChange assignmentChange, out string msg)
        {
            return objDAL.DelAssignmentChangeRGA_H(assignmentChange, out msg);
        }
    }
}