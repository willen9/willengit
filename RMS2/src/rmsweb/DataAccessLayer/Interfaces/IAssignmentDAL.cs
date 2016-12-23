using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IAssignmentDAL
    {
        bool AddAssignment(Assignment assignment, string type, out string msg);
        
        bool UpdateAssignment(Assignment assignment, out string msg);

        bool DelAssignment(Assignment assignment, out string msg);
        bool DelRoutineService(Assignment assignment, out string msg);
        List<Assignment> SearchAssignment(string col, string condition, string value);

        Assignment GetAssignmentInfo(Assignment assignment);

        bool Confirmed(Assignment assignment, string type, out string msg);

        bool AddAssignmentRGA_H(Assignment assignment, out string msg);

        List<Assignment> AssignmentInfo(Assignment assignment);
    }
}