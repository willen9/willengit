using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wmsapi.Models
{
    public class SMSCA
    {
        [MaxLength(10)]
        public virtual string SYSID { get; set; }

        [MaxLength(5)]
        public string SCA001 { get; set; }

        [MaxLength(15)]
        public string SCA002 { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}