using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using donationApi.CustomValidation;

namespace donationApi.Models
{
    public class clsDonation
    {
         public int id { get; set; }
        [Required]
        [HebrewValidation(ErrorMessage = "שם פרטי בעברית בלבד")]
         public string name { get; set; }
        [Required]
         public string amount { get; set; }
        [Required]
         public int type { get; set; }
        [Required]
         public string designation { get; set; }
         public string conditions { get; set; }
        [Required]
         public int currency { get; set; }
        [Required]
         public int exchangeRate { get; set; }

    }
}