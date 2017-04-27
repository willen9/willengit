namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanList")]
    public partial class PlanList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoreCode { get; set; }

        [StringLength(20)]
        public string Creator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateTime { get; set; }

        [StringLength(20)]
        public string Updater { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updateTime { get; set; }
    }
}
