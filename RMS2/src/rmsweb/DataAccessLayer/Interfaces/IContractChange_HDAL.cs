using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IContractChange_HDAL
    {
        bool AddContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg);

        bool UpdateContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg);

        List<ContractChange_H> GetContractChange_H(string col, string condition, string value);

        bool DelContractChange_H(string ContractType, string ContractNo, string Version);

        ContractChange_H ContractChange_HInfo(string ContractType, string ContractNo, string Version);

        bool TypeContractChange_H(ContractChange_H contractChange_H, out string msg);

        bool IsContractChange_H(ContractChange_H contractChange_H);
    }
}