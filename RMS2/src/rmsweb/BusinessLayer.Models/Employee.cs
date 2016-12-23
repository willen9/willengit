using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Employee
    {
        public string Company { get; set; }
        public string UserGroup { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Modifier { get; set; }
        public string ModiDate { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string TelNo { get; set; }
        public string MobilePhone { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Address { get; set; }
    }
}