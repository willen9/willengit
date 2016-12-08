using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDLB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DLB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DLB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DLB003 { get; set; }

        [MaxLength(35)]
        public virtual string DLB004 { get; set; }

        [MaxLength(100)]
        public virtual string DLB005 { get; set; }

        [MaxLength(100)]
        public virtual string DLB006 { get; set; }

        [MaxLength(8)]
        public virtual string DLB007 { get; set; }

        [MaxLength(12)]
        public virtual string DLB008 { get; set; }

        [MaxLength(12)]
        public virtual string DLB009 { get; set; }

        [MaxLength(15)]
        public virtual string DLB010 { get; set; }

        [MaxLength(15)]
        public virtual string DLB011 { get; set; }

        [MaxLength(255)]
        public virtual string DLB020 { get; set; }

        [MaxLength(5)]
        public virtual string DLB021 { get; set; }

        [MaxLength(5)]
        public virtual string DLB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DLB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DLB026 { get; set; }

        [MaxLength(8)]
        public virtual string DLB034 { get; set; }

        [MaxLength(8)]
        public virtual string DLB035 { get; set; }

        public virtual Guid? DLB800 { get; set; }

        public virtual Guid? DLB801 { get; set; }

        [MaxLength(3)]
        public virtual string DLB851 { get; set; }

        [MaxLength(15)]
        public virtual string DLB852 { get; set; }

        [MaxLength(4)]
        public virtual string DLB853 { get; set; }

        [MaxLength(3)]
        public virtual string DLB854 { get; set; }

        [MaxLength(15)]
        public virtual string DLB855 { get; set; }

        [MaxLength(4)]
        public virtual string DLB856 { get; set; }

        [MaxLength(3)]
        public virtual string DLB857 { get; set; }

        [MaxLength(15)]
        public virtual string DLB858 { get; set; }

        [MaxLength(4)]
        public virtual string DLB859 { get; set; }

        [MaxLength(3)]
        public virtual string DLB992 { get; set; }

        public virtual int? DLB993 { get; set; }

        public virtual int? DLB994 { get; set; }

        [MaxLength(5)]
        public virtual string DLB995 { get; set; }

        [MaxLength(3)]
        public virtual string DLB996 { get; set; }

        [MaxLength(8)]
        public virtual string DLB997 { get; set; }

        [MaxLength(16)]
        public virtual string DLB998 { get; set; }

        [MaxLength(1)]
        public virtual string DLB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}