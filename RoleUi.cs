using PPM.Domain;
using PPM.Model;

namespace PPM.UIConsole
{
    public class RoleUi
    {
        RoleRepository roleManager = new RoleRepository();

        //add Role
        public void AddRoles()
        {
            Console.Write("HOW MANY ROLES YOU WANT TO ADD \n please enter the value in numbers:");
            if (int.TryParse(Console.ReadLine(), out int roleCount) && roleCount > 0)
            {
                for (int i = 0; i < roleCount; i++)
                {
                    Role role = new Role();
                    Console.WriteLine("Add Role:");
                    while (true)
                    {
                        Console.Write("Enter Role ID: ");
                        string id = Console.ReadLine();
                        if (RoleRepository.ValidateRoleId(id) && roleManager.IsRoleIdUnique(id))
                        {
                            role.Id = id;
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

                    while (true)
                    {
                        Console.Write("Enter Role Name: ");
                        string name = Console.ReadLine();
                        if (
                            RoleRepository.ValidateRoleName(name)
                            && roleManager.IsRoleNameUnique(name)
                        )
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
                            Console.WriteLine(
                                "Role Name is not unique. Please enter a unique Name."
                            );
                        }
                        Console.ResetColor();
                    }

                    roleManager.Add(role);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Role Added sucscessfully.");
                    Console.ResetColor();
                }
            }
        }

        // view Roles
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
                    Console.WriteLine($"Role ID   : {role.Id}");
                    Console.WriteLine($"Role Name : {role.Name}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        //Delete Role
        public void DeleteRole()
        {
            EmployeeRepository employeeManager = new();
            Role role = new();
            while (true)
            {
                // Get Role ID from the user and perform validation
                Console.Write("Enter Role ID to delete: ");
                string roleId = Console.ReadLine();
                if (RoleRepository.ValidateRoleId(roleId))
                {
                    role.Id = roleId;
                    if (employeeManager.IsRoleAssignedToEmployees(roleId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Cannot delete the role. The role is assigned to one or more employees."
                        );
                        Console.ResetColor();
                        break;
                    }
                    // bool result = roleManager.DeleteRole(roleId);
                    roleManager.Delete(roleId);
                    // if (result)
                    // {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Role deleted successfully.");
                    Console.ResetColor();
                    // break;
                    // }
                    // else
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Red;
                    //     Console.WriteLine("Failed to delete the Role : Role ID you Provide Is Not Exist.");
                    //     Console.ResetColor();
                    //     return;
                    // }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Role ID is required.");
                    Console.ResetColor();
                }
            }
        }

        //get Role by id
        public void GetRoleById()
        {
            // RoleRepository roleManager = new();
            Role role1 = new();
            while (true)
            {
                // Get Role ID from the user
                Console.Write("Enter Role ID to retrieve: ");
                string roleId = Console.ReadLine();
                if (RoleRepository.ValidateRoleId(roleId))
                {
                    role1.Id = roleId;
                    Role role = roleManager.ListByID(roleId);
                    if (role != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Role ID   : {role.Id}");
                        Console.WriteLine($"Role Name : {role.Name}");
                        Console.ResetColor();
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
