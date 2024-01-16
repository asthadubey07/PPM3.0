using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PPM.Dal.ADO
{
    public class ProjectEmployeeAdo
    {
        private readonly string connectionString =
            "Server=RHJ-9F-D219\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";

        // Check if the project exists
        // Checks if a project with the specified ID exists in the database.
        public bool ProjectExists(string projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Project WHERE ProjectId = @ProjectId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        int projectCount = (int)command.ExecuteScalar();
                        return projectCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if project exists: {ex.Message}");
                return false;
            }
        }

        // Check if the employee exists
        // Checks if an employee with the specified ID exists in the database.
        public bool EmployeeExists(string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Employee WHERE EmployeeId = @EmployeeId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        int employeeCount = (int)command.ExecuteScalar();
                        return employeeCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee exists: {ex.Message}");
                return false;
            }
        }

        // Check if the employee is already in the project
        // Checks if an employee is already assigned to a project.
        public bool EmployeeAlreadyInProject(string projectId, string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT COUNT(*) FROM EmployeeProject WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        int assignmentCount = (int)command.ExecuteScalar();
                        return assignmentCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee is already in the project: {ex.Message}");
                return false;
            }
        }

        // AddEmployeeToProject
        // Adds an employee to a project in the database.
        public bool AddEmployeeToProject(string projectId, string employeeId)
        {
            try
            {
                if (!ProjectExists(projectId))
                {
                    Console.WriteLine("Project With This ID does not exist");
                    return false;
                }

                if (!EmployeeExists(employeeId))
                {
                    Console.WriteLine("Employee With This ID does not exist");
                    return false;
                }

                if (EmployeeAlreadyInProject(projectId, employeeId))
                {
                    Console.WriteLine("Employee With This ID Is Already In The Project");
                    return false;
                }

                // Add the employee to the project
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string addAssignmentQuery =
                        "INSERT INTO EmployeeProject (ProjectId, EmployeeId, CreatedOn, ModifiedOn) VALUES (@ProjectId, @EmployeeId, @CreatedOn, @ModifiedOn)";

                    using (SqlCommand addAssignmentCommand = new SqlCommand(addAssignmentQuery, connection))
                    {
                        addAssignmentCommand.Parameters.AddWithValue("@ProjectId", projectId);
                        addAssignmentCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                        addAssignmentCommand.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        addAssignmentCommand.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);

                        addAssignmentCommand.ExecuteNonQuery();
                    }
                }

                // Successfully added employee to the project
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee to the project: {ex.Message}");
                return false;
            }
        }

        // DeleteEmployeeFromProject
        // Removes an employee from a project in the database.
        public void DeleteEmployeeFromProject(string projectId, string employeeId)
        {
            try
            {
                if (!ProjectExists(projectId))
                {
                    throw new Exception("Project not found");
                }

                if (!EmployeeExists(employeeId))
                {
                    throw new Exception("Employee not found");
                }

                if (!EmployeeAlreadyInProject(projectId, employeeId))
                {
                    throw new Exception("Employee not found in the project");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Remove the employee from the project
                    string deleteAssignmentQuery =
                        "DELETE FROM EmployeeProject WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId";
                    using (SqlCommand deleteAssignmentCommand = new SqlCommand(deleteAssignmentQuery, connection))
                    {
                        deleteAssignmentCommand.Parameters.AddWithValue("@ProjectId", projectId);
                        deleteAssignmentCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

                        deleteAssignmentCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee from the project: {ex.Message}");
            }
        }

        // GetEmployeesInProjectByRole
        // Retrieves a dictionary of employees in a project grouped by their role.
        public Dictionary<string, List<Employee>> GetEmployeesInProjectByRole(string projectId)
        {
            try
            {
                if (!ProjectExists(projectId))
                {
                    Console.WriteLine("Project With This ID does not exist");
                    return null;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get employees in the project by role
                    string query =
                        @"SELECT e.EmployeeId, e.FirstName, e.LastName, e.RoleId FROM Employee e 
                    JOIN EmployeeProject pea ON e.EmployeeId = pea.EmployeeId WHERE pea.ProjectId = @ProjectId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        Dictionary<string, List<Employee>> employeesByRole = new Dictionary<string, List<Employee>>();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string roleId = reader["RoleId"].ToString();
                                Employee employee = new Employee
                                {
                                    Id = reader["EmployeeId"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    RoleId = roleId
                                };

                                if (!employeesByRole.ContainsKey(roleId))
                                {
                                    employeesByRole[roleId] = new List<Employee>();
                                }

                                employeesByRole[roleId].Add(employee);
                            }
                        }

                        return employeesByRole;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting employees by role: {ex.Message}");
                return null;
            }
        }
    }
}
