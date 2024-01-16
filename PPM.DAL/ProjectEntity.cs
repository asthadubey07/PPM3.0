using PPM.Model;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class ProjectDal
    {
        private readonly PPMContext projectContext;

        // Constructor with dependency injection
        public ProjectDal(PPMContext context)
        {
            projectContext = context;
        }
        // public ProjectDal()
        // {
        //     projectContext = new PPMContext();
        // }
        // CREATE
        // Adds a project to the database.
        public void Add(Project project)
        {
            try
            {
                project.CreatedOn = DateTime.Now;
                project.ModifiedOn = DateTime.Now;

                projectContext.Project.Add(project);
                projectContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding project to the database: {ex.Message}");
            }
        }

        // READ
        // Retrieves a list of all projects from the database.
        public List<Project> ListAll()
        {
            try
            {
                return projectContext.Project.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving projects from the database: {ex.Message}");
                return new List<Project>();
            }
        }

        // DELETE
        // Deletes a project from the database based on the project ID.
        public void Delete(int projectId)
        {
            try
            {
                var project = projectContext.Project.Find(projectId);
                if (project != null)
                {
                    projectContext.Project.Remove(project);
                    projectContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project from the database: {ex.Message}");
            }
        }

        // GetById
        // Retrieves a project from the database based on the project ID.
        public Project? ListByID(int projectId)
        {
            try
            {
                return projectContext.Project.Find(projectId);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error retrieving project from the database: {ex.Message}");
                return null;
            }
        }

        // Update a project
        // Updates an existing project in the database.
        public void Update(Project updatedProject)
        {
            var existingProject = projectContext.Project.SingleOrDefault(
                p => p.ProjectId == updatedProject.ProjectId
            );
            if (existingProject != null)
            {
                existingProject.Name = updatedProject.Name;
                existingProject.StartDate = updatedProject.StartDate;
                existingProject.EndDate = updatedProject.EndDate;
                existingProject.ModifiedOn = DateTime.UtcNow;

                // Mark the entity as modified
                projectContext.Entry(existingProject).State = EntityState.Modified;
                projectContext.SaveChanges();
            }
        }

        // ProjectExists
        // Checks if a project with the specified ID exists in the database.
        public bool ProjectExists(int projectId)
        {
            try
            {
                return projectContext.Project.Any(p => p.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if project exists in the database: {ex.Message}");
                return false;
            }
        }

        // EmployeeExistsInProject
        // Checks if an employee with the specified ID exists in the specified project.
        public bool EmployeeExistsInProject(int projectId, int employeeId)
        {
            try
            {
                return projectContext.ProjectEmployees.Any(
                    ep => ep.projectId == projectId && ep.employeeId == employeeId
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee exists in project: {ex.Message}");
                return false;
            }
        }

        // Check if the project ID is unique
        // Checks if a project ID is unique in the database.
        public bool IsProjectIdUnique(int projectId)
        {
            try
            {
                return !projectContext.Project.Any(p => p.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if project ID is unique: {ex.Message}");
                return false;
            }
        }

        // Check if an employee is assigned to any projects
        // Checks if an employee with the specified ID is assigned to any projects.
        public bool IsEmployeeAssignedToProjects(int employeeId)
        {
            try
            {
                return projectContext.ProjectEmployees.Any(ep => ep.employeeId == employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error checking if employee is assigned to projects: {ex.Message}"
                );
                return false;
            }
        }

        // Project Name validation for null value
        // Validates that a project name is not null or empty.
        public bool ValidateProjectName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        // // Project ID validation for null value
        // public bool ValidateProjectId(string id)
        // {
        //     return !string.IsNullOrEmpty(id);
        // }

    }
}

