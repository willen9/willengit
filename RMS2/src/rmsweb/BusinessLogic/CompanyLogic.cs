using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class CompanyLogic
    {
        ICompanyDAL objDal = new CompanyDAL();

        public Company GetCompany()
        {
            return objDal.GetCompany();
        }

        public bool UpdateCompany(Company company)
        {
            return objDal.UpdateCompany(company);
        }
    }
}