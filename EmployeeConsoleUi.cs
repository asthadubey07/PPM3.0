using PPM.Domain;

namespace PPM.UIConsole
{
    public class EmployeeConsoleUi
    {
        EmployeeUi employeeUi = new();

        public void EmployeeModule()
        {
            while (true)
            {
                Console.WriteLine("Employee Module:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. List All Employees");
                Console.WriteLine("3. List Employee By Id");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        employeeUi.AddEmployees();
                        break;
                    case "2":
                        employeeUi.ViewEmployees();
                        break;
                    case "3":
                        employeeUi.GetEmployeeById();
                        break;
                    case "4":
                        employeeUi.DeleteEmployee();
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
