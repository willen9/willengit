using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IDepartmentDAL
    {
        DataTable SelectDepartment(Department department, string condition);
        bool IsModuleId(string deptid);
        string UpdateDepartment(Department department);

        bool DeleteDepartment(Department department);

        string InsertDepartment(Department department);

        DataTable SearchDetailDepartment(string col,string condition,string value);

        List<Department> GetDepartment(Department department, string Col, string Condition, string conditionValue, int Page);
    }
}
