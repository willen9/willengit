namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonationList")]
    public partial class DonationList
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(14)]
        public string DocEntry { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Serialno { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoreCode { get; set; }

        public int? Coin { get; set; }

        public int? money1000 { get; set; }

        public int? money500 { get; set; }

        public int? money200 { get; set; }

        public int? money100 { get; set; }

        [StringLength(50)]
        public string Operator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? OperationTime { get; set; }

        public int? CheckTotal { get; set; }

        public int? CheckInvoice { get; set; }

        public int? Checkcurrency { get; set; }

        public int? CheckCurrencyCoin { get; set; }

        [StringLength(50)]
        public string Typer { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TypeTime { get; set; }

        public int? ReCheckCoin { get; set; }

        public int? ReCheckmoney1000 { get; set; }

        public int? ReCheckmoney500 { get; set; }

        public int? ReCheckmoney200 { get; set; }

        public int? ReCheckmoney100 { get; set; }

        [StringLength(50)]
        public string Rechecker { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Rechecktime { get; set; }

        [StringLength(50)]
        public string checker { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckTime { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool? printYN { get; set; }

        public bool? Additional { get; set; }

        public virtual DonationHead DonationHead { get; set; }
    }
}
