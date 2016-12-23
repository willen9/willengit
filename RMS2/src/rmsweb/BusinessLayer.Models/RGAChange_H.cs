using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    //定保計畫單頭資料檔
    public class RGAChange_H
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
        public string Confirmor { get; set; }

        //定保計畫單別
        public string ConfirmedDate { get; set; }

        //定保計畫單號
        public string RGAType { get; set; }

        public string RGANo { get; set; }

        //單據日期
        public string Version { get; set; }

        public string OrderDate { get; set; }

        //客戶代號
        public int NoOfPrints { get; set; }

        public string SourceOrderType { get; set; }

        public string SourceOrderNo { get; set; }

        public string RepairAll { get; set; }

        public string Repairman { get; set; }

        public string ReasonOfChange { get; set; }

        public string NewCustomerId { get; set; }

        public string NewAddressSName { get; set; }

        public string NewAddress { get; set; }
        public string NewContact { get; set; }

        public string NewTelNo { get; set; }

        public string NewFaxNo { get; set; }

        public string NewProductNo { get; set; }

        public string NewProductName { get; set; }

        public string NewSerialNo { get; set; }

        public string NewAssetNo { get; set; }

        public string NewWarrantyStartDate { get; set; }

        public string NewWarrantyExpiryDate { get; set; }

        public string NewUnderWarranty { get; set; }

        public string NewSaleDate { get; set; }

        public string NewPurchaseDate { get; set; }

        public string NewContractType { get; set; }

        public string NewContractNo { get; set; }

        public string NewPCompletionDate { get; set; }

        public string Confirmed { get; set; }

        public string NewOptionDetail { get; set; }

        public string NewRemark { get; set; }


        public string NewRepairDetail { get; set; }

        public string NewReturned { get; set; }


        public string NewMalfunctionReason { get; set; }
        public string NewTestResult { get; set; }
        public string NewInternalRemark { get; set; }

        public string OldCustomerId { get; set; }

        public string OldAddressSName { get; set; }

        public string OldAddress { get; set; }

        public string OldContact { get; set; }

        public string OldTelNo { get; set; }

        public string OldFaxNo { get; set; }

        public string OldProductNo { get; set; }

        public string OldProductName { get; set; }

        public string OldSerialNo { get; set; }

        public string OldAssetNo { get; set; }

        public string OldWarrantyStartDate { get; set; }

        public string OldWarrantyExpiryDate { get; set; }

        public string OldUnderWarranty { get; set; }
        public string OldSaleDate { get; set; }

        public string OldPurchaseDate { get; set; }


        public string OldContractType { get; set; }


        public string OldContractNo { get; set; }

        public string OldPCompletionDate { get; set; }

        public string OldOptionDetail { get; set; }


        public string OldRemark { get; set; }
        public string OldReturned { get; set; }
        public string OldMalfunctionReason { get; set; }
        public string OldTestResult { get; set; }

        public string OldInternalRemark { get; set; }

    }
}