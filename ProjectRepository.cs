using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepository : IEntityOperation<Project>
    {
        public static List<Project> projects = new List<Project>();

        //add project
        public void Add(Project project)
        {
            projects.Add(project);
        }

        //view projects
        public List<Project> ListAll()
        {
            return projects;
        }

        public void Delete(string projectId)
        {
            Project projectToDelete = projects.Find(p => p.Id == projectId);
            if (projectToDelete != null)
            {
                projects.Remove(projectToDelete);
            }
        }

        //VALIDATION PART

        //  get project by id
        public Project ListByID(string projectId)
        {
            return projects.FirstOrDefault(p => p.Id == projectId);
        }

        //ProjectExists
        public bool ProjectExists(string projectId)
        {
            // Check if a project with the given ID exists in the collection.
            return projects.Any(project => project.Id == projectId);
        }

        //EmployeeExistsInProject
        public bool EmployeeExistsInProject(string projectId, string employeeId)
        {
            Project project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                // Check if the employee is assigned to the project by their ID
                return project.Employees.Any(employee => employee.Id == employeeId);
            }

            // Project with the specified projectId does not exist
            return false;
        }

        // Check if the project ID already exists
        public bool IsProjectIdUnique(string projectId)
        {
            return !projects.Exists(p => p.Id == projectId);
        }

        // Project ID validation for null value
        public bool ValidateProjectId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }
            return true;
        }

        // Project Name validation for null value
        public bool ValidateProjectName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            return true;
        }

        public bool IsEmployeeAssignedToProjects(string employeeId)
        {
            // Iterate through the list of projects and check if the employee is assigned to any.
            foreach (var project in projects)
            {
                foreach (var assignment in project.Employees)
                {
                    if (assignment.Id == employeeId)
                    {
                        // The employee is assigned to at least one project.
                        return true;
                    }
                }
            }
            // The employee is not assigned to any projects.
            return false;
        }
    }
}
