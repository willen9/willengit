using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDKA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DKA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DKA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DKA003 { get; set; }

        [MaxLength(8)]
        public virtual string DKA004 { get; set; }

        [MaxLength(16)]
        public virtual string DKA005 { get; set; }

        [MaxLength(16)]
        public virtual string DKA006 { get; set; }

        [MaxLength(12)]
        public virtual string DKA007 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DKA016 { get; set; }

        [MaxLength(255)]
        public virtual string DKA020 { get; set; }

        [MaxLength(8)]
        public virtual string DKA030 { get; set; }

        public virtual Guid? DKA800 { get; set; }

        public virtual Guid? DKA801 { get; set; }

        [MaxLength(3)]
        public virtual string DKA851 { get; set; }

        [MaxLength(15)]
        public virtual string DKA852 { get; set; }

        [MaxLength(3)]
        public virtual string DKA854 { get; set; }

        [MaxLength(15)]
        public virtual string DKA855 { get; set; }

        [MaxLength(3)]
        public virtual string DKA857 { get; set; }

        [MaxLength(15)]
        public virtual string DKA858 { get; set; }

        [MaxLength(3)]
        public virtual string DKA992 { get; set; }

        public virtual int? DKA993 { get; set; }

        public virtual int? DKA994 { get; set; }

        [MaxLength(12)]
        public virtual string DKA995 { get; set; }

        [MaxLength(3)]
        public virtual string DKA996 { get; set; }

        [MaxLength(8)]
        public virtual string DKA997 { get; set; }

        [MaxLength(16)]
        public virtual string DKA928 { get; set; }

        [MaxLength(1)]
        public virtual string DKA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}