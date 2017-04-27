namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Complaints
    {
        [Key]
        [StringLength(14)]
        public string DocEntry { get; set; }

        public int? StoreCode { get; set; }

        [StringLength(50)]
        public string StoreName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EvenDate { get; set; }

        [StringLength(5)]
        public string EvenYM { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateTime { get; set; }

        public int? Categolgy { get; set; }

        [StringLength(200)]
        public string Event { get; set; }

        [StringLength(200)]
        public string Reason { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(10)]
        public string deal { get; set; }

        [StringLength(50)]
        public string Operator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? OperrateTime { get; set; }

        public bool? Finish { get; set; }

        public virtual Category Category { get; set; }
    }
}
