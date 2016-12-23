using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class Assignment
    {
        //公司別
        public string Company { get; set; }

        public string Confirmor { get; set; }

        public string ModiReason { get; set; }

        public string ConfirmedDate { get; set; }

        public string OrderCategory { get; set; }

        public string UserGroup { get; set; }

        //群組
        public string Creator { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        //修改日期
        public string AssignOrderType { get; set; }

        public string OrderSName { get; set; }

        public string AutoConfirm { get; set; }

        public string CheckExOrder { get; set; }

        public string AsignOrderTypeName { get; set; }

        //定保計畫單別
        public string AssignOrderNo { get; set; }

        //定保計畫單號
        public string OrderDate { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        //單據日期
        public string Version { get; set; }

        //計畫日期
        public string SourceOrderType { get; set; }

        //客戶代號
        public string SourceOrderNo { get; set; }

        public string Designee { get; set; }

        public string Assignor { get; set; }

        public string AssignDate { get; set; }

        public int NoOfPrints { get; set; }

        public string Remark { get; set; }

        public string Confirmed { get; set; }

        public string DesigneeName { get; set; }

        public string AssignorName { get; set; }

    }
}