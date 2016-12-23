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
    public class QuestionList_DDAL : IQuestionList_DDAL
    {
        public List<QuestionList_D> SearchQuestionList_D(QuestionList_D questionList_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<QuestionList_D> objQuestionList_D = new List<QuestionList_D>();

            string sql = @"select * from QuestionList_D where QuestionNo=@QuestionNo";


            DataTable dtQuestionList_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_D.QuestionNo)
                }); ;

            if (dtQuestionList_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuestionList_D.Rows)
                {
                    QuestionList_D obj = new QuestionList_D();
                    obj.QuestionNo = dr["QuestionNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.Solution = dr["Solution"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objQuestionList_D.Add(obj);
                }
            }

            return objQuestionList_D;
        }

    }
}