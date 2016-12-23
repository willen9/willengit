using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RoutineService_H
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
        public string ContractType { get; set; }

        //定保計畫單別
        public string RoutineSerOrderType { get; set; }

        //定保計畫單號
        public string RoutineSerOrderNo { get; set; }

        //單據日期
        public string OrderDate { get; set; }

        //計畫日期
        public string ConfirmedDate { get; set; }
        
        //來源單號
        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string SourceOrderItemId { get; set; }

        //品號
        public string ArrangeMonth { get; set; }

        //品名
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        //序號
        public string AddressSName { get; set; }

        //銷貨日期
        public string Address { get; set; }

        //定保次數
        public string Contact { get; set; }

        //保固期限
        public string TelNo { get; set; }

        //保固起日
        public string FaxNo { get; set; }

        //保固迄日
        public string ProductNo { get; set; }

        //確認碼
        public string ProductName { get; set; }

        //確認人員
        public string SerialNo { get; set; }

        //備註
        public string SaleDate { get; set; }

        //內部備註
        public string WarrantySDate { get; set; }

        public string WarrantyEDate { get; set; }

        public string Confirmed { get; set; }

        public string Remark { get; set; }

        public string InternalRemark { get; set; }

        public string AssignCheck { get; set; }

        public string AssignDate { get; set; }

        public string RoutineSerMan { get; set; }

        public int NoOfPrints { get; set; }

        public string Confirmor { get; set; }
    }
}