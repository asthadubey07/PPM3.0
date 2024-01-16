using PPM.Model;

using System.Collections.Generic;
namespace PPM.API;
public interface IProjectEmployeeDal
{
   
    bool AddEmployeeToProject(int projectId, int employeeId);
    void DeleteEmployeeFromProject(int projectId, int employeeId);
 IEnumerable<Employee> GetEmployeesInProject(int projectId);
    IEnumerable<Project> GetProjectsForEmployee(int employeeId);
    IEnumerable<Employee> GetEmployeesByRole(int roleId);

}