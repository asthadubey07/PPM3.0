using System.Xml.Serialization;
using PPM.Model;

namespace PPM.Domain
{
    public class AppDataSerializer
    {
        public static void SerializeData(
            List<Project> projects,
            List<Employee> employees,
            List<Role> roles,
            string projectPath,
            string employeePath,
            string rolePath)
        {
            using (var projectWriter = new StreamWriter(projectPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Project>));
                serializer.Serialize(projectWriter, projects);
            }

            using (var employeeyWriter = new StreamWriter(employeePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
                serializer.Serialize(employeeyWriter, employees);
            }
            using (var roleWriter = new StreamWriter(rolePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
                serializer.Serialize(roleWriter, roles);
            }
        }
    }
}