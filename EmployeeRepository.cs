using System.Text.RegularExpressions;
using PPM.Model;

namespace PPM.Domain
{
    public class EmployeeRepository : IEntityOperation<Employee>
    {
        public static List<Employee> employees = new List<Employee>();

        //Add Employee
        public void Add(Employee employee)
        {
            employees.Add(employee);
        }

        //View Employees
        public List<Employee> ListAll()
        {
            return employees;
        }

        //Delete employee

        public void Delete(string employeeId)
        {
            // Find and delete the employee
            Employee employeeToDelete = employees.Find(e => e.Id == employeeId);
            if (employeeToDelete != null)
            {
                employees.Remove(employeeToDelete);
            }
        }

        //validation part
        //GetEmployeeById
        public Employee ListByID(string employeeId)
        {
            return employees.Find(e => e.Id == employeeId);
        }

        // Check if the Employee ID already exists
        public bool IsEmployeeIdUnique(string employeeId)
        {
            return !employees.Exists(p => p.Id == employeeId);
        }

        // Employee ID validation for null value
        public bool ValidateEmployeeId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }
            return true;
        }

        // Employee Name validation for null value
        public bool ValidateEmployeeFirstName(string FirstName)
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                return false;
            }
            return true;
        }

        //ValidateEmployeeLastName
        public bool ValidateEmployeeLastName(string LastName)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                return false;
            }
            return true;
        }

        //IsValidEmail
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        //IsValidPhoneNumber
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            string pattern = @"^-?\d+(\.\d+)?$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        //ValidateEmployeeAddress
        public bool ValidateEmployeeAddress(string Address)
        {
            if (string.IsNullOrEmpty(Address))
            {
                return false;
            }
            return true;
        }

        public bool IsRoleAssignedToEmployees(string roleId)
        {
            // Iterate through the list of employees and check if any of them have the given role.
            foreach (var employee in employees)
            {
                if (employee.RoleId == roleId)
                {
                    // The role is assigned to at least one employee.
                    return true;
                }
            }
            // The role is not assigned to any employees.
            return false;
        }
    }
}
