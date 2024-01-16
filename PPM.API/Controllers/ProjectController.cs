using Microsoft.AspNetCore.Mvc;
using PPM.DAL;
using PPM.Model;
using System;

namespace PPM.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDal _projectDal;

        // Constructor to inject ProjectDal dependency
        public ProjectController(ProjectDal projectDal)
        {
            _projectDal = projectDal;
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            try
            {
                // Retrieve all projects
                var projects = _projectDal.ListAll();

                // Return the list of projects
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] Project project)
        {
            try
            {
                // Check if the incoming project data is null
                if (project == null)
                    return BadRequest();

                // Add a new project
                _projectDal.Add(project);

                // Return the newly created project with a Created status
                return CreatedAtAction(
                    nameof(GetProjectById),
                    new { projectId = project.ProjectId },
                    project
                );
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(int projectId)
        {
            try
            {
                // Retrieve a project by ID
                var project = _projectDal.ListByID(projectId);

                // Check if the project does not exist
                if (project == null)
                    return NotFound();

                // Return the project
                return Ok(project);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] Project project)
        {
            try
            {
                // Check if the incoming project data is invalid
                if (project == null || project.ProjectId != projectId)
                    return BadRequest();

                // Retrieve the existing project
                var existingProject = _projectDal.ListByID(projectId);

                // Check if the existing project does not exist
                if (existingProject == null)
                    return NotFound();

                // Update the project
                _projectDal.Update(project);

                // Return a NoContent status
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            try
            {
                // Delete a project by ID
                _projectDal.Delete(projectId);

                // Return a NoContent status
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
