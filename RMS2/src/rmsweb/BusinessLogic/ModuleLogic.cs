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
    public class ModuleLogic
    {
        public IModule objDal = new ModuleDAL();
        public bool IsModuleId(string ModuleId)
        {
            return objDal.IsModuleId(ModuleId);
        }
        public DataTable SelectModule(Module module, string condition,string condition1)
        {
            return objDal.SelectModule(module, condition, condition1);
        }
        public bool DeleteProgram(string SupportAplProgramId)
        {
            return objDal.DeleteProgram(SupportAplProgramId);
        }

        public bool UpdateModule(Module module)
        {
            return objDal.UpdateModule(module);
        }

        public bool AddModule(Module module, out string msg)
        {
            return objDal.AddModule(module, out msg);
        }

        public DataTable SearchDetailModule(string col, string condition, string value)
        {
            return objDal.SearchDetailModule(col, condition, value);
        }
    }
}