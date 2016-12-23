using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ICompletionOrderDAL
    {
        bool AddCompletionOrder(CompletionOrder completionOrder, out string msg);

        bool UpdateCompletionOrder(CompletionOrder completionOrder, out string msg);

        bool DelCompletionOrder(CompletionOrder completionOrder, out string msg);

        List<CompletionOrder> SearchCompletionOrder(string col, string condition, string value);

        CompletionOrder GetCompletionOrderInfo(CompletionOrder completionOrder);

        bool ConfirmedCompletionOrder(CompletionOrder completionOrder, out string msg);

    }
}