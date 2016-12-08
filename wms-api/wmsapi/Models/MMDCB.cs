using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDCB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DCB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DCB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DCB003 { get; set; }

        [MaxLength(35)]
        public virtual string DCB004 { get; set; }

        [MaxLength(100)]
        public virtual string DCB005 { get; set; }

        [MaxLength(100)]
        public virtual string DCB006 { get; set; }

        [MaxLength(8)]
        public virtual string DCB007 { get; set; }

        [MaxLength(12)]
        public virtual string DCB008 { get; set; }

        [MaxLength(12)]
        public virtual string DCB009 { get; set; }

        [MaxLength(15)]
        public virtual string DCB010 { get; set; }

        [MaxLength(15)]
        public virtual string DCB011 { get; set; }

        [MaxLength(255)]
        public virtual string DCB020 { get; set; }

        [MaxLength(5)]
        public virtual string DCB021 { get; set; }

        [MaxLength(5)]
        public virtual string DCB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DCB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DCB026 { get; set; }

        [MaxLength(8)]
        public virtual string DCB034 { get; set; }

        [MaxLength(8)]
        public virtual string DCB035 { get; set; }

        [MaxLength(8)]
        public virtual string DCB036 { get; set; }

        public virtual Guid? DCB800 { get; set; }

        public virtual Guid? DCB801 { get; set; }

        [MaxLength(3)]
        public virtual string DCB851 { get; set; }

        [MaxLength(15)]
        public virtual string DCB852 { get; set; }

        [MaxLength(4)]
        public virtual string DCB853 { get; set; }

        [MaxLength(3)]
        public virtual string DCB854 { get; set; }

        [MaxLength(15)]
        public virtual string DCB855 { get; set; }

        [MaxLength(4)]
        public virtual string DCB856 { get; set; }

        [MaxLength(3)]
        public virtual string DCB857 { get; set; }

        [MaxLength(15)]
        public virtual string DCB858 { get; set; }

        [MaxLength(4)]
        public virtual string DCB859 { get; set; }

        [MaxLength(3)]
        public virtual string DCB992 { get; set; }

        public virtual int? DCB993 { get; set; }

        public virtual int? DCB994 { get; set; }

        [MaxLength(12)]
        public virtual string DCB995 { get; set; }

        [MaxLength(3)]
        public virtual string DCB996 { get; set; }

        [MaxLength(8)]
        public virtual string DCB997 { get; set; }

        [MaxLength(16)]
        public virtual string DCB998 { get; set; }

        [MaxLength(1)]
        public virtual string DCB999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}