using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISyscodeNumbersDAL
    {
        string CountSysc(string CodeType, string CodeNumber);
    }
}