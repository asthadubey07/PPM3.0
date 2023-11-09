using NUnit.Framework;
using PPM.Domain;
using PPM.Model;

namespace PPM.Unit.Testing
{
    [TestFixture]
    public class EmployeeTest
    {
        public EmployeeRepository employeeManager;

        //TEST CASE ADD EMPLOYEE METHOD
        [Test]
        public void AddEmployee()
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "1",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };
            // Act
            employeeManager.AddEmployee(employee);

            // Assert
            List<Employee> employees = employeeManager.GetEmployees(); // Modify this method as per your implementation
            Assert.Contains(employee, employees);
        }

        [TestCase("3453464")]
        public void Unique_EmployeeId(string employeeId)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isUnique = employeeManager.IsEmployeeIdUnique(employeeId);

            // Assert
            Assert.IsTrue(isUnique);
        }

        [TestCase("1")]
        public void NotUnique_EmployeeId(string employeeId)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "1",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };
            employeeManager.AddEmployee(employee);

            // Act
            bool isUnique = employeeManager.IsEmployeeIdUnique(employeeId);

            // Assert
            Assert.IsFalse(isUnique);
        }

        [TestCase("5")]
        public void Valid_EmployeeId(string employeeId)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isValid = employeeManager.ValidateEmployeeId(employeeId);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_EmployeeId(string employeeId)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isNullOrEmpty = employeeManager.ValidateEmployeeId(employeeId);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestCase("Astha")]
        public void Valid_EmployeeFirstName(string firstName)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isValid = employeeManager.ValidateEmployeeFirstName(firstName);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_EmployeeFirstName(string firstName)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isNullOrEmpty = employeeManager.ValidateEmployeeFirstName(firstName);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestCase("DUBEY")]
        public static void Valid_EmployeeLastName(string lastName)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isValid = employeeManager.ValidateEmployeeLastName(lastName);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_EmployeeLastName(string lastName)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isNullOrEmpty = employeeManager.ValidateEmployeeLastName(lastName);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestCase("Asthadubey@gmail.com")]
        public void Valid_Email(string Email)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isValid = employeeManager.IsValidEmail(Email);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_Email(string Email)
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository();

            // Act
            bool isNullOrEmpty = employeeRepository.IsValidEmail(Email);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestCase("9131770898")]
        public void Valid_PhoneNumber(string PhoneNumber)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isValid = employeeManager.IsValidPhoneNumber(PhoneNumber);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_PhoneNumber(string PhoneNumber)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            bool isNullOrEmpty = employeeManager.IsValidPhoneNumber(PhoneNumber);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        [TestCase("bhopal")]
        public void Valid_Address(string Address)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            // Act
            bool isValid = employeeManager.ValidateEmployeeAddress(Address);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InValid_Address(string Address)
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            // Act
            bool isNullOrEmpty = employeeManager.ValidateEmployeeAddress(Address);

            // Assert
            Assert.IsFalse(isNullOrEmpty);
        }

        //TEST CASE FOR  VIEW ROLES
        [Test]
        public void ViewEmployees_ReturnsEmptyList()
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();

            // Act
            var employees = employeeManager.GetEmployees();

            // Assert
            Assert.IsNotNull(employees);
            Assert.That(employees.Count, Is.EqualTo(0));
        }

        [Test]
        public void ViewEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "1",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };

            // Act
            employeeManager.AddEmployee(employee);
            var employees = employeeManager.GetEmployees();

            // Assert
            Assert.IsNotNull(employees);
            Assert.IsInstanceOf<List<Employee>>(employees);
        }

        [Test]
        public void Get_ExistingEmployee_ById()
        {
            // Arrange:
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "1",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };
           employeeManager.AddEmployee(employee);

            // Act:
            Employee result = EmployeeRepository.GetEmployeeById("1");

            // Assert:
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(employee.Id));
            Assert.That(result.FristName, Is.EqualTo(employee.FristName));
            Assert.That(result.LastName, Is.EqualTo(employee.LastName));
            Assert.That(result.Email, Is.EqualTo(employee.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(employee.PhoneNumber));
            Assert.That(result.Address, Is.EqualTo(employee.Address));
            Assert.That(result.RoleId, Is.EqualTo(employee.RoleId));
        }

        [Test]
        public void Get_NonExistingEmployee_ById()
        {
            // Arrange
            EmployeeRepository employeeManager = new EmployeeRepository();
            string employeeId = "5684";

            // Act
            Employee result = EmployeeRepository.GetEmployeeById(employeeId);

            // Assert
            Assert.IsNull(result);
        }
    }
}
