using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Currency
    {
        public string Company { get; set; }
        public string UserGroup { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Modifier { get; set; }
        public string ModiDate { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string EffectiveDate { get; set; }
        public string ExchangeRate { get; set; }
        public string Remark { get; set; }
    }
}