﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫明細資料檔
    public class ServiceArrangeModi_D
    {
        //公司別
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
        public string SerArrangeOrderType { get; set; }

        //定保計畫單別
        public string SerArrangeOrderNo { get; set; }

        //定保計畫單號
        public string Version { get; set; }

        //項次
        public string ItemId { get; set; }

        //保養月份
        public string NewArrangeMonth { get; set; }

        //地址簡稱
        public string NewAddressSName { get; set; }

        //地址
        public string NewAddress { get; set; }

        //結案碼
        public string NewIsClosed { get; set; }

        //備註
        public string NewRemark { get; set; }

        //保養月份
        public string ArrangeMonth { get; set; }

        //地址簡稱
        public string AddressSName { get; set; }

        //地址
        public string Address { get; set; }

        //結案碼
        public string IsClosed { get; set; }

        //備註
        public string Remark { get; set; }

        //保養月份
        public string OldArrangeMonth { get; set; }

        //地址簡稱
        public string OldAddressSName { get; set; }

        //地址
        public string OldAddress { get; set; }

        //結案碼
        public string OldIsClosed { get; set; }

        //備註
        public string OldRemark { get; set; }

    }
}