using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Contract_HLogic
    {
        public IContract_HDAL objDal = new Contract_HDAL();

        public List<Contract_H> GetContract_H(string col, string condition, string value)
        {
            return objDal.GetContract_H(col, condition, value);
        }

        public List<Contract_H> GetContractH(Contract_H contract_H, string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.GetContractH(contract_H, Col, Condition, conditionValue, Page);
        }

        public bool AddContract_H(Contract_H contract_H,string strProductNo,string strProductName,
            string strQTY,string strUnit,string strWarrantyPeriod,string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark, string strProductSerial, out string msg)
        {
            return objDal.AddContract_H(contract_H, strProductNo, strProductName,
             strQTY, strUnit, strWarrantyPeriod, strWarrantySDate,
             strWarrantyEDate, strRoutineServiceFreq, strRemark, strProductSerial, out msg);
        }

        public Contract_H Contract_HInfo(string ContractType, string ContractNo)
        {
            return objDal.Contract_HInfo(ContractType, ContractNo);
        }

        public bool DelContract_H(string ContractType, string ContractNo)
        {
            return objDal.DelContract_H(ContractType, ContractNo);
        }

        public bool UpdateContract_H(Contract_H contract_H, string strProductNo, string strProductName,
            string strQTY, string strUnit, string strWarrantyPeriod, string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark, string strProductSerial, out string msg)
        {
            return objDal.UpdateContract_H(contract_H,  strProductNo,  strProductName,
             strQTY,  strUnit,  strWarrantyPeriod,  strWarrantySDate,
             strWarrantyEDate,  strRoutineServiceFreq,  strRemark,strProductSerial, out  msg);
        }

        public bool TypeContract_H(Contract_H contract_H,out string msg)
        {
            return objDal.TypeContract_H(contract_H,out msg);
        }

        public bool AddServiceArrange(ServiceArrange_H serviceArrange_H, string strchk, out string msg)
        {
            return objDal.AddServiceArrange(serviceArrange_H, strchk, out msg);
        }
    }
}