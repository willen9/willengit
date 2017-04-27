namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? ZoneCode { get; set; }

        public int? SerialNo { get; set; }

        public int? BusinessCode { get; set; }

        public int? ModificationCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModificationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DevelopDate { get; set; }

        [StringLength(50)]
        public string Developer { get; set; }

        public int? DBC { get; set; }

        public bool? Sunday { get; set; }

        public bool? Monday { get; set; }

        public bool? Tuesday { get; set; }

        public bool? Wednesday { get; set; }

        public bool? Thursday { get; set; }

        public bool? Friday { get; set; }

        public bool? Saturday { get; set; }

        [StringLength(10)]
        public string ReceiptZIP { get; set; }

        [StringLength(50)]
        public string ReceipAddress { get; set; }

        [StringLength(10)]
        public string ZIP { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastTime { get; set; }

        public object FSTime { get; set; }

        public object FETime { get; set; }

        public object SSTime { get; set; }

        public object SETime { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public int? Level { get; set; }

        [StringLength(20)]
        public string Creator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateTime { get; set; }

        [StringLength(20)]
        public string Updater { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updateTime { get; set; }

        public virtual BusinessCategory BusinessCategory { get; set; }

        public virtual DonationBox DonationBox { get; set; }

        public virtual Modification Modification { get; set; }

        public virtual Zone Zone { get; set; }
    }
}
