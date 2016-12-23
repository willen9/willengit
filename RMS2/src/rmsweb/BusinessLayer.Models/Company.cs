using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
    public class Company
    {
        public string UserGroup { get; set; }

        public string Creator { get; set; }

        public string CreateDate { get; set; }

        public string Modifier { get; set; }

        public string ModiDate { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyFName { get; set; }

        public string TelNo { get; set; }

        public string FaxNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }
    }
}