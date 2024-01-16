namespace PPM.Dal.ADO
{
    public class RoleAdo
    {
        private readonly string  connectionString =
            "Server=RHJ-9F-D219\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";

        // CREATE
        // Adds a new role to the database.
        public void AddRole(Role role)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "INSERT INTO Role (RoleId, Name, CreatedOn, ModifiedOn) VALUES (@RoleId, @Name,@CreatedOn, @ModifiedOn)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", role.Id);
                        command.Parameters.AddWithValue("@Name", role.Name);
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        command.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error adding role to the database: {ex.Message}");
            }
        }

        // READ
        // Retrieves a list of all roles from the database.
        public List<Role> ListAllRole()
        {
            List<Role> roles = new List<Role>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT RoleId, Name FROM Role";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Role role = new Role
                                {
                                    Id = reader["RoleId"].ToString(),
                                    Name = reader["Name"].ToString()
                                };

                                roles.Add(role);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error retrieving roles from the database: {ex.Message}");
            }

            return roles;
        }

        // DELETE
        // Deletes a role from the database by its ID.
        public void DeleteRole(string roleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Role WHERE RoleId = @RoleId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting role from the database: {ex.Message}");
            }
        }

        // GetById
        // Retrieves a role from the database by its ID.
        public Role ListByIDRole(string roleId)
        {
            Role role = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT RoleId, Name FROM Role WHERE RoleId = @RoleId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                role = new Role
                                {
                                    Id = reader["RoleId"].ToString(),
                                    Name = reader["Name"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving role from the database: {ex.Message}");
            }

            return role;
        }

        // RoleExists
        // Checks if a role with the specified ID exists in the database.
        public bool RoleExists(string roleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Role WHERE RoleId = @RoleId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role exists in the database: {ex.Message}");
                return false;
            }
        }

        // Check if the role ID is unique
        // Checks if the specified role ID is unique in the database.
        public bool IsRoleIdUnique(string roleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Role WHERE RoleId = @RoleId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        int count = (int)command.ExecuteScalar();

                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role ID is unique in the database: {ex.Message}");
                return false;
            }
        }

        // Check if the role name is unique
        // Checks if the specified role name is unique in the database.
        public bool IsRoleNameUnique(string roleName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Role WHERE Name = @Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", roleName);
                        int count = (int)command.ExecuteScalar();

                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role name is unique in the database: {ex.Message}");
                return false;
            }
        }

        // Role ID validation for null value
        // Validates that the role ID is not null or empty.
        public static bool ValidateRoleId(string roleId)
        {
            return !string.IsNullOrEmpty(roleId);
        }

        // Role Name validation for null value
        // Validates that the role name is not null or empty.
        public static bool ValidateRoleName(string roleName)
        {
            return !string.IsNullOrEmpty(roleName);
        }
    }
}
