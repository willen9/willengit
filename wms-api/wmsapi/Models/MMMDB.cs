using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class MMMDB
    {
        [Required]
        [MaxLength(45)]
        public virtual string MDB001 { get; set; }

        [MaxLength(100)]
        public virtual string MDB002 { get; set; }

        [MaxLength(100)]
        public virtual string MDB003 { get; set; }

        [MaxLength(100)]
        public virtual string MDB004 { get; set; }

        [MaxLength(12)]
        public virtual string MDB005 { get; set; }

        [MaxLength(12)]
        public virtual string MDB006 { get; set; }

        [MaxLength(30)]
        public virtual string MDB007 { get; set; }

        [MaxLength(10)]
        public virtual string MDB008 { get; set; }

        [MaxLength(5)]
        public virtual string MDB009 { get; set; }

        [MaxLength(5)]
        public virtual string MDB010 { get; set; }

        [MaxLength(5)]
        public virtual string MDB011 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB012 { get; set; }

        [MaxLength(12)]
        public virtual string MDB013 { get; set; }

        [MaxLength(12)]
        public virtual string MDB014 { get; set; }

        [MaxLength(1)]
        public virtual string MDB015 { get; set; }

        [MaxLength(1)]
        public virtual string MDB016 { get; set; }

        [MaxLength(1)]
        public virtual string MDB017 { get; set; }

        [MaxLength(1)]
        public virtual string MDB018 { get; set; }

        [MaxLength(1)]
        public virtual string MDB019 { get; set; }

        [MaxLength(8)]
        public virtual string MDB020 { get; set; }

        [MaxLength(8)]
        public virtual string MDB021 { get; set; }

        [MaxLength(255)]
        public virtual string MDB022 { get; set; }

        [MaxLength(45)]
        public virtual string MDB023 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB024 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB025 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB026 { get; set; }

        [MaxLength(5)]
        public virtual string MDB027 { get; set; }

        [MaxLength(25)]
        public virtual string MDB028 { get; set; }

        [MaxLength(5)]
        public virtual string MDB029 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB030 { get; set; }

        [MaxLength(10)]
        public virtual string MDB031 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB035 { get; set; }

        [Column(TypeName = "numeric")]
        public virtual decimal? MDB036 { get; set; }

        [MaxLength(1)]
        public virtual string MDB040 { get; set; }

        public virtual int? MDB041 { get; set; }

        public virtual int? MDB042 { get; set; }

        public virtual int? MDB043 { get; set; }

        public virtual int? MDB044 { get; set; }

        [MaxLength(8)]
        public virtual string MDB051 { get; set; }

        [MaxLength(8)]
        public virtual string MDB052 { get; set; }

        [MaxLength(8)]
        public virtual string MDB053 { get; set; }

        [MaxLength(8)]
        public virtual string MDB054 { get; set; }

        [MaxLength(8)]
        public virtual string MDB055 { get; set; }

        [MaxLength(8)]
        public virtual string MDB056 { get; set; }

        [MaxLength(20)]
        public virtual string MDB057 { get; set; }

        [MaxLength(20)]
        public virtual string MDB058 { get; set; }

        [MaxLength(20)]
        public virtual string MDB059 { get; set; }

        [MaxLength(20)]
        public virtual string MDB060 { get; set; }

        [MaxLength(1)]
        public virtual string MDB061 { get; set; }

        [MaxLength(1)]
        public virtual string MDB062 { get; set; }

        [MaxLength(1)]
        public virtual string MDB063 { get; set; }

        [MaxLength(1)]
        public virtual string MDB064 { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(16)]
        public virtual string Creator { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        [MaxLength(16)]
        public virtual string Modifier { get; set; }
    }
}