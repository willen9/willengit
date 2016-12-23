using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SyscodeNumbersLogic
    {
        ISyscodeNumbersDAL objDAL = new SyscodeNumbersDAL();

        public string CountSysc(string CodeType, string CodeNumber)
        {
            return objDAL.CountSysc(CodeType, CodeNumber);
        }
    }
}