using PPM.Domain;

namespace PPM.UIConsole
{
    public class RoleConsoleUi
    {
        // Method to handle the Role Module
        public void RoleModule()
        {
            // Create an instance of RoleUi
            RoleUi roleUi = new();

            // Display menu options and process user input
            while (true)
            {
                Console.WriteLine("Role Module:");
                Console.WriteLine("1. Add Role");
                Console.WriteLine("2. List All Roles");
                Console.WriteLine("3. List Role By Id");
                Console.WriteLine("4. Delete Role");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Enter your choice: ");

                // Read user input and perform the corresponding action
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Invoke the method to add roles
                        roleUi.AddRoles();
                        break;
                    case "2":
                        // Invoke the method to view all roles
                        roleUi.ViewRoles();
                        break;
                    case "3":
                        // Invoke the method to get role by ID
                        roleUi.GetRoleById();
                        break;
                    case "4":
                        // Invoke the method to delete a role
                        roleUi.DeleteRole();
                        break;
                    case "5":
                        return; // Return to the main menu
                    default:
                        // Display an error message for invalid choices
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
