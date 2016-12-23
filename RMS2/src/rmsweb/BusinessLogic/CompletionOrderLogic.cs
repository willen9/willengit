using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class CompletionOrderLogic
    {
        ICompletionOrderDAL objDAL = new CompletionOrderDAL();

        public bool AddCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            return objDAL.AddCompletionOrder(completionOrder, out msg);
        }

        public bool UpdateCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            return objDAL.UpdateCompletionOrder(completionOrder, out msg);
        }

        public bool DelCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            return objDAL.DelCompletionOrder(completionOrder, out msg);
        }

        public List<CompletionOrder> SearchCompletionOrder(string col, string condition, string value)
        {
            return objDAL.SearchCompletionOrder(col, condition, value);
        }

        public CompletionOrder GetCompletionOrderInfo(CompletionOrder completionOrder)
        {
            return objDAL.GetCompletionOrderInfo(completionOrder);
        }

        public bool ConfirmedCompletionOrder(CompletionOrder completionOrder, out string msg)
        {
            return objDAL.ConfirmedCompletionOrder(completionOrder, out msg);
        }

    }
}