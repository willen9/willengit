using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IWarranty_DDAL
    {
        List<Warranty_D> SearchWarranty_D(string ProductNo, string SerialNo);

        List<Warranty_D> SearchSerialNo(string Col, string Condition, string conditionValue, int Page);

        Warranty_D SearchWarranty_DInfo(string ProductNo, string SerialNo, string ChangeDate, string WarrantyType,string TargetId);

        List<Warranty_D> SearchWarranty(string Col, string Condition, string conditionValue);
    }
}