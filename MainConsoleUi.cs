using PPM.Domain;

namespace PPM.UIConsole
{
    public class MainConsoleUi
    {
        public void AllMenu()
        {
            ProjectConsoleUi projectConsoleUi = new();
            EmployeeConsoleUi employeeConsoleUi = new();
            RoleConsoleUi roleConsoleUi = new();
            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        projectConsoleUi.ProjectModule();
                        break;
                    case "2":
                        employeeConsoleUi.EmployeeModule();
                        break;
                    case "3":
                        roleConsoleUi.RoleModule();
                        break;
                    case "4":
                        SaveAppData();
                        break;
                    case "5":
                        Exit();
                        return;
                    default:
                        Default();
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine(" Project Module:");
            Console.WriteLine(" Employee Module:");
            Console.WriteLine(" Role Module:");
            Console.WriteLine(" Save");
            Console.WriteLine(" Quit");
            Console.Write("Enter your choice: ");
        }

        public static void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please select a valid option.");
            Console.ResetColor();
        }

        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Thank you for chosing PPM!!");
            Console.ResetColor();
        }

        public void SaveAppData()
        {
            string projectPath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Project.xml";
            string employeePath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Employee.xml";
           
            string rolePath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Role.xml";

            AppDataSerializer.SerializeData(
                ProjectRepository.projects,
                EmployeeRepository.employees,
                RoleRepository.roles,
                projectPath,
                employeePath,
                rolePath
            );
            Console.WriteLine("Application data saved successfully.");
        }
    }
}
