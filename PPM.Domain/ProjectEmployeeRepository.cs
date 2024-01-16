using PPM.DAL;

namespace PPM.Domain
{
    public class ProjectEmployeeRepository
    {
        // Instance of ProjectEmployeeDal to interact with data access layer
       readonly ProjectEmployeeDal projectEmployeeDal = new();

        // Adds an employee to a project.
        public bool AddEmployeeToProject(string projectId, string employeeId)
        {
            try
            {
                return projectEmployeeDal.AddEmployeeToProject(projectId, employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee to project: {ex.Message}");

                // Print details of the inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return false;
            }
        }

        // Deletes an employee from a project.
        public void DeleteEmployeeFromProject(string projectId, string employeeId)
        {
            try
            {
                projectEmployeeDal.DeleteEmployeeFromProject(projectId, employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee from project: {ex.Message}");
            }
        }

        // GetEmployeesInProjectByRole
        // public Dictionary<string, List<Employee>> GetEmployeesInProjectByRole(string projectId)
        // {
        //     return projectEmployeeDal.GetEmployeesInProjectByRole(projectId);
        // }
    }
}
