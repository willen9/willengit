using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISaleSerialDAL
    {
        bool AddSaleSerial(SaleSerial saleSerial, out string msg);

        bool UpdateSaleSerial(SaleSerial saleSerial, out string msg);

        bool DelSaleSerial(SaleSerial saleSerial, out string msg);

        List<SaleSerial> SearchSaleSerial(string col, string condition, string value);
        //List<SaleSerial> SearchSaleSerial(string col, string condition, string value, string str);

        SaleSerial GetSaleSerialInfo(SaleSerial saleSerial);

        bool ImportFile(string path, SaleSerial saleSerial);

        bool AddWarranty(string WarrantyId, Warranty_D warranty_D, string SerArrangeOrderType, string RoutineServiceFreq, out string msg);
    }
}