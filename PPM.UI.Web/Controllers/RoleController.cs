using System.Net;
using PPM.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PPM.Model;

namespace PPM.UI.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly RoleDal _roledal;

        public RoleController(HttpClient httpClient, RoleDal roledal)
        {
            _httpClient = httpClient;
            _roledal = roledal;

            _httpClient.BaseAddress = new Uri("http://localhost:5143/");
        }

        private bool IsUserAuthorized(string requiredRole)
        {
            // Check user role using session information
            var userRole = HttpContext.Session.GetString("userRole");
            return userRole == requiredRole;
        }

        public bool IsRoleIdUnique(int roleId)
        {
            return !_roledal.IsRoleIdUnique(roleId);
        }

        public bool IsRoleNameUnique(string roleName)
        {
            return !_roledal.IsRoleNameUnique(roleName);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (IsUserAuthorized("Admin"))
                {
                    var response = await _httpClient.GetAsync("api/Role/GetAllRoles");

                    if (response.IsSuccessStatusCode)
                    {
                        var roles = await response.Content.ReadFromJsonAsync<List<Role>>();
                        return View(roles);
                    }

                    // Handle errors if needed
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int roleId)
        {
            try
            {
                if (IsUserAuthorized("Admin") || IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync($"api/Role/GetRoleById/{roleId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var roleJson = await response.Content.ReadAsStringAsync();
                        var role = JsonConvert.DeserializeObject<Role>(roleJson);

                        if (role != null)
                        {
                            return View(role);
                        }
                        else
                        {
                            return View("NotFound");
                        }
                    }

                    // Handle errors if needed
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
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
                if (IsUserAuthorized("Admin"))
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
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                if (IsUserAuthorized("Admin"))
                {
                    // Validate model
                    if (!ModelState.IsValid)
                    {
                        return View(role);
                    }

                    // Check if RoleId is unique
                    if (IsRoleIdUnique(role.RoleId))
                    {
                        ModelState.AddModelError("RoleId", "Role ID must be unique.");
                        return View(role);
                    }

                    // Check if RoleName is unique
                    if (IsRoleNameUnique(role.Name))
                    {
                        ModelState.AddModelError("Name", "Role Name must be unique.");
                        return View(role);
                    }

                    // Add any additional validation logic here

                    var response = await _httpClient.PostAsJsonAsync("api/Role/CreateRole", role);

                    if (response.IsSuccessStatusCode)
                    {
                        var createdRole = await response.Content.ReadFromJsonAsync<Role>();
                        TempData["SuccessMessage"] = "Role Created Successfully!";
                        return RedirectToAction("Details", new { roleId = createdRole?.RoleId });
                    }

                    TempData["ErrorMessage"] = "Failed to create role. Please try again.";
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
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
                if (IsUserAuthorized("Admin"))
                {
                    var response = await _httpClient.GetAsync($"api/Role/GetRoleById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var roleJson = await response.Content.ReadAsStringAsync();
                        var role = JsonConvert.DeserializeObject<Role>(roleJson);

                        if (role != null)
                        {
                            return View(role);
                        }
                        else
                        {
                            return View("RoleNotFound");
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        // Handle other HTTP status codes if needed
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
        public async Task<IActionResult> Edit(int id, Role updatedRole)
        {
            try
            {
                if (IsUserAuthorized("Admin"))
                {
                    var response = await _httpClient.PutAsJsonAsync(
                        $"api/Role/UpdateRole/{id}",
                        updatedRole
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Role Updated Successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to Update role. Please try again.";
                        return View("Error");
                    }
                }

                // Redirect unauthorized users to the home page
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
                if (IsUserAuthorized("Admin"))
                {
                    var response = await _httpClient.GetAsync($"api/Role/GetRoleById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var roleJson = await response.Content.ReadAsStringAsync();
                        var role = JsonConvert.DeserializeObject<Role>(roleJson);

                        if (role != null)
                        {
                            return View(role);
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
        public async Task<IActionResult> ConfirmDelete(int roleId)
        {
            try
            {
                if (IsUserAuthorized("Admin"))
                {
                    var response = await _httpClient.DeleteAsync($"api/Role/DeleteRole/{roleId}");

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Role Deleted Successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["ErrorMessage"] = "Failed to delete role. Please try again.";
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
