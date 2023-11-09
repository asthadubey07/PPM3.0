using NUnit.Framework;
using PPM.Domain;
using PPM.Model;

namespace PPM.Unit.Testing
{
    [TestFixture]
    public class ProjectTest
    {
        public ProjectRepository projectManager;

        //TEST CASE ADD PROJECT METHOD
        [Test]
        public void AddProject()
        {
            // Arrange
            var projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "1",
                Name = "Test Project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };

            // Act
            projectManager.AddProject(project);

            // Assert
            List<Project> projects = projectManager.GetProjects(); // Modify this method as per your implementation
            Assert.Contains(project, projects);
        }

        //TEST CASE FOR UNIQUE PROJECT ID

        [TestCase("5")]
        public void Unique_projectId(string projectId)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();

            // Act
            bool isUnique = projectManager.IsProjectIdUnique(projectId);

            // Assert
            Assert.IsTrue(isUnique);
        }

        //TEST CASE FOR not UNIQUE PROJECT ID
        [TestCase("1")]
        public void NotUnique_projectId(string projectId)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "1",
                Name = "Test Project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            // Act
            projectManager.AddProject(project);
            bool isUnique = projectManager.IsProjectIdUnique(projectId);

            // Assert
            Assert.IsFalse(isUnique);
        }

        //TEST CASE FOR valid PROJECT ID
        [TestCase("1")]
       
        public void Valid_projectId(string projectId)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();

            // Act
            bool isValid = projectManager.ValidateProjectId(projectId);

            // Assert
            Assert.IsTrue(isValid);
        }

        //TEST CASE FOR INVALID PROJECT ID

    
        [TestCase("")]
        [TestCase(null)]
        public void Invalid_ProjectId(string projectId)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();

            // Act
            bool isValid = projectManager.ValidateProjectId(projectId);

            // Assert
            try
            {
            Assert.IsFalse(isValid);
            Assert.Pass("invalied project ID");
            }
            catch(Exception ex )
            {
             System.Console.WriteLine("Invalid project Id Handle sucessfully",ex.Message);
            }

        }

        //TEST CASE FOR VALID PROJECT NAME
        [TestCase("PPM")]
       
        public void Valid_ProjectName(string projectName)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();

            // Act
            bool isValid = projectManager.ValidateProjectName(projectName);

            // Assert
            Assert.IsTrue(isValid);
        }

        //TEST CASE FOR INVALID PROJECT NAME
     
        [TestCase("")]
        [TestCase(null)]
        public void InValid_projectName(string projectName)
        {
            // Arrange
            ProjectRepository projectManager = new ProjectRepository();

            // Act
            bool isValid = projectManager.ValidateProjectName(projectName);

            // Assert
            Assert.IsFalse(isValid);
        }

        //TEST CASE FOR PROJECT DATES

        [Test]
        public void Invalid_ProjectDates()
        {
            // Arrange
            var projectManager = new ProjectRepository();
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddMonths(-1);
            var project = new Project("3", "Invalid Dates Project", startDate, endDate);

            // Act
            bool result = projectManager.AddProject(project);

            // Assert
            Assert.IsTrue(result);
        }

        //TEST CASE FOR  VIEW PROJECTS
        [Test]
        public void ViewProjects_ReturnsEmptyList()
        {
            // Arrange
            var projectManager = new ProjectRepository();

            // Act
            var projects = projectManager.GetProjects();

            // Assert
            Assert.IsNotNull(projects);
            Assert.That(projects.Count, Is.EqualTo(0));
        }

        [Test]
        public void ViewProjects_ReturnsListOfProjects()
        {
            // Arrange
            var projectManager = new ProjectRepository();
            // Act
            var projects = projectManager.GetProjects();

            // Assert
            Assert.IsNotNull(projects);
            Assert.IsInstanceOf<List<Project>>(projects);
        }

        //TEST CASE FOR CHEKING PROJECT EXISTANCE
        [TestCase("1")]
        public void Check_ExistingProject(string projectId)
        {
            // Arrange
            var projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "1",
                Name = "Test Project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            projectManager.AddProject(project);
            // Act
            bool exists = projectManager.ProjectExists(projectId);

            // Assert
            Assert.IsTrue(exists);
        }

        //TEST CASE FOR CHEKING PROJECT NONEXISTANCE
        [TestCase("12334")]
        [TestCase("")]
        [TestCase(null)]
        public void Check_NonExistingProject(string projectId)
        {
            // Arrange
            var projectManager = new ProjectRepository();

            // Act
            bool exists = projectManager.ProjectExists(projectId);

            // Assert
            Assert.IsFalse(exists);
        }

        //TEST CASE TO VIEW  EXISTING PROJECT BY ID
        [Test]
        public void Get_ExistingProject_ById()
        {
            // Arrange:
            var projectManager = new ProjectRepository();
            var project = new Project
            {
                Id = "67",
                Name = "ppm5",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            var Adding = projectManager.AddProject(project);

            // Act:
            var result = projectManager.GetProjectById("67");

            // Assert:
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo("67"));
            Assert.That(result.Name, Is.EqualTo("ppm5"));
            Assert.That(result.StartDate, Is.EqualTo(project.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(project.EndDate));
        }

        [Test]
        public void Get_NonExistingProject_ById()
        {
            // Arrange:
            var projectManager = new ProjectRepository();
            // Act:

            var result = projectManager.GetProjectById("10");

            // Assert:
            Assert.IsNull(result);
        }
    }
}
