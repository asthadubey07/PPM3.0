using PPM.DAL;
using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepository : IEntityOperation<Project>
    {
        // List to store projects in memory
        public readonly static List<Project> projects = new List<Project>();

        // Instance of ProjectDal to interact with data access layer
        readonly ProjectDal projectDal = new ProjectDal();

        // Adds a new project.
        public void Add(Project project)
        {
            try
            {
                projectDal.Add(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding project: {ex.Message}");

                // Details of the inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        // Retrieves a list of all projects.
        public List<Project> ListAll()
        {
            try
            {
                return projectDal.ListAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving projects: {ex.Message}");
                return new List<Project>();
            }
        }

        // Deletes a project by its ID.
        public void Delete(string projectId)
        {
            try
            {
                projectDal.Delete(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
            }
        }

        // VALIDATION PART

        // Retrieves a project by its ID.
        public Project? ListByID(string projectId)
        {
            try
            {
                return projectDal.ListByID(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving project: {ex.Message}");
                return null;
            }
        }

        // Checks if a project with the specified ID exists.
        public bool ProjectExists(string projectId)
        {
            try
            {
                return projectDal.ProjectExists(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if project exists: {ex.Message}");
                return false;
            }
        }

        // Checks if an employee is assigned to the specified project.
        public bool EmployeeExistsInProject(string projectId, string employeeId)
        {
            try
            {
                return projectDal.EmployeeExistsInProject(projectId, employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee exists in project: {ex.Message}");
                return false;
            }
        }

        // Checks if the project ID is unique.
        public bool IsProjectIdUnique(string projectId)
        {
            try
            {
                return projectDal.IsProjectIdUnique(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking project ID uniqueness: {ex.Message}");
                return false;
            }
        }

        // Validates a project ID for null value.
        public bool ValidateProjectId(string Id)
        {
            try
            {
                return projectDal.ValidateProjectId(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating project ID: {ex.Message}");
                return false;
            }
        }

        // Validates a project name for null value.
        public bool ValidateProjectName(string Name)
        {
            try
            {
                return projectDal.ValidateProjectName(Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating project name: {ex.Message}");
                return false;
            }
        }

        // Checks if an employee is assigned to any projects.
        public bool IsEmployeeAssignedToProjects(string employeeId)
        {
            try
            {
                return projectDal.IsEmployeeAssignedToProjects(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee is assigned to projects: {ex.Message}");
                return false;
            }
        }
    }
}
