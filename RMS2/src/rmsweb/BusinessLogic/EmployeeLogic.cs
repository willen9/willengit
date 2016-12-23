using System.Data;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class EmployeeLogic
    {
        public IEmployee objDal = new EmployeeDAL();

        public DataTable SelectEmployee(Employee employee,string condition)
        {
            return objDal.SelectEmployee(employee,condition);
        }

        public bool IsModuleId(string employeeId)
        {
            return objDal.IsModuleId(employeeId);
        }

        public string UpdateEmployee(Employee employee)
        {
            return objDal.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(Employee employee)
        {
            return objDal.DeleteEmployee(employee);
        }

        public string InsertEmployee(Employee employee)
        {
            return objDal.InsertEmployee(employee);
        }

        public DataTable SearchDetailEmployee(string col, string condition, string value)
        {
            return objDal.SearchDetailEmployee(col, condition, value);
        }

        public bool ImportFile(string path)
        {
            return objDal.ImportFile(path);
        }

        public string GetEmployeeName(string EmployeeId)
        {
            return objDal.GetEmployeeName(EmployeeId);
        }

        public List<Employee> GetEmployee(string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.GetEmployee( Col, Condition, conditionValue, Page);
        }
    }
}