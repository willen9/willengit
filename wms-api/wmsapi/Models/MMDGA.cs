using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDGA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DGA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DGA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DGA003 { get; set; }

        [MaxLength(8)]
        public virtual string DGA004 { get; set; }

        [MaxLength(16)]
        public virtual string DGA005 { get; set; }

        [MaxLength(16)]
        public virtual string DGA006 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DGA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DGA016 { get; set; }

        [MaxLength(255)]
        public virtual string DGA020 { get; set; }

        [MaxLength(8)]
        public virtual string DGA030 { get; set; }

        public virtual int? DGA041 { get; set; }

        public virtual int? DGA042 { get; set; }

        public virtual Guid? DGA800 { get; set; }

        public virtual Guid? DGA801 { get; set; }

        [MaxLength(3)]
        public virtual string DGA851 { get; set; }

        [MaxLength(15)]
        public virtual string DGA852 { get; set; }

        [MaxLength(3)]
        public virtual string DGA854 { get; set; }

        [MaxLength(15)]
        public virtual string DGA855 { get; set; }

        [MaxLength(3)]
        public virtual string DGA857 { get; set; }

        [MaxLength(15)]
        public virtual string DGA858 { get; set; }

        [MaxLength(3)]
        public virtual string DGA992 { get; set; }

        public virtual int? DGA993 { get; set; }

        public virtual int? DGA994 { get; set; }

        [MaxLength(12)]
        public virtual string DGA995 { get; set; }

        [MaxLength(3)]
        public virtual string DGA996 { get; set; }

        [MaxLength(8)]
        public virtual string DGA997 { get; set; }

        [MaxLength(16)]
        public virtual string DGA928 { get; set; }

        [MaxLength(1)]
        public virtual string DGA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}