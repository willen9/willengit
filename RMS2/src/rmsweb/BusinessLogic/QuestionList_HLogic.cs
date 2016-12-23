using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class QuestionList_HLogic
    {
        IQuestionList_HDAL objDAL = new QuestionList_HDAL();

        public bool AddQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg)
        {
            return objDAL.AddQuestionList_H(questionList_H, strItemId,
             strSolution, strRemark, out msg);
        }

        public bool UpdateQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg)
        {
            return objDAL.UpdateQuestionList_H(questionList_H, strItemId,
             strSolution, strRemark, out msg);
        }

        public bool DelQuestionList_H(QuestionList_H questionList_H, out string msg)
        {
            return objDAL.DelQuestionList_H(questionList_H, out msg);
        }

        public List<QuestionList_H> SearchQuestionList_H(string col, string condition, string value)
        {
            return objDAL.SearchQuestionList_H(col, condition, value);
        }

        public DataTable Search_H(QuestionList_H customer, string col, string condition, string value)
        {
            return objDAL.Search_H(customer,col, condition, value);
        }
        
        public DataTable Search_HCUR(QuestionList_H customer)
        {
            return objDAL.Search_HCUR(customer);
        }
        public DataTable Search_CCUR(QuestionList_H customer)
        {
            return objDAL.Search_CCUR(customer);
        }

        public List<QuestionList_H> SearchRGA_H(string col, string condition, string value)
        {
            return objDAL.SearchRGA_H(col, condition, value);
        }
        public QuestionList_H GetQuestionList_HInfo(QuestionList_H questionList_H)
        {
            return objDAL.GetQuestionList_HInfo(questionList_H);
        }

        public bool IsQuestionList_H(QuestionList_H questionList_H)
        {
            return objDAL.IsQuestionList_H(questionList_H);
        }

        public bool ImportFile(string path)
        {
            return objDAL.ImportFile(path);
        }

        public List<QuestionList_H> SearchQuestionList(QuestionList_H questionList_H, int Page)
        {
            return objDAL.SearchQuestionList(questionList_H, Page);
        }
    }
}