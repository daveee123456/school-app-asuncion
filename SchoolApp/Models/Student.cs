
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolApp.Validation;

namespace SchoolApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [RegularExpression(@"^\d{4}-\d{4}$",
            ErrorMessage = "Student Number must follow the format 1234-5678.")]
        [Display(Name = "Student Number")]
        public string? StudentNo { get; set; }

        [Required(ErrorMessage = "Name is required. Dave")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required. Dave")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please confirm your email. Dave")]
        [Compare("Email", ErrorMessage = "The email addresses do not match.")]
        [Display(Name = "Confirm Email Address")]
        public string? ConfirmEmail { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Range(16, 60, ErrorMessage = "Age must be between 16 and 60.")]
        public int? Age { get; set; }

        [NotFutureDate(ErrorMessage = "Birth Date cannot be in the future.")]
        public DateTime? BirthDate { get; set; }
    }
}







