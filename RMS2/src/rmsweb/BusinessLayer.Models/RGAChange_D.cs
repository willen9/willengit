using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RGAChange_D
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
        public string RGAType { get; set; }

        //定保計畫單別
        public string RGANo { get; set; }

        public string Version { get; set; }

        //定保計畫單號
        public string ItemId { get; set; }

        public string NewPartNo { get; set; }

        //單據日期
        public string NewPartName { get; set; }

        public double NewQTY { get; set; }

        //客戶代號
        public string NewUnit { get; set; }

        public string NewRemark { get; set; }

        public string NewRepaired { get; set; }

        public string NewSourceOrderType { get; set; }

        public string NewSourceOrderNo { get; set; }

        public string NewSourceItemId { get; set; }

        public string NewResponseOrderType { get; set; }

        public string NewResponseOrderNo { get; set; }

        public string NewResponseDate { get; set; }

        public string OldPartNo { get; set; }

        public string OldPartName { get; set; }

        public double OldQTY { get; set; }

        //客戶代號
        public string OldUnit { get; set; }

        public string OldRemark { get; set; }

        public string OldRepaired { get; set; }

        public string OldSourceOrderType { get; set; }

        public string OldSourceOrderNo { get; set; }

        public string OldSourceItemId { get; set; }

        public string OldResponseOrderType { get; set; }

        public string OldResponseOrderNo { get; set; }

        public string OldResponseDate { get; set; }

    }
}