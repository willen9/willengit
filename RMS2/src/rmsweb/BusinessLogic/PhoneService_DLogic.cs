using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class PhoneService_DLogic
    {
        IPhoneService_DDAL objDAL = new PhoneService_DDAL();

        public List<PhoneService_D> SearchPhoneService_D(PhoneService_D phoneService_D)
        {
            return objDAL.SearchPhoneService_D(phoneService_D);
        }

    }
}