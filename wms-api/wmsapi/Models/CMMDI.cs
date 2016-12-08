using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class CMMDI
    {
        [Required]
        [MaxLength(12)]
        public virtual string MDI001 { get; set; }

        [MaxLength(20)]
        public virtual string MDI002 { get; set; }

        [MaxLength(60)]
        public virtual string MDI003 { get; set; }

        [MaxLength(60)]
        public virtual string MDI004 { get; set; }

        [MaxLength(12)]
        public virtual string MDI005 { get; set; }

        [MaxLength(30)]
        public virtual string MDI006 { get; set; }

        [MaxLength(3)]
        public virtual string MDI007 { get; set; }

        [MaxLength(16)]
        public virtual string MDI008 { get; set; }

        [MaxLength(255)]
        public virtual string MDI010 { get; set; }

        [MaxLength(30)]
        public virtual string MDI011 { get; set; }

        [MaxLength(30)]
        public virtual string MDI012 { get; set; }

        [MaxLength(20)]
        public virtual string MDI013 { get; set; }

        [MaxLength(20)]
        public virtual string MDI014 { get; set; }

        [MaxLength(20)]
        public virtual string MDI015 { get; set; }

        [MaxLength(100)]
        public virtual string MDI016 { get; set; }

        [MaxLength(8)]
        public virtual string MDI017 { get; set; }

        [MaxLength(8)]
        public virtual string MDI018 { get; set; }

        [MaxLength(8)]
        public virtual string MDI021 { get; set; }

        [MaxLength(100)]
        public virtual string MDI022 { get; set; }

        [MaxLength(30)]
        public virtual string MDI023 { get; set; }

        [MaxLength(8)]
        public virtual string MDI024 { get; set; }

        [MaxLength(100)]
        public virtual string MDI025 { get; set; }

        [MaxLength(30)]
        public virtual string MDI026 { get; set; }

        [MaxLength(2)]
        public virtual string MDI033 { get; set; }

        [MaxLength(50)]
        public virtual string MDI034 { get; set; }

        [MaxLength(8)]
        public virtual string MDI051 { get; set; }

        [MaxLength(8)]
        public virtual string MDI052 { get; set; }

        [MaxLength(1)]
        public virtual string MDI061 { get; set; }

        [MaxLength(1)]
        public virtual string MDI062 { get; set; }

        [MaxLength(100)]
        public virtual string Creator { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public virtual string Modifier { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }
    }
}