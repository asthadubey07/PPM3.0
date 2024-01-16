using System.ComponentModel.DataAnnotations;

namespace PPM.Model
{
    public class Employee
    {
        // Employee ID property with validation attributes
        [Required(ErrorMessage = "Employee ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number")]
        public int EmployeeId { get; set; }

        // First Name property with validation attributes
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Invalid First Name ")]
        [StringLength(
            60,
            MinimumLength = 2,
            ErrorMessage = "First Name must be between 2 and 60 characters"
        )]
        public string? FirstName { get; set; }

        // Last Name property with validation attributes
        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Invalid Last Name ")]
        [StringLength(
            60,
            MinimumLength = 2,
            ErrorMessage = "Last Name must be between 2 and 60 characters"
        )]
        public string? LastName { get; set; }

        // Email property with validation attributes
        [RegularExpression(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}"
                + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\"
                + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Invalid Email Address."
        )]
        [Required(ErrorMessage = " Email is required.")]
        public string? Email { get; set; }

        // PhoneNumber property with validation attributes
        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression(
            @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|
        (^[0-9]{3}-[0-9]{4}-[0-9]{4}$)",
            ErrorMessage = "Invalid Phone Number"
        )]
        public string? PhoneNumber { get; set; }

        // Password property with validation attributes
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(
            @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Invalid Pasword "
        )]
        public string? Password { get; set; }

        // Address property with validation attributes
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        // RoleId property representing the role of the employee
        public int RoleId { get; set; }

        // CreatedOn property representing the creation timestamp
        public DateTime CreatedOn { get; set; }

        // ModifiedOn property representing the last modification timestamp
        public DateTime ModifiedOn { get; set; }
    }
}
