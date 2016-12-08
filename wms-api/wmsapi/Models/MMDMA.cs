using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMDMA
    {
        [Required]
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [Required]
        [MaxLength(3)]
        public virtual string DMA001 { get; set; }

        [Required]
        [MaxLength(15)]
        public virtual string DMA002 { get; set; }

        [Required]
        [MaxLength(8)]
        public virtual string DMA003 { get; set; }

        [MaxLength(8)]
        public virtual string DMA004 { get; set; }

        [MaxLength(16)]
        public virtual string DMA005 { get; set; }

        [MaxLength(16)]
        public virtual string DMA006 { get; set; }

        [MaxLength(12)]
        public virtual string DMA007 { get; set; }

        [MaxLength(30)]
        public virtual string DMA010 { get; set; }

        [MaxLength(8)]
        public virtual string DMA011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DMA015 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? DMA016 { get; set; }

        [MaxLength(255)]
        public virtual string DMA020 { get; set; }

        [MaxLength(8)]
        public virtual string DMA030 { get; set; }

        public virtual int? DMA041 { get; set; }

        public virtual int? DMA042 { get; set; }

        [MaxLength(10)]
        public virtual string DMA051 { get; set; }

        [MaxLength(60)]
        public virtual string DMA052 { get; set; }

        [MaxLength(8)]
        public virtual string DMA053 { get; set; }

        [MaxLength(8)]
        public virtual string DMA054 { get; set; }

        [MaxLength(12)]
        public virtual string DMA055 { get; set; }

        [MaxLength(35)]
        public virtual string DMA056 { get; set; }

        [MaxLength(100)]
        public virtual string DMA057 { get; set; }

        [MaxLength(1)]
        public virtual string DMA601 { get; set; }

        [MaxLength(1)]
        public virtual string DMA602 { get; set; }

        [MaxLength(3)]
        public virtual string DMA701 { get; set; }

        [MaxLength(15)]
        public virtual string DMA702 { get; set; }

        public virtual Guid? DMA800 { get; set; }

        public virtual Guid? DMA801 { get; set; }

        [MaxLength(3)]
        public virtual string DMA851 { get; set; }

        [MaxLength(15)]
        public virtual string DMA852 { get; set; }

        [MaxLength(3)]
        public virtual string DMA854 { get; set; }

        [MaxLength(15)]
        public virtual string DMA855 { get; set; }

        [MaxLength(3)]
        public virtual string DMA857 { get; set; }

        [MaxLength(15)]
        public virtual string DMA858 { get; set; }

        [MaxLength(3)]
        public virtual string DMA992 { get; set; }

        public virtual int? DMA993 { get; set; }

        public virtual int? DMA994 { get; set; }

        [MaxLength(5)]
        public virtual string DMA995 { get; set; }

        [MaxLength(3)]
        public virtual string DMA996 { get; set; }

        [MaxLength(8)]
        public virtual string DMA997 { get; set; }

        [MaxLength(16)]
        public virtual string DMA928 { get; set; }

        [MaxLength(1)]
        public virtual string DMA999 { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}