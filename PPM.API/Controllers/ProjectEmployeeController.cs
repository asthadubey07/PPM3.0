using PPM.DAL;
using PPM.Model;
using System;
using Microsoft.AspNetCore.Mvc;

namespace PPM.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly ProjectEmployeeDal _projectEmployeeDal;

        // Constructor to inject ProjectEmployeeDal dependency
        public ProjectEmployeeController(ProjectEmployeeDal projectEmployeeDal)
        {
            _projectEmployeeDal = projectEmployeeDal;
        }

        [HttpPost]
        [Route("AddEmployeeToProject")]
        public IActionResult AddEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                // Check if projectId or employeeId is invalid
                if (projectId <= 0 || employeeId <= 0)
                {
                    return BadRequest("Invalid projectId or employeeId");
                }

                // Check if the project exists
                var projectExists = _projectEmployeeDal.ProjectExists(projectId);
                // Check if the employee exists
                var employeeExists = _projectEmployeeDal.EmployeeExists(employeeId);

                if (!projectExists)
                {
                    return NotFound("Project with this ID does not exist");
                }

                if (!employeeExists)
                {
                    return NotFound("Employee with this ID does not exist");
                }

                // Check if the employee is already in the project
                var isEmployeeAlreadyInProject = _projectEmployeeDal.EmployeeAlreadyInProject(
                    projectId,
                    employeeId
                );

                if (isEmployeeAlreadyInProject)
                {
                    return BadRequest("Employee with this ID is already in the project");
                }

                // Add the employee to the project
                var result = _projectEmployeeDal.AddEmployeeToProject(projectId, employeeId);
                if (result)
                {
                    return Ok("Employee added to the project successfully");
                }
                else
                {
                    return NotFound(
                        "Employee or Project not found, or error adding employee to the project"
                    );
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteEmployeeFromProject")]
        public IActionResult DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                // Check if projectId or employeeId is invalid
                if (projectId <= 0 || employeeId <= 0)
                {
                    return BadRequest("Invalid projectId or employeeId");
                }

                // Check if the project or employee does not exist
                var projectExists = _projectEmployeeDal.ProjectExists(projectId);
                var employeeExists = _projectEmployeeDal.EmployeeExists(employeeId);
                if (!projectExists || !employeeExists)
                {
                    return NotFound("Employee or Project not found");
                }

                // Check if the employee is in the specified project
                var isEmployeeInProject = _projectEmployeeDal.EmployeeAlreadyInProject(
                    projectId,
                    employeeId
                );

                if (!isEmployeeInProject)
                {
                    return NotFound("Employee is not in the specified project");
                }

                // Delete the employee from the project
                var result = _projectEmployeeDal.DeleteEmployeeFromProject(projectId, employeeId);

                if (result)
                {
                    return Ok("Employee deleted from the project successfully");
                }
                else
                {
                    return NotFound(
                        "Employee or Project not found, or error adding employee to the project"
                    );
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("employees-in-project/{projectId}")]
        public IActionResult GetEmployeesInProject(int projectId)
        {
            try
            {
                // Retrieve employees in the specified project
                var employees = _projectEmployeeDal.GetEmployeesInProject(projectId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("projects-for-employee/{employeeId}")]
        public IActionResult GetProjectsForEmployee(int employeeId)
        {
            try
            {
                // Retrieve projects associated with the specified employee
                var projects = _projectEmployeeDal.GetProjectsForEmployee(employeeId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("employees-by-role/{roleId}")]
        public IActionResult GetEmployeesByRole(int roleId)
        {
            try
            {
                // Retrieve employees based on the specified role
                var employees = _projectEmployeeDal.GetEmployeesByRole(roleId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
