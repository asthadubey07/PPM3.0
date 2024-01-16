using PPM.Model;
using PPM.DAL;

namespace PPM.Domain
{
    public class RoleRepository : IEntityOperation<Role>
    {
        // List to store roles in memory
        public readonly static List<Role> roles = new List<Role>();

        // Instance of RoleDal to interact with data access layer
        readonly RoleDal roleDal = new RoleDal();

        // Adds a new role.
        public void Add(Role role)
        {
            try
            {
                roleDal.AddRole(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding role: {ex.Message}");

                // Details of the inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        // Retrieves a list of all roles.
        public List<Role> ListAll()
        {
            try
            {
                return roleDal.ListAllRole();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles: {ex.Message}");
                return new List<Role>();
            }
        }

        // Deletes a role by its ID.
        public void Delete(string roleId)
        {
            try
            {
                roleDal.DeleteRole(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting role: {ex.Message}");
            }
        }

        // Retrieves a role by its ID.
        public Role? ListByID(string roleId)
        {
            try
            {
                // Find the Role with the given ID
                return roleDal.ListByIDRole(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving role: {ex.Message}");
                return null;
            }
        }

        // VALIDATION PART

        // Checks if a role with the specified ID exists.
        public bool RoleExists(string roleId)
        {
            try
            {
                return roleDal.RoleExists(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role exists: {ex.Message}");
                return false;
            }
        }

        // Checks if a role ID is unique.
        public bool IsRoleIdUnique(string roleId)
        {
            try
            {
                return roleDal.IsRoleIdUnique(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking role ID uniqueness: {ex.Message}");
                return false;
            }
        }

        // Validates a role ID for null value.
        public static bool ValidateRoleId(string Id)
        {
            try
            {
                return RoleDal.ValidateRoleId(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating role ID: {ex.Message}");
                return false;
            }
        }

        // Validates a role name for null value.
        public static bool ValidateRoleName(string Name)
        {
            try
            {
                return RoleDal.ValidateRoleName(Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating role name: {ex.Message}");
                return false;
            }
        }

        // Checks if a role name is unique.
        public bool IsRoleNameUnique(string roleName)
        {
            try
            {
                return roleDal.IsRoleNameUnique(roleName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role name is unique: {ex.Message}");
                return false;
            }
        }
    }
}
