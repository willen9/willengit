using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDCA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DCA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DCA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DCA003 { get; set; }

        [MaxLength(8)]
        public virtual string DCA004 { get; set; }

        [MaxLength(16)]
        public virtual string DCA005 { get; set; }

        [MaxLength(16)]
        public virtual string DCA006 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DCA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DCA016 { get; set; }

        [MaxLength(255)]
        public virtual string DCA020 { get; set; }

        [MaxLength(8)]
        public virtual string DCA030 { get; set; }

        public virtual int? DCA041 { get; set; }

        public virtual int? DCA042 { get; set; }

        public virtual Guid? DCA800 { get; set; }

        public virtual Guid? DCA801 { get; set; }

        [MaxLength(3)]
        public virtual string DCA851 { get; set; }

        [MaxLength(15)]
        public virtual string DCA852 { get; set; }

        [MaxLength(3)]
        public virtual string DCA854 { get; set; }

        [MaxLength(15)]
        public virtual string DCA855 { get; set; }

        [MaxLength(3)]
        public virtual string DCA857 { get; set; }

        [MaxLength(15)]
        public virtual string DCA858 { get; set; }

        [MaxLength(3)]
        public virtual string DCA992 { get; set; }

        public virtual int? DCA993 { get; set; }

        public virtual int? DCA994 { get; set; }

        [MaxLength(12)]
        public virtual string DCA995 { get; set; }

        [MaxLength(3)]
        public virtual string DCA996 { get; set; }

        [MaxLength(8)]
        public virtual string DCA997 { get; set; }

        [MaxLength(16)]
        public virtual string DCA928 { get; set; }

        [MaxLength(1)]
        public virtual string DCA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}