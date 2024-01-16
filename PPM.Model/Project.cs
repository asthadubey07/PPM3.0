using System.ComponentModel.DataAnnotations;

namespace PPM.Model
{
    // Custom validation attribute to ensure that a date is greater than another date.
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (propertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance);

            if ((DateTime)value <= comparisonValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be greater than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }

    // Represents a project in the system.
    public class Project
    {
        // Unique identifier for the project.
        [Required(ErrorMessage = "Project ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Project ID must be a positive number")]
        public int ProjectId { get; set; }

        // Name of the project.
        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "Project Name must be between 4 and 60 characters")]
        public string? Name { get; set; }

        // Start date of the project.
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        // End date of the project, must be greater than the start date.
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be greater than Start Date.")]
        public DateTime EndDate { get; set; }

        // Date and time when the project was created.
        public DateTime CreatedOn { get; set; }

        // Date and time when the project was last modified.
        public DateTime ModifiedOn { get; set; }
    }
}
