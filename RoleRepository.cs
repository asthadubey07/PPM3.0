using PPM.Model;

namespace PPM.Domain
{
    public class RoleRepository : IEntityOperation<Role>
    {
        public static List<Role> roles = new List<Role>();

        //add role
        public void Add(Role role)
        {
            roles.Add(role);
        }

        //view Roles
        public List<Role> ListAll()
        {
            return roles;
        }

        //Delete Roles
        public void Delete(string roleId)
        {
            Role roleToDelete = roles.Find(r => r.Id == roleId);
            if (roleToDelete != null)
            {
                roles.Remove(roleToDelete);
            }
        }

        //Get role by ID
        public Role ListByID(string roleId)
        {
            // Find the Role with the given ID
            Role role = roles.Find(r => r.Id == roleId);
            return role; // Return the Role (or null if not found)
        }

        //VALIDATION PART
        //RoleExists
        public bool RoleExists(string roleId)
        {
            return roles.Exists(role => role.Id == roleId);
        }

        // Check if the role ID already exists
        public bool IsRoleIdUnique(string roleId)
        {
            return !roles.Exists(r => r.Id == roleId);
        }

        // Role ID validation for null value
        public static bool ValidateRoleId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }
            return true;
        }

        // Role Name validation for null value
        public static bool ValidateRoleName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            return true;
        }

        public bool IsRoleNameUnique(string roleName)
        {
            return !roles.Exists(r => r.Name == roleName);
        }
    }
}
