using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDAL
{
    public interface ILabelSetting
    {
        DataTable GetAllBtw();
        DataTable GetInfo(LabelSetting model);
        bool DelLabelSettings(List<LabelSetting> lstDel);
        bool SaveLabelSetting(LabelSetting modelOld,LabelSetting model, string mode, out string msg);
    }
}
