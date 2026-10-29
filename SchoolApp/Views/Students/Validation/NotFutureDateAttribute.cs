
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Validation
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is DateTime date)
            {
                return date.Date <= DateTime.Today;
            }

            return false;
        }
    }
}

