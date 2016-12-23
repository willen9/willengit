using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IBOM_DDAL
    {
        List<BOM_D> SearchBOM_D(BOM_D bOM_D, int Page);

        BOM_D SearchBOM_DInfo(string ComponentNo);

        bool IsBOM_D(BOM_D bOM_D);

        List<BOM_D> SearchBOMD(string MajorComponentNo, string Col, string Condition, string conditionValue, int Page);

        List<BOM_D> SearchBOMDInfo(string Col, string Condition, string conditionValue);
        
    }
}