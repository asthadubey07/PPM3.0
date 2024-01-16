using PPM.Domain;

namespace PPM.UIConsole
{
    public class ProjectEmployeeUi
    {
        // Create an instance of ProjectEmployeeRepository
        readonly ProjectEmployeeRepository projectEmployeeManager = new();

        // Method to add an employee to a project
        public void AddEmployeeToProject()
        {
            Console.WriteLine("Enter the Project ID: ");
            string projectId = Console.ReadLine();

            Console.WriteLine("Enter the Employee ID: ");
            string employeeId = Console.ReadLine();

            bool success = projectEmployeeManager.AddEmployeeToProject(projectId, employeeId);

            if (success)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Employee added to the project successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Failed to add employee to the project. Check the provided IDs and project's employee list."
                );
                Console.ResetColor();
            }
        }

        // Method to delete an employee from a project
        public void DeleteEmployeeFromProject()
        {
            Console.WriteLine("Enter Project ID: ");
            string projectId = Console.ReadLine();
            Console.WriteLine("Enter Employee ID: ");
            string employeeId = Console.ReadLine();

            try
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                projectEmployeeManager.DeleteEmployeeFromProject(projectId, employeeId);
                Console.WriteLine("Employee deleted from the project.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        // // Method to view employee details by role in a project
        // public void ViewProjectEmployeesByRole()
        // {
        //     Console.WriteLine("Enter Project ID: ");
        //     string projectId = Console.ReadLine();

        //     var employeesByRole = projectEmployeeManager.GetEmployeesInProjectByRole(projectId);

        //     if (employeesByRole != null)
        //     {
        //         foreach (var role in employeesByRole)
        //         {
        //             Console.WriteLine($"Role: {role.Key}");
        //             foreach (var employee in role.Value)
        //             {
        //                 Console.ForegroundColor = ConsoleColor.DarkYellow;
        //                 Console.WriteLine(
        //                     $"Employee ID: {employee.Id}, FirstName: {employee.FirstName} LastName:{employee.LastName}"
        //                 );
        //                 Console.ResetColor();
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Console.ForegroundColor = ConsoleColor.Red;
        //         Console.WriteLine("Project not found.");
        //         Console.ResetColor();
        //     }
        // }
    }
}
