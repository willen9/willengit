using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDMB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DMB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DMB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DMB003 { get; set; }

        [MaxLength(35)]
        public virtual string DMB004 { get; set; }

        [MaxLength(100)]
        public virtual string DMB005 { get; set; }

        [MaxLength(100)]
        public virtual string DMB006 { get; set; }

        [MaxLength(8)]
        public virtual string DMB007 { get; set; }

        [MaxLength(12)]
        public virtual string DMB008 { get; set; }

        [MaxLength(12)]
        public virtual string DMB009 { get; set; }

        [MaxLength(15)]
        public virtual string DMB010 { get; set; }

        [MaxLength(15)]
        public virtual string DMB011 { get; set; }

        [MaxLength(255)]
        public virtual string DMB020 { get; set; }

        [MaxLength(5)]
        public virtual string DMB021 { get; set; }

        [MaxLength(5)]
        public virtual string DMB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DMB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DMB026 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DMB027 { get; set; }

        [MaxLength(8)]
        public virtual string DMB034 { get; set; }

        [MaxLength(8)]
        public virtual string DMB035 { get; set; }

        [MaxLength(8)]
        public virtual string DMB036 { get; set; }

        public virtual Guid? DMB800 { get; set; }

        public virtual Guid? DMB801 { get; set; }

        [MaxLength(3)]
        public virtual string DMB851 { get; set; }

        [MaxLength(15)]
        public virtual string DMB852 { get; set; }

        [MaxLength(4)]
        public virtual string DMB853 { get; set; }

        [MaxLength(3)]
        public virtual string DMB854 { get; set; }

        [MaxLength(15)]
        public virtual string DMB855 { get; set; }

        [MaxLength(4)]
        public virtual string DMB856 { get; set; }

        [MaxLength(3)]
        public virtual string DMB857 { get; set; }

        [MaxLength(15)]
        public virtual string DMB858 { get; set; }
        [MaxLength(4)]

        public virtual string DMB859 { get; set; }

        [MaxLength(3)]
        public virtual string DMB992 { get; set; }

        public virtual int? DMB993 { get; set; }

        public virtual int? DMB994 { get; set; }

        [MaxLength(12)]
        public virtual string DMB995 { get; set; }

        [MaxLength(3)]
        public virtual string DMB996 { get; set; }

        [MaxLength(8)]
        public virtual string DMB997 { get; set; }

        [MaxLength(16)]
        public virtual string DMB998 { get; set; }

        [MaxLength(1)]
        public virtual string DMB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}