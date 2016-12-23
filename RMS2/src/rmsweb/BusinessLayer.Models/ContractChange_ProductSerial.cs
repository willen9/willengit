using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //維護合約變更子單身序號檔
    public class ContractChange_ProductSerial
    {
        //公司別
        public string Company { get; set; }

        //群組
        public string UserGroup { get; set; }

        //建立人員
        public string Creator { get; set; }

        //建立日期
        public string CreateDate { get; set; }

        //修改人員
        public string Modifier { get; set; }

        //修改日期
        public string ModiDate { get; set; }

        //合約單別
        public string ContractType { get; set; }

        //合約單號
        public string ContractNo { get; set; }

        //變更版次
        public string Version { get; set; }

        //項次
        public string ItemId { get; set; }

        //品號
        public string ProductNo { get; set; }

        //序號
        public string SerialNo { get; set; }

        //新結案碼
        public string NewIsClosed { get; set; }

        //原結案碼
        public string OldIsClosed { get; set; }

        //品名
        public string ProductName { get; set; }

        //數量
        public string QTY { get; set; }

        //單位
        public string Unit { get; set; }

        //保固期限
        public string WarrantyPeriod { get; set; }

        //保固起日
        public string WarrantySDate { get; set; }

        //保固迄日
        public string WarrantyEDate { get; set; }

        //定保次數
        public string RoutineServiceFreq { get; set; }

        //備註
        public string Remark { get; set; }
    }
}