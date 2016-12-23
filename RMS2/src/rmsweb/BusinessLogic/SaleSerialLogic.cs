using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class SaleSerialLogic
    {
        ISaleSerialDAL objDAL = new SaleSerialDAL();

        public bool AddSaleSerial(SaleSerial saleSerial, out string msg)
        {
            return objDAL.AddSaleSerial(saleSerial, out msg);
        }

        public bool UpdateSaleSerial(SaleSerial saleSerial, out string msg)
        {
            return objDAL.UpdateSaleSerial(saleSerial,out msg);
        }

        public bool DelSaleSerial(SaleSerial saleSerial, out string msg)
        {
            return objDAL.DelSaleSerial(saleSerial, out msg);
        }

        public List<SaleSerial> SearchSaleSerial(string col, string condition, string value)
        {
            return objDAL.SearchSaleSerial(col, condition, value);
        }
        //public List<SaleSerial> SearchSaleSerial(string col, string condition, string value, string str)
        //{
        //    return objDAL.SearchSaleSerial(col, condition, value, str);
        //}
        public SaleSerial GetSaleSerialInfo(SaleSerial saleSerial)
        {
            return objDAL.GetSaleSerialInfo(saleSerial);
        }

        public bool ImportFile(string path, SaleSerial saleSerial)
        {
            return objDAL.ImportFile(path, saleSerial); 
        }

        public bool AddWarranty(string WarrantyId, Warranty_D warranty_D, string SerArrangeOrderType, string RoutineServiceFreq,out string msg)
        {
            return objDAL.AddWarranty(WarrantyId, warranty_D, SerArrangeOrderType, RoutineServiceFreq,out msg);
        }
    }
}