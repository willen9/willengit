using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Contract_ProductDLogic
    {
        public IContract_ProductDDAL objDAL = new Contract_ProductDDAL();

        public List<Contract_ProductD> GetContract_ProductD(string ContractType, string ContractNo)
        {
            return objDAL.GetContract_ProductD(ContractType, ContractNo);
        }

        public List<Contract_ProductD> GetContractSerial(Contract_ProductD contract_ProductD, int Page)
        {
            return objDAL.GetContractSerial(contract_ProductD, Page);
        }

        public List<Contract_ProductD> GetContractProduct(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetContractProduct(Col, Condition, conditionValue, Page);
        }
    }
}