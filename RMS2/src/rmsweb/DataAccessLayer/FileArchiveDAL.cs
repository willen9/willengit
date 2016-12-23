using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class FileArchiveDAL : IFileArchiveDAL
    {
        public bool AddFileArchive(FileArchive fileArchive, string fileName, string filePath)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            try
            {
                fileArchive.FileContent = "[{FileName:'"+ fileName + "',FilePath:'"+ filePath + "'}]";
                int item = int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from FileArchive where FileKey like @FileKey",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("FileKey",DbType.String,fileArchive.FileKey+"-%"),
                    }).ToString());
                dbMySQL.ExecuteNonQuery(@"insert into FileArchive (FileKey,FileContent,Company,Creator,CreateDate) 
                    VALUES (@FileKey,@FileContent,@Company,@Creator,@CreateDate)",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,fileArchive.Company),
                        DataAccessMySQL.CreateParameter("Creator",DbType.String,fileArchive.Creator),
                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("FileKey",DbType.String,fileArchive.FileKey+"-"+(item+1).ToString("0000")),
                        DataAccessMySQL.CreateParameter("FileContent",DbType.String,fileArchive.FileContent)
                    });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public List<FileArchive> SearchFile(string FileKey)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<FileArchive> objFileArchive = new List<FileArchive>();

            string sql = @"select * from FileArchive where FileKey like @FileKey";

            DataTable dtFileArchive = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("FileKey",DbType.String,FileKey+"-%")
                }); 

            if (dtFileArchive.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFileArchive.Rows)
                {
                    FileArchive obj = new FileArchive();
                    obj.FileKey = dr["FileKey"].ToString();
                    obj.FileContent = dr["FileContent"].ToString();
                    objFileArchive.Add(obj);
                }
            }

            return objFileArchive;
        }

        public bool DelFile(string FileKey)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            try
            {

                dbMySQL.ExecuteNonQuery("delete from FileArchive where FileKey=@FileKey",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("FileKey",DbType.String,FileKey)
                        });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}