using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDAL
{
    public interface ILabelMaintain
    {
        DataTable GetInfo(LabelMaintain model);
        bool DelLabelMaintains(List<LabelMaintain> lstDel);
        bool SaveLabelMaintain(LabelMaintain model,string mode,out string msg);
    }
}
