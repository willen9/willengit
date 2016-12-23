using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class RepairRecordLogic
    {
        IRepairRecordDAL objDAL = new RepairRecordDAL();

        public List<RepairRecord> SearchRepairRecord(string col, string condition, string value)
        {
            return objDAL.SearchRepairRecord(col, condition, value);
        }
        
    }
}