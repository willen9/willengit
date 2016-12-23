using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IContract_HDAL
    {
        List<Contract_H> GetContract_H(string col, string condition, string value);

        List<Contract_H> GetContractH(Contract_H contract_H, string Col, string Condition, string conditionValue, int Page);

        bool AddContract_H(Contract_H contract_H, string strProductNo, string strProductName,
            string strQTY, string strUnit, string strWarrantyPeriod, string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark, string strProductSerial, out string msg);

        Contract_H Contract_HInfo(string ContractType, string ContractNo);

        bool DelContract_H(string ContractType, string ContractNo);

        bool UpdateContract_H(Contract_H contract_H, string strProductNo, string strProductName,
            string strQTY, string strUnit, string strWarrantyPeriod, string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark, string strProductSerial, out string msg);

        bool TypeContract_H(Contract_H contract_H, out string msg);

        bool AddServiceArrange(ServiceArrange_H serviceArrange_H, string strchk, out string msg);
    }
}