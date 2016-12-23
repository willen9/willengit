using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IProgram
    {
        DataTable SelectCurrency(Program Program, string condition,string condition1);

        //List<Program> GetProgram(Program program, int Page);

        bool UpdateProgram(Program Program);

        bool AddProgram(Program program, out string msg);

        bool DeleteProgram(string SupportAplProgramId);
        
        bool IsProgramId(string ProgramId);

        bool IsModuleId(string ModuleId);
        //string UpdateCurrency(Program program);
        //bool DeleteCurrency(Program program);
        Program GetProgram(string ProgId);
        //string InsertCurrency(Program program);

        DataTable SearchDetailProgram(string col, string condition, string value);
    }
}
