using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IModule
    {
        DataTable SelectModule(Module module, string condition,string condition1);

        bool DeleteProgram(string SupportAplProgramId);

        bool AddModule(Module module, out string msg);

        bool UpdateModule(Module module);

        bool IsModuleId(string ModuleId);
        //string UpdateCurrency(Program program);

        //bool DeleteCurrency(Program program);

        //string InsertCurrency(Program program);

        DataTable SearchDetailModule(string col, string condition, string value);
    }
}
