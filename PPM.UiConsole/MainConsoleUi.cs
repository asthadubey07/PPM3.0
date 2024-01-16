using PPM.Domain;

namespace PPM.UIConsole
{
    public class MainConsoleUi
    {
        // Method to display all available modules and handle user input
        public void AllMenu()
        {
            // Create instances of different module UIs
            ProjectConsoleUi projectConsoleUi = new();
            EmployeeConsoleUi employeeConsoleUi = new();
            RoleConsoleUi roleConsoleUi = new();
            
            while (true)
            {
                ShowMainMenu(); // Display the main menu options
                string choice = Console.ReadLine();
                
                // Switch based on user's choice
                switch (choice)
                {
                    case "1":
                        projectConsoleUi.ProjectModule(); // Redirect to the Project module
                        break;
                    case "2":
                        employeeConsoleUi.EmployeeModule(); // Redirect to the Employee module
                        break;
                    case "3":
                        roleConsoleUi.RoleModule(); // Redirect to the Role module
                        break;
                    case "4":
                        SaveAppData(); // Save application data
                        break;
                    case "5":
                        Exit(); // Exit the application
                        return;
                    default:
                        Default(); // Display a message for invalid choices
                        break;
                }
            }
        }

        // Display the main menu options
        static void ShowMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine(" Press 1 For Project Module:");
            Console.WriteLine(" Press 2 For Employee Module:");
            Console.WriteLine(" Press 3 For Role Module:");
            Console.WriteLine(" Press 4 To Save:");
            Console.WriteLine(" Press 5 To Quit:");
            Console.Write("Enter your choice:: ");
        }

        // Display a message for invalid choices
        public static void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please select a valid option.");
            Console.ResetColor();
        }

        // Display a message on application exit
        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Thank you for choosing PPM!!");
            Console.ResetColor();
        }

        // Save application data to XML files
        public void SaveAppData()
        {
            // Set file paths for project, employee, and role data
            string projectPath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Project.xml";
            string employeePath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Employee.xml";
            string rolePath = "C:\\Users\\ADwivedi\\Desktop\\ppmProject\\PPM.xml\\Role.xml";

            // Serialize and save data to XML files
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
