using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDAL
{
    public interface IPrtData
    {
        DataTable GetInfo(PrtData model);
        bool PintRepeat(List<PrtData> lstModels);
        DataTable ExportData(List<PrtData> lstModels);
    }
}
