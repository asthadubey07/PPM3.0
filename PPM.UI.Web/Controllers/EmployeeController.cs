using System.Net;
using PPM.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PPM.Model;

namespace PPM.UI.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly EmployeeDal _employeeDal;

    public EmployeeController(HttpClient httpClient)
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
    public async Task<IActionResult> Index()
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.GetAsync("api/Employee/GetAllEmployees");

                if (response.IsSuccessStatusCode)
                {
                    var employeesJson = await response.Content.ReadAsStringAsync();
                    var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);

                    return View(employees);
                }
                else
                {
                    return View("Error");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.GetAsync($"api/Employee/GetEmployeeById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var employeeJson = await response.Content.ReadAsStringAsync();
                    var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);

                    if (employee != null)
                    {
                        return View(employee);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else
                {
                    return View("Error");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                return View();
            }

            // Redirect unauthorized users to the home page
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                // Validate model
                if (!ModelState.IsValid)
                {
                    return View(employee);
                }

                var response = await _httpClient.PostAsJsonAsync(
                    "api/Employee/CreateEmployee",
                    employee
                );

                if (response.IsSuccessStatusCode)
                {
                    var createdEmployee = await response.Content.ReadFromJsonAsync<Employee>();
                    TempData["SuccessMessage"] = "Employee created successfully.";
                    return RedirectToAction("Details", new { id = createdEmployee?.EmployeeId });
                }

                TempData["ErrorMessage"] = "Failed to create Employee. Please try again.";
                return View("Error");
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.GetAsync($"api/Employee/GetEmployeeById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var employeeJson = await response.Content.ReadAsStringAsync();
                    var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);

                    if (employee != null)
                    {
                        return View(employee);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Error");
                }
            }

            // Redirect unauthorized users to the home page
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Employee updatedEmployee)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.PutAsJsonAsync(
                    $"api/Employee/UpdateEmployee/{id}",
                    updatedEmployee
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Employee updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to updated Employee. Please try again.";
                    return View("Error");
                }
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.GetAsync($"api/Employee/GetEmployeeById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var employeeJson = await response.Content.ReadAsStringAsync();
                    var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);

                    if (employee != null)
                    {
                        return View(employee);
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Error");
                }
            }

            // Redirect unauthorized users to the home page
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(int employeeId)
    {
        try
        {
            if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
            {
                var response = await _httpClient.DeleteAsync(
                    $"api/Employee/DeleteEmployee/{employeeId}"
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Employee Deleted Successfully.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Failed to Delete Employee. Please try again.";
                return View("Error");
            }

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
