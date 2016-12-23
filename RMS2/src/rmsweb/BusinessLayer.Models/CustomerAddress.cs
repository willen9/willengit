using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class CustomerAddress
    {
        public string Company { get; set; }
        public string UserGroup { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Modifier { get; set; }
        public string ModiDate { get; set; }
        public string CustomerId { get; set; }
        public string AddressId { get; set; }
        public string AddressSName { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string FaxNo { get; set; }
        public string Remark { get; set; }
        public string Contact { get; set; }
    }
}