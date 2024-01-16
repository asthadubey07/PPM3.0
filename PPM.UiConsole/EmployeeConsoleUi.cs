using PPM.Domain;

namespace PPM.UIConsole
{
    public class EmployeeConsoleUi
    {
        // Create an instance of EmployeeUi
       readonly EmployeeUi employeeUi = new();

        // Method to handle the Employee Module functionality
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
                
                // Read user input for the desired action
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Invoke the method to add employees
                        employeeUi.AddEmployees();
                        break;
                    case "2":
                        // Invoke the method to view all employees
                        employeeUi.ViewEmployees();
                        break;
                    case "3":
                        // Invoke the method to get employee details by ID
                        employeeUi.GetEmployeeById();
                        break;
                    case "4":
                        // Invoke the method to delete an employee
                        employeeUi.DeleteEmployee();
                        break;
                    case "5":
                        return; // Return to the main menu
                    default:
                        // Display an error message for an invalid choice
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
