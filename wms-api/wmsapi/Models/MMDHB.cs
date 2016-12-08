using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDHB
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DHB001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DHB002 { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string DHB003 { get; set; }

        [MaxLength(35)]
        public virtual string DHB004 { get; set; }

        [MaxLength(100)]
        public virtual string DHB005 { get; set; }

        [MaxLength(100)]
        public virtual string DHB006 { get; set; }

        [MaxLength(8)]
        public virtual string DHB007 { get; set; }

        [MaxLength(12)]
        public virtual string DHB008 { get; set; }

        [MaxLength(12)]
        public virtual string DHB009 { get; set; }

        [MaxLength(15)]
        public virtual string DHB010 { get; set; }

        [MaxLength(15)]
        public virtual string DHB011 { get; set; }

        [MaxLength(255)]
        public virtual string DHB020 { get; set; }

        [MaxLength(5)]
        public virtual string DHB021 { get; set; }

        [MaxLength(5)]
        public virtual string DHB022 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DHB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DHB026 { get; set; }

        [MaxLength(8)]
        public virtual string DHB034 { get; set; }

        [MaxLength(8)]
        public virtual string DHB035 { get; set; }

        public virtual Guid? DHB800 { get; set; }

        public virtual Guid? DHB801 { get; set; }

        [MaxLength(3)]
        public virtual string DHB851 { get; set; }

        [MaxLength(15)]
        public virtual string DHB852 { get; set; }

        [MaxLength(4)]
        public virtual string DHB853 { get; set; }

        [MaxLength(3)]
        public virtual string DHB854 { get; set; }

        [MaxLength(15)]
        public virtual string DHB855 { get; set; }

        [MaxLength(4)]
        public virtual string DHB856 { get; set; }

        [MaxLength(3)]
        public virtual string DHB857 { get; set; }

        [MaxLength(15)]
        public virtual string DHB858 { get; set; }

        [MaxLength(4)]
        public virtual string DHB859 { get; set; }

        [MaxLength(3)]
        public virtual string DHB992 { get; set; }

        public virtual int? DHB993 { get; set; }

        public virtual int? DHB994 { get; set; }

        [MaxLength(12)]
        public virtual string DHB995 { get; set; }

        [MaxLength(3)]
        public virtual string DHB996 { get; set; }

        [MaxLength(8)]
        public virtual string DHB997 { get; set; }

        [MaxLength(16)]
        public virtual string DHB998 { get; set; }

        [MaxLength(1)]
        public virtual string DHB999 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}