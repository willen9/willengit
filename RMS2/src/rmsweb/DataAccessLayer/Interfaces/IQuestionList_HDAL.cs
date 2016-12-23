using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IQuestionList_HDAL
    {
        bool AddQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg);

        bool UpdateQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg);

        bool DelQuestionList_H(QuestionList_H questionList_H, out string msg);

        List<QuestionList_H> SearchQuestionList_H(string col, string condition, string value);

        DataTable Search_H(QuestionList_H customer, string col, string condition, string value);
        
        DataTable Search_HCUR(QuestionList_H customer);

        DataTable Search_CCUR(QuestionList_H customer);

        List<QuestionList_H> SearchRGA_H(string col, string condition, string value);

        QuestionList_H GetQuestionList_HInfo(QuestionList_H questionList_H);

        bool IsQuestionList_H(QuestionList_H questionList_H);

        bool ImportFile(string path);

        List<QuestionList_H> SearchQuestionList(QuestionList_H questionList_H, int Page);
    }
}