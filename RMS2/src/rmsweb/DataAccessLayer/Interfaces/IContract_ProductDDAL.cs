using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IContract_ProductDDAL
    {
        List<Contract_ProductD> GetContract_ProductD(string ContractType, string ContractNo);

        List<Contract_ProductD> GetContractSerial(Contract_ProductD contract_ProductD, int Page);

        List<Contract_ProductD> GetContractProduct(string Col, string Condition, string conditionValue, int Page);
    }
}