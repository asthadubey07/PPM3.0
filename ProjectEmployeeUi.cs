using PPM.Domain;

namespace PPM.UIConsole
{
    public class ProjectEmployeeUi
    {
        ProjectEmployeeRepository projectEmployeeManager = new();

        //ADD EMPLOYEE TO PROJECT
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

        //DELETE EMPLOYEE FROM PROJECT
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

        //VIEW EMPLOYEE DETAILS BY ROLE
        public void ViewProjectEmployeesByRole()
        {
            Console.WriteLine("Enter Project ID: ");
            string projectId = Console.ReadLine();

            var employeesByRole = projectEmployeeManager.GetEmployeesInProjectByRole(projectId);

            if (employeesByRole != null)
            {
                foreach (var role in employeesByRole)
                {
                    Console.WriteLine($"Role: {role.Key}");
                    foreach (var employee in role.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(
                            $"Employee ID: {employee.Id}, FirstName: {employee.FristName} LastName:{employee.LastName}"
                        );
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Project not found.");
                Console.ResetColor();
            }
        }
    }
}
