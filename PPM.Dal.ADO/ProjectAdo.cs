using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PPM.Dal.ADO
{
    public class ProjectAdo
    {
        private readonly string connectionString =
                  "Server=RHJ-9F-D219\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";

        // Add
        // Adds a project to the database.
        public void Add(Project project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        @"INSERT INTO Project 
                             (ProjectId, Name, StartDate, EndDate, CreatedOn, ModifiedOn) 
                             VALUES 
                             (@ProjectId, @Name, @StartDate, @EndDate, @CreatedOn, @ModifiedOn)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", project.Id);
                        command.Parameters.AddWithValue("@Name", project.Name);
                        command.Parameters.AddWithValue("@StartDate", project.StartDate);
                        command.Parameters.AddWithValue("@EndDate", project.EndDate);
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        command.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding project to the database: {ex.Message}");
            }
        }

        // ListAll
        // Retrieves a list of all projects from the database.
        public List<Project> ListAll()
        {
            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT ProjectId, Name, StartDate, EndDate, CreatedOn, ModifiedOn FROM Project";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Project project = new Project
                                {
                                    Id = reader["ProjectId"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                };

                                projects.Add(project);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles from the database: {ex.Message}");
            }

            return projects;
        }

        // Delete
        // Deletes a project from the database based on the project ID.
        public void Delete(string projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Project WHERE ProjectId = @ProjectId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting role from the database: {ex.Message}");
            }
        }

        // ListByID
        // Retrieves a project from the database based on the project ID.
        public Project ListByID(string projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT ProjectId, Name, StartDate, EndDate, CreatedOn, ModifiedOn "
                        + "FROM Project WHERE ProjectId = @ProjectId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Project
                                {
                                    Id = reader["ProjectId"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                                };
                            }
                            else
                            {
                                return null; // No project found with the specified ID
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving role from the database: {ex.Message}");
                return null;
            }
        }

        // ProjectExists
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

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if project exists in the database: {ex.Message}");
                return false;
            }
        }

        // EmployeeExistsInProject
        // Checks if an employee is assigned to a project based on the project and employee IDs.
        public bool EmployeeExistsInProject(string projectId, string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT COUNT(*) FROM EmployeeProject "
                        + "WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error checking if employee Exists In Project in the database: {ex.Message}"
                );
                return false;
            }
        }

        // IsProjectIdUnique
        // Checks if the project ID is unique in the database.
        public bool IsProjectIdUnique(string projectId)
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

                        int count = (int)command.ExecuteScalar();

                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                      $"Error checking if project ID is unique in the database: {ex.Message}"
                  );
                return false;
            }
        }

        // IsEmployeeAssignedToProjects
        // Checks if an employee is assigned to any projects based on the employee ID.
        public bool IsEmployeeAssignedToProjects(string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT COUNT(*) FROM EmployeeProject WHERE EmployeeId = @EmployeeId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee is assigned to project: {ex.Message}");
                return false;
            }
        }

        // ValidateProjectId
        // Validates that the project ID is not null or empty.
        public bool ValidateProjectId(string Id)
        {
            return !string.IsNullOrEmpty(Id);
        }

        // ValidateProjectName
        // Validates that the project name is not null or empty.
        public bool ValidateProjectName(string Name)
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
