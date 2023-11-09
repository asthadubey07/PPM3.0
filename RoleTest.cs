using NUnit.Framework;
using PPM.Domain;
using PPM.Model;

namespace PPM.Unit.Testing
{
    [TestFixture]
    public class RoleTest
    {
        public RoleRepository roleManager;

        //TEST CASE ADD ROLE METHOD
        [Test]
        public void AddRole()
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            var role = new Role { Id = "1", Name = "Tester", };

            // Act
            roleManager.AddRole(role);

            // Assert
            List<Role> roles = roleManager.GetRoles(); // Modify this method as per your implementation
            Assert.Contains(role, roles);
        }

        //TEST CASE FOR UNIQUE ROLE ID

        [TestCase("5")]
        public void Unique_RoleId(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool isUnique = roleManager.IsRoleIdUnique(roleId);

            // Assert
            Assert.IsTrue(isUnique);
        }

        //TEST CASE FOR not UNIQUE ROLE ID
        [TestCase("1")]
        public void NotUnique_projectId(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();
            var role = new Role { Id = "1", Name = "tester", };
            // Act
            roleManager.AddRole(role);
            bool isUnique = roleManager.IsRoleIdUnique(roleId);

            // Assert
            Assert.IsFalse(isUnique);
        }

        //TEST CASE FOR valid ROLE ID
        [TestCase("1")]
      
        public static void Valid_RoleId(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool isValid = RoleRepository.ValidateRoleId(roleId);

            // Assert
            Assert.IsTrue(isValid);
        }

        //TEST CASE FOR INVALID ROLE ID

    
        [TestCase("")]
        [TestCase(null)]
        public static void Invalid_RoleId(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool isValid = RoleRepository.ValidateRoleId(roleId);

            // Assert
            Assert.IsFalse(isValid);
        }

        //TEST CASE FOR VALID ROLE NAME
        [TestCase("PPM")]
        public static void Valid_RoleName(string roleName)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool isValid = RoleRepository.ValidateRoleName(roleName);

            // Assert
            Assert.IsTrue(isValid);
        }

        //TEST CASE FOR INVALID ROLE NAME
      
        [TestCase("")]
        [TestCase(null)]
        public static void InValid_RoleName(string roleName)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool isValid = RoleRepository.ValidateRoleName(roleName);

            // Assert
            Assert.IsFalse(isValid);
        }

        //TEST CASE FOR  VIEW ROLES
        [Test]
        public void ViewRoles_ReturnsEmptyList()
        {
            // Arrange
            var roleManager = new RoleRepository();

            // Act
            var roles = roleManager.GetRoles();

            // Assert
            Assert.IsNotNull(roles);
            Assert.That(roles.Count, Is.EqualTo(0));
        }

        [Test]
        public void ViewRoles_ReturnsListOfRoles()
        {
            // Arrange
            var roleManager = new RoleRepository();
            var role5 = new Role { Id = "6", Name = "Manager", };
            // Act
            roleManager.AddRole(role5);
            var roles = roleManager.GetRoles();

            // Assert
            Assert.IsNotNull(roles);
            Assert.IsInstanceOf<List<Role>>(roles);
        }

        //TEST CASE FOR CHEKING PROJECT EXISTANCE
        [TestCase("1")]
       
        public void Check_ExistingRole(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();
            var role = new Role { Id = "1", Name = "developer" };
            roleManager.AddRole(role);
            // Act
            bool exists = roleManager.RoleExists(roleId);

            // Assert
            Assert.IsTrue(exists);
        }

        //TEST CASE FOR CHEKING PROJECT NONEXISTANCE
        [TestCase("")]
        [TestCase(null)]
        public void Check_NonExistingRole(string roleId)
        {
            // Arrange
            RoleRepository roleManager = new RoleRepository();

            // Act
            bool exists = roleManager.RoleExists(roleId);

            // Assert
            Assert.IsFalse(exists);
        }
    }
}
