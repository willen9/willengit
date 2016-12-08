using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDNB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DNB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DNB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DNB003 { get; set; }

        [MaxLength(35)]
        public virtual string DNB004 { get; set; }

        [MaxLength(100)]
        public virtual string DNB005 { get; set; }

        [MaxLength(100)]
        public virtual string DNB006 { get; set; }

        [MaxLength(8)]
        public virtual string DNB007 { get; set; }

        [MaxLength(12)]
        public virtual string DNB008 { get; set; }

        [MaxLength(12)]
        public virtual string DNB009 { get; set; }

        [MaxLength(15)]
        public virtual string DNB010 { get; set; }

        [MaxLength(15)]
        public virtual string DNB011 { get; set; }

        [MaxLength(255)]
        public virtual string DNB020 { get; set; }

        [MaxLength(5)]
        public virtual string DNB021 { get; set; }

        [MaxLength(5)]
        public virtual string DNB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DNB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DNB026 { get; set; }

        [MaxLength(8)]
        public virtual string DNB034 { get; set; }

        [MaxLength(8)]
        public virtual string DNB035 { get; set; }

        public virtual Guid? DNB800 { get; set; }

        public virtual Guid? DNB801 { get; set; }

        [MaxLength(3)]
        public virtual string DNB851 { get; set; }

        [MaxLength(15)]
        public virtual string DNB852 { get; set; }

        [MaxLength(4)]
        public virtual string DNB853 { get; set; }

        [MaxLength(3)]
        public virtual string DNB854 { get; set; }

        [MaxLength(15)]
        public virtual string DNB855 { get; set; }

        [MaxLength(4)]
        public virtual string DNB856 { get; set; }

        [MaxLength(3)]
        public virtual string DNB857 { get; set; }

        [MaxLength(15)]
        public virtual string DNB858 { get; set; }

        [MaxLength(4)]
        public virtual string DNB859 { get; set; }

        [MaxLength(3)]
        public virtual string DNB992 { get; set; }

        public virtual int? DNB993 { get; set; }

        public virtual int? DNB994 { get; set; }

        [MaxLength(12)]
        public virtual string DNB995 { get; set; }

        [MaxLength(3)]
        public virtual string DNB996 { get; set; }

        [MaxLength(8)]
        public virtual string DNB997 { get; set; }

        [MaxLength(16)]
        public virtual string DNB998 { get; set; }

        [MaxLength(1)]
        public virtual string DNB999 { get; set; }
        
        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}