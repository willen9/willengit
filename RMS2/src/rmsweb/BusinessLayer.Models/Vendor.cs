using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Vendor
    {
        public string Company { get; set; }
        public string UserGroup { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Modifier { get; set; }
        public string ModiDate { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorFName { get; set; }
        public string CurrencyId { get; set; }
        public string TaxRate { get; set; }
        public string Contact { get; set; }
        public string ContactName { get; set; }
        public string TelNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string AddressSName { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
    }
}