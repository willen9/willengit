using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Warranty_DLogic
    {
        IWarranty_DDAL objDAL = new Warranty_DDAL();

        public List<Warranty_D> SearchWarranty_D(string ProductNo, string SerialNo)
        {
            return objDAL.SearchWarranty_D(ProductNo, SerialNo);
        }

        public List<Warranty_D> SearchSerialNo(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.SearchSerialNo(Col,  Condition,  conditionValue, Page);
        }

        public Warranty_D SearchWarranty_DInfo(string ProductNo, string SerialNo, string ChangeDate, string WarrantyType,string TargetId)
        {
            return objDAL.SearchWarranty_DInfo(ProductNo, SerialNo, ChangeDate, WarrantyType, TargetId);
        }

        public List<Warranty_D> SearchWarranty(string Col, string Condition, string conditionValue)
        {
            return objDAL.SearchWarranty(Col, Condition, conditionValue);
        }
    }
}