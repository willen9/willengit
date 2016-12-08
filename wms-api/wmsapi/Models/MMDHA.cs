using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDHA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DHA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DHA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DHA003 { get; set; }

        [MaxLength(8)]
        public virtual string DHA004 { get; set; }

        [MaxLength(16)]
        public virtual string DHA005 { get; set; }

        [MaxLength(16)]
        public virtual string DHA006 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DHA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DHA016 { get; set; }

        [MaxLength(255)]
        public virtual string DHA020 { get; set; }

        [MaxLength(8)]
        public virtual string DHA030 { get; set; }

        public virtual int? DHA041 { get; set; }

        public virtual int? DHA042 { get; set; }

        public virtual Guid? DHA800 { get; set; }

        public virtual Guid? DHA801 { get; set; }

        [MaxLength(3)]
        public virtual string DHA851 { get; set; }

        [MaxLength(15)]
        public virtual string DHA852 { get; set; }

        [MaxLength(3)]
        public virtual string DHA854 { get; set; }

        [MaxLength(15)]
        public virtual string DHA855 { get; set; }

        [MaxLength(3)]
        public virtual string DHA857 { get; set; }

        [MaxLength(15)]
        public virtual string DHA858 { get; set; }

        [MaxLength(3)]
        public virtual string DHA992 { get; set; }

        public virtual int? DHA993 { get; set; }

        public virtual int? DHA994 { get; set; }

        [MaxLength(12)]
        public virtual string DHA995 { get; set; }

        [MaxLength(3)]
        public virtual string DHA996 { get; set; }

        [MaxLength(8)]
        public virtual string DHA997 { get; set; }

        [MaxLength(16)]
        public virtual string DHA928 { get; set; }

        [MaxLength(1)]
        public virtual string DHA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}