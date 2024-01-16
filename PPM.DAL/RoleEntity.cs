using PPM.Model;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class RoleDal
    {
        private readonly PPMContext RoleContext;

        // Constructor with dependency injection
        public RoleDal(PPMContext context)
        {
            RoleContext = context;
        }
        // public RoleDal()

        // {
        //    RoleContext = new PPMContext();
        // }
        // CREATE
        // Adds a role to the database.
        public void AddRole(Role role)
        {
            try
            {
                role.CreatedOn = DateTime.Now;
                role.ModifiedOn = DateTime.Now;

                RoleContext.Role.Add(role);
                RoleContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding role to the database: {ex.Message}");

                // Print details of the inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        // READ
        // Retrieves a list of all roles from the database.
        public List<Role> ListAllRole()
        {
            try
            {
                return RoleContext.Role.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles from the database: {ex.Message}");
                return new List<Role>();
            }
        }

        // DELETE
        // Deletes a role from the database based on the role ID.
        public void DeleteRole(int roleId)
        {
            try
            {
                var role = RoleContext.Role.Find(roleId);
                if (role != null)
                {
                    RoleContext.Role.Remove(role);
                    RoleContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting role from the database: {ex.Message}");
            }
        }

        // GetById
        // Retrieves a role from the database based on the role ID.
        public Role? ListByID(int roleId)
        {
            try
            {
                return RoleContext.Role.Find(roleId);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error retrieving role from the database: {ex.Message}");
                return null;
            }
        }

        // Update a role
        // Updates an existing role in the database.
        public void Update(Role updatedRole)
        {
            var existingRole = RoleContext.Role.SingleOrDefault(
                r => r.RoleId == updatedRole.RoleId
            );
            if (existingRole != null)
            {
                existingRole.Name = updatedRole.Name;
                existingRole.ModifiedOn = DateTime.UtcNow;

                // Mark the entity as modified
                RoleContext.Entry(existingRole).State = EntityState.Modified;
                RoleContext.SaveChanges();
            }
        }

        // RoleExists
        // Checks if a role with the specified ID exists in the database.
        public bool RoleExists(int roleId)
        {
            try
            {
                return RoleContext.Role.Any(r => r.RoleId == roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role exists in the database: {ex.Message}");
                return false;
            }
        }

        // Check if the role ID is unique
        // Checks if a role ID is unique in the database.
        public bool IsRoleIdUnique(int roleId)
        {
            try
            {
                return !RoleContext.Role.Any(r => r.RoleId == roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error checking if role ID is unique in the database: {ex.Message}"
                );
                return false;
            }
        }

        // Check if the role name is unique
        // Checks if a role name is unique in the database.
        public bool IsRoleNameUnique(string roleName)
        {
            try
            {
                return !RoleContext.Role.Any(r => r.Name == roleName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error checking if role name is unique in the database: {ex.Message}"
                );
                return false;
            }
        }
        // Role Name validation for null value
        // Validates that a role name is not null or empty.
        public static bool ValidateRoleName(string roleName)
        {
            return !string.IsNullOrEmpty(roleName);
        }

        // Role ID validation for null value
        // public static bool ValidateRoleId(int roleId)
        // {
        //     return !int.IsNullOrEmpty(roleId);
        // }
    }
}
