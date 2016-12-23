using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class OrderCategory
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

        //單別
        public string OrderType { get; set; }

        //單據名稱
        public string OrderSName { get; set; }

        //單據全名
        public string OrderFName { get; set; }

        //單據性質
        public string OrderCategoryID { get; set; }
        public string OrderCategoryName { get; set; }

        //編碼方式
        public string CodeMode { get; set; }

        //流水碼數
        public string SerialNrCodeLength { get; set; }

        //自動確認
        public string AutoConfirm { get; set; }

        //核對來源單據
        public string CheckExOrder { get; set; }

        //備註
        public string Remark { get; set; }

        //建單後_天
        public string DaysAfterCreate { get; set; }
    }
}