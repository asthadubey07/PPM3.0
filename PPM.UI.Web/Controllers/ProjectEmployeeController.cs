using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PPM.Model;

namespace PPM.UI.Web.Controllers
{
    public class ProjectEmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProjectEmployeeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5143/");
        }

        private bool IsUserAuthorized(string requiredRole)
        {
            // Check user role using session information
            var userRole = HttpContext.Session.GetString("userRole");
            return userRole == requiredRole;
        }

        [HttpGet]
        public async Task<IActionResult> EmployessInProject(int id)
        {
            try
            {
                if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync(
                        $"api/ProjectEmployee/GetEmployeesInProject/employees-in-project/{id}"
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var employeesJson = await response.Content.ReadAsStringAsync();
                        var employees = JsonConvert.DeserializeObject<List<Employee>>(
                            employeesJson
                        );
                        return View(employees);
                    }

                    // Handle errors if needed
                    return View("Error");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProjectsForEmployee(int id)
        {
            try
            {
                if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync(
                        $"api/ProjectEmployee/GetProjectsForEmployee/projects-for-employee/{id}"
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var projectsJson = await response.Content.ReadAsStringAsync();
                        var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);
                        return View(projects);
                    }
                }

                // Handle errors if needed
                return View("Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesByRole(int id)
        {
            try
            {
                if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync(
                        $"api/ProjectEmployee/GetEmployeesByRole/employees-by-role/{id}"
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var employeesJson = await response.Content.ReadAsStringAsync();
                        var employees = JsonConvert.DeserializeObject<List<Employee>>(
                            employeesJson
                        );
                        return View(employees);
                    }
                }
                // Handle errors if needed
                return View("Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddEmployeeConfirmation(int projectId)
        {
            // Redirect to AddEmployeeToProject action with the projectId
            return RedirectToAction("AddEmployeeToProject", new { projectId });
        }

        [HttpGet]
        public IActionResult AddEmployeeToProject()
        {
            return View();
        }

        public async Task<IActionResult> AddEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                // Check user role using session information
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.PostAsync(
                        $"api/ProjectEmployee/AddEmployeeToProject/AddEmployeeToProject?projectId={projectId}&employeeId={employeeId}",
                        null
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        TempData["SuccessMessage"] = "Employee added to project successfully.";
                        return RedirectToAction("index", "Project");
                    }
                    else
                    {
                        TempData["ErrorMessage"] =
                            $"Failed To Add Employee to Project: {response.ReasonPhrase}";
                        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult DeleteEmployeeFromProject()
        {
            return View();
        }

        public async Task<IActionResult> DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.DeleteAsync(
                        $"api/ProjectEmployee/DeleteEmployeeFromProject/DeleteEmployeeFromProject?projectId={projectId}&employeeId={employeeId}"
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        TempData["SuccessMessage"] = "Employee deleted from project successfully.";
                        return RedirectToAction("index", "Employee");
                    }
                    else
                    {
                        TempData["ErrorMessage"] =
                            $"Failed to delete Employee from Project: {response.ReasonPhrase}";
                        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
