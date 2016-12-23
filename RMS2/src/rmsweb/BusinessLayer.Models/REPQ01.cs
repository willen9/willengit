using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class REPQ
    {
        //公司別
        public string Item { get; set; }

        //群組
        public string Qty { get; set; }

        //建立人員
        public string Ratio { get; set; }
    }
}