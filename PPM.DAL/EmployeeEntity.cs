using PPM.Model;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAL
{
    public class EmployeeDal
    {
        private readonly PPMContext EmployeeContext;

        // Constructor for dependency injection
        public EmployeeDal(PPMContext context)
        {
            EmployeeContext = context;
        }
        // public EmployeeDal()
        // {
        //     EmployeeContext = new PPMContext();
        // }


        // CREATE: Add an employee to the database
        public void Add(Employee employee)
        {
            try
            {
                // Set created and modified timestamps
                employee.CreatedOn = DateTime.Now;
                employee.ModifiedOn = DateTime.Now;

                // Add employee to the DbSet and save changes
                EmployeeContext.Employee.Add(employee);
                EmployeeContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee to the database: {ex.Message}");
            }
        }

        // READ: Retrieve all employees from the database
        public List<Employee> ListAll()
        {
            try
            {
                return EmployeeContext.Employee.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees from the database: {ex.Message}");
                return new List<Employee>();
            }
        }

        // DELETE: Remove an employee from the database by ID
        public void Delete(int employeeId)
        {
            try
            {
                var employee = EmployeeContext.Employee.Find(employeeId);
                if (employee != null)
                {
                    EmployeeContext.Employee.Remove(employee);
                    EmployeeContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee from the database: {ex.Message}");
            }
        }

        // GetById: Retrieve an employee by ID
        public Employee? ListByID(int employeeId)
        {
            try
            {
                return EmployeeContext.Employee.Find(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee from the database: {ex.Message}");
                return null;
            }
        }

        // Update: Update employee information
        public void Update(Employee updatedEmployee)
        {
            var existingEmployee = EmployeeContext.Employee.SingleOrDefault(
                e => e.EmployeeId == updatedEmployee.EmployeeId
            );
            if (existingEmployee != null)
            {
                // Update employee properties
                existingEmployee.FirstName = updatedEmployee.FirstName;
                existingEmployee.LastName = updatedEmployee.LastName;
                existingEmployee.Email = updatedEmployee.Email;
                existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
                existingEmployee.Password = updatedEmployee.Password;
                existingEmployee.Address = updatedEmployee.Address;
                existingEmployee.RoleId = updatedEmployee.RoleId;
                existingEmployee.ModifiedOn = DateTime.UtcNow;

                // Mark the entity as modified and save changes
                EmployeeContext.Entry(existingEmployee).State = EntityState.Modified;
                EmployeeContext.SaveChanges();
            }
        }

        // Check if the employee ID is unique
        public bool IsEmployeeIdUnique(int employeeId)
        {
            try
            {
                return !EmployeeContext.Employee.Any(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking uniqueness of employee ID: {ex.Message}");
                return false;
            }
        }

        // Check if a role is assigned to employees
        public bool IsRoleAssignedToEmployees(int roleId)
        {
            try
            {
                return EmployeeContext.Employee.Any(e => e.RoleId == roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if role is assigned to employees: {ex.Message}");
                return false;
            }
        }

        // Check if an employee exists
        public bool EmployeeExists(int employeeId)
        {
            try
            {
                return EmployeeContext.Employee.Any(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if employee exists: {ex.Message}");
                return false;
            }
        }

        // Validate employee first name
        public bool ValidateEmployeeFirstName(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }

        // Validate employee last name
        public bool ValidateEmployeeLastName(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        // Validate email
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        // Validate phone number
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            string pattern = @"^-?\d+(\.\d+)?$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

        // Validate address
        public bool ValidateEmployeeAddress(string address)
        {
            return !string.IsNullOrEmpty(address);
        }

        // // Validate employee ID
        // public bool ValidateEmployeeId(int id)
        // {
        //     return !string.IsNullOrEmpty(id);
        // }
    }
}

