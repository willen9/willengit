using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ContractChange_ProductSerialLogic
    {
        IContractChange_ProductSerialDAL objDAL = new ContractChange_ProductSerialDAL();

        public List<ContractChange_ProductSerial> GetContractChange_ProductSerial(string ContractType, string ContractNo)
        {
            return objDAL.GetContractChange_ProductSerial(ContractType, ContractNo);
        }
    }
}