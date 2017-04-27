using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MustardSeedMission.ViewModels
{
    public class ComplaintsViewModel
    {
        public string DocEntry { get; set; }

        public int? StoreCode { get; set; }

        public string StoreName { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public DateTime? EvenDate { get; set; }

        public string EvenYM { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Categolgy { get; set; }

        public string Event { get; set; }

        public string Reason { get; set; }

        public string Description { get; set; }

        public string deal { get; set; }

        public string Operator { get; set; }

        public DateTime? OperrateTime { get; set; }

        public bool? Finish { get; set; }

        public int? RegionCode { get; set; }

        public string RegionName { get; set; }

        public int? ZoneCode { get; set; }

        public string ZoneName { get; set; }
    }
}