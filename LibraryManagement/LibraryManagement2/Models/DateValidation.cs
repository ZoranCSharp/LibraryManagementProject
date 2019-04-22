using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement2.Models
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var issue = (Issued)validationContext.ObjectInstance;
            if (issue.ReturnDate.HasValue)
            {
                if (issue.IssuedDate < issue.ReturnDate)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Return date must be after issued date");
            }
            else return ValidationResult.Success;
        }
    }
}