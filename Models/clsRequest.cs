using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace donationApi.Models
{
    public class clsRequest
    {
        public int zehutID { get; set; }
        public List<clsDonation> donations { get; set; }
    }
}