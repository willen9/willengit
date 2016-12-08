using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDBB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DBB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DBB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DBB003 { get; set; }

        [MaxLength(35)]
        public virtual string DBB004 { get; set; }

        [MaxLength(100)]
        public virtual string DBB005 { get; set; }

        [MaxLength(100)]
        public virtual string DBB006 { get; set; }

        [MaxLength(8)]
        public virtual string DBB007 { get; set; }

        [MaxLength(12)]
        public virtual string DBB008 { get; set; }

        [MaxLength(12)]
        public virtual string DBB009 { get; set; }

        [MaxLength(15)]
        public virtual string DBB010 { get; set; }

        [MaxLength(15)]
        public virtual string DBB011 { get; set; }

        [MaxLength(255)]
        public virtual string DBB020 { get; set; }

        [MaxLength(5)]
        public virtual string DBB021 { get; set; }

        [MaxLength(5)]
        public virtual string DBB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DBB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DBB026 { get; set; }

        [MaxLength(8)]
        public virtual string DBB034 { get; set; }

        [MaxLength(8)]
        public virtual string DBB035 { get; set; }

        [MaxLength(8)]
        public virtual string DBB036 { get; set; }

        public virtual Guid? DBB800 { get; set; }

        public virtual Guid? DBB801 { get; set; }

        [MaxLength(3)]
        public virtual string DBB851 { get; set; }

        [MaxLength(15)]
        public virtual string DBB852 { get; set; }

        [MaxLength(4)]
        public virtual string DBB853 { get; set; }

        [MaxLength(3)]
        public virtual string DBB854 { get; set; }

        [MaxLength(15)]
        public virtual string DBB855 { get; set; }

        [MaxLength(4)]
        public virtual string DBB856 { get; set; }

        [MaxLength(3)]
        public virtual string DBB857 { get; set; }

        [MaxLength(15)]
        public virtual string DBB858 { get; set; }

        [MaxLength(4)]
        public virtual string DBB859 { get; set; }

        [MaxLength(3)]
        public virtual string DBB992 { get; set; }

        public virtual int? DBB993 { get; set; }

        public virtual int? DBB994 { get; set; }

        [MaxLength(12)]
        public virtual string DBB995 { get; set; }

        [MaxLength(3)]
        public virtual string DBB996 { get; set; }

        [MaxLength(8)]
        public virtual string DBB997 { get; set; }

        [MaxLength(16)]
        public virtual string DBB998 { get; set; }

        [MaxLength(1)]
        public virtual string DBB999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}