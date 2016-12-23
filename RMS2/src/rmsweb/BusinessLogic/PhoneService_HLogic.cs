using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class PhoneService_HLogic
    {
        IPhoneService_HDAL objDAL = new PhoneService_HDAL();

        public bool AddPhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription, string strProcessMan,
            string strHours, string strUnit, string strRemark, out string msg)
        {
            return objDAL.AddPhoneService_H(phoneService_H, strItemId,
             strProcessDate, strDescription, strProcessMan, strHours, strUnit, strRemark, out msg);
        }

        public bool UpdatePhoneService_H(PhoneService_H phoneService_H, string strItemId,
            string strProcessDate, string strDescription, string strProcessMan,
            string strHours, string strUnit, string strRemark, out string msg)
        {
            return objDAL.UpdatePhoneService_H(phoneService_H, strItemId,
             strProcessDate, strDescription, strProcessMan, strHours, strUnit, strRemark, out msg);
        }

        public bool DelPhoneService_H(PhoneService_H phoneService_H, out string msg)
        {
            return objDAL.DelPhoneService_H(phoneService_H, out msg);
        }

        public List<PhoneService_H> SearchPhoneService_H(string col, string condition, string value)
        {
            return objDAL.SearchPhoneService_H(col, condition, value);
        }

        public PhoneService_H GetPhoneService_HInfo(PhoneService_H phoneService_H)
        {
            return objDAL.GetPhoneService_HInfo(phoneService_H);
        }

        public bool IsPhoneService_H(PhoneService_H phoneService_H)
        {
            return objDAL.IsPhoneService_H(phoneService_H);
        }

        public bool ConfirmedPhoneService_H(PhoneService_H phoneService_H, out string msg)
        {
            return objDAL.ConfirmedPhoneService_H(phoneService_H, out msg);
        }

        public List<PhoneService_H> SearchPhoneService(PhoneService_H phoneService_H, int Page)
        {
            return objDAL.SearchPhoneService(phoneService_H, Page);
        }
    }
}