using PPM.Domain;
using PPM.Model;

namespace PPM.UIConsole
{
    public class EmployeeUi
    {
        EmployeeRepository employeeManager = new EmployeeRepository();

        public RoleRepository roleManager = new();

        //add project
        public void AddEmployees()
        {
            System.Console.Write("HOW MANY EMPLOYEE U WANT TO ADD ");
            if (int.TryParse(System.Console.ReadLine(), out int employeeCount) && employeeCount > 0)
            {
                for (int i = 0; i < employeeCount; i++)
                {
                    Employee employee = new();
                    System.Console.WriteLine("Add Employee:");
                    while (true)
                    {
                        Console.Write("Enter Employee ID: ");
                        string id = Console.ReadLine();
                        if (
                            employeeManager.ValidateEmployeeId(id)
                            && employeeManager.IsEmployeeIdUnique(id)
                        )
                        {
                            employee.Id = id;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (!employeeManager.ValidateEmployeeId(id))
                        {
                            Console.WriteLine(
                                "Invalid Employee ID : Employee ID Can Not Be Empty."
                            );
                        }
                        else
                        {
                            Console.WriteLine(
                                "Employee ID is not unique. Please enter a unique ID."
                            );
                        }
                        Console.ResetColor();
                    }

                    while (true)
                    {
                        System.Console.Write("Enter Employee FirstName: ");
                        string firstName = System.Console.ReadLine();

                        if (employeeManager.ValidateEmployeeFirstName(firstName))
                        {
                            employee.FristName = firstName;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Employee FirstName cannot be empty.");
                            Console.ResetColor();
                        }
                    }

                    while (true)
                    {
                        System.Console.Write("Enter Employee LastName: ");
                        string lastName = System.Console.ReadLine();

                        if (employeeManager.ValidateEmployeeLastName(lastName))
                        {
                            employee.LastName = lastName;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Employee lastName cannot be empty.");
                            Console.ResetColor();
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        if (employeeManager.IsValidEmail(email))
                        {
                            employee.Email = email;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Email. Please enter a valid email address.");
                        Console.ResetColor();
                    }

                    while (true)
                    {
                        Console.Write("Enter Phone Number: ");
                        string phoneNumber = Console.ReadLine();
                        if (employeeManager.IsValidPhoneNumber(phoneNumber))
                        {
                            employee.PhoneNumber = phoneNumber;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Invalid Phone Number. Please enter a valid phone number."
                        );
                        Console.ResetColor();
                    }
                    while (true)
                    {
                        Console.Write("Enter Address: ");
                        string Address = Console.ReadLine();
                        if (employeeManager.ValidateEmployeeAddress(Address))
                        {
                            employee.Address = Address;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Address. Please enter a valid Address.");
                        Console.ResetColor();
                    }
                    while (true)
                    {
                        Console.Write("Enter Employee RoleId: ");
                        string RoleId = Console.ReadLine();
                        if (!roleManager.RoleExists(RoleId))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Role ID does not exist. Please add the role first.");
                            Console.ResetColor();
                        }
                        else
                        {
                            employee.RoleId = RoleId;
                            break;
                        }
                        return;
                    }
                    employeeManager.Add(employee);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Employee Added sucscessfully.");
                    Console.ResetColor();
                }
            }
        }

        // view Employees
        public void ViewEmployees()
        {
            var employees = employeeManager.ListAll();

            if (employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Employess found.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("List of Employess:");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.ResetColor();
                Console.ResetColor();

                foreach (var employee in employees)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Employee ID          : {employee.Id}");
                    Console.WriteLine($"Employee FirstName   : {employee.FristName}");
                    Console.WriteLine($"Employee LastName    : {employee.LastName}");
                    Console.WriteLine($"Employee Email       : {employee.Email}");
                    Console.WriteLine($"Employee PhoneNumber : {employee.PhoneNumber}");
                    Console.WriteLine($"Employee Address     : {employee.Address}");
                    Console.WriteLine($"Employee RoleId      : {employee.RoleId}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        //Delete Employee
        public void DeleteEmployee()
        {
            ProjectRepository projectManager = new();
            Employee employee = new();
            while (true)
            {
                // Get employee ID from the user and perform validation
                Console.Write("Enter Employee ID to delete: ");
                string employeeId = Console.ReadLine();
                if (employeeManager.ValidateEmployeeId(employeeId))
                {
                    employee.Id = employeeId;

                    if (projectManager.IsEmployeeAssignedToProjects(employeeId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Cannot delete the employee. Employee is assigned to one or more projects."
                        );
                        Console.ResetColor();
                        break;
                    }
                    employeeManager.Delete(employeeId);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Employee deleted successfully.");
                    Console.ResetColor();

                    // else
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Red;
                    //     Console.WriteLine(
                    //         "Failed to delete the employee: Employee ID Is Not Valid."
                    //     );
                    //     Console.ResetColor();
                    //     return;
                    // }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee ID is required.");
                    Console.ResetColor();
                }
            }
        }

        //get employee by id
        public void GetEmployeeById()
        {
            Employee employee1 = new();
            while (true)
            {
                // Get employee ID from the user
                Console.Write("Enter Employee ID to retrieve the Employee Details: ");
                string employeeId = Console.ReadLine();
                if (employeeManager.ValidateEmployeeId(employeeId))
                {
                    employee1.Id = employeeId;

                    // Call the domain logic to get the employee by ID
                    Employee employee = employeeManager.ListByID(employeeId);

                    if (employee != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Employee FirstName   : {employee.FristName}");
                        Console.WriteLine($"Employee LastName    : {employee.LastName}");
                        Console.WriteLine($"Employee Email       : {employee.Email}");
                        Console.WriteLine($"Employee PhoneNumber : {employee.PhoneNumber}");
                        Console.WriteLine($"Employee Address     : {employee.Address}");
                        Console.WriteLine($"Employee RoleId      : {employee.RoleId}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Employee not found : Employee ID Is Not Valid.");
                        Console.ResetColor();
                    }
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee ID is required.");
                    Console.ResetColor();
                }
            }
        }
    }
}
