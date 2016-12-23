using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IVendor
    {
        DataTable SelectVendor(Vendor vendor,int Page,string condition1,string condition2);
        bool IsModuleId(string vendorid);
        string UpdateVendor(Vendor vendor, string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark);

        bool DeleteVendor(Vendor vendor);

        string InsertVendor(Vendor vendor, string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark);

        DataTable SearchDetailVendor(string col, string condition, string value);

        string GetVendorName(string VendorId);

        bool ImportFile(string path, string type, out string msg);

        DataTable GetAddressInfo(VendorAddress vendorAddress);

        DataTable GetContactInfo(VendorContact vendorContact);

        bool SaveNewContact(VendorContact vendorContact);

        bool DeleteContact(VendorContact vendorContact);

        bool SaveContact(VendorContact vendorContact);

        bool DeleteAddress(VendorAddress vendorAddress);

        bool SaveAddress(VendorAddress vendorAddress);

        bool SaveNewAddress(VendorAddress vendorAddress);

        DataTable SelectVendorList(string Col, string Condition, string conditionValue, int Page);
    }
}
