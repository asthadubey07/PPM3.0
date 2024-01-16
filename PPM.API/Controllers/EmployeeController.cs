using Microsoft.AspNetCore.Mvc;
using PPM.DAL;
using PPM.Model;
using System;
using System.Collections.Generic;

namespace PPM.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDal _employeeDal;

        // Constructor to inject EmployeeDal dependency
        public EmployeeController(EmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                // Retrieve all employees
                var employees = _employeeDal.ListAll();

                // Check if there are no employees
                if (employees.Count == 0)
                    return NotFound();

                // Return the list of employees
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(int employeeId)
        {
            try
            {
                // Retrieve an employee by ID
                var employee = _employeeDal.ListByID(employeeId);

                // Check if the employee does not exist
                if (employee == null)
                    return NotFound();

                // Return the employee
                return Ok(employee);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                // Check if the incoming employee data is null
                if (employee == null)
                    return BadRequest();

                // Add a new employee
                _employeeDal.Add(employee);

                // Return the newly created employee with a Created status
                return CreatedAtAction(
                    nameof(GetEmployeeById),
                    new { employeeId = employee.EmployeeId },
                    employee
                );
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                // Delete an employee by ID
                _employeeDal.Delete(employeeId);

                // Return a NoContent status
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] Employee updatedEmployee)
        {
            try
            {
                // Check if the incoming employee data is invalid
                if (updatedEmployee == null || updatedEmployee.EmployeeId != employeeId)
                    return BadRequest("Invalid employee data.");

                // Retrieve the existing employee
                var existingEmployee = _employeeDal.ListByID(employeeId);

                // Check if the existing employee does not exist
                if (existingEmployee == null)
                    return NotFound();

                // Update the employee
                _employeeDal.Update(updatedEmployee);

                // Return a NoContent status
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("IsEmployeeIdUnique/{employeeId}")]
        public IActionResult IsEmployeeIdUnique(int employeeId)
        {
            try
            {
                // Check if the employee ID is unique
                var isUnique = _employeeDal.IsEmployeeIdUnique(employeeId);

                // Return the result
                return Ok(isUnique);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("IsRoleAssignedToEmployees/{roleId}")]
        public IActionResult IsRoleAssignedToEmployees(int roleId)
        {
            try
            {
                // Check if a role is assigned to employees
                var isRoleAssigned = _employeeDal.IsRoleAssignedToEmployees(roleId);

                // Return the result
                return Ok(isRoleAssigned);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("EmployeeExists/{employeeId}")]
        public IActionResult EmployeeExists(int employeeId)
        {
            try
            {
                // Check if an employee exists
                var doesExist = _employeeDal.EmployeeExists(employeeId);

                // Return the result
                return Ok(doesExist);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an Internal Server Error status
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
