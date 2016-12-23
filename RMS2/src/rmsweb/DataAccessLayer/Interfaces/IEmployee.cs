using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployee
    {
        DataTable SelectEmployee(Employee employee,string condition);
        bool IsModuleId(string employeeId);
        string UpdateEmployee(Employee employee);

        bool DeleteEmployee(Employee employee);

        string InsertEmployee(Employee employee);

        DataTable SearchDetailEmployee(string col, string condition, string value);

        bool ImportFile(string path);

        string GetEmployeeName(string EmployeeId);

        List<Employee> GetEmployee(string Col, string Condition, string conditionValue, int Page);
    }
}
