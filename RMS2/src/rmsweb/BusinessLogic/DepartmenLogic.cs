using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace BusinessLogic
{
    public class DepartmenLogic
    {
        public IDepartmentDAL objDal = new DepartmentDAL();

        public DataTable SelectDepartment(Department department,string condition)
        {
            return objDal.SelectDepartment(department,condition);
        }
        public bool IsModuleId(string deptid)
        {
            return objDal.IsModuleId(deptid);
        }

        public string UpdateDepartment(Department department)
        {
            return objDal.UpdateDepartment(department);
        }

        public bool DeleteDepartment(Department department)
        {
            return objDal.DeleteDepartment(department);
        }

        public string InsertDepartment(Department department)
        {
            return objDal.InsertDepartment(department);
        }

        public DataTable SearchDetailDepartment(string col, string condition, string value)
        {
            return objDal.SearchDetailDepartment(col,condition,value);
        }

        public List<Department> GetDepartment(Department department, string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.GetDepartment(department, Col, Condition, conditionValue, Page);
        }
    }
}