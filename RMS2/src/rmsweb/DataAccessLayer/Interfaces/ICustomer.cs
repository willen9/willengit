using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomer
    {
        DataTable SelectCustomer(Customer customer,string condition1,string condition2);
        bool IsModuleId(string customerid);
        string UpdateCustomer(Customer customer,string AddressId,string AddressName,string Address,string Contact,string TelNo,string FaxNo,string Remark);

        bool DeleteCustomer(Customer customer);

        string InsertCustomer(Customer customer,string AddressId,string AddressName,string Address,string Contact,string TelNo,string FaxNo,string Remark);

        DataTable SearchDetailCustomer(string col, string condition, string value);

        bool ImportFile(string path,string type,string customerId,out string msg);

        DataTable GetAddressInfo(CustomerAddress customerAddress);

        DataTable GetContactInfo(CustomerContact customerContact);

        bool DeleteContact(CustomerContact customerContact);

        bool SaveContact(CustomerContact customerContact);

        bool SaveNewContact(CustomerContact customerContact);

        string GetContactInfo(string customerId,string contact);

        bool SaveAddress(CustomerAddress customerAddress);

        bool SaveNewAddress(CustomerAddress customerAddress);

        bool DeleteAddress(CustomerAddress customerAddress);

        List<Customer> GetCustomer(string Col, string Condition, string conditionValue, int Page);

        Customer GetCustomerInfo(string CustomerId);
    }
}
