using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace donationApi.CustomValidation
{
    public class HebrewValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //if (value == null | value.ToString() ==string.Empty) { return ValidationResult.Success; }
            if (value == null || value.ToString() == "") { return ValidationResult.Success; }


            string name = value.ToString().Trim();

            bool valid = true;
              
            Regex regex = new Regex("([א-ת])$");
            if (!regex.IsMatch(name))
            {
                valid = false;
            }


            if (valid)
            {
                return ValidationResult.Success;
            }
            string ErrorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(ErrorMessage);
        } 
        
       

    
    }
}