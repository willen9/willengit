using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class Substitutes_HLogic
    {
        ISubstitutes_HDAL objDAL = new Substitutes_HDAL();

        public bool AddSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg)
        {
            return objDAL.AddSubstitutes_H(substitutes_H, strSubstitutesNo,
             strQTY, strPriority, strRemark, out msg);
        }

        public bool UpdateSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg)
        {
            return objDAL.UpdateSubstitutes_H(substitutes_H, strSubstitutesNo,
             strQTY, strPriority, strRemark, out msg);
        }

        public bool DelSubstitutes_H(Substitutes_H substitutes_H, out string msg)
        {
            return objDAL.DelSubstitutes_H(substitutes_H, out msg);
        }

        public List<Substitutes_H> SearchSubstitutes_H(string col, string condition, string value)
        {
            return objDAL.SearchSubstitutes_H(col, condition, value);
        }

        public Substitutes_H GetSubstitutes_HInfo(Substitutes_H substitutes_H)
        {
            return objDAL.GetSubstitutes_HInfo(substitutes_H);
        }

        public bool IsSubstitutes_H(Substitutes_H substitutes_H)
        {
            return objDAL.IsSubstitutes_H(substitutes_H);
        }

        public bool ImportFile(string path)
        {
            return objDAL.ImportFile(path);
        }

        public bool ConfirmedSubstitutes_H(Substitutes_H substitutes_H, out string msg)
        {
            return objDAL.ConfirmedSubstitutes_H(substitutes_H, out msg);
        }
    }
}