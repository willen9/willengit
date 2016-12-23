using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ICompanyDAL
    {
        Company GetCompany();

        bool UpdateCompany(Company company);
    }
}