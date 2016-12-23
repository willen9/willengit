using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ISubstitutes_HDAL
    {
        bool AddSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg);

        bool UpdateSubstitutes_H(Substitutes_H substitutes_H, string strSubstitutesNo,
            string strQTY, string strPriority, string strRemark, out string msg);

        bool DelSubstitutes_H(Substitutes_H substitutes_H, out string msg);

        List<Substitutes_H> SearchSubstitutes_H(string col, string condition, string value);

        Substitutes_H GetSubstitutes_HInfo(Substitutes_H substitutes_H);

        bool IsSubstitutes_H(Substitutes_H substitutes_H);

        bool ImportFile(string path);

        bool ConfirmedSubstitutes_H(Substitutes_H substitutes_H, out string msg);

    }
}