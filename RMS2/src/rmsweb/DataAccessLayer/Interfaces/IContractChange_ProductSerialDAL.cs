using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IContractChange_ProductSerialDAL
    {
        List<ContractChange_ProductSerial> GetContractChange_ProductSerial(string ContractType, string ContractNo);
    }
}