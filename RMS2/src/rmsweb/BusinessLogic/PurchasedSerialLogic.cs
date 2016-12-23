using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class PurchasedSerialLogic
    {
        IPurchasedSerialDAL objDAL = new PurchasedSerialDAL();

        public bool AddPurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            return objDAL.AddPurchasedSerial(purchasedSerial, out msg);
        }

        public bool UpdatePurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            return objDAL.UpdatePurchasedSerial(purchasedSerial, out msg);
        }

        public bool DelPurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            return objDAL.DelPurchasedSerial(purchasedSerial, out msg);
        }

        public List<PurchasedSerial> SearchPurchasedSerial(string col, string condition, string value)
        {
            return objDAL.SearchPurchasedSerial(col, condition, value);
        }

        public PurchasedSerial GetPurchasedSerialInfo(PurchasedSerial purchasedSerial)
        {
            return objDAL.GetPurchasedSerialInfo(purchasedSerial);
        }

        public bool ImportFile(string path, PurchasedSerial purchasedSerial)
        {
            return objDAL.ImportFile(path, purchasedSerial); 
        }

        public bool IsSerialNo(string SerialNo)
        {
            return objDAL.IsSerialNo(SerialNo);
        }

        public bool AddWarranty(string WarrantyId, Warranty_D warranty_D, out string msg)
        {
            return objDAL.AddWarranty(WarrantyId, warranty_D,out msg);
        }
    }
}