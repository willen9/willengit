using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ContractChange_HLogic
    {
        IContractChange_HDAL objDal = new ContractChange_HDAL();

         public bool AddContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg)
        {
             return objDal.AddContractChange_H(contractChange_H,  strItemId,  strProductNo,
             strSerialNo,  strNewIsClosed, out  msg);
        }

         public List<ContractChange_H> GetContractChange_H(string col, string condition, string value)
         {
             return objDal.GetContractChange_H(col, condition, value);
         }

        public bool UpdateContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg)
         {
             return objDal.UpdateContractChange_H(contractChange_H, strItemId, strProductNo,
              strSerialNo, strNewIsClosed, out  msg);
         }

        public bool DelContractChange_H(string ContractType, string ContractNo, string Version)
        {
            return objDal.DelContractChange_H(ContractType, ContractNo, Version);
        }

        public ContractChange_H ContractChange_HInfo(string ContractType, string ContractNo, string Version)
        {
            return objDal.ContractChange_HInfo(ContractType, ContractNo, Version);
        }

        public bool TypeContractChange_H(ContractChange_H contractChange_H, out string msg)
        {
            return objDal.TypeContractChange_H(contractChange_H, out msg);
        }

        public bool IsContractChange_H(ContractChange_H contractChange_H)
        {
            return objDal.IsContractChange_H(contractChange_H);
        }
    }
}