using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class QuestionList_H
    {
        //公司別
        public string Company { get; set; }

        public string OrderSName { get; set; }

        public string SerialNo { get; set; }

        public string CustomerName { get; set; }

        public string RGANo { get; set; }

        public string RGAType { get; set; }

        public string ProductNo { get; set; }
        public string MajorComponentName { get; set; }

        public string StatusCode { get; set; }

        public string Lifetime { get; set; }

        public string QTY { get; set; }
        public string ComponentNo { get;set;}
        public string ProductName { get; set; }
        public string UserGroup { get; set; }

        public string RecEDate { get; set; }

        public string SubstitutesType { get; set; }

        public string CustomerId { get; set; }

        public string RecSDate { get; set; }

        //群組
        public string Creator { get; set; }

        //建立人員
        public string CreateDate { get; set; }

        //建立日期
        public string Modifier { get; set; }

        //修改人員
        public string ModiDate { get; set; }

        //修改日期
        public string QuestionNo { get; set; }

        //定保計畫單別
        public string QuestionTopic { get; set; }

        //定保計畫單號
        public string QuestionDetail { get; set; }

        public string Remark { get; set; }
        
    }
}