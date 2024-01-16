using PPM.Model;
using PPM.DAL;
using System.Text.RegularExpressions;

namespace PPM.Domain
{
    public class EmployeeRepository : IEntityOperation<Employee>
    {
        // Instance of EmployeeDal to interact with data access layer
       readonly EmployeeDal employeeDal = new();
        // List to store employees in memory
        public readonly static List<Employee> employees = new List<Employee>();

        // Adds a new employee.
        public void Add(Employee employee)
        {
            try
            {
                employeeDal.Add(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
            }
        }

        // Retrieves a list of all employees.
        public List<Employee> ListAll()
        {
            try
            {
                return employeeDal.ListAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
                return new List<Employee>();
            }
        }

        // Deletes an employee by its ID.
        public void Delete(string employeeId)
        {
            try
            {
                // Find and delete the employee
                employeeDal.Delete(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }

        // Validation part

        // Retrieves an employee by its ID.
        public Employee? ListByID(string employeeId)
        {
            try
            {
                return employeeDal.ListByID(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee: {ex.Message}");
                return null;
            }
        }

        // Checks if the Employee ID already exists.
        public bool IsEmployeeIdUnique(string employeeId)
        {
            try
            {
                return employeeDal.IsEmployeeIdUnique(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking employee ID uniqueness: {ex.Message}");
                return false;
            }
        }

        // Checks if an employee exists.
        public bool EmployeeExists(string employeeId)
        {
            try
            {
                return employeeDal.EmployeeExists(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee exists: {ex.Message}");
                return false;
            }
        }

        // Employee ID validation for null value.
        public bool ValidateEmployeeId(string Id)
        {
            try
            {
                return employeeDal.ValidateEmployeeId(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee ID: {ex.Message}");
                return false;
            }
        }

        // Employee first name validation for null value.
        public bool ValidateEmployeeFirstName(string FirstName)
        {
            try
            {
                return employeeDal.ValidateEmployeeFirstName(FirstName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee first name: {ex.Message}");
                return false;
            }
        }

        // Employee last name validation for null value.
        public bool ValidateEmployeeLastName(string LastName)
        {
            try
            {
                return employeeDal.ValidateEmployeeLastName(LastName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee last name: {ex.Message}");
                return false;
            }
        }

        // Email validation for null value.
        public bool IsValidEmail(string email)
        {
            try
            {
                return employeeDal.IsValidEmail(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee email: {ex.Message}");
                return false;
            }
        }

        // Phone number validation for null value.
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            try
            {
                return employeeDal.IsValidPhoneNumber(phoneNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee phone number: {ex.Message}");
                return false;
            }
        }

        // Employee address validation for null value.
        public bool ValidateEmployeeAddress(string Address)
        {
            try
            {
                return employeeDal.ValidateEmployeeAddress(Address);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee address: {ex.Message}");
                return false;
            }
        }

        // Checks if a role is assigned to any employees.
        public bool IsRoleAssignedToEmployees(string roleId)
        {
            try
            {
                return employeeDal.IsRoleAssignedToEmployees(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role is assigned to employees: {ex.Message}");
                return false;
            }
        }

        // Password validation using EmployeeValidator class.
        public bool ValidatePassword(string Password)
        {
            // Check if the password is not empty
            if (string.IsNullOrEmpty(Password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Password cannot be empty.");
                Console.ResetColor();
                return false;
            }

            // Use the EmployeeValidator class to perform more complex validations
            var validator = new EmployeeValidator();
            if (!validator.ValidatePasswordComplexity(Password))
            {
                return false;
            }

            // If the password passes all validations
            return true;
        }
    }

    public class EmployeeValidator
    {
        // Other validation methods...

        // Validates password complexity using a regex pattern.
        public bool ValidatePasswordComplexity(string password)
        {
            // Define a regex pattern for a strong password
            // This example requires at least one uppercase letter, one lowercase letter, one digit, and one special character
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            // Check if the password matches the pattern
            if (!Regex.IsMatch(password, passwordPattern))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Password must meet the required complexity criteria.");
                Console.ResetColor();
                return false;
            }

            // If the password passes all validations
            return true;
        }
    }
}
