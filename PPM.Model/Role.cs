using System.ComponentModel.DataAnnotations;

namespace PPM.Model
{
    // Represents a role in the system.
    public class Role
    {
        // Unique identifier for the role.
        [Required(ErrorMessage = "Role ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Role ID must be a positive number")]
        public int RoleId { get; set; }

        // Name of the role.
        [Required(ErrorMessage = "Role Name is required")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Role Name must start with a Capital Letter")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "Role Name must be between 4 and 60 characters")]
        public string? Name { get; set; }

        // Date and time when the role was created.
        public DateTime CreatedOn { get; set; }

        // Date and time when the role was last modified.
        public DateTime ModifiedOn { get; set; }
    }
}
