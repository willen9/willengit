using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Department
    {
        public string Company { get; set; }
        public string UserGroup { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Modifier { get; set; }
        public string ModiDate { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Remark { get; set; }
    }
}