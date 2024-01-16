using PPM.Domain;
using PPM.Model;

namespace PPM.UIConsole
{
    public class RoleUi
    {
        // Create an instance of RoleRepository
       readonly RoleRepository  roleManager = new();

        // Add Roles method
        public void AddRoles()
        {
            Console.Write("HOW MANY ROLES YOU WANT TO ADD \n please enter the value in numbers:");
            if (int.TryParse(Console.ReadLine(), out int roleCount) && roleCount > 0)
            {
                for (int i = 0; i < roleCount; i++)
                {
                    Role role = new Role();
                    Console.WriteLine("Add Role:");

                    // Validate and set Role ID
                    while (true)
                    {
                        Console.Write("Enter Role ID: ");
                        string id = Console.ReadLine();
                        if (RoleRepository.ValidateRoleId(id) && roleManager.IsRoleIdUnique(id))
                        {
                            role.RoleId = id;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (!RoleRepository.ValidateRoleId(id))
                        {
                            Console.WriteLine("Invalid Role ID : Role ID Can Not Be Empty.");
                        }
                        else
                        {
                            Console.WriteLine("Role ID is not unique. Please enter a unique ID.");
                        }
                        Console.ResetColor();
                    }

                    // Validate and set Role Name
                    while (true)
                    {
                        Console.Write("Enter Role Name: ");
                        string name = Console.ReadLine();
                        if (RoleRepository.ValidateRoleName(name) && roleManager.IsRoleNameUnique(name))
                        {
                            role.Name = name;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (!RoleRepository.ValidateRoleName(name))
                        {
                            Console.WriteLine("Invalid Role Name : Role Name Can Not Be Empty.");
                        }
                        else
                        {
                            Console.WriteLine("Role Name is not unique. Please enter a unique Name.");
                        }
                        Console.ResetColor();
                    }

                    // Add the role to the manager
                    roleManager.Add(role);

                    // Display success or failure messages
                    if (roleManager.RoleExists(role.RoleId))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Role Added successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cannot add Role. Please try again later!.");
                        Console.ResetColor();
                    }
                }
            }
        }

        // View Roles method
        public void ViewRoles()
        {
            var roles = roleManager.ListAll();

            if (roles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No roles found.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("List of roles:");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.ResetColor();
                Console.ResetColor();

                foreach (var role in roles)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Role ID   : {role.RoleId}");
                    Console.WriteLine($"Role Name : {role.Name}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        // Delete Role method
        public void DeleteRole()
        {
            EmployeeRepository employeeManager = new();
            Role role = new();

            // Get Role ID from the user and perform validation
            while (true)
            {
                Console.Write("Enter Role ID to delete: ");
                string roleId = Console.ReadLine();
                if (RoleRepository.ValidateRoleId(roleId))
                {
                    role.RoleId = roleId;

                    // Check if the role is assigned to employees
                    if (employeeManager.IsRoleAssignedToEmployees(roleId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Cannot delete the role. The role is assigned to one or more employees."
                        );
                        Console.ResetColor();
                        return;
                    }

                    // Delete the role and display success or failure messages
                    if (roleManager.RoleExists(roleId))
                    {
                        roleManager.Delete(roleId);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        if (roleManager.RoleExists(roleId))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cannot delete the Role. Please try again later.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Role deleted successfully.");
                            Console.ResetColor();
                        }
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Failed to delete the Role : Role ID you Provide Is Not Exist."
                        );
                        Console.ResetColor();
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Role ID is required.");
                    Console.ResetColor();
                }
            }
        }

        // Get Role by ID method
        public void GetRoleById()
        {
            Role role1 = new();

            // Get Role ID from the user
            while (true)
            {
                Console.Write("Enter Role ID to retrieve: ");
                string roleId = Console.ReadLine();
                if (RoleRepository.ValidateRoleId(roleId))
                {
                    role1.RoleId = roleId;

                    // Retrieve and display the role or show an error message
                    Role role = roleManager.ListByID(roleId);
                    if (role != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Role ID   : {role.RoleId}");
                        Console.WriteLine($"Role Name : {role.Name}");
                        Console.ResetColor();
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Role not found:  Role ID you Provide Is Not Exist.");
                        Console.ResetColor();
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Role ID is required.");
                    Console.ResetColor();
                }
            }
        }
    }
}
