using System.Data;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class CustomerLogic
    {
        public ICustomer objDal = new CustomerDAL();

        public DataTable SelectCustomer(Customer customer,string condition1,string condition2)
        {
            return objDal.SelectCustomer(customer,condition1,condition2);
        }
        public bool IsModuleId(string customerid)
        {
            return objDal.IsModuleId(customerid);
        }
        public string UpdateCustomer(Customer customer,string AddressId,string AddressName,string Address,string Contact,string TelNo,string FaxNo,string Remark)
        {
            return objDal.UpdateCustomer(customer, AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
        }

        public bool DeleteCustomer(Customer customer)
        {
            return objDal.DeleteCustomer(customer);
        }

        public string InsertCustomer(Customer customer,string AddressId,string AddressName,string Address,string Contact,string TelNo,string FaxNo,string Remark)
        {
            return objDal.InsertCustomer(customer,AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
        }

        public DataTable SearchDetailCustomer(string col, string condition, string value)
        {
            return objDal.SearchDetailCustomer(col, condition, value);
        }

        public bool ImportFile(string path, string type, string customerId, out string msg)
        {
            return objDal.ImportFile(path,type,customerId,out msg);
        }

        public DataTable GetAddressInfo(CustomerAddress customerAddress)
        {
            return objDal.GetAddressInfo(customerAddress);
        }

        public DataTable GetContactInfo(CustomerContact customerContact)
        {
            return objDal.GetContactInfo(customerContact);
        }

        public bool DeleteContact(CustomerContact customerContact)
        {
            return objDal.DeleteContact(customerContact);
        }

        public bool SaveContact(CustomerContact customerContact)
        {
            return objDal.SaveContact(customerContact);
        }

        public bool SaveNewContact(CustomerContact customerContact)
        {
            return objDal.SaveNewContact(customerContact);
        }

        public string GetContactInfo(string customerId,string contact)
        {
            return objDal.GetContactInfo(customerId, contact);
        }

        public bool SaveAddress(CustomerAddress customerAddress)
        {
            return objDal.SaveAddress(customerAddress);
        }

        public bool SaveNewAddress(CustomerAddress customerAddress)
        {
            return objDal.SaveNewAddress(customerAddress);
        }

        public bool DeleteAddress(CustomerAddress customerAddress)
        {
            return objDal.DeleteAddress(customerAddress);
        }

        public List<Customer> GetCustomer(string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.GetCustomer(Col, Condition, conditionValue, Page);
        }

        public Customer GetCustomerInfo(string CustomerId)
        {
            return objDal.GetCustomerInfo(CustomerId);
        }
    }
}