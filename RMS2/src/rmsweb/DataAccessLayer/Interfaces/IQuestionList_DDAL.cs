using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IQuestionList_DDAL
    {
        List<QuestionList_D> SearchQuestionList_D(QuestionList_D questionList_D);
    }
}