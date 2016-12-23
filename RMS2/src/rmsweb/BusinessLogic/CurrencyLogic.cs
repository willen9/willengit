using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace BusinessLogic
{
    public class CurrencyLogic
    {
        public ICurrency objDal = new CurrencyDAL();

        public DataTable SelectCurrency(Currency currency,string condition)
        {
            return objDal.SelectCurrency(currency, condition);
        }
        public bool IsModuleId(string currencyId)
        {
            return objDal.IsModuleId(currencyId);
        }
        public string UpdateCurrency(Currency currency)
        {
            return objDal.UpdateCurrency(currency);
        }

        public bool DeleteCurrency(Currency currency)
        {
            return objDal.DeleteCurrency(currency);
        }

        public string InsertCurrency(Currency currency)
        {
            return objDal.InsertCurrency(currency);
        }

        public DataTable SearchDetailCurrency(string col, string condition, string value)
        {
            return objDal.SearchDetailCurrency(col, condition, value);
        }
    }
}