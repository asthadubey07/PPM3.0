using PPM.Model;

namespace PPM.Domain
{
    public class ProjectEmployeeRepository
    {
        public ProjectRepository projectRepository = new();
        public EmployeeRepository employeeRepository = new();

        //AddEmployeeToProject
        public bool AddEmployeeToProject(string projectId, string employeeId)
        {
            // Check if the project exists
            Project project = projectRepository.ListByID(projectId);
            if (project == null)
            {
                // Project doesn't exist
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Project With This ID not exit");
                Console.ResetColor();
                return false;
            }

            // Check if the employee exists
            Employee employee = employeeRepository.ListByID(employeeId);
            if (employee == null)
            {
                // Employee doesn't exist
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee With This ID doesn't exist");
                Console.ResetColor();
                return false;
            }

            // Check if the employee is already in the project
            if (project.Employees.Contains(employee))
            {
                // Employee is already in the project
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee  With This ID Is Already In The Project");
                Console.ResetColor();
                return false;
            }

            // Add the employee to the project
            project.Employees.Add(employee);
            return true;
        }

        //DeleteEmployeeFromProject
        public bool DeleteEmployeeFromProject(string projectId, string employeeId)
        {
            // Implement validation logic
            if (!projectRepository.ProjectExists(projectId))
            {
                throw new Exception("Project not found");
            }

            if (!projectRepository.EmployeeExistsInProject(projectId, employeeId))
            {
                throw new Exception("Employee not found in the project");
            }

            // Retrieve the project and employee
            Project project = projectRepository.ListByID(projectId);
            Employee employee = employeeRepository.ListByID(employeeId);

            // Remove the employee from the project
            project.Employees.Remove(employee);
            return true;
        }

        //GetEmployeesInProjectByRole
        public Dictionary<string, List<Employee>> GetEmployeesInProjectByRole(string projectId)
        {
            Project project = projectRepository.ListByID(projectId);
            if (project != null)
            {
                var employeesInProject = project.Employees;
                var employeesByRole = new Dictionary<string, List<Employee>>();

                foreach (var employee in employeesInProject)
                {
                    if (!employeesByRole.ContainsKey(employee.RoleId))
                    {
                        employeesByRole[employee.RoleId] = new List<Employee>();
                    }

                    employeesByRole[employee.RoleId].Add(employee);
                }

                return employeesByRole;
            }
            // Project with the specified projectId does not exist
            return null;
        }
    }
}
