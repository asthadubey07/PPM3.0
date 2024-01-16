using Microsoft.AspNetCore.Mvc;
using PPM.DAL;
using PPM.Model;
using System;

namespace PPM.API
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleDal _roleDal;

        // Constructor to inject RoleDal dependency
        public RoleController(RoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            try
            {
                // Retrieve all roles
                var roles = _roleDal.ListAllRole();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                // Return Internal Server Error status with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{roleId}")]
        public IActionResult GetRoleById(int roleId)
        {
            try
            {
                // Retrieve role by ID
                var role = _roleDal.ListByID(roleId);
                if (role == null)
                    return NotFound(); // Return NotFound status if the role is not found

                return Ok(role);
            }
            catch (Exception ex)
            {
                // Return Internal Server Error status with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            try
            {
                if (role == null)
                    return BadRequest("Invalid role data."); // Return BadRequest status for invalid input

                // Add validation logic here if required

                // Add the role
                _roleDal.AddRole(role);
                return CreatedAtAction(nameof(GetRoleById), new { roleId = role.RoleId }, role);
                // Return Created status with the URI of the newly created resource
            }
            catch (Exception ex)
            {
                // Return Internal Server Error status with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(int roleId)
        {
            try
            {
                // Delete the role by ID
                _roleDal.DeleteRole(roleId);
                return NoContent(); // Return NoContent status for a successful deletion
            }
            catch (Exception ex)
            {
                // Return Internal Server Error status with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(int roleId, [FromBody] Role updatedRole)
        {
            try
            {
                if (updatedRole == null || updatedRole.RoleId != roleId)
                    return BadRequest("Invalid role data."); // Return BadRequest status for invalid input

                // Check if the role with the specified ID exists
                var existingRole = _roleDal.ListByID(roleId);
                if (existingRole == null)
                    return NotFound(); // Return NotFound status if the role is not found

                // Update the role
                _roleDal.Update(updatedRole);
                return NoContent(); // Return NoContent status for a successful update
            }
            catch (Exception ex)
            {
                // Return Internal Server Error status with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
