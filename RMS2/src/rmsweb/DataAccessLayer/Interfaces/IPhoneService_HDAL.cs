using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPhoneService_HDAL
    {
        bool AddPhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription,string strProcessMan,
            string strHours,string strUnit,string strRemark, out string msg);

        bool UpdatePhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription, string strProcessMan,
            string strHours, string strUnit, string strRemark, out string msg);

        bool DelPhoneService_H(PhoneService_H phoneService_H, out string msg);

        List<PhoneService_H> SearchPhoneService_H(string col, string condition, string value);

        PhoneService_H GetPhoneService_HInfo(PhoneService_H phoneService_H);

        bool IsPhoneService_H(PhoneService_H phoneService_H);

        bool ConfirmedPhoneService_H(PhoneService_H phoneService_H, out string msg);

        List<PhoneService_H> SearchPhoneService(PhoneService_H phoneService_H, int Page);
    }
}