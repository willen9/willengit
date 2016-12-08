using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDKB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DKB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DKB002 { get; set; }

        [Required]
        [MaxLength(4)]
        public virtual string DKB003 { get; set; }

        [MaxLength(35)]
        public virtual string DKB004 { get; set; }

        [MaxLength(100)]
        public virtual string DKB005 { get; set; }

        [MaxLength(100)]
        public virtual string DKB006 { get; set; }

        [MaxLength(8)]
        public virtual string DKB007 { get; set; }

        [MaxLength(12)]
        public virtual string DKB008 { get; set; }

        [MaxLength(12)]
        public virtual string DKB009 { get; set; }

        [MaxLength(15)]
        public virtual string DKB010 { get; set; }

        [MaxLength(15)]
        public virtual string DKB011 { get; set; }

        [MaxLength(255)]
        public virtual string DKB020 { get; set; }

        [MaxLength(5)]
        public virtual string DKB021 { get; set; }

        [MaxLength(5)]
        public virtual string DKB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKB026 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKB027 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKB028 { get; set; }

        [MaxLength(8)]
        public virtual string DKB034 { get; set; }

        [MaxLength(8)]
        public virtual string DKB035 { get; set; }

        [MaxLength(3)]
        public virtual string DKB050 { get; set; }

        [MaxLength(15)]
        public virtual string DKB051 { get; set; }

        public virtual Guid? DKB800 { get; set; }

        public virtual Guid? DKB801 { get; set; }

        [MaxLength(3)]
        public virtual string DKB851 { get; set; }

        [MaxLength(15)]
        public virtual string DKB852 { get; set; }

        [MaxLength(4)]
        public virtual string DKB853 { get; set; }

        [MaxLength(3)]
        public virtual string DKB854 { get; set; }

        [MaxLength(15)]
        public virtual string DKB855 { get; set; }

        [MaxLength(4)]
        public virtual string DKB856 { get; set; }

        [MaxLength(3)]
        public virtual string DKB857 { get; set; }

        [MaxLength(15)]
        public virtual string DKB858 { get; set; }

        [MaxLength(4)]
        public virtual string DKB859 { get; set; }

        [MaxLength(3)]
        public virtual string DKB992 { get; set; }

        public virtual int? DKB993 { get; set; }

        public virtual int? DKB994 { get; set; }

        [MaxLength(12)]
        public virtual string DKB995 { get; set; }

        [MaxLength(3)]
        public virtual string DKB996 { get; set; }

        [MaxLength(8)]
        public virtual string DKB997 { get; set; }

        [MaxLength(16)]
        public virtual string DKB998 { get; set; }

        [MaxLength(1)]
        public virtual string DKB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}