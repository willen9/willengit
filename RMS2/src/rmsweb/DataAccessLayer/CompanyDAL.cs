using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class CompanyDAL:ICompanyDAL
    {
        public Company GetCompany()
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Company objCompany = new Company();

            DataTable dtCompany = dbMySQL.ExecuteDataTable("select * from Company limit 1");

            if (dtCompany != null)
            {
                objCompany.CompanyId = dtCompany.Rows[0]["CompanyId"].ToString();
                objCompany.CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                objCompany.CompanyFName = dtCompany.Rows[0]["CompanyFName"].ToString();
                objCompany.TelNo = dtCompany.Rows[0]["TelNo"].ToString();
                objCompany.FaxNo = dtCompany.Rows[0]["FaxNo"].ToString();
                objCompany.Address = dtCompany.Rows[0]["Address"].ToString();
                objCompany.Remark = dtCompany.Rows[0]["Remark"].ToString();

                return objCompany;
            }
            else
            {
                return null;
            }

        }

        //存儲公司基本資料
        public bool UpdateCompany(Company company)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"Update Company set CompanyName=@CompanyName , 
                    CompanyFName=@CompanyFName , TelNo=@TelNo , FaxNo=@FaxNo , 
                     Address=@Address , Remark=@Remark where CompanyId=@CompanyId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CompanyName", DbType.String, company.CompanyName),
                    DataAccessMySQL.CreateParameter("CompanyFName", DbType.String, company.CompanyFName),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, company.TelNo),
                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, company.FaxNo),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, company.Address),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, company.Remark),
                    DataAccessMySQL.CreateParameter("CompanyId", DbType.String, company.CompanyId)
                });
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}