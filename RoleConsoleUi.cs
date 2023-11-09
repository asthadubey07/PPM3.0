using PPM.Domain;

namespace PPM.UIConsole
{
    public class RoleConsoleUi
    {
        public void RoleModule()
        {
            RoleUi roleUi = new();
            while (true)
            {
                Console.WriteLine("Role Module:");
                Console.WriteLine("1. Add Role");
                Console.WriteLine("2. List All Roles");
                Console.WriteLine("3. List Role By Id");
                Console.WriteLine("4. Delete Role");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        roleUi.AddRoles();
                        break;
                    case "2":
                        roleUi.ViewRoles();
                        break;
                    case "3":
                        roleUi.GetRoleById();
                        break;
                    case "4":
                        roleUi.DeleteRole();
                        break;
                    case "5":
                        return; // Return to the main menu
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
