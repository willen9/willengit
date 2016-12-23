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
    public class ProgramLogic
    {
        public IProgram objDal = new ProgramDAL();

        //public List<Program> GetProgram(Program program, int Page)
        //{
        //    return objDal.GetProgram(program, Page);
        //}
        public bool UpdateProgram(Program program)
        {
            return objDal.UpdateProgram(program);
        }
        public Program GetProgram(string ProgId)
        {
            return objDal.GetProgram(ProgId);
        }
        public bool AddProgram(Program program, out string msg)
        {
            return objDal.AddProgram(program, out msg);
        } 

        public DataTable SelectCurrency(Program program, string condition,string condition1)
        {
            return objDal.SelectCurrency(program, condition, condition1);
        }
        public bool DeleteProgram(string SupportAplProgramId)
        {
            return objDal.DeleteProgram(SupportAplProgramId);
        }
        public bool IsModuleId(string ModuleId)
        {
            return objDal.IsModuleId(ModuleId);
        }
        public bool IsProgramId(string ProgramId)
        {
            return objDal.IsProgramId(ProgramId);
        }

        //public string UpdateCurrency(Program program)
        //{
        //    return objDal.UpdateCurrency(program);
        //}

        //public bool DeleteCurrency(Program program)
        //{
        //    return objDal.DeleteCurrency(program);
        //}

        //public string InsertCurrency(Program program)
        //{
        //    return objDal.InsertCurrency(program);
        //}

        public DataTable SearchDetailProgram(string col, string condition, string value)
        {
            return objDal.SearchDetailProgram(col, condition, value);
        }
    }
}