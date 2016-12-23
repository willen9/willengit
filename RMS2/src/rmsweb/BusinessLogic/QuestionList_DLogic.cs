using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class QuestionList_DLogic
    {
        IQuestionList_DDAL objDAL = new QuestionList_DDAL();

        public List<QuestionList_D> SearchQuestionList_D(QuestionList_D questionList_D)
        {
            return objDAL.SearchQuestionList_D(questionList_D);
        }

    }
}