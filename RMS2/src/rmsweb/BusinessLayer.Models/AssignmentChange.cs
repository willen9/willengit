using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class AssignmentChange
    {
        //公司別
        public string Company { get; set; }

        public string UserGroup { get; set; }

        //群組
        public string Creator { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        public string Confirmor { get; set; }

        //修改人員
        public string ConfirmedDate { get; set; }

        //修改日期
        public string AssignOrderType { get; set; }

        //定保計畫單別
        public string AssignOrderNo { get; set; }

        //定保計畫單號
        public string OrderDate { get; set; }

        public string CustomerId { get; set; }

        //單據日期
        public string Version { get; set; }

        public string ModiReason { get; set; }

        //計畫日期
        public string SourceOrderType { get; set; }

        //客戶代號
        public string SourceOrderNo { get; set; }

        public string Designee { get; set; }

        public string Assignor { get; set; }

        public string AssignDate { get; set; }

        public int NoOfPrints { get; set; }

        public string Remark { get; set; }

        public string OldOrderType { get; set; }

        public string OldOrderNo { get; set; }

        public string OldCustomerId { get; set; }

        public string OldDesignee { get; set; }

        public string OldAssignor { get; set; }

        public string OldAssignDate { get; set; }

        public string OldRemark { get; set; }

        public string CustomerName { get; set; }

        public string DesigneeName { get; set; }

        public string ConfirmorName { get; set; }

        public string OrderCategory { get; set; }

        public string OrderStatus { get; set; }

        public string AssignorName { get; set; }

        public string Confirmed { get; set; }

        public string StatusCode { get; set; }
    }
}