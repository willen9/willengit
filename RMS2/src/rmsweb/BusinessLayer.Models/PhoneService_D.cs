using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class PhoneService_D
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

        //修改日期
        public string PhoneSerType { get; set; }

        //定保計畫單別
        public string PhoneSerNo { get; set; }

        //定保計畫單號
        public string ItemId { get; set; }

        public string ProcessDate { get; set; }

        //修改日期
        public string Description { get; set; }

        //定保計畫單別
        public string ProcessMan { get; set; }

        public string ProcessManName { get; set; }

        //定保計畫單號
        public double Hours { get; set; }

        public string Unit { get; set; }

        //修改日期
        public string Remark { get; set; }

    }
}