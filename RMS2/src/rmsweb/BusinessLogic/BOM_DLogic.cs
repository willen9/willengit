using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class BOM_DLogic
    {
        IBOM_DDAL objDAL = new BOM_DDAL();

        public List<BOM_D> SearchBOM_D(BOM_D bOM_D, int Page)
        {
            return objDAL.SearchBOM_D(bOM_D,Page);
        }

        public BOM_D SearchBOM_DInfo(string ComponentNo)
        {
            return objDAL.SearchBOM_DInfo(ComponentNo);
        }

        public bool IsBOM_D(BOM_D bOM_D)
        {
            return objDAL.IsBOM_D(bOM_D);
        }

        public List<BOM_D> SearchBOMD(string MajorComponentNo, string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.SearchBOMD(MajorComponentNo, Col, Condition, conditionValue, Page);
        }

        public List<BOM_D> SearchBOMDInfo(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchBOMDInfo(Col, Condition, conditionValue);
        }
    }
}