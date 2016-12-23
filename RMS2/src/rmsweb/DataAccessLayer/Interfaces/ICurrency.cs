using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ICurrency
    {
        DataTable SelectCurrency(Currency currency,string condition);
        bool IsModuleId(string currencyId);
        string UpdateCurrency(Currency currency);

        bool DeleteCurrency(Currency currency);

        string InsertCurrency(Currency currency);

        DataTable SearchDetailCurrency(string col, string condition, string value);
    }
}
