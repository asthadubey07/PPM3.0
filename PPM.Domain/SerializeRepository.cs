using System.Xml.Serialization;
using PPM.Model;

namespace PPM.Domain
{
    public class AppDataSerializer
    {
        /// <summary>
        /// Serializes project, employee, and role data to XML files.
        /// </summary>
        /// <param name="projects">List of projects to serialize.</param>
        /// <param name="employees">List of employees to serialize.</param>
        /// <param name="roles">List of roles to serialize.</param>
        /// <param name="projectPath">Path to the project XML file.</param>
        /// <param name="employeePath">Path to the employee XML file.</param>
        /// <param name="rolePath">Path to the role XML file.</param>
        public static void SerializeData(
            List<Project> projects,
            List<Employee> employees,
            List<Role> roles,
            string projectPath,
            string employeePath,
            string rolePath)
        {
            // Serialize projects to XML file.
            using (var projectWriter = new StreamWriter(projectPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Project>));
                serializer.Serialize(projectWriter, projects);
            }

            // Serialize employees to XML file.
            using (var employeeWriter = new StreamWriter(employeePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
                serializer.Serialize(employeeWriter, employees);
            }

            // Serialize roles to XML file.
            using (var roleWriter = new StreamWriter(rolePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
                serializer.Serialize(roleWriter, roles);
            }
        }
    }
}
