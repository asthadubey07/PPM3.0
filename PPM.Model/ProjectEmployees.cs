using System;
using System.ComponentModel.DataAnnotations;

namespace PPM.Model
{
    // Represents the association between projects and employees.
    public class ProjectEmployees
    {
        // Identifier for the project.
        public int projectId { get; set; }

        // Identifier for the employee.
        public int employeeId { get; set; }

        // Date and time when the association was created.
        [Required(ErrorMessage = "CreatedOn is required.")]
        public DateTime CreatedOn { get; set; }

        // Date and time when the association was last modified.
        [Required(ErrorMessage = "ModifiedOn is required.")]
        public DateTime ModifiedOn { get; set; }
    }
}
