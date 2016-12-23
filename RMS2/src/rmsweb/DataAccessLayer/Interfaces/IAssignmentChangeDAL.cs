using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IAssignmentChangeDAL
    {
        bool AddAssignmentChange(AssignmentChange assignmentChange, out string msg);

        bool AddAssignmentServiceArrangeChange(AssignmentChange assignmentChange, out string msg);

        bool UpdateAssignmentChange(AssignmentChange assignmentChange, string type, out string msg);

        bool DelAssignmentChange(AssignmentChange assignmentChange, out string msg);

        List<AssignmentChange> SearchAssignmentChange(string col, string condition, string value);

        List<AssignmentChange> SearchAssignmentChangeRoutineService_H(string col, string condition, string value);

        AssignmentChange GetAssignmentChangeInfo(AssignmentChange assignmentChange);
        
        List<Assignment> GetAssignmentSupportApl( string Col, string Condition, string conditionValue, int Page);

        List<Assignment> GetAssignmentServiceApl(string Col, string Condition, string conditionValue, int Page);
        List<AssignmentChange> SearchAssignmentChangeRGA_H(string col, string condition, string value);

        List<Assignment> GetAssignmentRGA_H(string Col, string Condition, string conditionValue, int Page);

        List<Assignment> GetAssignmentRoutineService_H(Assignment assignment, int Page);

        bool Confirmed(AssignmentChange assignmentChange, string type, out string msg);

        bool DelAssignmentChangeRGA_H(AssignmentChange assignmentChange, out string msg);

        List<AssignmentChange> SearchAssignmentChangeSUPI(string col, string condition, string value);

    }
}