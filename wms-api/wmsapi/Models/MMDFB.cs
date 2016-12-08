using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDFB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DFB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DFB002 { get; set; }

        [Required]
        [MaxLength(4)]
        public virtual string DFB003 { get; set; }

        [MaxLength(35)]
        public virtual string DFB004 { get; set; }

        [MaxLength(100)]
        public virtual string DFB005 { get; set; }

        [MaxLength(100)]
        public virtual string DFB006 { get; set; }

        [MaxLength(8)]
        public virtual string DFB007 { get; set; }

        [MaxLength(12)]
        public virtual string DFB008 { get; set; }

        [MaxLength(12)]
        public virtual string DFB009 { get; set; }

        [MaxLength(15)]
        public virtual string DFB010 { get; set; }

        [MaxLength(15)]
        public virtual string DFB011 { get; set; }

        [MaxLength(255)]
        public virtual string DFB020 { get; set; }

        [MaxLength(5)]
        public virtual string DFB021 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DFB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DFB026 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DFB027 { get; set; }

        [MaxLength(8)]
        public virtual string DFB034 { get; set; }

        [MaxLength(8)]
        public virtual string DFB035 { get; set; }

        public virtual Guid? DFB800 { get; set; }

        public virtual Guid? DFB801 { get; set; }

        [MaxLength(3)]
        public virtual string DFB851 { get; set; }

        [MaxLength(15)]
        public virtual string DFB852 { get; set; }

        [MaxLength(4)]
        public virtual string DFB853 { get; set; }

        [MaxLength(3)]
        public virtual string DFB854 { get; set; }

        [MaxLength(15)]
        public virtual string DFB855 { get; set; }

        [MaxLength(4)]
        public virtual string DFB856 { get; set; }

        [MaxLength(3)]
        public virtual string DFB857 { get; set; }

        [MaxLength(15)]
        public virtual string DFB858 { get; set; }

        [MaxLength(4)]
        public virtual string DFB859 { get; set; }

        [MaxLength(3)]
        public virtual string DFB992 { get; set; }

        public virtual int? DFB993 { get; set; }

        public virtual int? DFB994 { get; set; }

        [MaxLength(12)]
        public virtual string DFB995 { get; set; }

        [MaxLength(3)]
        public virtual string DFB996 { get; set; }

        [MaxLength(8)]
        public virtual string DFB997 { get; set; }

        [MaxLength(16)]
        public virtual string DFB998 { get; set; }

        [MaxLength(1)]
        public virtual string DFB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}