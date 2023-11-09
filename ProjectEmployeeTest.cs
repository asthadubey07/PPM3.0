using NUnit.Framework;
using PPM.Domain;
using PPM.Model;

namespace PPM.Unit.Testing
{
    [TestFixture]
    public class ProjectEmployeeTest
    {
        public ProjectEmployeeRepository projectEmployeeManager;

        [TestCase("2", "2")]
        public void AddEmployeeToProject_ValidData_ReturnsTrue(string projectId, string employeeId)
        {
            // Arrange
            var projectEmployeeManager = new ProjectEmployeeRepository();
            var projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "2",
                Name = "Test Project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            projectManager.AddProject(project);
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "2",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };
            employeeManager.AddEmployee(employee);

            // Act
            List<Project> projects = projectManager.GetProjects();
            List<Employee> employees = employeeManager.GetEmployees();
            bool result = projectEmployeeManager.AddEmployeeToProject(projectId, employeeId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("", "2")]
        [TestCase("2", "")]
        [TestCase("", "")]
        [TestCase(null, "2")]
        [TestCase("2", null)]
        [TestCase(null, null)]
        [TestCase("35462354", "2")]
        public void AddEmployeeToProject_InValidData_ReturnsFalse(
            string projectId,
            string employeeId
        )
        {
            // Arrange
            var projectEmployeeManager = new ProjectEmployeeRepository();
            var projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "2",
                Name = "Test Project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            projectManager.AddProject(project);
            EmployeeRepository employeeManager = new EmployeeRepository();
            Employee employee = new Employee
            {
                Id = "2",
                FristName = "Astha",
                LastName = "Dubey",
                Email = "Astha@gmail.com",
                PhoneNumber = "1234567890",
                Address = "bhopal",
                RoleId = "1"
            };
            employeeManager.AddEmployee(employee);

            // Act
            List<Project> projects = projectManager.GetProjects();
            List<Employee> employees = employeeManager.GetEmployees();
            bool result = projectEmployeeManager.AddEmployeeToProject(projectId, employeeId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("54645765", "1")]
        public void AddEmployeeToProject_ProjectNotExists_ReturnsFalse(
            string projectId,
            string employeeId
        )
        {
            // Arrange
            var projectEmployeeManager = new ProjectEmployeeRepository();
            // Act
            bool result = projectEmployeeManager.AddEmployeeToProject(projectId, employeeId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("1", "113454657")]
        public void AddEmployeeToProject_EmployeeNotExists_ReturnsFalse(
            string projectId,
            string employeeId
        )
        {
            // Arrange
            var projectEmployeeManager = new ProjectEmployeeRepository();
            // Act
            bool result = projectEmployeeManager.AddEmployeeToProject(projectId, employeeId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
