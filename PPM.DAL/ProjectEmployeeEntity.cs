using PPM.Model;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class ProjectEmployeeDal
    {
        private readonly PPMContext ProjectEmployeeContext;

        // Constructor with dependency injection
        public ProjectEmployeeDal(PPMContext context)
        {
            ProjectEmployeeContext = context;
        }
        // public ProjectEmployeeDal()
        // {

        //     ProjectEmployeeContext = new PPMContext();
        // }

        // Check if the project exists
        public bool ProjectExists(int projectId)
        {
            return ProjectEmployeeContext.Project.Any(p => p.ProjectId == projectId);
        }

        // Check if the employee exists
        public bool EmployeeExists(int employeeId)
        {
            return ProjectEmployeeContext.Employee.Any(e => e.EmployeeId == employeeId);
        }

        // Check if the employee is already in the project
        public bool EmployeeAlreadyInProject(int projectId, int employeeId)
        {
            return ProjectEmployeeContext.ProjectEmployees.Any(
                ep => ep.projectId == projectId && ep.employeeId == employeeId
            );
        }

        // Add employee to project
        public bool AddEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                if (!ProjectExists(projectId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Project with this ID does not exist.");
                    Console.ResetColor();
                    return false;
                }

                if (!EmployeeExists(employeeId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee with this ID does not exist.");
                    Console.ResetColor();
                    return false;
                }

                if (EmployeeAlreadyInProject(projectId, employeeId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee with this ID is already in the project.");
                    Console.ResetColor();
                    return false;
                }

                // Add the employee to the project
                ProjectEmployeeContext.ProjectEmployees.Add(
                    new ProjectEmployees
                    {
                        projectId = projectId,
                        employeeId = employeeId,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now
                    }
                );

                ProjectEmployeeContext.SaveChanges();

                // Successfully added employee to the project
                return true;
            }
            catch (Exception ex)
            {
                // Log or print the exception details for troubleshooting.
                Console.WriteLine($"Error adding employee to the project: {ex.Message}");
                return false;
            }
        }

        // Remove employee from project
        public bool DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                var projectExists = ProjectExists(projectId);
                var employeeExists = EmployeeExists(employeeId);

                if (!projectExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Project with this ID does not exist.");
                    Console.ResetColor();
                    return false;
                }

                if (!employeeExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee with this ID does not exist.");
                    Console.ResetColor();
                    return false;
                }

                if (!EmployeeAlreadyInProject(projectId, employeeId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee with this ID is not in the project.");
                    Console.ResetColor();
                    return false;
                }

                // Remove the employee from the project
                var employeeProject = ProjectEmployeeContext.ProjectEmployees.SingleOrDefault(
                    ep => ep.projectId == projectId && ep.employeeId == employeeId
                );

                if (employeeProject != null)
                {
                    ProjectEmployeeContext.ProjectEmployees.Remove(employeeProject);
                    ProjectEmployeeContext.SaveChanges();
                }

                // Successfully deleted employee from the project
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee from the project: {ex.Message}");
                return false;
            }
        }

        // Get all employees in a project
        public List<Employee> GetEmployeesInProject(int projectId)
        {
            var employeeIds = ProjectEmployeeContext.ProjectEmployees
                .Where(ep => ep.projectId == projectId)
                .Select(ep => ep.employeeId)
                .ToList();

            return ProjectEmployeeContext.Employee
                .Where(e => employeeIds.Contains(e.EmployeeId))
                .ToList();
        }

        // Get all projects for an employee
        public List<Project> GetProjectsForEmployee(int employeeId)
        {
            var projectIds = ProjectEmployeeContext.ProjectEmployees
                .Where(ep => ep.employeeId == employeeId)
                .Select(ep => ep.projectId)
                .ToList();

            return ProjectEmployeeContext.Project
                .Where(p => projectIds.Contains(p.ProjectId))
                .ToList();
        }

        // Get all employees with a specific role
        public List<Employee> GetEmployeesByRole(int roleId)
        {
            return ProjectEmployeeContext.Employee.Where(e => e.RoleId == roleId).ToList();
        }


        // Get employees in the project by role
        // public Dictionary<string, List<Employee>>? GetEmployeesInProjectByRole(string projectId)
        // {
        //     if (!ProjectExists(projectId))
        //     {
        //         Console.ForegroundColor = ConsoleColor.Red;
        //         Console.WriteLine("Project With This ID does not exist");
        //         Console.ResetColor();
        //         return null;
        //     }

        //     var employeesByRole = ProjectEmployeeContext.Employee
        //         .Join(
        //             ProjectEmployeeContext.ProjectEmployees,
        //             e => e.EmployeeId,
        //             ep => ep.projectId,
        //             (e, ep) => new { e, ep }
        //         )
        //         .Where(joinResult => joinResult.ep.projectId == projectId)
        //         .GroupBy(joinResult => joinResult.e.RoleId)
        //         .ToDictionary(
        //             group => group.Key,
        //             group => group.Select(joinResult => joinResult.e).ToList()
        //         );

        //     return employeesByRole;
        // }
    }
}

