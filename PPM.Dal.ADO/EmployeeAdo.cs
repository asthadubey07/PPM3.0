using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PPM.Dal.ADO
{
    public class EmployeeAdo
    {
        private readonly string connectionString =
            "Server=RHJ-9F-D219\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";

        // Add
        // Adds an employee to the database.
        public void Add(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "INSERT INTO Employee (EmployeeId, FirstName, LastName, Email, PhoneNumber, Address, RoleId, CreatedOn, ModifiedOn) "
                        + "VALUES (@EmployeeId, @FirstName, @LastName, @Email, @PhoneNumber, @Address, @RoleId, @CreatedOn, @ModifiedOn)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", employee.Address);
                        command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        command.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee to the database: {ex.Message}");
            }
        }

        // ListAll
        // Retrieves a list of all employees from the database.
        public List<Employee> ListAll()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT EmployeeId, FirstName, LastName, Email, PhoneNumber, Address, RoleId FROM Employee";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee
                                {
                                    Id = reader["EmployeeId"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    RoleId = reader["RoleId"].ToString()
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees from the database: {ex.Message}");
            }

            return employees;
        }

        // Delete
        // Deletes an employee from the database based on the employee ID.
        public void Delete(string employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee from the database: {ex.Message}");
            }
        }

        // ListByID
        // Retrieves an employee from the database based on the employee ID.
        public Employee ListByID(string employeeId)
        {
            Employee employee = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query =
                        "SELECT EmployeeId, FirstName, LastName, Email, PhoneNumber, Address, RoleId "
                        + "FROM Employee WHERE EmployeeId = @EmployeeId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new Employee
                                {
                                    Id = reader["EmployeeId"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    RoleId = reader["RoleId"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee from the database: {ex.Message}");
            }

            return employee;
        }

        // IsEmployeeIdUnique
        // Checks if an employee ID is unique in the database.
        public bool IsEmployeeIdUnique(string employeeId)
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

                        int count = (int)command.ExecuteScalar();

                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking uniqueness of employee ID: {ex.Message}");
                return false;
            }
        }

        // IsRoleAssignedToEmployees
        // Checks if a role is assigned to any employees in the database.
        public bool IsRoleAssignedToEmployees(string roleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Employee WHERE RoleId = @RoleId";

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
                Console.WriteLine($"Error checking if role is assigned to employees: {ex.Message}");
                return false;
            }
        }

        // EmployeeExists
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
                Console.WriteLine($"Error checking if role is assigned to employees: {ex.Message}");
                return false;
            }
        }

        // ValidateEmployeeId
        // Validates that an employee ID is not null or empty.
        public bool ValidateEmployeeId(string id)
        {
            return !string.IsNullOrEmpty(id);
        }

        // ValidateEmployeeFirstName
        // Validates that an employee's first name is not null or empty.
        public bool ValidateEmployeeFirstName(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }

        // ValidateEmployeeLastName
        // Validates that an employee's last name is not null or empty.
        public bool ValidateEmployeeLastName(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        // IsValidEmail
        // Validates that an email address is in a valid format.
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        // IsValidPhoneNumber
        // Validates that a phone number is in a valid format.
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            string pattern = @"^-?\d+(\.\d+)?$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        // ValidateEmployeeAddress
        // Validates that an employee's address is not null or empty.
        public bool ValidateEmployeeAddress(string address)
        {
            return !string.IsNullOrEmpty(address);
        }
    }
}
