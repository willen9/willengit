namespace MustardSeedMission.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonationHead")]
    public partial class DonationHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonationHead()
        {
            DonationLists = new HashSet<DonationList>();
        }

        [Key]
        [StringLength(14)]
        public string DocEntry { get; set; }

        public int? PlanCode { get; set; }

        [StringLength(50)]
        public string Operator { get; set; }

        [Column(TypeName = "date")]
        public DateTime? WorkDay { get; set; }

        [StringLength(50)]
        public string Creator { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateTime { get; set; }

        [StringLength(20)]
        public string Updater { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonationList> DonationLists { get; set; }
    }
}
