using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IPhoneService_DDAL
    {
        List<PhoneService_D> SearchPhoneService_D(PhoneService_D phoneService_D);
    }
}