using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IRepairRecordDAL
    {
        List<RepairRecord> SearchRepairRecord(string col, string condition, string value);

    }
}