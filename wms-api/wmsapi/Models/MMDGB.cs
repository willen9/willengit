using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDGB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DGB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DGB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DGB003 { get; set; }

        [MaxLength(35)]
        public virtual string DGB004 { get; set; }

        [MaxLength(100)]
        public virtual string DGB005 { get; set; }

        [MaxLength(100)]
        public virtual string DGB006 { get; set; }

        [MaxLength(8)]
        public virtual string DGB007 { get; set; }

        [MaxLength(12)]
        public virtual string DGB008 { get; set; }

        [MaxLength(12)]
        public virtual string DGB009 { get; set; }

        [MaxLength(15)]
        public virtual string DGB010 { get; set; }

        [MaxLength(15)]
        public virtual string DGB011 { get; set; }

        [MaxLength(255)]
        public virtual string DGB020 { get; set; }

        [MaxLength(5)]
        public virtual string DGB021 { get; set; }

        [MaxLength(5)]
        public virtual string DGB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DGB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DGB026 { get; set; }

        [MaxLength(8)]
        public virtual string DGB034 { get; set; }

        [MaxLength(8)]
        public virtual string DGB035 { get; set; }

        [MaxLength(8)]
        public virtual string DGB036 { get; set; }

        public virtual Guid? DGB800 { get; set; }

        public virtual Guid? DGB801 { get; set; }

        [MaxLength(3)]
        public virtual string DGB851 { get; set; }

        [MaxLength(15)]
        public virtual string DGB852 { get; set; }

        [MaxLength(4)]
        public virtual string DGB853 { get; set; }

        [MaxLength(3)]
        public virtual string DGB854 { get; set; }

        [MaxLength(15)]
        public virtual string DGB855 { get; set; }

        [MaxLength(4)]
        public virtual string DGB856 { get; set; }

        [MaxLength(3)]
        public virtual string DGB857 { get; set; }

        [MaxLength(15)]
        public virtual string DGB858 { get; set; }

        [MaxLength(4)]
        public virtual string DGB859 { get; set; }

        [MaxLength(3)]
        public virtual string DGB992 { get; set; }

        public virtual int? DGB993 { get; set; }

        public virtual int? DGB994 { get; set; }

        [MaxLength(12)]
        public virtual string DGB995 { get; set; }

        [MaxLength(3)]
        public virtual string DGB996 { get; set; }

        [MaxLength(8)]
        public virtual string DGB997 { get; set; }

        [MaxLength(16)]
        public virtual string DGB998 { get; set; }

        [MaxLength(1)]
        public virtual string DGB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }
    }
}