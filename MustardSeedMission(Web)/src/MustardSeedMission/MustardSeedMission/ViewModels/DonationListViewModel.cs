using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class DonationListViewModel
    {
        public string DocEntry { get; set; }

        public int Serialno { get; set; }

        public int StoreCode { get; set; }

        public int? Coin { get; set; }

        public int? money1000 { get; set; }

        public int? money500 { get; set; }

        public int? money200 { get; set; }

        public int? money100 { get; set; }

        public string Operator { get; set; }

        public DateTime? OperationTime { get; set; }

        public int? CheckTotal { get; set; }

        public int? CheckInvoice { get; set; }

        public int? Checkcurrency { get; set; }

        public int? CheckCurrencyCoin { get; set; }

        public string Typer { get; set; }

        public DateTime? TypeTime { get; set; }

        public int? ReCheckCoin { get; set; }

        public int? ReCheckmoney1000 { get; set; }

        public int? ReCheckmoney500 { get; set; }

        public int? ReCheckmoney200 { get; set; }

        public int? ReCheckmoney100 { get; set; }

        public string Rechecker { get; set; }

        public DateTime? Rechecktime { get; set; }

        public string checker { get; set; }

        public DateTime? CheckTime { get; set; }

        public string Description { get; set; }

        public bool? printYN { get; set; }

        public bool? Additional { get; set; }

        public int? RegionCode { get; set; }

        public string RegionName { get; set; }

        public int? ZoneCode { get; set; }

        public string ZoneName { get; set; }

        public string StoreName { get; set; }

        public string OperatorName { get; set; }

        public string TyperName { get; set; }

        public string RecheckerName { get; set; }

        public string checkerName { get; set; }

        public DateTime? WorkDay { get; set; }

        public int? PlanCode { get; set; }

        public string PlanName { get; set; }

        public int? ModificationCode { get; set; }

        public string ModificationName { get; set; }

        public int? BusinessCode { get; set; }

        public string BusinessName { get; set; }

        public DateTime? ModificationDate { get; set; }

        public DateTime? DevelopDate { get; set; }

        public int SerialnoOld { get; set; }

        public int StoreCodeOld { get; set; }
    }
}