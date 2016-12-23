using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPurchasedSerialDAL
    {
        bool AddPurchasedSerial(PurchasedSerial purchasedSerial, out string msg);

        bool UpdatePurchasedSerial(PurchasedSerial purchasedSerial, out string msg);

        bool DelPurchasedSerial(PurchasedSerial purchasedSerial, out string msg);

        List<PurchasedSerial> SearchPurchasedSerial(string col, string condition, string value);

        PurchasedSerial GetPurchasedSerialInfo(PurchasedSerial purchasedSerial);

        bool ImportFile(string path, PurchasedSerial purchasedSerial);

        bool IsSerialNo(string SerialNo);

        bool AddWarranty(string WarrantyId, Warranty_D warranty_D, out string msg);
    }
}