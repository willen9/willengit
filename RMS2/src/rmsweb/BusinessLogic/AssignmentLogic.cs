using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class AssignmentLogic
    {
        IAssignmentDAL objDAL = new AssignmentDAL();

        public bool AddAssignment(Assignment assignment,string type, out string msg)
        {
            return objDAL.AddAssignment(assignment,type, out msg);
        }

        public bool UpdateAssignment(Assignment assignment, out string msg)
        {
            return objDAL.UpdateAssignment(assignment, out msg);
        }
        public bool DelAssignment(Assignment assignment, out string msg)
        {
            return objDAL.DelAssignment(assignment, out msg);
        }
        public bool DelRoutineService(Assignment assignment, out string msg)
        {
            return objDAL.DelRoutineService(assignment, out msg);
        }

        public List<Assignment> SearchAssignment(string col, string condition, string value)
        {
            return objDAL.SearchAssignment(col, condition, value);
        }

        public Assignment GetAssignmentInfo(Assignment assignment)
        {
            return objDAL.GetAssignmentInfo(assignment);
        }

        public bool Confirmed(Assignment assignment, string type, out string msg)
        {
            return objDAL.Confirmed(assignment, type, out msg);
        }

        public bool AddAssignmentRGA_H(Assignment assignment, out string msg)
        {
            return objDAL.AddAssignmentRGA_H(assignment, out msg);
        }

        public List<Assignment> AssignmentInfo(Assignment assignment)
        {
            return objDAL.AssignmentInfo(assignment);
        }
    }
}